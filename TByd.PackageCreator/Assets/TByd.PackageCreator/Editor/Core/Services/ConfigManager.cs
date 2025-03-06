using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 配置管理器类，实现配置的保存、加载、验证、导入/导出和历史记录功能
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        private static ConfigManager _instance;

        /// <summary>
        /// 获取ConfigManager单例实例
        /// </summary>
        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigManager();
                }
                return _instance;
            }
        }

        // 当前加载的配置
        private PackageConfig _currentConfig;

        // 配置历史记录
        private List<ConfigHistoryEntry> _history = new List<ConfigHistoryEntry>();

        // 最后保存的文件路径
        private string _lastSavedPath;

        // 最大历史记录数量
        private const int MaxHistoryCount = 20;

        // 默认配置保存目录
        private const string DefaultConfigDirectory = "PackageConfigs";

        // JSON文件扩展名
        private const string JsonExtension = ".json";

        // 错误处理器
        private readonly ErrorHandler _errorHandler;

        /// <summary>
        /// 获取当前加载的配置
        /// </summary>
        public PackageConfig CurrentConfig => _currentConfig;

        /// <summary>
        /// 获取配置历史记录
        /// </summary>
        public IReadOnlyList<ConfigHistoryEntry> History => _history.AsReadOnly();

        /// <summary>
        /// 创建ConfigManager实例
        /// </summary>
        private ConfigManager()
        {
            _errorHandler = ErrorHandler.Instance;
            _currentConfig = CreateDefaultConfig();
        }

        /// <summary>
        /// 创建新配置
        /// </summary>
        /// <param name="name">包名称</param>
        /// <param name="displayName">显示名称</param>
        /// <returns>新创建的配置</returns>
        public PackageConfig CreateNewConfig(string name, string displayName)
        {
            var config = new PackageConfig(name, displayName);

            // 设置一些默认值
            config.Version = "0.1.0";
            config.Description = $"{displayName} - 由TByd.PackageCreator创建";
            config.UnityVersion = "2021.3";
            config.Author = new PackageAuthor("TByd", "", "");

            _currentConfig = config;
            AddToHistory(config, "创建新配置");

            return config;
        }

        /// <summary>
        /// 保存当前配置
        /// </summary>
        /// <param name="filePath">文件路径，为空则使用上次保存的路径或默认路径</param>
        /// <returns>操作结果</returns>
        public async Task<ValidationResult> SaveConfigAsync(string filePath = null)
        {
            var result = new ValidationResult();

            if (_currentConfig == null)
            {
                result.AddError("没有可保存的配置");
                return result;
            }

            // 验证配置
            var validationResult = ValidateConfig(_currentConfig);
            if (!validationResult.IsValid)
            {
                result.Merge(validationResult);
                result.AddError("配置验证失败，无法保存");
                return result;
            }

            try
            {
                // 确定保存路径
                string savePath = DetermineSavePath(filePath);
                if (string.IsNullOrEmpty(savePath))
                {
                    result.AddError("无法确定保存路径");
                    return result;
                }

                // 确保目录存在
                string directory = Path.GetDirectoryName(savePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 将配置序列化为JSON
                string json = SerializeConfig(_currentConfig);
                if (string.IsNullOrEmpty(json))
                {
                    result.AddError("配置序列化失败");
                    return result;
                }

                // 保存文件
                await FileUtils.WriteTextFileAsync(savePath, json);

                // 记录保存路径
                _lastSavedPath = savePath;

                // 添加到历史记录
                AddToHistory(_currentConfig, $"保存到 {Path.GetFileName(savePath)}");

                result.AddInfo($"配置已保存到: {savePath}");
                AssetDatabase.Refresh();
            }
            catch (Exception ex)
            {
                result.AddError($"保存配置时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileWriteError, ex, "保存配置时发生错误");
            }

            return result;
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>加载结果</returns>
        public async Task<ValidationResult> LoadConfigAsync(string filePath)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(filePath))
            {
                result.AddError("文件路径不能为空");
                return result;
            }

            if (!File.Exists(filePath))
            {
                result.AddError($"文件不存在: {filePath}");
                return result;
            }

            try
            {
                // 读取文件内容
                string json = await FileUtils.ReadTextFileAsync(filePath);
                if (string.IsNullOrEmpty(json))
                {
                    result.AddError("文件内容为空");
                    return result;
                }

                // 反序列化配置
                var config = DeserializeConfig(json);
                if (config == null)
                {
                    result.AddError("配置反序列化失败");
                    return result;
                }

                // 验证配置
                var validationResult = ValidateConfig(config);
                result.Merge(validationResult);

                // 即使有警告也加载配置
                _currentConfig = config;
                _lastSavedPath = filePath;

                // 添加到历史记录
                AddToHistory(config, $"从 {Path.GetFileName(filePath)} 加载");

                result.AddInfo($"配置已从 {filePath} 加载");
            }
            catch (Exception ex)
            {
                result.AddError($"加载配置时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileReadError, ex, "加载配置时发生错误");
            }

            return result;
        }

        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>导入结果</returns>
        public async Task<ValidationResult> ImportConfigAsync(string filePath)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(filePath))
            {
                result.AddError("文件路径不能为空");
                return result;
            }

            if (!File.Exists(filePath))
            {
                result.AddError($"文件不存在: {filePath}");
                return result;
            }

            try
            {
                // 确定文件类型
                string extension = Path.GetExtension(filePath).ToLowerInvariant();

                // 根据文件类型选择导入方法
                switch (extension)
                {
                    case JsonExtension:
                        // JSON文件直接加载
                        return await LoadConfigAsync(filePath);

                    case ".unitypackage":
                        // Unity包导入功能（简化版）
                        result.AddError("暂不支持从Unity包导入");
                        break;

                    case ".xml":
                    case ".yaml":
                    case ".yml":
                        // 其他格式转换功能（简化版）
                        result.AddError($"暂不支持{extension}格式导入");
                        break;

                    default:
                        result.AddError($"不支持的文件格式: {extension}");
                        break;
                }
            }
            catch (Exception ex)
            {
                result.AddError($"导入配置时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.OperationFailed, ex, "导入配置时发生错误");
            }

            return result;
        }

        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="config">要导出的配置</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>导出结果</returns>
        public async Task<ValidationResult> ExportConfigAsync(PackageConfig config, string filePath)
        {
            var result = new ValidationResult();

            if (config == null)
            {
                result.AddError("配置不能为空");
                return result;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                result.AddError("导出路径不能为空");
                return result;
            }

            try
            {
                // 验证配置
                var validationResult = ValidateConfig(config);
                if (!validationResult.IsValid)
                {
                    result.Merge(validationResult);
                    result.AddError("配置验证失败，无法导出");
                    return result;
                }

                // 确定文件类型
                string extension = Path.GetExtension(filePath).ToLowerInvariant();

                // 根据文件类型选择导出方法
                switch (extension)
                {
                    case JsonExtension:
                        // 将配置序列化为JSON
                        string json = SerializeConfig(config);
                        if (string.IsNullOrEmpty(json))
                        {
                            result.AddError("配置序列化失败");
                            return result;
                        }

                        // 确保目录存在
                        string directory = Path.GetDirectoryName(filePath);
                        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        // 保存文件
                        await FileUtils.WriteTextFileAsync(filePath, json);
                        result.AddInfo($"配置已导出到: {filePath}");
                        break;

                    default:
                        result.AddError($"不支持的导出格式: {extension}");
                        break;
                }
            }
            catch (Exception ex)
            {
                result.AddError($"导出配置时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileWriteError, ex, "导出配置时发生错误");
            }

            return result;
        }

        /// <summary>
        /// 验证配置
        /// </summary>
        /// <param name="config">要验证的配置</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateConfig(PackageConfig config)
        {
            var result = new ValidationResult();

            if (config == null)
            {
                result.AddError("配置不能为空");
                return result;
            }

            // 验证基本字段
            if (string.IsNullOrEmpty(config.Name))
            {
                result.AddError("包名称不能为空", "Name");
            }
            else if (!IsValidPackageName(config.Name))
            {
                result.AddError("包名称格式无效，应使用反向域名格式，例如：com.company.package", "Name");
            }

            if (string.IsNullOrEmpty(config.DisplayName))
            {
                result.AddError("包显示名称不能为空", "DisplayName");
            }

            if (string.IsNullOrEmpty(config.Version))
            {
                result.AddError("包版本不能为空", "Version");
            }
            else if (!IsValidVersion(config.Version))
            {
                result.AddError("包版本格式无效，应使用语义化版本格式，例如：1.0.0", "Version");
            }

            if (string.IsNullOrEmpty(config.Description))
            {
                result.AddWarning("包描述为空，建议添加描述", "Description");
            }

            if (string.IsNullOrEmpty(config.UnityVersion))
            {
                result.AddWarning("Unity版本未指定，将使用默认版本", "UnityVersion");
            }

            // 验证作者信息
            if (config.Author == null)
            {
                result.AddWarning("包作者信息未指定", "Author");
            }
            else
            {
                if (string.IsNullOrEmpty(config.Author.Name))
                {
                    result.AddWarning("作者名称为空", "Author.Name");
                }
            }

            // 验证依赖项
            if (config.Dependencies != null && config.Dependencies.Count > 0)
            {
                foreach (var dependency in config.Dependencies)
                {
                    if (string.IsNullOrEmpty(dependency.Id))
                    {
                        result.AddError("依赖项ID不能为空", "Dependencies");
                    }
                    else if (!IsValidPackageName(dependency.Id))
                    {
                        result.AddError($"依赖项ID格式无效：{dependency.Id}", "Dependencies");
                    }

                    if (string.IsNullOrEmpty(dependency.Version))
                    {
                        result.AddWarning($"依赖项 {dependency.Id} 的版本未指定，将使用最新版本", "Dependencies");
                    }
                    else if (!IsValidDependencyVersion(dependency.Version))
                    {
                        result.AddWarning($"依赖项 {dependency.Id} 的版本格式可能无效：{dependency.Version}", "Dependencies");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 添加配置到历史记录
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="description">描述</param>
        public void AddToHistory(PackageConfig config, string description)
        {
            if (config == null)
                return;

            // 创建配置的深拷贝
            var configCopy = CloneConfig(config);

            // 创建历史记录条目
            var entry = new ConfigHistoryEntry(configCopy, description);

            // 添加到历史记录
            _history.Insert(0, entry);

            // 限制历史记录数量
            if (_history.Count > MaxHistoryCount)
            {
                _history.RemoveAt(_history.Count - 1);
            }
        }

        /// <summary>
        /// 从历史记录中恢复配置
        /// </summary>
        /// <param name="entryIndex">历史记录索引</param>
        /// <returns>恢复的配置</returns>
        public PackageConfig RestoreFromHistory(int entryIndex)
        {
            if (entryIndex < 0 || entryIndex >= _history.Count)
                return null;

            // 获取历史记录条目
            var entry = _history[entryIndex];

            // 创建配置的深拷贝
            var configCopy = CloneConfig(entry.Config);

            // 设置为当前配置
            _currentConfig = configCopy;

            // 添加到历史记录
            AddToHistory(configCopy, $"从历史记录恢复：{entry.Description}");

            return configCopy;
        }

        /// <summary>
        /// 清除历史记录
        /// </summary>
        public void ClearHistory()
        {
            _history.Clear();
        }

        /// <summary>
        /// 创建默认配置
        /// </summary>
        /// <returns>默认配置</returns>
        private PackageConfig CreateDefaultConfig()
        {
            var config = new PackageConfig(
                "com.mycompany.mypackage",
                "My Package",
                "0.1.0",
                "A new package created by TByd.PackageCreator"
            );

            config.UnityVersion = "2021.3";
            config.Author = new PackageAuthor("Your Name", "your.email@example.com", "");

            return config;
        }

        /// <summary>
        /// 确定保存路径
        /// </summary>
        /// <param name="filePath">指定的文件路径</param>
        /// <returns>最终的保存路径</returns>
        private string DetermineSavePath(string filePath)
        {
            // 如果指定了文件路径，则使用指定的路径
            if (!string.IsNullOrEmpty(filePath))
                return filePath;

            // 如果有上次保存的路径，则使用上次的路径
            if (!string.IsNullOrEmpty(_lastSavedPath))
                return _lastSavedPath;

            // 否则使用默认路径
            string projectPath = Application.dataPath.Replace("/Assets", "");
            string configDirectory = Path.Combine(projectPath, DefaultConfigDirectory);

            // 确保目录存在
            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            // 使用包名作为文件名
            string fileName = _currentConfig?.Name?.Replace(".", "-") ?? "config";
            return Path.Combine(configDirectory, $"{fileName}{JsonExtension}");
        }

        /// <summary>
        /// 将配置序列化为JSON
        /// </summary>
        /// <param name="config">配置</param>
        /// <returns>JSON字符串</returns>
        private string SerializeConfig(PackageConfig config)
        {
            try
            {
                // 使用Newtonsoft.Json序列化配置
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                    Error = (sender, args) =>
                    {
                        _errorHandler.LogWarning(ErrorType.SerializationError, $"JSON序列化警告: {args.ErrorContext.Error.Message}");
                        args.ErrorContext.Handled = true;
                    }
                };

                return JsonConvert.SerializeObject(config, settings);
            }
            catch (Exception ex)
            {
                _errorHandler.LogException(ErrorType.SerializationError, ex, "配置序列化失败");
                Debug.LogError($"序列化错误：{ex.Message}\n{ex.StackTrace}");
                return "{}"; // 返回空对象而不是null，避免空引用
            }
        }

        /// <summary>
        /// 从JSON反序列化配置
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>配置</returns>
        private PackageConfig DeserializeConfig(string json)
        {
            try
            {
                // 直接使用Newtonsoft.Json反序列化
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        _errorHandler.LogWarning(ErrorType.DeserializationError, $"JSON反序列化警告: {args.ErrorContext.Error.Message}");
                        args.ErrorContext.Handled = true;
                    }
                };

                return JsonConvert.DeserializeObject<PackageConfig>(json, settings);
            }
            catch (Exception ex)
            {
                _errorHandler.LogException(ErrorType.DeserializationError, ex, "配置反序列化失败");
                return null;
            }
        }

        /// <summary>
        /// 克隆配置
        /// </summary>
        /// <param name="config">源配置</param>
        /// <returns>克隆的配置</returns>
        private PackageConfig CloneConfig(PackageConfig config)
        {
            if (config == null)
                return null;

            try
            {
                // 使用Newtonsoft.Json进行序列化和反序列化来实现深拷贝
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        _errorHandler.LogWarning(ErrorType.SerializationError, $"JSON序列化警告: {args.ErrorContext.Error.Message}");
                        args.ErrorContext.Handled = true;
                    }
                };

                string json = JsonConvert.SerializeObject(config, settings);
                return JsonConvert.DeserializeObject<PackageConfig>(json, settings);
            }
            catch (Exception ex)
            {
                _errorHandler.LogException(ErrorType.SerializationError, ex, "配置克隆失败");

                // 发生错误时回退到手动复制（确保不会返回null）
                var fallbackConfig = new PackageConfig(
                    config.Name ?? string.Empty,
                    config.DisplayName ?? string.Empty,
                    config.Version ?? "0.1.0",
                    config.Description ?? string.Empty
                );

                return fallbackConfig;
            }
        }

        /// <summary>
        /// 验证包名称是否有效
        /// </summary>
        /// <param name="name">包名称</param>
        /// <returns>是否有效</returns>
        private bool IsValidPackageName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            // 包名应该是反向域名格式，例如：com.company.package
            string[] parts = name.Split('.');
            return parts.Length >= 2 && parts.All(p => !string.IsNullOrEmpty(p));
        }

        /// <summary>
        /// 验证版本是否有效
        /// </summary>
        /// <param name="version">版本</param>
        /// <returns>是否有效</returns>
        private bool IsValidVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return false;

            // 简单的语义化版本验证
            string[] parts = version.Split('-');
            string[] versionParts = parts[0].Split('.');

            return versionParts.Length >= 2 && versionParts.All(p => int.TryParse(p, out _));
        }

        /// <summary>
        /// 验证依赖版本是否有效
        /// </summary>
        /// <param name="version">依赖版本</param>
        /// <returns>是否有效</returns>
        private bool IsValidDependencyVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return false;

            // 支持常见的依赖版本格式
            // 精确版本：1.0.0
            // 版本范围：>1.0.0, >=1.0.0, <2.0.0, <=2.0.0
            // 波浪号：~1.0.0（兼容补丁版本更新）
            // 插入符号：^1.0.0（兼容次要版本更新）

            // 这里为简化，仅做基本检查
            return true;
        }
    }
}
