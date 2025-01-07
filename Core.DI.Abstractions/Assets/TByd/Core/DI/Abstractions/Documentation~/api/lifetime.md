# 生命周期接口

## ILifetime

生命周期管理的基础接口。

```csharp
public interface ILifetime
{
    /// <summary>
    /// 获取或创建实例
    /// </summary>
    object GetOrCreateInstance(ILifetimeScope scope, Registration registration);
}
```

## ILifetimeScope

生命周期作用域接口。

```csharp
public interface ILifetimeScope : IDisposable
{
    /// <summary>
    /// 创建子作用域
    /// </summary>
    ILifetimeScope BeginLifetimeScope();
    
    /// <summary>
    /// 解析服务
    /// </summary>
    T Resolve<T>();
}
```

## IInitializable

同步初始化接口。

```csharp
public interface IInitializable
{
    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize();
}
```

## IAsyncInitializable

异步初始化接口。

```csharp
public interface IAsyncInitializable
{
    /// <summary>
    /// 异步初始化
    /// </summary>
    Task InitializeAsync();
}
```

## IStartable

同步启动接口。

```csharp
public interface IStartable
{
    /// <summary>
    /// 启动
    /// </summary>
    void Start();
}
```

## IAsyncStartable

异步启动接口。

```csharp
public interface IAsyncStartable
{
    /// <summary>
    /// 异步启动
    /// </summary>
    Task StartAsync();
}
```

## 生命周期实现

### SingletonLifetime

```csharp
public class SingletonLifetime : ILifetime
{
    private object _instance;
    private readonly object _syncRoot = new object();
    
    public object GetOrCreateInstance(ILifetimeScope scope, Registration registration)
    {
        if (_instance == null)
        {
            lock (_syncRoot)
            {
                if (_instance == null)
                {
                    _instance = registration.CreateInstance(scope);
                }
            }
        }
        return _instance;
    }
}
```

### TransientLifetime

```csharp
public class TransientLifetime : ILifetime
{
    public object GetOrCreateInstance(ILifetimeScope scope, Registration registration)
    {
        return registration.CreateInstance(scope);
    }
}
```

### ScopedLifetime

```csharp
public class ScopedLifetime : ILifetime
{
    private readonly ConditionalWeakTable<ILifetimeScope, object> _instances
        = new ConditionalWeakTable<ILifetimeScope, object>();
    
    public object GetOrCreateInstance(ILifetimeScope scope, Registration registration)
    {
        return _instances.GetOrCreateValue(scope, _ => registration.CreateInstance(scope));
    }
}
```

## 使用示例

### 1. 初始化接口

```csharp
public class DatabaseService : IInitializable
{
    private readonly IDbConnection _connection;
    
    public void Initialize()
    {
        _connection.Open();
        // 执行初始化SQL...
    }
}
```

### 2. 异步初始化

```csharp
public class NetworkService : IAsyncInitializable
{
    private readonly HttpClient _client;
    
    public async Task InitializeAsync()
    {
        await _client.GetAsync("http://api.example.com/init");
    }
}
```

### 3. 启动服务

```csharp
public class BackgroundService : IStartable, IDisposable
{
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    
    public void Start()
    {
        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                await Task.Delay(1000);
                // 执行后台任务...
            }
        });
    }
    
    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
```

### 4. 作用域使用

```csharp
public class RequestScope : IDisposable
{
    private readonly ILifetimeScope _scope;
    
    public RequestScope(IContainer container)
    {
        _scope = container.BeginLifetimeScope();
    }
    
    public T Resolve<T>()
    {
        return _scope.Resolve<T>();
    }
    
    public void Dispose()
    {
        _scope.Dispose();
    }
} 