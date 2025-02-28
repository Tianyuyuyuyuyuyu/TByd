using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;

namespace TByd.CodeStyle.Editor.Git
{
    /// <summary>
    /// Git钩子监控器，用于监控Git钩子的状态
    /// </summary>
    [InitializeOnLoad]
    public static class GitHookMonitor
    {
        // 上次检查时间
        private static double s_LastCheckTime;
        
        // 检查间隔（秒）
        private const double c_CheckInterval = 60;
        
        // 是否已初始化
        private static bool s_Initialized;
        
        // 钩子状态缓存
        private static Dictionary<GitHookType, bool> s_HookStatusCache = new Dictionary<GitHookType, bool>();
        
        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static GitHookMonitor()
        {
            // 延迟初始化，确保配置已加载
            EditorApplication.delayCall += Initialize;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize()
        {
            if (s_Initialized)
                return;
                
            // 检查项目是否是Git仓库
            if (!GitRepository.IsProjectGitRepository())
            {
                Debug.LogWarning("[TByd.CodeStyle] 项目不是Git仓库，Git钩子监控器初始化失败");
                return;
            }
            
            // 初始化Git钩子管理器
            GitHookManager.Initialize();
            
            // 订阅钩子状态变更事件
            GitHookManager.HookStatusChanged += OnHookStatusChanged;
            
            // 订阅编辑器更新事件
            EditorApplication.update += OnEditorUpdate;
            
            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += OnConfigChanged;
            
            // 刷新钩子状态缓存
            RefreshHookStatusCache();
            
            // 检查是否需要自动安装钩子
            CheckAutoInstallHooks();
            
            s_Initialized = true;
            
            Debug.Log("[TByd.CodeStyle] Git钩子监控器初始化成功");
        }
        
        /// <summary>
        /// 编辑器更新事件处理
        /// </summary>
        private static void OnEditorUpdate()
        {
            // 定期检查钩子状态
            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - s_LastCheckTime > c_CheckInterval)
            {
                CheckHookStatus();
                s_LastCheckTime = currentTime;
            }
        }
        
        /// <summary>
        /// 配置变更事件处理
        /// </summary>
        private static void OnConfigChanged()
        {
            // 重新初始化Git钩子管理器，以应用可能的Git仓库路径变更
            GitHookManager.Reinitialize();
            
            // 检查是否需要自动安装钩子
            CheckAutoInstallHooks();
        }
        
        /// <summary>
        /// 钩子状态变更事件处理
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <param name="_installed">是否已安装</param>
        private static void OnHookStatusChanged(GitHookType _hookType, bool _installed)
        {
            // 更新缓存
            s_HookStatusCache[_hookType] = _installed;
            
            // 显示通知
            string hookName = _hookType.GetFileName();
            string message = _installed ? 
                $"Git钩子 {hookName} 已安装" : 
                $"Git钩子 {hookName} 已卸载";
                
            NotificationSystem.ShowNotification(message, _installed ? NotificationType.Success : NotificationType.Info);
        }
        
        /// <summary>
        /// 刷新钩子状态缓存
        /// </summary>
        private static void RefreshHookStatusCache()
        {
            s_HookStatusCache = GitHookManager.GetAllHookStatus();
        }
        
        /// <summary>
        /// 检查钩子状态
        /// </summary>
        private static void CheckHookStatus()
        {
            // 获取当前配置
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            // 如果未启用Git提交规范检查，则跳过
            if (!config.EnableGitCommitCheck)
                return;
                
            // 如果未启用钩子状态检查，则跳过
            if (!config.GitHookConfig.CheckHooksOnStartup)
                return;
                
            // 刷新钩子状态缓存
            GitHookManager.RefreshHookStatusCache();
            
            // 获取所有钩子状态
            Dictionary<GitHookType, bool> hookStatus = GitHookManager.GetAllHookStatus();
            
            // 检查是否有钩子状态变化
            bool hasChanges = false;
            foreach (var pair in hookStatus)
            {
                if (s_HookStatusCache.TryGetValue(pair.Key, out bool cachedStatus) && cachedStatus != pair.Value)
                {
                    hasChanges = true;
                    break;
                }
            }
            
            // 如果有变化，则更新缓存并显示通知
            if (hasChanges)
            {
                s_HookStatusCache = hookStatus;
                
                // 检查是否有钩子被卸载
                bool hasUninstalledHooks = false;
                foreach (var pair in hookStatus)
                {
                    if (config.GitHookConfig.IsHookEnabled(pair.Key) && !pair.Value)
                    {
                        hasUninstalledHooks = true;
                        break;
                    }
                }
                
                // 如果有钩子被卸载，则显示通知
                if (hasUninstalledHooks)
                {
                    NotificationSystem.ShowNotification("检测到Git钩子状态变化，部分钩子可能已被卸载", NotificationType.Warning);
                }
            }
        }
        
        /// <summary>
        /// 检查是否需要自动安装钩子
        /// </summary>
        private static void CheckAutoInstallHooks()
        {
            // 获取当前配置
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            // 如果未启用Git提交规范检查，则跳过
            if (!config.EnableGitCommitCheck)
                return;
                
            // 如果未启用自动安装钩子，则跳过
            if (!config.GitHookConfig.AutoInstallHooks)
                return;
                
            // 获取所有钩子状态
            Dictionary<GitHookType, bool> hookStatus = GitHookManager.GetAllHookStatus();
            
            // 检查是否有需要安装的钩子
            bool needInstall = false;
            foreach (var hookConfig in config.GitHookConfig.HookConfigs)
            {
                if (hookConfig.Enabled && 
                    hookStatus.TryGetValue(hookConfig.HookType, out bool installed) && 
                    !installed)
                {
                    needInstall = true;
                    break;
                }
            }
            
            // 如果需要安装钩子，则显示通知并安装
            if (needInstall)
            {
                // 显示确认对话框
                bool install = EditorUtility.DisplayDialog(
                    "安装Git钩子",
                    "检测到部分Git钩子未安装，是否立即安装？\n\n" +
                    "这些钩子用于在Git提交时检查代码风格和提交消息格式。",
                    "安装",
                    "稍后再说");
                    
                if (install)
                {
                    InstallEnabledHooks();
                }
            }
        }
        
        /// <summary>
        /// 安装已启用的钩子
        /// </summary>
        public static void InstallEnabledHooks()
        {
            // 获取当前配置
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            // 安装已启用的钩子
            foreach (var hookConfig in config.GitHookConfig.HookConfigs)
            {
                if (hookConfig.Enabled)
                {
                    GitHookManager.InstallHook(hookConfig.HookType);
                }
            }
        }
        
        /// <summary>
        /// 卸载所有钩子
        /// </summary>
        public static void UninstallAllHooks()
        {
            GitHookManager.UninstallAllHooks();
        }
        
        /// <summary>
        /// 获取钩子状态
        /// </summary>
        /// <returns>钩子状态字典</returns>
        public static Dictionary<GitHookType, bool> GetHookStatus()
        {
            return GitHookManager.GetAllHookStatus();
        }
        
        /// <summary>
        /// 获取钩子文件路径
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <param name="_hooksDir">钩子目录</param>
        /// <returns>钩子文件路径</returns>
        public static string GetHookPath(GitHookType _hookType, string _hooksDir)
        {
            string hookFileName = _hookType.GetFileName();
            return Path.Combine(_hooksDir, hookFileName);
        }
        
        /// <summary>
        /// 检查钩子是否已安装
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <param name="_hooksDir">钩子目录</param>
        /// <returns>是否已安装</returns>
        public static bool IsHookInstalled(GitHookType _hookType, string _hooksDir)
        {
            string hookPath = GetHookPath(_hookType, _hooksDir);
            
            if (!File.Exists(hookPath))
                return false;
                
            try
            {
                string content = File.ReadAllText(hookPath);
                return content.Contains("TByd.CodeStyle");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 读取钩子文件失败: {e.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <param name="_hooksDir">钩子目录</param>
        /// <returns>是否安装成功</returns>
        public static bool InstallHook(GitHookType _hookType, string _hooksDir)
        {
            string hookPath = GetHookPath(_hookType, _hooksDir);
            
            try
            {
                // 检查是否已存在非托管钩子
                if (File.Exists(hookPath) && !IsHookInstalled(_hookType, _hooksDir))
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
                
                // 创建钩子内容
                string hookContent = $"#!/bin/sh\n# TByd.CodeStyle {_hookType.GetFileName()} hook\n\n" +
                                    "# 这是一个测试钩子，用于单元测试\n" +
                                    "echo \"TByd.CodeStyle hook test\"\n" +
                                    "exit 0\n";
                
                // 写入钩子文件
                File.WriteAllText(hookPath, hookContent);
                
                Debug.Log($"[TByd.CodeStyle] 已安装钩子: {_hookType.GetFileName()}");
                
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
        /// <param name="_hooksDir">钩子目录</param>
        /// <returns>是否卸载成功</returns>
        public static bool UninstallHook(GitHookType _hookType, string _hooksDir)
        {
            string hookPath = GetHookPath(_hookType, _hooksDir);
            
            try
            {
                // 检查钩子是否存在且由TByd.CodeStyle管理
                if (!File.Exists(hookPath) || !IsHookInstalled(_hookType, _hooksDir))
                {
                    Debug.LogWarning($"[TByd.CodeStyle] 钩子不存在或不是由TByd.CodeStyle管理: {_hookType.GetFileName()}");
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
                
                Debug.Log($"[TByd.CodeStyle] 已卸载钩子: {_hookType.GetFileName()}");
                
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 卸载钩子失败: {e.Message}");
                return false;
            }
        }
    }
} 