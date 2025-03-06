using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Utils;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 文件生成器类，负责根据模板创建目录结构和文件
    /// </summary>
    public class FileGenerator
    {
        // 变量替换正则表达式，匹配 ${变量名}
        private static readonly Regex VariablePattern = new Regex(@"\$\{([^}]+)\}", RegexOptions.Compiled);

        // 文件生成策略集合
        private readonly List<IFileGenerationStrategy> _strategies = new List<IFileGenerationStrategy>();

        // 默认文件生成策略
        private readonly IFileGenerationStrategy _defaultStrategy;

        // 事件：开始生成
        public event Action<string> GenerationStarted;

        // 事件：目录创建
        public event Action<string, int, int> DirectoryCreated;

        // 事件：文件创建
        public event Action<string, int, int> FileCreated;

        // 事件：生成完成
        public event Action<bool, string> GenerationCompleted;

        /// <summary>
        /// 创建文件生成器实例
        /// </summary>
        /// <param name="defaultStrategy">默认文件生成策略</param>
        public FileGenerator(IFileGenerationStrategy defaultStrategy = null)
        {
            _defaultStrategy = defaultStrategy ?? new DefaultFileGenerationStrategy();
            _strategies.Add(_defaultStrategy);
        }

        /// <summary>
        /// 注册文件生成策略
        /// </summary>
        /// <param name="strategy">文件生成策略</param>
        public void RegisterStrategy(IFileGenerationStrategy strategy)
        {
            if (strategy == null) throw new ArgumentNullException(nameof(strategy));

            // 防止重复注册
            if (_strategies.Any(s => s.StrategyName == strategy.StrategyName))
            {
                Debug.LogWarning($"策略 '{strategy.StrategyName}' 已经注册过，将被忽略。");
                return;
            }

            _strategies.Add(strategy);
            Debug.Log($"注册文件生成策略: {strategy.StrategyName}");
        }

        /// <summary>
        /// 生成文件和目录结构
        /// </summary>
        /// <param name="template">包模板</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>生成结果</returns>
        public async Task<ValidationResult> GenerateAsync(IPackageTemplate template, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();

            try
            {
                // 通知开始生成
                GenerationStarted?.Invoke(template.Name);

                // 验证目标路径
                if (!ValidateTargetPath(targetPath, result))
                {
                    GenerationCompleted?.Invoke(false, "目标路径验证失败");
                    return result;
                }

                // 创建目录结构
                var directoryResult = await CreateDirectoriesAsync(template.Directories, config, targetPath);
                result.Merge(directoryResult);

                if (!result.IsValid)
                {
                    GenerationCompleted?.Invoke(false, "目录结构创建失败");
                    return result;
                }

                // 创建文件
                var fileResult = await CreateFilesAsync(template.Files, config, targetPath);
                result.Merge(fileResult);

                // 通知生成完成
                GenerationCompleted?.Invoke(result.IsValid, result.IsValid ? "生成成功" : "生成过程中出现错误");

                return result;
            }
            catch (Exception ex)
            {
                result.AddError($"生成过程中发生异常: {ex.Message}");
                Debug.LogException(ex);
                GenerationCompleted?.Invoke(false, $"生成过程中发生异常: {ex.Message}");
                return result;
            }
        }

        /// <summary>
        /// 验证目标路径
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        /// <param name="result">验证结果</param>
        /// <returns>是否验证通过</returns>
        private bool ValidateTargetPath(string targetPath, ValidationResult result)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                result.AddError("目标路径不能为空");
                return false;
            }

            if (!FileUtils.IsValidPath(targetPath))
            {
                result.AddError($"目标路径无效: {targetPath}");
                return false;
            }

            if (!SecureFileOperations.IsPathInSafeZone(targetPath))
            {
                result.AddError($"目标路径不在安全区域内: {targetPath}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 创建目录结构
        /// </summary>
        /// <param name="directories">目录列表</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标基础路径</param>
        /// <returns>创建结果</returns>
        private async Task<ValidationResult> CreateDirectoriesAsync(IReadOnlyList<TemplateDirectory> directories, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();
            int totalDirectories = CountTotalDirectories(directories);
            int currentDirectory = 0;

            foreach (var directory in directories)
            {
                // 更新目录计数
                currentDirectory = await CreateDirectoryAsync(directory, config, targetPath, result, currentDirectory, totalDirectories);
            }

            return result;
        }

        /// <summary>
        /// 创建单个目录及其子目录
        /// </summary>
        /// <param name="directory">目录模板</param>
        /// <param name="config">包配置</param>
        /// <param name="basePath">基础路径</param>
        /// <param name="result">验证结果</param>
        /// <param name="currentDirectory">当前目录计数</param>
        /// <param name="totalDirectories">总目录数</param>
        /// <returns>更新后的目录计数</returns>
        private async Task<int> CreateDirectoryAsync(TemplateDirectory directory, PackageConfig config, string basePath,
                                         ValidationResult result, int currentDirectory, int totalDirectories)
        {
            // 替换路径中的变量
            string processedPath = ReplaceVariables(directory.RelativePath, config);
            string fullPath = Path.Combine(basePath, processedPath);

            try
            {
                // 增加计数
                currentDirectory++;

                // 创建目录
                if (FileUtils.EnsureDirectoryExists(fullPath))
                {
                    DirectoryCreated?.Invoke(processedPath, currentDirectory, totalDirectories);
                    result.AddInfo($"创建目录: {processedPath}");
                }
                else
                {
                    if (directory.IsRequired)
                    {
                        result.AddError($"无法创建必需目录: {processedPath}");
                    }
                    else
                    {
                        result.AddWarning($"无法创建可选目录: {processedPath}");
                    }
                }

                // 创建子目录
                if (directory.Subdirectories != null && directory.Subdirectories.Count > 0)
                {
                    foreach (var subDirectory in directory.Subdirectories)
                    {
                        // 使用递归调用创建子目录，并更新计数
                        currentDirectory = await CreateDirectoryAsync(subDirectory, config, basePath, result, currentDirectory, totalDirectories);
                    }
                }

                // 添加短暂延迟，避免可能的文件系统争用
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                if (directory.IsRequired)
                {
                    result.AddError($"创建目录时发生错误: {processedPath}, 错误: {ex.Message}");
                }
                else
                {
                    result.AddWarning($"创建可选目录时发生错误: {processedPath}, 错误: {ex.Message}");
                }
                Debug.LogException(ex);
            }

            // 返回更新后的计数
            return currentDirectory;
        }

        /// <summary>
        /// 计算总目录数（包括子目录）
        /// </summary>
        /// <param name="directories">目录列表</param>
        /// <returns>目录总数</returns>
        private int CountTotalDirectories(IReadOnlyList<TemplateDirectory> directories)
        {
            int count = 0;

            foreach (var directory in directories)
            {
                count++; // 计数当前目录

                // 递归计数子目录
                if (directory.Subdirectories != null && directory.Subdirectories.Count > 0)
                {
                    count += CountTotalDirectories(directory.Subdirectories);
                }
            }

            return count;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="files">文件列表</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>创建结果</returns>
        private async Task<ValidationResult> CreateFilesAsync(IReadOnlyList<TemplateFile> files, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();
            int totalFiles = files.Count;
            int currentFile = 0;

            foreach (var file in files)
            {
                currentFile++;

                // 替换文件路径中的变量
                string processedPath = ReplaceVariables(file.RelativePath, config);
                string fullPath = Path.Combine(targetPath, processedPath);

                // 确保父目录存在
                string directoryPath = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    if (!FileUtils.EnsureDirectoryExists(directoryPath))
                    {
                        if (file.IsRequired)
                        {
                            result.AddError($"无法创建文件的父目录: {processedPath}");
                        }
                        else
                        {
                            result.AddWarning($"无法创建可选文件的父目录: {processedPath}");
                        }
                        continue;
                    }
                }

                try
                {
                    // 查找匹配的文件生成策略
                    var strategy = FindStrategyForFile(file);

                    // 使用策略生成文件
                    var fileResult = await strategy.GenerateFileAsync(file, config, fullPath);
                    result.Merge(fileResult);

                    if (fileResult.IsValid)
                    {
                        FileCreated?.Invoke(processedPath, currentFile, totalFiles);
                        result.AddInfo($"创建文件: {processedPath}");
                    }
                    else if (file.IsRequired)
                    {
                        result.AddError($"创建必需文件失败: {processedPath}");
                    }
                    else
                    {
                        result.AddWarning($"创建可选文件失败: {processedPath}");
                    }

                    // 添加短暂延迟，避免可能的文件系统争用
                    await Task.Delay(20);
                }
                catch (Exception ex)
                {
                    if (file.IsRequired)
                    {
                        result.AddError($"创建文件时发生错误: {processedPath}, 错误: {ex.Message}");
                    }
                    else
                    {
                        result.AddWarning($"创建可选文件时发生错误: {processedPath}, 错误: {ex.Message}");
                    }
                    Debug.LogException(ex);
                }
            }

            return result;
        }

        /// <summary>
        /// 为文件查找合适的生成策略
        /// </summary>
        /// <param name="file">模板文件</param>
        /// <returns>文件生成策略</returns>
        private IFileGenerationStrategy FindStrategyForFile(TemplateFile file)
        {
            string extension = Path.GetExtension(file.RelativePath);

            // 查找支持此文件类型的策略
            foreach (var strategy in _strategies)
            {
                if (strategy.SupportsFileType(extension))
                {
                    return strategy;
                }
            }

            // 如果没有找到匹配的策略，使用默认策略
            return _defaultStrategy;
        }

        /// <summary>
        /// 替换字符串中的变量占位符
        /// </summary>
        /// <param name="template">包含变量的字符串</param>
        /// <param name="config">包配置</param>
        /// <returns>替换后的字符串</returns>
        public string ReplaceVariables(string template, PackageConfig config)
        {
            if (string.IsNullOrEmpty(template))
                return template;

            return VariablePattern.Replace(template, match =>
            {
                string variableName = match.Groups[1].Value;
                return GetVariableValue(variableName, config);
            });
        }

        /// <summary>
        /// 获取变量的值
        /// </summary>
        /// <param name="variableName">变量名</param>
        /// <param name="config">包配置</param>
        /// <returns>变量值</returns>
        private string GetVariableValue(string variableName, PackageConfig config)
        {
            // 基于变量名从配置中获取值
            switch (variableName.ToLowerInvariant())
            {
                case "name":
                    return config.Name;
                case "displayname":
                    return config.DisplayName;
                case "version":
                    return config.Version;
                case "description":
                    return config.Description;
                case "unityversion":
                    return config.UnityVersion;
                case "author":
                    return config.Author?.Name ?? string.Empty;
                case "company":
                    return config.Company ?? string.Empty;
                case "year":
                    return DateTime.Now.Year.ToString();
                case "date":
                    return DateTime.Now.ToString("yyyy-MM-dd");
                case "namespace":
                    // 如果没有明确的命名空间，则从包名生成一个
                    return !string.IsNullOrEmpty(config.RootNamespace)
                        ? config.RootNamespace
                        : GenerateNamespaceFromPackageName(config.Name);
                default:
                    // 查找自定义变量
                    if (config.CustomVariables != null &&
                        config.CustomVariables.TryGetValue(variableName, out string value))
                    {
                        return value;
                    }

                    // 未找到变量值，返回原始占位符
                    Debug.LogWarning($"未找到变量值: {variableName}");
                    return $"${{{variableName}}}";
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
            Regex invalidChars = new Regex(@"[^a-zA-Z0-9\.]");
            string validName = invalidChars.Replace(packageName, ".");

            // 替换多个连续的点为单个点
            Regex multipleDots = new Regex(@"\.{2,}");
            validName = multipleDots.Replace(validName, ".");

            // 移除开头和结尾的点
            validName = validName.Trim('.');

            return validName;
        }
    }
}
