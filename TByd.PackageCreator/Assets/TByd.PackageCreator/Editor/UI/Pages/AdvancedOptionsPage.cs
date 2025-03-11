using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.ViewModels;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 高级选项页面，用于管理自定义变量和高级设置
    /// </summary>
    public class AdvancedOptionsPage : BasePage
    {
        // 视图模型
        private readonly AdvancedOptionsViewModel _viewModel;

        // 滚动位置
        private Vector2 _customVariablesScrollPosition;
        private Vector2 _previewScrollPosition;

        // 页面分组展开状态
        private bool _customVariablesExpanded = true;
        private bool _directoryOptionsExpanded = true;
        private bool _previewExpanded = false;

        // 变量编辑状态
        private Dictionary<string, bool> _variableEditStates = new Dictionary<string, bool>();
        private Dictionary<string, string> _variableEditValues = new Dictionary<string, string>();

        // 需要重绘标记
        private bool _needsRepaint;

        // 预览模板
        private string _previewTemplate =
            "包名: ${PACKAGE_NAME}\n" +
            "命名空间: ${NAMESPACE}\n" +
            "类名: ${CLASS_NAME}\n" +
            "作者: ${AUTHOR}\n" +
            "日期: ${DATE}";

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => "高级选项";

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public override bool IsValid => true; // 高级选项页面始终有效，不影响包创建流程

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public AdvancedOptionsPage(IConfigManager configManager)
        {
            _viewModel = new AdvancedOptionsViewModel(configManager);
        }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public override void OnEnter()
        {
            // 初始化视图模型
            _viewModel.Initialize();

            // 重置滚动位置
            _customVariablesScrollPosition = Vector2.zero;
            _previewScrollPosition = Vector2.zero;

            // 清空编辑状态
            _variableEditStates.Clear();
            _variableEditValues.Clear();

            // 设置重绘标记
            _needsRepaint = true;
        }

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        public override void OnExit()
        {
            // 清理视图模型
            _viewModel.Cleanup();

            // 清空编辑状态
            _variableEditStates.Clear();
            _variableEditValues.Clear();
        }

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        public override void Draw()
        {
            // 如果配置为空，显示错误信息
            if (_viewModel.PackageConfig == null)
            {
                DrawHelpBox("配置数据未正确初始化，请返回选择模板页面重新开始。", MessageType.Error);
                return;
            }

            // 绘制页面标题
            DrawTitle();

            // 绘制搜索栏
            DrawSearchBar();

            EditorGUILayout.Space(10);

            // 绘制自定义变量区域
            DrawCustomVariablesSection();

            EditorGUILayout.Space(10);

            // 绘制目录选项区域
            DrawDirectoryOptionsSection();

            EditorGUILayout.Space(10);

            // 绘制预览区域
            DrawPreviewSection();

            // 如果有错误信息，显示错误
            if (!string.IsNullOrEmpty(_viewModel.ErrorMessage))
            {
                EditorGUILayout.Space(10);
                DrawHelpBox(_viewModel.ErrorMessage, MessageType.Error);
            }

            // 处理重绘请求
            if (_needsRepaint)
            {
                _needsRepaint = false;
                EditorWindow.GetWindow<EditorWindow>().Repaint();
            }
        }

        /// <summary>
        /// 绘制搜索栏
        /// </summary>
        private void DrawSearchBar()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("搜索:", GUILayout.Width(50));

            EditorGUI.BeginChangeCheck();
            string newSearchKeyword = EditorGUILayout.TextField(_viewModel.SearchKeyword);
            if (EditorGUI.EndChangeCheck() && newSearchKeyword != _viewModel.SearchKeyword)
            {
                _viewModel.SearchKeyword = newSearchKeyword;
                _needsRepaint = true;
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制自定义变量区域
        /// </summary>
        private void DrawCustomVariablesSection()
        {
            // 使用折叠面板绘制自定义变量区域
            _customVariablesExpanded = EditorGUILayout.Foldout(_customVariablesExpanded, "自定义变量", true, PackageCreatorStyles.FoldoutStyle);
            if (_customVariablesExpanded)
            {
                BeginVerticalGroup();

                // 绘制添加变量按钮
                EditorGUILayout.BeginHorizontal();

                if (!_viewModel.IsAddingVariable)
                {
                    if (GUILayout.Button("添加变量", GUILayout.Width(120)))
                    {
                        _viewModel.IsAddingVariable = true;
                        _needsRepaint = true;
                    }
                }

                GUILayout.FlexibleSpace();

                // 显示变量数量
                EditorGUILayout.LabelField($"共 {_viewModel.CustomVariables.Count} 个变量", EditorStyles.miniLabel);

                EditorGUILayout.EndHorizontal();

                // 如果处于添加状态，绘制添加表单
                if (_viewModel.IsAddingVariable)
                {
                    EditorGUILayout.Space(5);
                    DrawAddVariableForm();
                    EditorGUILayout.Space(5);
                }

                EditorGUILayout.Space(5);

                // 绘制变量列表
                _customVariablesScrollPosition = EditorGUILayout.BeginScrollView(_customVariablesScrollPosition, GUILayout.Height(200));

                var filteredVariables = _viewModel.FilteredCustomVariables;
                if (filteredVariables != null && filteredVariables.Count > 0)
                {
                    foreach (var kv in filteredVariables.ToList())
                    {
                        DrawVariableItem(kv.Key, kv.Value);
                        EditorGUILayout.Space(5);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("没有自定义变量。您可以点击上方的\"添加变量\"按钮添加新变量。", MessageType.Info);
                }

                EditorGUILayout.EndScrollView();

                EndVerticalGroup();
            }
        }

        /// <summary>
        /// 绘制目录选项区域
        /// </summary>
        private void DrawDirectoryOptionsSection()
        {
            // 使用折叠面板绘制目录选项区域
            _directoryOptionsExpanded = EditorGUILayout.Foldout(_directoryOptionsExpanded, "目录选项", true, PackageCreatorStyles.FoldoutStyle);
            if (_directoryOptionsExpanded)
            {
                BeginVerticalGroup();

                EditorGUILayout.HelpBox("选择要包含在生成包中的目录结构。", MessageType.Info);

                EditorGUILayout.Space(5);

                // 包含Tests
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                bool includeTests = EditorGUILayout.Toggle("包含Tests目录", _viewModel.IncludeTests);
                if (EditorGUI.EndChangeCheck() && includeTests != _viewModel.IncludeTests)
                {
                    _viewModel.IncludeTests = includeTests;
                    _needsRepaint = true;
                }
                EditorGUILayout.EndHorizontal();

                // 包含Samples
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                bool includeSamples = EditorGUILayout.Toggle("包含Samples目录", _viewModel.IncludeSamples);
                if (EditorGUI.EndChangeCheck() && includeSamples != _viewModel.IncludeSamples)
                {
                    _viewModel.IncludeSamples = includeSamples;
                    _needsRepaint = true;
                }
                EditorGUILayout.EndHorizontal();

                // 包含Documentation
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                bool includeDocumentation = EditorGUILayout.Toggle("包含Documentation目录", _viewModel.IncludeDocumentation);
                if (EditorGUI.EndChangeCheck() && includeDocumentation != _viewModel.IncludeDocumentation)
                {
                    _viewModel.IncludeDocumentation = includeDocumentation;
                    _needsRepaint = true;
                }
                EditorGUILayout.EndHorizontal();

                EndVerticalGroup();
            }
        }

        /// <summary>
        /// 绘制预览区域
        /// </summary>
        private void DrawPreviewSection()
        {
            // 使用折叠面板绘制预览区域
            _previewExpanded = EditorGUILayout.Foldout(_previewExpanded, "变量替换预览", true, PackageCreatorStyles.FoldoutStyle);
            if (_previewExpanded)
            {
                BeginVerticalGroup();

                EditorGUILayout.HelpBox("在下方输入包含变量的文本，查看替换效果。变量格式为 ${VARIABLE_NAME}。", MessageType.Info);

                EditorGUILayout.Space(5);

                // 模板输入
                EditorGUILayout.LabelField("模板:", EditorStyles.boldLabel);
                EditorGUI.BeginChangeCheck();
                string newTemplate = EditorGUILayout.TextArea(_previewTemplate, GUILayout.Height(100));
                if (EditorGUI.EndChangeCheck() && newTemplate != _previewTemplate)
                {
                    _previewTemplate = newTemplate;
                    _needsRepaint = true;
                }

                EditorGUILayout.Space(10);

                // 预览输出
                EditorGUILayout.LabelField("预览:", EditorStyles.boldLabel);
                string previewResult = _viewModel.GetPreviewString(_previewTemplate);
                GUI.enabled = false;
                EditorGUILayout.TextArea(previewResult, GUILayout.Height(100));
                GUI.enabled = true;

                EndVerticalGroup();
            }
        }

        /// <summary>
        /// 绘制添加变量表单
        /// </summary>
        private void DrawAddVariableForm()
        {
            BeginVerticalGroup("添加新变量");

            // 变量名输入
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("变量名:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
            EditorGUI.BeginChangeCheck();
            string newKey = EditorGUILayout.TextField(_viewModel.NewVariableKey);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.NewVariableKey = newKey;
            }
            EditorGUILayout.EndHorizontal();

            // 变量值输入
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("变量值:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
            EditorGUI.BeginChangeCheck();
            string newValue = EditorGUILayout.TextField(_viewModel.NewVariableValue);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.NewVariableValue = newValue;
            }
            EditorGUILayout.EndHorizontal();

            // 提示信息
            EditorGUILayout.HelpBox("变量名必须以字母开头，只能包含字母、数字和下划线。", MessageType.Info);

            // 按钮行
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            // 添加按钮
            if (GUILayout.Button("添加", GUILayout.Width(80)))
            {
                if (_viewModel.AddVariable())
                {
                    _needsRepaint = true;
                }
            }

            // 取消按钮
            if (GUILayout.Button("取消", GUILayout.Width(80)))
            {
                _viewModel.IsAddingVariable = false;
                _needsRepaint = true;
            }

            EditorGUILayout.EndHorizontal();

            EndVerticalGroup();
        }

        /// <summary>
        /// 绘制变量项
        /// </summary>
        /// <param name="key">变量键</param>
        /// <param name="value">变量值</param>
        private void DrawVariableItem(string key, string value)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            // 确保有此键的编辑状态
            if (!_variableEditStates.ContainsKey(key))
            {
                _variableEditStates[key] = false;
                _variableEditValues[key] = value;
            }

            if (_variableEditStates[key])
            {
                // 编辑状态

                // 变量名输入
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("变量名:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
                string editKey = key;
                EditorGUI.BeginChangeCheck();
                editKey = EditorGUILayout.TextField(editKey);
                if (EditorGUI.EndChangeCheck())
                {
                    _variableEditValues[key + "_key"] = editKey;
                }
                EditorGUILayout.EndHorizontal();

                // 变量值输入
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("变量值:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
                string editValue = _variableEditValues[key];
                EditorGUI.BeginChangeCheck();
                editValue = EditorGUILayout.TextField(editValue);
                if (EditorGUI.EndChangeCheck())
                {
                    _variableEditValues[key] = editValue;
                }
                EditorGUILayout.EndHorizontal();

                // 按钮行
                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                // 保存按钮
                if (GUILayout.Button("保存", GUILayout.Width(80)))
                {
                    string newKey = _variableEditValues.ContainsKey(key + "_key") ? _variableEditValues[key + "_key"] : key;
                    string newValue = _variableEditValues[key];

                    if (_viewModel.UpdateVariable(key, newKey, newValue))
                    {
                        // 更新成功，重置编辑状态
                        _variableEditStates.Remove(key);
                        _variableEditValues.Remove(key);
                        if (_variableEditValues.ContainsKey(key + "_key"))
                            _variableEditValues.Remove(key + "_key");

                        // 如果键名变更，为新键建立编辑状态
                        if (key != newKey)
                        {
                            _variableEditStates[newKey] = false;
                            _variableEditValues[newKey] = newValue;
                        }

                        _needsRepaint = true;
                    }
                }

                // 取消按钮
                if (GUILayout.Button("取消", GUILayout.Width(80)))
                {
                    _variableEditStates[key] = false;
                    _variableEditValues[key] = value;
                    if (_variableEditValues.ContainsKey(key + "_key"))
                        _variableEditValues.Remove(key + "_key");
                    _needsRepaint = true;
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                // 显示状态

                EditorGUILayout.BeginHorizontal();

                // 变量名显示
                EditorGUILayout.LabelField(key, EditorStyles.boldLabel);

                GUILayout.FlexibleSpace();

                // 变量值显示
                EditorGUILayout.LabelField(value, GUILayout.MinWidth(100));

                // 编辑按钮
                if (GUILayout.Button("编辑", GUILayout.Width(60)))
                {
                    _variableEditStates[key] = true;
                    _variableEditValues[key] = value;
                    _needsRepaint = true;
                }

                // 删除按钮
                if (GUILayout.Button("删除", GUILayout.Width(60)))
                {
                    _viewModel.RemoveVariable(key);
                    _variableEditStates.Remove(key);
                    _variableEditValues.Remove(key);
                    _needsRepaint = true;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }
}
