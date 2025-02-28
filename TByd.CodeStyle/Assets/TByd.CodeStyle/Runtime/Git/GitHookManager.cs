using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Git
{
    /// <summary>
    /// Git钩子管理器，用于管理Git钩子的安装、卸载和状态监控
    /// </summary>
    public static class GitHookManager
    {
        // 钩子模板目录
        private const string c_HookTemplatesDir = "Packages/com.tbyd.codestyle/PackageResources/GitHooks";
        
        // 钩子状态变更事件
        public static event Action<GitHookType, bool> HookStatusChanged;
        
        // 已安装的钩子缓存
        private static Dictionary<GitHookType, bool> s_InstalledHooksCache = new Dictionary<GitHookType, bool>();
        
        // 是否已初始化
        private static bool s_Initialized;
        
        // Git仓库
        private static GitRepository s_Repository;
        
        /// <summary>
        /// 初始化Git钩子管理器
        /// </summary>
        public static void Initialize()
        {
            if (s_Initialized)
                return;
                
            // 检查项目是否是Git仓库
            if (!GitRepository.IsProjectGitRepository())
            {
                Debug.LogWarning("[TByd.CodeStyle] 项目不是Git仓库，Git钩子管理器初始化失败");
                // 确保仓库和缓存为空
                s_Repository = null;
                s_InstalledHooksCache.Clear();
                return;
            }
            
            try
            {
                // 获取项目Git仓库
                s_Repository = GitRepository.GetProjectRepository();
                
                // 刷新钩子状态缓存
                RefreshHookStatusCache();
                
                s_Initialized = true;
                
                Debug.Log("[TByd.CodeStyle] Git钩子管理器初始化成功");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] Git钩子管理器初始化失败: {e.Message}");
                s_Repository = null;
                s_InstalledHooksCache.Clear();
            }
        }
        
        /// <summary>
        /// 重新初始化Git钩子管理器
        /// </summary>
        /// <remarks>
        /// 当Git仓库路径变更时调用此方法重新初始化
        /// </remarks>
        public static void Reinitialize()
        {
            s_Initialized = false;
            s_Repository = null;
            s_InstalledHooksCache.Clear();
            Initialize();
        }
        
        /// <summary>
        /// 刷新钩子状态缓存
        /// </summary>
        public static void RefreshHookStatusCache()
        {
            // 如果未初始化或仓库无效，则清空缓存并返回
            if (!s_Initialized || s_Repository == null || !s_Repository.IsValid)
            {
                s_InstalledHooksCache.Clear();
                return;
            }
            
            try
            {
                s_InstalledHooksCache.Clear();
                
                // 检查所有钩子类型
                foreach (GitHookType hookType in Enum.GetValues(typeof(GitHookType)))
                {
                    string hookFileName = hookType.GetFileName();
                    bool isInstalled = s_Repository.IsManagedHook(hookFileName);
                    
                    s_InstalledHooksCache[hookType] = isInstalled;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 刷新钩子状态缓存失败: {e.Message}");
                s_InstalledHooksCache.Clear();
            }
        }
        
        /// <summary>
        /// 检查钩子是否已安装
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>是否已安装</returns>
        public static bool IsHookInstalled(GitHookType _hookType)
        {
            if (s_Repository == null || !s_Repository.IsValid)
                return false;
                
            // 如果缓存中没有，则刷新缓存
            if (!s_InstalledHooksCache.ContainsKey(_hookType))
            {
                RefreshHookStatusCache();
            }
            
            return s_InstalledHooksCache.TryGetValue(_hookType, out bool isInstalled) && isInstalled;
        }
        
        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>是否安装成功</returns>
        public static bool InstallHook(GitHookType _hookType)
        {
            // 如果未初始化或仓库无效，则返回失败
            if (!s_Initialized || s_Repository == null || !s_Repository.IsValid)
            {
                Debug.LogError("[TByd.CodeStyle] Git仓库无效，无法安装钩子");
                return false;
            }
            
            string hookFileName = _hookType.GetFileName();
            string hookPath = s_Repository.GetHookPath(hookFileName);
            
            try
            {
                // 检查是否已存在非托管钩子
                if (s_Repository.HookExists(hookFileName) && !s_Repository.IsManagedHook(hookFileName))
                {
                    // 备份现有钩子
                    string backupPath = hookPath + ".backup";
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }
                    
                    File.Move(hookPath, backupPath);
                    Debug.Log($"[TByd.CodeStyle] 已备份现有钩子: {hookPath} -> {backupPath}");
                }
                
                // 获取钩子模板
                string hookContent = GetHookTemplate(_hookType);
                
                // 写入钩子文件
                File.WriteAllText(hookPath, hookContent);
                
                // 设置执行权限
                SetExecutablePermission(hookPath);
                
                // 更新缓存
                s_InstalledHooksCache[_hookType] = true;
                
                // 触发事件
                HookStatusChanged?.Invoke(_hookType, true);
                
                Debug.Log($"[TByd.CodeStyle] 已安装钩子: {hookFileName}");
                
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 安装钩子失败: {e.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>是否卸载成功</returns>
        public static bool UninstallHook(GitHookType _hookType)
        {
            // 如果未初始化或仓库无效，则返回失败
            if (!s_Initialized || s_Repository == null || !s_Repository.IsValid)
            {
                Debug.LogError("[TByd.CodeStyle] Git仓库无效，无法卸载钩子");
                return false;
            }
            
            string hookFileName = _hookType.GetFileName();
            string hookPath = s_Repository.GetHookPath(hookFileName);
            
            try
            {
                // 检查钩子是否存在且由TByd.CodeStyle管理
                if (!s_Repository.HookExists(hookFileName) || !s_Repository.IsManagedHook(hookFileName))
                {
                    Debug.LogWarning($"[TByd.CodeStyle] 钩子不存在或不是由TByd.CodeStyle管理: {hookFileName}");
                    return false;
                }
                
                // 删除钩子文件
                File.Delete(hookPath);
                
                // 检查是否有备份，如果有则恢复
                string backupPath = hookPath + ".backup";
                if (File.Exists(backupPath))
                {
                    File.Move(backupPath, hookPath);
                    Debug.Log($"[TByd.CodeStyle] 已恢复备份钩子: {backupPath} -> {hookPath}");
                }
                
                // 更新缓存
                s_InstalledHooksCache[_hookType] = false;
                
                // 触发事件
                HookStatusChanged?.Invoke(_hookType, false);
                
                Debug.Log($"[TByd.CodeStyle] 已卸载钩子: {hookFileName}");
                
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 卸载钩子失败: {e.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 获取钩子模板
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>钩子模板内容</returns>
        private static string GetHookTemplate(GitHookType _hookType)
        {
            // 首先尝试从包路径获取模板
            string templatePath = Path.Combine(c_HookTemplatesDir, _hookType.GetFileName() + ".template");
            
            // 检查包路径模板文件是否存在
            if (File.Exists(templatePath))
            {
                Debug.Log($"[TByd.CodeStyle] 从包路径加载钩子模板: {templatePath}");
                return File.ReadAllText(templatePath);
            }
            
            // 如果包路径不存在，尝试从项目路径获取模板
            string projectTemplatePath = Path.Combine(Application.dataPath, "TByd.CodeStyle/PackageResources/GitHooks", _hookType.GetFileName() + ".template");
            
            // 检查项目路径模板文件是否存在
            if (File.Exists(projectTemplatePath))
            {
                Debug.Log($"[TByd.CodeStyle] 从项目路径加载钩子模板: {projectTemplatePath}");
                return File.ReadAllText(projectTemplatePath);
            }
            
            // 如果模板文件不存在，则生成默认模板
            Debug.LogWarning($"[TByd.CodeStyle] 未找到钩子模板文件，使用默认模板: {_hookType.GetFileName()}");
            return GenerateDefaultHookTemplate(_hookType);
        }
        
        /// <summary>
        /// 生成默认钩子模板
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>默认钩子模板内容</returns>
        private static string GenerateDefaultHookTemplate(GitHookType _hookType)
        {
            StringBuilder sb = new StringBuilder();
            
            // 添加shebang
            sb.AppendLine("#!/bin/sh");
            sb.AppendLine();
            
            // 添加生成标记
            sb.AppendLine("# Generated by TByd.CodeStyle");
            sb.AppendLine("# 请勿手动修改此文件，它会在TByd.CodeStyle更新时被覆盖");
            sb.AppendLine();
            
            // 添加钩子描述
            sb.AppendLine($"# {_hookType.GetDescription()}");
            sb.AppendLine();
            
            // 根据钩子类型添加不同的内容
            switch (_hookType)
            {
                case GitHookType.PreCommit:
                    sb.AppendLine("# 检查是否在Unity编辑器中运行");
                    sb.AppendLine("if [ -n \"$UNITY_EDITOR\" ]; then");
                    sb.AppendLine("    echo \"在Unity编辑器中运行Git钩子\"");
                    sb.AppendLine("    # 在Unity编辑器中执行代码检查");
                    sb.AppendLine("    exit 0");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 获取暂存的文件列表");
                    sb.AppendLine("files=$(git diff --cached --name-only --diff-filter=ACM | grep \"\\.cs$\")");
                    sb.AppendLine();
                    sb.AppendLine("# 如果没有C#文件，则跳过检查");
                    sb.AppendLine("if [ -z \"$files\" ]; then");
                    sb.AppendLine("    echo \"没有需要检查的C#文件\"");
                    sb.AppendLine("    exit 0");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("echo \"TByd.CodeStyle: 正在检查代码风格...\"");
                    sb.AppendLine("echo \"TByd.CodeStyle: 钩子已触发\"");
                    sb.AppendLine("# 这里可以添加代码风格检查的逻辑");
                    sb.AppendLine("# 如果检查失败，返回非零值");
                    sb.AppendLine("exit 0");
                    break;
                    
                case GitHookType.CommitMsg:
                    sb.AppendLine("# 获取提交消息文件路径");
                    sb.AppendLine("commit_msg_file=$1");
                    sb.AppendLine();
                    sb.AppendLine("# 读取提交消息");
                    sb.AppendLine("commit_msg=$(cat $commit_msg_file)");
                    sb.AppendLine();
                    sb.AppendLine("echo \"TByd.CodeStyle: 正在检查提交消息格式...\"");
                    sb.AppendLine();
                    sb.AppendLine("# 检查提交消息是否为空");
                    sb.AppendLine("if [ -z \"$commit_msg\" ]; then");
                    sb.AppendLine("    echo \"错误: 提交消息不能为空\"");
                    sb.AppendLine("    exit 1");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 移除注释行");
                    sb.AppendLine("clean_msg=$(echo \"$commit_msg\" | grep -v \"^#\")");
                    sb.AppendLine();
                    sb.AppendLine("# 检查清理后的提交消息是否为空");
                    sb.AppendLine("if [ -z \"$clean_msg\" ]; then");
                    sb.AppendLine("    echo \"错误: 提交消息不能只包含注释\"");
                    sb.AppendLine("    exit 1");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 如果是在Unity编辑器中运行，则使用C#代码进行更高级的检查");
                    sb.AppendLine("# 但我们不会直接退出，而是继续进行基本检查");
                    sb.AppendLine("if [ -n \"$UNITY_EDITOR\" ]; then");
                    sb.AppendLine("    echo \"在Unity编辑器中运行Git钩子，使用高级提交消息检查\"");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 检查提交消息格式");
                    sb.AppendLine("# 格式: <type>(<scope>): <subject>");
                    sb.AppendLine("# 例如: feat(ui): 添加登录界面");
                    sb.AppendLine();
                    sb.AppendLine("# 提取类型和作用域");
                    sb.AppendLine("type_scope_subject=$(echo \"$clean_msg\" | head -1)");
                    sb.AppendLine("type=$(echo \"$type_scope_subject\" | grep -oE \"^(feat|fix|docs|style|refactor|perf|test|build|ci|chore|revert)\")");
                    sb.AppendLine("scope=$(echo \"$type_scope_subject\" | grep -oE \"\\([a-zA-Z0-9_\\-\\.]+\\)\" | tr -d \"()\")");
                    sb.AppendLine("subject=$(echo \"$type_scope_subject\" | sed -E \"s/^(feat|fix|docs|style|refactor|perf|test|build|ci|chore|revert)(\\([a-zA-Z0-9_\\-\\.]+\\))?:\\s*//\")");
                    sb.AppendLine();
                    sb.AppendLine("# 检查类型");
                    sb.AppendLine("if [ -z \"$type\" ]; then");
                    sb.AppendLine("    echo \"错误: 提交消息格式不正确\"");
                    sb.AppendLine("    echo \"正确格式: <type>(<scope>): <subject>\"");
                    sb.AppendLine("    echo \"例如: feat(ui): 添加登录界面\"");
                    sb.AppendLine("    echo \"可用的类型: feat, fix, docs, style, refactor, perf, test, build, ci, chore, revert\"");
                    sb.AppendLine("    exit 1");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 检查作用域");
                    sb.AppendLine("# 注意：这里我们恢复了作用域检查的强制性");
                    sb.AppendLine("# 如果没有作用域，则提交失败");
                    sb.AppendLine("if [ -z \"$scope\" ]; then");
                    sb.AppendLine("    echo \"错误: 提交消息缺少作用域\"");
                    sb.AppendLine("    echo \"正确格式: <type>(<scope>): <subject>\"");
                    sb.AppendLine("    echo \"例如: feat(ui): 添加登录界面\"");
                    sb.AppendLine("    echo \"常用作用域: core, ui, config, git, editor, docs\"");
                    sb.AppendLine("    exit 1");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 检查主题");
                    sb.AppendLine("if [ -z \"$subject\" ]; then");
                    sb.AppendLine("    echo \"错误: 提交消息缺少主题描述\"");
                    sb.AppendLine("    echo \"正确格式: <type>(<scope>): <subject>\"");
                    sb.AppendLine("    exit 1");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 检查提交消息长度");
                    sb.AppendLine("subject_length=$(echo \"$type_scope_subject\" | wc -c)");
                    sb.AppendLine("if [ $subject_length -gt 100 ]; then");
                    sb.AppendLine("    echo \"错误: 提交消息第一行不能超过100个字符\"");
                    sb.AppendLine("    echo \"当前长度: $subject_length\"");
                    sb.AppendLine("    exit 1");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("# 检查主题是否以句号结尾");
                    sb.AppendLine("if echo \"$subject\" | grep -q \"\\.$\"; then");
                    sb.AppendLine("    echo \"警告: 主题描述不应以句号结尾\"");
                    sb.AppendLine("fi");
                    sb.AppendLine();
                    sb.AppendLine("echo \"提交消息格式检查通过\"");
                    sb.AppendLine("exit 0");
                    break;
                    
                default:
                    sb.AppendLine("echo \"TByd.CodeStyle: 钩子已触发\"");
                    sb.AppendLine("# 这里可以添加钩子的具体逻辑");
                    sb.AppendLine("exit 0");
                    break;
            }
            
            return sb.ToString();
        }
        
        /// <summary>
        /// 设置文件的执行权限
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        private static void SetExecutablePermission(string _filePath)
        {
            // 在Windows上，不需要设置执行权限
            if (Application.platform == RuntimePlatform.WindowsEditor)
                return;
                
            try
            {
                // 在macOS和Linux上，使用chmod命令设置执行权限
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "chmod";
                process.StartInfo.Arguments = $"+x \"{_filePath}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                
                process.Start();
                process.WaitForExit();
                
                if (process.ExitCode != 0)
                {
                    Debug.LogWarning($"[TByd.CodeStyle] 设置执行权限失败: {_filePath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 设置执行权限异常: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取所有钩子的状态
        /// </summary>
        /// <returns>钩子状态字典</returns>
        public static Dictionary<GitHookType, bool> GetAllHookStatus()
        {
            // 如果未初始化或仓库无效，则返回空字典
            if (!s_Initialized || s_Repository == null || !s_Repository.IsValid)
            {
                return new Dictionary<GitHookType, bool>();
            }
                
            // 刷新缓存
            RefreshHookStatusCache();
            
            return new Dictionary<GitHookType, bool>(s_InstalledHooksCache);
        }
        
        /// <summary>
        /// 安装所有钩子
        /// </summary>
        /// <returns>是否全部安装成功</returns>
        public static bool InstallAllHooks()
        {
            bool allSuccess = true;
            
            // 安装所有钩子
            foreach (GitHookType hookType in Enum.GetValues(typeof(GitHookType)))
            {
                // 目前只关注提交相关的钩子
                if (hookType == GitHookType.PreCommit || 
                    hookType == GitHookType.CommitMsg || 
                    hookType == GitHookType.PrepareCommitMsg)
                {
                    if (!InstallHook(hookType))
                    {
                        allSuccess = false;
                    }
                }
            }
            
            return allSuccess;
        }
        
        /// <summary>
        /// 卸载所有钩子
        /// </summary>
        /// <returns>是否全部卸载成功</returns>
        public static bool UninstallAllHooks()
        {
            // 如果未初始化或仓库无效，则返回失败
            if (!s_Initialized || s_Repository == null || !s_Repository.IsValid)
            {
                Debug.LogError("[TByd.CodeStyle] Git仓库无效，无法卸载钩子");
                return false;
            }
            
            bool allSuccess = true;
            
            try
            {
                // 获取所有已安装的钩子
                Dictionary<GitHookType, bool> hookStatus = GetAllHookStatus();
                
                // 卸载所有已安装的钩子
                foreach (var pair in hookStatus)
                {
                    if (pair.Value)
                    {
                        if (!UninstallHook(pair.Key))
                        {
                            allSuccess = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 卸载所有钩子失败: {e.Message}");
                allSuccess = false;
            }
            
            return allSuccess;
        }
    }
} 