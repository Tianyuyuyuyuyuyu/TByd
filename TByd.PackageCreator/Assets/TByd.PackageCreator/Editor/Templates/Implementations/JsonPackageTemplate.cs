using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.Templates.Data;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Templates.Implementations
{
    /// <summary>
    /// 从JSON数据创建的包模板实现
    /// 注意：与其他预定义模板不同，此类是一个通用的JSON数据驱动实现，
    /// 用于从JSON文件动态加载模板定义，而非特定类型的预定义模板。
    /// </summary>
    public class JsonPackageTemplate : IPackageTemplate
    {
        private readonly JsonTemplateData _mData;
        private Texture2D _mIcon;
        private readonly ErrorHandler _mErrorHandler;

        /// <summary>
        /// 创建从JSON数据创建的包模板
        /// </summary>
        /// <param name="data">JSON模板数据</param>
        public JsonPackageTemplate(JsonTemplateData data)
        {
            _mData = data ?? throw new ArgumentNullException(nameof(data));
            _mErrorHandler = ErrorHandler.Instance;

            _mErrorHandler.LogInfo($"创建JsonPackageTemplate: ID={_mData.id}, Name={_mData.name}, Category={_mData.category}");

            LoadIcon();
        }

        /// <summary>
        /// 加载图标资源
        /// </summary>
        private void LoadIcon()
        {
            if (string.IsNullOrEmpty(_mData.iconPath)) return;

            try
            {
                // 如果是资源路径，尝试加载
                if (_mData.iconPath.StartsWith("Assets/"))
                {
                    _mIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(_mData.iconPath);
                    if (_mIcon != null)
                    {
                        _mErrorHandler.LogInfo($"已加载模板图标: {_mData.iconPath}");
                    }
                    else
                    {
                        _mErrorHandler.LogWarning(ErrorType.ResourceNotFound, $"未能加载模板图标: {_mData.iconPath}");
                    }
                }
                // 未来可以添加从文件加载图标的逻辑
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.ResourceNotFound, ex, $"加载模板图标时出错: {_mData.iconPath}");
            }
        }

        public string Id => _mData.id;
        public string Name => _mData.name;
        public string Description => _mData.description;
        public string Version => _mData.version;
        public string Author => _mData.author;
        public string Category => string.IsNullOrEmpty(_mData.category) ? "自定义" : _mData.category;
        public Texture2D Icon => _mIcon;

        public IReadOnlyList<TemplateDirectory> Directories =>
            _mData.directories != null ? Array.AsReadOnly(_mData.directories) : Array.Empty<TemplateDirectory>();

        public IReadOnlyList<TemplateFile> Files =>
            _mData.files != null ? Array.AsReadOnly(_mData.files) : Array.Empty<TemplateFile>();

        public IReadOnlyList<TemplateOption> Options =>
            _mData.options != null ? Array.AsReadOnly(_mData.options) : Array.Empty<TemplateOption>();

        public ValidationResult ValidateConfig(PackageConfig config)
        {
            var result = new ValidationResult();

            // 基本验证逻辑
            if (string.IsNullOrEmpty(config.Name))
            {
                result.AddError("包名称不能为空");
            }

            if (string.IsNullOrEmpty(config.Version))
            {
                result.AddError("包版本不能为空");
            }

            // 未来可以添加更复杂的验证逻辑

            return result;
        }

        /// <summary>
        /// 根据配置生成包结构（同步方法）
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>操作是否成功</returns>
        public bool Generate(PackageConfig config, string targetPath)
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
        public async Task<ValidationResult> GenerateAsync(PackageConfig config, string targetPath, FileGenerator fileGenerator = null)
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
        public TemplatePreviewInfo GetPreviewInfo()
        {
            return new TemplatePreviewInfo(Name, Description);
        }

        public override string ToString()
        {
            return $"Template[{Id}]: {Name} ({Category})";
        }
    }
}
