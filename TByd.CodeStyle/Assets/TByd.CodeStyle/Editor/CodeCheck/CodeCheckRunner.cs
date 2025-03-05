using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.CodeCheck;

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
            {
                return;
            }

            // 订阅配置变更事件
            ConfigProvider.OnConfigChanged += OnConfigChanged;

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
            var config = ConfigProvider.GetConfig();

            // 初始化检查器
            s_Checker = new CodeChecker(config.CodeCheckConfig);

            // 初始化修复器
            s_Fixer = new CodeFixer(config.CodeCheckConfig, s_Checker);
        }

        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult CheckCode(string code, string filePath)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }

            return s_Checker?.CheckCode(code, filePath);
        }

        /// <summary>
        /// 检查文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult CheckFile(string filePath)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }

            return s_Checker?.CheckFile(filePath);
        }

        /// <summary>
        /// 检查目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归检查子目录</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult CheckDirectory(string directoryPath, bool recursive = true)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }

            return s_Checker?.CheckDirectory(directoryPath, recursive);
        }

        /// <summary>
        /// 修复代码
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>修复后的代码</returns>
        public static string FixCode(string code, string filePath)
        {
            if (s_Fixer == null)
            {
                InitializeCheckerAndFixer();
            }

            return s_Fixer?.FixCode(code, filePath);
        }

        /// <summary>
        /// 修复文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否修复成功</returns>
        public static bool FixFile(string filePath)
        {
            if (s_Fixer == null)
            {
                InitializeCheckerAndFixer();
            }

            return s_Fixer != null && s_Fixer.FixFile(filePath);
        }

        /// <summary>
        /// 修复目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归修复子目录</param>
        /// <returns>修复的文件数量</returns>
        public static int FixDirectory(string directoryPath, bool recursive = true)
        {
            if (s_Fixer == null)
            {
                InitializeCheckerAndFixer();
            }

            if (s_Fixer != null)
            {
                return s_Fixer.FixDirectory(directoryPath, recursive);
            }

            return 0;
        }

        /// <summary>
        /// 生成检查报告
        /// </summary>
        /// <param name="result">检查结果</param>
        /// <returns>报告文本</returns>
        public static string GenerateReport(CodeCheckResult result)
        {
            if (s_Checker == null)
            {
                InitializeCheckerAndFixer();
            }

            return s_Checker?.GenerateReport(result);
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

            return s_Checker?.GetRules();
        }

        /// <summary>
        /// 从命令行参数检查文件
        /// </summary>
        /// <param name="args">命令行参数</param>
        /// <returns>退出代码</returns>
        public static int CheckFromCommandLine(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    Console.Error.WriteLine("错误: 缺少文件路径参数");
                    return 1;
                }

                var filePath = args[0];

                if (filePath.EndsWith(".txt") && File.Exists(filePath))
                {
                    // 如果是文本文件，读取文件列表
                    var files = File.ReadAllLines(filePath);

                    var allIssues = new List<CodeCheckIssue>();

                    foreach (var file in files)
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

                        var result = CheckFile(file);

                        if (!result.IsValid)
                        {
                            allIssues.AddRange(result.Issues);
                        }
                    }

                    if (allIssues.Count > 0)
                    {
                        var combinedResult = CodeCheckResult.Failure(allIssues);
                        Console.Error.WriteLine(GenerateReport(combinedResult));
                        return 1;
                    }

                    Console.WriteLine("代码检查通过");
                    return 0;
                }

                if (Directory.Exists(filePath))
                {
                    // 如果是目录，检查目录
                    var result = CheckDirectory(filePath);

                    if (!result.IsValid)
                    {
                        Console.Error.WriteLine(GenerateReport(result));
                        return 1;
                    }

                    Console.WriteLine("代码检查通过");
                    return 0;
                }

                if (File.Exists(filePath))
                {
                    // 如果是文件，检查文件
                    var result = CheckFile(filePath);

                    if (!result.IsValid)
                    {
                        Console.Error.WriteLine(GenerateReport(result));
                        return 1;
                    }

                    Console.WriteLine("代码检查通过");
                    return 0;
                }

                Console.Error.WriteLine($"错误: 文件或目录不存在: {filePath}");
                return 1;
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
                var repoPath = GetGitRepositoryPath();
                if (string.IsNullOrEmpty(repoPath))
                {
                    Debug.LogError("[TByd.CodeStyle] 无法获取Git仓库路径");
                    return false;
                }

                // 获取暂存区中的C#文件
                var stagedFiles = GetStagedCSharpFiles(repoPath);
                if (stagedFiles.Count == 0)
                {
                    Debug.Log("[TByd.CodeStyle] 没有暂存的C#文件需要检查");
                    return true;
                }

                var allIssues = new List<CodeCheckIssue>();

                // 检查每个文件
                foreach (var file in stagedFiles)
                {
                    var fullPath = Path.Combine(repoPath, file);

                    if (!File.Exists(fullPath))
                    {
                        Debug.LogWarning($"[TByd.CodeStyle] 文件不存在: {fullPath}");
                        continue;
                    }

                    var result = CheckFile(fullPath);

                    if (!result.IsValid)
                    {
                        allIssues.AddRange(result.Issues);
                    }
                }

                if (allIssues.Count > 0)
                {
                    // 创建组合结果
                    var combinedResult = CodeCheckResult.Failure(allIssues);

                    // 生成报告
                    var report = GenerateReport(combinedResult);

                    // 显示通知
                    NotificationSystem.ShowNotification(
                        "代码检查失败，请修复问题后再提交",
                        NotificationType.k_Error);

                    // 显示报告窗口
                    CodeCheckReportWindow.ShowWindow(report);

                    return false;
                }

                // 显示通知
                NotificationSystem.ShowNotification(
                    "代码检查通过",
                    NotificationType.k_Success);

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
                var projectPath = Path.GetDirectoryName(Application.dataPath);

                // 检查.git目录是否存在
                if (projectPath != null && Directory.Exists(Path.Combine(projectPath, ".git")))
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
        /// <param name="repoPath">仓库路径</param>
        /// <returns>文件列表</returns>
        private static List<string> GetStagedCSharpFiles(string repoPath)
        {
            var stagedFiles = new List<string>();

            try
            {
                // 创建Git命令
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "git";
                process.StartInfo.Arguments = "diff --cached --name-only --diff-filter=ACM";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = repoPath;
                process.StartInfo.CreateNoWindow = true;

                // 执行命令
                process.Start();

                // 读取输出
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // 分割输出为行
                var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // 过滤C#文件
                stagedFiles.AddRange(lines.Where(line => line.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)));
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
        /// <param name="report">报告文本</param>
        public static void ShowWindow(string report)
        {
            var window = GetWindow<CodeCheckReportWindow>("代码检查报告");
            window.m_Report = report;
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
