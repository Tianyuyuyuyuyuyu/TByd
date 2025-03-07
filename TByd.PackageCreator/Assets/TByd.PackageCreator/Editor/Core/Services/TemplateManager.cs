using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Templates.Data;
using TByd.PackageCreator.Editor.Templates.Providers;

namespace TByd.PackageCreator.Editor.Core.Services
{
    /// <summary>
    /// 模板管理器，负责管理所有可用的包模板
    /// </summary>
    public class TemplateManager : ITemplateManager
    {
        private readonly List<ITemplateProvider> _mProviders = new List<ITemplateProvider>();
        private readonly Dictionary<string, IPackageTemplate> _mTemplatesCache = new Dictionary<string, IPackageTemplate>();
        private static TemplateManager _sInstance;
        private readonly ErrorHandler _mErrorHandler;

        /// <summary>
        /// 模板变更事件
        /// </summary>
        public event EventHandler<TemplateChangedEventArgs> OnTemplateChanged;

        /// <summary>
        /// 获取TemplateManager实例
        /// </summary>
        public static TemplateManager Instance
        {
            get { return _sInstance ??= new TemplateManager(); }
        }

        private TemplateManager()
        {
            _mErrorHandler = ErrorHandler.Instance;
            LoadInternalProviders();
        }

        /// <summary>
        /// 加载内部提供者
        /// </summary>
        private void LoadInternalProviders()
        {
            try
            {
                // 加载内置模板提供者
                var builtInProvider = new BuiltInTemplateProvider();
                RegisterProvider(builtInProvider);

                // 反射查找其他实现了ITemplateProvider的类型并实例化
                LoadTemplateProvidersFromAssembly();
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.OperationFailed, ex, "加载内置模板提供者时出错");
            }
        }

        /// <summary>
        /// 通过反射从程序集中查找模板提供者
        /// </summary>
        private void LoadTemplateProvidersFromAssembly()
        {
            try
            {
                var providerType = typeof(ITemplateProvider);
                var assembly = Assembly.GetExecutingAssembly();

                var providerTypes = assembly.GetTypes()
                    .Where(t => providerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .Where(t => t != typeof(BuiltInTemplateProvider)) // 排除已手动加载的提供者
                    .Where(t => t != typeof(CustomJsonTemplateProvider)) // 排除需要特殊构造的提供者
                    .ToList();

                foreach (var type in providerTypes)
                {
                    try
                    {
                        // 检查是否有无参构造函数
                        var hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null;
                        if (!hasDefaultConstructor)
                        {
                            _mErrorHandler.LogInfo($"跳过模板提供者 {type.Name}，因为它没有无参构造函数");
                            continue;
                        }

                        if (Activator.CreateInstance(type) is ITemplateProvider provider)
                        {
                            RegisterProvider(provider);
                            _mErrorHandler.LogInfo($"已通过反射加载模板提供者: {provider.ProviderName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _mErrorHandler.LogException(ErrorType.OperationFailed, ex, $"实例化模板提供者类型 {type.Name} 时出错");
                    }
                }
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.OperationFailed, ex, "通过反射查找模板提供者时出错");
            }
        }

        /// <summary>
        /// 获取所有可用的模板
        /// </summary>
        /// <returns>模板集合</returns>
        public IReadOnlyList<IPackageTemplate> GetAllTemplates()
        {
            RefreshTemplatesCache();
            return _mTemplatesCache.Values.ToList().AsReadOnly();
        }

        /// <summary>
        /// 根据ID获取模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>找到的模板，如果未找到则返回null</returns>
        public IPackageTemplate GetTemplateById(string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                _mErrorHandler.LogError(ErrorType.InvalidArgument, "模板ID不能为空");
                return null;
            }

            RefreshTemplatesCache();

            if (_mTemplatesCache.TryGetValue(templateId, out var template))
            {
                return template;
            }

            _mErrorHandler.LogWarning(ErrorType.ResourceNotFound, $"未找到ID为 {templateId} 的模板");
            return null;
        }

        /// <summary>
        /// 注册一个模板提供者
        /// </summary>
        /// <param name="provider">模板提供者</param>
        public void RegisterProvider(ITemplateProvider provider)
        {
            if (provider == null)
            {
                _mErrorHandler.LogError(ErrorType.InvalidArgument, "不能注册空的模板提供者");
                return;
            }

            if (_mProviders.Any(p => p.ProviderName == provider.ProviderName))
            {
                _mErrorHandler.LogWarning(ErrorType.DuplicateResource, $"模板提供者 '{provider.ProviderName}' 已经注册");
                return;
            }

            _mProviders.Add(provider);

            // 注册新提供者后刷新缓存
            RefreshTemplatesCache();

            _mErrorHandler.LogInfo($"已注册模板提供者: {provider.ProviderName} v{provider.ProviderVersion}");

            // 触发模板变更事件
            OnTemplateChangedMethod(EnumTemplateChangeType.Reloaded, null);
        }

        /// <summary>
        /// 移除一个模板提供者
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <returns>是否成功移除</returns>
        public bool RemoveProvider(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                _mErrorHandler.LogError(ErrorType.InvalidArgument, "提供者名称不能为空");
                return false;
            }

            var provider = _mProviders.FirstOrDefault(p => p.ProviderName == providerName);
            if (provider == null)
            {
                _mErrorHandler.LogWarning(ErrorType.ResourceNotFound, $"未找到名称为 {providerName} 的模板提供者");
                return false;
            }

            _mProviders.Remove(provider);

            // 移除提供者后刷新缓存
            RefreshTemplatesCache();

            _mErrorHandler.LogInfo($"已移除模板提供者: {providerName}");

            // 触发模板变更事件
            OnTemplateChangedMethod(EnumTemplateChangeType.Reloaded, null);

            return true;
        }

        /// <summary>
        /// 获取所有注册的模板提供者
        /// </summary>
        /// <returns>模板提供者集合</returns>
        public IReadOnlyList<ITemplateProvider> GetRegisteredProviders()
        {
            return _mProviders.AsReadOnly();
        }

        /// <summary>
        /// 从JSON文件加载自定义模板
        /// </summary>
        /// <param name="jsonFilePath">JSON文件路径</param>
        /// <returns>加载的模板，如果加载失败则返回null</returns>
        public IPackageTemplate LoadTemplateFromJson(string jsonFilePath)
        {
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                _mErrorHandler.LogError(ErrorType.InvalidArgument, "JSON文件路径不能为空");
                return null;
            }

            if (!File.Exists(jsonFilePath))
            {
                _mErrorHandler.LogError(ErrorType.FileNotFound, $"未找到JSON文件: {jsonFilePath}");
                return null;
            }

            try
            {
                // 使用模板序列化器从文件加载模板
                var template = TemplateSerializer.DeserializeFromFile(jsonFilePath);

                if (template == null)
                {
                    _mErrorHandler.LogError(ErrorType.InvalidData, $"无法从JSON文件加载有效模板: {jsonFilePath}");
                    return null;
                }

                // 检查模板ID是否有效
                if (string.IsNullOrEmpty(template.Id))
                {
                    _mErrorHandler.LogError(ErrorType.InvalidData, $"从JSON加载的模板ID不能为空: {jsonFilePath}");
                    return null;
                }

                // 检查是否已存在同ID的模板
                RefreshTemplatesCache();
                if (_mTemplatesCache.ContainsKey(template.Id))
                {
                    _mErrorHandler.LogWarning(ErrorType.DuplicateResource,
                        $"已存在ID为 {template.Id} 的模板，将被覆盖");

                    // 先移除旧模板的提供者(如果是自定义JSON提供者)
                    var customProvider = _mProviders.FirstOrDefault(p =>
                        p is CustomJsonTemplateProvider jsonProvider &&
                        jsonProvider.ContainsTemplate(template.Id));

                    if (customProvider != null)
                    {
                        _mProviders.Remove(customProvider);
                    }
                }

                // 创建一个自定义模板提供者并注册
                var provider = new CustomJsonTemplateProvider(template, Path.GetFileName(jsonFilePath));
                RegisterProvider(provider);

                _mErrorHandler.LogInfo($"已成功从JSON加载模板: {template.Id} ({template.Name})");

                // 通知模板已添加
                OnTemplateChangedMethod(EnumTemplateChangeType.Added, template.Id);

                return template;
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.FileReadError, ex, $"加载JSON模板文件时出错: {jsonFilePath}");
                return null;
            }
        }

        /// <summary>
        /// 重新加载所有模板
        /// </summary>
        public void ReloadTemplates()
        {
            RefreshTemplatesCache();
            OnTemplateChangedMethod(EnumTemplateChangeType.Reloaded, null);
            _mErrorHandler.LogInfo("已重新加载所有模板");
        }

        /// <summary>
        /// 刷新模板缓存
        /// </summary>
        private void RefreshTemplatesCache()
        {
            _mTemplatesCache.Clear();

            foreach (var provider in _mProviders)
            {
                try
                {
                    var templates = provider.GetTemplates();
                    if (templates == null) continue;

                    foreach (var template in templates)
                    {
                        if (template == null) continue;

                        if (string.IsNullOrEmpty(template.Id))
                        {
                            _mErrorHandler.LogWarning(ErrorType.InvalidData, $"提供者 {provider.ProviderName} 返回了一个无效的模板(ID为空)");
                            continue;
                        }

                        if (_mTemplatesCache.ContainsKey(template.Id))
                        {
                            _mErrorHandler.LogWarning(ErrorType.DuplicateResource,
                                $"模板ID冲突: {template.Id}, 来自提供者 {provider.ProviderName}. 该模板将被忽略");
                            continue;
                        }

                        _mTemplatesCache.Add(template.Id, template);
                    }
                }
                catch (Exception ex)
                {
                    _mErrorHandler.LogException(ErrorType.OperationFailed, ex,
                        $"从提供者 {provider.ProviderName} 获取模板时出错");
                }
            }
        }

        /// <summary>
        /// 触发模板变更事件
        /// </summary>
        /// <param name="changeType">变更类型</param>
        /// <param name="templateId">模板ID</param>
        private void OnTemplateChangedMethod(EnumTemplateChangeType changeType, string templateId)
        {
            OnTemplateChanged?.Invoke(this, new TemplateChangedEventArgs(changeType, templateId));
        }
    }
}
