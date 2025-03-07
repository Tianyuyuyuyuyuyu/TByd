using System.Linq;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Templates.Implementations;

namespace TByd.PackageCreator.Tests.Editor.Templates
{
    /// <summary>
    /// 测试BasePackageTemplate的基础功能
    /// </summary>
    public class BasePackageTemplateTests
    {
        private TestPackageTemplate _template;

        [SetUp]
        public void Setup()
        {
            _template = new TestPackageTemplate();
        }

        [Test]
        public void HasCorrectIdAndName()
        {
            Assert.AreEqual("com.tbyd.test.template", _template.Id);
            Assert.AreEqual("测试模板", _template.Name);
            Assert.AreEqual("用于测试的基础模板", _template.Description);
        }

        [Test]
        public void HasDefaultVersion()
        {
            Assert.AreEqual("1.0.0", _template.Version);
        }

        [Test]
        public void HasDefaultAuthor()
        {
            Assert.AreEqual("TByd", _template.Author);
        }

        [Test]
        public void HasBaseDirectories()
        {
            var directories = _template.Directories;

            // 至少应该有一个目录（Documentation~）
            Assert.IsTrue(directories.Count >= 1);
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Documentation~"));
        }

        [Test]
        public void HasBaseFiles()
        {
            var files = _template.Files;

            // 基础文件包括package.json, README.md, CHANGELOG.md, LICENSE.md
            Assert.IsTrue(files.Count >= 4);
            Assert.IsTrue(files.Any(f => f.RelativePath == "package.json"));
            Assert.IsTrue(files.Any(f => f.RelativePath == "README.md"));
            Assert.IsTrue(files.Any(f => f.RelativePath == "CHANGELOG.md"));
            Assert.IsTrue(files.Any(f => f.RelativePath == "LICENSE.md"));
        }

        [Test]
        public void HasBaseOptions()
        {
            var options = _template.Options;

            // 基础选项至少包括许可类型选项
            Assert.IsTrue(options.Count >= 1);
            var licenseOption = options.FirstOrDefault(o => o.Key == "licenseType");
            Assert.IsNotNull(licenseOption);
            Assert.AreEqual(TemplateOptionType.Enum, licenseOption.Type);
            Assert.AreEqual("MIT", licenseOption.DefaultValue);

            // 检查许可选项的可选值
            Assert.IsTrue(licenseOption.PossibleValues.Contains("MIT"));
            Assert.IsTrue(licenseOption.PossibleValues.Contains("Apache-2.0"));
        }

        [Test]
        public void ValidatesConfigCorrectly()
        {
            // 创建无效的配置（缺少Name和Version）
            var invalidConfig = new PackageConfig(null, "测试包");
            invalidConfig.Name = null;
            invalidConfig.Version = null;

            var result = _template.ValidateConfig(invalidConfig);

            // 应该有两个错误（Name为空，Version为空）
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.HasErrors);
            var errorMessages = result.GetMessages(ValidationMessageLevel.Error);
            Assert.AreEqual(2, errorMessages.Count);
            Assert.IsTrue(errorMessages.Any(e => e.Message.Contains("包名称不能为空")));
            Assert.IsTrue(errorMessages.Any(e => e.Message.Contains("包版本不能为空")));

            // 创建有效的配置
            var validConfig = new PackageConfig("com.test.package", "测试包", "1.0.0", "测试包描述");
            result = _template.ValidateConfig(validConfig);

            // 不应该有错误，可能有警告
            Assert.AreEqual(0, result.GetMessages(ValidationMessageLevel.Error).Count);
        }

        [Test]
        public void GetPreviewInfoCorrectly()
        {
            var previewInfo = _template.GetPreviewInfo();

            Assert.AreEqual(_template.Name, previewInfo.Title);
            Assert.AreEqual(_template.Description, previewInfo.Content);

            // 基础预览至少应该有3个特点
            Assert.IsTrue(previewInfo.Features.Count >= 3);
        }

        /// <summary>
        /// 用于测试的模板实现
        /// </summary>
        private class TestPackageTemplate : BasePackageTemplate
        {
            public override string Id => "com.tbyd.test.template";
            public override string Name => "测试模板";
            public override string Description => "用于测试的基础模板";
        }
    }
}
