using System;
using System.Collections.Generic;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Git.Commit
{
    /// <summary>
    /// 提交消息验证器，用于验证提交消息是否符合规范
    /// </summary>
    public class CommitMessageValidator
    {
        // 规则列表
        private readonly List<ICommitMessageRule> m_Rules = new List<ICommitMessageRule>();

        // Git提交配置
        private readonly GitCommitConfig m_Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_config">Git提交配置</param>
        public CommitMessageValidator(GitCommitConfig _config)
        {
            m_Config = _config;

            // 注册默认规则
            RegisterDefaultRules();
        }

        /// <summary>
        /// 注册默认规则
        /// </summary>
        private void RegisterDefaultRules()
        {
            // 格式规则
            m_Rules.Add(new FormatRule());

            // 提交类型规则
            m_Rules.Add(new CommitTypeRule());

            // 作用域规则
            m_Rules.Add(new ScopeRule());

            // 简短描述规则
            m_Rules.Add(new SubjectRule());

            // 详细描述规则
            m_Rules.Add(new BodyRule());

            // 页脚规则
            m_Rules.Add(new FooterRule());
        }

        /// <summary>
        /// 注册规则
        /// </summary>
        /// <param name="_rule">规则</param>
        public void RegisterRule(ICommitMessageRule _rule)
        {
            m_Rules.Add(_rule);
        }

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <returns>验证结果</returns>
        public CommitMessageValidationResult Validate(CommitMessage _message)
        {
            List<CommitMessageRuleResult> results = new List<CommitMessageRuleResult>();

            // 验证所有规则
            foreach (var rule in m_Rules)
            {
                try
                {
                    CommitMessageRuleResult result = rule.Validate(_message, m_Config);
                    results.Add(result);

                    // 如果验证失败，记录日志
                    if (!result.IsValid)
                    {
                        Debug.LogWarning($"[TByd.CodeStyle] 提交消息验证失败: {rule.Id} - {result.ErrorMessage}");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[TByd.CodeStyle] 规则验证异常: {rule.Id} - {e.Message}");

                    // 添加异常结果
                    results.Add(CommitMessageRuleResult.Failure($"规则验证异常: {e.Message}"));
                }
            }

            return new CommitMessageValidationResult(_message, results);
        }

        /// <summary>
        /// 验证提交消息文本
        /// </summary>
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>验证结果</returns>
        public CommitMessageValidationResult ValidateText(string _messageText)
        {
            // 添加调试日志
            Debug.Log($"[TByd.CodeStyle] ValidateText: '{_messageText}'");

            CommitMessage message = CommitMessageParser.Parse(_messageText);

            // 添加调试日志
            Debug.Log($"[TByd.CodeStyle] 解析结果: Type={message.Type}, Scope={message.Scope}, Subject='{message.Subject}'");

            return Validate(message);
        }

        /// <summary>
        /// 验证提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>验证结果</returns>
        public CommitMessageValidationResult ValidateFile(string _filePath)
        {
            CommitMessage message = CommitMessageParser.ParseFromFile(_filePath);
            return Validate(message);
        }
    }

    /// <summary>
    /// 提交消息验证结果
    /// </summary>
    public class CommitMessageValidationResult
    {
        // 提交消息
        private readonly CommitMessage m_Message;

        // 规则验证结果列表
        private readonly List<CommitMessageRuleResult> m_Results;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_results">规则验证结果列表</param>
        public CommitMessageValidationResult(CommitMessage _message, List<CommitMessageRuleResult> _results)
        {
            m_Message = _message;
            m_Results = _results;
        }

        /// <summary>
        /// 提交消息
        /// </summary>
        public CommitMessage Message => m_Message;

        /// <summary>
        /// 规则验证结果列表
        /// </summary>
        public List<CommitMessageRuleResult> Results => m_Results;

        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (var result in m_Results)
                {
                    if (!result.IsValid)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// 错误消息列表
        /// </summary>
        public List<string> Errors => GetErrorMessages();

        /// <summary>
        /// 获取错误消息列表
        /// </summary>
        /// <returns>错误消息列表</returns>
        public List<string> GetErrorMessages()
        {
            List<string> errorMessages = new List<string>();

            foreach (var result in m_Results)
            {
                if (!result.IsValid)
                {
                    errorMessages.Add(result.ErrorMessage);
                }
            }

            return errorMessages;
        }

        /// <summary>
        /// 获取修复建议列表
        /// </summary>
        /// <returns>修复建议列表</returns>
        public List<string> GetFixSuggestions()
        {
            List<string> fixSuggestions = new List<string>();

            foreach (var result in m_Results)
            {
                if (!result.IsValid && !string.IsNullOrEmpty(result.FixSuggestion))
                {
                    fixSuggestions.Add(result.FixSuggestion);
                }
            }

            return fixSuggestions;
        }

        /// <summary>
        /// 获取格式化的错误消息
        /// </summary>
        /// <returns>格式化的错误消息</returns>
        public string GetFormattedErrorMessage()
        {
            if (IsValid)
            {
                return "提交消息验证通过";
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("提交消息验证失败:");

            foreach (var result in m_Results)
            {
                if (!result.IsValid)
                {
                    sb.AppendLine($"- {result.ErrorMessage}");

                    if (!string.IsNullOrEmpty(result.FixSuggestion))
                    {
                        sb.AppendLine($"  建议: {result.FixSuggestion}");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
