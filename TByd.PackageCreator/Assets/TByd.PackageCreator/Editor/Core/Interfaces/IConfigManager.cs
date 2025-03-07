using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Editor.Core.Interfaces
{
    /// <summary>
    /// 配置管理器接口，定义了配置管理的主要功能
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// 获取当前加载的配置
        /// </summary>
        PackageConfig CurrentConfig { get; }

        /// <summary>
        /// 获取配置历史记录
        /// </summary>
        IReadOnlyList<ConfigHistoryEntry> History { get; }

        /// <summary>
        /// 创建新配置
        /// </summary>
        /// <param name="name">包名称</param>
        /// <param name="displayName">显示名称</param>
        /// <returns>新创建的配置</returns>
        PackageConfig CreateNewConfig(string name, string displayName);

        /// <summary>
        /// 保存当前配置
        /// </summary>
        /// <param name="filePath">文件路径，为空则使用上次保存的路径或默认路径</param>
        /// <returns>操作结果</returns>
        Task<ValidationResult> SaveConfigAsync(string filePath = null);

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>加载结果</returns>
        Task<ValidationResult> LoadConfigAsync(string filePath);

        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>导入结果</returns>
        Task<ValidationResult> ImportConfigAsync(string filePath);

        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="config">要导出的配置</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>导出结果</returns>
        Task<ValidationResult> ExportConfigAsync(PackageConfig config, string filePath);

        /// <summary>
        /// 验证配置
        /// </summary>
        /// <param name="config">要验证的配置</param>
        /// <returns>验证结果</returns>
        ValidationResult ValidateConfig(PackageConfig config);

        /// <summary>
        /// 添加配置到历史记录
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="description">描述</param>
        void AddToHistory(PackageConfig config, string description);

        /// <summary>
        /// 从历史记录中恢复配置
        /// </summary>
        /// <param name="entryIndex">历史记录索引</param>
        /// <returns>恢复的配置</returns>
        PackageConfig RestoreFromHistory(int entryIndex);

        /// <summary>
        /// 清除历史记录
        /// </summary>
        void ClearHistory();
    }

    /// <summary>
    /// 配置历史记录条目
    /// </summary>
    [Serializable]
    public class ConfigHistoryEntry
    {
        /// <summary>
        /// 配置
        /// </summary>
        [JsonProperty("config")]
        public PackageConfig Config { get; private set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("timestamp")]
        public System.DateTime Timestamp { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// 无参数构造函数，用于JSON反序列化
        /// </summary>
        [JsonConstructor]
        public ConfigHistoryEntry()
        {
            Config = null;
            Timestamp = System.DateTime.Now;
            Description = string.Empty;
        }

        /// <summary>
        /// 创建新的配置历史记录条目
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="description">描述</param>
        public ConfigHistoryEntry(PackageConfig config, string description)
        {
            Config = config;
            Timestamp = System.DateTime.Now;
            Description = description;
        }
    }
}
