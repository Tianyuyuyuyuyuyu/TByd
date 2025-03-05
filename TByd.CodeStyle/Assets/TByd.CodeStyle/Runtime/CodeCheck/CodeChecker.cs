using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TByd.CodeStyle.Runtime.CodeCheck.Rules;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.CodeCheck
{
    /// <summary>
    /// 代码检查器，用于管理和执行代码检查规则
    /// </summary>
    public class CodeChecker
    {
        // 规则列表
        private readonly List<ICodeCheckRule> m_Rules = new List<ICodeCheckRule>();

        // 代码检查配置
        private readonly CodeCheckConfig m_Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">代码检查配置</param>
        public CodeChecker(CodeCheckConfig config)
        {
            m_Config = config;

            // 注册默认规则
            RegisterDefaultRules();
        }

        /// <summary>
        /// 注册默认规则
        /// </summary>
        private void RegisterDefaultRules()
        {
            // 命名规则
            RegisterRule(new PrivateFieldNamingRule());
            RegisterRule(new StaticFieldNamingRule());
            RegisterRule(new ConstantNamingRule());
            RegisterRule(new PropertyNamingRule());
            RegisterRule(new MethodNamingRule());
            RegisterRule(new ParameterNamingRule());

            // Unity规则
            RegisterRule(new AvoidGameObjectFindRule());
            RegisterRule(new AvoidFindInUpdateRule());
            RegisterRule(new AvoidGetComponentInUpdateRule());
            RegisterRule(new AvoidSendMessageRule());
            RegisterRule(new AvoidCoroutineStringRule());
            RegisterRule(new AvoidHardcodedPathRule());
            RegisterRule(new AvoidOnGUIRule());
            RegisterRule(new AvoidCameraMainRule());
            RegisterRule(new AvoidEmptyMonoBehaviourMethodsRule());
        }

        /// <summary>
        /// 注册规则
        /// </summary>
        /// <param name="rule">规则</param>
        public void RegisterRule(ICodeCheckRule rule)
        {
            if (rule == null)
                return;

            // 检查是否已注册同ID的规则
            var existingRule = m_Rules.FirstOrDefault(r => r.Id == rule.Id);
            if (existingRule != null)
            {
                Debug.LogWarning($"[TByd.CodeStyle] 规则'{rule.Id}'已经注册，将被跳过");
                return;
            }

            // 添加规则
            m_Rules.Add(rule);

            // 从配置中获取规则设置
            var ruleConfig = m_Config.GetRule(rule.Id);
            if (ruleConfig != null)
            {
                rule.Enabled = ruleConfig.Severity != CodeCheckConfig.RuleSeverity.k_Disabled;

                // 转换严重程度
                switch (ruleConfig.Severity)
                {
                    case CodeCheckConfig.RuleSeverity.k_Info:
                        rule.Severity = CodeCheckRuleSeverity.k_Info;
                        break;
                    case CodeCheckConfig.RuleSeverity.k_Warning:
                        rule.Severity = CodeCheckRuleSeverity.k_Warning;
                        break;
                    case CodeCheckConfig.RuleSeverity.k_Error:
                        rule.Severity = CodeCheckRuleSeverity.k_Error;
                        break;
                }
            }
        }

        /// <summary>
        /// 获取所有规则
        /// </summary>
        /// <returns>规则列表</returns>
        public List<ICodeCheckRule> GetRules()
        {
            return m_Rules;
        }

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <returns>规则</returns>
        public ICodeCheckRule GetRule(string ruleId)
        {
            return m_Rules.FirstOrDefault(r => r.Id == ruleId);
        }

        /// <summary>
        /// 启用规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        public void EnableRule(string ruleId)
        {
            var rule = GetRule(ruleId);

            if (rule != null)
            {
                rule.Enabled = true;
            }
        }

        /// <summary>
        /// 禁用规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        public void DisableRule(string ruleId)
        {
            var rule = GetRule(ruleId);

            if (rule != null)
            {
                rule.Enabled = false;
            }
        }

        /// <summary>
        /// 设置规则严重程度
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <param name="severity">严重程度</param>
        public void SetRuleSeverity(string ruleId, CodeCheckRuleSeverity severity)
        {
            var rule = GetRule(ruleId);

            if (rule != null)
            {
                rule.Severity = severity;
            }
        }

        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public CodeCheckResult CheckCode(string code, string filePath)
        {
            if (string.IsNullOrEmpty(code))
            {
                return CodeCheckResult.Success();
            }

            // 检查文件是否应该被忽略
            if (ShouldIgnoreFile(filePath))
            {
                return CodeCheckResult.Success();
            }

            var allIssues = new List<CodeCheckIssue>();

            // 执行所有启用的规则
            foreach (var rule in m_Rules.Where(r => r.Enabled))
            {
                try
                {
                    var result = rule.Check(code, filePath, m_Config);

                    if (!result.IsValid)
                    {
                        allIssues.AddRange(result.Issues);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[TByd.CodeStyle] 规则 '{rule.Id}' 执行异常: {e.Message}");
                }
            }

            if (allIssues.Count > 0)
            {
                return CodeCheckResult.Failure(allIssues);
            }

            return CodeCheckResult.Success();
        }

        /// <summary>
        /// 检查文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public CodeCheckResult CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"[TByd.CodeStyle] 文件不存在: {filePath}");
                return CodeCheckResult.Success();
            }

            // 检查文件是否应该被忽略
            if (ShouldIgnoreFile(filePath))
            {
                return CodeCheckResult.Success();
            }

            try
            {
                var code = File.ReadAllText(filePath);
                return CheckCode(code, filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 读取文件失败: {e.Message}");
                return CodeCheckResult.Success();
            }
        }

        /// <summary>
        /// 检查目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归检查子目录</param>
        /// <returns>检查结果</returns>
        public CodeCheckResult CheckDirectory(string directoryPath, bool recursive = true)
        {
            if (!Directory.Exists(directoryPath))
            {
                Debug.LogError($"[TByd.CodeStyle] 目录不存在: {directoryPath}");
                return CodeCheckResult.Success();
            }

            var allIssues = new List<CodeCheckIssue>();

            // 获取所有C#文件
            var files = Directory.GetFiles(
                directoryPath,
                "*.cs",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var filePath in files)
            {
                // 检查文件是否应该被忽略
                if (ShouldIgnoreFile(filePath))
                {
                    continue;
                }

                var result = CheckFile(filePath);

                if (!result.IsValid)
                {
                    allIssues.AddRange(result.Issues);
                }
            }

            if (allIssues.Count > 0)
            {
                return CodeCheckResult.Failure(allIssues);
            }

            return CodeCheckResult.Success();
        }

        /// <summary>
        /// 检查文件是否应该被忽略
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否应该被忽略</returns>
        private bool ShouldIgnoreFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return true;
            }

            // 检查文件扩展名
            if (!filePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // 检查忽略路径
            foreach (var ignorePath in m_Config.IgnoredPaths)
            {
                if (string.IsNullOrEmpty(ignorePath))
                {
                    continue;
                }

                // 支持通配符
                if (ignorePath.Contains("*"))
                {
                    var pattern = "^" + Regex.Escape(ignorePath).Replace("\\*", ".*") + "$";
                    if (Regex.IsMatch(filePath, pattern, RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
                else if (filePath.Contains(ignorePath))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 生成检查报告
        /// </summary>
        /// <param name="result">检查结果</param>
        /// <returns>报告文本</returns>
        public string GenerateReport(CodeCheckResult result)
        {
            if (result.IsValid)
            {
                return "代码检查通过，未发现问题。";
            }

            var sb = new StringBuilder();
            sb.AppendLine("代码检查报告");
            sb.AppendLine("====================");
            sb.AppendLine();

            // 按文件分组
            var issuesByFile = result.Issues
                .GroupBy(i => i.FilePath)
                .OrderBy(g => g.Key);

            foreach (var fileGroup in issuesByFile)
            {
                sb.AppendLine($"文件: {fileGroup.Key}");
                sb.AppendLine("--------------------");

                // 按行号排序
                var sortedIssues = fileGroup.OrderBy(i => i.LineNumber).ThenBy(i => i.ColumnNumber);

                foreach (var issue in sortedIssues)
                {
                    var severityStr = issue.Severity switch
                    {
                        CodeCheckRuleSeverity.k_Error => "[错误]",
                        CodeCheckRuleSeverity.k_Warning => "[警告]",
                        CodeCheckRuleSeverity.k_Info => "[信息]",
                        _ => "[未知]"
                    };

                    sb.AppendLine($"  {severityStr} 行 {issue.LineNumber}, 列 {issue.ColumnNumber}: {issue.Message} ({issue.RuleId})");

                    if (!string.IsNullOrEmpty(issue.FixSuggestion))
                    {
                        sb.AppendLine($"    建议: {issue.FixSuggestion}");
                    }

                    if (!string.IsNullOrEmpty(issue.CodeSnippet))
                    {
                        sb.AppendLine($"    代码: {issue.CodeSnippet}");
                    }

                    sb.AppendLine();
                }
            }

            // 添加统计信息
            var errorCount = result.Issues.Count(i => i.Severity == CodeCheckRuleSeverity.k_Error);
            var warningCount = result.Issues.Count(i => i.Severity == CodeCheckRuleSeverity.k_Warning);
            var infoCount = result.Issues.Count(i => i.Severity == CodeCheckRuleSeverity.k_Info);

            sb.AppendLine("====================");
            sb.AppendLine($"总计: {result.Issues.Count} 个问题");
            sb.AppendLine($"  错误: {errorCount}");
            sb.AppendLine($"  警告: {warningCount}");
            sb.AppendLine($"  信息: {infoCount}");

            return sb.ToString();
        }
    }
}
