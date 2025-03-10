using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Controls;
using TByd.PackageCreator.Editor.UI.Pages;
using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.Utils;
using TByd.PackageCreator.Editor.UI.ViewModels;
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
        private const float MinWindowWidth = 800f;
        private const float MinWindowHeight = 600f;
        private const float PreferredWindowWidth = 900f;
        private const float PreferredWindowHeight = 700f;

        // 菜单路径
        private const string MenuPath = "Tools/TByd/UPM Package Creator";
        private const string WindowTitle = "UPM Package Creator";

        // EditorPrefs键
        private const string CurrentPageIndexKey = "TByd.PackageCreator.CurrentPageIndex";

        #endregion

        #region 字段

        // 页面导航器
        private PageNavigator _pageNavigator;

        // 服务实例
        private ITemplateManager _templateManager;
        private IConfigManager _configManager;

        // UI状态
        private Vector2 _scrollPosition;
        private bool _isInitialized;

        // 工具栏
        private string[] _toolbarTitles;
        private int _selectedToolbarIndex;

        // 页面状态
        private bool _showSettings;
        private bool _isInitialPageEntered;

        #endregion

        #region 菜单项和初始化

        /// <summary>
        /// 打开窗口的菜单项
        /// </summary>
        [MenuItem(MenuPath, false, 100)]
        public static void ShowWindow()
        {
            // 创建并显示窗口
            var window = GetWindow<PackageCreatorWindow>(false, WindowTitle, true);
            window.minSize = new Vector2(MinWindowWidth, MinWindowHeight);
            window.position = new Rect(
                (Screen.currentResolution.width - PreferredWindowWidth) / 2,
                (Screen.currentResolution.height - PreferredWindowHeight) / 2,
                PreferredWindowWidth,
                PreferredWindowHeight);
        }

        #endregion

        #region Unity生命周期

        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void OnEnable()
        {
            // 初始化服务
            InitializeServices();

            // 初始化UI
            InitializeUI();

            // 加载窗口状态
            LoadWindowState();

            // 标记为已初始化
            _isInitialized = true;
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void OnDisable()
        {
            // 保存窗口状态
            SaveWindowState();
        }

        /// <summary>
        /// 绘制窗口内容
        /// </summary>
        private void OnGUI()
        {
            if (!_isInitialized)
                return;

            // 绘制标题栏
            DrawTitleBar();

            // 绘制工具栏
            DrawToolbar();

            // 开始滚动视图
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 绘制当前页面
            if (_pageNavigator.CurrentPage != null)
            {
                _pageNavigator.CurrentPage.Draw();
            }

            // 结束滚动视图
            EditorGUILayout.EndScrollView();

            // 绘制导航按钮
            DrawNavigationButtons();

            // 处理键盘事件
            HandleKeyboardEvents();

            // 如果有状态变化，重绘窗口
            if (GUI.changed)
            {
                Repaint();
            }
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化服务
        /// </summary>
        private void InitializeServices()
        {
            // 初始化模板管理器
            _templateManager = TemplateManager.Instance;

            // 初始化配置管理器
            _configManager = ConfigManager.Instance;

            // 初始化UI状态管理器
            UIStateManager.Instance.ResetState();
            UIStateManager.Instance.OnStateChanged += OnStateChanged;
        }

        /// <summary>
        /// 初始化UI
        /// </summary>
        private void InitializeUI()
        {
            // 初始化页面导航器
            _pageNavigator = new PageNavigator();

            // 添加页面
            CreatePages();

            // 初始化工具栏
            InitializeToolbar();

            // 确保第一个页面的OnEnter方法被调用
            if (_pageNavigator.PageCount > 0)
            {
                _pageNavigator.GetPageAt(0).OnEnter();
            }
        }

        /// <summary>
        /// 创建页面
        /// </summary>
        private void CreatePages()
        {
            // 添加模板选择页面
            _pageNavigator.AddPage(new TemplateSelectionPage(_templateManager));

            // 添加配置页面（占位）
            _pageNavigator.AddPage(new PlaceholderPage("基本配置", "配置包的基本信息"));

            // 添加依赖页面（占位）
            _pageNavigator.AddPage(new PlaceholderPage("依赖配置", "配置包的依赖项"));

            // 添加高级选项页面（占位）
            _pageNavigator.AddPage(new PlaceholderPage("高级选项", "配置包的高级选项"));

            // 添加摘要页面（占位）
            _pageNavigator.AddPage(new PlaceholderPage("摘要", "查看包配置摘要"));

            // 添加结果页面（占位）
            _pageNavigator.AddPage(new PlaceholderPage("创建结果", "包创建结果"));
        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void InitializeToolbar()
        {
            // 创建工具栏标题
            _toolbarTitles = new string[_pageNavigator.PageCount];

            for (int i = 0; i < _pageNavigator.PageCount; i++)
            {
                _toolbarTitles[i] = _pageNavigator.GetPageAt(i)?.Title ?? $"步骤 {i + 1}";
            }
        }

        #endregion

        #region UI绘制方法

        /// <summary>
        /// 绘制标题栏
        /// </summary>
        private void DrawTitleBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            GUILayout.FlexibleSpace();

            // 绘制设置按钮
            if (GUILayout.Button("设置", EditorStyles.toolbarButton))
            {
                _showSettings = !_showSettings;
            }

            EditorGUILayout.EndHorizontal();

            // 绘制分隔线
            PackageCreatorStyles.DrawSeparator();
        }

        /// <summary>
        /// 绘制工具栏
        /// </summary>
        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            // 绘制工具栏按钮
            int newSelectedIndex = GUILayout.Toolbar(_selectedToolbarIndex, _toolbarTitles, EditorStyles.toolbarButton);

            // 处理页面切换
            if (newSelectedIndex != _pageNavigator.CurrentPageIndex && newSelectedIndex >= 0 && newSelectedIndex < _pageNavigator.PageCount)
            {
                // 只允许向前导航到有效的页面或向后导航
                if (newSelectedIndex < _pageNavigator.CurrentPageIndex || _pageNavigator.GetPageAt(newSelectedIndex - 1)?.IsValid == true)
                {
                    _pageNavigator.GoToPage(newSelectedIndex);
                    _selectedToolbarIndex = newSelectedIndex;
                }
            }
            // 确保即使是第一次打开窗口（newSelectedIndex等于CurrentPageIndex）也能正确初始化当前页面
            else if (newSelectedIndex == 0 && _pageNavigator.CurrentPageIndex == 0 && !_isInitialPageEntered)
            {
                _pageNavigator.GetPageAt(0).OnEnter();
                _isInitialPageEntered = true;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
        }

        /// <summary>
        /// 绘制导航按钮
        /// </summary>
        private void DrawNavigationButtons()
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            // 上一步按钮
            EditorGUI.BeginDisabledGroup(!_pageNavigator.CanGoPrevious);
            if (GUILayout.Button("上一步", PackageCreatorStyles.SecondaryButton, GUILayout.Width(100)))
            {
                _pageNavigator.GoPrevious();
                _selectedToolbarIndex = _pageNavigator.CurrentPageIndex;
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.FlexibleSpace();

            // 下一步/完成按钮
            EditorGUI.BeginDisabledGroup(!_pageNavigator.CanGoNext && !_pageNavigator.CurrentPage?.IsValid == true);

            string buttonText = _pageNavigator.CurrentPageIndex == _pageNavigator.PageCount - 2 ? "创建包" : "下一步";
            GUIStyle buttonStyle = _pageNavigator.CurrentPageIndex == _pageNavigator.PageCount - 2 ?
                PackageCreatorStyles.PrimaryButton : PackageCreatorStyles.SecondaryButton;

            if (GUILayout.Button(buttonText, buttonStyle, GUILayout.Width(100)))
            {
                if (_pageNavigator.CurrentPageIndex == _pageNavigator.PageCount - 2)
                {
                    // 如果是倒数第二页，执行创建操作
                    StartCreatingPackage();
                }
                else
                {
                    // 否则前进到下一页
                    _pageNavigator.GoNext();
                    _selectedToolbarIndex = _pageNavigator.CurrentPageIndex;
                }
            }

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 处理键盘事件
        /// </summary>
        private void HandleKeyboardEvents()
        {
            Event current = Event.current;

            if (current.type == EventType.KeyDown)
            {
                if (current.keyCode == KeyCode.Escape)
                {
                    // ESC键关闭设置面板
                    if (_showSettings)
                    {
                        _showSettings = false;
                        current.Use();
                    }
                }
                else if (current.control && current.keyCode == KeyCode.S)
                {
                    // Ctrl+S保存当前状态
                    SaveWindowState();
                    current.Use();
                }
            }
        }

        /// <summary>
        /// 状态变更处理
        /// </summary>
        private void OnStateChanged(PackageCreationState state)
        {
            // 根据状态更新UI
            Repaint();
        }

        #endregion

        #region 状态管理

        /// <summary>
        /// 保存窗口状态
        /// </summary>
        private void SaveWindowState()
        {
            // 保存当前页面索引
            EditorPrefs.SetInt(CurrentPageIndexKey, _pageNavigator.CurrentPageIndex);

            // 保存其他状态...
        }

        /// <summary>
        /// 加载窗口状态
        /// </summary>
        private void LoadWindowState()
        {
            // 加载当前页面索引
            int savedPageIndex = EditorPrefs.GetInt(CurrentPageIndexKey, 0);

            // 确保索引有效
            if (savedPageIndex >= 0 && savedPageIndex < _pageNavigator.PageCount)
            {
                _pageNavigator.GoToPage(savedPageIndex);
                _selectedToolbarIndex = savedPageIndex;
            }

            // 加载其他状态...
        }

        /// <summary>
        /// 开始创建包
        /// </summary>
        private void StartCreatingPackage()
        {
            // 更新状态
            UIStateManager.Instance.UpdateState(state =>
            {
                state.IsCreating = true;
                state.CreationProgress = 0f;
            });

            // 执行创建操作
            CreatePackage();
        }

        /// <summary>
        /// 创建包
        /// </summary>
        private void CreatePackage()
        {
            // 前进到结果页面
            _pageNavigator.GoNext();
            _selectedToolbarIndex = _pageNavigator.CurrentPageIndex;

            // 这里将添加实际的包创建逻辑
            // 目前只是占位，后续会实现具体功能

            // 模拟创建完成
            UIStateManager.Instance.UpdateState(state =>
            {
                state.IsCreationSuccessful = true;
                state.CreationProgress = 1f;
                state.IsCreating = false;
            });
        }

        #endregion
    }

    /// <summary>
    /// 占位页面，用于开发阶段
    /// </summary>
    public class PlaceholderPage : BasePage
    {
        private string _title;
        private string _description;

        /// <summary>
        /// 页面标题
        /// </summary>
        public override string Title => _title;

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public override bool IsValid => true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">页面标题</param>
        /// <param name="description">页面描述</param>
        public PlaceholderPage(string title, string description)
        {
            _title = title;
            _description = description;
        }

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        public override void Draw()
        {
            DrawTitle();

            EditorGUILayout.HelpBox($"这是{_title}页面的占位内容。\n\n{_description}", MessageType.Info);

            EditorGUILayout.Space(20);

            EditorGUILayout.LabelField("此页面尚未实现，将在后续开发中完成。", EditorStyles.boldLabel);
        }
    }
}
