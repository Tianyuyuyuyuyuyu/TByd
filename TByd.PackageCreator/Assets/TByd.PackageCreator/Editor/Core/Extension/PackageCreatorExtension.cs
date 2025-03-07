using System;
using TByd.PackageCreator.Editor.Core.Interfaces;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Extension
{
    /// <summary>
    /// 包创建器扩展基类，所有扩展插件应该继承此类
    /// </summary>
    public abstract class PackageCreatorExtension
    {
        /// <summary>
        /// 扩展名称
        /// </summary>
        public abstract string ExtensionName { get; }

        /// <summary>
        /// 扩展描述
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 扩展版本
        /// </summary>
        public abstract Version Version { get; }

        /// <summary>
        /// 扩展作者
        /// </summary>
        public abstract string Author { get; }

        /// <summary>
        /// 初始化扩展
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// 注册此扩展提供的模板提供者
        /// </summary>
        /// <param name="provider">模板提供者</param>
        protected void RegisterTemplateProvider(ITemplateProvider provider)
        {
            Debug.Log($"注册模板提供者: {provider.ProviderName}");
            // TODO: 实现注册逻辑，将在ExtensionManager中完成
        }

        /// <summary>
        /// 注册此扩展提供的文件生成策略
        /// </summary>
        /// <param name="strategy">文件生成策略</param>
        protected void RegisterFileGenerationStrategy(IFileGenerationStrategy strategy)
        {
            Debug.Log($"注册文件生成策略: {strategy.StrategyName}");
            // TODO: 实现注册逻辑，将在ExtensionManager中完成
        }

        /// <summary>
        /// 注册此扩展提供的验证规则
        /// </summary>
        /// <param name="rule">验证规则</param>
        protected void RegisterValidationRule(IValidationRule rule)
        {
            Debug.Log($"注册验证规则: {rule.RuleName}");
            // TODO: 实现注册逻辑，将在ExtensionManager中完成
        }
    }
}
