using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.CodeCheck;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Editor.CodeCheck
{
    /// <summary>
    /// 代码检查运行器，用于在编辑器中执行代码检查
    /// </summary>
    [InitializeOnLoad]
    public static class CodeCheckRunner
    {
        // 是否已初始化
        private static bool s_Initialized;
        
        // 代码检查器
        private static CodeChecker s_Checker;
        
        // 代码修复器
        private static CodeFixer s_Fixer;
        
        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static CodeCheckRunner()
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
                
            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += OnConfigChanged;
            
            // 初始化检查器和修复器
            InitializeCheckerAndFixer();
            
            s_Initialized = true;
            
            Debug.Log("[TByd.CodeStyle] 代码检查运行器初始化成功");
        }
        
        /// <summary>
        /// 配置变更事件处理
        /// </summary>
        private static void OnConfigChanged()
        {
            // 重新初始化检查器和修复器
            InitializeCheckerAndFixer();
        }
        
        /// <summary>
        /// 初始化检查器和修复器
        /// </summary>
        private static void InitializeCheckerAndFixer()
        {
            // 获取当前配置
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            // 初始化检查器
            s_Checker = new CodeChecker(config.CodeCheckConfig);
            
            // 初始化修复器
            s_Fixer = new CodeFixer(config.CodeCheckConfig, s_Checker);
        }
        
        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult CheckCode(string _code, string _filePath)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Checker.CheckCode(_code, _filePath);
        }
        
        /// <summary>
        /// 检查文件
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult CheckFile(string _filePath)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Checker.CheckFile(_filePath);
        }
        
        /// <summary>
        /// 检查目录
        /// </summary>
        /// <param name="_directoryPath">目录路径</param>
        /// <param name="_recursive">是否递归检查子目录</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult CheckDirectory(string _directoryPath, bool _recursive = true)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Checker.CheckDirectory(_directoryPath, _recursive);
        }
        
        /// <summary>
        /// 修复代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <returns>修复后的代码</returns>
        public static string FixCode(string _code, string _filePath)
        {
            if (s_Fixer == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Fixer.FixCode(_code, _filePath);
        }
        
        /// <summary>
        /// 修复文件
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <returns>是否修复成功</returns>
        public static bool FixFile(string _filePath)
        {
            if (s_Fixer == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Fixer.FixFile(_filePath);
        }
        
        /// <summary>
        /// 修复目录
        /// </summary>
        /// <param name="_directoryPath">目录路径</param>
        /// <param name="_recursive">是否递归修复子目录</param>
        /// <returns>修复的文件数量</returns>
        public static int FixDirectory(string _directoryPath, bool _recursive = true)
        {
            if (s_Fixer == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Fixer.FixDirectory(_directoryPath, _recursive);
        }
        
        /// <summary>
        /// 生成检查报告
        /// </summary>
        /// <param name="_result">检查结果</param>
        /// <returns>报告文本</returns>
        public static string GenerateReport(CodeCheckResult _result)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Checker.GenerateReport(_result);
        }
        
        /// <summary>
        /// 获取所有代码检查规则
        /// </summary>
        /// <returns>规则列表</returns>
        public static List<ICodeCheckRule> GetRules()
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }
            
            return s_Checker.GetRules();
        }
        
        /// <summary>
        /// 从命令行参数检查文件
        /// </summary>
        /// <param name="_args">命令行参数</param>
        /// <returns>退出代码</returns>
        public static int CheckFromCommandLine(string[] _args)
        {
            try
            {
                if (_args.Length < 1)
                {
                    Console.Error.WriteLine("错误: 缺少文件路径参数");
                    return 1;
                }
                
                string filePath = _args[0];
                
                if (filePath.EndsWith(".txt") && File.Exists(filePath))
                {
                    // 如果是文本文件，读取文件列表
                    string[] files = File.ReadAllLines(filePath);
                    
                    List<CodeCheckIssue> allIssues = new List<CodeCheckIssue>();
                    
                    foreach (string file in files)
                    {
                        if (string.IsNullOrEmpty(file.Trim()))
                        {
                            continue;
                        }
                        
                        if (!File.Exists(file))
                        {
                            Console.Error.WriteLine($"警告: 文件不存在: {file}");
                            continue;
                        }
                        
                        CodeCheckResult result = CheckFile(file);
                        
                        if (!result.IsValid)
                        {
                            allIssues.AddRange(result.Issues);
                        }
                    }
                    
                    if (allIssues.Count > 0)
                    {
                        CodeCheckResult combinedResult = CodeCheckResult.Failure(allIssues);
                        Console.Error.WriteLine(GenerateReport(combinedResult));
                        return 1;
                    }
                    
                    Console.WriteLine("代码检查通过");
                    return 0;
                }
                else if (Directory.Exists(filePath))
                {
                    // 如果是目录，检查目录
                    CodeCheckResult result = CheckDirectory(filePath);
                    
                    if (!result.IsValid)
                    {
                        Console.Error.WriteLine(GenerateReport(result));
                        return 1;
                    }
                    
                    Console.WriteLine("代码检查通过");
                    return 0;
                }
                else if (File.Exists(filePath))
                {
                    // 如果是文件，检查文件
                    CodeCheckResult result = CheckFile(filePath);
                    
                    if (!result.IsValid)
                    {
                        Console.Error.WriteLine(GenerateReport(result));
                        return 1;
                    }
                    
                    Console.WriteLine("代码检查通过");
                    return 0;
                }
                else
                {
                    Console.Error.WriteLine($"错误: 文件或目录不存在: {filePath}");
                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"错误: {e.Message}");
                return 1;
            }
        }
        
        /// <summary>
        /// 检查暂存区中的文件
        /// </summary>
        /// <returns>是否检查通过</returns>
        public static bool CheckStagedFiles()
        {
            try
            {
                // 获取Git仓库路径
                string repoPath = GetGitRepositoryPath();
                if (string.IsNullOrEmpty(repoPath))
                {
                    Debug.LogError("[TByd.CodeStyle] 无法获取Git仓库路径");
                    return false;
                }
                
                // 获取暂存区中的C#文件
                List<string> stagedFiles = GetStagedCSharpFiles(repoPath);
                if (stagedFiles.Count == 0)
                {
                    Debug.Log("[TByd.CodeStyle] 没有暂存的C#文件需要检查");
                    return true;
                }
                
                List<CodeCheckIssue> allIssues = new List<CodeCheckIssue>();
                
                // 检查每个文件
                foreach (string file in stagedFiles)
                {
                    string fullPath = Path.Combine(repoPath, file);
                    
                    if (!File.Exists(fullPath))
                    {
                        Debug.LogWarning($"[TByd.CodeStyle] 文件不存在: {fullPath}");
                        continue;
                    }
                    
                    CodeCheckResult result = CheckFile(fullPath);
                    
                    if (!result.IsValid)
                    {
                        allIssues.AddRange(result.Issues);
                    }
                }
                
                if (allIssues.Count > 0)
                {
                    // 创建组合结果
                    CodeCheckResult combinedResult = CodeCheckResult.Failure(allIssues);
                    
                    // 生成报告
                    string report = GenerateReport(combinedResult);
                    
                    // 显示通知
                    NotificationSystem.ShowNotification(
                        "代码检查失败，请修复问题后再提交", 
                        NotificationType.Error);
                    
                    // 显示报告窗口
                    CodeCheckReportWindow.ShowWindow(report);
                    
                    return false;
                }
                
                // 显示通知
                NotificationSystem.ShowNotification(
                    "代码检查通过", 
                    NotificationType.Success);
                
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 检查暂存区文件失败: {e.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 获取Git仓库路径
        /// </summary>
        /// <returns>仓库路径</returns>
        private static string GetGitRepositoryPath()
        {
            try
            {
                // 获取项目路径
                string projectPath = Path.GetDirectoryName(Application.dataPath);
                
                // 检查.git目录是否存在
                if (Directory.Exists(Path.Combine(projectPath, ".git")))
                {
                    return projectPath;
                }
                
                return string.Empty;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 获取Git仓库路径失败: {e.Message}");
                return string.Empty;
            }
        }
        
        /// <summary>
        /// 获取暂存区中的C#文件
        /// </summary>
        /// <param name="_repoPath">仓库路径</param>
        /// <returns>文件列表</returns>
        private static List<string> GetStagedCSharpFiles(string _repoPath)
        {
            List<string> stagedFiles = new List<string>();
            
            try
            {
                // 创建Git命令
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "git";
                process.StartInfo.Arguments = "diff --cached --name-only --diff-filter=ACM";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = _repoPath;
                process.StartInfo.CreateNoWindow = true;
                
                // 执行命令
                process.Start();
                
                // 读取输出
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                
                // 分割输出为行
                string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                
                // 过滤C#文件
                foreach (string line in lines)
                {
                    if (line.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                    {
                        stagedFiles.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 获取暂存区文件失败: {e.Message}");
            }
            
            return stagedFiles;
        }
    }
    
    /// <summary>
    /// 代码检查报告窗口
    /// </summary>
    public class CodeCheckReportWindow : EditorWindow
    {
        // 报告文本
        private string m_Report;
        
        // 滚动位置
        private Vector2 m_ScrollPosition;
        
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="_report">报告文本</param>
        public static void ShowWindow(string _report)
        {
            CodeCheckReportWindow window = GetWindow<CodeCheckReportWindow>("代码检查报告");
            window.m_Report = _report;
            window.Show();
        }
        
        /// <summary>
        /// 绘制GUI
        /// </summary>
        private void OnGUI()
        {
            EditorGUILayout.Space();
            
            // 显示报告
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            EditorGUILayout.TextArea(m_Report, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
            
            EditorGUILayout.Space();
            
            // 显示按钮
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("关闭", GUILayout.Width(100)))
            {
                Close();
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
} 