# TByd.Template API 参考

本文档提供了TByd.Template包中所有公共API的详细说明。

## 目录

- [命名空间](#命名空间)
- [类型](#类型)
  - [TemplateManager](#templatemanager)
  - [TemplateConfig](#templateconfig)
  - [TemplateLogger](#templatelogger)
- [接口](#接口)
- [枚举](#枚举)
  - [LogLevel](#loglevel)
- [扩展方法](#扩展方法)
- [编辑器工具](#编辑器工具)
  - [TemplatePackageWizard](#templatepackagewizard)

## 命名空间

### TByd.Template

主命名空间，包含所有运行时类型。

### TByd.Template.Utilities

包含工具类和扩展方法。

### TByd.Template.Editor

包含编辑器相关的类型和工具。

### TByd.Template.Tests

包含测试相关的类型。

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

#### 方法

| 名称 | 返回类型 | 描述 |
|------|----------|------|
| `Initialize(TemplateConfig)` | `UniTask` | 异步初始化管理器 |
| `ProcessTemplate(string)` | `string` | 处理模板操作示例 |
| `ProcessTemplateAsync(string)` | `UniTask<string>` | 异步处理模板操作示例 |

#### 示例

```csharp
// 获取实例
var manager = TemplateManager.Instance;

// 初始化
await manager.Initialize();

// 使用功能
string result = manager.ProcessTemplate("输入数据");
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

#### 构造函数

```csharp
// 默认构造函数
public TemplateConfig();

// 自定义设置构造函数
public TemplateConfig(bool enableLogging, LogLevel logLevel, bool autoInitialize);
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

#### 示例

```csharp
// 配置日志
TemplateLogger.Configure(true, LogLevel.Info);

// 使用日志
TemplateLogger.Info("这是一条信息日志");
TemplateLogger.Warning("这是一条警告日志");
```

## 接口

(待实现 - 此模板中暂无接口定义)

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

## 扩展方法

(待实现 - 此模板中暂无扩展方法)

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