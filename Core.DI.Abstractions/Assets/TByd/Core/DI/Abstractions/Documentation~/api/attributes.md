# 特性

## InjectAttribute

用于标记需要注入的依赖。

```csharp
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, 
    AllowMultiple = false, Inherited = true)]
public class InjectAttribute : Attribute
{
    /// <summary>
    /// 注入名称
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// 创建InjectAttribute的新实例
    /// </summary>
    public InjectAttribute() { }
    
    /// <summary>
    /// 使用指定名称创建InjectAttribute的新实例
    /// </summary>
    public InjectAttribute(string name)
    {
        Name = name;
    }
}
```

## OptionalAttribute

标记一个依赖为可选的。

```csharp
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, 
    AllowMultiple = false, Inherited = true)]
public class OptionalAttribute : Attribute
{
}
```

## NamedAttribute

用于命名注入。

```csharp
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, 
    AllowMultiple = false, Inherited = true)]
public class NamedAttribute : Attribute
{
    /// <summary>
    /// 注入名称
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// 使用指定名称创建NamedAttribute的新实例
    /// </summary>
    public NamedAttribute(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("注入名称不能为空", nameof(name));
            
        Name = name;
    }
}
```

## 使用示例

### 1. 基础注入

```csharp
public class UserService
{
    // 字段注入
    [Inject]
    private readonly IUserRepository _repository;
    
    // 属性注入
    [Inject]
    public ILogger Logger { get; set; }
    
    // 构造函数注入
    public UserService([Inject] IUserValidator validator)
    {
        // ...
    }
}
```

### 2. 命名注入

```csharp
public class ConfigService
{
    // 使用Inject特性的名称参数
    [Inject("development")]
    private IConfig _devConfig;
    
    // 使用Named特性
    [Inject]
    [Named("production")]
    private IConfig _prodConfig;
    
    // 构造函数命名注入
    public ConfigService(
        [Inject("test")] IConfig testConfig)
    {
        // ...
    }
}
```

### 3. 可选注入

```csharp
public class AnalyticsService
{
    // 可选字段注入
    [Inject, Optional]
    private IAnalytics _analytics;
    
    // 可选属性注入
    [Inject]
    [Optional]
    public ILogger Logger { get; set; }
    
    // 可选构造函数参数
    public AnalyticsService(
        [Inject, Optional] IMetrics metrics = null)
    {
        // ...
    }
    
    public void TrackEvent(string name)
    {
        // 安全使用可选依赖
        _analytics?.TrackEvent(name);
        Logger?.Log($"Tracked event: {name}");
    }
}
```

### 4. 组合使用

```csharp
public class ComplexService
{
    // 必需的命名注入
    [Inject("primary")]
    private readonly IDatabase _primaryDb;
    
    // 可选的命名注入
    [Inject("backup")]
    [Optional]
    private readonly IDatabase _backupDb;
    
    public async Task SaveData(string data)
    {
        // 保存到主数据库
        await _primaryDb.SaveAsync(data);
        
        // 如果有备份数据库，也保存一份
        if (_backupDb != null)
        {
            await _backupDb.SaveAsync(data);
        }
    }
}
```

### 5. 集合注入

```csharp
public class LoggingService
{
    // 注入所有日志记录器
    [Inject]
    private readonly IEnumerable<ILogger> _loggers;
    
    public void Log(string message)
    {
        foreach (var logger in _loggers)
        {
            logger.Log(message);
        }
    }
}
```

### 6. 泛型服务注入

```csharp
public class Repository<T>
{
    // 注入泛型服务
    [Inject]
    private readonly IValidator<T> _validator;
    
    // 可选的泛型缓存
    [Inject, Optional]
    private readonly ICache<T> _cache;
    
    public async Task<T> GetById(int id)
    {
        // 尝试从缓存获取
        if (_cache != null)
        {
            var cached = await _cache.GetAsync(id);
            if (cached != null) return cached;
        }
        
        // 获取并验证
        var item = await LoadFromDb(id);
        await _validator.ValidateAsync(item);
        
        // 更新缓存
        _cache?.SetAsync(id, item);
        
        return item;
    }
} 