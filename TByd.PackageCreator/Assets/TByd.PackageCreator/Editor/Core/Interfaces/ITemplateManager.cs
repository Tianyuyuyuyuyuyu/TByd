using System;
using System.Collections.Generic;

namespace TByd.PackageCreator.Editor.Core.Interfaces
{
    /// <summary>
    /// 模板管理器接口，负责管理所有可用的包模板
    /// </summary>
    public interface ITemplateManager
    {
        /// <summary>
        /// 获取所有可用的模板
        /// </summary>
        /// <returns>模板集合</returns>
        IReadOnlyList<IPackageTemplate> GetAllTemplates();

        /// <summary>
        /// 根据ID获取模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>找到的模板，如果未找到则返回null</returns>
        IPackageTemplate GetTemplateById(string templateId);

        /// <summary>
        /// 注册一个模板提供者
        /// </summary>
        /// <param name="provider">模板提供者</param>
        void RegisterProvider(ITemplateProvider provider);

        /// <summary>
        /// 移除一个模板提供者
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <returns>是否成功移除</returns>
        bool RemoveProvider(string providerName);

        /// <summary>
        /// 获取所有注册的模板提供者
        /// </summary>
        /// <returns>模板提供者集合</returns>
        IReadOnlyList<ITemplateProvider> GetRegisteredProviders();

        /// <summary>
        /// 从JSON文件加载自定义模板
        /// </summary>
        /// <param name="jsonFilePath">JSON文件路径</param>
        /// <returns>加载的模板，如果加载失败则返回null</returns>
        IPackageTemplate LoadTemplateFromJson(string jsonFilePath);

        /// <summary>
        /// 模板变更事件
        /// </summary>
        event EventHandler<TemplateChangedEventArgs> OnTemplateChanged;

        /// <summary>
        /// 重新加载所有模板
        /// </summary>
        void ReloadTemplates();
    }

    /// <summary>
    /// 模板变更事件参数
    /// </summary>
    public class TemplateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 变更类型
        /// </summary>
        public EnumTemplateChangeType ChangeType { get; }

        /// <summary>
        /// 受影响的模板ID
        /// </summary>
        public string TemplateId { get; }

        /// <summary>
        /// 创建模板变更事件参数
        /// </summary>
        /// <param name="changeType">变更类型</param>
        /// <param name="templateId">模板ID</param>
        public TemplateChangedEventArgs(EnumTemplateChangeType changeType, string templateId)
        {
            ChangeType = changeType;
            TemplateId = templateId;
        }
    }

    /// <summary>
    /// 模板变更类型
    /// </summary>
    public enum EnumTemplateChangeType
    {
        /// <summary>
        /// 添加了新模板
        /// </summary>
        Added,

        /// <summary>
        /// 移除了模板
        /// </summary>
        Removed,

        /// <summary>
        /// 模板被更新
        /// </summary>
        Updated,

        /// <summary>
        /// 所有模板被重新加载
        /// </summary>
        Reloaded
    }
}
