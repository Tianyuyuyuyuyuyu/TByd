using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Tests.Editor.Core.Models
{
    /// <summary>
    /// 模板结构测试类
    /// </summary>
    public class TemplateStructuresTests
    {
        [Test]
        public void TemplateDirectory_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var path = "Runtime";
            var description = "运行时代码目录";
            var isRequired = true;

            // 执行
            var directory = new TemplateDirectory(path, description, isRequired);

            // 断言
            Assert.AreEqual(path, directory.RelativePath);
            Assert.AreEqual(description, directory.Description);
            Assert.AreEqual(isRequired, directory.IsRequired);
            Assert.IsNotNull(directory.Subdirectories);
            Assert.AreEqual(0, directory.Subdirectories.Count);
        }

        [Test]
        public void TemplateDirectory_AddSubdirectory_ShouldAddDirectoryToSubdirectories()
        {
            // 安排
            var parentDir = new TemplateDirectory("Parent", "父目录");
            var childDir = new TemplateDirectory("Child", "子目录");

            // 执行
            parentDir.AddSubdirectory(childDir);

            // 断言
            Assert.AreEqual(1, parentDir.Subdirectories.Count);
            Assert.AreSame(childDir, parentDir.Subdirectories[0]);
        }

        [Test]
        public void TemplateFile_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var path = "package.json";
            var content = "{\"name\": \"${NAME}\"}";
            var description = "包配置文件";
            var isRequired = true;
            var supportsVars = true;

            // 执行
            var file = new TemplateFile(path, content, description, isRequired, supportsVars);

            // 断言
            Assert.AreEqual(path, file.RelativePath);
            Assert.AreEqual(content, file.ContentTemplate);
            Assert.AreEqual(description, file.Description);
            Assert.AreEqual(isRequired, file.IsRequired);
            Assert.AreEqual(supportsVars, file.SupportsVariableReplacement);
        }

        [Test]
        public void TemplatePreviewInfo_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var title = "基础UPM包";
            var content = "创建基础的UPM包结构";

            // 执行
            var previewInfo = new TemplatePreviewInfo(title, content);

            // 断言
            Assert.AreEqual(title, previewInfo.Title);
            Assert.AreEqual(content, previewInfo.Content);
            Assert.IsNotNull(previewInfo.Features);
            Assert.AreEqual(0, previewInfo.Features.Count);
        }

        [Test]
        public void TemplatePreviewInfo_AddFeature_ShouldAddFeatureToFeaturesList()
        {
            // 安排
            var preview = new TemplatePreviewInfo("测试", "测试内容");
            var feature = "支持Assembly Definition";

            // 执行
            preview.AddFeature(feature);

            // 断言
            Assert.AreEqual(1, preview.Features.Count);
            Assert.AreEqual(feature, preview.Features[0]);
        }
    }
}
