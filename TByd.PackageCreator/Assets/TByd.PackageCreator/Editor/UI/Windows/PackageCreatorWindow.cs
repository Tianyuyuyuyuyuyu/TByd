using System;
using System.Collections.Generic;
using System.IO;
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

            // 如果正在创建包，显示进度条
            var state = UIStateManager.Instance.CreationState;
            if (state.IsCreating)
            {
                DrawCreationProgress();
                return; // 创建过程中不显示其他UI元素
            }

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

        /// <summary>
        /// 绘制包创建进度
        /// </summary>
        private void DrawCreationProgress()
        {
            var state = UIStateManager.Instance.CreationState;

            GUILayout.Space(20);

            EditorGUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            ProgressIndicatorControl.DrawFullProgressIndicator(
                "正在创建包",
                "请稍候，正在创建UPM包...",
                state.CreationProgress,
                false
            );

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();

            // 重绘窗口以更新进度
            Repaint();
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

            // 添加配置页面
            _pageNavigator.AddPage(new ConfigurationPage(_configManager));

            // 添加依赖页面
            _pageNavigator.AddPage(new DependenciesPage(_configManager));

            // 添加高级选项页面
            _pageNavigator.AddPage(new AdvancedOptionsPage(_configManager));

            // 添加摘要页面
            _pageNavigator.AddPage(new SummaryPage(_configManager, _templateManager));

            // 添加结果页面
            var resultPage = new ResultPage(_configManager);
            resultPage.OnRestartRequested += () => _pageNavigator.GoToPage(0);
            _pageNavigator.AddPage(resultPage);
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
                // 尝试切换到依赖配置页面（页面索引为2）
                if (newSelectedIndex == 2)
                {
                    // 对于依赖配置页面，我们允许直接访问，但基本配置仍然需要有效
                    var configPage = _pageNavigator.GetPageAt(1);
                    if (configPage != null && !configPage.IsValid)
                    {
                        // 提示用户基本配置无效，但仍然允许访问依赖页面
                        EditorUtility.DisplayDialog("警告",
                            "基本配置页面的信息不完整。在创建包之前，您需要完成必要的配置信息。\n\n" +
                            "您可以继续编辑依赖项，但请在最终创建包之前返回完成基本配置。",
                            "了解");
                    }

                    // 无论如何都允许切换到依赖页面
                    _pageNavigator.GoToPage(newSelectedIndex);
                    _selectedToolbarIndex = newSelectedIndex;
                }
                // 对于其他页面，保持原有逻辑
                else if (newSelectedIndex < _pageNavigator.CurrentPageIndex || _pageNavigator.GetPageAt(newSelectedIndex - 1)?.IsValid == true)
                {
                    _pageNavigator.GoToPage(newSelectedIndex);
                    _selectedToolbarIndex = newSelectedIndex;
                }
                else if (_pageNavigator.GetPageAt(newSelectedIndex - 1) != null && !_pageNavigator.GetPageAt(newSelectedIndex - 1).IsValid)
                {
                    // 当前页面无效时，显示提示信息
                    string previousPageTitle = _toolbarTitles[newSelectedIndex - 1];
                    EditorUtility.DisplayDialog("无法导航", $"在进入下一步之前，请先完成{previousPageTitle}页面的必要信息。", "确定");
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
            try
            {
                // 验证配置、模板和设置
                var config = _configManager.CurrentConfig;
                var template = UIStateManager.Instance.CreationState.SelectedTemplate;

                // 深度克隆配置，确保不会使用引用
                var configForCreation = new PackageConfig
                {
                    Name = config.Name,
                    DisplayName = config.DisplayName,
                    Description = config.Description,
                    Version = config.Version,
                    UnityVersion = config.UnityVersion,
                    Keywords = config.Keywords != null ? new List<string>(config.Keywords) : null,
                    Dependencies = config.Dependencies,
                    CustomVariables = config.CustomVariables != null ? new Dictionary<string, string>(config.CustomVariables) : null,
                    RootNamespace = config.RootNamespace,
                    Company = config.Company
                };

                // 拷贝作者信息
                if (config.Author != null)
                {
                    configForCreation.Author = new PackageAuthor
                    {
                        Name = config.Author.Name,
                        Email = config.Author.Email,
                        Url = config.Author.Url
                    };
                }

                // 保存当前配置到UIStateManager
                UIStateManager.Instance.UpdateState(state =>
                {
                    state.CurrentConfig = configForCreation;
                    Debug.Log($"已保存当前配置到UIStateManager: {configForCreation.Name}, 作者: {configForCreation.Author?.Name ?? "未指定"}");
                });

                // 导航到结果页面（结果页面是最后一个页面，索引为5）
                _pageNavigator.GoToPage(5);
                _selectedToolbarIndex = _pageNavigator.CurrentPageIndex;

                // 检查配置和模板是否有效
                if (configForCreation == null)
                {
                    UIStateManager.Instance.UpdateState(state =>
                    {
                        state.IsCreationSuccessful = false;
                        state.ErrorMessage = "无效的包配置";
                        state.CreationProgress = 1f;
                        state.IsCreating = false;

                        // 创建失败的验证结果
                        var validationResult = new ValidationResult();
                        validationResult.AddError("无效的包配置");
                        state.CreationResult = validationResult;
                    });
                    return;
                }

                if (template == null)
                {
                    UIStateManager.Instance.UpdateState(state =>
                    {
                        state.IsCreationSuccessful = false;
                        state.ErrorMessage = "无效的包模板";
                        state.CreationProgress = 1f;
                        state.IsCreating = false;

                        // 创建失败的验证结果
                        var validationResult = new ValidationResult();
                        validationResult.AddError("无效的包模板");
                        state.CreationResult = validationResult;
                    });
                    return;
                }

                // 设置初始状态
                UIStateManager.Instance.UpdateState(state =>
                {
                    state.IsCreating = true;
                    state.CreationProgress = 0f;
                });

                // 获取Unity项目根目录（Assets的父目录）
                string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                string packagePath = Path.GetFullPath(Path.Combine(projectRoot, configForCreation.Name));

                // 确保路径采用统一的格式
                packagePath = packagePath.Replace("\\", "/");
                Debug.Log($"初始化包路径: {packagePath}");

                // 基于模板生成包结构
                bool success = false;
                string errorMessage = null;
                ValidationResult validationResult = null;

                try
                {
                    // 调用模板的Generate方法生成包结构
                    Debug.Log("调用模板的Generate方法");

                    // 使用EditorApplication.delayCall避免阻塞主线程
                    float startTime = Time.realtimeSinceStartup;
                    bool isComplete = false;
                    bool isTimeout = false;

                    // 启动一个检查超时的计时器
                    EditorApplication.update += CheckTimeout;

                    // 在延迟调用中执行包生成
                    EditorApplication.delayCall += () =>
                    {
                        try
                        {
                            // 调用模板的Generate方法，该方法现在会立即返回而不会阻塞线程
                            success = template.Generate(configForCreation, packagePath);
                            Debug.Log($"模板Generate方法返回: {success}");

                            // 注意：Generate方法已修改为异步执行，返回true表示任务已成功启动
                            // 实际的任务完成会由BasePackageTemplate内部的异步机制处理

                            // 为模板完成设置一个更长的超时时间
                            // 通过UpdateUI方法监听UIStateManager状态变化来确定任务是否完成
                            // 这里不做任何处理，让异步过程继续进行
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                            errorMessage = $"创建包时发生异常: {e.Message}";
                            isComplete = true;
                            UpdateUIWithResult();
                        }
                    };

                    // 超时检查函数
                    void CheckTimeout()
                    {
                        if (isComplete) return;

                        // 检查UIStateManager中的CreationProgress是否已经到达1.0
                        var state = UIStateManager.Instance.CreationState;

                        // 检查多个条件来确定任务是否已完成
                        if (state.CreationProgress >= 0.99f ||
                            state.IsCreationSuccessful ||
                            !string.IsNullOrEmpty(state.PackagePath) ||
                            !state.IsCreating)
                        {
                            // 说明BasePackageTemplate已经将任务标记为完成
                            isComplete = true;
                            // 如果已经有IsCreationSuccessful设置，使用它
                            success = state.IsCreationSuccessful;

                            // 停止超时检查
                            EditorApplication.update -= CheckTimeout;
                            Debug.Log("检测到任务已完成，停止超时检查");
                            return;
                        }

                        float elapsedTime = Time.realtimeSinceStartup - startTime;
                        // 增加超时时间至90秒，文件系统操作在某些系统上可能比较慢
                        if (elapsedTime > 90f)
                        {
                            // 在超时前，检查是否实际已经创建了文件
                            bool filesExist = Directory.Exists(packagePath);

                            if (filesExist)
                            {
                                // 如果文件已经存在，认为是成功的，即使超时
                                Debug.Log($"检测到文件夹{packagePath}已存在，认为任务已成功完成");
                                isComplete = true;
                                success = true;

                                // 手动更新状态
                                UIStateManager.Instance.UpdateState(s =>
                                {
                                    s.IsCreationSuccessful = true;
                                    s.CreationProgress = 1.0f;
                                    s.PackagePath = packagePath;
                                    s.IsCreating = false;
                                });

                                // 停止超时检查
                                EditorApplication.update -= CheckTimeout;
                                return;
                            }

                            isTimeout = true;
                            isComplete = true;
                            errorMessage = $"创建包操作超时(超过90秒)，可能是由于文件系统操作被阻塞";
                            Debug.LogError(errorMessage);

                            // 停止超时检查
                            EditorApplication.update -= CheckTimeout;

                            // 更新UI状态
                            UpdateUIWithResult();
                        }
                    }

                    // 更新UI的结果状态函数
                    void UpdateUIWithResult()
                    {
                        // 停止超时检查
                        EditorApplication.update -= CheckTimeout;

                        // 检查CreationState是否已经被BasePackageTemplate更新
                        var currentState = UIStateManager.Instance.CreationState;

                        // 如果BasePackageTemplate设置了包路径并成功完成，优先使用这个状态
                        if (currentState.CreationProgress >= 0.99f &&
                            currentState.IsCreationSuccessful &&
                            !string.IsNullOrEmpty(currentState.PackagePath) &&
                            !isTimeout)
                        {
                            // 如果BasePackageTemplate已经更新了状态且成功，不要覆盖它
                            Debug.Log("包创建已由异步处理完成并成功，跳过状态更新");
                            // 强制重绘窗口
                            Repaint();
                            return;
                        }

                        // 如果包创建失败
                        if (!success || isTimeout)
                        {
                            if (string.IsNullOrEmpty(errorMessage))
                            {
                                // 如果没有异常信息，则使用通用错误信息
                                errorMessage = "创建包时发生错误，请查看控制台获取详细信息。";
                            }

                            // 创建验证结果对象，包含错误信息
                            validationResult = new ValidationResult();
                            validationResult.AddError(errorMessage);

                            Debug.LogError($"包创建失败: {errorMessage}");
                        }
                        else
                        {
                            // 包创建成功
                            Debug.Log($"包创建成功！路径: {packagePath}");
                        }

                        // 更新UI状态
                        UIStateManager.Instance.UpdateState(state =>
                        {
                            // 只有在当前状态不是已完成状态时才更新，或者是被超时强制更新
                            if (isTimeout || !(state.CreationProgress >= 0.99f && state.IsCreationSuccessful && !string.IsNullOrEmpty(state.PackagePath)))
                            {
                                state.IsCreationSuccessful = success && !isTimeout;
                                state.PackagePath = packagePath;
                                state.ErrorMessage = errorMessage;
                                state.CreationProgress = 1f;
                                state.IsCreating = false;
                                state.CreationResult = validationResult;

                                Debug.Log($"更新UI状态: 成功={success && !isTimeout}, 路径={packagePath}, 错误消息={errorMessage ?? "无"}");
                            }
                        });

                        // 强制重绘窗口
                        Repaint();
                    }

                    // 将下面的代码移入延迟调用中，避免阻塞主线程
                    return;
                }
                catch (Exception ex)
                {
                    // 更新状态为失败
                    UIStateManager.Instance.UpdateState(state =>
                    {
                        state.IsCreationSuccessful = false;
                        state.ErrorMessage = $"创建包时发生错误: {ex.Message}";
                        state.CreationProgress = 1f;
                        state.IsCreating = false;

                        // 创建失败的验证结果
                        var validationResult = new ValidationResult();
                        validationResult.AddError(ex.Message);
                        state.CreationResult = validationResult;
                    });
                }
            }
            catch (System.Exception ex)
            {
                // 更新状态为失败
                UIStateManager.Instance.UpdateState(state =>
                {
                    state.IsCreationSuccessful = false;
                    state.ErrorMessage = $"创建包时发生错误: {ex.Message}";
                    state.CreationProgress = 1f;
                    state.IsCreating = false;

                    // 创建失败的验证结果
                    var validationResult = new ValidationResult();
                    validationResult.AddError(ex.Message);
                    state.CreationResult = validationResult;
                });
            }
        }

        /// <summary>
        /// 模拟单个创建步骤
        /// </summary>
        /// <param name="progress">步骤完成后的进度</param>
        /// <param name="callback">步骤完成后的回调</param>
        private void SimulateCreationStep(float progress, Action callback)
        {
            // 更新进度
            UIStateManager.Instance.UpdateState(state =>
            {
                state.CreationProgress = progress;
            });

            // 重绘窗口以更新进度条
            Repaint();

            // 模拟步骤耗时（0.5秒）
            EditorApplication.delayCall += () =>
            {
                callback?.Invoke();
            };
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
