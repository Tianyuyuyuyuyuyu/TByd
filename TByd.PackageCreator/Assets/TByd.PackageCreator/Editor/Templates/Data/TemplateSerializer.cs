using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace TByd.PackageCreator.Editor.Templates.Data
{
    /// <summary>
    /// 模板序列化器，用于将模板序列化为JSON或从JSON反序列化
    /// </summary>
    public static class TemplateSerializer
    {
        private static readonly ErrorHandler SErrorHandler = ErrorHandler.Instance;

        /// <summary>
        /// 将模板序列化为JSON字符串
        /// </summary>
        /// <param name="template">要序列化的模板</param>
        /// <returns>JSON字符串，失败时返回null</returns>
        public static string SerializeToJson(IPackageTemplate template)
        {
            if (template == null)
            {
                SErrorHandler.LogError(ErrorType.InvalidArgument, "不能序列化空模板");
                return null;
            }

            try
            {
                var jsonTemplate = new JsonTemplateData
                {
                    id = template.Id,
                    name = template.Name,
                    description = template.Description,
                    version = template.Version,
                    author = template.Author,
                    directories = template.Directories.ToArray(),
                    files = template.Files.ToArray(),
                    options = template.Options.ToArray()
                };

                return JsonConvert.SerializeObject(jsonTemplate, Formatting.Indented);
            }
            catch (Exception ex)
            {
                SErrorHandler.LogException(ErrorType.OperationFailed, ex, $"序列化模板 {template.Id} 时出错");
                return null;
            }
        }

        /// <summary>
        /// 从JSON字符串反序列化模板
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>反序列化的模板，失败时返回null</returns>
        public static IPackageTemplate DeserializeFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                SErrorHandler.LogError(ErrorType.InvalidArgument, "不能从空JSON字符串反序列化模板");
                return null;
            }

            try
            {
                var jsonTemplate = JsonConvert.DeserializeObject<JsonTemplateData>(json);
                if (jsonTemplate == null)
                {
                    SErrorHandler.LogError(ErrorType.InvalidData, "JSON解析失败");
                    return null;
                }

                return new JsonPackageTemplate(jsonTemplate);
            }
            catch (Exception ex)
            {
                SErrorHandler.LogException(ErrorType.OperationFailed, ex, "从JSON反序列化模板时出错");
                return null;
            }
        }

        /// <summary>
        /// 从JSON文件反序列化模板
        /// </summary>
        /// <param name="filePath">JSON文件路径</param>
        /// <returns>反序列化的模板，失败时返回null</returns>
        public static IPackageTemplate DeserializeFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                SErrorHandler.LogError(ErrorType.InvalidArgument, "文件路径不能为空");
                return null;
            }

            if (!File.Exists(filePath))
            {
                SErrorHandler.LogError(ErrorType.FileNotFound, $"未找到文件: {filePath}");
                return null;
            }

            try
            {
                var json = File.ReadAllText(filePath);
                return DeserializeFromJson(json);
            }
            catch (Exception ex)
            {
                SErrorHandler.LogException(ErrorType.FileReadError, ex, $"读取模板文件时出错: {filePath}");
                return null;
            }
        }
    }

    /// <summary>
    /// 用于JSON序列化的模板数据结构
    /// </summary>
    [Serializable]
    public class JsonTemplateData
    {
        [FormerlySerializedAs("Id")] [JsonProperty("id")]
        public string id;

        [FormerlySerializedAs("Name")] [JsonProperty("name")]
        public string name;

        [FormerlySerializedAs("Description")] [JsonProperty("description")]
        public string description;

        [FormerlySerializedAs("Version")] [JsonProperty("version")]
        public string version;

        [FormerlySerializedAs("Author")] [JsonProperty("author")]
        public string author;

        [FormerlySerializedAs("IconPath")] [JsonProperty("iconPath")]
        public string iconPath;

        [FormerlySerializedAs("Directories")] [JsonProperty("directories")]
        public TemplateDirectory[] directories;

        [FormerlySerializedAs("Files")] [JsonProperty("files")]
        public TemplateFile[] files;

        [FormerlySerializedAs("Options")] [JsonProperty("options")]
        public TemplateOption[] options;
    }

    /// <summary>
    /// 从JSON数据创建的包模板实现
    /// </summary>
    public class JsonPackageTemplate : IPackageTemplate
    {
        private readonly JsonTemplateData _mData;
        private Texture2D _mIcon;

        /// <summary>
        /// 创建从JSON数据创建的包模板
        /// </summary>
        /// <param name="data">JSON模板数据</param>
        public JsonPackageTemplate(JsonTemplateData data)
        {
            _mData = data ?? throw new ArgumentNullException(nameof(data));
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
                }
                // 未来可以添加从文件加载图标的逻辑
            }
            catch (Exception ex)
            {
                Debug.LogError($"加载模板图标时出错: {ex.Message}");
            }
        }

        public string Id => _mData.id;
        public string Name => _mData.name;
        public string Description => _mData.description;
        public string Version => _mData.version;
        public string Author => _mData.author;
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
        /// 根据配置生成包结构
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

        public TemplatePreviewInfo GetPreviewInfo()
        {
            return new TemplatePreviewInfo(Name, Description);
        }
    }
}
