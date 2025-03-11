using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Utils;

namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 摘要页面的视图模型，处理包创建前的所有配置信息汇总
    /// </summary>
    public class SummaryViewModel : IViewModel
    {
        // 服务引用
        private readonly IConfigManager _configManager;
        private readonly ITemplateManager _templateManager;

        // 包配置引用
        private PackageConfig _packageConfig;

        // 模板引用
        private IPackageTemplate _selectedTemplate;

        // 错误信息
        private List<string> _validationErrors = new List<string>();

        /// <summary>
        /// 获取包配置
        /// </summary>
        public PackageConfig PackageConfig => _packageConfig;

        /// <summary>
        /// 获取选中的模板
        /// </summary>
        public IPackageTemplate SelectedTemplate => _selectedTemplate;

        /// <summary>
        /// 获取验证错误列表
        /// </summary>
        public IReadOnlyList<string> ValidationErrors => _validationErrors;

        /// <summary>
        /// 判断当前配置是否有效
        /// </summary>
        public bool IsValid => _validationErrors.Count == 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        /// <param name="templateManager">模板管理器</param>
        public SummaryViewModel(IConfigManager configManager, ITemplateManager templateManager)
        {
            _configManager = configManager ?? throw new ArgumentNullException(nameof(configManager));
            _templateManager = templateManager ?? throw new ArgumentNullException(nameof(templateManager));
        }

        /// <summary>
        /// 初始化视图模型
        /// </summary>
        public void Initialize()
        {
            _packageConfig = _configManager.CurrentConfig;

            // 从UIStateManager获取选中的模板
            _selectedTemplate = UIStateManager.Instance.CreationState.SelectedTemplate;

            ValidateConfiguration();
        }

        /// <summary>
        /// 清理视图模型
        /// </summary>
        public void Cleanup()
        {
            // 暂无需要清理的资源
        }

        /// <summary>
        /// 验证当前配置是否完整有效
        /// </summary>
        private void ValidateConfiguration()
        {
            _validationErrors.Clear();

            // 验证必要的基础配置
            if (_packageConfig == null)
            {
                _validationErrors.Add("包配置不存在，请重新开始配置流程");
                return;
            }

            if (_selectedTemplate == null)
            {
                _validationErrors.Add("未选择模板，请返回模板选择页面选择一个模板");
            }

            if (string.IsNullOrWhiteSpace(_packageConfig.Name))
            {
                _validationErrors.Add("包名称不能为空");
            }
            else if (!_packageConfig.Name.Contains("."))
            {
                _validationErrors.Add("包名称必须包含至少一个点号(.)，推荐使用反向域名格式，如：com.yourcompany.packagename");
            }

            if (string.IsNullOrWhiteSpace(_packageConfig.DisplayName))
            {
                _validationErrors.Add("显示名称不能为空");
            }

            if (string.IsNullOrWhiteSpace(_packageConfig.Version))
            {
                _validationErrors.Add("版本号不能为空");
            }

            if (string.IsNullOrWhiteSpace(_packageConfig.Description))
            {
                _validationErrors.Add("包描述不能为空");
            }

            if (_packageConfig.Author == null || string.IsNullOrWhiteSpace(_packageConfig.Author.Name))
            {
                _validationErrors.Add("作者信息不能为空");
            }
        }

        /// <summary>
        /// 生成包基本信息的摘要文本
        /// </summary>
        /// <returns>包含基本信息的摘要文本</returns>
        public string GetBasicInfoSummary()
        {
            StringBuilder summary = new StringBuilder();

            if (_packageConfig != null)
            {
                summary.AppendLine($"模板: {_selectedTemplate?.Name ?? "未选择"}");
                summary.AppendLine($"包名称: {_packageConfig.Name}");
                summary.AppendLine($"显示名称: {_packageConfig.DisplayName}");
                summary.AppendLine($"版本: {_packageConfig.Version}");
                summary.AppendLine($"描述: {_packageConfig.Description}");
                summary.AppendLine($"作者: {(_packageConfig.Author != null ? _packageConfig.Author.Name : "未设置")}");

                if (!string.IsNullOrEmpty(_packageConfig.UnityVersion))
                {
                    summary.AppendLine($"Unity兼容版本: {_packageConfig.UnityVersion}");
                }

                if (!string.IsNullOrEmpty(_packageConfig.RootNamespace))
                {
                    summary.AppendLine($"根命名空间: {_packageConfig.RootNamespace}");
                }
            }

            return summary.ToString();
        }

        /// <summary>
        /// 生成依赖项的摘要文本
        /// </summary>
        /// <returns>包含依赖项的摘要文本</returns>
        public string GetDependenciesSummary()
        {
            StringBuilder summary = new StringBuilder();

            if (_packageConfig?.Dependencies != null && _packageConfig.Dependencies.Count > 0)
            {
                foreach (var dependency in _packageConfig.Dependencies)
                {
                    summary.AppendLine($"{dependency.Id}: {dependency.Version}");
                }
            }
            else
            {
                summary.AppendLine("无依赖项");
            }

            return summary.ToString();
        }

        /// <summary>
        /// 生成自定义变量的摘要文本
        /// </summary>
        /// <returns>包含自定义变量的摘要文本</returns>
        public string GetCustomVariablesSummary()
        {
            StringBuilder summary = new StringBuilder();

            if (_packageConfig?.CustomVariables != null && _packageConfig.CustomVariables.Count > 0)
            {
                foreach (var variable in _packageConfig.CustomVariables)
                {
                    summary.AppendLine($"{variable.Key}: {variable.Value}");
                }
            }
            else
            {
                summary.AppendLine("无自定义变量");
            }

            return summary.ToString();
        }

        /// <summary>
        /// 生成目录选项的摘要文本
        /// </summary>
        /// <returns>包含目录选项的摘要文本</returns>
        public string GetDirectoryOptionsSummary()
        {
            StringBuilder summary = new StringBuilder();

            if (_packageConfig != null)
            {
                summary.AppendLine($"包含Tests目录: {(_packageConfig.IncludeTests ? "是" : "否")}");
                summary.AppendLine($"包含Samples目录: {(_packageConfig.IncludeSamples ? "是" : "否")}");
                summary.AppendLine($"包含Documentation目录: {(_packageConfig.IncludeDocumentation ? "是" : "否")}");
            }

            return summary.ToString();
        }
    }
}
