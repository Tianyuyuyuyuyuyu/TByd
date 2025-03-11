using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Templates.Data;

namespace TByd.PackageCreator.Editor.Templates.Providers
{
    /// <summary>
    /// JSON文件夹模板提供者，用于从指定文件夹加载JSON模板
    /// </summary>
    internal class JsonFolderTemplateProvider : ITemplateProvider
    {
        private readonly string _folderPath;
        private readonly List<IPackageTemplate> _templates = new List<IPackageTemplate>();
        private readonly ErrorHandler _errorHandler;

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string ProviderName => "JsonFolderTemplateProvider";

        /// <summary>
        /// 提供者版本
        /// </summary>
        public Version ProviderVersion => new Version(1, 0, 0);

        /// <summary>
        /// 创建JSON文件夹模板提供者
        /// </summary>
        /// <param name="folderPath">JSON模板文件夹路径</param>
        public JsonFolderTemplateProvider(string folderPath)
        {
            _folderPath = folderPath ?? throw new ArgumentNullException(nameof(folderPath));
            _errorHandler = ErrorHandler.Instance;
            LoadTemplatesFromFolder();
        }

        /// <summary>
        /// 获取此提供者提供的所有模板
        /// </summary>
        /// <returns>模板集合</returns>
        public IEnumerable<IPackageTemplate> GetTemplates()
        {
            return _templates;
        }

        /// <summary>
        /// 检查是否包含指定ID的模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>是否包含</returns>
        public bool ContainsTemplate(string templateId)
        {
            return _templates.Any(t => t.Id == templateId);
        }

        /// <summary>
        /// 从文件夹加载所有JSON模板
        /// </summary>
        private void LoadTemplatesFromFolder()
        {
            try
            {
                _templates.Clear();

                if (!Directory.Exists(_folderPath))
                {
                    _errorHandler.LogWarning(ErrorType.FileNotFound, $"模板文件夹不存在: {_folderPath}");
                    return;
                }

                // 获取文件夹中所有JSON文件
                string[] jsonFiles = Directory.GetFiles(_folderPath, "*.json", SearchOption.AllDirectories);
                _errorHandler.LogInfo($"在 {_folderPath} 中找到 {jsonFiles.Length} 个JSON文件");

                foreach (string jsonFile in jsonFiles)
                {
                    try
                    {
                        _errorHandler.LogInfo($"正在加载JSON模板文件: {jsonFile}");

                        // 读取JSON文件内容并输出前100个字符用于调试
                        string jsonContent = File.ReadAllText(jsonFile);
                        string jsonPreview = jsonContent.Length > 100 ? jsonContent.Substring(0, 100) + "..." : jsonContent;
                        _errorHandler.LogInfo($"JSON内容预览: {jsonPreview}");

                        // 使用模板序列化器从文件加载模板
                        var template = TemplateSerializer.DeserializeFromFile(jsonFile);

                        if (template == null)
                        {
                            _errorHandler.LogWarning(ErrorType.InvalidData, $"无法从JSON文件加载有效模板: {jsonFile}");
                            continue;
                        }

                        // 检查模板ID是否有效
                        if (string.IsNullOrEmpty(template.Id))
                        {
                            _errorHandler.LogWarning(ErrorType.InvalidData, $"从JSON加载的模板ID不能为空: {jsonFile}");
                            continue;
                        }

                        // 检查是否已存在同ID的模板
                        if (_templates.Any(t => t.Id == template.Id))
                        {
                            _errorHandler.LogWarning(ErrorType.DuplicateResource,
                                $"已存在ID为 {template.Id} 的模板，将被忽略: {jsonFile}");
                            continue;
                        }

                        // 添加到模板列表
                        _templates.Add(template);
                        _errorHandler.LogInfo($"已加载JSON模板: {template.Id} ({template.Name}) 从 {jsonFile}，分类: {template.Category}");
                    }
                    catch (Exception ex)
                    {
                        _errorHandler.LogException(ErrorType.FileReadError, ex, $"加载JSON模板文件时出错: {jsonFile}");
                    }
                }

                _errorHandler.LogInfo($"成功加载了 {_templates.Count} 个JSON模板");

                // 输出所有已加载模板的详细信息
                if (_templates.Count > 0)
                {
                    _errorHandler.LogInfo("已加载的模板详细信息:");
                    foreach (var template in _templates)
                    {
                        _errorHandler.LogInfo($"- ID: {template.Id}, 名称: {template.Name}, 分类: {template.Category}");
                    }
                }
            }
            catch (Exception ex)
            {
                _errorHandler.LogException(ErrorType.OperationFailed, ex, $"从文件夹加载JSON模板时出错: {_folderPath}");
            }
        }

        /// <summary>
        /// 重新加载所有模板
        /// </summary>
        public void ReloadTemplates()
        {
            _templates.Clear();
            LoadTemplatesFromFolder();
        }
    }
}
