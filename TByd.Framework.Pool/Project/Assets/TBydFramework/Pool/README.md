# TBydFramework Pool

![Version](https://img.shields.io/badge/version-0.2.0-blue.svg)

一个高性能、易用的Unity对象池系统。

## 特性

- ✨ **多种池类型**
  - 通用对象池 (`ObjectPool<T>`)
  - GameObject池 (`GameObjectPool`)
  - Addressable资源池 (`AddressableGameObjectPool`)

- 🚀 **高性能**
  - 线程安全实现
  - 预热支持
  - 可配置的容量控制

- 🎯 **易用性**
  - 简洁的API设计
  - 完整的生命周期管理
  - 丰富的示例代码

- 📦 **可扩展**
  - 自定义对象行为
  - 灵活的池配置
  - 支持共享池模式

## 安装

### 通过Unity Package Manager

1. 打开 Package Manager
2. 点击 "+" 按钮
3. 选择 "Add package from git URL"
4. 输入: `https://your-repository-url.git`

### 手动安装

下载并将文件夹放入你的Unity项目的 `Assets` 目录。

## 快速开始

### 基础对象池

```csharp
// 创建池
var pool = new ObjectPool<MyClass>(
    createFunc: () => new MyClass()
);

// 使用对象
var obj = pool.Get();
// ... 使用对象
pool.Return(obj);
```

### GameObject池

```csharp
// 创建池
var pool = new GameObjectPool(prefab);

// 预热
pool.Prewarm(5);

// 使用对象
var instance = pool.Get();
// ... 使用GameObject
pool.Return(instance);
```

更多示例请查看 [示例文档](Samples~/README.md)。

## 文档

- [使用指南](Documentation~/manual.md)
- [API文档](Documentation~/api.md)
- [更新日志](CHANGELOG.md)

## 依赖

- Unity 2021.3 或更高版本
- 可选：Addressables package (用于AddressableGameObjectPool)

## 贡献

欢迎贡献代码！请查看 [贡献指南](CONTRIBUTING.md)。

## 许可

本项目采用 MIT 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情。

## 支持

如果你遇到问题或有建议：

1. 查看 [文档](Documentation~/manual.md)
2. 查看 [示例](Samples~/README.md)
3. 提交 [Issue](https://github.com/your-repo/issues)
4. 联系作者