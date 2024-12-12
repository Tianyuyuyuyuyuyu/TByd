# TBydFramework Pool 使用指南

## 简介

TBydFramework Pool 是一个灵活的对象池系统，专为Unity项目设计。它提供了多种对象池实现，可以有效管理各类资源的重用，提高性能并减少内存碎片。

## 特性

- **通用对象池 (ObjectPool<T>)**
- **GameObject专用池 (GameObjectPool)**
- **Addressable资源池 (AddressableGameObjectPool)**
- **线程安全支持**
- **完整的生命周期管理**
- **可配置的池容量**
- **预热功能**

## 安装

### 通过Unity Package Manager安装
```shell
https://your-repository-url.git
```

### 添加必要的程序集引用
- `TBydFramework.Pool.Runtime`
- 可选：`Unity.Addressables` (用于Addressable支持)

## 快速开始

### 1. 基础对象池 
```csharp
// 创建池
var pool = new ObjectPool<MyClass>(
    createFunc: () => new MyClass(),
    defaultCapacity: 10
);
// 使用池
var obj = pool.Get();
// ... 使用对象
pool.Return(obj);
```

### 2. GameObject池
```csharp
// 创建池
var pool = new GameObjectPool(prefab);
// 预热
pool.Prewarm(5);
// 使用池
var instance = pool.Get();
// ... 使用GameObject
pool.Return(instance);
```

### 3. Addressable池
```csharp
// 创建池
var pool = new AddressableGameObjectPool(prefabReference);
// 异步预热
await pool.PrewarmAsync(5);
// 使用池
var instance = pool.Rent();
// ... 使用GameObject
pool.Return(instance);
```

## 高级用法

### 自定义对象行为
```csharp
var pool = new ObjectPool<MyClass>(
    createFunc: () => new MyClass(),
    actionOnGet: item => item.OnSpawn(),
    actionOnReturn: item => item.OnDespawn(),
    actionOnDestroy: item => item.OnDestroy()
);
```

### 共享池实现
```csharp
public class SharedPoolManager
{
    private static GameObjectPool sharedPool;
    private static readonly object lockObj = new object();

    public static GameObjectPool GetPool(GameObject prefab)
    {
        lock (lockObj)
        {
            if (sharedPool == null)
            {
                sharedPool = new GameObjectPool(prefab);
            }
            return sharedPool;
        }
    }
}
```

## 性能优化

1. **预热**
   - 在适当时机预热池。
   - 根据实际需求设置预热数量。

2. **容量控制**
   - 设置合理的默认容量。
   - 限制最大容量防止内存泄漏。

3. **对象重用**
   - 重置对象状态而不是销毁。
   - 使用对象池管理频繁创建的资源。

## 故障排除

### 常见问题

1. **对象未正确返回池**
   - 检查是否所有`Get`都有对应的`Return`。
   - 使用`try-finally`确保返回。

2. **内存占用过高**
   - 检查`maxSize`设置。
   - 监控池的使用情况。
   - 适时清理不需要的池。

3. **GameObject显示异常**
   - 确认`Return`时的状态重置。
   - 检查父级`Transform`设置。

### 调试技巧

1. 启用详细日志
   ```csharp
   #define POOL_VERBOSE_LOGGING
   ```

2. 监控池状态
   ```csharp
   Debug.Log($"Pool status: {pool.Count} available");
   ```

## 最佳实践

1. **对象生命周期**
   - 明确定义获取和返回时的行为。
   - 正确清理对象状态。

2. **内存管理**
   - 及时返回不用的对象。
   - 定期监控池的大小。

3. **场景管理**
   - 场景切换时清理池。
   - 使用`DontDestroyOnLoad`管理持久池。

## 示例

查看 [示例](../Samples~/README.md) 了解更多使用场景和最佳实践。

## 支持

如有问题，请通过以下方式获取支持：
1. 查看示例代码。
2. 阅读API文档。
3. 提交Issue。
4. 联系作者。

## 依赖要求

- Unity 2021.3 或更高版本
- TBydFramework.Pool v0.2.0 或更高版本
- 可选：Unity Addressables 1.19.19 或更高版本