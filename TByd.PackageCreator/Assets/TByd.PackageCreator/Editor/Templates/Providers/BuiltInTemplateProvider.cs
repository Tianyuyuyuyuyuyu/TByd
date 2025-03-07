using System;
using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Templates.Implementations;

namespace TByd.PackageCreator.Editor.Templates.Providers
{
    /// <summary>
    /// 内置模板提供者，提供包含在包中的默认模板
    /// </summary>
    public class BuiltInTemplateProvider : ITemplateProvider
    {
        private readonly List<IPackageTemplate> _mTemplates = new List<IPackageTemplate>();
        private readonly ErrorHandler _mErrorHandler;

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
            _mErrorHandler = ErrorHandler.Instance;
            LoadBuiltInTemplates();
        }

        /// <summary>
        /// 获取此提供者提供的所有模板
        /// </summary>
        /// <returns>模板集合</returns>
        public IEnumerable<IPackageTemplate> GetTemplates()
        {
            return _mTemplates.AsReadOnly();
        }

        /// <summary>
        /// 加载内置模板
        /// </summary>
        private void LoadBuiltInTemplates()
        {
            try
            {
                _mErrorHandler.LogInfo("正在加载内置模板...");

                // 添加基础包模板
                _mTemplates.Add(new BasicPackageTemplate());

                // 添加编辑器工具模板
                _mTemplates.Add(new EditorToolTemplate());

                // 添加运行时库模板
                _mTemplates.Add(new RuntimeLibraryTemplate());

                _mErrorHandler.LogInfo($"已成功加载 {_mTemplates.Count} 个内置模板");
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.OperationFailed, ex, "加载内置模板时出错");
            }
        }
    }
}
