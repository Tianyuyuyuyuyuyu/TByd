using System;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.ViewModels;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 基本配置页面，用于设置包的基本信息
    /// </summary>
    public class ConfigurationPage : BasePage
    {
        // 视图模型
        private readonly ConfigurationViewModel _viewModel;

        // 滚动位置
        private Vector2 _scrollPosition;

        // 文件名验证提示延迟显示计时器
        private float _showNameValidationTimer;
        private bool _showNameValidation;

        // 页面分组展开状态
        private bool _basicInfoExpanded = true;
        private bool _authorInfoExpanded = true;
        private bool _additionalInfoExpanded = true;

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => "基本配置";

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public override bool IsValid => _viewModel.IsValid;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public ConfigurationPage(IConfigManager configManager)
        {
            _viewModel = new ConfigurationViewModel(configManager);
        }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public override void OnEnter()
        {
            // 初始化视图模型
            _viewModel.Initialize();

            // 重置滚动位置
            _scrollPosition = Vector2.zero;

            // 重置验证显示计时器
            _showNameValidationTimer = 0;
            _showNameValidation = false;
        }

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        public override void OnExit()
        {
            // 清理视图模型
            _viewModel.Cleanup();
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

            // 开始滚动区域
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 使用折叠面板绘制基本信息区域
            _basicInfoExpanded = EditorGUILayout.Foldout(_basicInfoExpanded, "基本信息", true, PackageCreatorStyles.FoldoutStyle);
            if (_basicInfoExpanded)
            {
                BeginVerticalGroup();
                DrawBasicInfoFields();
                EndVerticalGroup();
            }

            EditorGUILayout.Space(10);

            // 使用折叠面板绘制作者信息区域
            _authorInfoExpanded = EditorGUILayout.Foldout(_authorInfoExpanded, "作者信息", true, PackageCreatorStyles.FoldoutStyle);
            if (_authorInfoExpanded)
            {
                BeginVerticalGroup();
                DrawAuthorInfoFields();
                EndVerticalGroup();
            }

            EditorGUILayout.Space(10);

            // 使用折叠面板绘制附加信息区域
            _additionalInfoExpanded = EditorGUILayout.Foldout(_additionalInfoExpanded, "附加信息", true, PackageCreatorStyles.FoldoutStyle);
            if (_additionalInfoExpanded)
            {
                BeginVerticalGroup();
                DrawAdditionalInfoFields();
                EndVerticalGroup();
            }

            // 结束滚动区域
            EditorGUILayout.EndScrollView();

            // 处理字段验证显示
            HandleFieldValidationDisplay();
        }

        /// <summary>
        /// 绘制基本信息字段
        /// </summary>
        private void DrawBasicInfoFields()
        {
            // 包名称 - 使用固定宽度标签和控件
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("包名称:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newName = EditorGUILayout.TextField(_viewModel.PackageName);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.PackageName = newName;
                _showNameValidationTimer = (float)(EditorApplication.timeSinceStartup + 0.5f); // 延迟显示验证信息
            }
            EditorGUILayout.EndHorizontal();

            // 显示名称验证消息
            if (_showNameValidation && !_viewModel.IsPackageNameValid)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(125); // 与标签对齐
                EditorGUILayout.HelpBox("包名称必须符合UPM规范: 使用小写字母，可包含连字符(-)或点(.)，格式应为com.example.package", MessageType.Warning);
                EditorGUILayout.EndHorizontal();
            }

            // 显示名称
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("显示名称:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newDisplayName = EditorGUILayout.TextField(_viewModel.DisplayName);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.DisplayName = newDisplayName;
            }
            EditorGUILayout.EndHorizontal();

            // 版本号
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("版本号:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newVersion = EditorGUILayout.TextField(_viewModel.Version);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.Version = newVersion;
            }
            EditorGUILayout.EndHorizontal();

            // 版本提示
            if (!_viewModel.IsVersionValid)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(125); // 与标签对齐
                EditorGUILayout.HelpBox("版本号必须符合语义化版本规范 (例如: 1.0.0)", MessageType.Warning);
                EditorGUILayout.EndHorizontal();
            }

            // 描述
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("描述:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120), GUILayout.Height(60));

            EditorGUI.BeginChangeCheck();
            string newDescription = EditorGUILayout.TextArea(_viewModel.Description, GUILayout.Height(60));
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.Description = newDescription;
            }
            EditorGUILayout.EndHorizontal();

            // 根命名空间
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("根命名空间:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newNamespace = EditorGUILayout.TextField(_viewModel.RootNamespace);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.RootNamespace = newNamespace;
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制作者信息字段
        /// </summary>
        private void DrawAuthorInfoFields()
        {
            // 作者名称
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("作者名称:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newAuthorName = EditorGUILayout.TextField(_viewModel.AuthorName);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.AuthorName = newAuthorName;
            }
            EditorGUILayout.EndHorizontal();

            // 作者邮箱
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("作者邮箱:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newAuthorEmail = EditorGUILayout.TextField(_viewModel.AuthorEmail);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.AuthorEmail = newAuthorEmail;
            }
            EditorGUILayout.EndHorizontal();

            // 作者URL
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("作者网址:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newAuthorUrl = EditorGUILayout.TextField(_viewModel.AuthorUrl);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.AuthorUrl = newAuthorUrl;
            }
            EditorGUILayout.EndHorizontal();

            // 公司/组织名称
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("公司/组织:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newCompany = EditorGUILayout.TextField(_viewModel.Company);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.Company = newCompany;
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制附加信息字段
        /// </summary>
        private void DrawAdditionalInfoFields()
        {
            // Unity兼容版本
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Unity版本:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newUnityVersion = EditorGUILayout.TextField(_viewModel.UnityVersion);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.UnityVersion = newUnityVersion;
            }
            EditorGUILayout.EndHorizontal();

            // 许可证类型
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("许可证:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            int licenseIndex = Array.IndexOf(_viewModel.LicenseOptions, _viewModel.License);
            licenseIndex = EditorGUILayout.Popup(licenseIndex, _viewModel.LicenseOptions, GUILayout.Width(150));
            if (EditorGUI.EndChangeCheck() && licenseIndex >= 0)
            {
                _viewModel.License = _viewModel.LicenseOptions[licenseIndex];
            }
            EditorGUILayout.EndHorizontal();

            // 文档URL
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("文档URL:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newDocUrl = EditorGUILayout.TextField(_viewModel.DocumentationUrl);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.DocumentationUrl = newDocUrl;
            }
            EditorGUILayout.EndHorizontal();

            // 变更日志URL
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("变更日志URL:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newChangelogUrl = EditorGUILayout.TextField(_viewModel.ChangelogUrl);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.ChangelogUrl = newChangelogUrl;
            }
            EditorGUILayout.EndHorizontal();

            // 许可证URL
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("许可证URL:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            string newLicenseUrl = EditorGUILayout.TextField(_viewModel.LicenseUrl);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.LicenseUrl = newLicenseUrl;
            }
            EditorGUILayout.EndHorizontal();

            // 包含Tests
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("包含测试:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            bool includeTests = EditorGUILayout.Toggle(_viewModel.IncludeTests);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.IncludeTests = includeTests;
            }
            EditorGUILayout.EndHorizontal();

            // 包含Samples
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("包含示例:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            bool includeSamples = EditorGUILayout.Toggle(_viewModel.IncludeSamples);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.IncludeSamples = includeSamples;
            }
            EditorGUILayout.EndHorizontal();

            // 包含Documentation
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("包含文档:", PackageCreatorStyles.FieldLabel, GUILayout.Width(120));

            EditorGUI.BeginChangeCheck();
            bool includeDocumentation = EditorGUILayout.Toggle(_viewModel.IncludeDocumentation);
            if (EditorGUI.EndChangeCheck())
            {
                _viewModel.IncludeDocumentation = includeDocumentation;
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 处理字段验证显示
        /// </summary>
        private void HandleFieldValidationDisplay()
        {
            // 如果验证计时器已经超时，显示验证消息
            if (_showNameValidationTimer > 0 && EditorApplication.timeSinceStartup > _showNameValidationTimer)
            {
                _showNameValidation = true;
                _showNameValidationTimer = 0;
                EditorWindow.GetWindowWithRect<EditorWindow>(EditorWindow.GetWindow<EditorWindow>().position, true).Repaint();
            }
        }
    }
}
