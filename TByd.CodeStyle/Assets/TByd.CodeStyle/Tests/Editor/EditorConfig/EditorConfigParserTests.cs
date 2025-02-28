using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using UnityEngine;

namespace TByd.CodeStyle.Tests.Editor.EditorConfig
{
    /// <summary>
    /// EditorConfig解析器测试
    /// </summary>
    public class EditorConfigParserTests
    {
        // 测试EditorConfig内容
        private const string c_TestEditorConfigContent = @"# EditorConfig is awesome: https://editorconfig.org/

# top-most EditorConfig file
root = true

# Unix-style newlines with a newline ending every file
[*]
end_of_line = lf
insert_final_newline = true
charset = utf-8
trim_trailing_whitespace = true

# 4 space indentation for C# files
[*.cs]
indent_style = space
indent_size = 4
tab_width = 4

# 2 space indentation for JSON files
[*.json]
indent_style = space
indent_size = 2

# Tab indentation for Makefiles
[Makefile]
indent_style = tab
";
        
        /// <summary>
        /// 测试解析EditorConfig内容
        /// </summary>
        [Test]
        public void ParseContent_ValidContent_ReturnsCorrectRules()
        {
            // 解析测试内容
            List<EditorConfigRule> rules = EditorConfigParser.ParseContent(c_TestEditorConfigContent);
            
            // 验证规则数量
            Assert.AreEqual(4, rules.Count, "应该解析出4个规则");
            
            // 验证第一个规则 [*]
            EditorConfigRule allFilesRule = rules[0];
            Assert.AreEqual("*", allFilesRule.Pattern, "第一个规则的模式应该是 *");
            Assert.AreEqual(4, allFilesRule.Properties.Count, "第一个规则应该有4个属性");
            Assert.AreEqual("lf", allFilesRule.GetProperty("end_of_line"), "end_of_line 属性值不正确");
            Assert.AreEqual("true", allFilesRule.GetProperty("insert_final_newline"), "insert_final_newline 属性值不正确");
            Assert.AreEqual("utf-8", allFilesRule.GetProperty("charset"), "charset 属性值不正确");
            Assert.AreEqual("true", allFilesRule.GetProperty("trim_trailing_whitespace"), "trim_trailing_whitespace 属性值不正确");
            
            // 验证第二个规则 [*.cs]
            EditorConfigRule csharpRule = rules[1];
            Assert.AreEqual("*.cs", csharpRule.Pattern, "第二个规则的模式应该是 *.cs");
            Assert.AreEqual(3, csharpRule.Properties.Count, "第二个规则应该有3个属性");
            Assert.AreEqual("space", csharpRule.GetProperty("indent_style"), "indent_style 属性值不正确");
            Assert.AreEqual("4", csharpRule.GetProperty("indent_size"), "indent_size 属性值不正确");
            Assert.AreEqual("4", csharpRule.GetProperty("tab_width"), "tab_width 属性值不正确");
            
            // 验证第三个规则 [*.json]
            EditorConfigRule jsonRule = rules[2];
            Assert.AreEqual("*.json", jsonRule.Pattern, "第三个规则的模式应该是 *.json");
            Assert.AreEqual(2, jsonRule.Properties.Count, "第三个规则应该有2个属性");
            Assert.AreEqual("space", jsonRule.GetProperty("indent_style"), "indent_style 属性值不正确");
            Assert.AreEqual("2", jsonRule.GetProperty("indent_size"), "indent_size 属性值不正确");
            
            // 验证第四个规则 [Makefile]
            EditorConfigRule makefileRule = rules[3];
            Assert.AreEqual("Makefile", makefileRule.Pattern, "第四个规则的模式应该是 Makefile");
            Assert.AreEqual(1, makefileRule.Properties.Count, "第四个规则应该有1个属性");
            Assert.AreEqual("tab", makefileRule.GetProperty("indent_style"), "indent_style 属性值不正确");
        }
        
        /// <summary>
        /// 测试生成EditorConfig内容
        /// </summary>
        [Test]
        public void GenerateContent_ValidRules_ReturnsCorrectContent()
        {
            // 创建测试规则
            List<EditorConfigRule> rules = new List<EditorConfigRule>();
            
            // 添加 [*] 规则
            EditorConfigRule allFilesRule = new EditorConfigRule("*");
            allFilesRule.SetProperty("end_of_line", "lf");
            allFilesRule.SetProperty("insert_final_newline", "true");
            allFilesRule.SetProperty("charset", "utf-8");
            allFilesRule.SetProperty("trim_trailing_whitespace", "true");
            rules.Add(allFilesRule);
            
            // 添加 [*.cs] 规则
            EditorConfigRule csharpRule = new EditorConfigRule("*.cs");
            csharpRule.SetProperty("indent_style", "space");
            csharpRule.SetProperty("indent_size", "4");
            csharpRule.SetProperty("tab_width", "4");
            rules.Add(csharpRule);
            
            // 生成内容
            string content = EditorConfigParser.GenerateContent(rules);
            
            // 验证内容
            Assert.IsTrue(content.Contains("root = true"), "生成的内容应该包含 root = true");
            Assert.IsTrue(content.Contains("[*]"), "生成的内容应该包含 [*] 节");
            Assert.IsTrue(content.Contains("end_of_line = lf"), "生成的内容应该包含 end_of_line = lf");
            Assert.IsTrue(content.Contains("[*.cs]"), "生成的内容应该包含 [*.cs] 节");
            Assert.IsTrue(content.Contains("indent_style = space"), "生成的内容应该包含 indent_style = space");
            Assert.IsTrue(content.Contains("indent_size = 4"), "生成的内容应该包含 indent_size = 4");
        }
        
        /// <summary>
        /// 测试EditorConfig规则属性操作
        /// </summary>
        [Test]
        public void EditorConfigRule_PropertyOperations_WorksCorrectly()
        {
            // 创建测试规则
            EditorConfigRule rule = new EditorConfigRule("*.cs");
            
            // 测试添加属性
            rule.SetProperty("indent_style", "space");
            Assert.AreEqual("space", rule.GetProperty("indent_style"), "添加属性后应该能够获取到正确的值");
            
            // 测试更新属性
            rule.SetProperty("indent_style", "tab");
            Assert.AreEqual("tab", rule.GetProperty("indent_style"), "更新属性后应该能够获取到新的值");
            
            // 测试属性名不区分大小写
            Assert.AreEqual("tab", rule.GetProperty("INDENT_STYLE"), "属性名不应区分大小写");
            
            // 测试移除属性
            rule.RemoveProperty("indent_style");
            Assert.AreEqual("", rule.GetProperty("indent_style"), "移除属性后应该获取不到值");
            
            // 测试清空所有属性
            rule.SetProperty("indent_size", "4");
            rule.SetProperty("tab_width", "4");
            Assert.AreEqual(2, rule.Properties.Count, "应该有2个属性");
            
            rule.ClearProperties();
            Assert.AreEqual(0, rule.Properties.Count, "清空后应该没有属性");
        }
        
        /// <summary>
        /// 测试EditorConfig模板
        /// </summary>
        [Test]
        public void EditorConfigTemplate_GetDefaultRules_ReturnsValidRules()
        {
            // 获取默认规则
            List<EditorConfigRule> rules = EditorConfigTemplate.GetDefaultRules();
            
            // 验证规则数量
            Assert.IsTrue(rules.Count > 0, "默认规则列表不应为空");
            
            // 验证是否包含通用规则
            bool hasAllFilesRule = false;
            foreach (EditorConfigRule rule in rules)
            {
                if (rule.Pattern == "*")
                {
                    hasAllFilesRule = true;
                    break;
                }
            }
            
            Assert.IsTrue(hasAllFilesRule, "默认规则应包含通用规则 [*]");
            
            // 验证是否包含C#规则
            bool hasCSharpRule = false;
            foreach (EditorConfigRule rule in rules)
            {
                if (rule.Pattern == "*.cs")
                {
                    hasCSharpRule = true;
                    break;
                }
            }
            
            Assert.IsTrue(hasCSharpRule, "默认规则应包含C#规则 [*.cs]");
        }
    }
} 