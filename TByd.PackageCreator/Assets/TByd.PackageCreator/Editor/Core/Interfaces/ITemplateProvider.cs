using System;
using System.Collections.Generic;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 模板提供者接口，用于支持插件式模板提供
    /// </summary>
    public interface ITemplateProvider
    {
        /// <summary>
        /// 获取此提供者提供的所有模板
        /// </summary>
        /// <returns>模板集合</returns>
        IEnumerable<IPackageTemplate> GetTemplates();

        /// <summary>
        /// 提供者名称
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// 提供者版本
        /// </summary>
        Version ProviderVersion { get; }
    }
}
