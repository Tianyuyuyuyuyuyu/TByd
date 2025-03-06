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

        #region 模板搜索和过滤

        private string _templateSearchQuery = "";
        private bool _showAdvancedTemplates = true;
        private bool _showBasicTemplates = true;
        private bool _showExperimentalTemplates = false;
        private bool _compareMode = false;
        private List<TemplateType> _templatesForComparison = new List<TemplateType>();

        /// <summary>
        /// 判断模板是否应该显示（基于搜索和过滤条件）
        /// </summary>
        private bool ShouldShowTemplate(TemplateType templateType, string title, string description)
        {
            // 根据类型过滤
            bool typeMatch = true;
            switch (templateType)
            {
                case TemplateType.BasicPackage:
                    typeMatch = _showBasicTemplates;
                    break;
                case TemplateType.EditorTool:
                case TemplateType.RuntimeLibrary:
                    typeMatch = _showAdvancedTemplates;
                    break;
            }

            if (!typeMatch)
                return false;

            // 如果没有搜索查询，则直接返回true
            if (string.IsNullOrWhiteSpace(_templateSearchQuery))
                return true;

            // 搜索标题和描述
            string lowerSearchQuery = _templateSearchQuery.ToLowerInvariant();
            return title.ToLowerInvariant().Contains(lowerSearchQuery) ||
                   description.ToLowerInvariant().Contains(lowerSearchQuery);
        }

        /// <summary>
        /// 切换模板对比模式
        /// </summary>
        private void ToggleCompareMode()
        {
            _compareMode = !_compareMode;
            if (_compareMode)
            {
                // 进入对比模式时，清空对比列表并添加当前选择的模板（如果有）
                _templatesForComparison.Clear();
                if (_selectedTemplate.HasValue)
                {
                    _templatesForComparison.Add(_selectedTemplate.Value);
                }
            }
            else
            {
                // 离开对比模式，清空对比列表
                _templatesForComparison.Clear();
            }
            Repaint();
        }

        /// <summary>
        /// 切换模板在对比列表中的状态
        /// </summary>
        private void ToggleTemplateInComparison(TemplateType templateType)
        {
            if (_templatesForComparison.Contains(templateType))
            {
                _templatesForComparison.Remove(templateType);
            }
            else
            {
                // 限制对比数量为3个
                if (_templatesForComparison.Count < 3)
                {
                    _templatesForComparison.Add(templateType);
                }
            }
            Repaint();
        }

        #endregion

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

            // 绘制固定在顶部的内容（根据当前选择的选项卡）
            DrawFixedHeaderContent();

            // 开始滚动视图
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 绘制可滚动的内容（根据当前选择的选项卡）
            DrawScrollableContent();

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
        /// 绘制固定在顶部的内容
        /// </summary>
        private void DrawFixedHeaderContent()
        {
            switch (_selectedTab)
            {
                case Tab.TemplateSelection:
                    DrawTemplateSelectionHeader();
                    break;
                case Tab.PackageConfiguration:
                    DrawPackageConfigurationHeader();
                    break;
                case Tab.CreationResult:
                    DrawCreationResultHeader();
                    break;
            }
        }

        /// <summary>
        /// 绘制可滚动的内容
        /// </summary>
        private void DrawScrollableContent()
        {
            switch (_selectedTab)
            {
                case Tab.TemplateSelection:
                    DrawTemplateSelectionContent();
                    break;
                case Tab.PackageConfiguration:
                    DrawPackageConfigurationContent();
                    break;
                case Tab.CreationResult:
                    DrawCreationResultContent();
                    break;
            }
        }

        /// <summary>
        /// 绘制模板选择选项卡的固定头部
        /// </summary>
        private void DrawTemplateSelectionHeader()
        {
            EditorGUILayout.LabelField("选择包模板", PackageCreatorStyles.TitleLabel);
            EditorGUILayout.HelpBox("选择要创建的包模板类型。不同的模板提供不同的结构和功能。", MessageType.Info);

            // 添加分隔线
            EditorGUILayout.Space(5);
            PackageCreatorStyles.DrawSeparator();
        }

        /// <summary>
        /// 绘制模板选择选项卡的可滚动内容
        /// </summary>
        private void DrawTemplateSelectionContent()
        {
            EditorGUILayout.Space(5);

            if (_compareMode && _templatesForComparison.Count > 0)
            {
                // 对比模式下显示对比内容
                DrawTemplateComparison();
            }
            else
            {
                // 水平分割左右两栏
                EditorGUILayout.BeginHorizontal();

                // 左侧模板列表区域，占总宽度的60%
                EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.6f - 15f));

                // 添加搜索和过滤区域到左侧列表区域顶部
                EditorGUILayout.BeginHorizontal();

                // 搜索框
                GUI.SetNextControlName("TemplateSearchField");
                _templateSearchQuery = EditorGUILayout.TextField(_templateSearchQuery, EditorStyles.toolbarSearchField);

                // 清除搜索按钮
                if (!string.IsNullOrEmpty(_templateSearchQuery))
                {
                    if (GUILayout.Button("×", EditorStyles.miniButton, GUILayout.Width(20)))
                    {
                        _templateSearchQuery = "";
                        GUI.FocusControl(null);
                    }
                }

                EditorGUILayout.EndHorizontal();

                // 过滤选项
                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 40;
                EditorGUILayout.PrefixLabel("过滤:");
                _showBasicTemplates = EditorGUILayout.ToggleLeft("基础", _showBasicTemplates, GUILayout.Width(50));
                _showAdvancedTemplates = EditorGUILayout.ToggleLeft("高级", _showAdvancedTemplates, GUILayout.Width(50));
                _showExperimentalTemplates = EditorGUILayout.ToggleLeft("实验", _showExperimentalTemplates, GUILayout.Width(50));

                // 添加对比模式按钮
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(_compareMode ? "退出对比" : "对比模式", EditorStyles.miniButton, GUILayout.Width(70)))
                {
                    ToggleCompareMode();
                }
                EditorGUILayout.EndHorizontal();

                PackageCreatorStyles.DrawSeparator();

                // 正常模式下显示模板卡片
                if (ShouldShowTemplate(TemplateType.BasicPackage, "基础包模板", "创建一个基本的UPM包结构，包含必要的目录和文件。"))
                {
                    DrawTemplateCard(TemplateType.BasicPackage, "基础包模板",
                        "创建一个基本的UPM包结构，包含必要的目录和文件。",
                        "适合创建简单的包或从头开始定制包结构。包含package.json、README、LICENSE和基本目录结构。",
                        _basicPackageIcon);

                    EditorGUILayout.Space(-10);
                }

                if (ShouldShowTemplate(TemplateType.EditorTool, "编辑器工具包", "创建专为Unity编辑器扩展设计的包结构。"))
                {
                    DrawTemplateCard(TemplateType.EditorTool, "编辑器工具包",
                        "创建专为Unity编辑器扩展设计的包结构。",
                        "包含编辑器UI组件、窗口和工具类的模板。适合开发Unity编辑器插件、扩展和工具。",
                        _editorToolIcon);

                    EditorGUILayout.Space(-10);
                }

                if (ShouldShowTemplate(TemplateType.RuntimeLibrary, "运行时库", "创建专注于运行时功能的包结构。"))
                {
                    DrawTemplateCard(TemplateType.RuntimeLibrary, "运行时库",
                        "创建专注于运行时功能的包结构。",
                        "用于实现游戏运行时使用的功能模块，包含运行时组件、系统和API的模板结构。适合开发游戏框架和运行时库。",
                        _runtimeLibraryIcon);

                    EditorGUILayout.Space(-10);
                }

                // 如果没有模板匹配，显示提示
                if (!ShouldShowTemplate(TemplateType.BasicPackage, "基础包模板", "创建一个基本的UPM包结构，包含必要的目录和文件。") &&
                    !ShouldShowTemplate(TemplateType.EditorTool, "编辑器工具包", "创建专为Unity编辑器扩展设计的包结构。") &&
                    !ShouldShowTemplate(TemplateType.RuntimeLibrary, "运行时库", "创建专注于运行时功能的包结构。"))
                {
                    EditorGUILayout.HelpBox("没有找到匹配的模板。请尝试修改搜索条件。", MessageType.Info);
                }

                EditorGUILayout.EndVertical();

                // 右侧预览区域
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                if (_selectedTemplate.HasValue)
                {
                    EditorGUILayout.LabelField("模板预览", EditorStyles.boldLabel);
                    EditorGUILayout.Space(5);

                    string templateTitle = "";
                    switch (_selectedTemplate.Value)
                    {
                        case TemplateType.BasicPackage:
                            templateTitle = "基础包模板";
                            break;
                        case TemplateType.EditorTool:
                            templateTitle = "编辑器工具包";
                            break;
                        case TemplateType.RuntimeLibrary:
                            templateTitle = "运行时库";
                            break;
                    }

                    // 显示所选模板的标题
                    EditorGUILayout.LabelField(templateTitle, EditorStyles.boldLabel);
                    PackageCreatorStyles.DrawSeparator();
                    EditorGUILayout.Space(5);

                    // 显示文件结构预览
                    EditorGUILayout.LabelField("文件结构:", EditorStyles.boldLabel);
                    DrawTemplatePreview(_selectedTemplate.Value);

                    EditorGUILayout.Space(10);

                    // 添加"使用此模板"按钮
                    EditorGUI.BeginDisabledGroup(false);
                    if (GUILayout.Button("使用此模板", GUILayout.Height(30)))
                    {
                        _selectedTab = Tab.PackageConfiguration;
                        Repaint();
                    }
                    EditorGUI.EndDisabledGroup();
                }
                else
                {
                    // 如果未选择模板，则显示提示信息
                    EditorGUILayout.LabelField("预览区域", EditorStyles.boldLabel);
                    EditorGUILayout.Space(10);
                    EditorGUILayout.HelpBox("请从左侧选择一个模板以查看详细信息", MessageType.Info);
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// 绘制包配置选项卡的固定头部
        /// </summary>
        private void DrawPackageConfigurationHeader()
        {
            EditorGUILayout.LabelField("配置包信息", PackageCreatorStyles.TitleLabel);
            EditorGUILayout.HelpBox("设置包的基本信息和选项。这些信息将用于生成package.json和其他相关文件。", MessageType.Info);
            EditorGUILayout.Space(5);
            PackageCreatorStyles.DrawSeparator();
        }

        /// <summary>
        /// 绘制创建结果选项卡的固定头部
        /// </summary>
        private void DrawCreationResultHeader()
        {
            EditorGUILayout.LabelField("创建结果", PackageCreatorStyles.TitleLabel);
            EditorGUILayout.Space(5);
            PackageCreatorStyles.DrawSeparator();
        }

        /// <summary>
        /// 绘制包配置选项卡的可滚动内容
        /// </summary>
        private void DrawPackageConfigurationContent()
        {
            EditorGUILayout.Space(10);

            // 这里保留原有的DrawPackageConfigurationTab方法中除头部之外的内容
            // 基本信息区域
            EditorGUILayout.LabelField("基本信息", EditorStyles.boldLabel);
            PackageCreatorStyles.BeginGroup();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("包名");
            _packageName = EditorGUILayout.TextField(_packageName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("显示名称");
            _displayName = EditorGUILayout.TextField(_displayName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("版本");
            _packageVersion = EditorGUILayout.TextField(_packageVersion);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("作者");
            _authorName = EditorGUILayout.TextField(_authorName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("描述");
            _description = EditorGUILayout.TextArea(_description, GUILayout.Height(60));
            EditorGUILayout.EndHorizontal();

            PackageCreatorStyles.EndGroup();

            // 选项区域
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("选项", EditorStyles.boldLabel);
            PackageCreatorStyles.BeginGroup();

            _includeTests = EditorGUILayout.ToggleLeft("包含测试文件夹", _includeTests);
            _includeSamples = EditorGUILayout.ToggleLeft("包含示例文件夹", _includeSamples);
            _includeDocumentation = EditorGUILayout.ToggleLeft("包含文档文件夹", _includeDocumentation);

            PackageCreatorStyles.EndGroup();
        }

        /// <summary>
        /// 绘制创建结果选项卡的可滚动内容
        /// </summary>
        private void DrawCreationResultContent()
        {
            EditorGUILayout.Space(10);

            // 这里保留原有的DrawCreationResultTab方法中除头部之外的内容
            EditorGUILayout.HelpBox("包已成功创建！", MessageType.Info);

            // 创建结果信息区域
            PackageCreatorStyles.BeginGroup();

            EditorGUILayout.LabelField("包名: " + _packageName);
            EditorGUILayout.LabelField("位置: " + "Assets/Packages/" + _packageName);
            EditorGUILayout.Space(5);

            if (GUILayout.Button("在项目中查看"))
            {
                // 这里添加打开包文件夹的功能
                Debug.Log("打开包文件夹: " + "Assets/Packages/" + _packageName);
            }

            PackageCreatorStyles.EndGroup();
        }

        /// <summary>
        /// 绘制模板选择选项卡
        /// </summary>
        private void DrawTemplateSelectionTab()
        {
            // 这个方法现在分为头部和内容两部分，仅作为兼容性保留
            DrawTemplateSelectionHeader();
            DrawTemplateSelectionContent();
        }

        /// <summary>
        /// 绘制模板对比界面
        /// </summary>
        private void DrawTemplateComparison()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            // 对比模式标题
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("模板对比", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            // 如果没有选择任何模板进行对比
            if (_templatesForComparison.Count == 0)
            {
                EditorGUILayout.HelpBox("请选择至少一个模板进行对比。", MessageType.Info);
                EditorGUILayout.EndVertical();
                return;
            }

            // 添加或移除对比模板的选项
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("选择对比模板:", GUILayout.Width(100));

            GUI.enabled = !_templatesForComparison.Contains(TemplateType.BasicPackage);
            if (GUILayout.Button("基础包模板", _templatesForComparison.Count < 3 ? EditorStyles.miniButton : EditorStyles.miniButtonLeft))
            {
                ToggleTemplateInComparison(TemplateType.BasicPackage);
            }

            GUI.enabled = !_templatesForComparison.Contains(TemplateType.EditorTool);
            if (GUILayout.Button("编辑器工具包", _templatesForComparison.Count < 3 ? EditorStyles.miniButton : EditorStyles.miniButtonMid))
            {
                ToggleTemplateInComparison(TemplateType.EditorTool);
            }

            GUI.enabled = !_templatesForComparison.Contains(TemplateType.RuntimeLibrary);
            if (GUILayout.Button("运行时库", _templatesForComparison.Count < 3 ? EditorStyles.miniButton : EditorStyles.miniButtonRight))
            {
                ToggleTemplateInComparison(TemplateType.RuntimeLibrary);
            }

            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

            // 当前对比的模板
            EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("当前对比:", GUILayout.Width(100));

            foreach (var template in _templatesForComparison)
            {
                string templateName = "";
                switch (template)
                {
                    case TemplateType.BasicPackage:
                        templateName = "基础包模板";
                        break;
                    case TemplateType.EditorTool:
                        templateName = "编辑器工具包";
                        break;
                    case TemplateType.RuntimeLibrary:
                        templateName = "运行时库";
                        break;
                }

                GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.MaxWidth(100));
                GUILayout.Label(templateName, EditorStyles.miniLabel);
                if (GUILayout.Button("×", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                {
                    _templatesForComparison.Remove(template);
                    Repaint();
                    break;  // 中断循环防止集合修改异常
                }
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndHorizontal();

            // 绘制对比表格
            EditorGUILayout.Space(10);
            DrawComparisonTable();

            EditorGUILayout.Space(10);

            // 若选择了多个模板，显示"选择此模板"按钮
            if (_templatesForComparison.Count > 1)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                foreach (var template in _templatesForComparison)
                {
                    string templateName = "";
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            templateName = "基础包模板";
                            break;
                        case TemplateType.EditorTool:
                            templateName = "编辑器工具包";
                            break;
                        case TemplateType.RuntimeLibrary:
                            templateName = "运行时库";
                            break;
                    }

                    if (GUILayout.Button("选择: " + templateName, GUILayout.Width(120)))
                    {
                        _selectedTemplate = template;
                        _compareMode = false;
                        _templatesForComparison.Clear();
                        Repaint();
                    }
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制对比表格
        /// </summary>
        private void DrawComparisonTable()
        {
            // 表格特性列表
            string[] features = {
                "适用场景",
                "包含文件夹",
                "Runtime支持",
                "Editor支持",
                "提供示例",
                "依赖管理",
                "复杂度",
                "适合新手",
                "扩展性",
                "适合团队协作"
            };

            // 表格头部
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("特性", EditorStyles.boldLabel, GUILayout.Width(120));

            foreach (var template in _templatesForComparison)
            {
                string templateName = "";
                switch (template)
                {
                    case TemplateType.BasicPackage:
                        templateName = "基础包模板";
                        break;
                    case TemplateType.EditorTool:
                        templateName = "编辑器工具包";
                        break;
                    case TemplateType.RuntimeLibrary:
                        templateName = "运行时库";
                        break;
                }

                EditorGUILayout.LabelField(templateName, EditorStyles.boldLabel, GUILayout.MinWidth(120));
            }

            EditorGUILayout.EndHorizontal();

            // 分隔线
            PackageCreatorStyles.DrawSeparator();

            // 表格内容
            for (int i = 0; i < features.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(features[i], GUILayout.Width(120));

                foreach (var template in _templatesForComparison)
                {
                    string value = GetFeatureValue(template, i);

                    // 设置不同评级的颜色
                    Color originalColor = GUI.color;
                    if (value.Contains("高") || value.Contains("是") || value.Contains("完全支持"))
                        GUI.color = new Color(0.5f, 0.8f, 0.5f); // 绿色，表示好
                    else if (value.Contains("中") || value.Contains("部分"))
                        GUI.color = new Color(0.85f, 0.85f, 0.5f); // 黄色，表示中等
                    else if (value.Contains("低") || value.Contains("否") || value.Contains("不支持"))
                        GUI.color = new Color(0.85f, 0.6f, 0.6f); // 红色，表示差

                    EditorGUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.MinWidth(120));
                    EditorGUILayout.LabelField(value, EditorStyles.wordWrappedLabel);
                    EditorGUILayout.EndHorizontal();

                    GUI.color = originalColor;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(2);
            }

            // 文件结构对比
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("文件结构对比", EditorStyles.boldLabel);
            PackageCreatorStyles.DrawSeparator();

            EditorGUILayout.BeginHorizontal();

            // 为每个模板显示一个文件结构预览
            foreach (var template in _templatesForComparison)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MinWidth(120));

                string templateName = "";
                switch (template)
                {
                    case TemplateType.BasicPackage:
                        templateName = "基础包模板";
                        break;
                    case TemplateType.EditorTool:
                        templateName = "编辑器工具包";
                        break;
                    case TemplateType.RuntimeLibrary:
                        templateName = "运行时库";
                        break;
                }

                EditorGUILayout.LabelField(templateName, EditorStyles.boldLabel);
                EditorGUILayout.Space(5);

                DrawTemplatePreview(template);

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 获取特定模板特定特性的值
        /// </summary>
        private string GetFeatureValue(TemplateType template, int featureIndex)
        {
            switch (featureIndex)
            {
                case 0: // 适用场景
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "简单项目，快速原型开发";
                        case TemplateType.EditorTool:
                            return "Unity编辑器扩展，工具开发";
                        case TemplateType.RuntimeLibrary:
                            return "游戏运行时框架，系统开发";
                        default:
                            return "";
                    }

                case 1: // 包含文件夹
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "基础目录结构";
                        case TemplateType.EditorTool:
                            return "基础+编辑器专用目录";
                        case TemplateType.RuntimeLibrary:
                            return "基础+运行时系统专用目录";
                        default:
                            return "";
                    }

                case 2: // Runtime支持
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "基础支持";
                        case TemplateType.EditorTool:
                            return "基础支持";
                        case TemplateType.RuntimeLibrary:
                            return "完全支持，包含组件和系统";
                        default:
                            return "";
                    }

                case 3: // Editor支持
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "不支持";
                        case TemplateType.EditorTool:
                            return "完全支持，包含UI和工具类";
                        case TemplateType.RuntimeLibrary:
                            return "基础支持";
                        default:
                            return "";
                    }

                case 4: // 提供示例
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return _includeSamples ? "是" : "否";
                        case TemplateType.EditorTool:
                            return _includeSamples ? "是" : "否";
                        case TemplateType.RuntimeLibrary:
                            return _includeSamples ? "是" : "否";
                        default:
                            return "";
                    }

                case 5: // 依赖管理
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "基础配置";
                        case TemplateType.EditorTool:
                            return "预配置编辑器依赖";
                        case TemplateType.RuntimeLibrary:
                            return "预配置运行时依赖";
                        default:
                            return "";
                    }

                case 6: // 复杂度
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "低";
                        case TemplateType.EditorTool:
                            return "中";
                        case TemplateType.RuntimeLibrary:
                            return "中-高";
                        default:
                            return "";
                    }

                case 7: // 适合新手
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "是";
                        case TemplateType.EditorTool:
                            return "部分";
                        case TemplateType.RuntimeLibrary:
                            return "否";
                        default:
                            return "";
                    }

                case 8: // 扩展性
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "中";
                        case TemplateType.EditorTool:
                            return "高";
                        case TemplateType.RuntimeLibrary:
                            return "高";
                        default:
                            return "";
                    }

                case 9: // 适合团队协作
                    switch (template)
                    {
                        case TemplateType.BasicPackage:
                            return "部分适合";
                        case TemplateType.EditorTool:
                            return "适合";
                        case TemplateType.RuntimeLibrary:
                            return "非常适合";
                        default:
                            return "";
                    }

                default:
                    return "";
            }
        }

        /// <summary>
        /// 绘制模板卡片并处理选择
        /// </summary>
        private void DrawTemplateCard(TemplateType templateType, string title, string description, string details, Texture2D icon)
        {
            bool isSelected = _selectedTemplate == templateType;

            // 在对比模式下使用不同的绘制逻辑
            if (_compareMode)
            {
                EditorGUILayout.BeginHorizontal();

                // 显示是否选中进行对比的复选框
                bool isInComparison = _templatesForComparison.Contains(templateType);
                bool newIsInComparison = EditorGUILayout.Toggle(isInComparison, GUILayout.Width(20));

                if (newIsInComparison != isInComparison)
                {
                    if (newIsInComparison && _templatesForComparison.Count < 3)
                    {
                        _templatesForComparison.Add(templateType);
                    }
                    else if (!newIsInComparison)
                    {
                        _templatesForComparison.Remove(templateType);
                    }
                }

                // 使用基本样式的卡片，因为这是对比模式
                var card = new TemplateCardControl(title, description, "", icon, isInComparison);
                card.Draw();

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                // 正常模式下的卡片显示
                var card = new TemplateCardControl(title, description, details, icon, isSelected);

                // 在左右分栏布局中，使用更紧凑的展示方式
                if (card.DrawCompact())
                {
                    // 选择此模板
                    _selectedTemplate = templateType;
                    // 使窗口重绘
                    Repaint();
                }
            }
        }

        /// <summary>
        /// 绘制模板预览
        /// </summary>
        private void DrawTemplatePreview(TemplateType templateType)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("包含文件夹:", EditorStyles.boldLabel, GUILayout.Width(100));
            EditorGUILayout.BeginVertical();

            switch (templateType)
            {
                case TemplateType.BasicPackage:
                    EditorGUILayout.LabelField("• Runtime - 运行时代码");
                    EditorGUILayout.LabelField("• package.json - 包清单文件");
                    EditorGUILayout.LabelField("• README.md - 说明文档");
                    EditorGUILayout.LabelField("• LICENSE.md - 许可证文件");
                    if (_includeTests)
                        EditorGUILayout.LabelField("• Tests - 测试代码");
                    if (_includeDocumentation)
                        EditorGUILayout.LabelField("• Documentation~ - 文档文件夹");
                    if (_includeSamples)
                        EditorGUILayout.LabelField("• Samples~ - 示例代码");
                    break;

                case TemplateType.EditorTool:
                    EditorGUILayout.LabelField("• Runtime - 运行时代码");
                    EditorGUILayout.LabelField("• Editor - 编辑器代码");
                    EditorGUILayout.LabelField("• Editor/UI - 编辑器UI组件");
                    EditorGUILayout.LabelField("• Editor/Resources - 编辑器资源");
                    EditorGUILayout.LabelField("• package.json - 包清单文件");
                    EditorGUILayout.LabelField("• README.md - 说明文档");
                    EditorGUILayout.LabelField("• LICENSE.md - 许可证文件");
                    if (_includeTests)
                    {
                        EditorGUILayout.LabelField("• Tests/Runtime - 运行时测试");
                        EditorGUILayout.LabelField("• Tests/Editor - 编辑器测试");
                    }
                    if (_includeDocumentation)
                        EditorGUILayout.LabelField("• Documentation~ - 文档文件夹");
                    if (_includeSamples)
                        EditorGUILayout.LabelField("• Samples~ - 示例代码");
                    break;

                case TemplateType.RuntimeLibrary:
                    EditorGUILayout.LabelField("• Runtime - 运行时代码");
                    EditorGUILayout.LabelField("• Runtime/Components - 组件代码");
                    EditorGUILayout.LabelField("• Runtime/Systems - 系统代码");
                    EditorGUILayout.LabelField("• Runtime/Data - 数据结构");
                    EditorGUILayout.LabelField("• Runtime/Utils - 工具类");
                    EditorGUILayout.LabelField("• package.json - 包清单文件");
                    EditorGUILayout.LabelField("• README.md - 说明文档");
                    EditorGUILayout.LabelField("• LICENSE.md - 许可证文件");
                    if (_includeTests)
                        EditorGUILayout.LabelField("• Tests/Runtime - 运行时测试");
                    if (_includeDocumentation)
                        EditorGUILayout.LabelField("• Documentation~ - 文档文件夹");
                    if (_includeSamples)
                        EditorGUILayout.LabelField("• Samples~ - 示例代码");
                    break;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
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

        /// <summary>
        /// 绘制包配置选项卡
        /// </summary>
        private void DrawPackageConfigurationTab()
        {
            // 这个方法现在分为头部和内容两部分，仅作为兼容性保留
            DrawPackageConfigurationHeader();
            DrawPackageConfigurationContent();
        }

        /// <summary>
        /// 绘制创建结果选项卡
        /// </summary>
        private void DrawCreationResultTab()
        {
            // 这个方法现在分为头部和内容两部分，仅作为兼容性保留
            DrawCreationResultHeader();
            DrawCreationResultContent();
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
