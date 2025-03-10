using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Editor.Templates.Implementations
{
    /// <summary>
    /// 运行时库模板，专为Unity运行时功能库设计
    /// </summary>
    public class RuntimeLibraryTemplate : BasicPackageTemplate
    {
        /// <summary>
        /// 模板唯一标识符
        /// </summary>
        public override string Id => "com.tbyd.packagecreator.template.runtimelibrary";

        /// <summary>
        /// 模板名称
        /// </summary>
        public override string Name => "运行时库包";

        /// <summary>
        /// 模板描述
        /// </summary>
        public override string Description => "专为Unity运行时功能库设计的包模板，提供运行时功能开发的基础结构";

        /// <summary>
        /// 模板分类
        /// </summary>
        public override string Category => "运行时";

        /// <summary>
        /// 初始化模板
        /// </summary>
        protected override void InitializeTemplate()
        {
            base.InitializeTemplate();

            // 添加运行时库特有的目录
            AddDirectory("Runtime/Scripts", "运行时脚本目录");
            AddDirectory("Runtime/Prefabs", "预制体目录", false);
            AddDirectory("Runtime/Materials", "材质目录", false);
            AddDirectory("Runtime/Textures", "纹理目录", false);
            AddDirectory("Runtime/Shaders", "着色器目录", false);
            AddDirectory("Runtime/Animations", "动画目录", false);

            // 添加运行时库特有的文件
            AddFile("Runtime/Scripts/Example.cs", GetExampleClassTemplate(), "示例类", false);

            // 添加运行时库特有的选项
            AddOption("includeScriptableObjects", "包含ScriptableObject", "是否包含ScriptableObject资源", TemplateOptionType.Boolean, "false");
            AddOption("includeShaders", "包含着色器", "是否包含自定义着色器", TemplateOptionType.Boolean, "false");
            AddOption("libraryType", "库类型", "库的主要功能类型", TemplateOptionType.Enum, "Utility").PossibleValues =
                new List<string> { "Utility", "Gameplay", "Graphics", "Audio", "AI", "Physics", "Networking", "Other" };
            AddOption("targetPlatforms", "目标平台", "库支持的主要平台", TemplateOptionType.Enum, "All").PossibleValues =
                new List<string> { "All", "Mobile", "Desktop", "Console", "WebGL", "VR/AR" };
        }

        /// <summary>
        /// 获取示例类模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetExampleClassTemplate()
        {
            return @"using UnityEngine;

namespace #ROOT_NAMESPACE#
{
    /// <summary>
    /// 示例类，展示基本功能
    /// </summary>
    public class Example : MonoBehaviour
    {
        [SerializeField] private string _exampleString = ""Hello World"";
        [SerializeField] private int _exampleInt = 42;
        [SerializeField] private bool _exampleBool = true;

        /// <summary>
        /// 示例公共属性
        /// </summary>
        public string ExampleProperty { get; set; } = ""Example Property"";

        private void Awake()
        {
            Debug.Log($""[{GetType().Name}] Initialized with: {_exampleString}, {_exampleInt}, {_exampleBool}"");
        }

        /// <summary>
        /// 示例公共方法
        /// </summary>
        /// <param name=""message"">消息内容</param>
        /// <returns>格式化后的消息</returns>
        public string ExampleMethod(string message)
        {
            return $""[{GetType().Name}] {message}"";
        }
    }
}";
        }

        /// <summary>
        /// 获取模板预览信息
        /// </summary>
        /// <returns>预览信息</returns>
        public override TemplatePreviewInfo GetPreviewInfo()
        {
            var previewInfo = base.GetPreviewInfo();

            // 添加运行时库特有的特点
            previewInfo.AddFeature("专为运行时功能设计的目录结构");
            previewInfo.AddFeature("包含资源目录（预制体、材质、纹理等）");
            previewInfo.AddFeature("提供示例脚本和基础功能");

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

            // 运行时库特有的验证逻辑
            if (string.IsNullOrEmpty(config.MinUnityVersion))
            {
                result.AddWarning("建议指定最低Unity版本要求，以确保兼容性");
            }

            return result;
        }

        /// <summary>
        /// 获取package.json模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected override string GetPackageJsonTemplate()
        {
            // 扩展基础模板，添加运行时库特有的字段
            var baseTemplate = base.GetPackageJsonTemplate();

            // 在运行时库中，我们可能需要添加额外的字段，如samples配置
            // 这里只是示例，实际实现可能需要更复杂的JSON处理
            return baseTemplate;
        }
    }
}
