# TByd.PackageCreator UI 架构设计

## 一、设计理念

TByd.PackageCreator 的 UI 架构采用 MVVM（Model-View-ViewModel）模式，旨在实现以下目标：

1. **关注点分离**：将 UI 表现、业务逻辑和数据模型清晰分离
2. **可测试性**：便于对各个组件进行单元测试
3. **可维护性**：避免单一脚本过于庞大，便于团队协作和代码维护
4. **可扩展性**：便于添加新功能和页面，不影响现有代码
5. **一致性**：确保整个应用的 UI 风格和交互方式保持一致

## 二、目录结构

```
Assets/TByd.PackageCreator/Editor/UI/
├── Windows/                  # 主窗口和对话框
│   ├── PackageCreatorWindow.cs  # 主窗口
│   ├── SettingsWindow.cs        # 设置窗口
│   └── ResultWindow.cs          # 结果窗口
├── Pages/                    # 页面组件（对应向导步骤）
│   ├── TemplateSelectionPage.cs # 模板选择页面
│   ├── ConfigurationPage.cs     # 配置页面
│   ├── DependenciesPage.cs      # 依赖配置页面
│   ├── AdvancedOptionsPage.cs   # 高级选项页面
│   └── SummaryPage.cs           # 摘要页面
├── Controls/                 # 可复用UI控件
│   ├── TemplateCardControl.cs   # 模板卡片控件
│   ├── SearchBar.cs             # 搜索栏控件
│   ├── DependencyItem.cs        # 依赖项控件
│   └── ValidationMessage.cs     # 验证消息控件
├── ViewModels/               # 视图模型（连接UI和业务逻辑）
│   ├── TemplateSelectionViewModel.cs
│   ├── ConfigurationViewModel.cs
│   ├── DependenciesViewModel.cs
│   └── ResultViewModel.cs
├── Styles/                   # UI样式
│   ├── PackageCreatorStyles.cs  # 基础样式
│   ├── ThemeManager.cs          # 主题管理
│   └── ColorScheme.cs           # 颜色方案
└── Utils/                    # UI工具类
    ├── EditorGUILayoutExtensions.cs
    ├── UIAnimations.cs
    └── ResponsiveLayout.cs
```

## 三、核心组件

### 3.1 页面导航系统

页面导航系统负责管理向导式 UI 流程，处理页面之间的切换和数据传递。

```csharp
namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// 页面接口，所有页面组件必须实现此接口
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 页面是否有效，用于确定是否可以进入下一页
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        void OnEnter();

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        void OnExit();

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        void Draw();
    }

    /// <summary>
    /// 页面导航器，管理页面切换和数据传递
    /// </summary>
    public class PageNavigator
    {
        private List<IPage> _pages = new List<IPage>();
        private int _currentPageIndex = 0;

        /// <summary>
        /// 当前页面
        /// </summary>
        public IPage CurrentPage => _pages[_currentPageIndex];

        /// <summary>
        /// 是否可以前进到下一页
        /// </summary>
        public bool CanGoNext => _currentPageIndex < _pages.Count - 1 && CurrentPage.IsValid;

        /// <summary>
        /// 是否可以返回上一页
        /// </summary>
        public bool CanGoPrevious => _currentPageIndex > 0;

        /// <summary>
        /// 前进到下一页
        /// </summary>
        public void GoNext()
        {
            if (CanGoNext)
            {
                CurrentPage.OnExit();
                _currentPageIndex++;
                CurrentPage.OnEnter();
            }
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        public void GoPrevious()
        {
            if (CanGoPrevious)
            {
                CurrentPage.OnExit();
                _currentPageIndex--;
                CurrentPage.OnEnter();
            }
        }

        /// <summary>
        /// 跳转到指定页面
        /// </summary>
        /// <param name="index">页面索引</param>
        public void GoToPage(int index)
        {
            if (index >= 0 && index < _pages.Count)
            {
                CurrentPage.OnExit();
                _currentPageIndex = index;
                CurrentPage.OnEnter();
            }
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="page">页面实例</param>
        public void AddPage(IPage page)
        {
            _pages.Add(page);
        }
    }
}
```

### 3.2 状态管理系统

状态管理系统负责管理 UI 状态和数据流，确保 UI 组件与数据模型的同步。

```csharp
namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// UI状态管理器，处理UI状态和数据流
    /// </summary>
    public class UIStateManager
    {
        // 单例实例
        private static UIStateManager _instance;
        public static UIStateManager Instance => _instance ?? (_instance = new UIStateManager());

        // 全局状态
        public PackageCreationState CreationState { get; private set; } = new PackageCreationState();

        // 状态变更事件
        public event Action<PackageCreationState> OnStateChanged;

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="updateAction">更新操作</param>
        public void UpdateState(Action<PackageCreationState> updateAction)
        {
            updateAction?.Invoke(CreationState);
            OnStateChanged?.Invoke(CreationState);
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        public void ResetState()
        {
            CreationState = new PackageCreationState();
            OnStateChanged?.Invoke(CreationState);
        }
    }

    /// <summary>
    /// 包创建状态，存储整个创建过程的状态数据
    /// </summary>
    public class PackageCreationState
    {
        // 选中的模板
        public IPackageTemplate SelectedTemplate { get; set; }

        // 包配置
        public PackageConfig PackageConfig { get; set; } = new PackageConfig();

        // 验证结果
        public ValidationResult ValidationResult { get; set; }

        // 创建结果
        public bool IsCreationSuccessful { get; set; }

        // 错误信息
        public string ErrorMessage { get; set; }
    }
}
```

### 3.3 数据绑定系统

数据绑定系统提供简单的数据绑定功能，连接 UI 和数据模型。

```csharp
namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// 数据绑定类，提供简单的数据绑定功能
    /// </summary>
    /// <typeparam name="T">绑定数据类型</typeparam>
    public class Binding<T>
    {
        private T _value;

        /// <summary>
        /// 绑定的值
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _value = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// 值变更事件
        /// </summary>
        public event Action<T> OnValueChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialValue">初始值</param>
        public Binding(T initialValue = default)
        {
            _value = initialValue;
        }
    }
}
```

## 四、视图模型设计

视图模型是连接 UI 和业务逻辑的桥梁，负责处理 UI 事件和数据转换。

### 4.1 基础视图模型接口

```csharp
namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 视图模型接口，所有视图模型必须实现此接口
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// 初始化视图模型
        /// </summary>
        void Initialize();

        /// <summary>
        /// 清理视图模型资源
        /// </summary>
        void Cleanup();
    }
}
```

### 4.2 模板选择视图模型

```csharp
namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 模板选择视图模型
    /// </summary>
    public class TemplateSelectionViewModel : IViewModel
    {
        // 模板管理器
        private readonly ITemplateManager _templateManager;

        // 模板列表
        public Binding<List<IPackageTemplate>> Templates { get; } = new Binding<List<IPackageTemplate>>(new List<IPackageTemplate>());

        // 选中的模板
        public Binding<IPackageTemplate> SelectedTemplate { get; } = new Binding<IPackageTemplate>();

        // 搜索关键字
        public Binding<string> SearchKeyword { get; } = new Binding<string>("");

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templateManager">模板管理器</param>
        public TemplateSelectionViewModel(ITemplateManager templateManager)
        {
            _templateManager = templateManager;
        }

        /// <summary>
        /// 初始化视图模型
        /// </summary>
        public void Initialize()
        {
            // 加载模板列表
            Templates.Value = _templateManager.GetAllTemplates().ToList();

            // 监听搜索关键字变化
            SearchKeyword.OnValueChanged += keyword => FilterTemplates(keyword);
        }

        /// <summary>
        /// 清理视图模型资源
        /// </summary>
        public void Cleanup()
        {
            // 清理事件订阅
            SearchKeyword.OnValueChanged = null;
        }

        /// <summary>
        /// 根据关键字筛选模板
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        private void FilterTemplates(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                Templates.Value = _templateManager.GetAllTemplates().ToList();
                return;
            }

            Templates.Value = _templateManager.GetAllTemplates()
                .Where(t => t.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// 选择模板
        /// </summary>
        /// <param name="template">要选择的模板</param>
        public void SelectTemplate(IPackageTemplate template)
        {
            SelectedTemplate.Value = template;

            // 更新全局状态
            UIStateManager.Instance.UpdateState(state => state.SelectedTemplate = template);
        }
    }
}
```

## 五、页面组件设计

页面组件是 UI 的主要构成部分，每个页面对应向导流程中的一个步骤。

### 5.1 基础页面实现

```csharp
namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 基础页面类，提供页面通用功能
    /// </summary>
    public abstract class BasePage : IPage
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public abstract bool IsValid { get; }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// 绘制页面标题
        /// </summary>
        protected void DrawTitle()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(Title, PackageCreatorStyles.TitleStyle);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }
    }
}
```

### 5.2 模板选择页面

```csharp
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
            DrawTitle();

            // 绘制搜索栏
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("搜索:", GUILayout.Width(50));
            string searchText = EditorGUILayout.TextField(_viewModel.SearchKeyword.Value);
            if (searchText != _viewModel.SearchKeyword.Value)
            {
                _viewModel.SearchKeyword.Value = searchText;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            // 绘制模板列表
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            if (_viewModel.Templates.Value.Count == 0)
            {
                EditorGUILayout.HelpBox("没有找到匹配的模板", MessageType.Info);
            }
            else
            {
                foreach (var template in _viewModel.Templates.Value)
                {
                    bool isSelected = _viewModel.SelectedTemplate.Value == template;

                    // 使用模板卡片控件
                    if (TemplateCardControl.Draw(template, isSelected))
                    {
                        _viewModel.SelectTemplate(template);
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }
}
```

## 六、主窗口设计

主窗口是用户交互的主要入口，负责组织和管理页面组件。

```csharp
namespace TByd.PackageCreator.Editor.UI.Windows
{
    /// <summary>
    /// 包创建器主窗口
    /// </summary>
    public class PackageCreatorWindow : EditorWindow
    {
        // 页面导航器
        private PageNavigator _pageNavigator;

        // 服务实例
        private ITemplateManager _templateManager;
        private IConfigManager _configManager;

        // 窗口尺寸
        private const float MinWindowWidth = 800f;
        private const float MinWindowHeight = 600f;
        private const float PreferredWindowWidth = 900f;
        private const float PreferredWindowHeight = 700f;

        // 菜单路径
        private const string MenuPath = "Tools/TByd/UPM Package Creator";
        private const string WindowTitle = "UPM Package Creator";

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

        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void OnEnable()
        {
            // 初始化服务
            _templateManager = new TemplateManager();
            _configManager = new ConfigManager();

            // 初始化页面导航器
            _pageNavigator = new PageNavigator();

            // 添加页面
            _pageNavigator.AddPage(new TemplateSelectionPage(_templateManager));
            _pageNavigator.AddPage(new ConfigurationPage(_configManager));
            _pageNavigator.AddPage(new DependenciesPage(_configManager));
            _pageNavigator.AddPage(new AdvancedOptionsPage(_configManager));
            _pageNavigator.AddPage(new SummaryPage(_templateManager, _configManager));

            // 进入第一个页面
            _pageNavigator.CurrentPage.OnEnter();
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
            // 绘制当前页面
            _pageNavigator.CurrentPage.Draw();

            // 绘制导航按钮
            DrawNavigationButtons();
        }

        /// <summary>
        /// 绘制导航按钮
        /// </summary>
        private void DrawNavigationButtons()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // 上一步按钮
            EditorGUI.BeginDisabledGroup(!_pageNavigator.CanGoPrevious);
            if (GUILayout.Button("上一步", GUILayout.Width(100)))
            {
                _pageNavigator.GoPrevious();
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.Space(10);

            // 下一步按钮
            EditorGUI.BeginDisabledGroup(!_pageNavigator.CanGoNext);
            if (GUILayout.Button("下一步", GUILayout.Width(100)))
            {
                _pageNavigator.GoNext();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 保存窗口状态
        /// </summary>
        private void SaveWindowState()
        {
            // 保存当前页面索引
            EditorPrefs.SetInt("TByd.PackageCreator.CurrentPageIndex", _pageNavigator.CurrentPageIndex);
        }

        /// <summary>
        /// 加载窗口状态
        /// </summary>
        private void LoadWindowState()
        {
            // 加载当前页面索引
            int pageIndex = EditorPrefs.GetInt("TByd.PackageCreator.CurrentPageIndex", 0);
            _pageNavigator.GoToPage(pageIndex);
        }
    }
}
```

## 七、实现建议

### 7.1 渐进式开发

1. 首先实现基础架构和导航系统
2. 然后实现各个页面的基本框架
3. 最后完善各页面的详细功能

### 7.2 组件化开发

1. 每个 UI 组件应该是自包含的，有明确的职责
2. 组件之间通过接口或事件通信，避免直接依赖
3. 使用依赖注入简化组件间的关系

### 7.3 测试策略

1. 为视图模型编写单元测试
2. 创建 UI 测试场景验证交互流程
3. 使用模拟服务进行隔离测试

### 7.4 性能考虑

1. 避免在 OnGUI 中进行耗时操作
2. 使用缓存减少重复计算
3. 延迟加载大型资源
4. 优化绘制频率和复杂度

## 八、未来扩展

### 8.1 自定义控件系统

未来可以考虑实现更丰富的自定义控件系统，提供更多样化的 UI 元素。

### 8.2 主题系统扩展

扩展主题系统，支持用户自定义主题和样式。

### 8.3 响应式布局增强

增强响应式布局支持，适应不同屏幕尺寸和分辨率。

### 8.4 多语言支持集成

将 UI 系统与本地化系统深度集成，实现无缝的多语言切换。
