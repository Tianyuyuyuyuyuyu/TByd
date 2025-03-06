using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Templates.Implementations
{
    /// <summary>
    /// 包模板基类，实现了一些通用功能
    /// </summary>
    public abstract class BasePackageTemplate : IPackageTemplate
    {
        private readonly ErrorHandler _errorHandler;
        private readonly List<TemplateDirectory> _directories = new List<TemplateDirectory>();
        private readonly List<TemplateFile> _files = new List<TemplateFile>();
        private readonly List<TemplateOption> _options = new List<TemplateOption>();
        private Texture2D _icon;

        /// <summary>
        /// 模板唯一标识符
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 模板版本
        /// </summary>
        public virtual string Version => "1.0.0";

        /// <summary>
        /// 模板作者
        /// </summary>
        public virtual string Author => "TByd";

        /// <summary>
        /// 模板图标
        /// </summary>
        public virtual Texture2D Icon => _icon;

        /// <summary>
        /// 模板目录结构
        /// </summary>
        public IReadOnlyList<TemplateDirectory> Directories => _directories.AsReadOnly();

        /// <summary>
        /// 模板文件
        /// </summary>
        public IReadOnlyList<TemplateFile> Files => _files.AsReadOnly();

        /// <summary>
        /// 模板选项
        /// </summary>
        public IReadOnlyList<TemplateOption> Options => _options.AsReadOnly();

        /// <summary>
        /// 初始化模板基类
        /// </summary>
        protected BasePackageTemplate()
        {
            _errorHandler = ErrorHandler.Instance;
            InitializeTemplate();
        }

        /// <summary>
        /// 初始化模板，子类应该重写此方法并调用父类方法
        /// </summary>
        protected virtual void InitializeTemplate()
        {
            // 加载模板图标
            LoadIcon();

            // 初始化基础目录和文件
            InitializeBaseDirectories();
            InitializeBaseFiles();
            InitializeBaseOptions();
        }

        /// <summary>
        /// 加载模板图标
        /// </summary>
        protected virtual void LoadIcon()
        {
            try
            {
                // 图标路径约定为"Editor/Templates/Icons/{模板ID的最后一部分}.png"
                string iconName = Id.Split('.').Last();
                string iconPath = $"Packages/com.tbyd.packagecreator/Editor/Templates/Icons/{iconName}.png";
                _icon = Resources.Load<Texture2D>(iconPath);

                // 如果找不到特定图标，加载默认图标
                if (_icon == null)
                {
                    _icon = Resources.Load<Texture2D>("Packages/com.tbyd.packagecreator/Editor/Templates/Icons/default.png");
                }
            }
            catch (Exception ex)
            {
                _errorHandler.LogWarning(ErrorType.ResourceNotFound, $"加载模板图标时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化基础目录结构
        /// </summary>
        protected virtual void InitializeBaseDirectories()
        {
            AddDirectory("Documentation~", "文档目录", false);
        }

        /// <summary>
        /// 初始化基础文件
        /// </summary>
        protected virtual void InitializeBaseFiles()
        {
            AddFile("package.json", GetPackageJsonTemplate(), "包配置文件");
            AddFile("README.md", GetReadmeTemplate(), "自述文件");
            AddFile("CHANGELOG.md", GetChangelogTemplate(), "更新日志");
            AddFile("LICENSE.md", GetLicenseTemplate(), "许可证文件");
        }

        /// <summary>
        /// 初始化基础选项
        /// </summary>
        protected virtual void InitializeBaseOptions()
        {
            AddOption(
                "licenseType",
                "许可类型",
                "包使用的许可类型",
                TemplateOptionType.Enum,
                "MIT"
            ).PossibleValues = new List<string> { "MIT", "Apache-2.0", "GPL-3.0", "Custom" };
        }

        /// <summary>
        /// 添加目录
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="description">描述</param>
        /// <param name="isRequired">是否必需</param>
        /// <returns>添加的目录</returns>
        protected TemplateDirectory AddDirectory(string relativePath, string description = "", bool isRequired = true)
        {
            var directory = new TemplateDirectory(relativePath, description, isRequired);
            _directories.Add(directory);
            return directory;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="contentTemplate">内容模板</param>
        /// <param name="description">描述</param>
        /// <param name="isRequired">是否必需</param>
        /// <param name="supportsVariableReplacement">是否支持变量替换</param>
        /// <returns>添加的文件</returns>
        protected TemplateFile AddFile(string relativePath, string contentTemplate, string description = "",
            bool isRequired = true, bool supportsVariableReplacement = true)
        {
            var file = new TemplateFile(relativePath, contentTemplate, description, isRequired, supportsVariableReplacement);
            _files.Add(file);
            return file;
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="key">选项键</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="description">描述</param>
        /// <param name="optionType">选项类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="isRequired">是否必需</param>
        /// <returns>添加的选项</returns>
        protected TemplateOption AddOption(string key, string displayName, string description,
            TemplateOptionType optionType, string defaultValue = "", bool isRequired = true)
        {
            var option = new TemplateOption(key, displayName, description, optionType, defaultValue, isRequired);
            _options.Add(option);
            return option;
        }

        /// <summary>
        /// 移除选项
        /// </summary>
        /// <param name="key">选项键</param>
        /// <returns>是否成功移除</returns>
        protected bool RemoveOption(string key)
        {
            int index = _options.FindIndex(o => o.Key == key);
            if (index >= 0)
            {
                _options.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取package.json模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetPackageJsonTemplate()
        {
            return @"{
  ""name"": ""#PACKAGE_NAME#"",
  ""version"": ""#PACKAGE_VERSION#"",
  ""displayName"": ""#DISPLAY_NAME#"",
  ""description"": ""#DESCRIPTION#"",
  ""unity"": ""2021.3"",
  ""documentationUrl"": ""#DOCUMENTATION_URL#"",
  ""changelogUrl"": ""#CHANGELOG_URL#"",
  ""licensesUrl"": ""#LICENSE_URL#"",
  ""keywords"": [
    #KEYWORDS#
  ],
  ""author"": {
    ""name"": ""#AUTHOR_NAME#"",
    ""email"": ""#AUTHOR_EMAIL#"",
    ""url"": ""#AUTHOR_URL#""
  },
  ""dependencies"": {
    #DEPENDENCIES#
  }
}";
        }

        /// <summary>
        /// 获取README.md模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetReadmeTemplate()
        {
            return @"# #DISPLAY_NAME#

## 描述
#DESCRIPTION#

## 安装
可通过Unity包管理器安装此包。

## 使用方法
待补充

## 许可
#LICENSE#";
        }

        /// <summary>
        /// 获取CHANGELOG.md模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetChangelogTemplate()
        {
            return @"# 更新日志

## [#PACKAGE_VERSION#] - #CURRENT_DATE#
### 初始版本
- 初始功能实现

[未发布]: #REPOSITORY_URL#/compare/v#PACKAGE_VERSION#...HEAD
[#PACKAGE_VERSION#]: #REPOSITORY_URL#/releases/tag/v#PACKAGE_VERSION#";
        }

        /// <summary>
        /// 获取LICENSE.md模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetLicenseTemplate()
        {
            return @"# 许可证

## #LICENSE_TYPE#

Copyright (c) #CURRENT_YEAR# #AUTHOR_NAME#

#LICENSE_CONTENT#";
        }

        /// <summary>
        /// 验证包配置是否符合模板要求
        /// </summary>
        /// <param name="config">包配置</param>
        /// <returns>验证结果</returns>
        public virtual ValidationResult ValidateConfig(PackageConfig config)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(config.Name))
            {
                result.AddError("包名称不能为空");
            }
            else if (!config.Name.Contains("."))
            {
                result.AddWarning("包名称建议使用反向域名格式，如 'com.company.package'");
            }

            if (string.IsNullOrEmpty(config.Version))
            {
                result.AddError("包版本不能为空");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(config.Version, @"^\d+\.\d+\.\d+(-preview(\.\d+)?)?$"))
            {
                result.AddWarning("包版本应该符合语义化版本格式，如 '1.0.0' 或 '1.0.0-preview'");
            }

            if (string.IsNullOrEmpty(config.DisplayName))
            {
                result.AddWarning("包显示名称建议不要为空");
            }

            if (string.IsNullOrEmpty(config.Description))
            {
                result.AddWarning("包描述建议不要为空");
            }

            return result;
        }

        /// <summary>
        /// 根据配置生成包结构
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Generate(PackageConfig config, string targetPath)
        {
            // 创建文件生成器
            var fileGenerator = new FileGenerator();

            // 注册标准策略
            fileGenerator.RegisterStrategy(new JsonFileGenerationStrategy());
            fileGenerator.RegisterStrategy(new CSharpFileGenerationStrategy());

            // 执行异步生成并等待结果
            var task = GenerateAsync(config, targetPath, fileGenerator);
            task.Wait();

            // 返回生成结果
            return task.Result.IsValid;
        }

        /// <summary>
        /// 根据配置异步生成包结构
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="fileGenerator">文件生成器</param>
        /// <returns>生成结果</returns>
        public virtual async Task<ValidationResult> GenerateAsync(PackageConfig config, string targetPath, FileGenerator fileGenerator = null)
        {
            // 创建默认文件生成器（如果未提供）
            if (fileGenerator == null)
            {
                fileGenerator = new FileGenerator();
                fileGenerator.RegisterStrategy(new JsonFileGenerationStrategy());
                fileGenerator.RegisterStrategy(new CSharpFileGenerationStrategy());
            }

            // 验证配置
            var validationResult = ValidateConfig(config);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            // 使用文件生成器生成目录和文件
            return await fileGenerator.GenerateAsync(this, config, targetPath);
        }

        /// <summary>
        /// 获取模板预览信息
        /// </summary>
        /// <returns>预览信息</returns>
        public virtual TemplatePreviewInfo GetPreviewInfo()
        {
            var previewInfo = new TemplatePreviewInfo(Name, Description);

            // 添加一些基本特点
            previewInfo.AddFeature("符合Unity包规范的结构");
            previewInfo.AddFeature("完整的文档模板");
            previewInfo.AddFeature("基本的许可证支持");

            return previewInfo;
        }
    }
}
