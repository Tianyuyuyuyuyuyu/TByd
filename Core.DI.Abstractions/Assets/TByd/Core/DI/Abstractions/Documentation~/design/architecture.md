# 架构说明

## 系统概述

Core.DI.Abstractions 是一个轻量级的依赖注入抽象层，旨在为Unity项目提供框架无关的DI功能。它的主要目标是：

1. 提供统一的DI抽象接口
2. 支持多种DI框架的实现
3. 保持高性能和低内存占用
4. 提供友好的开发体验

## 核心模块

### 1. 容器模块

容器模块是整个系统的核心，负责：
- 服务注册和解析
- 生命周期管理
- 作用域控制

关键接口：
```csharp
public interface IContainer : IDisposable
{
    T Resolve<T>();
    T Resolve<T>(string name);
    bool TryResolve<T>(out T instance);
    IEnumerable<T> ResolveAll<T>();
}
```

### 2. 生命周期模块

管理服务实例的生命周期，支持：
- 单例生命周期
- 瞬态生命周期
- 作用域生命周期

核心接口：
```csharp
public interface ILifetime
{
    object GetOrCreateInstance(ILifetimeScope scope, Registration registration);
}
```

### 3. 特性模块

提供注入相关的特性支持：
- [Inject] 注入特性
- [Optional] 可选依赖
- [Named] 命名注入

示例：
```csharp
public class UserService
{
    [Inject]
    private readonly IUserRepository _repository;
    
    [Inject("logger")]
    private readonly ILogger _logger;
}
```

## 扩展点

### 1. 容器适配器

实现 `IContainer` 接口以支持不同的DI框架：
```csharp
public class CustomContainer : IContainer
{
    private readonly IThirdPartyContainer _container;
    
    public T Resolve<T>()
    {
        return _container.Get<T>();
    }
}
```

### 2. 生命周期扩展

创建自定义生命周期：
```csharp
public class CustomLifetime : ILifetime
{
    public object GetOrCreateInstance(ILifetimeScope scope, Registration registration)
    {
        // 自定义实例创建逻辑
    }
}
```

### 3. 特性扩展

添加新的注入特性：
```csharp
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
public class CustomInjectAttribute : Attribute
{
    // 自定义注入行为
}
```

## 性能考虑

### 1. 实例缓存

- 使用 `ConditionalWeakTable` 避免内存泄漏
- 实现高效的实例缓存策略
- 支持手动缓存清理

### 2. 反射优化

- 缓存反射结果
- 使用表达式树优化性能
- 支持AOT编译

### 3. 线程安全

- 使用适当的锁机制
- 实现线程安全的单例
- 保护共享资源

## 最佳实践

### 1. 接口设计

- 保持接口简单明确
- 遵循依赖倒置原则
- 支持可测试性

### 2. 生命周期管理

- 正确处理资源释放
- 避免循环依赖
- 合理使用作用域

### 3. 异常处理

- 提供清晰的错误信息
- 支持优雅降级
- 保护应用程序稳定性

## 未来规划

### 1. 功能增强

- [ ] 支持属性过滤器
- [ ] 添加条件注入
- [ ] 实现循环依赖检测

### 2. 性能优化

- [ ] 引入编译时代码生成
- [ ] 优化反射性能
- [ ] 减少内存分配

### 3. 开发体验

- [ ] 提供更多调试工具
- [ ] 改进错误提示
- [ ] 添加性能分析工具 