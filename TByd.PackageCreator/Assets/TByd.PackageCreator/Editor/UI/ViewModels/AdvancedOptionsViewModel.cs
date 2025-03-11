using System;
using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Utils;

namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 高级选项页面的视图模型，处理自定义变量和高级设置选项
    /// </summary>
    public class AdvancedOptionsViewModel : IViewModel
    {
        // 配置管理器
        private readonly IConfigManager _configManager;

        // 包配置引用
        private PackageConfig _packageConfig;

        // 自定义变量临时字段
        private string _newVariableKey = string.Empty;
        private string _newVariableValue = string.Empty;

        // 错误信息
        private string _errorMessage = string.Empty;

        // 搜索关键词
        private string _searchKeyword = string.Empty;

        // 是否显示添加表单
        private bool _isAddingVariable = false;

        // 是否修改了自定义变量
        private bool _isCustomVariablesModified = false;

        /// <summary>
        /// 包配置对象
        /// </summary>
        public PackageConfig PackageConfig => _packageConfig;

        /// <summary>
        /// 自定义变量列表
        /// </summary>
        public Dictionary<string, string> CustomVariables => _packageConfig?.CustomVariables;

        /// <summary>
        /// 筛选后的自定义变量
        /// </summary>
        public Dictionary<string, string> FilteredCustomVariables
        {
            get
            {
                if (string.IsNullOrEmpty(_searchKeyword) || CustomVariables == null)
                    return CustomVariables;

                return CustomVariables
                    .Where(kv =>
                        kv.Key.IndexOf(_searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        kv.Value.IndexOf(_searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value);
            }
        }

        /// <summary>
        /// 是否显示添加变量表单
        /// </summary>
        public bool IsAddingVariable
        {
            get => _isAddingVariable;
            set => _isAddingVariable = value;
        }

        /// <summary>
        /// 新变量键名
        /// </summary>
        public string NewVariableKey
        {
            get => _newVariableKey;
            set => _newVariableKey = value;
        }

        /// <summary>
        /// 新变量值
        /// </summary>
        public string NewVariableValue
        {
            get => _newVariableValue;
            set => _newVariableValue = value;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            private set => _errorMessage = value;
        }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string SearchKeyword
        {
            get => _searchKeyword;
            set => _searchKeyword = value;
        }

        /// <summary>
        /// 是否包含Tests文件夹
        /// </summary>
        public bool IncludeTests
        {
            get => _packageConfig?.IncludeTests ?? true;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.IncludeTests = value;
                }
            }
        }

        /// <summary>
        /// 是否包含Samples文件夹
        /// </summary>
        public bool IncludeSamples
        {
            get => _packageConfig?.IncludeSamples ?? true;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.IncludeSamples = value;
                }
            }
        }

        /// <summary>
        /// 是否包含Documentation文件夹
        /// </summary>
        public bool IncludeDocumentation
        {
            get => _packageConfig?.IncludeDocumentation ?? true;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.IncludeDocumentation = value;
                }
            }
        }

        /// <summary>
        /// 是否自定义变量已修改
        /// </summary>
        public bool IsCustomVariablesModified => _isCustomVariablesModified;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public AdvancedOptionsViewModel(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        /// <summary>
        /// 初始化视图模型
        /// </summary>
        public void Initialize()
        {
            // 从全局状态获取包配置
            var state = UIStateManager.Instance.CreationState;
            _packageConfig = state.PackageConfig;

            // 如果包配置为空，创建一个新的
            if (_packageConfig == null)
            {
                _packageConfig = new PackageConfig();
                state.PackageConfig = _packageConfig;
            }

            // 如果自定义变量为空，创建一个新的
            if (_packageConfig.CustomVariables == null)
            {
                _packageConfig.CustomVariables = new Dictionary<string, string>();
            }

            // 重置临时字段
            _newVariableKey = string.Empty;
            _newVariableValue = string.Empty;
            _isAddingVariable = false;
            _errorMessage = string.Empty;
            _searchKeyword = string.Empty;
            _isCustomVariablesModified = false;
        }

        /// <summary>
        /// 清理视图模型
        /// </summary>
        public void Cleanup()
        {
            // 保存修改到全局状态
            UIStateManager.Instance.UpdateState(state =>
            {
                state.PackageConfig = _packageConfig;
            });
        }

        /// <summary>
        /// 添加新变量
        /// </summary>
        /// <returns>是否添加成功</returns>
        public bool AddVariable()
        {
            if (string.IsNullOrEmpty(_newVariableKey))
            {
                _errorMessage = "变量名不能为空";
                return false;
            }

            // 按照变量命名规范验证键名（允许字母、数字、下划线，以字母开头）
            if (!System.Text.RegularExpressions.Regex.IsMatch(_newVariableKey, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                _errorMessage = "变量名无效。变量名必须以字母开头，只能包含字母、数字和下划线。";
                return false;
            }

            // 检查是否已存在相同键名的变量
            if (CustomVariables.ContainsKey(_newVariableKey))
            {
                _errorMessage = $"已存在名称为 {_newVariableKey} 的变量";
                return false;
            }

            // 添加新变量
            CustomVariables.Add(_newVariableKey, _newVariableValue);

            // 重置表单
            _newVariableKey = string.Empty;
            _newVariableValue = string.Empty;
            _errorMessage = string.Empty;
            _isAddingVariable = false;
            _isCustomVariablesModified = true;

            return true;
        }

        /// <summary>
        /// 更新变量
        /// </summary>
        /// <param name="key">要更新的变量键名</param>
        /// <param name="newKey">新键名</param>
        /// <param name="newValue">新值</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateVariable(string key, string newKey, string newValue)
        {
            if (string.IsNullOrEmpty(newKey))
            {
                _errorMessage = "变量名不能为空";
                return false;
            }

            // 按照变量命名规范验证键名
            if (!System.Text.RegularExpressions.Regex.IsMatch(newKey, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                _errorMessage = "变量名无效。变量名必须以字母开头，只能包含字母、数字和下划线。";
                return false;
            }

            // 如果键名改变，检查是否与现有键名冲突
            if (key != newKey && CustomVariables.ContainsKey(newKey))
            {
                _errorMessage = $"已存在名称为 {newKey} 的变量";
                return false;
            }

            // 更新变量
            if (key != newKey)
            {
                // 键名改变，需要移除旧键添加新键
                CustomVariables.Remove(key);
                CustomVariables.Add(newKey, newValue);
            }
            else
            {
                // 只更新值
                CustomVariables[key] = newValue;
            }

            _errorMessage = string.Empty;
            _isCustomVariablesModified = true;

            return true;
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        /// <param name="key">要删除的变量键名</param>
        public void RemoveVariable(string key)
        {
            if (CustomVariables.ContainsKey(key))
            {
                CustomVariables.Remove(key);
                _errorMessage = string.Empty;
                _isCustomVariablesModified = true;
            }
        }

        /// <summary>
        /// 获取预览替换字符串
        /// </summary>
        /// <param name="templateString">模板字符串</param>
        /// <returns>替换后的字符串</returns>
        public string GetPreviewString(string templateString)
        {
            if (string.IsNullOrEmpty(templateString))
                return string.Empty;

            string result = templateString;

            foreach (var kv in CustomVariables)
            {
                // 替换格式为 ${KEY} 的变量
                result = result.Replace("${" + kv.Key + "}", kv.Value);
            }

            return result;
        }
    }
}
