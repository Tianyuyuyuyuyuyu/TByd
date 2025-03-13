using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Utils.FileSystem;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Services
{
    /// <summary>
    /// 文件生成器类，负责根据模板创建目录结构和文件
    /// </summary>
    public class FileGenerator
    {
        // 变量替换正则表达式，匹配 ${变量名}
        private static readonly Regex SVariablePattern = new Regex(@"\$\{([^}]+)\}", RegexOptions.Compiled);

        // 文件生成策略集合
        private readonly List<IFileGenerationStrategy> _mStrategies = new List<IFileGenerationStrategy>();

        // 默认文件生成策略
        private IFileGenerationStrategy _mDefaultStrategy;

        // 事件：开始生成
        public event Action<string> OnGenerationStarted;

        // 事件：目录创建
        public event Action<string, int, int> OnDirectoryCreated;

        // 事件：文件创建
        public event Action<string, int, int> OnFileCreated;

        // 事件：生成完成
        public event Action<bool, string> OnGenerationCompleted;

        /// <summary>
        /// 创建文件生成器实例
        /// </summary>
        /// <param name="defaultStrategy">默认文件生成策略</param>
        /// <param name="createDefaultIfNull">是否在defaultStrategy为null时创建默认策略</param>
        public FileGenerator(IFileGenerationStrategy defaultStrategy = null, bool createDefaultIfNull = true)
        {
            if (defaultStrategy != null)
            {
                _mDefaultStrategy = defaultStrategy;
                _mStrategies.Add(_mDefaultStrategy);
            }
            else if (createDefaultIfNull)
            {
                try
                {
                    // 创建默认策略，但指示不要创建变量处理器，避免循环依赖
                    _mDefaultStrategy = new DefaultFileGenerationStrategy(false);
                    _mStrategies.Add(_mDefaultStrategy);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"创建默认策略时发生错误: {ex.Message}");
                    // 出错时设为null，后续惰性创建
                    _mDefaultStrategy = null;
                }
            }
        }

        /// <summary>
        /// 获取默认策略（惰性初始化）
        /// </summary>
        private IFileGenerationStrategy GetDefaultStrategy()
        {
            // 惰性初始化默认策略
            if (_mDefaultStrategy == null)
            {
                try
                {
                    _mDefaultStrategy = new DefaultFileGenerationStrategy(false);
                    _mStrategies.Add(_mDefaultStrategy);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"惰性创建默认策略时发生错误: {ex.Message}");
                    throw;
                }
            }
            return _mDefaultStrategy;
        }

        /// <summary>
        /// 注册文件生成策略
        /// </summary>
        /// <param name="strategy">文件生成策略</param>
        public void RegisterStrategy(IFileGenerationStrategy strategy)
        {
            if (strategy == null) throw new ArgumentNullException(nameof(strategy));

            // 检查是否是默认策略类型
            if (strategy is DefaultFileGenerationStrategy && _mDefaultStrategy == null)
            {
                _mDefaultStrategy = strategy;
            }

            // 防止重复注册
            if (_mStrategies.Any(s => s.GetType() == strategy.GetType()))
            {
                Debug.LogWarning($"策略类型 '{strategy.GetType().Name}' 已经注册过，将被忽略。");
                return;
            }

            _mStrategies.Add(strategy);
            Debug.Log($"注册文件生成策略: {strategy.StrategyName}");
        }

        /// <summary>
        /// 根据模板和配置异步生成包
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="config">配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>验证结果</returns>
        public async Task<ValidationResult> GenerateAsync(IPackageTemplate template, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();

            try
            {
                OnGenerationStarted?.Invoke(targetPath);

                Debug.Log($"开始生成包: {config.Name} => {targetPath}");

                // 验证目标路径
                if (!ValidateTargetPath(targetPath, result))
                {
                    return result;
                }

                // 验证模板有效性（添加更多模板验证）
                if (template == null)
                {
                    result.AddError("无效的模板");
                    return result;
                }

                if (template.Directories == null || template.Files == null)
                {
                    result.AddError("模板目录或文件列表为空");
                    return result;
                }

                // 分批处理目录和文件创建，避免一次性操作太多文件系统对象

                // 创建目录结构
                await Task.Yield(); // 让Unity有机会处理其他事件
                var dirResult = await CreateDirectoriesAsync(template.Directories, config, targetPath);
                if (!dirResult.IsValid)
                {
                    result.Merge(dirResult);
                    return result;
                }

                // 创建文件
                await Task.Yield(); // 让Unity有机会处理其他事件
                var fileResult = await CreateFilesAsync(template.Files, config, targetPath);
                if (!fileResult.IsValid)
                {
                    result.Merge(fileResult);
                    return result;
                }

                OnGenerationCompleted?.Invoke(true, targetPath);
                return result;
            }
            catch (OperationCanceledException)
            {
                // 用户取消操作
                result.AddInfo("用户取消了包创建操作");
                OnGenerationCompleted?.Invoke(false, targetPath);
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                result.AddError($"生成包时发生异常: {ex.Message}");
                OnGenerationCompleted?.Invoke(false, targetPath);
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
        /// <param name="basePath">基础路径</param>
        /// <returns>验证结果</returns>
        private async Task<ValidationResult> CreateDirectoriesAsync(IReadOnlyList<TemplateDirectory> directories, PackageConfig config, string basePath)
        {
            var result = new ValidationResult();

            if (directories == null || directories.Count == 0)
            {
                return result;
            }

            // 计算目录总数
            int totalDirectories = CountTotalDirectories(directories);
            int currentDirectoryIndex = 0;

            try
            {
                // 创建根目录
                if (!string.IsNullOrEmpty(basePath) && !Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                    result.AddInfo($"创建根目录: {basePath}");
                }

                // 创建每个目录
                foreach (var directory in directories)
                {
                    // 每处理5个目录，让Unity有机会响应
                    if (currentDirectoryIndex % 5 == 0)
                    {
                        await Task.Yield();
                    }

                    // 添加延迟，防止文件系统操作过于频繁
                    if (currentDirectoryIndex > 0 && currentDirectoryIndex % 10 == 0)
                    {
                        await Task.Delay(10); // 10毫秒延迟
                    }

                    // 实际创建目录
                    var newIndex = await CreateDirectoryAsync(directory, config, basePath, result, currentDirectoryIndex, totalDirectories);
                    currentDirectoryIndex = newIndex;
                }
            }
            catch (Exception ex)
            {
                result.AddError($"创建目录结构时发生异常: {ex.Message}");
                Debug.LogException(ex);
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
            var processedPath = ReplaceVariables(directory.RelativePath, config);
            var fullPath = Path.Combine(basePath, processedPath);

            try
            {
                // 增加计数
                currentDirectory++;

                // 创建目录
                if (FileUtils.EnsureDirectoryExists(fullPath))
                {
                    OnDirectoryCreated?.Invoke(processedPath, currentDirectory, totalDirectories);
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
            var count = 0;

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
        /// <param name="basePath">基础路径</param>
        /// <returns>验证结果</returns>
        private async Task<ValidationResult> CreateFilesAsync(IReadOnlyList<TemplateFile> files, PackageConfig config, string basePath)
        {
            var result = new ValidationResult();

            try
            {
                if (files == null || files.Count == 0)
                {
                    return result;
                }

                // 获取可用的文件创建策略
                if (_mStrategies.Count == 0 && _mDefaultStrategy == null)
                {
                    result.AddError("没有可用的文件生成策略");
                    return result;
                }

                // 提前替换文件路径中的变量
                var processedFiles = new List<(TemplateFile file, string targetPath)>();
                foreach (var file in files)
                {
                    var relativePath = file.RelativePath;

                    // 替换路径中的变量
                    if (relativePath.Contains("${") || relativePath.Contains("["))
                    {
                        relativePath = ReplaceVariables(relativePath, config);
                        relativePath = relativePath.Replace("[", "").Replace("]", "");
                    }

                    var targetPath = Path.Combine(basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
                    processedFiles.Add((file, targetPath));
                }

                // 计算总文件数
                int totalFiles = processedFiles.Count;
                int currentFileIndex = 0;

                // 分批处理文件创建
                foreach (var (file, targetPath) in processedFiles)
                {
                    // 每处理3个文件，让Unity有机会响应
                    if (currentFileIndex % 3 == 0)
                    {
                        await Task.Yield();
                    }

                    // 添加延迟，防止文件系统操作过于频繁
                    if (currentFileIndex > 0 && currentFileIndex % 5 == 0)
                    {
                        await Task.Delay(20); // 20毫秒延迟
                    }

                    try
                    {
                        // 查找适合该文件类型的策略
                        var strategy = FindStrategyForFile(file);
                        if (strategy == null)
                        {
                            result.AddError($"无法找到处理文件的策略: {file.RelativePath}");
                            continue;
                        }

                        // 生成文件
                        var fileResult = await strategy.GenerateFileAsync(file, config, targetPath);
                        result.Merge(fileResult);

                        // 更新进度
                        currentFileIndex++;
                        OnFileCreated?.Invoke(targetPath, currentFileIndex, totalFiles);
                    }
                    catch (Exception ex)
                    {
                        result.AddError($"生成文件时发生异常: {file.RelativePath}, 错误: {ex.Message}");
                        Debug.LogException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError($"创建文件时发生异常: {ex.Message}");
                Debug.LogException(ex);
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
            var extension = Path.GetExtension(file.RelativePath);

            // 查找支持此文件类型的策略
            foreach (var strategy in _mStrategies)
            {
                if (strategy.SupportsFileType(extension))
                {
                    return strategy;
                }
            }

            // 如果没有找到匹配的策略，使用默认策略
            return GetDefaultStrategy();
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

            return SVariablePattern.Replace(template, match =>
            {
                var variableName = match.Groups[1].Value;
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
                    return !string.IsNullOrEmpty(config.Author.Name) ? config.Author.Name : "";

                case "AUTHOR_EMAIL":
                    return !string.IsNullOrEmpty(config.Author.Email) ? config.Author.Email : "";

                case "AUTHOR_URL":
                    return !string.IsNullOrEmpty(config.Author.Url) ? config.Author.Url : "";

                case "KEYWORDS":
                    // 确保在package.json中关键词有效，如果为空就提供一个默认的
                    if (config.Keywords == null || config.Keywords.Count == 0)
                    {
                        return "\"unity\", \"package\"";
                    }

                    // 为每个关键词添加引号并用逗号分隔
                    var formattedKeywords = string.Join(", ", config.Keywords.Select(k => $"\"{k}\""));
                    return !string.IsNullOrEmpty(formattedKeywords) ? formattedKeywords : "\"unity\", \"package\"";

                case "DEPENDENCIES":
                    if (config.Dependencies == null || config.Dependencies.Count == 0)
                    {
                        return ""; // 没有依赖项时返回空字符串，在JSON中会成为空对象
                    }

                    var dependencies = new List<string>();
                    foreach (var dependency in config.Dependencies)
                    {
                        dependencies.Add($"\"{dependency.Id}\": \"{dependency.Version}\"");
                    }

                    return string.Join(",\n    ", dependencies);

                case "DOCUMENTATION_URL":
                    return !string.IsNullOrEmpty(config.DocumentationUrl) ? config.DocumentationUrl : "";

                case "CHANGELOG_URL":
                    return !string.IsNullOrEmpty(config.ChangelogUrl) ? config.ChangelogUrl : "";

                case "LICENSE_URL":
                    return !string.IsNullOrEmpty(config.LicenseUrl) ? config.LicenseUrl : "";

                case "UNITY_VERSION":
                    return !string.IsNullOrEmpty(config.UnityVersion) ? config.UnityVersion : "2021.3";

                case "CURRENT_YEAR":
                    return DateTime.Now.Year.ToString();

                case "CURRENT_DATE":
                    return DateTime.Now.ToString("yyyy-MM-dd");

                case "ROOT_NAMESPACE":
                    // 从包名生成命名空间
                    return GenerateNamespaceFromPackageName(config.Name);

                // 处理其他变量...

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
    }
}
