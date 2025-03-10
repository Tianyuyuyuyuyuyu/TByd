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
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

            // 计算可用宽度
            float availableWidth = EditorGUIUtility.currentViewWidth;

            // 调整宽度比例，列表占45%，详情占55%
            float listWidth = availableWidth * 0.45f;
            float detailsWidth = availableWidth * 0.55f;

            // 绘制模板列表
            DrawTemplateList(listWidth);

            // 为了让详情区域更靠右，添加一个小的负间距
            GUILayout.Space(-5);

            // 绘制详情区域 - 常驻显示，不再根据ShowDetails.Value判断
            DrawTemplateDetails(detailsWidth);

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制搜索和过滤区域
        /// </summary>
        private void DrawSearchAndFilterArea()
        {
            EditorGUILayout.BeginHorizontal();

            // 绘制搜索栏
            EditorGUI.BeginChangeCheck();
            string newSearchText = SearchBarControl.Draw(_viewModel.SearchKeyword.Value, "搜索模板...");
            if (EditorGUI.EndChangeCheck() && newSearchText != _viewModel.SearchKeyword.Value)
            {
                _viewModel.SearchKeyword.Value = newSearchText;
                // 强制重绘
                EditorApplication.delayCall += () => EditorApplication.QueuePlayerLoopUpdate();
            }

            GUILayout.Space(10);

            // 绘制分类下拉框
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("分类:", GUILayout.Width(40));
            EditorGUI.BeginChangeCheck();
            int newCategoryIndex = EditorGUILayout.Popup(_viewModel.SelectedCategoryIndex.Value, _viewModel.Categories.Value, GUILayout.Width(120));
            if (EditorGUI.EndChangeCheck() && newCategoryIndex != _viewModel.SelectedCategoryIndex.Value)
            {
                _viewModel.SelectedCategoryIndex.Value = newCategoryIndex;
                // 强制重绘
                EditorApplication.delayCall += () => EditorApplication.QueuePlayerLoopUpdate();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            // 绘制排序下拉框
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("排序:", GUILayout.Width(40));
            EditorGUI.BeginChangeCheck();
            int sortIndex = (int)_viewModel.CurrentSortMode.Value;
            int newSortIndex = EditorGUILayout.Popup(sortIndex, _sortOptions, GUILayout.Width(120));
            if (EditorGUI.EndChangeCheck() && newSortIndex != sortIndex)
            {
                _viewModel.SetSortMode((TemplateSelectionViewModel.SortMode)newSortIndex);
                // 强制重绘
                EditorApplication.delayCall += () => EditorApplication.QueuePlayerLoopUpdate();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            // 删除详情切换按钮，详情常驻显示

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制模板列表
        /// </summary>
        private void DrawTemplateList(float width)
        {
            // 使用传入的宽度参数
            EditorGUILayout.BeginVertical(GUILayout.Width(width - 10)); // 减去一些边距

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
        private void DrawTemplateDetails(float width)
        {
            // 如果没有选中模板，不显示详情
            if (_viewModel.SelectedTemplate.Value == null)
            {
                return;
            }

            IPackageTemplate template = _viewModel.SelectedTemplate.Value;

            // 增加宽度，使详情区域右侧边界更贴近窗口右侧边界
            // 这里增加宽度而不是减少，因为我们希望右侧边界更靠近窗口边缘
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(width + 5));

            // 绘制详情标题
            EditorGUILayout.LabelField("模板详情", PackageCreatorStyles.HeaderLabel);

            // 绘制分隔线
            PackageCreatorStyles.DrawSeparator();

            // 绘制详情内容
            _detailsScrollPosition = EditorGUILayout.BeginScrollView(_detailsScrollPosition);

            // 基本信息区域
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("基本信息", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("名称:", GUILayout.Width(50));
            EditorGUILayout.LabelField(template.Name, EditorStyles.wordWrappedLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("分类:", GUILayout.Width(50));
            EditorGUILayout.LabelField(template.Category, EditorStyles.wordWrappedLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("描述:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(template.Description, EditorStyles.wordWrappedLabel);

            EditorGUILayout.EndVertical();

            // 可用选项区域
            if (template.Options != null && template.Options.Count > 0)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("可用选项", EditorStyles.boldLabel);

                foreach (var option in template.Options)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(option.DisplayName + ":", GUILayout.Width(80));
                    EditorGUILayout.LabelField(option.Description, EditorStyles.wordWrappedMiniLabel);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(2);
                }

                EditorGUILayout.EndVertical();
            }

            // 目录结构区域
            if (template.Directories != null && template.Directories.Count > 0)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("目录结构", EditorStyles.boldLabel);

                foreach (var directory in template.Directories)
                {
                    EditorGUILayout.LabelField($"• {directory.RelativePath}", EditorStyles.miniLabel);
                }

                EditorGUILayout.EndVertical();
            }

            // 包含文件区域
            if (template.Files != null && template.Files.Count > 0)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("包含文件", EditorStyles.boldLabel);

                foreach (var file in template.Files)
                {
                    EditorGUILayout.LabelField($"• {file.RelativePath}", EditorStyles.miniLabel);
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }
    }
}
