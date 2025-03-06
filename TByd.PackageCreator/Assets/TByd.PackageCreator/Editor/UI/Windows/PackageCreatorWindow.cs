using System;
using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.UI.Controls;
using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Windows
{
    /// <summary>
    /// 包创建器主窗口
    /// </summary>
    public class PackageCreatorWindow : EditorWindow
    {
        #region 常量

        // 窗口尺寸
        private const float MIN_WINDOW_WIDTH = 800f;
        private const float MIN_WINDOW_HEIGHT = 600f;
        private const float PREFERRED_WINDOW_WIDTH = 900f;
        private const float PREFERRED_WINDOW_HEIGHT = 700f;

        // 菜单路径
        private const string MENU_PATH = "Tools/TByd/UPM Package Creator";
        private const string WINDOW_TITLE = "UPM Package Creator";

        // EditorPrefs 键名
        private const string PREF_KEY_PREFIX = "TByd.PackageCreator.";
        private const string PREF_KEY_SELECTED_TAB = PREF_KEY_PREFIX + "SelectedTab";
        private const string PREF_KEY_SELECTED_TEMPLATE = PREF_KEY_PREFIX + "SelectedTemplate";

        #endregion

        #region 枚举

        /// <summary>
        /// 窗口选项卡枚举
        /// </summary>
        public enum Tab
        {
            TemplateSelection,
            PackageConfiguration,
            CreationResult
        }

        /// <summary>
        /// 模板类型枚举
        /// </summary>
        public enum TemplateType
        {
            BasicPackage,
            EditorTool,
            RuntimeLibrary
        }

        #endregion

        #region 字段

        // 当前选择的选项卡
        private Tab _selectedTab = Tab.TemplateSelection;

        // 当前选择的模板
        private TemplateType? _selectedTemplate = null;

        // 滚动位置
        private Vector2 _scrollPosition;

        // 图标
        private Texture2D _logoIcon;
        private Texture2D _basicPackageIcon;
        private Texture2D _editorToolIcon;
        private Texture2D _runtimeLibraryIcon;

        // 包配置字段
        private string _packageName = "com.example.package";
        private string _displayName = "Example Package";
        private string _packageVersion = "1.0.0";
        private string _authorName = "";
        private string _description = "A description for my package";
        private bool _includeTests = true;
        private bool _includeSamples = false;
        private bool _includeDocumentation = true;

        #endregion

        #region Unity 生命周期

        private void OnEnable()
        {
            // 加载窗口状态
            LoadWindowState();

            // 加载资源
            LoadResources();

            // 设置窗口标题和图标
            titleContent = new GUIContent(WINDOW_TITLE, _logoIcon);

            // 确保窗口尺寸合理
            minSize = new Vector2(MIN_WINDOW_WIDTH, MIN_WINDOW_HEIGHT);
        }

        private void OnDisable()
        {
            // 保存窗口状态
            SaveWindowState();
        }

        private void OnGUI()
        {
            // 绘制选项卡
            DrawTabs();

            // 开始滚动视图
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 基于选择的选项卡绘制内容
            switch (_selectedTab)
            {
                case Tab.TemplateSelection:
                    DrawTemplateSelectionTab();
                    break;
                case Tab.PackageConfiguration:
                    DrawPackageConfigurationTab();
                    break;
                case Tab.CreationResult:
                    DrawCreationResultTab();
                    break;
            }

            // 结束滚动视图
            EditorGUILayout.EndScrollView();

            // 绘制底部操作按钮
            DrawBottomButtons();
        }

        #endregion

        #region 菜单项和初始化

        /// <summary>
        /// 打开窗口的菜单项
        /// </summary>
        [MenuItem(MENU_PATH, false, 100)]
        public static void ShowWindow()
        {
            // 创建并显示窗口
            var window = GetWindow<PackageCreatorWindow>(false, WINDOW_TITLE, true);
            window.minSize = new Vector2(MIN_WINDOW_WIDTH, MIN_WINDOW_HEIGHT);
            window.position = new Rect(
                (Screen.currentResolution.width - PREFERRED_WINDOW_WIDTH) / 2,
                (Screen.currentResolution.height - PREFERRED_WINDOW_HEIGHT) / 2,
                PREFERRED_WINDOW_WIDTH,
                PREFERRED_WINDOW_HEIGHT);
        }

        #endregion

        #region GUI 绘制方法

        /// <summary>
        /// 绘制选项卡
        /// </summary>
        private void DrawTabs()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            if (GUILayout.Toggle(_selectedTab == Tab.TemplateSelection, "模板选择",
                    _selectedTab == Tab.TemplateSelection ? EditorStyles.toolbarButton : EditorStyles.toolbarButton))
            {
                if (_selectedTab != Tab.TemplateSelection)
                {
                    _selectedTab = Tab.TemplateSelection;
                    GUI.FocusControl(null);
                }
            }

            // 仅当选择了模板后才能进入包配置选项卡
            GUI.enabled = _selectedTemplate.HasValue;
            if (GUILayout.Toggle(_selectedTab == Tab.PackageConfiguration, "包配置",
                    _selectedTab == Tab.PackageConfiguration ? EditorStyles.toolbarButton : EditorStyles.toolbarButton))
            {
                if (_selectedTab != Tab.PackageConfiguration && _selectedTemplate.HasValue)
                {
                    _selectedTab = Tab.PackageConfiguration;
                    GUI.FocusControl(null);
                }
            }

            // 结果选项卡仅用于显示创建后的结果
            GUI.enabled = false;
            GUILayout.Toggle(_selectedTab == Tab.CreationResult, "创建结果",
                    _selectedTab == Tab.CreationResult ? EditorStyles.toolbarButton : EditorStyles.toolbarButton);

            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
        }

        /// <summary>
        /// 绘制模板选择选项卡
        /// </summary>
        private void DrawTemplateSelectionTab()
        {
            EditorGUILayout.LabelField("选择包模板", PackageCreatorStyles.TitleLabel);
            EditorGUILayout.HelpBox("选择要创建的包模板类型。不同的模板提供不同的结构和功能。", MessageType.Info);
            EditorGUILayout.Space(15);

            // 创建模板卡片
            DrawTemplateCard(TemplateType.BasicPackage, "基础包模板",
                "创建一个基本的UPM包结构，包含必要的目录和文件。",
                "适合创建简单的包或从头开始定制包结构。包含package.json、README、LICENSE和基本目录结构。",
                _basicPackageIcon);

            EditorGUILayout.Space(5);

            DrawTemplateCard(TemplateType.EditorTool, "编辑器工具包",
                "创建专为Unity编辑器扩展设计的包结构。",
                "包含编辑器UI组件、窗口和工具类的模板。适合开发Unity编辑器插件、扩展和工具。",
                _editorToolIcon);

            EditorGUILayout.Space(5);

            DrawTemplateCard(TemplateType.RuntimeLibrary, "运行时库",
                "创建专注于运行时功能的包结构。",
                "用于实现游戏运行时使用的功能模块，包含运行时组件、系统和API的模板结构。适合开发游戏框架和运行时库。",
                _runtimeLibraryIcon);
        }

        /// <summary>
        /// 绘制模板卡片并处理选择
        /// </summary>
        private void DrawTemplateCard(TemplateType templateType, string title, string description, string details, Texture2D icon)
        {
            bool isSelected = _selectedTemplate == templateType;
            var card = new TemplateCardControl(title, description, details, icon, isSelected);

            if (card.DrawDetailed())
            {
                // 选择此模板
                _selectedTemplate = templateType;
                // 使窗口重绘
                Repaint();
            }
        }

        /// <summary>
        /// 绘制包配置选项卡
        /// </summary>
        private void DrawPackageConfigurationTab()
        {
            EditorGUILayout.LabelField("包配置", PackageCreatorStyles.TitleLabel);
            EditorGUILayout.HelpBox("配置包的基本信息、版本、依赖项等。", MessageType.Info);
            EditorGUILayout.Space(15);

            // 基本信息
            PackageCreatorStyles.BeginGroup("基本信息");

            _packageName = EditorGUILayout.TextField("包名称", _packageName);
            if (!_packageName.StartsWith("com."))
            {
                EditorGUILayout.HelpBox("包名称应遵循反向域名格式，例如: 'com.yourcompany.packagename'", MessageType.Warning);
            }

            _displayName = EditorGUILayout.TextField("显示名称", _displayName);
            _packageVersion = EditorGUILayout.TextField("版本", _packageVersion);
            _authorName = EditorGUILayout.TextField("作者", _authorName);

            EditorGUILayout.LabelField("描述");
            _description = EditorGUILayout.TextArea(_description, GUILayout.Height(60));

            PackageCreatorStyles.EndGroup();

            // 可选功能
            PackageCreatorStyles.BeginGroup("可选组件");

            _includeTests = EditorGUILayout.Toggle("包含测试", _includeTests);
            _includeSamples = EditorGUILayout.Toggle("包含示例", _includeSamples);
            _includeDocumentation = EditorGUILayout.Toggle("包含文档", _includeDocumentation);

            PackageCreatorStyles.EndGroup();

            // 高级选项
            PackageCreatorStyles.BeginGroup("高级选项");

            EditorGUILayout.LabelField("目标Unity版本");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Unity 2021.3.8f1 及以上版本", EditorStyles.miniLabel);
            if (GUILayout.Button("修改", GUILayout.Width(100)))
            {
                // TODO: 显示版本选择对话框
                Debug.Log("版本选择功能将在未来实现");
            }
            EditorGUILayout.EndHorizontal();

            PackageCreatorStyles.EndGroup();
        }

        /// <summary>
        /// 绘制创建结果选项卡
        /// </summary>
        private void DrawCreationResultTab()
        {
            EditorGUILayout.LabelField("创建结果", PackageCreatorStyles.TitleLabel);

            PackageCreatorStyles.BeginGroup();

            EditorGUILayout.HelpBox("包已成功创建！", MessageType.Info);
            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("包信息", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("名称:", _packageName);
            EditorGUILayout.LabelField("位置:", $"Assets/Packages/{_packageName}");

            EditorGUILayout.Space(10);

            if (GUILayout.Button("在资源管理器中显示", PackageCreatorStyles.SecondaryButton))
            {
                // TODO: 在资源管理器中显示包位置
                Debug.Log("在资源管理器中显示功能将在未来实现");
            }

            if (GUILayout.Button("在Unity中查看", PackageCreatorStyles.SecondaryButton))
            {
                // TODO: 在Unity中打开包文件夹
                Debug.Log("在Unity中查看功能将在未来实现");
            }

            PackageCreatorStyles.EndGroup();
        }

        /// <summary>
        /// 绘制底部按钮
        /// </summary>
        private void DrawBottomButtons()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();

            // 上一步按钮
            GUI.enabled = _selectedTab > Tab.TemplateSelection;
            if (GUILayout.Button("上一步", PackageCreatorStyles.SecondaryButton))
            {
                _selectedTab--;
            }

            GUILayout.FlexibleSpace();

            // 下一步/创建按钮
            GUI.enabled = true;
            if (_selectedTab < Tab.CreationResult)
            {
                // 下一步按钮 - 只有当当前步骤完成时才启用
                bool canProceed = false;

                switch (_selectedTab)
                {
                    case Tab.TemplateSelection:
                        canProceed = _selectedTemplate.HasValue;
                        break;
                    case Tab.PackageConfiguration:
                        canProceed = !string.IsNullOrEmpty(_packageName) && !string.IsNullOrEmpty(_packageVersion);
                        break;
                }

                GUI.enabled = canProceed;

                if (GUILayout.Button("下一步", PackageCreatorStyles.PrimaryButton))
                {
                    _selectedTab++;
                }
            }
            else if (_selectedTab == Tab.CreationResult)
            {
                if (GUILayout.Button("创建新包", PackageCreatorStyles.PrimaryButton))
                {
                    // 重置为第一个选项卡，开始新的创建流程
                    _selectedTab = Tab.TemplateSelection;
                    _selectedTemplate = null;
                    _packageName = "com.example.package";
                    _displayName = "Example Package";
                    _packageVersion = "1.0.0";
                    _authorName = "";
                    _description = "A description for my package";
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region 资源加载

        /// <summary>
        /// 加载窗口所需资源
        /// </summary>
        private void LoadResources()
        {
            // 加载图标
            if (_logoIcon == null)
            {
                _logoIcon = EditorGUIUtility.FindTexture("d_PackageManager");
            }

            // 加载模板图标
            if (_basicPackageIcon == null)
            {
                _basicPackageIcon = EditorGUIUtility.FindTexture("d_Package");
            }

            if (_editorToolIcon == null)
            {
                _editorToolIcon = EditorGUIUtility.FindTexture("d_SceneViewTools");
            }

            if (_runtimeLibraryIcon == null)
            {
                _runtimeLibraryIcon = EditorGUIUtility.FindTexture("d_cs Script Icon");
            }
        }

        #endregion

        #region 状态保存与恢复

        /// <summary>
        /// 保存窗口状态
        /// </summary>
        private void SaveWindowState()
        {
            EditorPrefs.SetInt(PREF_KEY_SELECTED_TAB, (int)_selectedTab);

            if (_selectedTemplate.HasValue)
            {
                EditorPrefs.SetInt(PREF_KEY_SELECTED_TEMPLATE, (int)_selectedTemplate.Value);
            }
            else
            {
                EditorPrefs.DeleteKey(PREF_KEY_SELECTED_TEMPLATE);
            }
        }

        /// <summary>
        /// 加载窗口状态
        /// </summary>
        private void LoadWindowState()
        {
            if (EditorPrefs.HasKey(PREF_KEY_SELECTED_TAB))
            {
                _selectedTab = (Tab)EditorPrefs.GetInt(PREF_KEY_SELECTED_TAB, 0);
            }

            if (EditorPrefs.HasKey(PREF_KEY_SELECTED_TEMPLATE))
            {
                _selectedTemplate = (TemplateType)EditorPrefs.GetInt(PREF_KEY_SELECTED_TEMPLATE);
            }
        }

        #endregion
    }
}
