using System;
using System.IO;
using System.Text.RegularExpressions;
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

        // 变量替换正则表达式
        private static readonly Regex VariablePattern = new Regex(@"\$\{([^}]+)\}", RegexOptions.Compiled);

        // 文件生成器引用，用于变量替换
        private readonly FileGenerator _fileGenerator;

        /// <summary>
        /// 创建默认文件生成策略
        /// </summary>
        /// <param name="createVariableProcessor">是否创建变量处理器，设为false可防止循环依赖</param>
        public DefaultFileGenerationStrategy(bool createVariableProcessor = true)
        {
            // 根据参数决定是否创建FileGenerator实例
            if (createVariableProcessor)
            {
                try
                {
                    // 创建FileGenerator但不设置默认策略，防止循环依赖
                    _fileGenerator = new FileGenerator(null, false);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"创建变量处理器时出错，将使用内部方法替代: {ex.Message}");
                    _fileGenerator = null;
                }
            }
            else
            {
                _fileGenerator = null;
                // 不创建FileGenerator实例，避免循环依赖
                // 将使用内部方法替换变量
            }
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
        /// 异步生成文件
        /// </summary>
        /// <param name="file">文件模板</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>验证结果</returns>
        public async Task<ValidationResult> GenerateFileAsync(TemplateFile file, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();

            try
            {
                // 确保父目录存在
                var directoryPath = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    if (!FileUtils.EnsureDirectoryExists(directoryPath))
                    {
                        if (file.IsRequired)
                        {
                            result.AddError($"无法创建文件的父目录: {file.RelativePath}");
                        }
                        else
                        {
                            result.AddWarning($"无法创建可选文件的父目录: {file.RelativePath}");
                        }
                        return result;
                    }
                }

                // 获取文件内容
                var content = file.ContentTemplate;

                // 替换变量（如果支持）
                if (file.SupportsVariableReplacement)
                {
                    content = _fileGenerator != null
                        ? _fileGenerator.ReplaceVariables(content, config)
                        : ReplaceVariables(content, config);

                    // 检查内容中是否还有未替换的变量
                    if (content.Contains("${") && content.Contains("}"))
                    {
                        Debug.LogWarning($"文件 {file.RelativePath} 可能包含未替换的变量。请检查模板和配置。");
                    }
                }

                // 创建文件
                bool success = await WriteFileAsync(targetPath, content);
                if (success)
                {
                    result.AddInfo($"创建文件成功: {file.RelativePath}");
                }
                else
                {
                    if (file.IsRequired)
                    {
                        result.AddError($"创建文件失败: {file.RelativePath}");
                    }
                    else
                    {
                        result.AddWarning($"创建可选文件失败: {file.RelativePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                if (file.IsRequired)
                {
                    result.AddError($"生成文件时出错: {file.RelativePath}, 错误: {ex.Message}");
                }
                else
                {
                    result.AddWarning($"生成可选文件时出错: {file.RelativePath}, 错误: {ex.Message}");
                }
                Debug.LogException(ex);
            }

            return result;
        }

        /// <summary>
        /// 替换模板中的变量
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="config">包配置</param>
        /// <returns>替换后的字符串</returns>
        private string ReplaceVariables(string template, PackageConfig config)
        {
            if (string.IsNullOrEmpty(template))
                return template;

            return VariablePattern.Replace(template, match =>
            {
                var variableName = match.Groups[1].Value;
                return GetVariableValue(variableName, config);
            });
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <param name="variableName">变量名</param>
        /// <param name="config">包配置</param>
        /// <returns>变量值</returns>
        private string GetVariableValue(string variableName, PackageConfig config)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                return string.Empty;
            }

            // 转换为大写，便于匹配
            string upperVarName = variableName.ToUpperInvariant();

            switch (upperVarName)
            {
                case "PACKAGE_NAME":
                    return !string.IsNullOrEmpty(config.Name) ? config.Name : "com.mycompany.mypackage";

                case "PACKAGE_VERSION":
                    return !string.IsNullOrEmpty(config.Version) ? config.Version : "0.1.0";

                case "DISPLAY_NAME":
                    return !string.IsNullOrEmpty(config.DisplayName) ? config.DisplayName : "My Package";

                case "DESCRIPTION":
                    return !string.IsNullOrEmpty(config.Description) ? config.Description : "A package for Unity.";

                case "AUTHOR_NAME":
                    return !string.IsNullOrEmpty(config.Author?.Name) ? config.Author.Name : "";

                case "AUTHOR_EMAIL":
                    return !string.IsNullOrEmpty(config.Author?.Email) ? config.Author.Email : "";

                case "AUTHOR_URL":
                    return !string.IsNullOrEmpty(config.Author?.Url) ? config.Author.Url : "";

                case "CURRENT_YEAR":
                    return DateTime.Now.Year.ToString();

                case "CURRENT_DATE":
                    return DateTime.Now.ToString("yyyy-MM-dd");

                case "ROOT_NAMESPACE":
                    return GenerateNamespaceFromPackageName(config.Name);

                default:
                    // 尝试从自定义变量获取
                    if (config.CustomVariables != null && config.CustomVariables.ContainsKey(variableName))
                    {
                        return config.CustomVariables[variableName];
                    }

                    // 无法解析变量，返回空字符串
                    Debug.LogWarning($"未能解析变量 ${{{variableName}}}，将使用空字符串替换");
                    return string.Empty;
            }
        }

        /// <summary>
        /// 从包名生成命名空间
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <returns>生成的命名空间</returns>
        private string GenerateNamespaceFromPackageName(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
                return string.Empty;

            // 替换无效的字符为点
            var invalidChars = new Regex(@"[^a-zA-Z0-9\.]");
            var validName = invalidChars.Replace(packageName, ".");

            // 替换多个连续的点为单个点
            var multipleDots = new Regex(@"\.{2,}");
            validName = multipleDots.Replace(validName, ".");

            // 移除开头和结尾的点
            validName = validName.Trim('.');

            return validName;
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
