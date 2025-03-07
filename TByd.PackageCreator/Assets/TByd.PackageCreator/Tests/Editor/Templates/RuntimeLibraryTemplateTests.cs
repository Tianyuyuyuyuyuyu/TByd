using System.Linq;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Templates.Implementations;

namespace TByd.PackageCreator.Tests.Editor.Templates
{
    /// <summary>
    /// 测试RuntimeLibraryTemplate的功能
    /// </summary>
    public class RuntimeLibraryTemplateTests
    {
        private RuntimeLibraryTemplate _template;

        [SetUp]
        public void Setup()
        {
            _template = new RuntimeLibraryTemplate();
        }

        [Test]
        public void HasCorrectIdAndName()
        {
            Assert.AreEqual("com.tbyd.packagecreator.template.runtimelibrary", _template.Id);
            Assert.AreEqual("运行时库包", _template.Name);
            Assert.IsTrue(_template.Description.Contains("运行时功能库"));
        }

        [Test]
        public void InheritsBasicPackageTemplateFunctionality()
        {
            // 运行时库模板应该继承基础包模板的功能
            var directories = _template.Directories;

            // 验证基础目录存在
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Editor"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Documentation~"));

            // 验证基础文件存在
            var files = _template.Files;
            Assert.IsTrue(files.Any(f => f.RelativePath == "package.json"));
            Assert.IsTrue(files.Any(f => f.RelativePath == "README.md"));

            // 验证基础选项存在
            var options = _template.Options;
            Assert.IsTrue(options.Any(o => o.Key == "licenseType"));
        }

        [Test]
        public void HasRuntimeLibrarySpecificDirectories()
        {
            var directories = _template.Directories;

            // 运行时库模板应该包含特定的子目录
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime/Scripts"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime/Prefabs"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime/Materials"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime/Textures"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime/Shaders"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Runtime/Animations"));
        }

        [Test]
        public void HasRuntimeLibrarySpecificFiles()
        {
            var files = _template.Files;

            // 运行时库模板应该包含示例类文件
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("Example.cs")));
        }

        [Test]
        public void HasRuntimeLibrarySpecificOptions()
        {
            var options = _template.Options;

            // 验证运行时库特有的选项
            Assert.IsTrue(options.Any(o => o.Key == "includeScriptableObjects"));
            Assert.IsTrue(options.Any(o => o.Key == "includeShaders"));
            Assert.IsTrue(options.Any(o => o.Key == "libraryType"));
            Assert.IsTrue(options.Any(o => o.Key == "targetPlatforms"));

            // 验证库类型选项
            var libraryTypeOption = options.FirstOrDefault(o => o.Key == "libraryType");
            Assert.IsNotNull(libraryTypeOption);
            Assert.AreEqual(TemplateOptionType.k_Enum, libraryTypeOption.Type);
            Assert.AreEqual("Utility", libraryTypeOption.DefaultValue);

            // 验证库类型选项的可能值
            Assert.IsTrue(libraryTypeOption.PossibleValues.Contains("Utility"));
            Assert.IsTrue(libraryTypeOption.PossibleValues.Contains("Gameplay"));
            Assert.IsTrue(libraryTypeOption.PossibleValues.Contains("Graphics"));
        }

        [Test]
        public void ValidatesMinUnityVersion()
        {
            // 创建缺少MinUnityVersion的配置
            var config = new PackageConfig("com.test.package", "测试包", "1.0.0", "测试包描述");
            config.MinUnityVersion = null;

            var result = _template.ValidateConfig(config);

            // 应该有警告但没有错误
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.GetMessages(ValidationMessageLevel.k_Error).Count);
            var warningMessages = result.GetMessages(ValidationMessageLevel.k_Warning);
            Assert.IsTrue(warningMessages.Count > 0);
            Assert.IsTrue(warningMessages.Any(w => w.Message.Contains("Unity版本")));

            // 设置最低Unity版本后，警告应该减少
            config.MinUnityVersion = "2021.3";
            result = _template.ValidateConfig(config);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void GetPreviewInfoHasRuntimeLibrarySpecificFeatures()
        {
            var previewInfo = _template.GetPreviewInfo();

            // 验证运行时库特有的特点
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("运行时")));
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("资源目录")));
        }

        [Test]
        public void ExampleClassContainsBaseMonoBehaviourFunctionality()
        {
            var files = _template.Files;

            // 获取Example.cs文件
            var exampleFile = files.FirstOrDefault(f => f.RelativePath.Contains("Example.cs"));
            Assert.IsNotNull(exampleFile);

            // 检查文件内容是否包含MonoBehaviour基本功能
            Assert.IsTrue(exampleFile.ContentTemplate.Contains("MonoBehaviour"));
            Assert.IsTrue(exampleFile.ContentTemplate.Contains("Awake"));
            Assert.IsTrue(exampleFile.ContentTemplate.Contains("SerializeField"));
        }
    }
}
