using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Templates.Implementations;

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
                    category = template.Category,
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
                SErrorHandler.LogInfo($"开始反序列化JSON模板，内容预览: {(json.Length > 100 ? json.Substring(0, 100) + "..." : json)}");

                var jsonTemplate = JsonConvert.DeserializeObject<JsonTemplateData>(json);
                if (jsonTemplate == null)
                {
                    SErrorHandler.LogError(ErrorType.InvalidData, "JSON解析失败");
                    return null;
                }

                SErrorHandler.LogInfo($"JSON模板反序列化成功，ID: {jsonTemplate.id}, 名称: {jsonTemplate.name}, 分类: {jsonTemplate.category}");
                SErrorHandler.LogInfo($"目录数量: {(jsonTemplate.directories != null ? jsonTemplate.directories.Length : 0)}, 文件数量: {(jsonTemplate.files != null ? jsonTemplate.files.Length : 0)}");

                var template = new JsonPackageTemplate(jsonTemplate);
                SErrorHandler.LogInfo($"已创建模板对象，ID: {template.Id}, 名称: {template.Name}, 分类: {template.Category}");

                return template;
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
        [JsonProperty("id")]
        public string id;

        [JsonProperty("name")]
        public string name;

        [JsonProperty("description")]
        public string description;

        [JsonProperty("version")]
        public string version;

        [JsonProperty("author")]
        public string author;

        [JsonProperty("category")]
        public string category;

        [JsonProperty("iconPath")]
        public string iconPath;

        [JsonProperty("directories")]
        public TemplateDirectory[] directories;

        [JsonProperty("files")]
        public TemplateFile[] files;

        [JsonProperty("options")]
        public TemplateOption[] options;
    }
}
