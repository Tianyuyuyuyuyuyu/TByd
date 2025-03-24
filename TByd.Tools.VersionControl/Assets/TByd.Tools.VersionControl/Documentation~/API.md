# TByd.Template API 参考

本文档提供了TByd.Template包中所有公共API的详细说明。

## 目录

- [命名空间](#命名空间)
- [类型](#类型)
  - [TemplateManager](#templatemanager)
  - [TemplateConfig](#templateconfig)
  - [TemplateLogger](#templatelogger)
  - [TemplateProcessor](#templateprocessor)
  - [TemplateRenderer](#templaterenderer)
  - [TemplateCache](#templatecache)
- [接口](#接口)
  - [ITemplateProcessor](#itemplateprocessor)
  - [ITemplateRenderer](#itemplaterenderer)
  - [ITemplateCache](#itemplatecache)
- [枚举](#枚举)
  - [LogLevel](#loglevel)
  - [TemplateMode](#templatemode)
- [扩展方法](#扩展方法)
  - [StringExtensions](#stringextensions)
  - [GameObjectExtensions](#gameobjectextensions)
- [编辑器工具](#编辑器工具)
  - [TemplatePackageWizard](#templatepackagewizard)
  - [TemplateDebugger](#templatedebugger)

## 命名空间

### TByd.Template

主命名空间，包含所有运行时类型。

```csharp
using TByd.Template;
```

### TByd.Template.Utilities

包含工具类和扩展方法。

```csharp
using TByd.Template.Utilities;
```

### TByd.Template.Editor

包含编辑器相关的类型和工具。

```csharp
using TByd.Template.Editor;
```

### TByd.Template.Tests

包含测试相关的类型。

```csharp
using TByd.Template.Tests;
```

## 类型

### TemplateManager

`TemplateManager` 是包的主要入口点，提供对所有功能的访问。

```csharp
public class TemplateManager : MonoBehaviour
```

#### 属性

| 名称 | 类型 | 描述 |
|------|------|------|
| `Instance` | `TemplateManager` | 获取单例实例 |
| `Config` | `TemplateConfig` | 获取当前配置 |
| `Version` | `string` | 获取当前版本号 |
| `IsInitialized` | `bool` | 检查是否已初始化 |
| `Processor` | `ITemplateProcessor` | 获取或设置模板处理器 |
| `Renderer` | `ITemplateRenderer` | 获取或设置模板渲染器 |
| `Cache` | `ITemplateCache` | 获取或设置模板缓存 |

#### 事件

| 名称 | 类型 | 描述 |
|------|------|------|
| `OnInitialized` | `Action` | 初始化完成时触发 |
| `OnProcessCompleted` | `Action<string>` | 处理完成时触发 |
| `OnError` | `Action<Exception>` | 发生错误时触发 |

#### 方法

| 名称 | 返回类型 | 描述 |
|------|----------|------|
| `Initialize(TemplateConfig)` | `UniTask` | 异步初始化管理器 |
| `ProcessTemplate(string)` | `string` | 处理模板操作示例 |
| `ProcessTemplateAsync(string)` | `UniTask<string>` | 异步处理模板操作示例 |
| `RegisterProcessor(ITemplateProcessor)` | `void` | 注册自定义处理器 |
| `RegisterRenderer(ITemplateRenderer)` | `void` | 注册自定义渲染器 |
| `RegisterCache(ITemplateCache)` | `void` | 注册自定义缓存 |
| `ClearCache()` | `void` | 清除缓存 |
| `Dispose()` | `void` | 释放资源 |

#### 示例

```csharp
// 获取实例
var manager = TemplateManager.Instance;

// 初始化
await manager.Initialize();

// 使用功能
string result = manager.ProcessTemplate("输入数据");

// 异步处理
string asyncResult = await manager.ProcessTemplateAsync("异步输入数据");

// 注册自定义处理器
manager.RegisterProcessor(new MyCustomProcessor());

// 订阅事件
manager.OnProcessCompleted += (result) => {
    Debug.Log($"处理完成: {result}");
};
```

### TemplateConfig

`TemplateConfig` 包含管理器的配置信息。

```csharp
[Serializable]
public class TemplateConfig
```

#### 属性

| 名称 | 类型 | 描述 |
|------|------|------|
| `EnableLogging` | `bool` | 是否启用日志 |
| `LogLevel` | `LogLevel` | 日志级别 |
| `AutoInitialize` | `bool` | 是否自动初始化 |
| `CacheSettings` | `CacheSettings` | 缓存设置 |
| `TemplateMode` | `TemplateMode` | 模板模式 |
| `MaxRetryCount` | `int` | 最大重试次数 |
| `TimeoutSeconds` | `float` | 超时时间（秒） |

#### 构造函数

```csharp
// 默认构造函数
public TemplateConfig();

// 自定义设置构造函数
public TemplateConfig(bool enableLogging, LogLevel logLevel, bool autoInitialize);

// 完整设置构造函数
public TemplateConfig(bool enableLogging, LogLevel logLevel, bool autoInitialize, 
                     TemplateMode mode, int maxRetryCount, float timeoutSeconds);
```

#### 示例

```csharp
// 创建默认配置
var defaultConfig = new TemplateConfig();

// 创建自定义配置
var customConfig = new TemplateConfig
{
    EnableLogging = true,
    LogLevel = LogLevel.Info,
    AutoInitialize = false,
    TemplateMode = TemplateMode.Advanced,
    MaxRetryCount = 3,
    TimeoutSeconds = 30f
};

// 使用配置初始化管理器
await TemplateManager.Instance.Initialize(customConfig);
```

### TemplateLogger

`TemplateLogger` 提供统一的日志功能。

```csharp
public static class TemplateLogger
```

#### 方法

| 名称 | 返回类型 | 描述 |
|------|----------|------|
| `Configure(bool, LogLevel)` | `void` | 配置日志系统 |
| `Verbose(string)` | `void` | 记录详细日志 |
| `LogDebug(string)` | `void` | 记录调试日志 |
| `Info(string)` | `void` | 记录信息日志 |
| `Warning(string)` | `void` | 记录警告日志 |
| `Error(string)` | `void` | 记录错误日志 |
| `Exception(string, Exception)` | `void` | 记录异常日志 |
| `SetLogHandler(Action<string, LogType>)` | `void` | 设置自定义日志处理器 |

#### 示例

```csharp
// 配置日志
TemplateLogger.Configure(true, LogLevel.Info);

// 使用日志
TemplateLogger.Info("这是一条信息日志");
TemplateLogger.Warning("这是一条警告日志");
TemplateLogger.Error("这是一条错误日志");

// 记录异常
try
{
    // 可能抛出异常的代码
}
catch (Exception ex)
{
    TemplateLogger.Exception("操作失败", ex);
}

// 设置自定义日志处理器
TemplateLogger.SetLogHandler((message, logType) => {
    // 自定义日志处理逻辑
    Console.WriteLine($"[{logType}] {message}");
});
```

### TemplateProcessor

`TemplateProcessor` 负责处理模板数据。

```csharp
public class TemplateProcessor : ITemplateProcessor
```

#### 方法

| 名称 | 返回类型 | 描述 |
|------|----------|------|
| `Process(string)` | `string` | 处理模板数据 |
| `ProcessAsync(string)` | `UniTask<string>` | 异步处理模板数据 |
| `ValidateInput(string)` | `bool` | 验证输入数据 |
| `SetOptions(Dictionary<string, object>)` | `void` | 设置处理选项 |

#### 示例

```csharp
// 创建处理器
var processor = new TemplateProcessor();

// 设置选项
processor.SetOptions(new Dictionary<string, object>
{
    { "trimWhitespace", true },
    { "caseSensitive", false }
});

// 处理数据
string result = processor.Process("输入数据");

// 异步处理
string asyncResult = await processor.ProcessAsync("异步输入数据");
```

### TemplateRenderer

`TemplateRenderer` 负责渲染模板。

```csharp
public class TemplateRenderer : ITemplateRenderer
```

#### 方法

| 名称 | 返回类型 | 描述 |
|------|----------|------|
| `Render(string)` | `GameObject` | 渲染模板为GameObject |
| `RenderAsync(string)` | `UniTask<GameObject>` | 异步渲染模板 |
| `RenderToTexture(string)` | `Texture2D` | 渲染模板为纹理 |
| `RenderToUI(string, RectTransform)` | `void` | 渲染模板到UI元素 |

#### 示例

```csharp
// 创建渲染器
var renderer = new TemplateRenderer();

// 渲染为GameObject
GameObject obj = renderer.Render("<template-data>");

// 异步渲染
GameObject asyncObj = await renderer.RenderAsync("<template-data>");

// 渲染为纹理
Texture2D texture = renderer.RenderToTexture("<template-data>");

// 渲染到UI
RectTransform container = GetComponent<RectTransform>();
renderer.RenderToUI("<template-data>", container);
```

### TemplateCache

`TemplateCache` 提供缓存功能。

```csharp
public class TemplateCache : ITemplateCache
```

#### 方法

| 名称 | 返回类型 | 描述 |
|------|----------|------|
| `Get<T>(string)` | `T` | 获取缓存项 |
| `Set<T>(string, T)` | `void` | 设置缓存项 |
| `Remove(string)` | `bool` | 移除缓存项 |
| `Clear()` | `void` | 清除所有缓存 |
| `Contains(string)` | `bool` | 检查缓存项是否存在 |

#### 示例

```csharp
// 创建缓存
var cache = new TemplateCache();

// 设置缓存
cache.Set("key1", "value1");
cache.Set("key2", new Vector3(1, 2, 3));

// 获取缓存
string value1 = cache.Get<string>("key1");
Vector3 value2 = cache.Get<Vector3>("key2");

// 检查缓存
bool hasKey = cache.Contains("key1");

// 移除缓存
bool removed = cache.Remove("key1");

// 清除所有缓存
cache.Clear();
```

## 接口

### ITemplateProcessor

```csharp
public interface ITemplateProcessor
{
    string Process(string input);
    UniTask<string> ProcessAsync(string input);
    bool ValidateInput(string input);
    void SetOptions(Dictionary<string, object> options);
}
```

### ITemplateRenderer

```csharp
public interface ITemplateRenderer
{
    GameObject Render(string templateData);
    UniTask<GameObject> RenderAsync(string templateData);
    Texture2D RenderToTexture(string templateData);
    void RenderToUI(string templateData, RectTransform container);
}
```

### ITemplateCache

```csharp
public interface ITemplateCache
{
    T Get<T>(string key);
    void Set<T>(string key, T value);
    bool Remove(string key);
    void Clear();
    bool Contains(string key);
}
```

## 枚举

### LogLevel

```csharp
public enum LogLevel
{
    Verbose = 0,
    Debug = 1,
    Info = 2,
    Warning = 3,
    Error = 4,
    None = 5
}
```

| 值 | 描述 |
|------|------|
| `Verbose` | 详细日志，包含所有信息 |
| `Debug` | 调试信息 |
| `Info` | 一般信息 |
| `Warning` | 警告信息 |
| `Error` | 错误信息 |
| `None` | 关闭所有日志 |

### TemplateMode

```csharp
public enum TemplateMode
{
    Basic = 0,
    Advanced = 1,
    Expert = 2
}
```

| 值 | 描述 |
|------|------|
| `Basic` | 基本模式，适用于简单场景 |
| `Advanced` | 高级模式，提供更多功能 |
| `Expert` | 专家模式，完全自定义 |

## 扩展方法

### StringExtensions

```csharp
public static class StringExtensions
{
    public static string ToTemplateFormat(this string input);
    public static bool IsValidTemplate(this string input);
    public static string ApplyTemplate(this string template, Dictionary<string, string> values);
}
```

#### 示例

```csharp
// 使用扩展方法
string template = "Hello, {name}!";
bool isValid = template.IsValidTemplate(); // 返回 true

Dictionary<string, string> values = new Dictionary<string, string>
{
    { "name", "World" }
};

string result = template.ApplyTemplate(values); // 返回 "Hello, World!"
```

### GameObjectExtensions

```csharp
public static class GameObjectExtensions
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component;
    public static void ApplyTemplate(this GameObject gameObject, string templateData);
}
```

#### 示例

```csharp
// 获取或添加组件
Rigidbody rb = gameObject.GetOrAddComponent<Rigidbody>();

// 应用模板
gameObject.ApplyTemplate("<template-data>");
```

## 编辑器工具

### TemplatePackageWizard

`TemplatePackageWizard` 是一个编辑器窗口，用于创建新的UPM包。

```csharp
public class TemplatePackageWizard : EditorWindow
```

#### 菜单路径

- TByd/Template/创建新的UPM包

#### 功能

- 设置包名、版本、描述等信息
- 配置作者信息
- 选择输出路径
- 创建新的UPM包

#### 示例

```csharp
// 打开向导窗口
TemplatePackageWizard.ShowWindow();

// 或通过菜单打开
// TByd > Template > 创建新的UPM包
```

### TemplateDebugger

`TemplateDebugger` 是一个编辑器窗口，用于调试模板。

```csharp
public class TemplateDebugger : EditorWindow
```

#### 菜单路径

- TByd/Template/模板调试器

#### 功能

- 测试模板处理
- 查看处理结果
- 分析性能数据
- 导出调试报告

#### 示例

```csharp
// 打开调试器窗口
TemplateDebugger.ShowWindow();

// 或通过菜单打开
// TByd > Template > 模板调试器
``` 