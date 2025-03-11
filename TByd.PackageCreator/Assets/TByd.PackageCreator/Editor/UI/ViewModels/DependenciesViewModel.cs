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
    /// 依赖配置页面的视图模型，管理依赖列表和相关操作
    /// </summary>
    public class DependenciesViewModel : IViewModel
    {
        // 配置管理器
        private readonly IConfigManager _configManager;

        // 包配置引用
        private PackageConfig _packageConfig;

        // 新依赖的临时字段
        private string _newDependencyId = string.Empty;
        private string _newDependencyVersion = string.Empty;

        // 是否显示添加表单
        private bool _isAddingDependency = false;

        // 错误信息
        private string _errorMessage = string.Empty;

        // 搜索关键词
        private string _searchKeyword = string.Empty;

        // 推荐的常用包
        private readonly List<PackageDependency> _recommendedPackages = new List<PackageDependency>
        {
            new PackageDependency("com.unity.textmeshpro", "3.0.6"),
            new PackageDependency("com.unity.inputsystem", "1.4.4"),
            new PackageDependency("com.unity.cinemachine", "2.8.9"),
            new PackageDependency("com.unity.addressables", "1.19.19"),
            new PackageDependency("com.unity.visualscripting", "1.7.8"),
            new PackageDependency("com.unity.render-pipelines.universal", "12.1.7"),
            new PackageDependency("com.unity.mathematics", "1.2.6"),
            new PackageDependency("com.unity.burst", "1.6.6"),
            new PackageDependency("com.unity.collections", "1.3.1"),
            new PackageDependency("com.unity.jobs", "0.70.0-preview.7")
        };

        /// <summary>
        /// 包配置对象
        /// </summary>
        public PackageConfig PackageConfig => _packageConfig;

        /// <summary>
        /// 依赖列表
        /// </summary>
        public List<PackageDependency> Dependencies => _packageConfig?.Dependencies;

        /// <summary>
        /// 筛选后的依赖列表
        /// </summary>
        public List<PackageDependency> FilteredDependencies
        {
            get
            {
                if (string.IsNullOrEmpty(_searchKeyword) || Dependencies == null)
                    return Dependencies;

                return Dependencies.Where(d =>
                    d.Id.IndexOf(_searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    d.Version.IndexOf(_searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
        }

        /// <summary>
        /// 推荐包列表
        /// </summary>
        public List<PackageDependency> RecommendedPackages => _recommendedPackages;

        /// <summary>
        /// 筛选后的推荐包列表
        /// </summary>
        public List<PackageDependency> FilteredRecommendedPackages
        {
            get
            {
                if (string.IsNullOrEmpty(_searchKeyword))
                    return _recommendedPackages;

                return _recommendedPackages.Where(d =>
                    d.Id.IndexOf(_searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
        }

        /// <summary>
        /// 是否显示添加表单
        /// </summary>
        public bool IsAddingDependency
        {
            get => _isAddingDependency;
            set => _isAddingDependency = value;
        }

        /// <summary>
        /// 新依赖ID
        /// </summary>
        public string NewDependencyId
        {
            get => _newDependencyId;
            set => _newDependencyId = value;
        }

        /// <summary>
        /// 新依赖版本
        /// </summary>
        public string NewDependencyVersion
        {
            get => _newDependencyVersion;
            set => _newDependencyVersion = value;
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
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public DependenciesViewModel(IConfigManager configManager)
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

            // 如果依赖列表为空，创建一个新的
            if (_packageConfig.Dependencies == null)
            {
                _packageConfig.Dependencies = new List<PackageDependency>();
            }

            // 重置临时字段
            _newDependencyId = string.Empty;
            _newDependencyVersion = string.Empty;
            _isAddingDependency = false;
            _errorMessage = string.Empty;
            _searchKeyword = string.Empty;
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
        /// 添加新依赖
        /// </summary>
        /// <returns>是否添加成功</returns>
        public bool AddDependency()
        {
            if (string.IsNullOrEmpty(_newDependencyId))
            {
                _errorMessage = "包ID不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(_newDependencyVersion))
            {
                _errorMessage = "版本不能为空";
                return false;
            }

            // 检查是否已存在相同ID的依赖
            if (Dependencies.Any(d => d.Id == _newDependencyId))
            {
                _errorMessage = $"已存在ID为 {_newDependencyId} 的依赖";
                return false;
            }

            // 添加新依赖
            Dependencies.Add(new PackageDependency(_newDependencyId, _newDependencyVersion));

            // 重置表单
            _newDependencyId = string.Empty;
            _newDependencyVersion = string.Empty;
            _errorMessage = string.Empty;
            _isAddingDependency = false;

            return true;
        }

        /// <summary>
        /// 添加推荐依赖
        /// </summary>
        /// <param name="recommendedDependency">推荐依赖</param>
        /// <returns>是否添加成功</returns>
        public bool AddRecommendedDependency(PackageDependency recommendedDependency)
        {
            // 检查是否已存在相同ID的依赖
            if (Dependencies.Any(d => d.Id == recommendedDependency.Id))
            {
                _errorMessage = $"已存在ID为 {recommendedDependency.Id} 的依赖";
                return false;
            }

            // 添加新依赖
            Dependencies.Add(new PackageDependency(recommendedDependency.Id, recommendedDependency.Version));
            _errorMessage = string.Empty;

            return true;
        }

        /// <summary>
        /// 移除依赖
        /// </summary>
        /// <param name="dependency">要移除的依赖项</param>
        public void RemoveDependency(PackageDependency dependency)
        {
            Dependencies.Remove(dependency);
            _errorMessage = string.Empty;
        }

        /// <summary>
        /// 更新依赖
        /// </summary>
        /// <param name="oldDependency">原依赖项</param>
        /// <param name="newId">新ID</param>
        /// <param name="newVersion">新版本</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateDependency(PackageDependency oldDependency, string newId, string newVersion)
        {
            if (string.IsNullOrEmpty(newId))
            {
                _errorMessage = "包ID不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(newVersion))
            {
                _errorMessage = "版本不能为空";
                return false;
            }

            // 检查是否与其他依赖ID冲突
            if (newId != oldDependency.Id && Dependencies.Any(d => d.Id == newId))
            {
                _errorMessage = $"已存在ID为 {newId} 的依赖";
                return false;
            }

            // 更新依赖
            int index = Dependencies.IndexOf(oldDependency);
            if (index >= 0)
            {
                Dependencies[index] = new PackageDependency(newId, newVersion);
                _errorMessage = string.Empty;
                return true;
            }

            return false;
        }
    }
}
