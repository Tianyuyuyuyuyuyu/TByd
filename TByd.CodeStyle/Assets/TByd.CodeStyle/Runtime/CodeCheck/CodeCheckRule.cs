using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Runtime.CodeCheck
{
    /// <summary>
    /// 代码检查规则接口，用于验证代码是否符合规范
    /// </summary>
    public interface ICodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// 规则名称
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 规则描述
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 规则类别
        /// </summary>
        CodeCheckRuleCategory Category { get; }
        
        /// <summary>
        /// 规则严重程度
        /// </summary>
        CodeCheckRuleSeverity Severity { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        bool Enabled { get; set; }
        
        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_config">代码检查配置</param>
        /// <returns>检查结果</returns>
        CodeCheckResult Check(string _code, string _filePath, CodeCheckConfig _config);
    }
    
    /// <summary>
    /// 代码检查规则类别
    /// </summary>
    public enum CodeCheckRuleCategory
    {
        /// <summary>
        /// 命名规则
        /// </summary>
        Naming,
        
        /// <summary>
        /// 格式规则
        /// </summary>
        Formatting,
        
        /// <summary>
        /// 代码风格规则
        /// </summary>
        Style,
        
        /// <summary>
        /// 性能规则
        /// </summary>
        Performance,
        
        /// <summary>
        /// Unity特定规则
        /// </summary>
        Unity,
        
        /// <summary>
        /// 安全规则
        /// </summary>
        Security,
        
        /// <summary>
        /// 其他规则
        /// </summary>
        Other
    }
    
    /// <summary>
    /// 代码检查规则严重程度
    /// </summary>
    public enum CodeCheckRuleSeverity
    {
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        
        /// <summary>
        /// 错误
        /// </summary>
        Error
    }
    
    /// <summary>
    /// 代码检查结果
    /// </summary>
    public class CodeCheckResult
    {
        /// <summary>
        /// 是否通过检查
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 问题列表
        /// </summary>
        public List<CodeCheckIssue> Issues { get; set; } = new List<CodeCheckIssue>();
        
        /// <summary>
        /// 创建成功的检查结果
        /// </summary>
        /// <returns>检查结果</returns>
        public static CodeCheckResult Success()
        {
            return new CodeCheckResult
            {
                IsValid = true
            };
        }
        
        /// <summary>
        /// 创建失败的检查结果
        /// </summary>
        /// <param name="_issues">问题列表</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult Failure(List<CodeCheckIssue> _issues)
        {
            return new CodeCheckResult
            {
                IsValid = false,
                Issues = _issues
            };
        }
        
        /// <summary>
        /// 创建失败的检查结果
        /// </summary>
        /// <param name="_issue">问题</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult Failure(CodeCheckIssue _issue)
        {
            return new CodeCheckResult
            {
                IsValid = false,
                Issues = new List<CodeCheckIssue> { _issue }
            };
        }
    }
    
    /// <summary>
    /// 代码检查问题
    /// </summary>
    public class CodeCheckIssue
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public string RuleId { get; set; }
        
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// 行号
        /// </summary>
        public int LineNumber { get; set; }
        
        /// <summary>
        /// 列号
        /// </summary>
        public int ColumnNumber { get; set; }
        
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// 修复建议
        /// </summary>
        public string FixSuggestion { get; set; }
        
        /// <summary>
        /// 严重程度
        /// </summary>
        public CodeCheckRuleSeverity Severity { get; set; }
        
        /// <summary>
        /// 问题代码
        /// </summary>
        public string CodeSnippet { get; set; }
    }
    
    /// <summary>
    /// 代码检查规则基类
    /// </summary>
    public abstract class CodeCheckRuleBase : ICodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public abstract string Id { get; }
        
        /// <summary>
        /// 规则名称
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// 规则描述
        /// </summary>
        public abstract string Description { get; }
        
        /// <summary>
        /// 规则类别
        /// </summary>
        public abstract CodeCheckRuleCategory Category { get; }
        
        /// <summary>
        /// 规则严重程度
        /// </summary>
        public CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;
        
        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_config">代码检查配置</param>
        /// <returns>检查结果</returns>
        public abstract CodeCheckResult Check(string _code, string _filePath, CodeCheckConfig _config);
        
        /// <summary>
        /// 创建代码检查问题
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_lineNumber">行号</param>
        /// <param name="_columnNumber">列号</param>
        /// <param name="_message">问题描述</param>
        /// <param name="_fixSuggestion">修复建议</param>
        /// <param name="_codeSnippet">问题代码</param>
        /// <returns>代码检查问题</returns>
        protected CodeCheckIssue CreateIssue(
            string _filePath, 
            int _lineNumber, 
            int _columnNumber, 
            string _message, 
            string _fixSuggestion = null, 
            string _codeSnippet = null)
        {
            return new CodeCheckIssue
            {
                RuleId = Id,
                FilePath = _filePath,
                LineNumber = _lineNumber,
                ColumnNumber = _columnNumber,
                Message = _message,
                FixSuggestion = _fixSuggestion,
                Severity = Severity,
                CodeSnippet = _codeSnippet
            };
        }
        
        /// <summary>
        /// 获取代码行
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_lineNumber">行号（从1开始）</param>
        /// <returns>代码行</returns>
        protected string GetCodeLine(string _code, int _lineNumber)
        {
            string[] lines = _code.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (_lineNumber <= 0 || _lineNumber > lines.Length)
            {
                return string.Empty;
            }
            
            return lines[_lineNumber - 1];
        }
        
        /// <summary>
        /// 获取代码行号和列号
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_position">位置</param>
        /// <returns>行号和列号</returns>
        protected (int lineNumber, int columnNumber) GetLineAndColumn(string _code, int _position)
        {
            if (_position < 0 || _position >= _code.Length)
            {
                return (1, 1);
            }
            
            int lineNumber = 1;
            int columnNumber = 1;
            
            for (int i = 0; i < _position; i++)
            {
                if (_code[i] == '\n')
                {
                    lineNumber++;
                    columnNumber = 1;
                }
                else if (_code[i] != '\r')
                {
                    columnNumber++;
                }
            }
            
            return (lineNumber, columnNumber);
        }
    }
    
    /// <summary>
    /// 正则表达式检查规则基类
    /// </summary>
    public abstract class RegexCodeCheckRule : CodeCheckRuleBase
    {
        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected abstract string Pattern { get; }
        
        /// <summary>
        /// 正则表达式选项
        /// </summary>
        protected virtual RegexOptions Options => RegexOptions.Compiled;
        
        /// <summary>
        /// 是否匹配为问题（true表示匹配为问题，false表示不匹配为问题）
        /// </summary>
        protected virtual bool MatchIsIssue => true;
        
        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected abstract string IssueMessageTemplate { get; }
        
        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected virtual string FixSuggestionTemplate => null;
        
        // 添加私有字段存储正则表达式对象
        private Regex m_Regex;
        
        /// <summary>
        /// 获取正则表达式对象
        /// </summary>
        /// <returns>正则表达式对象</returns>
        protected Regex GetRegex()
        {
            if (m_Regex == null)
            {
                m_Regex = new Regex(Pattern, Options);
            }
            
            return m_Regex;
        }
        
        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_config">代码检查配置</param>
        /// <returns>检查结果</returns>
        public override CodeCheckResult Check(string _code, string _filePath, CodeCheckConfig _config)
        {
            if (string.IsNullOrEmpty(_code))
            {
                return CodeCheckResult.Success();
            }
            
            // 使用GetRegex方法获取正则表达式对象
            Regex regex = GetRegex();
            MatchCollection matches = regex.Matches(_code);
            
            if ((matches.Count > 0 && MatchIsIssue) || (matches.Count == 0 && !MatchIsIssue))
            {
                List<CodeCheckIssue> issues = new List<CodeCheckIssue>();
                
                if (MatchIsIssue)
                {
                    // 匹配为问题
                    foreach (Match match in matches)
                    {
                        (int lineNumber, int columnNumber) = GetLineAndColumn(_code, match.Index);
                        string codeSnippet = match.Value;
                        
                        string message = FormatMessage(IssueMessageTemplate, match);
                        string fixSuggestion = FixSuggestionTemplate != null ? FormatMessage(FixSuggestionTemplate, match) : null;
                        
                        issues.Add(CreateIssue(_filePath, lineNumber, columnNumber, message, fixSuggestion, codeSnippet));
                    }
                }
                else
                {
                    // 不匹配为问题
                    string message = FormatMessage(IssueMessageTemplate, null);
                    issues.Add(CreateIssue(_filePath, 1, 1, message, FixSuggestionTemplate, null));
                }
                
                return CodeCheckResult.Failure(issues);
            }
            
            return CodeCheckResult.Success();
        }
        
        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected virtual string FormatMessage(string _template, Match _match)
        {
            if (_match == null)
            {
                return _template;
            }
            
            string result = _template;
            
            // 替换{0}为完整匹配
            result = result.Replace("{0}", _match.Value);
            
            // 替换{group_name}为命名组
            // 使用GetRegex方法获取正则表达式对象
            foreach (string groupName in GetRegex().GetGroupNames())
            {
                if (groupName != "0" && _match.Groups[groupName].Success)
                {
                    result = result.Replace("{" + groupName + "}", _match.Groups[groupName].Value);
                }
            }
            
            return result;
        }
    }
} 