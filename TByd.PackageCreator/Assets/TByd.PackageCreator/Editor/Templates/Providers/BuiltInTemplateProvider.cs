using System;
using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Templates.Providers
{
    /// <summary>
    /// 内置模板提供者，提供包含在包中的默认模板
    /// </summary>
    public class BuiltInTemplateProvider : ITemplateProvider
    {
        private readonly List<IPackageTemplate> _templates = new List<IPackageTemplate>();
        private readonly ErrorHandler _errorHandler;

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string ProviderName => "BuildInTemplateProvider";

        /// <summary>
        /// 提供者版本
        /// </summary>
        public Version ProviderVersion => new Version(1, 0, 0);

        /// <summary>
        /// 创建内置模板提供者
        /// </summary>
        public BuiltInTemplateProvider()
        {
            _errorHandler = ErrorHandler.Instance;
            LoadBuiltInTemplates();
        }

        /// <summary>
        /// 获取此提供者提供的所有模板
        /// </summary>
        /// <returns>模板集合</returns>
        public IEnumerable<IPackageTemplate> GetTemplates()
        {
            return _templates.AsReadOnly();
        }

        /// <summary>
        /// 加载内置模板
        /// </summary>
        private void LoadBuiltInTemplates()
        {
            try
            {
                // 目前先预留内置模板加载逻辑
                // 未来这里将从DefaultTemplates目录加载内置模板资源
                _errorHandler.LogInfo("内置模板加载功能尚未完全实现");

                // 添加一个示例模板用于测试
                AddSampleTemplate();
            }
            catch (Exception ex)
            {
                _errorHandler.LogException(ErrorType.OperationFailed, ex, "加载内置模板时出错");
            }
        }

        /// <summary>
        /// 添加一个示例模板用于测试
        /// </summary>
        private void AddSampleTemplate()
        {
            // 这里仅用于测试，演示内置模板
            var basicTemplate = new SampleBasicTemplate();
            _templates.Add(basicTemplate);
        }
    }

    /// <summary>
    /// 示例基础模板，仅用于测试
    /// </summary>
    internal class SampleBasicTemplate : IPackageTemplate
    {
        public string Id => "com.tbyd.sample.basic";
        public string Name => "基础包示例模板";
        public string Description => "一个基础的Unity包模板示例，用于测试模板管理系统";
        public string Version => "1.0.0";
        public string Author => "TByd";
        public Texture2D Icon => null; // 实际使用时应加载图标资源

        public IReadOnlyList<TemplateDirectory> Directories => new List<TemplateDirectory>
        {
            new TemplateDirectory("Runtime", "运行时代码目录"),
            new TemplateDirectory("Editor", "编辑器代码目录"),
            new TemplateDirectory("Tests", "测试代码目录"),
            new TemplateDirectory("Documentation~", "文档目录")
        }.AsReadOnly();

        public IReadOnlyList<TemplateFile> Files => new List<TemplateFile>
        {
            new TemplateFile("package.json",
                @"{
  ""name"": ""#PACKAGE_NAME#"",
  ""version"": ""#PACKAGE_VERSION#"",
  ""displayName"": ""#DISPLAY_NAME#"",
  ""description"": ""#DESCRIPTION#"",
  ""unity"": ""2021.3"",
  ""author"": {
    ""name"": ""#AUTHOR_NAME#"",
    ""email"": ""#AUTHOR_EMAIL#"",
    ""url"": ""#AUTHOR_URL#""
  }
}", "包配置文件"),
            new TemplateFile("README.md",
                @"# #DISPLAY_NAME#

## 描述
#DESCRIPTION#

## 安装
可通过Unity包管理器安装此包。

## 使用方法
待补充

## 许可
#LICENSE#
", "自述文件"),
            new TemplateFile("CHANGELOG.md",
                @"# 更新日志

## [#PACKAGE_VERSION#] - #CURRENT_DATE#
### 初始版本
- 初始功能实现
", "更新日志")
        }.AsReadOnly();

        public IReadOnlyList<TemplateOption> Options => new List<TemplateOption>
        {
            new TemplateOption("includeTests", "包含测试", "是否包含测试代码", TemplateOptionType.Boolean, "true"),
            new TemplateOption("licenseType", "许可类型", "包使用的许可类型", TemplateOptionType.Enum, "MIT")
            {
                PossibleValues = new List<string> { "MIT", "Apache-2.0", "GPL-3.0", "Custom" }
            }
        }.AsReadOnly();

        public ValidationResult ValidateConfig(PackageConfig config)
        {
            // 简单验证示例
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(config.Name))
            {
                result.AddError("包名称不能为空");
            }

            if (string.IsNullOrEmpty(config.Version))
            {
                result.AddError("包版本不能为空");
            }

            return result;
        }

        public bool Generate(PackageConfig config, string targetPath)
        {
            // 实际生成逻辑将在FileGenerator中实现
            // 这里仅返回默认结果
            return true;
        }

        public TemplatePreviewInfo GetPreviewInfo()
        {
            var previewInfo = new TemplatePreviewInfo("基础Unity包", "一个基础的Unity包模板示例，提供标准的包结构");
            previewInfo.AddFeature("标准的包结构布局");
            previewInfo.AddFeature("基础的文档模板");
            previewInfo.AddFeature("符合Unity包规范的配置");
            return previewInfo;
        }
    }
}
