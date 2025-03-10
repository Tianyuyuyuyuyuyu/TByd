using System;
using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.UI.Utils;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 模板选择视图模型
    /// </summary>
    public class TemplateSelectionViewModel : IViewModel
    {
        // 模板管理器
        private readonly ITemplateManager _templateManager;

        // 事件处理程序
        private readonly Action<string, string> _searchKeywordChangedHandler;
        private readonly Action<int, int> _selectedCategoryChangedHandler;
        private readonly Action<SortMode, SortMode> _sortModeChangedHandler;

        // 模板列表
        public CollectionBinding<IPackageTemplate> Templates { get; } = new CollectionBinding<IPackageTemplate>();

        // 选中的模板
        public Binding<IPackageTemplate> SelectedTemplate { get; } = new Binding<IPackageTemplate>();

        // 搜索关键字
        public Binding<string> SearchKeyword { get; } = new Binding<string>("");

        // 模板分类
        public Binding<string[]> Categories { get; } = new Binding<string[]>(new string[0]);

        // 选中的分类
        public Binding<int> SelectedCategoryIndex { get; } = new Binding<int>(0);

        // 是否显示详情
        public Binding<bool> ShowDetails { get; } = new Binding<bool>(false);

        // 排序方式
        public enum SortMode
        {
            NameAsc,
            NameDesc,
            CategoryAsc,
            CategoryDesc,
            RecentlyUsed
        }

        // 当前排序方式
        public Binding<SortMode> CurrentSortMode { get; } = new Binding<SortMode>(SortMode.NameAsc);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templateManager">模板管理器</param>
        public TemplateSelectionViewModel(ITemplateManager templateManager)
        {
            _templateManager = templateManager ?? throw new ArgumentNullException(nameof(templateManager));

            // 创建事件处理程序
            _searchKeywordChangedHandler = (oldValue, newValue) => FilterTemplates();
            _selectedCategoryChangedHandler = (oldValue, newValue) => FilterTemplates();
            _sortModeChangedHandler = (oldValue, newValue) => SortTemplates();

            // 监听搜索关键字变化
            SearchKeyword.OnValueChanged += _searchKeywordChangedHandler;

            // 监听分类选择变化
            SelectedCategoryIndex.OnValueChanged += _selectedCategoryChangedHandler;

            // 监听排序方式变化
            CurrentSortMode.OnValueChanged += _sortModeChangedHandler;
        }

        /// <summary>
        /// 初始化视图模型
        /// </summary>
        public void Initialize()
        {
            // 加载所有模板
            LoadTemplates();

            // 加载分类
            LoadCategories();

            // 应用过滤和排序
            FilterTemplates();
        }

        /// <summary>
        /// 清理视图模型资源
        /// </summary>
        public void Cleanup()
        {
            // 清理事件订阅
            SearchKeyword.OnValueChanged -= _searchKeywordChangedHandler;
            SelectedCategoryIndex.OnValueChanged -= _selectedCategoryChangedHandler;
            CurrentSortMode.OnValueChanged -= _sortModeChangedHandler;
        }

        /// <summary>
        /// 加载所有模板
        /// </summary>
        private void LoadTemplates()
        {
            try
            {
                // 获取所有模板
                var templates = _templateManager.GetAllTemplates();

                // 清空当前列表
                Templates.Clear();

                // 添加所有模板
                foreach (var template in templates)
                {
                    Templates.Add(template);
                }

                // 如果有模板，默认选中第一个
                if (Templates.Count > 0)
                {
                    SelectedTemplate.Value = Templates[0];
                }
                else
                {
                    SelectedTemplate.Value = null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"加载模板失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载模板分类
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                // 获取所有分类
                var categories = Templates.Value
                    .Select(t => t.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();

                // 添加"全部"选项
                categories.Insert(0, "全部");

                // 更新分类列表
                Categories.Value = categories.ToArray();

                // 默认选中"全部"
                SelectedCategoryIndex.Value = 0;
            }
            catch (Exception ex)
            {
                Debug.LogError($"加载模板分类失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 过滤模板
        /// </summary>
        private void FilterTemplates()
        {
            try
            {
                // 获取所有模板
                var allTemplates = _templateManager.GetAllTemplates();

                // 应用分类过滤
                if (SelectedCategoryIndex.Value > 0 && SelectedCategoryIndex.Value < Categories.Value.Length)
                {
                    string selectedCategory = Categories.Value[SelectedCategoryIndex.Value];
                    allTemplates = allTemplates.Where(t => t.Category == selectedCategory).ToList();
                }

                // 应用搜索过滤
                if (!string.IsNullOrEmpty(SearchKeyword.Value))
                {
                    string keyword = SearchKeyword.Value.ToLowerInvariant();
                    allTemplates = allTemplates.Where(t =>
                        t.Name.ToLowerInvariant().Contains(keyword) ||
                        t.Description.ToLowerInvariant().Contains(keyword) ||
                        t.Category.ToLowerInvariant().Contains(keyword)
                    ).ToList();
                }

                // 更新模板列表
                Templates.Clear();
                foreach (var template in allTemplates)
                {
                    Templates.Add(template);
                }

                // 应用排序
                SortTemplates();

                // 如果有模板，默认选中第一个
                if (Templates.Count > 0)
                {
                    // 如果之前选中的模板仍在列表中，保持选中
                    if (SelectedTemplate.Value != null && Templates.Value.Contains(SelectedTemplate.Value))
                    {
                        // 保持当前选中
                    }
                    else
                    {
                        SelectedTemplate.Value = Templates[0];
                    }
                }
                else
                {
                    SelectedTemplate.Value = null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"过滤模板失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 排序模板
        /// </summary>
        private void SortTemplates()
        {
            try
            {
                // 获取当前模板列表
                var templates = Templates.Value.ToList();

                // 根据排序方式排序
                switch (CurrentSortMode.Value)
                {
                    case SortMode.NameAsc:
                        templates = templates.OrderBy(t => t.Name).ToList();
                        break;
                    case SortMode.NameDesc:
                        templates = templates.OrderByDescending(t => t.Name).ToList();
                        break;
                    case SortMode.CategoryAsc:
                        templates = templates.OrderBy(t => t.Category).ThenBy(t => t.Name).ToList();
                        break;
                    case SortMode.CategoryDesc:
                        templates = templates.OrderByDescending(t => t.Category).ThenBy(t => t.Name).ToList();
                        break;
                    case SortMode.RecentlyUsed:
                        // 这里需要实现最近使用的排序逻辑
                        // 暂时按名称排序
                        templates = templates.OrderBy(t => t.Name).ToList();
                        break;
                }

                // 更新模板列表
                Templates.Clear();
                foreach (var template in templates)
                {
                    Templates.Add(template);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"排序模板失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 选择模板
        /// </summary>
        /// <param name="template">要选择的模板</param>
        public void SelectTemplate(IPackageTemplate template)
        {
            SelectedTemplate.Value = template;

            // 更新全局状态
            UIStateManager.Instance.UpdateState(state => state.SelectedTemplate = template);
        }

        /// <summary>
        /// 切换详情显示
        /// </summary>
        public void ToggleDetails()
        {
            ShowDetails.Value = !ShowDetails.Value;
        }

        /// <summary>
        /// 设置排序方式
        /// </summary>
        /// <param name="sortMode">排序方式</param>
        public void SetSortMode(SortMode sortMode)
        {
            CurrentSortMode.Value = sortMode;
        }
    }
}
