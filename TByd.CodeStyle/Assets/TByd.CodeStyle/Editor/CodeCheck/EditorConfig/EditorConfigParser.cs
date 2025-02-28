using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TByd.CodeStyle.Editor.CodeCheck.EditorConfig
{
    /// <summary>
    /// EditorConfig解析器
    /// </summary>
    public static class EditorConfigParser
    {
        // 部分正则表达式
        private static readonly Regex s_SectionRegex = new Regex(@"^\s*\[(.*)\]\s*$", RegexOptions.Compiled);
        private static readonly Regex s_PropertyRegex = new Regex(@"^\s*([\w\-\.]+)\s*=\s*(.*?)\s*$", RegexOptions.Compiled);
        private static readonly Regex s_CommentRegex = new Regex(@"^\s*[#;].*$", RegexOptions.Compiled);
        
        /// <summary>
        /// 解析EditorConfig文件
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <returns>EditorConfig规则列表</returns>
        public static List<EditorConfigRule> ParseFile(string _filePath)
        {
            if (string.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
            {
                Debug.LogError($"[TByd.CodeStyle] EditorConfig文件不存在: {_filePath}");
                return new List<EditorConfigRule>();
            }
            
            try
            {
                string[] lines = File.ReadAllLines(_filePath);
                return ParseLines(lines);
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 解析EditorConfig文件失败: {e.Message}");
                return new List<EditorConfigRule>();
            }
        }
        
        /// <summary>
        /// 解析EditorConfig内容
        /// </summary>
        /// <param name="_content">EditorConfig内容</param>
        /// <returns>EditorConfig规则列表</returns>
        public static List<EditorConfigRule> ParseContent(string _content)
        {
            if (string.IsNullOrEmpty(_content))
            {
                return new List<EditorConfigRule>();
            }
            
            try
            {
                string[] lines = _content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                return ParseLines(lines);
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 解析EditorConfig内容失败: {e.Message}");
                return new List<EditorConfigRule>();
            }
        }
        
        /// <summary>
        /// 解析EditorConfig行
        /// </summary>
        /// <param name="_lines">EditorConfig行</param>
        /// <returns>EditorConfig规则列表</returns>
        private static List<EditorConfigRule> ParseLines(string[] _lines)
        {
            List<EditorConfigRule> rules = new List<EditorConfigRule>();
            EditorConfigRule currentRule = null;
            bool isRoot = false;
            
            foreach (string line in _lines)
            {
                // 跳过空行
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                
                // 跳过注释行
                if (s_CommentRegex.IsMatch(line))
                {
                    continue;
                }
                
                // 解析节（文件匹配模式）
                Match sectionMatch = s_SectionRegex.Match(line);
                if (sectionMatch.Success)
                {
                    string pattern = sectionMatch.Groups[1].Value.Trim();
                    currentRule = new EditorConfigRule(pattern);
                    rules.Add(currentRule);
                    continue;
                }
                
                // 解析属性
                Match propertyMatch = s_PropertyRegex.Match(line);
                if (propertyMatch.Success)
                {
                    string key = propertyMatch.Groups[1].Value.Trim();
                    string value = propertyMatch.Groups[2].Value.Trim();
                    
                    // 处理root属性（特殊属性，不属于任何节）
                    if (key.ToLowerInvariant() == "root")
                    {
                        isRoot = value.ToLowerInvariant() == "true";
                        continue;
                    }
                    
                    // 如果当前没有节，则跳过
                    if (currentRule == null)
                    {
                        Debug.LogWarning($"[TByd.CodeStyle] EditorConfig属性 '{key}' 没有关联的节，将被忽略");
                        continue;
                    }
                    
                    currentRule.SetProperty(key, value);
                }
            }
            
            return rules;
        }
        
        /// <summary>
        /// 生成EditorConfig内容
        /// </summary>
        /// <param name="_rules">EditorConfig规则列表</param>
        /// <param name="_isRoot">是否为根配置</param>
        /// <returns>EditorConfig内容</returns>
        public static string GenerateContent(List<EditorConfigRule> _rules, bool _isRoot = true)
        {
            if (_rules == null || _rules.Count == 0)
            {
                return string.Empty;
            }
            
            using (StringWriter writer = new StringWriter())
            {
                // 添加头部注释
                writer.WriteLine("# EditorConfig is awesome: https://editorconfig.org/");
                writer.WriteLine();
                
                // 添加root属性
                if (_isRoot)
                {
                    writer.WriteLine("# top-most EditorConfig file");
                    writer.WriteLine("root = true");
                    writer.WriteLine();
                }
                
                // 添加规则
                foreach (EditorConfigRule rule in _rules)
                {
                    // 添加节
                    writer.WriteLine($"[{rule.Pattern}]");
                    
                    // 添加属性
                    foreach (KeyValuePair<string, string> property in rule.Properties)
                    {
                        writer.WriteLine($"{property.Key} = {property.Value}");
                    }
                    
                    writer.WriteLine();
                }
                
                return writer.ToString();
            }
        }
    }
} 