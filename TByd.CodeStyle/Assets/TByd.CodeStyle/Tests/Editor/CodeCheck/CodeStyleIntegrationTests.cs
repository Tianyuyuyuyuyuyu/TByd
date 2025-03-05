using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Editor.CodeCheck.IDE;
using TByd.CodeStyle.Runtime.CodeCheck;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Tests.Editor.CodeCheck
{
    /// <summary>
    /// 代码风格集成测试
    /// </summary>
    public class CodeStyleIntegrationTests
    {
        private string m_TestDirectory;
        private string m_EditorConfigPath;
        private CodeStyleConfig m_Config;
        private CodeCheckConfig m_CodeCheckConfig;
        private CodeChecker m_CodeChecker;
        private List<IDeIntegration> m_RegisteredIntegrations = new List<IDeIntegration>();

        [SetUp]
        public void Setup()
        {
            // 创建测试目录
            m_TestDirectory = Path.Combine(Application.temporaryCachePath, "CodeStyleIntegrationTests");
            Directory.CreateDirectory(m_TestDirectory);

            // 创建.editorconfig文件
            m_EditorConfigPath = Path.Combine(m_TestDirectory, ".editorconfig");
            var editorConfigContent = @"root = true

[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
trim_trailing_whitespace = true

[*.cs]
indent_style = space
indent_size = 4
csharp_new_line_before_open_brace = all
csharp_prefer_braces = true
dotnet_naming_rule.private_fields_rule.symbols = private_fields
dotnet_naming_rule.private_fields_rule.style = prefix_m_
dotnet_naming_rule.private_fields_rule.severity = suggestion
dotnet_naming_symbols.private_fields.applicable_kinds = field
dotnet_naming_symbols.private_fields.applicable_accessibilities = private
dotnet_naming_style.prefix_m_.required_prefix = m_
dotnet_naming_style.prefix_m_.capitalization = pascal_case
";
            File.WriteAllText(m_EditorConfigPath, editorConfigContent);

            // 创建配置对象
            m_Config = ScriptableObject.CreateInstance<CodeStyleConfig>();
            m_Config.EnableCodeStyleCheck = true;
            m_Config.EnableIdeIntegration = true;
            m_Config.SyncEditorConfigWithIde = true;

            m_CodeCheckConfig = new CodeCheckConfig();

            // 初始化代码检查器
            m_CodeChecker = new CodeChecker(m_CodeCheckConfig);

            // 注册测试用的IDE集成
            RegisterIntegration(new MockIdeIntegration(IdeType.k_Rider, true));
        }

        [TearDown]
        public void TearDown()
        {
            // 清理注册的测试集成
            foreach (var integration in m_RegisteredIntegrations)
            {
                RemoveIntegration(integration);
            }
            m_RegisteredIntegrations.Clear();

            // 清理测试目录
            if (Directory.Exists(m_TestDirectory))
            {
                try
                {
                    Directory.Delete(m_TestDirectory, true);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"清理测试目录时出错：{e.Message}");
                }
            }

            // 销毁配置对象 - CodeStyleConfig是ScriptableObject，需要销毁
            Object.DestroyImmediate(m_Config);
            // CodeCheckConfig不是ScriptableObject，不需要销毁
        }

        // 注册IDE集成到Manager，并跟踪它们以便清理
        private void RegisterIntegration(IDeIntegration integration)
        {
            // 使用反射获取或添加集成
            var field = typeof(IdeIntegrationManager).GetField("s_Integrations",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (field != null && field.GetValue(null) is List<IDeIntegration> integrations)
            {
                integrations.Add(integration);
                m_RegisteredIntegrations.Add(integration);
            }
            else
            {
                IdeIntegrationManager.RegisterIntegration(integration);
                m_RegisteredIntegrations.Add(integration);
            }
        }

        // 从Manager中移除IDE集成
        private void RemoveIntegration(IDeIntegration integration)
        {
            var field = typeof(IdeIntegrationManager).GetField("s_Integrations",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (field != null && field.GetValue(null) is List<IDeIntegration> integrations)
            {
                integrations.Remove(integration);
            }
        }

        /// <summary>
        /// 测试完整的代码检查流程
        /// </summary>
        [Test]
        public void FullWorkflow_ValidAndInvalidCode_CorrectlyDetectsIssues()
        {
            // 1. 加载EditorConfig规则
            var rules = EditorConfigParser.ParseFile(m_EditorConfigPath);
            Assert.Greater(rules.Count, 0, "应该成功解析EditorConfig规则");

            // 注册规则到EditorConfig管理器
            typeof(EditorConfigManager)
                .GetField("s_Rules", BindingFlags.NonPublic | BindingFlags.Static)
                ?.SetValue(null, rules);

            // 2. 创建测试代码文件
            var validCodePath = Path.Combine(m_TestDirectory, "ValidCode.cs");
            var invalidCodePath = Path.Combine(m_TestDirectory, "InvalidCode.cs");

            // 有效的代码示例（符合命名规则）
            var validCode = @"
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    private int m_Count;
    private string m_Name;

    void Start()
    {
        m_Count = 10;
        m_Name = ""Player"";
        Debug.Log($""Starting with {m_Name}, count: {m_Count}"");
    }

    void Update()
    {
        // 使用缓存的变量而不是在Update中使用Find
        if (m_Name == ""Player"")
        {
            transform.Rotate(Vector3.up * Time.deltaTime);
        }
    }
}";

            // 无效的代码示例（不符合命名规则）
            var invalidCode = @"
using UnityEngine;

public class BadExampleClass : MonoBehaviour
{
    // 命名问题：没有使用m_前缀
    private int count;
    private string name;

    void Start()
    {
        count = 10;
        name = ""Player"";
        Debug.Log($""Starting with {name}, count: {count}"");
    }

    void Update()
    {
        // 性能问题：在Update中使用Find
        GameObject player = GameObject.Find(""Player"");
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
        }
    }
}";

            File.WriteAllText(validCodePath, validCode);
            File.WriteAllText(invalidCodePath, invalidCode);

            // 3. 执行代码检查
            var validResult = m_CodeChecker.CheckFile(validCodePath);
            var invalidResult = m_CodeChecker.CheckFile(invalidCodePath);

            // 验证检查结果
            Assert.IsTrue(validResult.IsValid || validResult.Issues.Count == 0,
                "符合规则的代码应该通过检查或没有严重问题");

            Assert.IsFalse(invalidResult.IsValid, "不符合规则的代码不应该通过检查");
            Assert.Greater(invalidResult.Issues.Count, 0, "不符合规则的代码应该有问题");

            // 验证检测到了不同类型的问题
            var foundNamingIssue = false;
            var foundFindInUpdateIssue = false;

            foreach (var issue in invalidResult.Issues)
            {
                if (issue.Message.Contains("前缀") || issue.Message.Contains("m_"))
                {
                    foundNamingIssue = true;
                }

                if (issue.Message.Contains("Update") && issue.Message.Contains("Find"))
                {
                    foundFindInUpdateIssue = true;
                }
            }

            Assert.IsTrue(foundNamingIssue, "应该检测到命名问题");
            Assert.IsTrue(foundFindInUpdateIssue, "应该检测到在Update中使用Find的问题");

            // 4. 将EditorConfig导出到IDE
            // 直接使用静态类方法
            var exportResult = IdeIntegrationManager.ExportConfigToAllIDEs(rules);

            // 验证导出结果
            Assert.IsTrue(exportResult, "配置导出应该成功");
        }

        /// <summary>
        /// 测试边缘情况：空文件
        /// </summary>
        [Test]
        public void EdgeCase_EmptyFile_HandledGracefully()
        {
            // 创建空文件
            var emptyFilePath = Path.Combine(m_TestDirectory, "EmptyFile.cs");
            File.WriteAllText(emptyFilePath, "");

            // 执行代码检查
            var result = m_CodeChecker.CheckFile(emptyFilePath);

            // 验证检查结果（不应抛出异常，应该优雅处理）
            Assert.IsTrue(result.IsValid, "空文件应该被优雅处理");
            Assert.AreEqual(0, result.Issues.Count, "空文件不应该报告问题");
        }

        /// <summary>
        /// 测试边缘情况：仅有注释的文件
        /// </summary>
        [Test]
        public void EdgeCase_CommentsOnlyFile_HandledGracefully()
        {
            // 创建仅有注释的文件
            var commentsFilePath = Path.Combine(m_TestDirectory, "CommentsOnly.cs");
            var commentsContent = @"
// 这是一个仅有注释的文件
// 没有实际的代码内容
/*
 * 多行注释
 * 同样没有实际的代码
 */
";
            File.WriteAllText(commentsFilePath, commentsContent);

            // 执行代码检查
            var result = m_CodeChecker.CheckFile(commentsFilePath);

            // 验证检查结果（不应抛出异常，应该优雅处理）
            Assert.IsTrue(result.IsValid, "仅有注释的文件应该被优雅处理");
        }

        /// <summary>
        /// 测试边缘情况：非C#文件
        /// </summary>
        [Test]
        public void EdgeCase_NonCSharpFile_HandledGracefully()
        {
            // 创建JSON文件
            var jsonFilePath = Path.Combine(m_TestDirectory, "Config.json");
            var jsonContent = @"{
  ""name"": ""TestProject"",
  ""version"": ""1.0.0"",
  ""description"": ""A test project for code style checking""
}";
            File.WriteAllText(jsonFilePath, jsonContent);

            // 执行代码检查
            var result = m_CodeChecker.CheckFile(jsonFilePath);

            // 验证检查结果（不应抛出异常，应该根据文件类型适当处理）
            Assert.IsTrue(result.IsValid, "非C#文件应该被优雅处理");
        }

        /// <summary>
        /// 模拟IDE集成类，用于测试
        /// </summary>
        private class MockIdeIntegration : IDeIntegration
        {
            public bool IsInstalled { get; private set; }
            public string Name { get; private set; }
            public IdeType IdeType { get; private set; }
            public bool ExportConfigCalled { get; private set; }
            public bool ExportRulesCalled { get; private set; }

            public MockIdeIntegration(IdeType ideType, bool isInstalled)
            {
                IdeType = ideType;
                IsInstalled = isInstalled;
                Name = ideType.ToString();
                ExportConfigCalled = false;
                ExportRulesCalled = false;
            }

            public bool ExportConfig(List<EditorConfigRule> rules)
            {
                ExportRulesCalled = true;
                return true;
            }

            // 添加用于调用的辅助方法，以保持向后兼容性
            public bool ExportConfig(CodeStyleConfig config)
            {
                ExportConfigCalled = true;
                return true;
            }
        }
    }
}
