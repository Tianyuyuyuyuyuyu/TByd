using System.Linq;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Templates.Implementations;

namespace TByd.PackageCreator.Tests.Editor.Templates
{
    /// <summary>
    /// 测试BasicPackageTemplate的功能
    /// </summary>
    public class BasicPackageTemplateTests
    {
        private BasicPackageTemplate _template;

        [SetUp]
        public void Setup()
        {
            _template = new BasicPackageTemplate();
        }

        [Test]
        public void HasCorrectIdAndName()
        {
            Assert.AreEqual("com.tbyd.packagecreator.template.basic", _template.Id);
            Assert.AreEqual("基础Unity包", _template.Name);
            Assert.IsTrue(_template.Description.Contains("基础的Unity包结构"));
        }

        [Test]
        public void HasCorrectDirectoryStructure()
        {
            var directories = _template.Directories;

            // 基础包应该包含Runtime、Editor等目录
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Editor"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Tests"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Tests/Editor"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Tests/Runtime"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Documentation~"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Samples~"));
        }

        [Test]
        public void HasAsmdefFiles()
        {
            var files = _template.Files;

            // 验证程序集定义文件存在
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("Runtime") && f.RelativePath.Contains(".asmdef")));
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("Editor") && f.RelativePath.Contains(".asmdef")));
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("Tests/Editor") && f.RelativePath.Contains(".asmdef")));
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("Tests/Runtime") && f.RelativePath.Contains(".asmdef")));
        }

        [Test]
        public void HasCorrectOptions()
        {
            var options = _template.Options;

            // 验证选项包含了基础包特有的选项
            Assert.IsTrue(options.Any(o => o.Key == "licenseType"));
            Assert.IsTrue(options.Any(o => o.Key == "includeEditor"));
            Assert.IsTrue(options.Any(o => o.Key == "includeTests"));
            Assert.IsTrue(options.Any(o => o.Key == "includeSamples"));

            // 验证includeEditor选项的默认值
            var editorOption = options.FirstOrDefault(o => o.Key == "includeEditor");
            Assert.IsNotNull(editorOption);
            Assert.AreEqual("true", editorOption.DefaultValue);
        }

        [Test]
        public void ValidatesRootNamespace()
        {
            // 创建缺少RootNamespace的配置
            var config = new PackageConfig("com.test.package", "测试包", "1.0.0", "测试包描述");
            config.RootNamespace = null;

            var result = _template.ValidateConfig(config);

            // 应该有警告但没有错误
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.GetMessages(ValidationMessageLevel.k_Error).Count);
            var warningMessages = result.GetMessages(ValidationMessageLevel.k_Warning);
            Assert.IsTrue(warningMessages.Count > 0);
            Assert.IsTrue(warningMessages.Any(w => w.Message.Contains("命名空间")));

            // 设置根命名空间后，警告应该减少
            config.RootNamespace = "TestNamespace";
            result = _template.ValidateConfig(config);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void GetPreviewInfoHasAdditionalFeatures()
        {
            var previewInfo = _template.GetPreviewInfo();

            // 验证基础包特有的特点
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("Runtime/Editor")));
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("Assembly Definition")));
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("测试")));
        }
    }
}
