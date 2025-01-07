# 快速入门

## 安装包

1. 配置私有仓库：
```json
{
  "scopedRegistries": [
    {
      "name": "TByd Registry",
      "url": "http://120.26.201.54:1998",
      "scopes": ["com.tbyd"]
    }
  ]
}
```

2. 添加包依赖：
```json
{
  "dependencies": {
    "com.tbyd.core.di.abstractions": "0.0.2"
  }
}
```

## 基础使用

### 1. 定义服务

```csharp
// 定义接口
public interface IMyService
{
    void DoSomething();
}

// 实现服务
public class MyService : IMyService, IInitializable
{
    public void Initialize()
    {
        Debug.Log("Service initialized!");
    }

    public void DoSomething()
    {
        Debug.Log("Service doing something...");
    }
}
```

### 2. 注册服务

```csharp
public class Installer
{
    public void Install(IContainerBuilder builder)
    {
        // 基础注册
        builder.Register<IMyService, MyService>();
        
        // 单例注册
        builder.Register<MyService>()
            .As<IMyService>()
            .SingleInstance();
            
        // 命名注册
        builder.Register<IMyService, MyService>()
            .Named("special");
    }
}
```

### 3. 使用服务

```csharp
public class MyComponent : MonoBehaviour
{
    // 字段注入
    [Inject] 
    private IMyService _service;
    
    // 命名注入
    [Inject("special")] 
    private IMyService _specialService;
    
    // 可选注入
    [Inject, Optional] 
    private IOptionalService _optionalService;
}
```

## 生命周期管理

### 1. 初始化

```csharp
public class MyInitializableService : IInitializable
{
    public void Initialize()
    {
        // 初始化逻辑
    }
}
```

### 2. 异步初始化

```csharp
public class MyAsyncService : IAsyncInitializable
{
    public async Task InitializeAsync()
    {
        await Task.Delay(100); // 模拟异步操作
    }
}
```

### 3. 资源释放

```csharp
public class MyDisposableService : IDisposable
{
    public void Dispose()
    {
        // 清理资源
    }
}
```

## 框架切换

1. VContainer实现：
```json
{
  "dependencies": {
    "com.tbyd.core.di.vcontainer": "0.0.1"
  }
}
```

2. Zenject实现：
```json
{
  "dependencies": {
    "com.tbyd.core.di.zenject": "0.0.1"
  }
}
```

## 下一步

- 阅读[基本概念](concepts.md)深入理解DI
- 查看[最佳实践](best-practices.md)了解推荐用法
- 参考[API文档](../api/index.md)获取详细信息 