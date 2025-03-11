using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Controls;
using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.ViewModels;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 依赖配置页面，用于管理包的依赖项
    /// </summary>
    public class DependenciesPage : BasePage
    {
        // 视图模型
        private readonly DependenciesViewModel _viewModel;

        // 滚动位置
        private Vector2 _dependenciesScrollPosition;
        private Vector2 _recommendedScrollPosition;

        // 页面分组展开状态
        private bool _currentDependenciesExpanded = true;
        private bool _recommendedDependenciesExpanded = true;

        // 依赖项控件缓存
        private readonly Dictionary<PackageDependency, DependencyItem> _dependencyControls = new Dictionary<PackageDependency, DependencyItem>();

        // 需要重绘标记
        private bool _needsRepaint;

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => "依赖配置";

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public override bool IsValid => true; // 依赖页面始终有效，不影响包创建流程

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public DependenciesPage(IConfigManager configManager)
        {
            _viewModel = new DependenciesViewModel(configManager);
        }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public override void OnEnter()
        {
            // 初始化视图模型
            _viewModel.Initialize();

            // 重置滚动位置
            _dependenciesScrollPosition = Vector2.zero;
            _recommendedScrollPosition = Vector2.zero;

            // 清空控件缓存
            _dependencyControls.Clear();

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

            // 清空控件缓存
            _dependencyControls.Clear();
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

            // 绘制分隔线
            DrawSeparator();

            // 绘制当前依赖区域
            DrawCurrentDependenciesSection();

            EditorGUILayout.Space(10);

            // 绘制推荐依赖区域
            DrawRecommendedDependenciesSection();

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
        /// 绘制当前依赖区域
        /// </summary>
        private void DrawCurrentDependenciesSection()
        {
            // 使用折叠面板绘制当前依赖区域
            _currentDependenciesExpanded = EditorGUILayout.Foldout(_currentDependenciesExpanded, "当前依赖项", true, PackageCreatorStyles.FoldoutStyle);
            if (_currentDependenciesExpanded)
            {
                BeginVerticalGroup();

                // 绘制添加依赖按钮
                EditorGUILayout.BeginHorizontal();

                if (!_viewModel.IsAddingDependency)
                {
                    if (GUILayout.Button("添加依赖项", GUILayout.Width(120)))
                    {
                        _viewModel.IsAddingDependency = true;
                        _needsRepaint = true;
                    }
                }

                GUILayout.FlexibleSpace();

                // 显示依赖数量
                EditorGUILayout.LabelField($"共 {_viewModel.Dependencies.Count} 个依赖项", EditorStyles.miniLabel);

                EditorGUILayout.EndHorizontal();

                // 如果处于添加状态，绘制添加表单
                if (_viewModel.IsAddingDependency)
                {
                    EditorGUILayout.Space(5);
                    DrawAddDependencyForm();
                    EditorGUILayout.Space(5);
                }

                EditorGUILayout.Space(5);

                // 绘制依赖列表
                _dependenciesScrollPosition = EditorGUILayout.BeginScrollView(_dependenciesScrollPosition, GUILayout.Height(200));

                var filteredDependencies = _viewModel.FilteredDependencies;
                if (filteredDependencies != null && filteredDependencies.Count > 0)
                {
                    foreach (var dependency in filteredDependencies)
                    {
                        // 确保每个依赖项有对应的控件
                        if (!_dependencyControls.TryGetValue(dependency, out var control))
                        {
                            control = new DependencyItem(
                                dependency,
                                RemoveDependency,
                                UpdateDependency);
                            _dependencyControls[dependency] = control;
                        }

                        // 绘制依赖项控件
                        if (control.Draw())
                        {
                            _needsRepaint = true;
                        }

                        EditorGUILayout.Space(5);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("没有依赖项。您可以点击上方的\"添加依赖项\"按钮添加新依赖，或从下方的推荐依赖中选择。", MessageType.Info);
                }

                EditorGUILayout.EndScrollView();

                EndVerticalGroup();
            }
        }

        /// <summary>
        /// 绘制推荐依赖区域
        /// </summary>
        private void DrawRecommendedDependenciesSection()
        {
            // 使用折叠面板绘制推荐依赖区域
            _recommendedDependenciesExpanded = EditorGUILayout.Foldout(_recommendedDependenciesExpanded, "推荐依赖项", true, PackageCreatorStyles.FoldoutStyle);
            if (_recommendedDependenciesExpanded)
            {
                BeginVerticalGroup();

                EditorGUILayout.LabelField("以下是常用的Unity包依赖，点击\"添加\"按钮可快速添加到您的项目中。", EditorStyles.wordWrappedLabel);

                EditorGUILayout.Space(5);

                // 绘制推荐依赖列表
                _recommendedScrollPosition = EditorGUILayout.BeginScrollView(_recommendedScrollPosition, GUILayout.Height(200));

                var filteredRecommended = _viewModel.FilteredRecommendedPackages;
                if (filteredRecommended != null && filteredRecommended.Count > 0)
                {
                    foreach (var recommended in filteredRecommended)
                    {
                        // 检查该推荐依赖是否已添加
                        bool isAdded = _viewModel.Dependencies.Exists(d => d.Id == recommended.Id);

                        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

                        // 包ID
                        EditorGUILayout.LabelField(recommended.Id, EditorStyles.boldLabel);

                        GUILayout.FlexibleSpace();

                        // 版本
                        EditorGUILayout.LabelField(recommended.Version, GUILayout.Width(60));

                        // 添加按钮
                        if (isAdded)
                        {
                            EditorGUI.BeginDisabledGroup(true);
                            GUILayout.Button("已添加", GUILayout.Width(60));
                            EditorGUI.EndDisabledGroup();
                        }
                        else
                        {
                            if (GUILayout.Button("添加", GUILayout.Width(60)))
                            {
                                if (_viewModel.AddRecommendedDependency(recommended))
                                {
                                    _needsRepaint = true;
                                }
                            }
                        }

                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space(2);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("没有找到匹配的推荐依赖项。", MessageType.Info);
                }

                EditorGUILayout.EndScrollView();

                EndVerticalGroup();
            }
        }

        /// <summary>
        /// 绘制添加依赖表单
        /// </summary>
        private void DrawAddDependencyForm()
        {
            BeginVerticalGroup("添加新依赖");

            // 包ID输入
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("包ID:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
            EditorGUI.BeginChangeCheck();
            string newId = EditorGUILayout.TextField(_viewModel.NewDependencyId);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.NewDependencyId = newId;
            }
            EditorGUILayout.EndHorizontal();

            // 版本输入
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("版本:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
            EditorGUI.BeginChangeCheck();
            string newVersion = EditorGUILayout.TextField(_viewModel.NewDependencyVersion);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.NewDependencyVersion = newVersion;
            }
            EditorGUILayout.EndHorizontal();

            // 提示信息
            EditorGUILayout.HelpBox("包ID应使用反向域名格式，例如: com.company.package\n版本应使用语义化版本格式，例如: 1.0.0, >=1.0.0, 1.0.x 等", MessageType.Info);

            // 按钮行
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            // 添加按钮
            if (GUILayout.Button("添加", GUILayout.Width(80)))
            {
                if (_viewModel.AddDependency())
                {
                    _needsRepaint = true;
                }
            }

            // 取消按钮
            if (GUILayout.Button("取消", GUILayout.Width(80)))
            {
                _viewModel.IsAddingDependency = false;
                _needsRepaint = true;
            }

            EditorGUILayout.EndHorizontal();

            EndVerticalGroup();
        }

        /// <summary>
        /// 移除依赖回调
        /// </summary>
        /// <param name="dependency">要移除的依赖</param>
        private void RemoveDependency(PackageDependency dependency)
        {
            _viewModel.RemoveDependency(dependency);
            _dependencyControls.Remove(dependency);
            _needsRepaint = true;
        }

        /// <summary>
        /// 更新依赖回调
        /// </summary>
        /// <param name="oldDependency">原依赖</param>
        /// <param name="newId">新ID</param>
        /// <param name="newVersion">新版本</param>
        private void UpdateDependency(PackageDependency oldDependency, string newId, string newVersion)
        {
            if (_viewModel.UpdateDependency(oldDependency, newId, newVersion))
            {
                // 更新成功后，需要更新控件缓存
                _dependencyControls.Remove(oldDependency);

                // 获取更新后的依赖项
                foreach (var dependency in _viewModel.Dependencies)
                {
                    if (dependency.Id == newId && dependency.Version == newVersion)
                    {
                        // 为新依赖项创建控件
                        _dependencyControls[dependency] = new DependencyItem(
                            dependency,
                            RemoveDependency,
                            UpdateDependency);
                        break;
                    }
                }

                _needsRepaint = true;
            }
        }
    }
}
