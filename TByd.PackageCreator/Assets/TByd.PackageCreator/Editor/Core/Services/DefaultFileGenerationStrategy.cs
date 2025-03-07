using System;
using System.IO;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Utils.FileSystem;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Services
{
    /// <summary>
    /// 默认文件生成策略，支持所有类型的文件生成
    /// </summary>
    public class DefaultFileGenerationStrategy : IFileGenerationStrategy
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        public string StrategyName => "默认文件生成策略";

        /// <summary>
        /// 支持的文件扩展名，默认支持所有类型
        /// </summary>
        public string[] SupportedFileExtensions => new[] { ".*" };

        // 变量替换正则表达式（委托给FileGenerator处理）
        private readonly FileGenerator _mVariableProcessor;

        /// <summary>
        /// 创建默认文件生成策略
        /// </summary>
        public DefaultFileGenerationStrategy()
        {
            _mVariableProcessor = new FileGenerator(null);
        }

        /// <summary>
        /// 检查此策略是否支持指定的文件类型
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>是否支持</returns>
        public bool SupportsFileType(string fileExtension)
        {
            // 默认策略支持所有文件类型
            return true;
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
                    result.AddInfo($"成功创建文件: {Path.GetFileName(targetPath)}");
                }
                else
                {
                    result.AddError($"创建文件失败: {Path.GetFileName(targetPath)}");
                }
            }
            catch (Exception ex)
            {
                result.AddError($"生成文件时发生异常: {ex.Message}");
                Debug.LogException(ex);
            }

            return result;
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

                Debug.LogError($"文件写入失败（文件不存在）: {filePath}");
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
