using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using UnityEngine;

namespace TByd.CodeStyle.Tests.Editor.EditorConfig
{
    /// <summary>
    /// EditorConfig文件匹配测试
    /// </summary>
    public class EditorConfigMatchTests
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

# Indentation override for all JS under lib directory
[lib/**.js]
indent_style = space
indent_size = 2

# Matches the exact files either package.json or .travis.yml
[{package.json,.travis.yml}]
indent_style = space
indent_size = 2
";
        
        /// <summary>
        /// 测试文件匹配
        /// </summary>
        [Test]
        public void GetFileProperties_MatchesCorrectRules()
        {
            // 解析测试内容
            List<EditorConfigRule> rules = EditorConfigParser.ParseContent(c_TestEditorConfigContent);
            
            // 创建临时测试文件
            string tempDir = Path.Combine(Path.GetTempPath(), "EditorConfigTest");
            Directory.CreateDirectory(tempDir);
            
            try
            {
                // 创建测试文件
                string csharpFile = Path.Combine(tempDir, "Test.cs");
                File.WriteAllText(csharpFile, "// Test C# file");
                
                string jsonFile = Path.Combine(tempDir, "Test.json");
                File.WriteAllText(jsonFile, "{ \"test\": true }");
                
                string makeFile = Path.Combine(tempDir, "Makefile");
                File.WriteAllText(makeFile, "# Test Makefile");
                
                string libDir = Path.Combine(tempDir, "lib");
                Directory.CreateDirectory(libDir);
                
                string jsFile = Path.Combine(libDir, "Test.js");
                File.WriteAllText(jsFile, "// Test JS file");
                
                string packageJsonFile = Path.Combine(tempDir, "package.json");
                File.WriteAllText(packageJsonFile, "{ \"name\": \"test\" }");
                
                // 模拟EditorConfigManager.GetRules()
                EditorConfigManager.GetRules();
                
                try
                {
                    // 替换GetRules方法，返回我们的测试规则
                    typeof(EditorConfigManager)
                        .GetField("s_Rules", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        ?.SetValue(null, rules);
                    
                    // 测试C#文件
                    Dictionary<string, string> csharpProps = EditorConfigManager.GetFileProperties(csharpFile);
                    Assert.AreEqual(7, csharpProps.Count, "C#文件应该匹配7个属性");
                    Assert.AreEqual("space", csharpProps["indent_style"], "C#文件的indent_style不正确");
                    Assert.AreEqual("4", csharpProps["indent_size"], "C#文件的indent_size不正确");
                    
                    // 测试JSON文件
                    Dictionary<string, string> jsonProps = EditorConfigManager.GetFileProperties(jsonFile);
                    Assert.AreEqual(6, jsonProps.Count, "JSON文件应该匹配6个属性");
                    Assert.AreEqual("space", jsonProps["indent_style"], "JSON文件的indent_style不正确");
                    Assert.AreEqual("2", jsonProps["indent_size"], "JSON文件的indent_size不正确");
                    
                    // 测试Makefile
                    Dictionary<string, string> makefileProps = EditorConfigManager.GetFileProperties(makeFile);
                    Assert.AreEqual(5, makefileProps.Count, "Makefile应该匹配5个属性");
                    Assert.AreEqual("tab", makefileProps["indent_style"], "Makefile的indent_style不正确");
                    
                    // 测试lib目录下的JS文件
                    Dictionary<string, string> jsProps = EditorConfigManager.GetFileProperties(jsFile);
                    Assert.AreEqual(6, jsProps.Count, "lib目录下的JS文件应该匹配6个属性");
                    Assert.AreEqual("space", jsProps["indent_style"], "lib目录下的JS文件的indent_style不正确");
                    Assert.AreEqual("2", jsProps["indent_size"], "lib目录下的JS文件的indent_size不正确");
                    
                    // 测试package.json文件
                    Dictionary<string, string> packageJsonProps = EditorConfigManager.GetFileProperties(packageJsonFile);
                    Assert.AreEqual(6, packageJsonProps.Count, "package.json文件应该匹配6个属性");
                    Assert.AreEqual("space", packageJsonProps["indent_style"], "package.json文件的indent_style不正确");
                    Assert.AreEqual("2", packageJsonProps["indent_size"], "package.json文件的indent_size不正确");
                }
                finally
                {
                    // 恢复原始的GetRules方法
                    EditorConfigManager.LoadProjectEditorConfig();
                }
            }
            finally
            {
                // 清理临时文件
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch
                {
                    // 忽略删除失败的异常
                }
            }
        }
        
        /// <summary>
        /// 测试文件验证
        /// </summary>
        [Test]
        public void ValidateFile_ChecksCorrectRules()
        {
            // 解析测试内容
            List<EditorConfigRule> rules = EditorConfigParser.ParseContent(c_TestEditorConfigContent);
            
            // 创建临时测试文件
            string tempDir = Path.Combine(Path.GetTempPath(), "EditorConfigTest");
            Directory.CreateDirectory(tempDir);
            
            try
            {
                // 创建符合规则的C#文件
                string validCSharpFile = Path.Combine(tempDir, "Valid.cs");
                File.WriteAllText(validCSharpFile, "// Valid C# file\n// Using spaces for indentation\n    public void Test()\n    {\n        // Code\n    }\n");
                
                // 创建不符合规则的C#文件（使用制表符而非空格）
                string invalidCSharpFile = Path.Combine(tempDir, "Invalid.cs");
                File.WriteAllText(invalidCSharpFile, "// Invalid C# file\n// Using tabs for indentation\n\tpublic void Test()\n\t{\n\t\t// Code\n\t}\n");
                
                // 模拟EditorConfigManager.GetRules()
                try
                {
                    // 替换GetRules方法，返回我们的测试规则
                    typeof(EditorConfigManager)
                        .GetField("s_Rules", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        ?.SetValue(null, rules);
                    
                    // 测试符合规则的文件
                    bool isValidFileValid = EditorConfigManager.ValidateFile(validCSharpFile);
                    Assert.IsTrue(isValidFileValid, "符合规则的文件应该通过验证");
                    
                    // 测试不符合规则的文件
                    bool isInvalidFileValid = EditorConfigManager.ValidateFile(invalidCSharpFile);
                    Assert.IsFalse(isInvalidFileValid, "不符合规则的文件不应该通过验证");
                }
                finally
                {
                    // 恢复原始的GetRules方法
                    EditorConfigManager.LoadProjectEditorConfig();
                }
            }
            finally
            {
                // 清理临时文件
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch
                {
                    // 忽略删除失败的异常
                }
            }
        }
    }
} 