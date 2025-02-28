using System;
using System.Collections.Generic;
using TByd.CodeStyle.Runtime.Config;

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
            // 如果不要求简短描述，则跳过验证
            if (!_config.RequireSubject)
            {
                return CommitMessageRuleResult.Success();
            }
            
            // 如果简短描述为空，则验证失败
            if (string.IsNullOrEmpty(_message.Subject))
            {
                return CommitMessageRuleResult.Failure(
                    "简短描述不能为空",
                    "请添加简短描述，例如: feat: 添加登录界面");
            }
            
            // 检查简短描述长度是否超过最大长度
            if (_message.Subject.Length > _config.SubjectMaxLength)
            {
                return CommitMessageRuleResult.Failure(
                    $"简短描述长度不能超过 {_config.SubjectMaxLength} 个字符",
                    $"当前长度: {_message.Subject.Length}，请缩短简短描述");
            }
            
            // 检查简短描述是否以句号结尾
            if (_message.Subject.EndsWith("."))
            {
                return CommitMessageRuleResult.Failure(
                    "简短描述不应以句号结尾",
                    "请移除简短描述末尾的句号");
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
    /// 格式规则，验证提交消息的整体格式是否符合要求
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
            // 如果提交消息为空，则验证失败
            if (_message.IsEmpty())
            {
                return CommitMessageRuleResult.Failure(
                    "提交消息不能为空",
                    "请按照格式填写提交消息: <type>(<scope>): <subject>");
            }
            
            // 如果原始消息不符合格式要求，则验证失败
            if (!CommitMessageParser.IsValidFormat(_message.RawMessage))
            {
                return CommitMessageRuleResult.Failure(
                    "提交消息格式不正确",
                    "正确格式: <type>(<scope>): <subject>\n例如: feat(ui): 添加登录界面");
            }
            
            return CommitMessageRuleResult.Success();
        }
    }
} 