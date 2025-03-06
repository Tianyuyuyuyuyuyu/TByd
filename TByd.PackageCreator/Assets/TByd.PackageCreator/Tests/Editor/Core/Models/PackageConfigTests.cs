using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Tests.Editor;

namespace TByd.PackageCreator.Tests.Editor.Core.Models
{
    /// <summary>
    /// 包配置测试类
    /// </summary>
    public class PackageConfigTests
    {
        [Test]
        public void PackageDependency_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var id = "com.unity.test-framework";
            var version = "1.1.31";

            // 执行
            var dependency = new PackageDependency(id, version);

            // 断言
            Assert.AreEqual(id, dependency.Id);
            Assert.AreEqual(version, dependency.Version);
        }

        [Test]
        public void PackageAuthor_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var name = "测试作者";
            var email = "test@example.com";
            var url = "https://example.com";

            // 执行
            var author = new PackageAuthor(name, email, url);

            // 断言
            Assert.AreEqual(name, author.Name);
            Assert.AreEqual(email, author.Email);
            Assert.AreEqual(url, author.Url);
        }

        [Test]
        public void PackageConfig_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var name = "com.example.test";
            var displayName = "测试包";
            var version = "1.0.0";
            var description = "这是一个测试包";

            // 执行
            var config = new PackageConfig(name, displayName, version, description);

            // 断言
            Assert.AreEqual(name, config.Name);
            Assert.AreEqual(displayName, config.DisplayName);
            Assert.AreEqual(version, config.Version);
            Assert.AreEqual(description, config.Description);
            Assert.IsNotNull(config.Dependencies);
            Assert.IsNotNull(config.Keywords);
            Assert.IsNotNull(config.Author);
            Assert.IsNotNull(config.CustomOptions);
        }

        [Test]
        public void PackageConfig_AddDependency_ShouldAddDependencyToList()
        {
            // 安排
            var config = new PackageConfig("com.example.test", "测试包");
            var dependencyId = "com.unity.test-framework";
            var dependencyVersion = "1.1.31";

            // 执行
            config.AddDependency(dependencyId, dependencyVersion);

            // 断言
            Assert.AreEqual(1, config.Dependencies.Count);
            Assert.AreEqual(dependencyId, config.Dependencies[0].Id);
            Assert.AreEqual(dependencyVersion, config.Dependencies[0].Version);
        }

        [Test]
        public void PackageConfig_AddKeyword_ShouldAddKeywordToList()
        {
            // 安排
            var config = new PackageConfig("com.example.test", "测试包");
            var keyword = "测试";

            // 执行
            config.AddKeyword(keyword);

            // 断言
            Assert.AreEqual(1, config.Keywords.Count);
            Assert.AreEqual(keyword, config.Keywords[0]);
        }

        [Test]
        public void PackageConfig_SetAuthor_ShouldSetAuthorProperties()
        {
            // 安排
            var config = new PackageConfig("com.example.test", "测试包");
            var name = "测试作者";
            var email = "test@example.com";
            var url = "https://example.com";

            // 执行
            config.SetAuthor(name, email, url);

            // 断言
            Assert.IsNotNull(config.Author);
            Assert.AreEqual(name, config.Author.Name);
            Assert.AreEqual(email, config.Author.Email);
            Assert.AreEqual(url, config.Author.Url);
        }

        [Test]
        public void PackageConfig_AddCustomOption_ShouldAddOptionToDictionary()
        {
            // 安排
            var config = new PackageConfig("com.example.test", "测试包");
            var key = "customKey";
            var value = "customValue";

            // 执行
            config.AddCustomOption(key, value);

            // 断言
            Assert.IsTrue(config.CustomOptions.ContainsKey(key));
            Assert.AreEqual(value, config.CustomOptions[key]);
        }
    }
}
