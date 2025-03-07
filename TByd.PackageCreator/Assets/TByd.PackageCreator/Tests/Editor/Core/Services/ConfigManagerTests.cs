using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using UnityEngine;
using UnityEngine.TestTools;

namespace TByd.PackageCreator.Tests.Editor.Core.Services
{
    /// <summary>
    /// 配置管理器测试类
    /// </summary>
    public class ConfigManagerTests
    {
        private ConfigManager _configManager;
        private string _testFilePath;
        private string _testImportPath;
        private string _testExportPath;

        [SetUp]
        public void Setup()
        {
            // 获取ConfigManager单例实例
            _configManager = ConfigManager.Instance;

            // 清除历史记录，确保测试环境干净
            _configManager.ClearHistory();

            // 创建测试文件路径
            _testFilePath = TestHelpers.GetTestFilePath("config.json");
            _testImportPath = TestHelpers.GetTestFilePath("import_config.json");
            _testExportPath = TestHelpers.GetTestFilePath("export_config.json");

            // 确保测试文件不存在
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);

            if (File.Exists(_testImportPath))
                File.Delete(_testImportPath);

            if (File.Exists(_testExportPath))
                File.Delete(_testExportPath);
        }

        [TearDown]
        public void Teardown()
        {
            // 清理测试文件
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);

            if (File.Exists(_testImportPath))
                File.Delete(_testImportPath);

            if (File.Exists(_testExportPath))
                File.Delete(_testExportPath);
        }

        [Test]
        public void CurrentConfig_ShouldNotBeNull()
        {
            // 断言：ConfigManager应该始终有一个当前配置，即使是默认配置
            Assert.IsNotNull(_configManager.CurrentConfig);
        }

        [Test]
        public void CreateNewConfig_ShouldReturnValidConfig()
        {
            // 安排：准备测试数据
            var name = "com.test.package";
            var displayName = "Test Package";

            // 执行：创建新配置
            var config = _configManager.CreateNewConfig(name, displayName);

            // 断言：验证创建的配置属性
            Assert.IsNotNull(config);
            Assert.AreEqual(name, config.Name);
            Assert.AreEqual(displayName, config.DisplayName);
            Assert.AreEqual("0.1.0", config.Version);
            Assert.IsTrue(config.Description.Contains(displayName));
            Assert.IsNotNull(config.Author);
        }

        [Test]
        public void History_ShouldBeEmptyInitially()
        {
            // 清除历史记录
            _configManager.ClearHistory();

            // 断言：初始状态下历史记录应该为空
            Assert.IsEmpty(_configManager.History);
        }

        [Test]
        public void AddToHistory_ShouldAddConfigToHistory()
        {
            // 安排：清除历史记录并准备测试数据
            _configManager.ClearHistory();
            var config = new PackageConfig("com.test.package", "Test Package");
            var description = "测试历史记录";

            // 执行：添加到历史记录
            _configManager.AddToHistory(config, description);

            // 断言：验证历史记录
            Assert.AreEqual(1, _configManager.History.Count);
            Assert.AreEqual(description, _configManager.History[0].Description);
            Assert.AreEqual(config.Name, _configManager.History[0].Config.Name);
        }

        [Test]
        public void History_ShouldLimitSize()
        {
            // 安排：清除历史记录
            _configManager.ClearHistory();

            // 执行：添加多个历史记录（超过限制）
            for (var i = 0; i < 25; i++)
            {
                var config = new PackageConfig($"com.test.package{i}", $"Test Package {i}");
                _configManager.AddToHistory(config, $"测试 {i}");
            }

            // 断言：历史记录数量应该有限制（ConfigManager中定义为20）
            Assert.LessOrEqual(_configManager.History.Count, 20);
            // 最新的记录应该在前面
            Assert.AreEqual("com.test.package24", _configManager.History[0].Config.Name);
        }

        [Test]
        public void RestoreFromHistory_ShouldRestoreConfig()
        {
            // 安排：清除历史记录并添加多个历史记录
            _configManager.ClearHistory();
            for (var i = 0; i < 3; i++)
            {
                var config = new PackageConfig($"com.test.package{i}", $"Test Package {i}");
                _configManager.AddToHistory(config, $"测试 {i}");
            }

            // 记住当前配置
            var currentConfigName = _configManager.CurrentConfig.Name;

            // 执行：从历史记录中恢复配置
            var restoredConfig = _configManager.RestoreFromHistory(2);

            // 断言：
            Assert.IsNotNull(restoredConfig);
            Assert.AreEqual("com.test.package0", restoredConfig.Name);
            Assert.AreEqual(restoredConfig.Name, _configManager.CurrentConfig.Name);
            Assert.AreNotEqual(currentConfigName, _configManager.CurrentConfig.Name);
        }

        [Test]
        public void ValidateConfig_WithValidConfig_ShouldReturnValid()
        {
            // 安排：创建有效的配置
            var config = new PackageConfig("com.test.package", "Test Package", "1.0.0", "This is a test package");
            config.Author = new PackageAuthor("Test Author");

            // 执行：验证配置
            var result = _configManager.ValidateConfig(config);

            // 断言：验证结果应该是有效的
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(result.HasErrors);
        }

        [Test]
        public void ValidateConfig_WithInvalidName_ShouldReturnInvalid()
        {
            // 安排：创建无效的配置（无效的包名）
            var config = new PackageConfig("invalid-name", "Test Package");

            // 执行：验证配置
            var result = _configManager.ValidateConfig(config);

            // 断言：验证结果应该是无效的，并且包含关于名称的错误
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.HasErrors);

            var errorMessages = result.GetMessages(ValidationMessageLevel.Error);
            var hasNameError = false;
            foreach (var error in errorMessages)
            {
                if (error.Field == "Name" && error.Message.Contains("包名称格式无效"))
                {
                    hasNameError = true;
                    break;
                }
            }
            Assert.IsTrue(hasNameError, "应该报告包名称格式无效错误");
        }

        [Test]
        public void ValidateConfig_WithMissingRequiredFields_ShouldReturnInvalid()
        {
            // 安排：创建无效的配置（缺少必填字段）
            var config = new PackageConfig("", "");

            // 执行：验证配置
            var result = _configManager.ValidateConfig(config);

            // 断言：验证结果应该是无效的，并且包含关于缺少字段的错误
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.HasErrors);

            var errorMessages = result.GetMessages(ValidationMessageLevel.Error);
            Assert.GreaterOrEqual(errorMessages.Count, 2, "应该至少有两个错误（名称和显示名称）");
        }

        [UnityTest]
        public IEnumerator SaveConfigAsync_ShouldSaveConfigToFile()
        {
            // 日志测试路径，帮助调试
            Debug.Log($"测试保存文件路径: {_testFilePath}");

            // 安排：创建配置
            var config = new PackageConfig("com.test.package", "Test Package", "1.0.0", "This is a test package");
            _configManager.CreateNewConfig("com.test.package", "Test Package");

            // 执行：保存配置
            var task = _configManager.SaveConfigAsync(_testFilePath);

            // 等待任务完成
            while (!task.IsCompleted)
                yield return null;

            try
            {
                var result = task.Result;

                // 断言：保存应该成功，文件应该存在
                Assert.IsTrue(result.IsValid, "保存结果应该是有效的");
                Assert.IsTrue(File.Exists(_testFilePath), $"文件应该存在: {_testFilePath}");

                // 验证文件内容
                var json = File.ReadAllText(_testFilePath);
                Assert.IsTrue(json.Contains("com.test.package"), "JSON应该包含包名");
                Assert.IsTrue(json.Contains("Test Package"), "JSON应该包含显示名称");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"验证结果时发生异常: {ex.Message}");
            }
        }

        [UnityTest]
        public IEnumerator LoadConfigAsync_ShouldLoadConfigFromFile()
        {
            // 安排：创建并保存配置
            var testName = "com.test.loadconfig";
            var testDisplayName = "Test Load Config";

            var config = new PackageConfig(testName, testDisplayName, "1.0.0", "This is a test package for loading");

            // 使用Newtonsoft.Json序列化，确保与反序列化方式匹配
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            Debug.Log($"序列化JSON: {json}"); // 添加日志帮助调试
            File.WriteAllText(_testFilePath, json);

            // 确认文件内容正确
            Debug.Log($"写入文件: {_testFilePath}");
            Debug.Log($"文件内容: {File.ReadAllText(_testFilePath)}");

            // 执行：加载配置
            var task = _configManager.LoadConfigAsync(_testFilePath);

            // 等待任务完成
            while (!task.IsCompleted)
                yield return null;

            var result = task.Result;

            // 如果加载失败，打印详细信息帮助调试
            if (!result.IsValid)
            {
                foreach (var msg in result.GetMessages(ValidationMessageLevel.Error))
                {
                    Debug.LogError($"加载错误: {msg.Message}");
                }
            }

            // 断言：加载应该成功，当前配置应该与加载的配置匹配
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(testName, _configManager.CurrentConfig.Name);
            Assert.AreEqual(testDisplayName, _configManager.CurrentConfig.DisplayName);
        }

        [UnityTest]
        public IEnumerator ExportConfigAsync_ShouldExportConfigToFile()
        {
            // 日志测试路径，帮助调试
            Debug.Log($"测试导出文件路径: {_testExportPath}");

            // 安排：创建配置
            var config = new PackageConfig("com.test.export", "Test Export", "1.0.0", "This is a test package for export");

            try
            {
                // 确保目录存在
                var directory = Path.GetDirectoryName(_testExportPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Debug.Log($"正在创建目录: {directory}");
                    Directory.CreateDirectory(directory);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"创建目录时发生异常: {ex.Message}");
                yield break; // 终止测试
            }

            // 首先验证序列化过程是否正常工作
            var directJson = JsonUtility.ToJson(config, true);
            Debug.Log($"直接序列化结果: {directJson}");

            // 执行：导出配置
            var task = _configManager.ExportConfigAsync(config, _testExportPath);

            // 等待任务完成 - 这部分必须在try块外
            while (!task.IsCompleted)
                yield return null;

            ValidationResult result = null;

            try
            {
                result = task.Result;

                // 输出结果信息，帮助调试
                if (!result.IsValid)
                {
                    foreach (var msg in result.Messages)
                    {
                        Debug.LogError($"导出错误: {msg.Message} (级别: {msg.Level})");
                    }
                }

                // 断言：导出应该成功，文件应该存在
                Assert.IsTrue(result.IsValid, "导出结果应该是有效的");
                Assert.IsTrue(File.Exists(_testExportPath), $"文件应该存在: {_testExportPath}");

                // 验证文件内容
                var json = File.ReadAllText(_testExportPath);
                Debug.Log($"文件实际内容: {json}");

                // 如果文件内容为空，这里可能有问题
                if (string.IsNullOrWhiteSpace(json))
                {
                    Assert.Fail("导出的JSON文件内容为空");
                }

                // 检查内容中是否包含关键信息
                var containsName = json.Contains("com.test.export");
                var containsDisplayName = json.Contains("Test Export");

                // 提供更详细的错误信息
                Assert.IsTrue(containsName, $"JSON应该包含包名'com.test.export'，但实际内容是: {(json.Length > 100 ? json.Substring(0, 100) + "..." : json)}");
                Assert.IsTrue(containsDisplayName, "JSON应该包含显示名称'Test Export'");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"验证结果时发生异常: {ex.Message}");
            }
            finally
            {
                // 确保清理测试文件
                if (File.Exists(_testExportPath))
                {
                    try
                    {
                        File.Delete(_testExportPath);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"清理测试文件失败: {_testExportPath}, 错误: {ex.Message}");
                    }
                }
            }
        }

        [UnityTest]
        public IEnumerator ImportConfigAsync_WithJsonFile_ShouldImportConfig()
        {
            // 日志测试路径，帮助调试
            Debug.Log($"测试导入文件路径: {_testImportPath}");

            // 安排：创建导出配置文件
            var testName = "com.test.import";
            var testDisplayName = "Test Import";

            try
            {
                // 创建有效的JSON格式，与我们的导出格式匹配
                var jsonObj = new Dictionary<string, object>
                {
                    { "name", testName },
                    { "displayName", testDisplayName },
                    { "version", "1.0.0" },
                    { "description", "This is a test package for import" },
                    { "unity", "2021.3" },
                    { "author", new Dictionary<string, object>
                        {
                            { "name", "Test Author" },
                            { "email", "" },
                            { "url", "" }
                        }
                    }
                };

                // 序列化为JSON字符串
                var json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                Debug.Log($"准备写入的JSON内容: {json}");
                File.WriteAllText(_testImportPath, json);

                // 验证文件是否正确创建
                Assert.IsTrue(File.Exists(_testImportPath), "测试文件应该已创建");
                var fileContent = File.ReadAllText(_testImportPath);
                Debug.Log($"文件实际内容: {fileContent}");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"准备测试文件时发生异常: {ex.Message}");
                yield break;
            }

            // 执行：导入配置
            var task = _configManager.ImportConfigAsync(_testImportPath);

            // 等待任务完成
            while (!task.IsCompleted)
                yield return null;

            try
            {
                var result = task.Result;

                // 输出结果信息，帮助调试
                if (!result.IsValid)
                {
                    foreach (var msg in result.Messages)
                    {
                        Debug.LogError($"导入错误: {msg.Message} (级别: {msg.Level})");
                    }
                }

                // 断言：导入应该成功，当前配置应该与导入的配置匹配
                Assert.IsTrue(result.IsValid, "导入结果应该是有效的");
                Assert.AreEqual(testName, _configManager.CurrentConfig.Name, "当前配置的名称应该匹配");
                Assert.AreEqual(testDisplayName, _configManager.CurrentConfig.DisplayName, "当前配置的显示名称应该匹配");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"验证结果时发生异常: {ex.Message}");
            }
        }

        [UnityTest]
        public IEnumerator ImportConfigAsync_WithNonexistentFile_ShouldReturnError()
        {
            // 安排：确保文件不存在
            var nonexistentFile = TestHelpers.GetTestFilePath("nonexistent.json");
            if (File.Exists(nonexistentFile))
                File.Delete(nonexistentFile);

            // 执行：导入不存在的文件
            var task = _configManager.ImportConfigAsync(nonexistentFile);

            // 等待任务完成
            while (!task.IsCompleted)
                yield return null;

            var result = task.Result;

            // 断言：导入应该失败
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.HasErrors);
        }

        [UnityTest]
        public IEnumerator ImportConfigAsync_WithUnsupportedFormat_ShouldReturnError()
        {
            // 安排：创建非JSON格式的文件
            var unsupportedFile = TestHelpers.GetTestFilePath("test.txt");

            try
            {
                File.WriteAllText(unsupportedFile, "This is not a JSON file");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"准备测试文件时发生异常: {ex.Message}");
                yield break;
            }

            // 执行：导入不支持的格式
            var task = _configManager.ImportConfigAsync(unsupportedFile);

            // 等待任务完成
            while (!task.IsCompleted)
                yield return null;

            try
            {
                var result = task.Result;

                // 断言：导入应该失败
                Assert.IsFalse(result.IsValid, "结果应该是无效的");
                Assert.IsTrue(result.HasErrors, "结果应该包含错误");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Assert.Fail($"验证结果时发生异常: {ex.Message}");
            }
            finally
            {
                // 清理
                if (File.Exists(unsupportedFile))
                {
                    try
                    {
                        File.Delete(unsupportedFile);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"清理测试文件失败: {unsupportedFile}, 错误: {ex.Message}");
                    }
                }
            }
        }
    }
}
