using System;
using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.UI.Controls;
using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.Utils;
using TByd.PackageCreator.Editor.UI.ViewModels;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 模板选择页面
    /// </summary>
    public class TemplateSelectionPage : BasePage
    {
        // 视图模型
        private readonly TemplateSelectionViewModel _viewModel;

        // 滚动位置
        private Vector2 _scrollPosition;

        // 详情滚动位置
        private Vector2 _detailsScrollPosition;

        // 排序选项
        private readonly string[] _sortOptions = new string[]
        {
            "名称 (A-Z)",
            "名称 (Z-A)",
            "分类 (A-Z)",
            "分类 (Z-A)",
            "最近使用"
        };

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => "选择模板";

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public override bool IsValid => _viewModel.SelectedTemplate.Value != null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templateManager">模板管理器</param>
        public TemplateSelectionPage(ITemplateManager templateManager)
        {
            _viewModel = new TemplateSelectionViewModel(templateManager);
        }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public override void OnEnter()
        {
            _viewModel.Initialize();
        }

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        public override void OnExit()
        {
            _viewModel.Cleanup();
        }

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        public override void Draw()
        {
            // 绘制搜索和过滤区域
            DrawSearchAndFilterArea();

            // 绘制分隔线
            DrawSeparator();

            // 绘制模板列表和详情区域
            EditorGUILayout.BeginHorizontal();

            // 绘制模板列表
            DrawTemplateList();

            // 绘制详情区域
            if (_viewModel.ShowDetails.Value)
            {
                DrawTemplateDetails();
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制搜索和过滤区域
        /// </summary>
        private void DrawSearchAndFilterArea()
        {
            EditorGUILayout.BeginHorizontal();

            // 绘制搜索栏
            string newSearchText = SearchBarControl.Draw(_viewModel.SearchKeyword.Value, "搜索模板...");
            if (newSearchText != _viewModel.SearchKeyword.Value)
            {
                _viewModel.SearchKeyword.Value = newSearchText;
            }

            GUILayout.Space(10);

            // 绘制分类下拉框
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("分类:", GUILayout.Width(40));
            int newCategoryIndex = EditorGUILayout.Popup(_viewModel.SelectedCategoryIndex.Value, _viewModel.Categories.Value, GUILayout.Width(120));
            if (newCategoryIndex != _viewModel.SelectedCategoryIndex.Value)
            {
                _viewModel.SelectedCategoryIndex.Value = newCategoryIndex;
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            // 绘制排序下拉框
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("排序:", GUILayout.Width(40));
            int sortIndex = (int)_viewModel.CurrentSortMode.Value;
            int newSortIndex = EditorGUILayout.Popup(sortIndex, _sortOptions, GUILayout.Width(120));
            if (newSortIndex != sortIndex)
            {
                _viewModel.SetSortMode((TemplateSelectionViewModel.SortMode)newSortIndex);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            // 绘制详情切换按钮
            bool showDetails = GUILayout.Toggle(_viewModel.ShowDetails.Value, "显示详情", EditorStyles.miniButton, GUILayout.Width(80));
            if (showDetails != _viewModel.ShowDetails.Value)
            {
                _viewModel.ToggleDetails();
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制模板列表
        /// </summary>
        private void DrawTemplateList()
        {
            float listWidth = _viewModel.ShowDetails.Value ? EditorGUIUtility.currentViewWidth * 0.6f : EditorGUIUtility.currentViewWidth - 20;

            EditorGUILayout.BeginVertical(GUILayout.Width(listWidth));

            // 绘制模板数量信息
            EditorGUILayout.LabelField($"找到 {_viewModel.Templates.Count} 个模板", EditorStyles.miniLabel);

            // 绘制模板列表
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            if (_viewModel.Templates.Count == 0)
            {
                // 没有模板时显示提示
                EditorGUILayout.HelpBox("没有找到匹配的模板。", MessageType.Info);
            }
            else
            {
                // 绘制模板列表
                for (int i = 0; i < _viewModel.Templates.Count; i++)
                {
                    IPackageTemplate template = _viewModel.Templates[i];
                    bool isSelected = _viewModel.SelectedTemplate.Value == template;

                    // 创建模板卡片控件实例
                    var cardControl = new TemplateCardControl(
                        template.Name,
                        template.Description,
                        template.Category,
                        template.Icon,
                        isSelected
                    );

                    // 使用模板卡片控件
                    if (cardControl.DrawCompact())
                    {
                        _viewModel.SelectTemplate(template);
                    }
                }
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制模板详情
        /// </summary>
        private void DrawTemplateDetails()
        {
            // 如果没有选中模板，不显示详情
            if (_viewModel.SelectedTemplate.Value == null)
            {
                return;
            }

            IPackageTemplate template = _viewModel.SelectedTemplate.Value;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.4f - 20));

            // 绘制详情标题
            EditorGUILayout.LabelField("模板详情", PackageCreatorStyles.HeaderLabel);

            // 绘制分隔线
            PackageCreatorStyles.DrawSeparator();

            // 绘制详情内容
            _detailsScrollPosition = EditorGUILayout.BeginScrollView(_detailsScrollPosition);

            // 绘制基本信息
            EditorGUILayout.LabelField("名称:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(template.Name);

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("分类:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(template.Category);

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("描述:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(template.Description, EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(10);

            // 绘制模板选项
            if (template.Options != null && template.Options.Count > 0)
            {
                EditorGUILayout.LabelField("可用选项:", EditorStyles.boldLabel);

                foreach (var option in template.Options)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(option.DisplayName, EditorStyles.boldLabel);
                    EditorGUILayout.LabelField(option.Description, EditorStyles.wordWrappedLabel);

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.Space(5);
                }
            }

            EditorGUILayout.Space(10);

            // 绘制模板结构
            if (template.Directories != null && template.Directories.Count > 0)
            {
                EditorGUILayout.LabelField("目录结构:", EditorStyles.boldLabel);

                foreach (var directory in template.Directories)
                {
                    EditorGUILayout.LabelField($"• {directory.RelativePath}", EditorStyles.miniLabel);
                }
            }

            EditorGUILayout.Space(10);

            // 绘制模板文件
            if (template.Files != null && template.Files.Count > 0)
            {
                EditorGUILayout.LabelField("包含文件:", EditorStyles.boldLabel);

                foreach (var file in template.Files)
                {
                    EditorGUILayout.LabelField($"• {file.RelativePath}", EditorStyles.miniLabel);
                }
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }
    }
}
