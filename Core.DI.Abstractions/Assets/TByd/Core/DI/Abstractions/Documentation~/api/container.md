# 容器接口

## IContainer

容器的核心接口，负责服务的解析。

```csharp
public interface IContainer : IDisposable
{
    /// <summary>
    /// 解析指定类型的服务
    /// </summary>
    T Resolve<T>();
    
    /// <summary>
    /// 解析指定类型和名称的服务
    /// </summary>
    T Resolve<T>(string name);
    
    /// <summary>
    /// 尝试解析服务
    /// </summary>
    bool TryResolve<T>(out T instance);
    
    /// <summary>
    /// 解析所有指定类型的服务
    /// </summary>
    IEnumerable<T> ResolveAll<T>();
}
```

## IContainerBuilder

用于构建容器的接口。

```csharp
public interface IContainerBuilder
{
    /// <summary>
    /// 注册服务
    /// </summary>
    IRegistrationBuilder<TService> Register<TService>();
    
    /// <summary>
    /// 注册服务实现
    /// </summary>
    IRegistrationBuilder<TService> Register<TService, TImplementation>()
        where TImplementation : TService;
    
    /// <summary>
    /// 注册实例
    /// </summary>
    IRegistrationBuilder<T> RegisterInstance<T>(T instance);
    
    /// <summary>
    /// 构建容器
    /// </summary>
    IContainer Build();
}
```

## IRegistration

服务注册信息接口。

```csharp
public interface IRegistration
{
    /// <summary>
    /// 服务类型
    /// </summary>
    Type ServiceType { get; }
    
    /// <summary>
    /// 实现类型
    /// </summary>
    Type ImplementationType { get; }
    
    /// <summary>
    /// 生命周期
    /// </summary>
    ILifetime Lifetime { get; }
    
    /// <summary>
    /// 注册名称
    /// </summary>
    string Name { get; }
}
```

## IRegistrationBuilder

注册构建器接口。

```csharp
public interface IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
{
    /// <summary>
    /// 设置生命周期
    /// </summary>
    IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
        SetLifetime(ILifetime lifetime);
    
    /// <summary>
    /// 注册为单例
    /// </summary>
    IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
        SingleInstance();
    
    /// <summary>
    /// 设置服务名称
    /// </summary>
    IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
        Named(string name);
    
    /// <summary>
    /// 注册服务接口
    /// </summary>
    IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
        As<TService>() where TService : class;
}
```

## 使用示例

### 基本注册和解析

```csharp
// 创建构建器
var builder = new ContainerBuilder();

// 注册服务
builder.Register<IMyService, MyService>();

// 构建容器
var container = builder.Build();

// 解析服务
var service = container.Resolve<IMyService>();
```

### 高级注册

```csharp
// 单例注册
builder.Register<IConfig>()
    .As<IConfig>()
    .SingleInstance();

// 命名注册
builder.Register<IService>()
    .Named("special")
    .As<IService>();

// 实例注册
var instance = new MyService();
builder.RegisterInstance(instance)
    .As<IMyService>();
```

### 批量解析

```csharp
// 注册多个实现
builder.Register<ILogger, ConsoleLogger>();
builder.Register<ILogger, FileLogger>();

// 解析所有实现
var loggers = container.ResolveAll<ILogger>();
```

### 作用域使用

```csharp
using (var scope = container.CreateScope())
{
    var scopedService = scope.Resolve<IScopedService>();
    // 使用服务...
} // 作用域结束，服务被释放
``` 