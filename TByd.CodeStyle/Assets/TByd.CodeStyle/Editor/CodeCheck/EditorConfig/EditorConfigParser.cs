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
        private static readonly Regex s_SectionRegex = new(@"^\s*\[(.*)\]\s*$", RegexOptions.Compiled);
        private static readonly Regex s_PropertyRegex = new(@"^\s*([\w\-\.]+)\s*=\s*(.*?)\s*$", RegexOptions.Compiled);
        private static readonly Regex s_CommentRegex = new(@"^\s*[#;].*$", RegexOptions.Compiled);

        /// <summary>
        /// 解析EditorConfig文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>EditorConfig规则列表</returns>
        public static List<EditorConfigRule> ParseFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Debug.LogError($"[TByd.CodeStyle] EditorConfig文件不存在: {filePath}");
                return new List<EditorConfigRule>();
            }

            try
            {
                var lines = File.ReadAllLines(filePath);
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
        /// <param name="content">EditorConfig内容</param>
        /// <returns>EditorConfig规则列表</returns>
        public static List<EditorConfigRule> ParseContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new List<EditorConfigRule>();
            }

            try
            {
                var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
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
        /// <param name="lines">EditorConfig行</param>
        /// <returns>EditorConfig规则列表</returns>
        private static List<EditorConfigRule> ParseLines(string[] lines)
        {
            var rules = new List<EditorConfigRule>();
            EditorConfigRule currentRule = null;

            foreach (var line in lines)
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
                var sectionMatch = s_SectionRegex.Match(line);
                if (sectionMatch.Success)
                {
                    var pattern = sectionMatch.Groups[1].Value.Trim();
                    currentRule = new EditorConfigRule(pattern);
                    rules.Add(currentRule);
                    continue;
                }

                // 解析属性
                var propertyMatch = s_PropertyRegex.Match(line);
                if (propertyMatch.Success)
                {
                    var key = propertyMatch.Groups[1].Value.Trim();
                    var value = propertyMatch.Groups[2].Value.Trim();

                    // 处理root属性（特殊属性，不属于任何节）
                    if (key.ToLowerInvariant() == "root")
                    {
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
        /// <param name="rules">EditorConfig规则列表</param>
        /// <param name="isRoot">是否为根配置</param>
        /// <returns>EditorConfig内容</returns>
        public static string GenerateContent(List<EditorConfigRule> rules, bool isRoot = true)
        {
            if (rules == null || rules.Count == 0)
            {
                return string.Empty;
            }

            using (var writer = new StringWriter())
            {
                // 添加头部注释
                writer.WriteLine("# EditorConfig is awesome: https://editorconfig.org/");
                writer.WriteLine();

                // 添加root属性
                if (isRoot)
                {
                    writer.WriteLine("# top-most EditorConfig file");
                    writer.WriteLine("root = true");
                    writer.WriteLine();
                }

                // 添加规则
                foreach (var rule in rules)
                {
                    // 添加节
                    writer.WriteLine($"[{rule.Pattern}]");

                    // 添加属性
                    foreach (var property in rule.Properties)
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
