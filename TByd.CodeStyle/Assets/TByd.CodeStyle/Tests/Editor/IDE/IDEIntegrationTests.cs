using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Editor.CodeCheck.IDE;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Tests.Editor.IDE
{
    /// <summary>
    /// IDE集成测试
    /// </summary>
    public class IdeIntegrationTests
    {
        private string m_TestDirectory;
        private string m_ConfigDirectory;
        private string m_SyncDirectory;
        private string m_OriginalSyncConfigPath;
        private List<IDeIntegration> m_RegisteredIntegrations = new List<IDeIntegration>();
        private List<EditorConfigRule> m_TestRules;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录
            m_TestDirectory = Path.Combine(Application.temporaryCachePath, "IDEIntegrationTests_" + System.Guid.NewGuid().ToString("N"));
            m_ConfigDirectory = Path.Combine(m_TestDirectory, "Config");
            m_SyncDirectory = Path.Combine(m_TestDirectory, "Sync");

            Directory.CreateDirectory(m_ConfigDirectory);
            Directory.CreateDirectory(m_SyncDirectory);

            // 创建测试配置文件
            CreateTestConfigFiles();

            // 设置用于测试的同步配置路径
            m_OriginalSyncConfigPath = GetPrivateStaticFieldValue<string>("s_SyncConfigPathForTesting");
            SetSyncConfigPath(m_SyncDirectory);

            // 初始化测试规则
            m_TestRules = new List<EditorConfigRule>();
            var rule = new EditorConfigRule("*.cs");
            rule.SetProperty("indent_style", "space");
            rule.SetProperty("indent_size", "4");
            m_TestRules.Add(rule);
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

            // 恢复原始同步配置路径
            SetSyncConfigPath(m_OriginalSyncConfigPath);

            // 清理测试目录
            if (Directory.Exists(m_TestDirectory))
            {
                try
                {
                    Directory.Delete(m_TestDirectory, true);
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning($"清理测试目录失败：{ex.Message}");
                }
            }
        }

        private void CreateTestConfigFiles()
        {
            // 创建.editorconfig
            var editorConfigContent = @"root = true
            [*]
            charset = utf-8
            end_of_line = lf
            indent_style = space
            indent_size = 4";
            File.WriteAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"), editorConfigContent);

            // 创建VS Code配置
            var vscodeDir = Path.Combine(m_ConfigDirectory, ".vscode");
            Directory.CreateDirectory(vscodeDir);
            var settingsContent = @"{
                ""editor.formatOnSave"": true,
                ""editor.formatOnType"": true
            }";
            File.WriteAllText(Path.Combine(vscodeDir, "settings.json"), settingsContent);
        }

        // 使用反射设置同步配置路径
        private void SetSyncConfigPath(string path)
        {
            var fieldInfo = typeof(IdeConfigSyncManager).GetField("s_SyncConfigPathForTesting",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(null, path);
            }
        }

        // 使用反射获取私有静态字段的值
        private T GetPrivateStaticFieldValue<T>(string fieldName)
        {
            var fieldInfo = typeof(IdeConfigSyncManager).GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(null);
            }

            return default(T);
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
        /// 测试注册和获取IDE集成
        /// </summary>
        [Test]
        public void RegisterAndGetIntegrations_ValidIntegrations_SuccessfullyRegistered()
        {
            // 注册测试IDE集成
            var riderIntegration = new MockIdeIntegration(IdeType.k_Rider, true);
            var vsIntegration = new MockIdeIntegration(IdeType.k_VisualStudio, false);
            var vscodeIntegration = new MockIdeIntegration(IdeType.k_VSCode, true);

            RegisterIntegration(riderIntegration);
            RegisterIntegration(vsIntegration);
            RegisterIntegration(vscodeIntegration);

            // 获取所有注册的集成
            var integrations = GetAllIntegrations();

            // 验证注册的集成数量
            Assert.AreEqual(3, integrations.Count(i => i is MockIdeIntegration), "应该有3个注册的IDE集成");
        }

        // 使用反射获取所有注册的集成
        private List<IDeIntegration> GetAllIntegrations()
        {
            var field = typeof(IdeIntegrationManager).GetField("s_Integrations",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (field != null && field.GetValue(null) is List<IDeIntegration> integrations)
            {
                return integrations;
            }

            return new List<IDeIntegration>();
        }

        /// <summary>
        /// 测试IDE安装检测
        /// </summary>
        [Test]
        public void IsIDEInstalled_CheckMultipleIDEs_ReturnsExpectedResults()
        {
            // 清理所有已注册的集成
            ClearIntegrations();

            // 注册测试IDE集成（使用模拟数据）
            var mockRiderIntegration = new MockIdeIntegration(IdeType.k_Rider, true); // 模拟已安装
            var mockVSIntegration = new MockIdeIntegration(IdeType.k_VisualStudio, false); // 模拟未安装
            var mockVSCodeIntegration = new MockIdeIntegration(IdeType.k_VSCode, true); // 模拟已安装

            RegisterIntegration(mockRiderIntegration);
            RegisterIntegration(mockVSIntegration);
            RegisterIntegration(mockVSCodeIntegration);

            // 检查IDE是否安装
            var isRiderInstalled = IdeIntegrationManager.IsIdeInstalled(IdeType.k_Rider);
            var isVSInstalled = IdeIntegrationManager.IsIdeInstalled(IdeType.k_VisualStudio);
            var isVSCodeInstalled = IdeIntegrationManager.IsIdeInstalled(IdeType.k_VSCode);

            // 验证结果
            Assert.IsTrue(isRiderInstalled, "Rider应该被检测为已安装");
            Assert.IsFalse(isVSInstalled, "Visual Studio应该被检测为未安装");
            Assert.IsTrue(isVSCodeInstalled, "VS Code应该被检测为已安装");
        }

        /// <summary>
        /// 测试获取已安装的IDE
        /// </summary>
        [Test]
        public void GetInstalledIDEs_WithMixedInstallations_ReturnsCorrectIDEs()
        {
            // 清除现有集成
            ClearIntegrations();

            // 创建模拟IDE集成
            var mockRiderIntegration = new MockIdeIntegration(IdeType.k_Rider, true);
            var mockVSIntegration = new MockIdeIntegration(IdeType.k_VisualStudio, false);
            var mockVSCodeIntegration = new MockIdeIntegration(IdeType.k_VSCode, true);

            // 注册测试IDE集成
            RegisterIntegration(mockRiderIntegration);
            RegisterIntegration(mockVSIntegration);
            RegisterIntegration(mockVSCodeIntegration);

            // 获取已安装的IDE列表
            var installedIDEs = IdeIntegrationManager.GetInstalledIntegrations();

            // 验证结果 - 使用名称匹配因为我们不能访问IDEType
            Assert.AreEqual(2, installedIDEs.Count, "应该有2个已安装的IDE");
            Assert.IsTrue(installedIDEs.Any(i => i.Name == "Rider"), "Rider应该在已安装列表中");
            Assert.IsFalse(installedIDEs.Any(i => i.Name == "VisualStudio"), "Visual Studio不应该在已安装列表中");
            Assert.IsTrue(installedIDEs.Any(i => i.Name == "VSCode"), "VS Code应该在已安装列表中");
        }

        /// <summary>
        /// 测试导出配置到IDE
        /// </summary>
        [Test]
        public void ExportConfigToIDE_ValidConfig_CallsExportMethod()
        {
            // 注册测试IDE集成（使用模拟数据）
            var mockRiderIntegration = new MockIdeIntegration(IdeType.k_Rider, true);
            RegisterIntegration(mockRiderIntegration);

            // 设置EditorConfig规则
            typeof(EditorConfigManager)
                .GetField("s_Rules", BindingFlags.NonPublic | BindingFlags.Static)
                ?.SetValue(null, m_TestRules);

            // 导出配置
            var result = IdeIntegrationManager.ExportConfigToAllIDEs(m_TestRules);

            // 验证结果
            Assert.IsTrue(result, "配置导出应该成功");
            Assert.IsTrue(mockRiderIntegration.ExportRulesCalled, "应该调用了ExportConfig(List<EditorConfigRule>)方法");
        }

        /// <summary>
        /// 测试导出配置到所有IDE
        /// </summary>
        [Test]
        public void ExportConfigToAllIDEs_ValidConfig_ExportsToInstalledIDEs()
        {
            // 注册测试IDE集成（使用模拟数据）
            var mockRiderIntegration = new MockIdeIntegration(IdeType.k_Rider, true); // 模拟已安装
            var mockVSIntegration = new MockIdeIntegration(IdeType.k_VisualStudio, false); // 模拟未安装
            var mockVSCodeIntegration = new MockIdeIntegration(IdeType.k_VSCode, true); // 模拟已安装

            RegisterIntegration(mockRiderIntegration);
            RegisterIntegration(mockVSIntegration);
            RegisterIntegration(mockVSCodeIntegration);

            // 设置EditorConfig规则
            typeof(EditorConfigManager)
                .GetField("s_Rules", BindingFlags.NonPublic | BindingFlags.Static)
                ?.SetValue(null, m_TestRules);

            // 导出配置到所有IDE
            IdeIntegrationManager.ExportConfigToAllIDEs(m_TestRules);

            // 验证结果
            Assert.IsTrue(mockRiderIntegration.ExportRulesCalled, "应该调用了Rider的ExportConfig方法");
            Assert.IsFalse(mockVSIntegration.ExportRulesCalled, "不应该调用未安装的Visual Studio的ExportConfig方法");
            Assert.IsTrue(mockVSCodeIntegration.ExportRulesCalled, "应该调用了VS Code的ExportConfig方法");
        }

        /// <summary>
        /// 模拟IDE集成类，用于测试
        /// </summary>
        private class MockIdeIntegration : IDeIntegration
        {
            public string Name { get; private set; }
            public bool IsInstalled { get; private set; }
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

            // 辅助方法，获取可执行路径
            public string GetExecutablePath()
            {
                return $"C:\\Mock\\{IdeType}.exe";
            }
        }

        // 清除所有注册的集成
        private void ClearIntegrations()
        {
            var field = typeof(IdeIntegrationManager).GetField("s_Integrations",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (field != null && field.GetValue(null) is List<IDeIntegration> integrations)
            {
                integrations.Clear();
            }
        }
    }
}
