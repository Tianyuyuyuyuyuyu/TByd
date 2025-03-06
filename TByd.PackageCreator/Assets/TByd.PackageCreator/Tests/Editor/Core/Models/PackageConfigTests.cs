using System;
using System.Collections.Generic;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Core.Models
{
    /// <summary>
    /// 包配置测试类
    /// </summary>
    public class PackageConfigTests
    {
        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            // 安排
            string name = "com.test.package";
            string displayName = "Test Package";
            string version = "1.0.0";
            string description = "Test package description";

            // 执行
            var config = new PackageConfig(name, displayName, version, description);

            // 断言
            Assert.AreEqual(name, config.Name);
            Assert.AreEqual(displayName, config.DisplayName);
            Assert.AreEqual(version, config.Version);
            Assert.AreEqual(description, config.Description);
            Assert.IsNotNull(config.Author);
        }

        [Test]
        public void Constructor_WithDefaultValues_ShouldSetDefaultVersion()
        {
            // 安排
            string name = "com.test.package";
            string displayName = "Test Package";

            // 执行：只提供必须的参数
            var config = new PackageConfig(name, displayName);

            // 断言：版本应该是默认值"0.1.0"
            Assert.AreEqual("0.1.0", config.Version);
        }

        [Test]
        public void Dependencies_ShouldBeEmptyByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认情况下依赖项列表应该为空，但不为null
            Assert.IsNotNull(config.Dependencies);
            Assert.IsEmpty(config.Dependencies);
        }

        [Test]
        public void Keywords_ShouldBeEmptyByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认情况下关键字列表应该为空，但不为null
            Assert.IsNotNull(config.Keywords);
            Assert.IsEmpty(config.Keywords);
        }

        [Test]
        public void CustomOptions_ShouldBeEmptyByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认情况下自定义选项应该为空，但不为null
            Assert.IsNotNull(config.CustomOptions);
            Assert.IsEmpty(config.CustomOptions);
        }

        [Test]
        public void CustomVariables_ShouldBeEmptyByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认情况下自定义变量应该为空，但不为null
            Assert.IsNotNull(config.CustomVariables);
            Assert.IsEmpty(config.CustomVariables);
        }

        [Test]
        public void AddDependency_ShouldAddToDependenciesList()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");
            string dependencyId = "com.unity.test";
            string dependencyVersion = "1.0.0";

            // 执行
            config.AddDependency(dependencyId, dependencyVersion);

            // 断言
            Assert.AreEqual(1, config.Dependencies.Count);
            Assert.AreEqual(dependencyId, config.Dependencies[0].Id);
            Assert.AreEqual(dependencyVersion, config.Dependencies[0].Version);
        }

        [Test]
        public void DefaultUnityVersion_ShouldBe2021_3()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认Unity版本应该是"2021.3"
            Assert.AreEqual("2021.3", config.UnityVersion);
        }

        [Test]
        public void IncludeTests_ShouldBeTrueByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认包含测试
            Assert.IsTrue(config.IncludeTests);
        }

        [Test]
        public void IncludeSamples_ShouldBeTrueByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认包含示例
            Assert.IsTrue(config.IncludeSamples);
        }

        [Test]
        public void IncludeDocumentation_ShouldBeTrueByDefault()
        {
            // 安排
            var config = new PackageConfig("com.test.package", "Test Package");

            // 断言：默认包含文档
            Assert.IsTrue(config.IncludeDocumentation);
        }

        [Test]
        public void PackageAuthor_PropertiesTest()
        {
            // 安排
            string name = "Test Author";
            string email = "test@example.com";
            string url = "https://example.com";

            // 执行
            var author = new PackageAuthor(name, email, url);

            // 断言
            Assert.AreEqual(name, author.Name);
            Assert.AreEqual(email, author.Email);
            Assert.AreEqual(url, author.Url);
        }

        [Test]
        public void PackageDependency_PropertiesTest()
        {
            // 安排
            string id = "com.unity.test";
            string version = "1.0.0";

            // 执行
            var dependency = new PackageDependency(id, version);

            // 断言
            Assert.AreEqual(id, dependency.Id);
            Assert.AreEqual(version, dependency.Version);
        }
    }
}
