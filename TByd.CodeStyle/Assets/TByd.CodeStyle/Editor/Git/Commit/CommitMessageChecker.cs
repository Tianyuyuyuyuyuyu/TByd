using System;
using System.IO;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Runtime.Git.Commit;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.Git.Commit
{
    /// <summary>
    /// 提交消息检查器，用于在编辑器中管理提交消息检查
    /// </summary>
    [InitializeOnLoad]
    public static class CommitMessageChecker
    {
        // 环境变量名，用于标识在Unity编辑器中运行
        private const string k_CUnityEditorEnvVar = "UNITY_EDITOR";

        // 是否已初始化
        private static bool s_Initialized;

        // 提交消息验证器
        private static CommitMessageValidator s_Validator;

        // 提交消息修复器
        private static CommitMessageFixer s_Fixer;

        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static CommitMessageChecker()
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

            // 初始化验证器和修复器
            InitializeValidatorAndFixer();

            // 设置环境变量，标识在Unity编辑器中运行
            SetUnityEditorEnvironmentVariable();

            s_Initialized = true;

            Debug.Log("[TByd.CodeStyle] 提交消息检查器初始化成功");
        }

        /// <summary>
        /// 配置变更事件处理
        /// </summary>
        private static void OnConfigChanged()
        {
            // 重新初始化验证器和修复器
            InitializeValidatorAndFixer();
        }

        /// <summary>
        /// 初始化验证器和修复器
        /// </summary>
        private static void InitializeValidatorAndFixer()
        {
            // 获取当前配置
            var config = ConfigProvider.GetConfig();

            // 初始化验证器
            s_Validator = new CommitMessageValidator(config.GitCommitConfig);

            // 初始化修复器
            s_Fixer = new CommitMessageFixer(config.GitCommitConfig);
        }

        /// <summary>
        /// 设置环境变量，标识在Unity编辑器中运行
        /// </summary>
        private static void SetUnityEditorEnvironmentVariable()
        {
            try
            {
                // 设置环境变量
                Environment.SetEnvironmentVariable(k_CUnityEditorEnvVar, "true");

                Debug.Log("[TByd.CodeStyle] 已设置环境变量: UNITY_EDITOR=true");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 设置环境变量失败: {e.Message}");
            }
        }

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="messageText">提交消息文本</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateMessage(string messageText)
        {
            if (s_Validator == null)
            {
                InitializeValidatorAndFixer();
            }

            // 添加调试日志
            Debug.Log($"[TByd.CodeStyle] 验证提交消息: '{messageText}'");

            // 解析提交消息
            var message = CommitMessageParser.Parse(messageText);
            Debug.Log(
                $"[TByd.CodeStyle] 解析结果: Type={message.Type}, Scope={message.Scope}, Subject='{message.Subject}'");

            // 验证提交消息
            if (s_Validator != null)
            {
                var result = s_Validator.ValidateText(messageText);

                // 输出验证结果
                Debug.Log($"[TByd.CodeStyle] 验证结果: IsValid={result.IsValid}, Errors.Count={result.Errors.Count}");
                foreach (var error in result.Errors)
                {
                    Debug.Log($"[TByd.CodeStyle] 错误: {error}");
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="messageText">提交消息文本</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateCommitMessage(string messageText)
        {
            return ValidateMessage(messageText);
        }

        /// <summary>
        /// 验证提交消息文件
        /// </summary>
        /// <param name="filePath">提交消息文件路径</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateMessageFile(string filePath)
        {
            if (s_Validator == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Validator?.ValidateFile(filePath);
        }

        /// <summary>
        /// 修复提交消息
        /// </summary>
        /// <param name="messageText">提交消息文本</param>
        /// <returns>修复后的提交消息文本</returns>
        public static string FixMessage(string messageText)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer?.FixText(messageText);
        }

        /// <summary>
        /// 修复提交消息文件
        /// </summary>
        /// <param name="filePath">提交消息文件路径</param>
        /// <returns>是否修复成功</returns>
        public static bool FixMessageFile(string filePath)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer != null && s_Fixer.FixFile(filePath);
        }

        /// <summary>
        /// 生成提交消息模板
        /// </summary>
        /// <returns>提交消息模板</returns>
        public static string GenerateTemplate()
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer?.GenerateTemplate();
        }

        /// <summary>
        /// 将模板写入提交消息文件
        /// </summary>
        /// <param name="filePath">提交消息文件路径</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteTemplateToFile(string filePath)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer != null && s_Fixer.WriteTemplateToFile(filePath);
        }

        /// <summary>
        /// 格式化提交消息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="scope">范围</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        /// <param name="footer">页脚</param>
        /// <param name="isBreakingChange">是否是破坏性变更</param>
        /// <returns>格式化后的提交消息</returns>
        public static string FormatCommitMessage(
            string type,
            string scope,
            string subject,
            string body = "",
            string footer = "",
            bool isBreakingChange = false)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            // 构建头部
            var header = type;

            // 添加范围
            if (!string.IsNullOrEmpty(scope))
            {
                header += $"({scope})";
            }

            // 添加破坏性变更标记
            if (isBreakingChange)
            {
                header += "!";
            }

            // 添加冒号和主题
            header += $": {subject}";

            // 构建完整消息
            var message = header;

            // 添加正文
            if (!string.IsNullOrEmpty(body))
            {
                message += $"\n\n{body}";
            }

            // 添加页脚
            if (!string.IsNullOrEmpty(footer))
            {
                message += $"\n\n{footer}";
            }

            return message;
        }

        /// <summary>
        /// 解析提交消息
        /// </summary>
        /// <param name="message">提交消息</param>
        /// <param name="type">类型</param>
        /// <param name="scope">范围</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        /// <param name="footer">页脚</param>
        /// <param name="isBreakingChange">是否是破坏性变更</param>
        /// <returns>是否解析成功</returns>
        public static bool ParseCommitMessage(
            string message,
            out string type,
            out string scope,
            out string subject,
            out string body,
            out string footer,
            out bool isBreakingChange)
        {
            // 初始化输出参数
            type = string.Empty;
            scope = string.Empty;
            subject = string.Empty;
            body = string.Empty;
            footer = string.Empty;
            isBreakingChange = false;

            if (string.IsNullOrEmpty(message))
            {
                return false;
            }

            try
            {
                // 分割消息为头部、正文和页脚
                var parts = message.Split(new[] { "\n\n" }, StringSplitOptions.None);
                var header = parts[0].Trim();

                // 解析正文和页脚
                if (parts.Length > 1)
                {
                    body = parts[1].Trim();
                }

                if (parts.Length > 2)
                {
                    footer = parts[2].Trim();
                }

                // 检查是否包含破坏性变更标记
                isBreakingChange = header.Contains("!") || footer.Contains("BREAKING CHANGE");

                // 解析头部
                var colonIndex = header.IndexOf(": ", StringComparison.Ordinal);
                if (colonIndex < 0)
                {
                    return false;
                }

                // 提取主题
                subject = header.Substring(colonIndex + 2).Trim();

                // 提取类型和范围
                var typeScope = header.Substring(0, colonIndex).Trim();

                // 移除破坏性变更标记
                typeScope = typeScope.Replace("!", "");

                // 解析类型和范围
                var scopeStartIndex = typeScope.IndexOf("(", StringComparison.Ordinal);
                if (scopeStartIndex < 0)
                {
                    // 没有范围
                    type = typeScope.Trim();
                }
                else
                {
                    // 有范围
                    type = typeScope.Substring(0, scopeStartIndex).Trim();

                    var scopeEndIndex = typeScope.IndexOf(")", scopeStartIndex, StringComparison.Ordinal);
                    if (scopeEndIndex > scopeStartIndex)
                    {
                        scope = typeScope.Substring(scopeStartIndex + 1, scopeEndIndex - scopeStartIndex - 1).Trim();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从命令行参数验证提交消息文件
        /// </summary>
        /// <param name="args">命令行参数</param>
        /// <returns>验证结果</returns>
        public static int ValidateFromCommandLine(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    Console.Error.WriteLine("错误: 缺少提交消息文件路径参数");
                    return 1;
                }

                var filePath = args[0];

                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"错误: 提交消息文件不存在: {filePath}");
                    return 1;
                }

                // 初始化验证器
                if (s_Validator == null)
                {
                    InitializeValidatorAndFixer();
                }

                // 验证提交消息
                if (s_Validator != null)
                {
                    var result = s_Validator.ValidateFile(filePath);

                    if (!result.IsValid)
                    {
                        Console.Error.WriteLine(result.GetFormattedErrorMessage());
                        return 1;
                    }
                }

                Console.WriteLine("提交消息验证通过");
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"错误: {e.Message}");
                return 1;
            }
        }
    }
}
