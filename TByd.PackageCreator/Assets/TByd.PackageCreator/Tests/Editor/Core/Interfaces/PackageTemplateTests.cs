using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Core.Interfaces
{
    /// <summary>
    /// 包模板测试类
    /// </summary>
    public class PackageTemplateTests
    {
        /// <summary>
        /// 测试用的模拟模板实现
        /// </summary>
        private class MockPackageTemplate : IPackageTemplate
        {
            public string Id => "mock.template";
            public string Name => "模拟模板";
            public string Description => "用于测试的模拟模板";
            public string Version => "1.0.0";
            public string Author => "测试人员";
            public Texture2D Icon => null;

            private List<TemplateDirectory> _directories = new List<TemplateDirectory>();
            private List<TemplateFile> _files = new List<TemplateFile>();
            private List<TemplateOption> _options = new List<TemplateOption>();

            public IReadOnlyList<TemplateDirectory> Directories => _directories;
            public IReadOnlyList<TemplateFile> Files => _files;
            public IReadOnlyList<TemplateOption> Options => _options;

            public MockPackageTemplate()
            {
                // 添加一些目录
                var runtimeDir = new TemplateDirectory("Runtime", "运行时代码");
                var editorDir = new TemplateDirectory("Editor", "编辑器代码");
                _directories.Add(runtimeDir);
                _directories.Add(editorDir);

                // 添加一些文件
                _files.Add(new TemplateFile("package.json", "{\"name\": \"${NAME}\"}", "包配置文件"));
                _files.Add(new TemplateFile("README.md", "# ${DISPLAY_NAME}", "自述文件"));

                // 添加一些选项
                var includeTestsOption = new TemplateOption("includeTests", "包含测试", "是否包含测试目录",
                    TemplateOptionType.Boolean, "true");
                _options.Add(includeTestsOption);
            }

            public ValidationResult ValidateConfig(PackageConfig config)
            {
                var result = new ValidationResult();

                // 简单验证
                if (string.IsNullOrEmpty(config.Name))
                {
                    result.AddError("包名称不能为空", "Name");
                }

                if (string.IsNullOrEmpty(config.DisplayName))
                {
                    result.AddError("包显示名称不能为空", "DisplayName");
                }

                return result;
            }

            public bool Generate(PackageConfig config, string targetPath)
            {
                // 这个方法在实际测试中会被模拟，这里只返回true
                return true;
            }

            public async Task<ValidationResult> GenerateAsync(PackageConfig config, string targetPath, FileGenerator fileGenerator = null)
            {
                // 异步版本的生成方法，在测试中只返回成功结果
                await Task.Delay(1); // 模拟异步操作
                return new ValidationResult(); // 返回一个有效的验证结果
            }

            public TemplatePreviewInfo GetPreviewInfo()
            {
                return new TemplatePreviewInfo("模拟模板", "这是一个用于测试的模拟模板");
            }
        }

        private MockPackageTemplate _template;

        [SetUp]
        public void Setup()
        {
            _template = new MockPackageTemplate();
        }

        [Test]
        public void IPackageTemplate_Properties_ShouldReturnCorrectValues()
        {
            // 断言基本属性
            Assert.AreEqual("mock.template", _template.Id);
            Assert.AreEqual("模拟模板", _template.Name);
            Assert.AreEqual("用于测试的模拟模板", _template.Description);
            Assert.AreEqual("1.0.0", _template.Version);
            Assert.AreEqual("测试人员", _template.Author);
            Assert.IsNull(_template.Icon);

            // 断言集合属性
            Assert.IsNotNull(_template.Directories);
            Assert.AreEqual(2, _template.Directories.Count);
            Assert.AreEqual("Runtime", _template.Directories[0].RelativePath);
            Assert.AreEqual("Editor", _template.Directories[1].RelativePath);

            Assert.IsNotNull(_template.Files);
            Assert.AreEqual(2, _template.Files.Count);
            Assert.AreEqual("package.json", _template.Files[0].RelativePath);
            Assert.AreEqual("README.md", _template.Files[1].RelativePath);

            Assert.IsNotNull(_template.Options);
            Assert.AreEqual(1, _template.Options.Count);
            Assert.AreEqual("includeTests", _template.Options[0].Key);
        }

        [Test]
        public void ValidateConfig_WithValidConfig_ShouldReturnIsValid()
        {
            // 安排
            var config = new PackageConfig("com.example.test", "测试包");

            // 执行
            var result = _template.ValidateConfig(config);

            // 断言
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(result.HasErrors);
            Assert.AreEqual(0, result.Messages.Count);
        }

        [Test]
        public void ValidateConfig_WithInvalidConfig_ShouldReturnErrors()
        {
            // 安排 - 空的包配置
            var config = new PackageConfig("", "");

            // 执行
            var result = _template.ValidateConfig(config);

            // 断言
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.HasErrors);
            Assert.AreEqual(2, result.Messages.Count);
        }

        [Test]
        public void Generate_ShouldReturnTrue()
        {
            // 安排
            var config = new PackageConfig("com.example.test", "测试包");
            var targetPath = TestHelpers.GetTestFilePath("TemplateTest");

            // 执行
            var success = _template.Generate(config, targetPath);

            // 断言
            Assert.IsTrue(success);
        }

        [Test]
        public void GetPreviewInfo_ShouldReturnValidPreviewInfo()
        {
            // 执行
            var previewInfo = _template.GetPreviewInfo();

            // 断言
            Assert.IsNotNull(previewInfo);
            Assert.AreEqual("模拟模板", previewInfo.Title);
            Assert.AreEqual("这是一个用于测试的模拟模板", previewInfo.Content);
        }
    }
}
