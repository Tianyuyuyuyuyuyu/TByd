using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Tests.Editor.Core.Models
{
    /// <summary>
    /// 配置序列化测试类
    /// </summary>
    public class ConfigSerializationTests
    {
        private string _testFilePath;

        [SetUp]
        public void Setup()
        {
            // 创建测试文件路径
            _testFilePath = TestHelpers.GetTestFilePath("serialization_test.json");

            // 确保测试文件不存在
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TearDown]
        public void Teardown()
        {
            // 清理测试文件
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [Test]
        public void Serialize_DeserializePackageConfig_ShouldMaintainAllProperties()
        {
            // 安排：创建一个包含所有属性的配置
            var originalConfig = CreateComplexConfig();

            // 执行：序列化和反序列化
            var json = JsonConvert.SerializeObject(originalConfig, Formatting.Indented);
            var deserializedConfig = JsonConvert.DeserializeObject<PackageConfig>(json);

            // 断言：验证所有属性是否保持一致
            Assert.AreEqual(originalConfig.Name, deserializedConfig.Name);
            Assert.AreEqual(originalConfig.DisplayName, deserializedConfig.DisplayName);
            Assert.AreEqual(originalConfig.Version, deserializedConfig.Version);
            Assert.AreEqual(originalConfig.Description, deserializedConfig.Description);
            Assert.AreEqual(originalConfig.UnityVersion, deserializedConfig.UnityVersion);

            // 验证作者信息
            Assert.IsNotNull(deserializedConfig.Author);
            Assert.AreEqual(originalConfig.Author.Name, deserializedConfig.Author.Name);
            Assert.AreEqual(originalConfig.Author.Email, deserializedConfig.Author.Email);
            Assert.AreEqual(originalConfig.Author.Url, deserializedConfig.Author.Url);

            // 验证依赖项
            Assert.AreEqual(originalConfig.Dependencies.Count, deserializedConfig.Dependencies.Count);
            for (var i = 0; i < originalConfig.Dependencies.Count; i++)
            {
                Assert.AreEqual(originalConfig.Dependencies[i].Id, deserializedConfig.Dependencies[i].Id);
                Assert.AreEqual(originalConfig.Dependencies[i].Version, deserializedConfig.Dependencies[i].Version);
            }

            // 验证关键字
            Assert.AreEqual(originalConfig.Keywords.Count, deserializedConfig.Keywords.Count);
            for (var i = 0; i < originalConfig.Keywords.Count; i++)
            {
                Assert.AreEqual(originalConfig.Keywords[i], deserializedConfig.Keywords[i]);
            }

            // 验证自定义选项
            Assert.IsNotNull(deserializedConfig.CustomOptions);
            foreach (var key in originalConfig.CustomOptions.Keys)
            {
                Assert.IsTrue(deserializedConfig.CustomOptions.ContainsKey(key));
                Assert.AreEqual(originalConfig.CustomOptions[key], deserializedConfig.CustomOptions[key]);
            }

            // 验证自定义变量
            Assert.IsNotNull(deserializedConfig.CustomVariables);
            foreach (var key in originalConfig.CustomVariables.Keys)
            {
                Assert.IsTrue(deserializedConfig.CustomVariables.ContainsKey(key));
                Assert.AreEqual(originalConfig.CustomVariables[key], deserializedConfig.CustomVariables[key]);
            }
        }

        [Test]
        public void SaveLoadPackageConfig_ShouldMaintainAllProperties()
        {
            // 安排：创建一个包含所有属性的配置
            var originalConfig = CreateComplexConfig();

            // 执行：保存到文件和从文件加载
            var json = JsonConvert.SerializeObject(originalConfig, Formatting.Indented);
            File.WriteAllText(_testFilePath, json);
            var loadedJson = File.ReadAllText(_testFilePath);
            var loadedConfig = JsonConvert.DeserializeObject<PackageConfig>(loadedJson);

            // 断言：验证所有属性是否保持一致
            Assert.AreEqual(originalConfig.Name, loadedConfig.Name);
            Assert.AreEqual(originalConfig.DisplayName, loadedConfig.DisplayName);
            Assert.AreEqual(originalConfig.Version, loadedConfig.Version);
            Assert.AreEqual(originalConfig.Description, loadedConfig.Description);

            // 验证作者信息
            Assert.IsNotNull(loadedConfig.Author);
            Assert.AreEqual(originalConfig.Author.Name, loadedConfig.Author.Name);

            // 验证依赖项
            Assert.AreEqual(originalConfig.Dependencies.Count, loadedConfig.Dependencies.Count);

            // 验证自定义变量
            Assert.IsNotNull(loadedConfig.CustomVariables);
            foreach (var key in originalConfig.CustomVariables.Keys)
            {
                Assert.IsTrue(loadedConfig.CustomVariables.ContainsKey(key));
                Assert.AreEqual(originalConfig.CustomVariables[key], loadedConfig.CustomVariables[key]);
            }
        }

        [Test]
        public void ConfigHistoryEntry_ShouldSerializeAndDeserializeCorrectly()
        {
            // 安排：创建一个ConfigHistoryEntry
            var config = new PackageConfig("com.test.package", "Test Package");
            var originalEntry = new ConfigHistoryEntry(config, "测试历史记录");

            // 执行：序列化ConfigHistoryEntry为JSON字符串
            var wrapper = new ConfigHistoryEntryWrapper(originalEntry);
            var json = JsonConvert.SerializeObject(wrapper, Formatting.Indented);

            // 反序列化为ConfigHistoryEntry
            var deserializedWrapper = JsonConvert.DeserializeObject<ConfigHistoryEntryWrapper>(json);
            var deserializedEntry = deserializedWrapper.Entry;

            // 断言：验证属性是否保持一致
            Assert.IsNotNull(deserializedEntry);
            Assert.AreEqual(originalEntry.Description, deserializedEntry.Description);
            Assert.AreEqual(originalEntry.Config.Name, deserializedEntry.Config.Name);
            Assert.AreEqual(originalEntry.Config.DisplayName, deserializedEntry.Config.DisplayName);
        }

        // 创建一个复杂的配置用于测试
        private PackageConfig CreateComplexConfig()
        {
            var config = new PackageConfig(
                "com.test.complexpackage",
                "Complex Test Package",
                "1.2.3",
                "This is a complex test package with many properties"
            );

            // 设置基本属性
            config.UnityVersion = "2021.3";
            config.MinUnityVersion = "2020.3";
            config.RootNamespace = "Test.ComplexPackage";
            config.Company = "Test Company";
            config.License = "MIT";
            config.DocumentationUrl = "https://example.com/docs";
            config.ChangelogUrl = "https://example.com/changelog";
            config.LicenseUrl = "https://example.com/license";

            // 设置作者信息
            config.Author = new PackageAuthor(
                "Test Author",
                "test@example.com",
                "https://example.com/author"
            );

            // 添加依赖项
            config.Dependencies.Add(new PackageDependency("com.unity.test", "1.0.0"));
            config.Dependencies.Add(new PackageDependency("com.test.dependency", "^2.0.0"));

            // 添加关键字
            config.Keywords.Add("test");
            config.Keywords.Add("complex");
            config.Keywords.Add("package");

            // 添加自定义选项
            config.CustomOptions["option1"] = "value1";
            config.CustomOptions["option2"] = "value2";

            // 添加自定义变量
            config.CustomVariables["var1"] = "value1";
            config.CustomVariables["var2"] = "value2";

            return config;
        }

        // 包装类，用于序列化/反序列化ConfigHistoryEntry
        [System.Serializable]
        private class ConfigHistoryEntryWrapper
        {
            [JsonProperty("entry")]
            public ConfigHistoryEntry Entry;

            [JsonConstructor]
            public ConfigHistoryEntryWrapper()
            {
            }

            public ConfigHistoryEntryWrapper(ConfigHistoryEntry entry)
            {
                Entry = entry;
            }
        }
    }
}
