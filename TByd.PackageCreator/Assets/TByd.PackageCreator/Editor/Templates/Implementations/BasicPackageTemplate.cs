using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Editor.Templates.Implementations
{
    /// <summary>
    /// 基础包模板，提供最基本的Unity包结构
    /// </summary>
    public class BasicPackageTemplate : BasePackageTemplate
    {
        /// <summary>
        /// 模板唯一标识符
        /// </summary>
        public override string Id => "com.tbyd.packagecreator.template.basic";

        /// <summary>
        /// 模板名称
        /// </summary>
        public override string Name => "基础Unity包";

        /// <summary>
        /// 模板描述
        /// </summary>
        public override string Description => "提供基础的Unity包结构，包含标准目录和必要文件";

        /// <summary>
        /// 初始化模板
        /// </summary>
        protected override void InitializeTemplate()
        {
            base.InitializeTemplate();

            // 添加基础包特有的目录
            AddDirectory("Runtime", "运行时代码目录");
            AddDirectory("Editor", "编辑器代码目录", false);
            AddDirectory("Tests", "测试代码目录", false);
            AddDirectory("Tests/Editor", "编辑器测试代码目录", false);
            AddDirectory("Tests/Runtime", "运行时测试代码目录", false);
            AddDirectory("Samples~", "示例代码目录", false);

            // 添加Assembly Definition文件
            AddFile("Runtime/[PACKAGE_NAME].Runtime.asmdef", GetRuntimeAsmdefTemplate(), "运行时程序集定义文件");
            AddFile("Editor/[PACKAGE_NAME].Editor.asmdef", GetEditorAsmdefTemplate(), "编辑器程序集定义文件", false);
            AddFile("Tests/Editor/[PACKAGE_NAME].Editor.Tests.asmdef", GetEditorTestsAsmdefTemplate(), "编辑器测试程序集定义文件", false);
            AddFile("Tests/Runtime/[PACKAGE_NAME].Runtime.Tests.asmdef", GetRuntimeTestsAsmdefTemplate(), "运行时测试程序集定义文件", false);

            // 添加基础包特有的选项
            AddOption("includeEditor", "包含编辑器模块", "是否包含编辑器特定代码", TemplateOptionType.Boolean, "true");
            AddOption("includeTests", "包含测试", "是否包含测试代码", TemplateOptionType.Boolean, "true");
            AddOption("includeSamples", "包含示例", "是否包含示例代码", TemplateOptionType.Boolean, "false");
        }

        /// <summary>
        /// 获取Runtime程序集定义模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetRuntimeAsmdefTemplate()
        {
            return @"{
    ""name"": ""#PACKAGE_NAME#.Runtime"",
    ""rootNamespace"": ""#ROOT_NAMESPACE#"",
    ""references"": [
        #RUNTIME_REFERENCES#
    ],
    ""includePlatforms"": [],
    ""excludePlatforms"": [],
    ""allowUnsafeCode"": false,
    ""overrideReferences"": false,
    ""precompiledReferences"": [],
    ""autoReferenced"": true,
    ""defineConstraints"": [],
    ""versionDefines"": [],
    ""noEngineReferences"": false
}";
        }

        /// <summary>
        /// 获取Editor程序集定义模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetEditorAsmdefTemplate()
        {
            return @"{
    ""name"": ""#PACKAGE_NAME#.Editor"",
    ""rootNamespace"": ""#ROOT_NAMESPACE#"",
    ""references"": [
        ""#PACKAGE_NAME#.Runtime""#EDITOR_REFERENCES#
    ],
    ""includePlatforms"": [
        ""Editor""
    ],
    ""excludePlatforms"": [],
    ""allowUnsafeCode"": false,
    ""overrideReferences"": false,
    ""precompiledReferences"": [],
    ""autoReferenced"": true,
    ""defineConstraints"": [],
    ""versionDefines"": [],
    ""noEngineReferences"": false
}";
        }

        /// <summary>
        /// 获取编辑器测试程序集定义模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetEditorTestsAsmdefTemplate()
        {
            return @"{
    ""name"": ""#PACKAGE_NAME#.Editor.Tests"",
    ""rootNamespace"": ""#ROOT_NAMESPACE#"",
    ""references"": [
        ""#PACKAGE_NAME#.Runtime"",
        ""#PACKAGE_NAME#.Editor"",
        ""UnityEngine.TestRunner"",
        ""UnityEditor.TestRunner""
    ],
    ""includePlatforms"": [
        ""Editor""
    ],
    ""excludePlatforms"": [],
    ""allowUnsafeCode"": false,
    ""overrideReferences"": true,
    ""precompiledReferences"": [
        ""nunit.framework.dll""
    ],
    ""autoReferenced"": false,
    ""defineConstraints"": [
        ""UNITY_INCLUDE_TESTS""
    ],
    ""versionDefines"": [],
    ""noEngineReferences"": false
}";
        }

        /// <summary>
        /// 获取运行时测试程序集定义模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetRuntimeTestsAsmdefTemplate()
        {
            return @"{
    ""name"": ""#PACKAGE_NAME#.Runtime.Tests"",
    ""rootNamespace"": ""#ROOT_NAMESPACE#"",
    ""references"": [
        ""#PACKAGE_NAME#.Runtime"",
        ""UnityEngine.TestRunner"",
        ""UnityEditor.TestRunner""
    ],
    ""includePlatforms"": [],
    ""excludePlatforms"": [],
    ""allowUnsafeCode"": false,
    ""overrideReferences"": true,
    ""precompiledReferences"": [
        ""nunit.framework.dll""
    ],
    ""autoReferenced"": false,
    ""defineConstraints"": [
        ""UNITY_INCLUDE_TESTS""
    ],
    ""versionDefines"": [],
    ""noEngineReferences"": false
}";
        }

        /// <summary>
        /// 获取模板预览信息
        /// </summary>
        /// <returns>预览信息</returns>
        public override TemplatePreviewInfo GetPreviewInfo()
        {
            var previewInfo = base.GetPreviewInfo();

            // 添加基础包特有的特点
            previewInfo.AddFeature("标准的Runtime/Editor分离结构");
            previewInfo.AddFeature("预配置的Assembly Definition文件");
            previewInfo.AddFeature("完整的测试框架支持");

            return previewInfo;
        }

        /// <summary>
        /// 验证包配置
        /// </summary>
        /// <param name="config">包配置</param>
        /// <returns>验证结果</returns>
        public override ValidationResult ValidateConfig(PackageConfig config)
        {
            var result = base.ValidateConfig(config);

            // 基础包特有的验证逻辑
            if (string.IsNullOrEmpty(config.RootNamespace))
            {
                result.AddWarning("建议设置根命名空间，以便更好地组织代码");
            }

            return result;
        }
    }
}
