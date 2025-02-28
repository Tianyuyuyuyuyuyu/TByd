using System;
using System.Collections.Generic;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Git.Commit
{
    /// <summary>
    /// 提交消息规则接口，用于验证提交消息的各个部分
    /// </summary>
    public interface ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 规则描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config);
    }

    /// <summary>
    /// 提交消息规则验证结果
    /// </summary>
    public class CommitMessageRuleResult
    {
        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 修复建议
        /// </summary>
        public string FixSuggestion { get; set; }

        /// <summary>
        /// 创建成功的验证结果
        /// </summary>
        /// <returns>验证结果</returns>
        public static CommitMessageRuleResult Success()
        {
            return new CommitMessageRuleResult
            {
                IsValid = true
            };
        }

        /// <summary>
        /// 创建失败的验证结果
        /// </summary>
        /// <param name="_errorMessage">错误消息</param>
        /// <param name="_fixSuggestion">修复建议</param>
        /// <returns>验证结果</returns>
        public static CommitMessageRuleResult Failure(string _errorMessage, string _fixSuggestion = null)
        {
            return new CommitMessageRuleResult
            {
                IsValid = false,
                ErrorMessage = _errorMessage,
                FixSuggestion = _fixSuggestion
            };
        }
    }

    /// <summary>
    /// 提交类型规则，验证提交类型是否有效
    /// </summary>
    public class CommitTypeRule : ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string Id => "commit-type";

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description => "提交类型必须是配置中定义的有效类型之一";

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        public CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config)
        {
            // 如果不要求提交类型，则跳过验证
            if (!_config.RequireType)
            {
                return CommitMessageRuleResult.Success();
            }

            // 如果提交类型为空，则验证失败
            if (string.IsNullOrEmpty(_message.Type))
            {
                return CommitMessageRuleResult.Failure(
                    "提交类型不能为空",
                    "请添加有效的提交类型，例如: feat: 添加新功能");
            }

            // 检查提交类型是否在配置的有效类型列表中
            bool isValidType = false;
            foreach (var commitType in _config.CommitTypes)
            {
                if (commitType.Enabled && commitType.Type.Equals(_message.Type, StringComparison.OrdinalIgnoreCase))
                {
                    isValidType = true;
                    break;
                }
            }

            if (!isValidType)
            {
                // 构建有效类型列表
                List<string> validTypes = new List<string>();
                foreach (var commitType in _config.CommitTypes)
                {
                    if (commitType.Enabled)
                    {
                        validTypes.Add(commitType.Type);
                    }
                }

                string validTypesStr = string.Join(", ", validTypes);

                return CommitMessageRuleResult.Failure(
                    $"提交类型 '{_message.Type}' 无效",
                    $"有效的提交类型: {validTypesStr}");
            }

            return CommitMessageRuleResult.Success();
        }
    }

    /// <summary>
    /// 作用域规则，验证作用域是否有效
    /// </summary>
    public class ScopeRule : ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string Id => "commit-scope";

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description => "如果配置要求作用域，则作用域必须是配置中定义的有效作用域之一";

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        public CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config)
        {
            // 如果不要求作用域，则跳过验证
            if (!_config.RequireScope)
            {
                return CommitMessageRuleResult.Success();
            }

            // 如果作用域为空，则验证失败
            if (string.IsNullOrEmpty(_message.Scope))
            {
                return CommitMessageRuleResult.Failure(
                    "作用域不能为空",
                    "请添加有效的作用域，例如: feat(ui): 添加登录界面");
            }

            // 检查作用域是否在配置的有效作用域列表中
            bool isValidScope = false;

            // 如果作用域列表为空，则任何非空作用域都是有效的
            if (_config.Scopes == null || _config.Scopes.Count == 0)
            {
                isValidScope = true;
            }
            else
            {
                isValidScope = _config.Scopes.Contains(_message.Scope);
            }

            if (!isValidScope)
            {
                string validScopesStr = _config.Scopes != null && _config.Scopes.Count > 0
                    ? string.Join(", ", _config.Scopes)
                    : "未配置有效作用域";

                return CommitMessageRuleResult.Failure(
                    $"作用域 '{_message.Scope}' 无效",
                    $"有效的作用域: {validScopesStr}");
            }

            return CommitMessageRuleResult.Success();
        }
    }

    /// <summary>
    /// 简短描述规则，验证简短描述是否符合要求
    /// </summary>
    public class SubjectRule : ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string Id => "commit-subject";

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description => "简短描述不能为空，且长度不能超过配置的最大长度";

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        public CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config)
        {
            // 添加调试日志
            Debug.Log($"[TByd.CodeStyle] SubjectRule.Validate: Subject='{_message.Subject}', RequireSubject={_config.RequireSubject}");

            // 如果不要求简短描述，则跳过验证
            if (!_config.RequireSubject)
            {
                Debug.Log($"[TByd.CodeStyle] SubjectRule.Validate: 不要求简短描述，跳过验证");
                return CommitMessageRuleResult.Success();
            }

            // 如果简短描述为空或只包含空白字符，则验证失败
            if (string.IsNullOrWhiteSpace(_message.Subject))
            {
                Debug.Log($"[TByd.CodeStyle] SubjectRule.Validate: 简短描述为空，验证失败");
                return CommitMessageRuleResult.Failure(
                    "简短描述不能为空",
                    "请添加简短描述，例如: feat: 添加登录界面");
            }

            // 检查简短描述长度是否超过配置的最大长度
            if (_message.Subject.Length > _config.SubjectMaxLength)
            {
                return CommitMessageRuleResult.Failure(
                    $"简短描述长度超过限制（最大{_config.SubjectMaxLength}个字符）",
                    $"当前长度: {_message.Subject.Length}，请精简描述");
            }

            return CommitMessageRuleResult.Success();
        }
    }

    /// <summary>
    /// 详细描述规则，验证详细描述是否符合要求
    /// </summary>
    public class BodyRule : ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string Id => "commit-body";

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description => "如果配置要求详细描述，则详细描述不能为空";

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        public CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config)
        {
            // 如果不要求详细描述，则跳过验证
            if (!_config.RequireBody)
            {
                return CommitMessageRuleResult.Success();
            }

            // 如果详细描述为空，则验证失败
            if (string.IsNullOrEmpty(_message.Body))
            {
                return CommitMessageRuleResult.Failure(
                    "详细描述不能为空",
                    "请添加详细描述，解释为什么进行此次修改");
            }

            return CommitMessageRuleResult.Success();
        }
    }

    /// <summary>
    /// 页脚规则，验证页脚是否符合要求
    /// </summary>
    public class FooterRule : ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string Id => "commit-footer";

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description => "如果配置要求页脚，则页脚不能为空";

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        public CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config)
        {
            // 如果不要求页脚，则跳过验证
            if (!_config.RequireFooter)
            {
                return CommitMessageRuleResult.Success();
            }

            // 如果页脚为空，则验证失败
            if (string.IsNullOrEmpty(_message.Footer))
            {
                return CommitMessageRuleResult.Failure(
                    "页脚不能为空",
                    "请添加页脚，例如关闭的问题: Closes #123");
            }

            return CommitMessageRuleResult.Success();
        }
    }

    /// <summary>
    /// 格式规则，验证提交消息格式是否符合要求
    /// </summary>
    public class FormatRule : ICommitMessageRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string Id => "commit-format";

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description => "提交消息必须符合格式要求: <type>(<scope>): <subject>";

        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <param name="_config">Git提交配置</param>
        /// <returns>验证结果</returns>
        public CommitMessageRuleResult Validate(CommitMessage _message, GitCommitConfig _config)
        {
            // 检查原始消息是否包含冒号
            if (!_message.RawHeader.Contains(":"))
            {
                return CommitMessageRuleResult.Failure(
                    "提交消息格式错误：缺少冒号",
                    "正确格式为: <type>(<scope>): <subject>");
            }

            // 检查冒号后是否有空格
            if (_message.RawHeader.Contains(":") && !_message.RawHeader.Contains(": "))
            {
                return CommitMessageRuleResult.Failure(
                    "提交消息格式错误：冒号后应有空格",
                    "正确格式为: <type>(<scope>): <subject>");
            }

            // 检查类型是否为空
            if (string.IsNullOrEmpty(_message.Type))
            {
                return CommitMessageRuleResult.Failure(
                    "提交消息格式错误：缺少类型",
                    "正确格式为: <type>(<scope>): <subject>");
            }

            return CommitMessageRuleResult.Success();
        }
    }
}
