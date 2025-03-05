# 反馈系统使用示例

## 示例一：API 设计反馈

### 情境

假设 AI 生成了以下 API 设计：

```csharp
public static class ResourceLoader
{
    public static T LoadResource<T>(string resourcePath, ResourceLoadingFlags flags,
        int priorityLevel, bool instantiate, IResourceLoadCallback callback,
        Dictionary<string, object> additionalParams = null) where T : UnityEngine.Object
    {
        // 实现代码...
    }
}
```

### 简短反馈方式

在对话中直接提供反馈：

```
反馈:#API设计# - 资源加载API参数过多，不符合Unity风格，应简化接口并提供多个重载方法
```

### 详细反馈记录

将以下内容添加到`.cursor/feedback/ai_feedback.md`：

```
## 2025-03-10: 资源加载API设计过于复杂
- 反馈类别: #API设计#
- 问题描述: 提供的ResourceLoader.LoadResource方法包含6个参数，过于复杂，不符合Unity简洁API设计风格
- 期望改进:
  1. 减少主方法参数数量（不超过3个）
  2. 提供多个便捷的重载方法
  3. 复杂参数应封装到配置对象中
- 参考资料: Unity Resources.Load API设计
```

### 规则改进效果

改进后，AI 生成更符合 Unity 风格的 API：

```csharp
public static class ResourceLoader
{
    // 简洁的主要API
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        return Load<T>(path, ResourceLoadOptions.Default);
    }

    // 带选项的重载
    public static T Load<T>(string path, ResourceLoadOptions options) where T : UnityEngine.Object
    {
        // 实现代码...
    }

    // 异步加载版本
    public static async Task<T> LoadAsync<T>(string path) where T : UnityEngine.Object
    {
        return await LoadAsync<T>(path, ResourceLoadOptions.Default);
    }

    // 带选项的异步加载
    public static async Task<T> LoadAsync<T>(string path, ResourceLoadOptions options) where T : UnityEngine.Object
    {
        // 实现代码...
    }
}

// 复杂参数封装在配置对象中
public class ResourceLoadOptions
{
    public static ResourceLoadOptions Default => new ResourceLoadOptions();

    public ResourceLoadingFlags Flags { get; set; } = ResourceLoadingFlags.None;
    public int PriorityLevel { get; set; } = 0;
    public bool Instantiate { get; set; } = false;
    public IResourceLoadCallback Callback { get; set; } = null;
    public Dictionary<string, object> AdditionalParams { get; set; } = null;
}
```

## 示例二：性能优化反馈

### 情境

假设 AI 提供的性能优化建议过于泛泛：

```
为了优化游戏性能，应该减少Update中的操作，并且注意内存分配。使用对象池可以提高性能。
```

### 简短反馈方式

```
反馈:#性能优化# - 性能建议过于泛泛，缺乏具体可测量的优化技术和预期改进数据
```

### 详细反馈记录

```
## 2025-03-12: 性能优化建议缺乏具体性
- 反馈类别: #性能优化#
- 问题描述: 提供的性能优化建议缺乏具体场景针对性，没有量化的改进指标和测量方法
- 期望改进:
  1. 提供特定场景（如粒子系统、UI、物理等）的具体优化技术
  2. 包含可量化的性能指标和预期改进幅度
  3. 提供性能测量方法和工具推荐
- 参考资料: Unity Profiler文档和官方性能优化指南
```

### 规则改进效果

改进后，AI 提供更具体的优化建议：

```
针对UI系统的具体优化策略：

1. 减少Canvas Rebuild：
   - 问题：频繁修改UI元素导致Canvas重建，在Profiler中表现为大量"Canvas.BuildBatch"调用
   - 优化方法：使用CanvasGroup控制整组UI的显隐，而不是逐个激活/禁用
   - 预期改进：UI密集场景下可减少30-50%的渲染时间
   - 测量方法：使用Profiler的CPU模块，关注"Canvas.BuildBatch"和"Layout.Rebuild"的耗时

2. UI元素池化：
   - 实现方式：为列表项创建对象池，而非即时创建销毁
   - 代码示例：[提供具体实现代码]
   - 性能提升：大型列表（100+项）滚动时，可减少90%的GC Alloc和提高50%的滚动流畅度
   - 基准测试：使用Unity Memory Profiler测量前后内存分配差异

3. 避免UI元素重叠透明度：
   - 检测方法：使用Frame Debugger查看Overdraw
   - 优化技术：合并图层，避免透明UI层叠
   - 预期改进：在移动平台上可提升15-25%的UI渲染性能

可以使用Unity Profiler的CPU Usage和Memory模块验证这些优化效果。特别关注GC Alloc、Batches数量以及Canvas.BuildBatch的调用频率。
```

## 示例三：架构模式反馈

### 情境

AI 提供了一个简单的单例实现，但没有考虑 Unity 生命周期：

```csharp
public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance => _instance ?? (_instance = new GameManager());

    private GameManager() { }

    public void Initialize() { /* 初始化逻辑 */ }
}
```

### 简短反馈方式

```
反馈:#架构模式# - 单例模式实现没有考虑Unity生命周期和场景切换问题
```

### 详细反馈记录

```
## 2025-03-15: Unity单例模式实现不完善
- 反馈类别: #架构模式#
- 问题描述: 提供的单例模式是纯C#实现，没有考虑Unity的MonoBehaviour生命周期和场景切换
- 期望改进:
  1. 提供基于MonoBehaviour的单例模式实现
  2. 处理场景切换时的单例持久化
  3. 考虑线程安全问题
  4. 提供单例的不同变体及适用场景分析
- 参考资料: Unity官方手册中关于DontDestroyOnLoad的说明
```

### 规则改进效果

改进后，AI 提供更完善的 Unity 单例实现：

```csharp
// 基础MonoBehaviour单例模板
public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T _instance;

    // 线程安全的单例访问
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 查找场景中已存在的实例
                _instance = FindObjectOfType<T>();

                // 如果场景中不存在，则创建
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                    _instance.Initialize();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else if (_instance != this)
        {
            // 已存在实例，销毁当前对象
            Destroy(gameObject);
        }
    }

    // 子类可重写的初始化方法
    protected virtual void Initialize() { }
}

// 使用示例
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private GameConfig _config;

    public GameState CurrentState { get; private set; }

    protected override void Initialize()
    {
        CurrentState = GameState.MainMenu;
        Debug.Log("GameManager initialized!");
    }

    public void StartGame()
    {
        // 游戏启动逻辑
    }
}
```

同时提供了场景特定单例的变体和使用场景分析：

```csharp
// 场景特定单例（不使用DontDestroyOnLoad）
public abstract class SceneSingletonBehaviour<T> : MonoBehaviour where T : SceneSingletonBehaviour<T>
{
    // 实现代码...
}

/*
单例变体选择指南：
1. 全局持久单例(SingletonBehaviour<T>)：适用于GameManager、AudioManager等贯穿游戏全程的系统
2. 场景特定单例(SceneSingletonBehaviour<T>)：适用于LevelManager等只在特定场景存在的管理器
3. 纯数据单例(ScriptableObject)：适用于配置数据、游戏设置等不需要MonoBehaviour的情况
*/
```

## 如何跟踪反馈效果

在`.cursor/feedback/ai_feedback.md`文件的"反馈效果追踪"表格中记录改进效果：

```
| 日期       | 改进的方面 | 改进前问题                       | 改进后效果                                   |
|------------|------------|----------------------------------|----------------------------------------------|
| 2025-03-10 | API设计    | 资源加载API参数过多              | API更简洁，提供多个便捷重载方法             |
| 2025-03-12 | 性能优化   | 性能建议泛泛，缺乏具体性         | 提供了具体场景优化方法和可测量的性能指标    |
| 2025-03-15 | 架构模式   | 单例模式未考虑Unity生命周期      | 提供了完善的MonoBehaviour单例模板及使用指南 |
```

## 如何使用反馈改进规则

基于收集的反馈，可以在`.cursorrules`文件中添加更具体的规则。例如：

```
API设计准则:
- Unity风格API应保持简洁，公共方法参数不超过3个
- 复杂参数应封装为配置对象或使用构建器模式
- 提供多个重载方法满足不同使用场景
- 异步方法应返回Task或使用回调，并提供取消选项

性能优化准则:
- 性能优化建议必须包含具体的测量方法和预期改进数值
- 针对不同Unity子系统(UI、物理、渲染等)提供专门的优化技术
- 包含Profiler截图或性能对比数据支持优化建议
- 优先推荐已在大型项目中验证过的优化技术

架构模式准则:
- 设计模式实现必须考虑Unity特有的生命周期和场景流程
- 提供多种实现变体及其适用场景分析
- 包含完整的代码示例，包括错误处理和边缘情况
- 考虑序列化、编辑器支持和多线程安全性
```

使用这种方式，您可以不断收集反馈并改进 AI 规则，使 AI 助手越来越符合您的具体项目需求和开发风格。
