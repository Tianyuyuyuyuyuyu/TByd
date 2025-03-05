using System;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// 配置提供者测试类
    /// </summary>
    public class ConfigProviderTests
    {
        // 测试配置路径
        private const string c_TestConfigPath = "Tests/TestConfig.json";

        // 原始配置路径
        private string m_OriginalConfigPath;

        // 完整的测试配置路径
        private string m_FullTestConfigPath;

        [SetUp]
        public void Setup()
        {
            Debug.Log($"[TByd.CodeStyle.Tests] 开始设置测试环境");

            // 构建完整的测试配置路径
            m_FullTestConfigPath = Path.Combine(Application.dataPath, c_TestConfigPath);
            Debug.Log($"[TByd.CodeStyle.Tests] 完整的测试配置路径: {m_FullTestConfigPath}");

            // 保存原始配置路径
            m_OriginalConfigPath = ConfigManager.ConfigPath;
            Debug.Log($"[TByd.CodeStyle.Tests] 原始配置路径: {m_OriginalConfigPath}");

            // 设置测试配置路径
            ConfigManager.ConfigPath = m_FullTestConfigPath;
            Debug.Log($"[TByd.CodeStyle.Tests] 测试配置路径: {m_FullTestConfigPath}");

            // 确保测试配置不存在
            if (File.Exists(m_FullTestConfigPath))
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 删除已存在的测试配置文件: {m_FullTestConfigPath}");
                File.Delete(m_FullTestConfigPath);
            }

            // 确保测试配置目录存在
            var directoryPath = Path.GetDirectoryName(m_FullTestConfigPath);
            Debug.Log($"[TByd.CodeStyle.Tests] 测试配置目录: {directoryPath}");

            if (!Directory.Exists(directoryPath))
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 创建测试配置目录: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 测试配置目录已存在: {directoryPath}");
            }

            // 确保有写入权限
            try
            {
                // 创建一个临时文件来测试写入权限
                var testFile = Path.Combine(directoryPath, "test_write.tmp");
                File.WriteAllText(testFile, "test");
                Debug.Log($"[TByd.CodeStyle.Tests] 成功写入测试文件: {testFile}");
                File.Delete(testFile);
                Debug.Log($"[TByd.CodeStyle.Tests] 成功删除测试文件: {testFile}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle.Tests] 无法写入测试目录: {e.Message}");
                Assert.Fail($"无法写入测试目录: {e.Message}");
            }

            // 重新初始化配置管理器
            Debug.Log($"[TByd.CodeStyle.Tests] 初始化配置管理器");
            ConfigManager.Initialize();
            Debug.Log($"[TByd.CodeStyle.Tests] 测试环境设置完成");
        }

        [TearDown]
        public void TearDown()
        {
            Debug.Log($"[TByd.CodeStyle.Tests] 开始清理测试环境");

            // 恢复原始配置路径
            Debug.Log($"[TByd.CodeStyle.Tests] 恢复原始配置路径: {m_OriginalConfigPath}");
            ConfigManager.ConfigPath = m_OriginalConfigPath;

            // 删除测试配置
            if (File.Exists(m_FullTestConfigPath))
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 删除测试配置文件: {m_FullTestConfigPath}");
                try
                {
                    File.Delete(m_FullTestConfigPath);
                    Debug.Log($"[TByd.CodeStyle.Tests] 测试配置文件删除成功");
                }
                catch (Exception e)
                {
                    Debug.LogError($"[TByd.CodeStyle.Tests] 删除测试配置文件失败: {e.Message}");
                }
            }
            else
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 测试配置文件不存在，无需删除");
            }

            // 重新初始化配置管理器
            Debug.Log($"[TByd.CodeStyle.Tests] 重新初始化配置管理器");
            ConfigManager.Initialize();
            Debug.Log($"[TByd.CodeStyle.Tests] 测试环境清理完成");
        }

        [Test]
        public void GetConfig_ReturnsValidConfig()
        {
            // 获取配置
            var config = ConfigProvider.GetConfig();

            // 验证配置不为空
            Assert.IsNotNull(config);

            // 验证配置包含默认值
            Assert.IsNotNull(config.GitSettings);
            Assert.IsNotNull(config.CommitMessageSettings);
        }

        [Test]
        public void SaveConfig_SavesConfigToFile()
        {
            // 获取配置
            var config = ConfigProvider.GetConfig();

            // 修改配置
            config.GitSettings.EnableGitHooks = true;
            config.CommitMessageSettings.RequireScope = true;

            // 确保测试目录存在
            var directoryPath = Path.GetDirectoryName(m_FullTestConfigPath);
            if (!Directory.Exists(directoryPath))
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 创建测试目录: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 测试目录已存在: {directoryPath}");
            }

            // 确保有写入权限
            try
            {
                // 创建一个临时文件来测试写入权限
                var testFile = Path.Combine(directoryPath, "test_write.tmp");
                File.WriteAllText(testFile, "test");
                Debug.Log($"[TByd.CodeStyle.Tests] 成功写入测试文件: {testFile}");
                File.Delete(testFile);
                Debug.Log($"[TByd.CodeStyle.Tests] 成功删除测试文件: {testFile}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle.Tests] 无法写入测试目录: {e.Message}");
                Assert.Fail($"无法写入测试目录: {e.Message}");
            }

            // 保存配置
            Debug.Log($"[TByd.CodeStyle.Tests] 开始保存配置到: {m_FullTestConfigPath}");
            ConfigProvider.SaveConfig();

            // 验证配置文件存在
            var fileExists = File.Exists(m_FullTestConfigPath);
            Debug.Log($"[TByd.CodeStyle.Tests] 配置文件存在: {fileExists}, 路径: {m_FullTestConfigPath}");
            Assert.IsTrue(fileExists, $"配置文件不存在: {m_FullTestConfigPath}");

            // 读取配置文件内容，确保内容正确
            var configContent = File.ReadAllText(m_FullTestConfigPath);
            Debug.Log($"[TByd.CodeStyle.Tests] 配置文件内容长度: {configContent.Length}");
            Assert.IsTrue(!string.IsNullOrEmpty(configContent), "配置文件内容为空");

            // 验证配置内容包含修改的值
            var containsEnableGitHooks = configContent.Contains("\"m_AutoInstallHooks\": true");
            var containsRequireScope = configContent.Contains("\"RequireScope\": true") || configContent.Contains("\"m_RequireScope\": true");
            Debug.Log($"[TByd.CodeStyle.Tests] 配置包含m_AutoInstallHooks: {containsEnableGitHooks}, 包含RequireScope: {containsRequireScope}");
            Debug.Log($"[TByd.CodeStyle.Tests] 配置内容: {configContent}");
            Assert.IsTrue(containsEnableGitHooks, "配置文件不包含EnableGitHooks设置");
            Assert.IsTrue(containsRequireScope, "配置文件不包含RequireScope设置");

            // 重新初始化配置管理器
            Debug.Log($"[TByd.CodeStyle.Tests] 重新初始化配置管理器");
            ConfigManager.Initialize();

            // 获取配置
            var loadedConfig = ConfigProvider.GetConfig();

            // 验证配置值已保存
            Debug.Log($"[TByd.CodeStyle.Tests] 加载的配置: EnableGitHooks={loadedConfig.GitSettings.EnableGitHooks}, RequireScope={loadedConfig.CommitMessageSettings.RequireScope}");
            Assert.IsTrue(loadedConfig.GitSettings.EnableGitHooks, "EnableGitHooks配置未正确保存");
            Assert.IsTrue(loadedConfig.CommitMessageSettings.RequireScope, "RequireScope配置未正确保存");
        }

        [Test]
        public void ConfigChanged_EventTriggered()
        {
            // 标记是否触发事件
            var eventTriggered = false;

            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += () => eventTriggered = true;

            // 获取配置
            var config = ConfigProvider.GetConfig();

            // 修改配置
            config.GitSettings.EnableGitHooks = !config.GitSettings.EnableGitHooks;

            // 保存配置
            ConfigProvider.SaveConfig();

            // 验证事件已触发
            Assert.IsTrue(eventTriggered);
        }
    }
}
