# TByd Core DI Abstractions 🎯

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.md)
[![Unity](https://img.shields.io/badge/Unity-2021.3%2B-blue.svg)](https://unity.com)
[![Version](https://img.shields.io/badge/Version-0.0.2-green.svg)](package.json)

TByd Core DI Abstractions提供了一套框架无关的依赖注入抽象接口，支持多种DI容器的实现。通过统一的抽象层，让您的代码不再与特定DI框架耦合。

## ✨ 特性

- 🔌 框架无关的依赖注入抽象层
- 🔄 完整的生命周期管理
  - 支持同步/异步初始化 (`IInitializable`/`IAsyncInitializable`)
  - 支持同步/异步启动 (`IStartable`/`IAsyncStartable`)
  - 支持资源释放 (`IDisposable`)
- 🎯 灵活的注入选项
  - 支持构造函数注入
  - 支持属性/字段注入 (`[Inject]`)
  - 支持可选注入 (`[Optional]`)
  - 支持命名注入 (`[Named("name")]`)
- ⚡ 强大的容器功能
  - 支持多种生命周期（Transient、Scoped、Singleton）
  - 支持链式注册API
  - 支持作用域管理
- 🛠️ 实用工具集
  - 类型反射工具 (`TypeUtility`)
  - 依赖注入工具 (`DIUtility`)

## 📦 安装

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
    "com.tbyd.core.di.abstractions": "0.0.2"
  }
}
```

### 通过Unity Package Manager

1. 确保已配置私有仓库
2. 打开Package Manager (Window > Package Manager)
3. 点击"+"按钮
4. 选择"Add package by name"
5. 输入：`com.tbyd.core.di.abstractions`
6. 选择版本：`0.0.2`

## 🚀 快速开始

### 1. 定义服务接口和实现

```csharp
public interface IMyService
{
    void DoSomething();
}

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
        
        // 使用链式API配置
        builder.Register<MyService>()
            .As<IMyService>()
            .SingleInstance()
            .SetLifetime(new CustomLifetime());
            
        // 命名注册
        builder.Register<IMyService, MyService>()
            .Named("special");
    }
}
```

### 3. 使用依赖注入

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
    
    // 构造函数注入
    public MyComponent(
        [Inject] IMyService service,
        [Inject("special")] IMyService specialService)
    {
        _service = service;
        _specialService = specialService;
    }
}
```

## 🔄 框架切换

本包设计为框架无关的抽象层，您可以轻松在不同的DI框架间切换：

1. VContainer实现：
```
com.tbyd.core.di.vcontainer
```

2. Zenject实现：
```
com.tbyd.core.di.zenject
```

切换框架时，只需要：
1. 更新包依赖
2. 更新容器初始化代码
3. 业务代码无需任何修改

## 📚 API文档

### 核心接口

- `IContainer` - 容器核心接口
- `IContainerBuilder` - 容器构建接口
- `IRegistration` - 注册信息接口
- `IRegistrationBuilder` - 注册构建器接口

### 生命周期接口

- `IInitializable` - 同步初始化
- `IAsyncInitializable` - 异步初始化
- `IStartable` - 同步启动
- `IAsyncStartable` - 异步启动
- `IDisposable` - 资源释放

### 特性

- `[Inject]` - 注入标记
- `[Optional]` - 可选注入
- `[Named]` - 命名注入

### 工具类

- `DIUtility` - 依赖注入工具
- `TypeUtility` - 类型工具

## 🤝 贡献

欢迎提交Issue和Pull Request！

## 📄 许可证

本项目基于MIT许可证。详见[LICENSE](LICENSE.md)文件。 