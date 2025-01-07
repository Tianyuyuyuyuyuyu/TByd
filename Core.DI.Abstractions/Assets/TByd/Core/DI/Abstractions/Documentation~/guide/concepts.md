# 基本概念

## 依赖注入

依赖注入（DI）是一种设计模式，它允许我们将对象的创建和依赖关系的处理从对象的行为中分离出来。

### 核心概念

1. 服务（Service）
   - 提供特定功能的类
   - 通常通过接口定义
   - 可以有多个实现

2. 容器（Container）
   - 管理服务的生命周期
   - 处理服务的创建和注入
   - 维护服务的依赖关系

3. 注册（Registration）
   - 告诉容器如何创建服务
   - 定义服务的生命周期
   - 配置服务的依赖关系

## 生命周期

### 1. 作用域

- **Transient（瞬态）**
  - 每次请求创建新实例
  - 不共享实例
  - 适用于无状态服务

- **Singleton（单例）**
  - 整个应用共享一个实例
  - 全局状态
  - 适用于配置服务

- **Scoped（作用域）**
  - 在同一作用域内共享实例
  - 作用域结束时释放
  - 适用于请求作用域服务

### 2. 初始化顺序

1. 构造注入
2. 属性/字段注入
3. `Initialize()`调用
4. `InitializeAsync()`调用
5. `Start()`调用
6. `StartAsync()`调用

### 3. 资源释放

1. `Dispose()`调用
2. 作用域释放
3. 容器释放

## 注入方式

### 1. 构造函数注入

```csharp
public class MyService
{
    private readonly ILogger _logger;
    
    public MyService([Inject] ILogger logger)
    {
        _logger = logger;
    }
}
```

### 2. 属性注入

```csharp
public class MyService
{
    [Inject]
    public ILogger Logger { get; set; }
}
```

### 3. 字段注入

```csharp
public class MyService
{
    [Inject]
    private ILogger _logger;
}
```

## 特性说明

### 1. [Inject]

```csharp
// 基础注入
[Inject]
private IService _service;

// 命名注入
[Inject("special")]
private IService _specialService;
```

### 2. [Optional]

```csharp
// 可选注入
[Inject, Optional]
private IOptionalService _service; // 可以为null
```

### 3. [Named]

```csharp
// 命名注入
[Inject]
[Named("special")]
private IService _service;
```

## 最佳实践

1. 优先使用构造函数注入
2. 接口优于具体类型
3. 避免服务定位器模式
4. 合理使用生命周期
5. 保持服务的单一职责

## 相关资源

- [快速开始](getting-started.md)
- [最佳实践](best-practices.md)
- [API文档](../api/index.md) 