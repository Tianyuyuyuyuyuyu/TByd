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
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="config">代码检查配置</param>
        /// <returns>检查结果</returns>
        CodeCheckResult Check(string code, string filePath, CodeCheckConfig config);
    }

    /// <summary>
    /// 代码检查规则类别
    /// </summary>
    public enum CodeCheckRuleCategory
    {
        /// <summary>
        /// 命名规则
        /// </summary>
        k_Naming,

        /// <summary>
        /// 格式规则
        /// </summary>
        k_Formatting,

        /// <summary>
        /// 代码风格规则
        /// </summary>
        k_Style,

        /// <summary>
        /// 性能规则
        /// </summary>
        k_Performance,

        /// <summary>
        /// Unity特定规则
        /// </summary>
        k_Unity,

        /// <summary>
        /// 安全规则
        /// </summary>
        k_Security,

        /// <summary>
        /// 其他规则
        /// </summary>
        k_Other
    }

    /// <summary>
    /// 代码检查规则严重程度
    /// </summary>
    public enum CodeCheckRuleSeverity
    {
        /// <summary>
        /// 信息
        /// </summary>
        k_Info,

        /// <summary>
        /// 警告
        /// </summary>
        k_Warning,

        /// <summary>
        /// 错误
        /// </summary>
        k_Error
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
        /// <param name="issues">问题列表</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult Failure(List<CodeCheckIssue> issues)
        {
            return new CodeCheckResult
            {
                IsValid = false,
                Issues = issues
            };
        }

        /// <summary>
        /// 创建失败的检查结果
        /// </summary>
        /// <param name="issue">问题</param>
        /// <returns>检查结果</returns>
        public static CodeCheckResult Failure(CodeCheckIssue issue)
        {
            return new CodeCheckResult
            {
                IsValid = false,
                Issues = new List<CodeCheckIssue> { issue }
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
        public CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.k_Warning;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="config">代码检查配置</param>
        /// <returns>检查结果</returns>
        public abstract CodeCheckResult Check(string code, string filePath, CodeCheckConfig config);

        /// <summary>
        /// 创建代码检查问题
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineNumber">行号</param>
        /// <param name="columnNumber">列号</param>
        /// <param name="message">问题描述</param>
        /// <param name="fixSuggestion">修复建议</param>
        /// <param name="codeSnippet">问题代码</param>
        /// <returns>代码检查问题</returns>
        protected CodeCheckIssue CreateIssue(
            string filePath,
            int lineNumber,
            int columnNumber,
            string message,
            string fixSuggestion = null,
            string codeSnippet = null)
        {
            return new CodeCheckIssue
            {
                RuleId = Id,
                FilePath = filePath,
                LineNumber = lineNumber,
                ColumnNumber = columnNumber,
                Message = message,
                FixSuggestion = fixSuggestion,
                Severity = Severity,
                CodeSnippet = codeSnippet
            };
        }

        /// <summary>
        /// 获取代码行
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="lineNumber">行号（从1开始）</param>
        /// <returns>代码行</returns>
        protected string GetCodeLine(string code, int lineNumber)
        {
            var lines = code.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lineNumber <= 0 || lineNumber > lines.Length)
            {
                return string.Empty;
            }

            return lines[lineNumber - 1];
        }

        /// <summary>
        /// 获取代码行号和列号
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="position">位置</param>
        /// <returns>行号和列号</returns>
        protected (int lineNumber, int columnNumber) GetLineAndColumn(string code, int position)
        {
            if (position < 0 || position >= code.Length)
            {
                return (1, 1);
            }

            var lineNumber = 1;
            var columnNumber = 1;

            for (var i = 0; i < position; i++)
            {
                if (code[i] == '\n')
                {
                    lineNumber++;
                    columnNumber = 1;
                }
                else if (code[i] != '\r')
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
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="config">代码检查配置</param>
        /// <returns>检查结果</returns>
        public override CodeCheckResult Check(string code, string filePath, CodeCheckConfig config)
        {
            if (string.IsNullOrEmpty(code))
            {
                return CodeCheckResult.Success();
            }

            // 使用GetRegex方法获取正则表达式对象
            var regex = GetRegex();
            var matches = regex.Matches(code);

            if ((matches.Count > 0 && MatchIsIssue) || (matches.Count == 0 && !MatchIsIssue))
            {
                var issues = new List<CodeCheckIssue>();

                if (MatchIsIssue)
                {
                    // 匹配为问题
                    foreach (Match match in matches)
                    {
                        (var lineNumber, var columnNumber) = GetLineAndColumn(code, match.Index);
                        var codeSnippet = match.Value;

                        var message = FormatMessage(IssueMessageTemplate, match);
                        var fixSuggestion = FixSuggestionTemplate != null ? FormatMessage(FixSuggestionTemplate, match) : null;

                        issues.Add(CreateIssue(filePath, lineNumber, columnNumber, message, fixSuggestion, codeSnippet));
                    }
                }
                else
                {
                    // 不匹配为问题
                    var message = FormatMessage(IssueMessageTemplate, null);
                    issues.Add(CreateIssue(filePath, 1, 1, message, FixSuggestionTemplate, null));
                }

                return CodeCheckResult.Failure(issues);
            }

            return CodeCheckResult.Success();
        }

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected virtual string FormatMessage(string template, Match match)
        {
            if (match == null)
            {
                return template;
            }

            var result = template;

            // 替换{0}为完整匹配
            result = result.Replace("{0}", match.Value);

            // 替换{group_name}为命名组
            // 使用GetRegex方法获取正则表达式对象
            foreach (var groupName in GetRegex().GetGroupNames())
            {
                if (groupName != "0" && match.Groups[groupName].Success)
                {
                    result = result.Replace("{" + groupName + "}", match.Groups[groupName].Value);
                }
            }

            return result;
        }
    }
}
