using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Extension
{
    /// <summary>
    /// 扩展管理器，负责管理所有包创建器扩展
    /// </summary>
    public class ExtensionManager
    {
        private static ExtensionManager s_Instance;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static ExtensionManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new ExtensionManager();
                }
                return s_Instance;
            }
        }

        private readonly List<PackageCreatorExtension> m_Extensions = new List<PackageCreatorExtension>();
        private readonly List<ITemplateProvider> m_TemplateProviders = new List<ITemplateProvider>();
        private readonly List<IFileGenerationStrategy> m_FileGenerationStrategies = new List<IFileGenerationStrategy>();
        private readonly List<IValidationRule> m_ValidationRules = new List<IValidationRule>();

        /// <summary>
        /// 已注册的扩展
        /// </summary>
        public IReadOnlyList<PackageCreatorExtension> Extensions => m_Extensions;

        /// <summary>
        /// 已注册的模板提供者
        /// </summary>
        public IReadOnlyList<ITemplateProvider> TemplateProviders => m_TemplateProviders;

        /// <summary>
        /// 已注册的文件生成策略
        /// </summary>
        public IReadOnlyList<IFileGenerationStrategy> FileGenerationStrategies => m_FileGenerationStrategies;

        /// <summary>
        /// 已注册的验证规则
        /// </summary>
        public IReadOnlyList<IValidationRule> ValidationRules => m_ValidationRules;

        /// <summary>
        /// 扩展注册事件
        /// </summary>
        public event Action<PackageCreatorExtension> ExtensionRegistered;

        /// <summary>
        /// 模板提供者注册事件
        /// </summary>
        public event Action<ITemplateProvider> TemplateProviderRegistered;

        /// <summary>
        /// 文件生成策略注册事件
        /// </summary>
        public event Action<IFileGenerationStrategy> FileGenerationStrategyRegistered;

        /// <summary>
        /// 验证规则注册事件
        /// </summary>
        public event Action<IValidationRule> ValidationRuleRegistered;

        /// <summary>
        /// 初始化扩展管理器
        /// </summary>
        public void Initialize()
        {
            FindAndRegisterExtensions();
            Debug.Log($"扩展管理器初始化完成，发现 {m_Extensions.Count} 个扩展");
        }

        /// <summary>
        /// 查找并注册所有扩展
        /// </summary>
        private void FindAndRegisterExtensions()
        {
            // 查找所有继承自PackageCreatorExtension的类
            var extensionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t != null && !t.IsAbstract && typeof(PackageCreatorExtension).IsAssignableFrom(t))
                .ToList();

            foreach (var extensionType in extensionTypes)
            {
                try
                {
                    var extension = (PackageCreatorExtension)Activator.CreateInstance(extensionType);
                    RegisterExtension(extension);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"加载扩展 {extensionType.Name} 失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <param name="extension">要注册的扩展</param>
        public void RegisterExtension(PackageCreatorExtension extension)
        {
            if (extension == null)
                return;

            if (m_Extensions.Any(e => e.ExtensionName == extension.ExtensionName))
            {
                Debug.LogWarning($"扩展 {extension.ExtensionName} 已经注册，跳过");
                return;
            }

            m_Extensions.Add(extension);
            Debug.Log($"注册扩展: {extension.ExtensionName} v{extension.Version} by {extension.Author}");

            // 初始化扩展
            extension.Initialize();

            // 触发事件
            ExtensionRegistered?.Invoke(extension);
        }

        /// <summary>
        /// 注册模板提供者
        /// </summary>
        /// <param name="provider">模板提供者</param>
        public void RegisterTemplateProvider(ITemplateProvider provider)
        {
            if (provider == null)
                return;

            if (m_TemplateProviders.Any(p => p.ProviderName == provider.ProviderName))
            {
                Debug.LogWarning($"模板提供者 {provider.ProviderName} 已经注册，跳过");
                return;
            }

            m_TemplateProviders.Add(provider);
            Debug.Log($"注册模板提供者: {provider.ProviderName} v{provider.ProviderVersion}");

            // 触发事件
            TemplateProviderRegistered?.Invoke(provider);
        }

        /// <summary>
        /// 注册文件生成策略
        /// </summary>
        /// <param name="strategy">文件生成策略</param>
        public void RegisterFileGenerationStrategy(IFileGenerationStrategy strategy)
        {
            if (strategy == null)
                return;

            if (m_FileGenerationStrategies.Any(s => s.StrategyName == strategy.StrategyName))
            {
                Debug.LogWarning($"文件生成策略 {strategy.StrategyName} 已经注册，跳过");
                return;
            }

            m_FileGenerationStrategies.Add(strategy);
            Debug.Log($"注册文件生成策略: {strategy.StrategyName}，支持文件类型: {string.Join(", ", strategy.SupportedFileExtensions)}");

            // 触发事件
            FileGenerationStrategyRegistered?.Invoke(strategy);
        }

        /// <summary>
        /// 注册验证规则
        /// </summary>
        /// <param name="rule">验证规则</param>
        public void RegisterValidationRule(IValidationRule rule)
        {
            if (rule == null)
                return;

            if (m_ValidationRules.Any(r => r.RuleName == rule.RuleName))
            {
                Debug.LogWarning($"验证规则 {rule.RuleName} 已经注册，跳过");
                return;
            }

            m_ValidationRules.Add(rule);
            Debug.Log($"注册验证规则: {rule.RuleName}，优先级: {rule.Priority}");

            // 按优先级排序验证规则
            m_ValidationRules.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            // 触发事件
            ValidationRuleRegistered?.Invoke(rule);
        }

        /// <summary>
        /// 获取指定类型的文件生成策略
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>支持该文件类型的策略列表</returns>
        public IEnumerable<IFileGenerationStrategy> GetFileGenerationStrategiesForType(string fileExtension)
        {
            return m_FileGenerationStrategies.Where(s => s.SupportsFileType(fileExtension));
        }
    }
}
