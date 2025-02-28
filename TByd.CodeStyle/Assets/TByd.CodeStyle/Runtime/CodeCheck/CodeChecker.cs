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
        /// <param name="_config">代码检查配置</param>
        public CodeChecker(CodeCheckConfig _config)
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
        /// <param name="_rule">规则</param>
        public void RegisterRule(ICodeCheckRule _rule)
        {
            // 检查规则是否已存在
            if (m_Rules.Any(r => r.Id == _rule.Id))
            {
                Debug.LogWarning($"[TByd.CodeStyle] 规则 '{_rule.Id}' 已存在，将被替换");
                
                // 移除已存在的规则
                m_Rules.RemoveAll(r => r.Id == _rule.Id);
            }
            
            // 添加规则
            m_Rules.Add(_rule);
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
        /// <param name="_ruleId">规则ID</param>
        /// <returns>规则</returns>
        public ICodeCheckRule GetRule(string _ruleId)
        {
            return m_Rules.FirstOrDefault(r => r.Id == _ruleId);
        }
        
        /// <summary>
        /// 启用规则
        /// </summary>
        /// <param name="_ruleId">规则ID</param>
        public void EnableRule(string _ruleId)
        {
            ICodeCheckRule rule = GetRule(_ruleId);
            
            if (rule != null)
            {
                rule.Enabled = true;
            }
        }
        
        /// <summary>
        /// 禁用规则
        /// </summary>
        /// <param name="_ruleId">规则ID</param>
        public void DisableRule(string _ruleId)
        {
            ICodeCheckRule rule = GetRule(_ruleId);
            
            if (rule != null)
            {
                rule.Enabled = false;
            }
        }
        
        /// <summary>
        /// 设置规则严重程度
        /// </summary>
        /// <param name="_ruleId">规则ID</param>
        /// <param name="_severity">严重程度</param>
        public void SetRuleSeverity(string _ruleId, CodeCheckRuleSeverity _severity)
        {
            ICodeCheckRule rule = GetRule(_ruleId);
            
            if (rule != null)
            {
                rule.Severity = _severity;
            }
        }
        
        /// <summary>
        /// 检查代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public CodeCheckResult CheckCode(string _code, string _filePath)
        {
            if (string.IsNullOrEmpty(_code))
            {
                return CodeCheckResult.Success();
            }
            
            // 检查文件是否应该被忽略
            if (ShouldIgnoreFile(_filePath))
            {
                return CodeCheckResult.Success();
            }
            
            List<CodeCheckIssue> allIssues = new List<CodeCheckIssue>();
            
            // 执行所有启用的规则
            foreach (ICodeCheckRule rule in m_Rules.Where(r => r.Enabled))
            {
                try
                {
                    CodeCheckResult result = rule.Check(_code, _filePath, m_Config);
                    
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
        /// <param name="_filePath">文件路径</param>
        /// <returns>检查结果</returns>
        public CodeCheckResult CheckFile(string _filePath)
        {
            if (!File.Exists(_filePath))
            {
                Debug.LogError($"[TByd.CodeStyle] 文件不存在: {_filePath}");
                return CodeCheckResult.Success();
            }
            
            // 检查文件是否应该被忽略
            if (ShouldIgnoreFile(_filePath))
            {
                return CodeCheckResult.Success();
            }
            
            try
            {
                string code = File.ReadAllText(_filePath);
                return CheckCode(code, _filePath);
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
        /// <param name="_directoryPath">目录路径</param>
        /// <param name="_recursive">是否递归检查子目录</param>
        /// <returns>检查结果</returns>
        public CodeCheckResult CheckDirectory(string _directoryPath, bool _recursive = true)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Debug.LogError($"[TByd.CodeStyle] 目录不存在: {_directoryPath}");
                return CodeCheckResult.Success();
            }
            
            List<CodeCheckIssue> allIssues = new List<CodeCheckIssue>();
            
            // 获取所有C#文件
            string[] files = Directory.GetFiles(
                _directoryPath, 
                "*.cs", 
                _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            
            foreach (string filePath in files)
            {
                // 检查文件是否应该被忽略
                if (ShouldIgnoreFile(filePath))
                {
                    continue;
                }
                
                CodeCheckResult result = CheckFile(filePath);
                
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
        /// <param name="_filePath">文件路径</param>
        /// <returns>是否应该被忽略</returns>
        private bool ShouldIgnoreFile(string _filePath)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                return true;
            }
            
            // 检查文件扩展名
            if (!_filePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            
            // 检查忽略路径
            foreach (string ignorePath in m_Config.IgnoredPaths)
            {
                if (string.IsNullOrEmpty(ignorePath))
                {
                    continue;
                }
                
                // 支持通配符
                if (ignorePath.Contains("*"))
                {
                    string pattern = "^" + Regex.Escape(ignorePath).Replace("\\*", ".*") + "$";
                    if (Regex.IsMatch(_filePath, pattern, RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
                else if (_filePath.Contains(ignorePath))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// 生成检查报告
        /// </summary>
        /// <param name="_result">检查结果</param>
        /// <returns>报告文本</returns>
        public string GenerateReport(CodeCheckResult _result)
        {
            if (_result.IsValid)
            {
                return "代码检查通过，未发现问题。";
            }
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("代码检查报告");
            sb.AppendLine("====================");
            sb.AppendLine();
            
            // 按文件分组
            var issuesByFile = _result.Issues
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
                    string severityStr = issue.Severity switch
                    {
                        CodeCheckRuleSeverity.Error => "[错误]",
                        CodeCheckRuleSeverity.Warning => "[警告]",
                        CodeCheckRuleSeverity.Info => "[信息]",
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
            int errorCount = _result.Issues.Count(i => i.Severity == CodeCheckRuleSeverity.Error);
            int warningCount = _result.Issues.Count(i => i.Severity == CodeCheckRuleSeverity.Warning);
            int infoCount = _result.Issues.Count(i => i.Severity == CodeCheckRuleSeverity.Info);
            
            sb.AppendLine("====================");
            sb.AppendLine($"总计: {_result.Issues.Count} 个问题");
            sb.AppendLine($"  错误: {errorCount}");
            sb.AppendLine($"  警告: {warningCount}");
            sb.AppendLine($"  信息: {infoCount}");
            
            return sb.ToString();
        }
    }
} 