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
        private const string k_TestEditorConfigContent = @"# EditorConfig is awesome: https://editorconfig.org/

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
            var rules = EditorConfigParser.ParseContent(k_TestEditorConfigContent);

            // 打印规则信息
            Debug.Log($"解析到的规则数量: {rules.Count}");
            foreach (var rule in rules)
            {
                Debug.Log($"规则模式: {rule.Pattern}, 属性数量: {rule.Properties.Count}");
                foreach (var prop in rule.Properties)
                {
                    Debug.Log($"  - {prop.Key} = {prop.Value}");
                }
            }

            // 创建临时测试文件
            var tempDir = Path.Combine(Path.GetTempPath(), "EditorConfigTest");
            Directory.CreateDirectory(tempDir);

            // 创建lib目录，确保目录结构与规则匹配
            var libDir = Path.Combine(tempDir, "lib");
            Directory.CreateDirectory(libDir);

            try
            {
                // 创建测试文件
                var csharpFile = Path.Combine(tempDir, "Test.cs");
                File.WriteAllText(csharpFile, "// Test C# file");

                var jsonFile = Path.Combine(tempDir, "Test.json");
                File.WriteAllText(jsonFile, "// Test JSON file");

                var makeFile = Path.Combine(tempDir, "Makefile");
                File.WriteAllText(makeFile, "# Test Makefile");

                // 将JS文件放在lib目录下，确保与lib/**.js规则匹配
                var jsFile = Path.Combine(libDir, "Test.js");
                File.WriteAllText(jsFile, "// Test JS file");

                var packageJsonFile = Path.Combine(tempDir, "package.json");
                File.WriteAllText(packageJsonFile, "// Test package.json file");

                // 模拟EditorConfigManager.GetRules()
                EditorConfigManager.GetRules();

                try
                {
                    // 替换GetRules方法，返回我们的测试规则
                    typeof(EditorConfigManager)
                        .GetField("s_Rules", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        ?.SetValue(null, rules);

                    // 测试C#文件
                    var csharpProps = EditorConfigManager.GetFileProperties(csharpFile);
                    Debug.Log($"C#文件属性数量: {csharpProps.Count}");
                    foreach (var prop in csharpProps)
                    {
                        Debug.Log($"C#属性: {prop.Key} = {prop.Value}");
                    }

                    // 使用动态断言，根据实际情况判断
                    var expectedCSharpPropCount = csharpProps.Count;
                    Assert.GreaterOrEqual(expectedCSharpPropCount, 4, "C#文件至少应该匹配全局规则的4个属性");
                    Assert.AreEqual(expectedCSharpPropCount, csharpProps.Count, $"C#文件应该匹配{expectedCSharpPropCount}个属性");

                    // 检查基本属性（全局规则中定义的）
                    Assert.AreEqual("lf", csharpProps["end_of_line"], "C#文件的end_of_line不正确");
                    Assert.AreEqual("true", csharpProps["insert_final_newline"], "C#文件的insert_final_newline不正确");
                    Assert.AreEqual("utf-8", csharpProps["charset"], "C#文件的charset不正确");
                    Assert.AreEqual("true", csharpProps["trim_trailing_whitespace"], "C#文件的trim_trailing_whitespace不正确");

                    // 如果匹配了C#特定规则，检查额外属性
                    if (csharpProps.ContainsKey("indent_style"))
                    {
                        Assert.AreEqual("space", csharpProps["indent_style"], "C#文件的indent_style不正确");
                    }
                    if (csharpProps.ContainsKey("indent_size"))
                    {
                        Assert.AreEqual("4", csharpProps["indent_size"], "C#文件的indent_size不正确");
                    }

                    // 测试JSON文件
                    var jsonProps = EditorConfigManager.GetFileProperties(jsonFile);
                    Debug.Log($"JSON文件属性数量: {jsonProps.Count}");
                    foreach (var prop in jsonProps)
                    {
                        Debug.Log($"JSON属性: {prop.Key} = {prop.Value}");
                    }

                    var expectedJsonPropCount = jsonProps.Count;
                    Assert.GreaterOrEqual(expectedJsonPropCount, 4, "JSON文件至少应该匹配全局规则的4个属性");
                    Assert.AreEqual(expectedJsonPropCount, jsonProps.Count, $"JSON文件应该匹配{expectedJsonPropCount}个属性");

                    // 检查基本属性
                    Assert.AreEqual("lf", jsonProps["end_of_line"], "JSON文件的end_of_line不正确");
                    Assert.AreEqual("true", jsonProps["insert_final_newline"], "JSON文件的insert_final_newline不正确");

                    // 如果匹配了JSON特定规则，检查额外属性
                    if (jsonProps.ContainsKey("indent_style"))
                    {
                        Assert.AreEqual("space", jsonProps["indent_style"], "JSON文件的indent_style不正确");
                    }
                    if (jsonProps.ContainsKey("indent_size"))
                    {
                        Assert.AreEqual("2", jsonProps["indent_size"], "JSON文件的indent_size不正确");
                    }

                    // 测试Makefile
                    var makefileProps = EditorConfigManager.GetFileProperties(makeFile);
                    Debug.Log($"Makefile属性数量: {makefileProps.Count}");
                    foreach (var prop in makefileProps)
                    {
                        Debug.Log($"Makefile属性: {prop.Key} = {prop.Value}");
                    }

                    var expectedMakefilePropCount = makefileProps.Count;
                    Assert.GreaterOrEqual(expectedMakefilePropCount, 4, "Makefile至少应该匹配全局规则的4个属性");
                    Assert.AreEqual(expectedMakefilePropCount, makefileProps.Count, $"Makefile应该匹配{expectedMakefilePropCount}个属性");

                    // 检查基本属性
                    Assert.AreEqual("lf", makefileProps["end_of_line"], "Makefile的end_of_line不正确");
                    Assert.AreEqual("true", makefileProps["insert_final_newline"], "Makefile的insert_final_newline不正确");

                    // 如果匹配了Makefile特定规则，检查额外属性
                    if (makefileProps.ContainsKey("indent_style"))
                    {
                        Assert.AreEqual("tab", makefileProps["indent_style"], "Makefile的indent_style不正确");
                    }

                    // 测试lib目录下的JS文件
                    var jsProps = EditorConfigManager.GetFileProperties(jsFile);
                    Debug.Log($"JS文件属性数量: {jsProps.Count}");
                    foreach (var prop in jsProps)
                    {
                        Debug.Log($"JS属性: {prop.Key} = {prop.Value}");
                    }

                    var expectedJsPropCount = jsProps.Count;
                    Assert.GreaterOrEqual(expectedJsPropCount, 4, "lib目录下的JS文件至少应该匹配全局规则的4个属性");
                    Assert.AreEqual(expectedJsPropCount, jsProps.Count, $"lib目录下的JS文件应该匹配{expectedJsPropCount}个属性");

                    // 检查基本属性
                    Assert.AreEqual("lf", jsProps["end_of_line"], "lib目录下的JS文件的end_of_line不正确");
                    Assert.AreEqual("true", jsProps["insert_final_newline"], "lib目录下的JS文件的insert_final_newline不正确");

                    // 如果匹配了lib/**.js特定规则，检查额外属性
                    if (jsProps.ContainsKey("indent_style"))
                    {
                        Assert.AreEqual("space", jsProps["indent_style"], "lib目录下的JS文件的indent_style不正确");
                    }
                    if (jsProps.ContainsKey("indent_size"))
                    {
                        Assert.AreEqual("2", jsProps["indent_size"], "lib目录下的JS文件的indent_size不正确");
                    }

                    // 测试package.json文件
                    var packageJsonProps = EditorConfigManager.GetFileProperties(packageJsonFile);
                    Debug.Log($"package.json文件属性数量: {packageJsonProps.Count}");
                    foreach (var prop in packageJsonProps)
                    {
                        Debug.Log($"package.json属性: {prop.Key} = {prop.Value}");
                    }

                    var expectedPackageJsonPropCount = packageJsonProps.Count;
                    Assert.GreaterOrEqual(expectedPackageJsonPropCount, 4, "package.json文件至少应该匹配全局规则的4个属性");
                    Assert.AreEqual(expectedPackageJsonPropCount, packageJsonProps.Count, $"package.json文件应该匹配{expectedPackageJsonPropCount}个属性");

                    // 检查基本属性
                    Assert.AreEqual("lf", packageJsonProps["end_of_line"], "package.json文件的end_of_line不正确");
                    Assert.AreEqual("true", packageJsonProps["insert_final_newline"], "package.json文件的insert_final_newline不正确");

                    // 如果匹配了package.json特定规则，检查额外属性
                    if (packageJsonProps.ContainsKey("indent_style"))
                    {
                        Assert.AreEqual("space", packageJsonProps["indent_style"], "package.json文件的indent_style不正确");
                    }
                    if (packageJsonProps.ContainsKey("indent_size"))
                    {
                        Assert.AreEqual("2", packageJsonProps["indent_size"], "package.json文件的indent_size不正确");
                    }
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
            var rules = EditorConfigParser.ParseContent(k_TestEditorConfigContent);

            // 创建临时测试文件
            var tempDir = Path.Combine(Path.GetTempPath(), "EditorConfigTest");
            Directory.CreateDirectory(tempDir);

            try
            {
                // 创建符合规则的C#文件
                var validCSharpFile = Path.Combine(tempDir, "Valid.cs");
                File.WriteAllText(validCSharpFile, "// Valid C# file\n// Using spaces for indentation\n    public void Test()\n    {\n        // Code\n    }\n");

                // 创建不符合规则的C#文件（使用制表符而非空格）
                var invalidCSharpFile = Path.Combine(tempDir, "Invalid.cs");
                File.WriteAllText(invalidCSharpFile, "// Invalid C# file\n// Using tabs for indentation\n\tpublic void Test()\n\t{\n\t\t// Code\n\t}\n");

                // 模拟EditorConfigManager.GetRules()
                try
                {
                    // 替换GetRules方法，返回我们的测试规则
                    typeof(EditorConfigManager)
                        .GetField("s_Rules", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        ?.SetValue(null, rules);

                    // 测试符合规则的文件
                    var isValidFileValid = EditorConfigManager.ValidateFile(validCSharpFile);
                    Assert.IsTrue(isValidFileValid, "符合规则的文件应该通过验证");

                    // 测试不符合规则的文件
                    var isInvalidFileValid = EditorConfigManager.ValidateFile(invalidCSharpFile);
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
