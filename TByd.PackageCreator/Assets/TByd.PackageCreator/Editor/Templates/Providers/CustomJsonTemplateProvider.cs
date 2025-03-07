using System;
using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core;

namespace TByd.PackageCreator.Editor.Templates.Providers
{
    /// <summary>
    /// 自定义JSON模板提供者，用于提供从JSON文件加载的模板
    /// </summary>
    internal class CustomJsonTemplateProvider : ITemplateProvider
    {
        private readonly IPackageTemplate m_Template;
        private readonly string m_SourceFileName;

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string ProviderName => $"CustomJson_{m_SourceFileName}";

        /// <summary>
        /// 提供者版本
        /// </summary>
        public Version ProviderVersion => new Version(1, 0, 0);

        /// <summary>
        /// 创建自定义JSON模板提供者
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="sourceFileName">源文件名</param>
        public CustomJsonTemplateProvider(IPackageTemplate template, string sourceFileName)
        {
            m_Template = template ?? throw new ArgumentNullException(nameof(template));
            m_SourceFileName = sourceFileName ?? "unknown";
        }

        /// <summary>
        /// 获取此提供者提供的所有模板
        /// </summary>
        /// <returns>模板集合</returns>
        public IEnumerable<IPackageTemplate> GetTemplates()
        {
            yield return m_Template;
        }

        /// <summary>
        /// 检查是否包含指定ID的模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>是否包含</returns>
        public bool ContainsTemplate(string templateId)
        {
            return m_Template.Id == templateId;
        }
    }
}
