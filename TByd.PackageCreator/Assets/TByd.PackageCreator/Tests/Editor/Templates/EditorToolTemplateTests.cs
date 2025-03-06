using System.Linq;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Templates.Implementations;

namespace TByd.PackageCreator.Tests.Editor.Templates
{
    /// <summary>
    /// 测试EditorToolTemplate的功能
    /// </summary>
    public class EditorToolTemplateTests
    {
        private EditorToolTemplate _template;

        [SetUp]
        public void Setup()
        {
            _template = new EditorToolTemplate();
        }

        [Test]
        public void HasCorrectIdAndName()
        {
            Assert.AreEqual("com.tbyd.packagecreator.template.editortool", _template.Id);
            Assert.AreEqual("编辑器工具包", _template.Name);
            Assert.IsTrue(_template.Description.Contains("编辑器扩展工具"));
        }

        [Test]
        public void InheritsBasicPackageTemplateFunctionality()
        {
            // 编辑器工具模板应该继承基础包模板的功能
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
        public void HasEditorToolSpecificDirectories()
        {
            var directories = _template.Directories;

            // 编辑器工具模板应该包含特定的子目录
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Editor/UI"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Editor/Utils"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Editor/Resources"));
            Assert.IsTrue(directories.Any(d => d.RelativePath == "Editor/Styles"));
        }

        [Test]
        public void HasEditorToolSpecificFiles()
        {
            var files = _template.Files;

            // 编辑器工具模板应该包含特定的文件
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("MainEditorWindow.cs")));
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("EditorUtils.cs")));
            Assert.IsTrue(files.Any(f => f.RelativePath.Contains("EditorStyles.cs")));
        }

        [Test]
        public void HasEditorToolSpecificOptions()
        {
            var options = _template.Options;

            // 验证编辑器工具特有的选项
            Assert.IsTrue(options.Any(o => o.Key == "includeCustomEditor"));
            Assert.IsTrue(options.Any(o => o.Key == "includeEditorWindow"));
            Assert.IsTrue(options.Any(o => o.Key == "includePropertyDrawer"));
            Assert.IsTrue(options.Any(o => o.Key == "uiFramework"));

            // 验证UI框架选项
            var uiFrameworkOption = options.FirstOrDefault(o => o.Key == "uiFramework");
            Assert.IsNotNull(uiFrameworkOption);
            Assert.AreEqual(TemplateOptionType.Enum, uiFrameworkOption.Type);
            Assert.AreEqual("IMGUI", uiFrameworkOption.DefaultValue);

            // 验证UI框架选项的可能值
            Assert.IsTrue(uiFrameworkOption.PossibleValues.Contains("IMGUI"));
            Assert.IsTrue(uiFrameworkOption.PossibleValues.Contains("UIElements"));
            Assert.IsTrue(uiFrameworkOption.PossibleValues.Contains("Hybrid"));
        }

        [Test]
        public void ValidatesPackageNameFormat()
        {
            // 创建不包含editor或tool关键字的配置
            var config = new PackageConfig("com.test.package", "测试包", "1.0.0", "测试包描述");

            var result = _template.ValidateConfig(config);

            // 应该有警告但没有错误
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.GetMessages(ValidationMessageLevel.Error).Count);
            var warningMessages = result.GetMessages(ValidationMessageLevel.Warning);
            Assert.IsTrue(warningMessages.Count > 0);
            Assert.IsTrue(warningMessages.Any(w => w.Message.Contains("editor") || w.Message.Contains("tool")));

            // 使用符合规范的包名
            config.Name = "com.test.editor.tool";
            result = _template.ValidateConfig(config);

            // 警告应该减少
            Assert.IsTrue(result.IsValid);
            warningMessages = result.GetMessages(ValidationMessageLevel.Warning);
            Assert.IsFalse(warningMessages.Any(w => w.Message.Contains("editor") && w.Message.Contains("tool")));
        }

        [Test]
        public void GetPreviewInfoHasEditorToolSpecificFeatures()
        {
            var previewInfo = _template.GetPreviewInfo();

            // 验证编辑器工具特有的特点
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("编辑器扩展")));
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("窗口")));
            Assert.IsTrue(previewInfo.Features.Any(f => f.Contains("UI")));
        }

        [Test]
        public void FileTemplatesContainPlaceholders()
        {
            var files = _template.Files;

            // 获取MainEditorWindow.cs文件
            var editorWindowFile = files.FirstOrDefault(f => f.RelativePath.Contains("MainEditorWindow.cs"));
            Assert.IsNotNull(editorWindowFile);

            // 检查文件内容是否包含占位符
            Assert.IsTrue(editorWindowFile.ContentTemplate.Contains("#ROOT_NAMESPACE#"));
            Assert.IsTrue(editorWindowFile.ContentTemplate.Contains("#DISPLAY_NAME#"));
        }
    }
}
