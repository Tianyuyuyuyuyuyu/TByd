using System;
using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Templates.Implementations;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Templates.Providers
{
    /// <summary>
    /// 内置模板提供者，提供包含在包中的默认模板
    /// </summary>
    public class BuiltInTemplateProvider : ITemplateProvider
    {
        private readonly List<IPackageTemplate> _templates = new List<IPackageTemplate>();
        private readonly ErrorHandler _errorHandler;

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string ProviderName => "BuildInTemplateProvider";

        /// <summary>
        /// 提供者版本
        /// </summary>
        public Version ProviderVersion => new Version(1, 0, 0);

        /// <summary>
        /// 创建内置模板提供者
        /// </summary>
        public BuiltInTemplateProvider()
        {
            _errorHandler = ErrorHandler.Instance;
            LoadBuiltInTemplates();
        }

        /// <summary>
        /// 获取此提供者提供的所有模板
        /// </summary>
        /// <returns>模板集合</returns>
        public IEnumerable<IPackageTemplate> GetTemplates()
        {
            return _templates.AsReadOnly();
        }

        /// <summary>
        /// 加载内置模板
        /// </summary>
        private void LoadBuiltInTemplates()
        {
            try
            {
                _errorHandler.LogInfo("正在加载内置模板...");

                // 添加基础包模板
                _templates.Add(new BasicPackageTemplate());

                // 添加编辑器工具模板
                _templates.Add(new EditorToolTemplate());

                // 添加运行时库模板
                _templates.Add(new RuntimeLibraryTemplate());

                _errorHandler.LogInfo($"已成功加载 {_templates.Count} 个内置模板");
            }
            catch (Exception ex)
            {
                _errorHandler.LogException(ErrorType.OperationFailed, ex, "加载内置模板时出错");
            }
        }
    }
}
