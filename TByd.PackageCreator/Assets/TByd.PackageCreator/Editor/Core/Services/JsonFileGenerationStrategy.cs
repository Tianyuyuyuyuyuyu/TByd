using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Utils.FileSystem;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Services
{
    /// <summary>
    /// JSON文件生成策略，专门用于生成和格式化JSON文件
    /// </summary>
    public class JsonFileGenerationStrategy : IFileGenerationStrategy
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        public string StrategyName => "JSON文件生成策略";

        /// <summary>
        /// 支持的文件扩展名，JSON相关文件
        /// </summary>
        public string[] SupportedFileExtensions => new[] { ".json", ".jsonc", ".json5" };

        // 变量替换处理器（委托给FileGenerator处理）
        private readonly FileGenerator _mVariableProcessor;

        /// <summary>
        /// 创建JSON文件生成策略
        /// </summary>
        public JsonFileGenerationStrategy()
        {
            _mVariableProcessor = new FileGenerator();
        }

        /// <summary>
        /// 检查此策略是否支持指定的文件类型
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>是否支持</returns>
        public bool SupportsFileType(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return false;

            // 转为小写进行比较
            var lowerExtension = fileExtension.ToLowerInvariant();
            return SupportedFileExtensions.Contains(lowerExtension);
        }

        /// <summary>
        /// 生成文件内容
        /// </summary>
        /// <param name="templateFile">模板文件定义</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>生成结果</returns>
        public async Task<ValidationResult> GenerateFileAsync(TemplateFile templateFile, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();

            try
            {
                // 获取文件内容
                var fileContent = templateFile.ContentTemplate;

                // 如果文件支持变量替换，则进行替换
                if (templateFile.SupportsVariableReplacement)
                {
                    fileContent = _mVariableProcessor.ReplaceVariables(fileContent, config);
                }

                // 尝试格式化JSON
                var formattedJson = FormatJson(fileContent, out var isValidJson);

                // 如果是有效的JSON，使用格式化后的内容
                if (isValidJson)
                {
                    fileContent = formattedJson;
                }
                else
                {
                    // 如果不是有效的JSON，记录警告但仍然使用原始内容
                    result.AddWarning($"JSON格式化失败，将使用原始内容: {Path.GetFileName(targetPath)}");
                }

                // 确保目标目录存在
                var directoryPath = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    FileUtils.EnsureDirectoryExists(directoryPath);
                }

                // 安全写入文件
                var writeSuccess = await WriteFileAsync(targetPath, fileContent);

                if (writeSuccess)
                {
                    result.AddInfo($"成功创建JSON文件: {Path.GetFileName(targetPath)}");
                }
                else
                {
                    result.AddError($"创建JSON文件失败: {Path.GetFileName(targetPath)}");
                }
            }
            catch (Exception ex)
            {
                result.AddError($"生成JSON文件时发生异常: {ex.Message}");
                Debug.LogException(ex);
            }

            return result;
        }

        /// <summary>
        /// 格式化JSON字符串
        /// </summary>
        /// <param name="jsonContent">JSON内容</param>
        /// <param name="isValidJson">是否为有效的JSON</param>
        /// <returns>格式化后的JSON</returns>
        private string FormatJson(string jsonContent, out bool isValidJson)
        {
            if (string.IsNullOrEmpty(jsonContent))
            {
                isValidJson = false;
                return jsonContent;
            }

            try
            {
                // 先尝试解析为JSON对象
                var jsonObject = JsonConvert.DeserializeObject(jsonContent);

                // 然后重新序列化为漂亮的JSON格式
                var formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

                isValidJson = true;
                return formattedJson;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"JSON格式化失败: {ex.Message}");
                isValidJson = false;
                return jsonContent;
            }
        }

        /// <summary>
        /// 异步写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <returns>是否成功</returns>
        private async Task<bool> WriteFileAsync(string filePath, string content)
        {
            try
            {
                // 首先创建备份（如果文件已存在）
                if (File.Exists(filePath))
                {
                    SecureFileOperations.CreateBackup(filePath);
                }

                // 使用异步文件操作写入文件
                await FileUtils.WriteTextFileAsync(filePath, content);

                // 检查文件是否创建成功
                if (File.Exists(filePath))
                {
                    return true;
                }

                Debug.LogError($"JSON文件写入失败（文件不存在）: {filePath}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }
    }
}
