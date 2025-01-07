# 最佳实践指南

## 设计原则

### 1. 依赖倒置原则（DIP）

✅ 推荐：
```csharp
public interface IUserService
{
    User GetUser(int id);
}

public class UserController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
}
```

❌ 避免：
```csharp
public class UserController
{
    private readonly UserService _userService = new UserService();
}
```

### 2. 单一职责原则（SRP）

✅ 推荐：
```csharp
public interface IUserRepository
{
    User GetUser(int id);
}

public interface IUserValidator
{
    bool ValidateUser(User user);
}
```

❌ 避免：
```csharp
public interface IUserService
{
    User GetUser(int id);
    bool ValidateUser(User user);
    void SendEmail(User user);
}
```

## 注册最佳实践

### 1. 使用模块化注册

✅ 推荐：
```csharp
public class UserModule : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<IUserService, UserService>();
        builder.Register<IUserRepository, UserRepository>();
    }
}
```

### 2. 合理使用生命周期

✅ 推荐：
```csharp
// 无状态服务使用Transient
builder.Register<IValidator, Validator>();

// 配置服务使用Singleton
builder.Register<IConfig, AppConfig>().SingleInstance();

// 请求作用域服务使用Scoped
builder.Register<IUserContext, UserContext>().InstancePerScope();
```

## 注入最佳实践

### 1. 优先使用构造函数注入

✅ 推荐：
```csharp
public class UserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;
    
    public UserService(IUserRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }
}
```

### 2. 合理使用可选注入

✅ 推荐：
```csharp
public class AnalyticsService
{
    [Inject, Optional]
    private IAnalytics _analytics;
    
    public void TrackEvent(string name)
    {
        _analytics?.TrackEvent(name);
    }
}
```

### 3. 避免循环依赖

❌ 避免：
```csharp
public class ServiceA
{
    public ServiceA(ServiceB b) { }
}

public class ServiceB
{
    public ServiceB(ServiceA a) { }
}
```

## 生命周期管理

### 1. 正确实现资源释放

✅ 推荐：
```csharp
public class ResourceService : IDisposable
{
    private bool _disposed;
    private readonly Resource _resource;
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _resource?.Dispose();
            }
            _disposed = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
```

### 2. 异步初始化

✅ 推荐：
```csharp
public class DatabaseService : IAsyncInitializable
{
    private readonly IDbConnection _connection;
    
    public async Task InitializeAsync()
    {
        await _connection.OpenAsync();
    }
}
```

## 性能优化

### 1. 缓存注入实例

✅ 推荐：
```csharp
public class CachedService
{
    private IExpensiveService _service;
    
    [Inject]
    private IExpensiveService Service => 
        _service ??= Container.Resolve<IExpensiveService>();
}
```

### 2. 延迟初始化

✅ 推荐：
```csharp
public class LazyService
{
    private readonly Lazy<IExpensiveService> _service;
    
    public LazyService(Lazy<IExpensiveService> service)
    {
        _service = service;
    }
}
```

## 测试友好性

### 1. 易于模拟的接口

✅ 推荐：
```csharp
public interface ITimeProvider
{
    DateTime Now { get; }
}

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
}
```

### 2. 可测试的组件

✅ 推荐：
```csharp
public class OrderService
{
    private readonly ITimeProvider _timeProvider;
    
    public OrderService(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    
    public bool IsValidOrder(Order order)
    {
        return order.Date > _timeProvider.Now.AddDays(-30);
    }
}
```

## 常见问题解决

### 1. 处理可选依赖

✅ 推荐：
```csharp
public class FeatureService
{
    private readonly IFeatureFlag _featureFlag;
    
    public FeatureService([Optional] IFeatureFlag featureFlag = null)
    {
        _featureFlag = featureFlag ?? new DefaultFeatureFlag();
    }
}
```

### 2. 处理多个实现

✅ 推荐：
```csharp
public class MultiLogger
{
    private readonly IEnumerable<ILogger> _loggers;
    
    public MultiLogger(IEnumerable<ILogger> loggers)
    {
        _loggers = loggers;
    }
    
    public void Log(string message)
    {
        foreach (var logger in _loggers)
        {
            logger.Log(message);
        }
    }
}
``` 