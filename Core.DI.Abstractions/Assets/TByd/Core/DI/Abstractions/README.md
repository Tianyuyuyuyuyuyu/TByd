# TByd Core DI Abstractions

TByd Core DI Abstractions提供了一套框架无关的依赖注入抽象接口，支持多种DI容器的实现。

## 特性

- 框架无关的依赖注入抽象层
- 支持多种生命周期（Transient、Scoped、Singleton）
- 支持ID标识的服务注册和解析
- 完整的生命周期作用域管理
- 类型安全的泛型API

## 安装

### 配置私有仓库

1. 打开`Packages/manifest.json`
2. 添加私有仓库的scoped registry配置：

```json
{
  "scopedRegistries": [
    {
      "name": "TByd Registry",
      "url": "http://120.26.201.54:1998",
      "scopes": [
        "com.tbyd"
      ]
    }
  ],
  "dependencies": {
    "com.tbyd.core.di.abstractions": "0.0.1"
  }
}
```

### 通过Unity Package Manager

1. 确保已配置私有仓库
2. 打开Package Manager
3. 点击"+"按钮
4. 选择"Add package by name"
5. 输入：`com.tbyd.core.di.abstractions`
6. 选择版本：`0.0.1`

## 基本使用

### 1. 定义服务接口和实现

```csharp
public interface IMyService
{
    void DoSomething();
}

public class MyService : IMyService
{
    public void DoSomething() { }
}
```

### 2. 注册服务

```csharp
public class Installer
{
    public void Install(IContainerBuilder builder)
    {
        // 注册为瞬态服务
        builder.Register<IMyService, MyService>();
        
        // 注册为单例
        builder.Register<IMyService, MyService>(LifetimeType.Singleton);
        
        // 注册带ID的服务
        builder.Register<IMyService, MyService>(id: "special");
    }
}
```

### 3. 使用依赖注入

```csharp
public class MyComponent
{
    [Inject]
    private IMyService _service;
    
    [Inject("special")]
    private IMyService _specialService;
}
```

## 许可证

本项目基于MIT许可证。详见[LICENSE](LICENSE.md)文件。 