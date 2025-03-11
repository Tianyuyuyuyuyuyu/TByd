using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.ViewModels;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 摘要页面，显示整个包创建过程的配置概览
    /// </summary>
    public class SummaryPage : BasePage
    {
        // 视图模型
        private readonly SummaryViewModel _viewModel;

        // 滚动位置
        private Vector2 _scrollPosition;

        // 折叠状态
        private bool _basicInfoExpanded = true;
        private bool _dependenciesExpanded = true;
        private bool _customVariablesExpanded = true;
        private bool _directoryOptionsExpanded = true;
        private bool _validationResultsExpanded = true;

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => "摘要";

        /// <summary>
        /// 判断当前页面是否有效
        /// </summary>
        public override bool IsValid => _viewModel.IsValid;

        /// <summary>
        /// 是否显示创建按钮
        /// </summary>
        public bool ShowCreateButton => true;

        /// <summary>
        /// 创建按钮文本
        /// </summary>
        public string CreateButtonText => "创建包";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        /// <param name="templateManager">模板管理器</param>
        public SummaryPage(IConfigManager configManager, ITemplateManager templateManager)
        {
            _viewModel = new SummaryViewModel(configManager, templateManager);
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
            GUILayout.Space(10);

            EditorGUILayout.LabelField("摘要页面", PackageCreatorStyles.PageTitleStyle);
            EditorGUILayout.LabelField("请检查以下配置信息，确认无误后点击\"创建包\"按钮", PackageCreatorStyles.PageDescriptionStyle);

            GUILayout.Space(10);

            // 显示验证结果
            DrawValidationResults();

            GUILayout.Space(10);

            // 使用滚动视图显示摘要信息
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 基本信息段落
            _basicInfoExpanded = EditorGUILayout.Foldout(_basicInfoExpanded, "基本信息", true);
            if (_basicInfoExpanded)
            {
                EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);
                DrawInfoSection(_viewModel.GetBasicInfoSummary());
                EditorGUILayout.EndVertical();
            }

            GUILayout.Space(5);

            // 依赖项段落
            _dependenciesExpanded = EditorGUILayout.Foldout(_dependenciesExpanded, "依赖项", true);
            if (_dependenciesExpanded)
            {
                EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);
                DrawInfoSection(_viewModel.GetDependenciesSummary());
                EditorGUILayout.EndVertical();
            }

            GUILayout.Space(5);

            // 自定义变量段落
            _customVariablesExpanded = EditorGUILayout.Foldout(_customVariablesExpanded, "自定义变量", true);
            if (_customVariablesExpanded)
            {
                EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);
                DrawInfoSection(_viewModel.GetCustomVariablesSummary());
                EditorGUILayout.EndVertical();
            }

            GUILayout.Space(5);

            // 目录选项段落
            _directoryOptionsExpanded = EditorGUILayout.Foldout(_directoryOptionsExpanded, "目录选项", true);
            if (_directoryOptionsExpanded)
            {
                EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);
                DrawInfoSection(_viewModel.GetDirectoryOptionsSummary());
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();

            GUILayout.Space(10);

            // 底部提示
            if (_viewModel.IsValid)
            {
                EditorGUILayout.HelpBox("所有配置有效，可以创建包。", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox("请先修复上述错误，然后再创建包。", MessageType.Error);
            }
        }

        /// <summary>
        /// 绘制信息段落内容
        /// </summary>
        /// <param name="content">段落内容</param>
        private void DrawInfoSection(string content)
        {
            EditorGUILayout.BeginVertical();

            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.wordWrap = true;
            style.richText = true;

            EditorGUILayout.LabelField(content, style);

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制验证结果
        /// </summary>
        private void DrawValidationResults()
        {
            if (_viewModel.ValidationErrors.Count > 0)
            {
                _validationResultsExpanded = EditorGUILayout.Foldout(_validationResultsExpanded, "验证结果", true);
                if (_validationResultsExpanded)
                {
                    EditorGUILayout.BeginVertical(PackageCreatorStyles.ErrorBoxStyle);

                    foreach (var error in _viewModel.ValidationErrors)
                    {
                        EditorGUILayout.HelpBox(error, MessageType.Error);
                    }

                    EditorGUILayout.EndVertical();
                }
            }
        }
    }
}
