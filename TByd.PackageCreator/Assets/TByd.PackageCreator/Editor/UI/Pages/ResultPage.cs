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
    /// 结果页面，显示包创建的结果和相关信息
    /// </summary>
    public class ResultPage : BasePage
    {
        // 视图模型
        private readonly ResultViewModel _viewModel;

        // 滚动位置
        private Vector2 _scrollPosition;
        private Vector2 _filesScrollPosition;

        // 折叠状态
        private bool _packageInfoExpanded = true;
        private bool _createdFilesExpanded = true;

        // 文件过滤
        private string _fileFilter = string.Empty;
        private List<string> _filteredFiles = new List<string>();

        // 错误显示
        private bool _showDetailedErrors = false;

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => "创建结果";

        /// <summary>
        /// 判断当前页面是否有效
        /// </summary>
        public override bool IsValid => true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public ResultPage(IConfigManager configManager)
        {
            _viewModel = new ResultViewModel(configManager);
        }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public override void OnEnter()
        {
            _viewModel.Initialize();
            FilterFiles();
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

            // 绘制标题
            if (_viewModel.IsCreationSuccessful)
            {
                EditorGUILayout.LabelField("包创建成功！", PackageCreatorStyles.PageTitleStyle);
                EditorGUILayout.LabelField("您的UPM包已成功创建，可以在下方查看详细信息。", PackageCreatorStyles.PageDescriptionStyle);
            }
            else
            {
                EditorGUILayout.LabelField("包创建失败", PackageCreatorStyles.PageTitleStyle);
                EditorGUILayout.LabelField("创建过程中发生错误，请查看下方详细信息。", PackageCreatorStyles.PageDescriptionStyle);

                // 显示错误信息
                if (!string.IsNullOrEmpty(_viewModel.ErrorMessage))
                {
                    GUILayout.Space(10);
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    // 使用更引人注意的样式显示主要错误信息
                    GUIStyle errorStyle = new GUIStyle(EditorStyles.boldLabel);
                    errorStyle.normal.textColor = Color.red;
                    errorStyle.wordWrap = true;

                    EditorGUILayout.LabelField("错误信息:", errorStyle);
                    EditorGUILayout.LabelField(_viewModel.ErrorMessage, EditorStyles.wordWrappedLabel);

                    EditorGUILayout.EndVertical();

                    // 添加更多错误信息的详细显示
                    GUILayout.Space(10);
                    _showDetailedErrors = EditorGUILayout.Foldout(_showDetailedErrors, "详细错误信息", true);

                    if (_showDetailedErrors)
                    {
                        EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);

                        // 获取验证错误并显示
                        if (_viewModel.CreationResult != null)
                        {
                            var errors = _viewModel.GetValidationMessages(ValidationMessageLevel.Error);
                            var warnings = _viewModel.GetValidationMessages(ValidationMessageLevel.Warning);
                            var infos = _viewModel.GetValidationMessages(ValidationMessageLevel.Info);

                            if (errors.Count > 0)
                            {
                                EditorGUILayout.LabelField("错误:", EditorStyles.boldLabel);
                                foreach (var error in errors)
                                {
                                    EditorGUILayout.HelpBox(error.Message, MessageType.Error);
                                }
                            }

                            if (warnings.Count > 0)
                            {
                                EditorGUILayout.LabelField("警告:", EditorStyles.boldLabel);
                                foreach (var warning in warnings)
                                {
                                    EditorGUILayout.HelpBox(warning.Message, MessageType.Warning);
                                }
                            }

                            if (infos.Count > 0)
                            {
                                EditorGUILayout.LabelField("信息:", EditorStyles.boldLabel);
                                foreach (var info in infos)
                                {
                                    EditorGUILayout.HelpBox(info.Message, MessageType.Info);
                                }
                            }

                            if (errors.Count == 0 && warnings.Count == 0 && infos.Count == 0)
                            {
                                EditorGUILayout.LabelField("没有捕获到具体错误信息。请检查Unity控制台以获取更多细节。");
                            }
                        }
                        else
                        {
                            EditorGUILayout.LabelField("没有验证结果信息。请检查Unity控制台以获取更多细节。");
                        }

                        EditorGUILayout.EndVertical();
                    }
                }
            }

            GUILayout.Space(10);

            // 使用滚动视图显示结果信息
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 显示验证消息
            DrawValidationMessages();

            GUILayout.Space(10);

            // 包信息段落
            _packageInfoExpanded = EditorGUILayout.Foldout(_packageInfoExpanded, "包信息", true);
            if (_packageInfoExpanded)
            {
                EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);
                DrawInfoSection(_viewModel.GetPackageSummary());

                // 显示包路径
                if (!string.IsNullOrEmpty(_viewModel.PackagePath))
                {
                    GUILayout.Space(5);
                    EditorGUILayout.LabelField("包路径:", EditorStyles.boldLabel);
                    EditorGUILayout.SelectableLabel(_viewModel.PackagePath, EditorStyles.textField, GUILayout.Height(20));

                    GUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();

                    // 添加调试按钮，仅在开发模式下显示
#if UNITY_EDITOR && DEVELOPMENT_BUILD
                    if (GUILayout.Button("调试配置数据", GUILayout.Height(30)))
                    {
                        string configInfo = "";
                        var config = _viewModel.PackageConfig;
                        if (config != null)
                        {
                            configInfo += $"包名: {config.Name}\n";
                            configInfo += $"显示名称: {config.DisplayName}\n";
                            configInfo += $"版本: {config.Version}\n";
                            configInfo += $"描述: {config.Description}\n";
                            configInfo += $"作者: {config.Author?.Name ?? "未指定"}\n";
                            configInfo += $"作者邮箱: {config.Author?.Email ?? "未指定"}\n";
                            configInfo += $"作者网站: {config.Author?.Url ?? "未指定"}\n";
                        }
                        else
                        {
                            configInfo = "配置数据为空";
                        }

                        // 在调试日志和弹窗中显示
                        Debug.Log(configInfo);
                        EditorUtility.DisplayDialog("配置数据调试", configInfo, "确定");
                    }
#endif

                    // 打开文件夹按钮
                    if (GUILayout.Button("在文件资源管理器中打开", GUILayout.Width(180)))
                    {
                        _viewModel.OpenPackageFolder();
                    }

                    // 复制路径按钮
                    if (GUILayout.Button("复制路径", GUILayout.Width(100)))
                    {
                        _viewModel.CopyPackagePathToClipboard();
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
            }

            GUILayout.Space(10);

            // 创建的文件列表段落
            if (_viewModel.IsCreationSuccessful && _viewModel.CreatedFiles.Count > 0)
            {
                _createdFilesExpanded = EditorGUILayout.Foldout(_createdFilesExpanded, $"创建的文件 ({_viewModel.CreatedFiles.Count})", true);
                if (_createdFilesExpanded)
                {
                    EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);

                    // 文件过滤
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("过滤:", GUILayout.Width(40));
                    string newFilter = EditorGUILayout.TextField(_fileFilter);
                    if (newFilter != _fileFilter)
                    {
                        _fileFilter = newFilter;
                        FilterFiles();
                    }
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    // 文件列表
                    _filesScrollPosition = EditorGUILayout.BeginScrollView(_filesScrollPosition, GUILayout.Height(200));

                    foreach (var file in _filteredFiles)
                    {
                        EditorGUILayout.BeginHorizontal();

                        // 文件图标
                        GUILayout.Space(10);
                        EditorGUILayout.LabelField(file, EditorStyles.label);

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndScrollView();

                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUILayout.EndScrollView();

            GUILayout.Space(20);

            // 底部按钮
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            // 重新开始按钮
            if (GUILayout.Button("重新开始", PackageCreatorStyles.PrimaryButton, GUILayout.Width(120)))
            {
                _viewModel.RestartCreationProcess();
                // 返回第一页
                if (OnRestartRequested != null)
                {
                    OnRestartRequested();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 重新开始请求事件
        /// </summary>
        public event System.Action OnRestartRequested;

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
        /// 过滤文件列表
        /// </summary>
        private void FilterFiles()
        {
            _filteredFiles.Clear();

            if (_viewModel.CreatedFiles == null)
                return;

            if (string.IsNullOrWhiteSpace(_fileFilter))
            {
                _filteredFiles.AddRange(_viewModel.CreatedFiles);
            }
            else
            {
                foreach (var file in _viewModel.CreatedFiles)
                {
                    if (file.ToLower().Contains(_fileFilter.ToLower()))
                    {
                        _filteredFiles.Add(file);
                    }
                }
            }
        }

        /// <summary>
        /// 绘制验证消息
        /// </summary>
        private void DrawValidationMessages()
        {
            var validationResult = _viewModel.CreationResult;
            if (validationResult == null)
                return;

            ValidationMessageControl.DrawValidationResult(validationResult, "验证消息", true);
        }
    }
}
