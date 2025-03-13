using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Templates.Implementations
{
    /// <summary>
    /// 包模板基类，实现了一些通用功能
    /// </summary>
    public abstract class BasePackageTemplate : IPackageTemplate
    {
        private readonly ErrorHandler _mErrorHandler;
        private readonly List<TemplateDirectory> _mDirectories = new List<TemplateDirectory>();
        private readonly List<TemplateFile> _mFiles = new List<TemplateFile>();
        private readonly List<TemplateOption> _mOptions = new List<TemplateOption>();
        private Texture2D _mIcon;

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
        /// 模板分类
        /// </summary>
        public virtual string Category => "通用";

        /// <summary>
        /// 模板图标
        /// </summary>
        public virtual Texture2D Icon => _mIcon;

        /// <summary>
        /// 模板目录结构
        /// </summary>
        public IReadOnlyList<TemplateDirectory> Directories => _mDirectories.AsReadOnly();

        /// <summary>
        /// 模板文件
        /// </summary>
        public IReadOnlyList<TemplateFile> Files => _mFiles.AsReadOnly();

        /// <summary>
        /// 模板选项
        /// </summary>
        public IReadOnlyList<TemplateOption> Options => _mOptions.AsReadOnly();

        /// <summary>
        /// 初始化模板基类
        /// </summary>
        protected BasePackageTemplate()
        {
            _mErrorHandler = ErrorHandler.Instance;
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
                var iconName = Id.Split('.').Last();
                var iconPath = $"Packages/com.tbyd.packagecreator/Editor/Templates/Icons/{iconName}.png";
                _mIcon = Resources.Load<Texture2D>(iconPath);

                // 如果找不到特定图标，加载默认图标
                if (_mIcon == null)
                {
                    _mIcon = Resources.Load<Texture2D>("Packages/com.tbyd.packagecreator/Editor/Templates/Icons/default.png");
                }
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogWarning(ErrorType.ResourceNotFound, $"加载模板图标时出错: {ex.Message}");
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
            _mDirectories.Add(directory);
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
            _mFiles.Add(file);
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
            _mOptions.Add(option);
            return option;
        }

        /// <summary>
        /// 移除选项
        /// </summary>
        /// <param name="key">选项键</param>
        /// <returns>是否成功移除</returns>
        protected bool RemoveOption(string key)
        {
            var index = _mOptions.FindIndex(o => o.Key == key);
            if (index >= 0)
            {
                _mOptions.RemoveAt(index);
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
  ""name"": ""${PACKAGE_NAME}"",
  ""version"": ""${PACKAGE_VERSION}"",
  ""displayName"": ""${DISPLAY_NAME}"",
  ""description"": ""${DESCRIPTION}"",
  ""unity"": ""2021.3"",
  ""documentationUrl"": ""${DOCUMENTATION_URL}"",
  ""changelogUrl"": ""${CHANGELOG_URL}"",
  ""licensesUrl"": ""${LICENSE_URL}"",
  ""keywords"": [
    ""unity"",
    ""package""
  ],
  ""author"": {
    ""name"": ""${AUTHOR_NAME}"",
    ""email"": ""${AUTHOR_EMAIL}"",
    ""url"": ""${AUTHOR_URL}""
  },
  ""dependencies"": {
  }
}";
        }

        /// <summary>
        /// 获取README.md模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetReadmeTemplate()
        {
            return @"# ${DISPLAY_NAME}

## 描述
${DESCRIPTION}

## 安装
可通过Unity包管理器安装此包。

## 使用方法
待补充

## 许可
${LICENSE}";
        }

        /// <summary>
        /// 获取CHANGELOG.md模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetChangelogTemplate()
        {
            return @"# 更新日志

## [${PACKAGE_VERSION}] - ${CURRENT_DATE}
### 初始版本
- 初始功能实现

[未发布]: ${REPOSITORY_URL}/compare/v${PACKAGE_VERSION}...HEAD
[${PACKAGE_VERSION}]: ${REPOSITORY_URL}/releases/tag/v${PACKAGE_VERSION}";
        }

        /// <summary>
        /// 获取LICENSE.md模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetLicenseTemplate()
        {
            return @"# 许可证

## ${LICENSE_TYPE}

Copyright (c) ${CURRENT_YEAR} ${AUTHOR_NAME}

${LICENSE_CONTENT}";
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
            try
            {
                Debug.Log($"BasePackageTemplate.Generate: 开始生成包 {config.Name} 到 {targetPath}");

                // 创建文件生成器
                var jsonStrategy = new JsonFileGenerationStrategy();
                var csharpStrategy = new CSharpFileGenerationStrategy();
                // 创建不带默认策略的FileGenerator
                var fileGenerator = new FileGenerator(null, false);
                fileGenerator.RegisterStrategy(jsonStrategy);
                fileGenerator.RegisterStrategy(csharpStrategy);
                // 最后再注册默认策略
                fileGenerator.RegisterStrategy(new DefaultFileGenerationStrategy(false));

                // 验证配置
                var validationResult = ValidateConfig(config);
                if (!validationResult.IsValid)
                {
                    Debug.LogError($"BasePackageTemplate.Generate: 配置验证失败: {string.Join(", ", validationResult.GetMessages(ValidationMessageLevel.Error).Select(m => m.Message))}");
                    return false;
                }

                // 执行异步生成并等待结果
                Debug.Log("BasePackageTemplate.Generate: 开始执行GenerateAsync");

                // 不再使用同步等待方式，而是启动异步任务并立即返回
                var task = GenerateAsync(config, targetPath, fileGenerator);

                // 使用EditorApplication.update来监控任务进度
                int frameCount = 0;

                // 这里不等待任务完成，而是返回true表示任务已经启动
                // 任务的实际结果将通过UI状态管理器传递
                EditorApplication.delayCall += () =>
                {
                    MonitorGenerationTask(task, config.Name);
                };

                return true; // 返回true表示已启动生成任务，而不是表示生成成功
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError($"BasePackageTemplate.Generate: 生成过程中发生异常: {ex.Message}");

                // 展开AggregateException以获取更详细的错误信息
                if (ex is AggregateException aggregateEx)
                {
                    foreach (var innerEx in aggregateEx.InnerExceptions)
                    {
                        Debug.LogError($"内部异常: {innerEx.GetType().Name} - {innerEx.Message}");
                        Debug.LogError($"堆栈: {innerEx.StackTrace}");
                    }
                }

                Debug.LogError($"堆栈跟踪: {ex.StackTrace}");
                return false;
            }
        }

        // 新增监控任务的方法
        private void MonitorGenerationTask(Task<ValidationResult> task, string packageName)
        {
            if (task.IsCompleted)
            {
                try
                {
                    // 任务完成，处理结果
                    var result = task.Result;
                    if (result.IsValid)
                    {
                        Debug.Log($"包 {packageName} 生成成功");

                        // 获取当前目录作为包路径
                        string packagePath = null;
                        try
                        {
                            // 构建包路径
                            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                            packagePath = Path.GetFullPath(Path.Combine(projectRoot, packageName));
                            packagePath = packagePath.Replace("\\", "/");
                            Debug.Log($"包生成完成，最终路径: {packagePath}");
                        }
                        catch (Exception ex)
                        {
                            Debug.LogWarning($"构建包路径时出错：{ex.Message}，但不影响包生成");
                        }

                        // 确保UI状态更新了包路径和成功状态
                        UpdateProgressUI(1.0f, $"包 {packageName} 生成成功", true, packagePath);
                    }
                    else
                    {
                        Debug.LogError($"包 {packageName} 生成失败: {string.Join(", ", result.GetMessages(ValidationMessageLevel.Error).Select(m => m.Message))}");

                        // 更新UI状态为失败
                        UpdateProgressUI(1.0f, $"包 {packageName} 生成失败", false);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    Debug.LogError($"处理生成结果时发生异常: {ex.Message}");

                    // 更新UI状态为失败
                    UpdateProgressUI(1.0f, "生成过程发生异常", false);
                }
            }
            else
            {
                // 更新进度（模拟进度，真实进度很难精确计算）
                float progress = 0.1f;
                if (EditorApplication.timeSinceStartup % 3 < 1) progress = 0.3f;
                else if (EditorApplication.timeSinceStartup % 3 < 2) progress = 0.6f;
                else progress = 0.8f;

                // 更新UI状态进度
                UpdateProgressUI(progress, "正在生成包文件...", null);

                // 任务尚未完成，延迟继续检查
                EditorApplication.delayCall += () =>
                {
                    MonitorGenerationTask(task, packageName);
                };
            }
        }

        // 更新UI进度
        private void UpdateProgressUI(float progress, string message, bool? isSuccess, string packagePath = null)
        {
            if (UnityEditor.EditorWindow.HasOpenInstances<UnityEditor.EditorWindow>())
            {
                try
                {
                    // 获取UI状态管理器并更新进度
                    var uiStateManagerType = Type.GetType("TByd.PackageCreator.Editor.UI.Utils.UIStateManager, Assembly-CSharp-Editor");
                    if (uiStateManagerType != null)
                    {
                        var instanceProp = uiStateManagerType.GetProperty("Instance", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                        if (instanceProp != null)
                        {
                            var uiStateManager = instanceProp.GetValue(null);
                            var stateProperty = uiStateManagerType.GetProperty("CreationState");
                            if (stateProperty != null)
                            {
                                var state = stateProperty.GetValue(uiStateManager);
                                var creationProgressProp = state.GetType().GetProperty("CreationProgress");
                                if (creationProgressProp != null)
                                {
                                    creationProgressProp.SetValue(state, progress);

                                    // 如果提供了isSuccess值，也更新成功状态
                                    if (isSuccess.HasValue)
                                    {
                                        var isSuccessfulProp = state.GetType().GetProperty("IsCreationSuccessful");
                                        if (isSuccessfulProp != null)
                                        {
                                            isSuccessfulProp.SetValue(state, isSuccess.Value);
                                            Debug.Log($"UpdateProgressUI: 设置IsCreationSuccessful = {isSuccess.Value}");
                                        }

                                        var isCreatingProp = state.GetType().GetProperty("IsCreating");
                                        if (isCreatingProp != null)
                                        {
                                            isCreatingProp.SetValue(state, false); // 生成已完成，不再处于创建状态
                                            Debug.Log($"UpdateProgressUI: 设置IsCreating = false");
                                        }

                                        // 如果有路径信息，也更新路径
                                        if (!string.IsNullOrEmpty(packagePath))
                                        {
                                            var packagePathProp = state.GetType().GetProperty("PackagePath");
                                            if (packagePathProp != null)
                                            {
                                                packagePathProp.SetValue(state, packagePath);
                                                Debug.Log($"UpdateProgressUI: 设置PackagePath = {packagePath}");
                                            }
                                        }
                                        else
                                        {
                                            Debug.LogWarning("UpdateProgressUI: 未提供有效的packagePath");
                                        }

                                        // 如果失败了，也设置错误信息
                                        if (!isSuccess.Value)
                                        {
                                            var errorMessageProp = state.GetType().GetProperty("ErrorMessage");
                                            if (errorMessageProp != null)
                                            {
                                                errorMessageProp.SetValue(state, message);
                                                Debug.Log($"UpdateProgressUI: 设置ErrorMessage = {message}");
                                            }
                                        }

                                        // 创建一个结果对象
                                        var validationResultType = Type.GetType("TByd.PackageCreator.Editor.Core.Models.ValidationResult, Assembly-CSharp-Editor");
                                        if (validationResultType != null)
                                        {
                                            var validationResult = Activator.CreateInstance(validationResultType);

                                            // 如果失败，添加错误消息
                                            if (!isSuccess.Value && !string.IsNullOrEmpty(message))
                                            {
                                                var addErrorMethod = validationResultType.GetMethod("AddError", new[] { typeof(string) });
                                                if (addErrorMethod != null)
                                                {
                                                    addErrorMethod.Invoke(validationResult, new object[] { message });
                                                }
                                            }

                                            // 设置结果对象
                                            var creationResultProp = state.GetType().GetProperty("CreationResult");
                                            if (creationResultProp != null)
                                            {
                                                creationResultProp.SetValue(state, validationResult);
                                                Debug.Log("UpdateProgressUI: 设置CreationResult");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 如果反射失败，只记录日志但不中断流程
                    Debug.LogWarning($"无法更新UI进度: {ex.Message}");
                }
            }
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
                var jsonStrategy = new JsonFileGenerationStrategy();
                var csharpStrategy = new CSharpFileGenerationStrategy();
                // 创建不带默认策略的FileGenerator
                fileGenerator = new FileGenerator(null, false);
                fileGenerator.RegisterStrategy(jsonStrategy);
                fileGenerator.RegisterStrategy(csharpStrategy);
                // 最后再注册默认策略
                fileGenerator.RegisterStrategy(new DefaultFileGenerationStrategy(false));
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
