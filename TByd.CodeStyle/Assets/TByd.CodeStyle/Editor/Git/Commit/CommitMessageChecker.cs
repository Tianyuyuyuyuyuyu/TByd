using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;
using TByd.CodeStyle.Runtime.Git.Commit;

namespace TByd.CodeStyle.Editor.Git.Commit
{
    /// <summary>
    /// 提交消息检查器，用于在编辑器中管理提交消息检查
    /// </summary>
    [InitializeOnLoad]
    public static class CommitMessageChecker
    {
        // 环境变量名，用于标识在Unity编辑器中运行
        private const string c_UnityEditorEnvVar = "UNITY_EDITOR";

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
                return;

            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += OnConfigChanged;

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
            CodeStyleConfig config = ConfigProvider.GetConfig();

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
                Environment.SetEnvironmentVariable(c_UnityEditorEnvVar, "true");

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
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateMessage(string _messageText)
        {
            if (s_Validator == null)
            {
                InitializeValidatorAndFixer();
            }

            // 添加调试日志
            Debug.Log($"[TByd.CodeStyle] 验证提交消息: '{_messageText}'");

            // 解析提交消息
            var message = Runtime.Git.Commit.CommitMessageParser.Parse(_messageText);
            Debug.Log($"[TByd.CodeStyle] 解析结果: Type={message.Type}, Scope={message.Scope}, Subject='{message.Subject}'");

            // 验证提交消息
            var result = s_Validator.ValidateText(_messageText);

            // 输出验证结果
            Debug.Log($"[TByd.CodeStyle] 验证结果: IsValid={result.IsValid}, Errors.Count={result.Errors.Count}");
            foreach (var error in result.Errors)
            {
                Debug.Log($"[TByd.CodeStyle] 错误: {error}");
            }

            return result;
        }

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateCommitMessage(string _messageText)
        {
            return ValidateMessage(_messageText);
        }

        /// <summary>
        /// 验证提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateMessageFile(string _filePath)
        {
            if (s_Validator == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Validator.ValidateFile(_filePath);
        }

        /// <summary>
        /// 修复提交消息
        /// </summary>
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>修复后的提交消息文本</returns>
        public static string FixMessage(string _messageText)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer.FixText(_messageText);
        }

        /// <summary>
        /// 修复提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>是否修复成功</returns>
        public static bool FixMessageFile(string _filePath)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer.FixFile(_filePath);
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

            return s_Fixer.GenerateTemplate();
        }

        /// <summary>
        /// 将模板写入提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteTemplateToFile(string _filePath)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            return s_Fixer.WriteTemplateToFile(_filePath);
        }

        /// <summary>
        /// 格式化提交消息
        /// </summary>
        /// <param name="_type">类型</param>
        /// <param name="_scope">范围</param>
        /// <param name="_subject">主题</param>
        /// <param name="_body">正文</param>
        /// <param name="_footer">页脚</param>
        /// <param name="_isBreakingChange">是否是破坏性变更</param>
        /// <returns>格式化后的提交消息</returns>
        public static string FormatCommitMessage(
            string _type,
            string _scope,
            string _subject,
            string _body = "",
            string _footer = "",
            bool _isBreakingChange = false)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }

            // 构建头部
            string header = _type;

            // 添加范围
            if (!string.IsNullOrEmpty(_scope))
            {
                header += $"({_scope})";
            }

            // 添加破坏性变更标记
            if (_isBreakingChange)
            {
                header += "!";
            }

            // 添加冒号和主题
            header += $": {_subject}";

            // 构建完整消息
            string message = header;

            // 添加正文
            if (!string.IsNullOrEmpty(_body))
            {
                message += $"\n\n{_body}";
            }

            // 添加页脚
            if (!string.IsNullOrEmpty(_footer))
            {
                message += $"\n\n{_footer}";
            }

            return message;
        }

        /// <summary>
        /// 解析提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_type">类型</param>
        /// <param name="_scope">范围</param>
        /// <param name="_subject">主题</param>
        /// <param name="_body">正文</param>
        /// <param name="_footer">页脚</param>
        /// <param name="_isBreakingChange">是否是破坏性变更</param>
        /// <returns>是否解析成功</returns>
        public static bool ParseCommitMessage(
            string _message,
            out string _type,
            out string _scope,
            out string _subject,
            out string _body,
            out string _footer,
            out bool _isBreakingChange)
        {
            // 初始化输出参数
            _type = string.Empty;
            _scope = string.Empty;
            _subject = string.Empty;
            _body = string.Empty;
            _footer = string.Empty;
            _isBreakingChange = false;

            if (string.IsNullOrEmpty(_message))
            {
                return false;
            }

            try
            {
                // 分割消息为头部、正文和页脚
                string[] parts = _message.Split(new[] { "\n\n" }, StringSplitOptions.None);
                string header = parts[0].Trim();

                // 解析正文和页脚
                if (parts.Length > 1)
                {
                    _body = parts[1].Trim();
                }

                if (parts.Length > 2)
                {
                    _footer = parts[2].Trim();
                }

                // 检查是否包含破坏性变更标记
                _isBreakingChange = header.Contains("!") || (_footer.Contains("BREAKING CHANGE"));

                // 解析头部
                int colonIndex = header.IndexOf(": ");
                if (colonIndex < 0)
                {
                    return false;
                }

                // 提取主题
                _subject = header.Substring(colonIndex + 2).Trim();

                // 提取类型和范围
                string typeScope = header.Substring(0, colonIndex).Trim();

                // 移除破坏性变更标记
                typeScope = typeScope.Replace("!", "");

                // 解析类型和范围
                int scopeStartIndex = typeScope.IndexOf("(");
                if (scopeStartIndex < 0)
                {
                    // 没有范围
                    _type = typeScope.Trim();
                }
                else
                {
                    // 有范围
                    _type = typeScope.Substring(0, scopeStartIndex).Trim();

                    int scopeEndIndex = typeScope.IndexOf(")", scopeStartIndex);
                    if (scopeEndIndex > scopeStartIndex)
                    {
                        _scope = typeScope.Substring(scopeStartIndex + 1, scopeEndIndex - scopeStartIndex - 1).Trim();
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
        /// <param name="_args">命令行参数</param>
        /// <returns>验证结果</returns>
        public static int ValidateFromCommandLine(string[] _args)
        {
            try
            {
                if (_args.Length < 1)
                {
                    Console.Error.WriteLine("错误: 缺少提交消息文件路径参数");
                    return 1;
                }

                string filePath = _args[0];

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
                CommitMessageValidationResult result = s_Validator.ValidateFile(filePath);

                if (!result.IsValid)
                {
                    Console.Error.WriteLine(result.GetFormattedErrorMessage());
                    return 1;
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
