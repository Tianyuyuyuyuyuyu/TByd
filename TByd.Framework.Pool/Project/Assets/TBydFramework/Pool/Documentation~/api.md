# TBydFramework Pool API 文档

> 当前版本: 0.2.0

## 核心类型

### ObjectPool<T>
基础泛型对象池类，用于管理任意类型的对象。

#### 构造函数 
```csharp
public ObjectPool<T>(
    Func<T> createFunc,
    Action<T> actionOnGet = null,
    Action<T> actionOnReturn = null,
    Action<T> actionOnDestroy = null,
    int defaultCapacity = 10,
    int maxSize = 10000
)
```

#### 主要属性
- `Count`: 当前池中可用对象的数量
- `MaxSize`: 池的最大容量

#### 主要方法
- `Get()`: 从池中获取一个对象
- `Return(T item)`: 将对象返回池中
- `Prewarm(int count)`: 预热池，创建指定数量的对象
- `Clear()`: 清空池中的所有对象

### GameObjectPool
专门用于管理Unity GameObject的对象池。

#### 构造函数
```csharp
public GameObjectPool(
    GameObject prefab,
    Transform parent = null,
    int defaultCapacity = 10,
    int maxSize = 10000
)
```

#### 主要方法
- `Get()`: 获取一个GameObject实例
- `Return(GameObject instance)`: 返回GameObject实例到池中
- `Prewarm(int count)`: 预热池
- `Clear()`: 清理池

### AddressableGameObjectPool
用于管理Addressable资源的对象池。

#### 构造函数
```csharp
public AddressableGameObjectPool(
    AssetReferenceGameObject prefabReference,
    Transform parent = null,
    int defaultCapacity = 10,
    int maxSize = 10000
)
```

#### 异步方法
- `PrewarmAsync(int count)`: 异步预热池
- `Rent()`: 获取一个实例
- `Return(GameObject instance)`: 返回实例到池中

## 使用示例

### 基础对象池
```csharp
// 创建一个简单的对象池
var pool = new ObjectPool<MyClass>(
    createFunc: () => new MyClass(),
    actionOnGet: item => item.Initialize(),
    actionOnReturn: item => item.Reset()
);
// 使用池
var obj = pool.Get();
// ... 使用对象
pool.Return(obj);
```

### GameObject池
```csharp
// 创建GameObject池
var pool = new GameObjectPool(prefab);
// 使用池
var instance = pool.Get();
// ... 使用GameObject
pool.Return(instance);
```

### Addressable池
```csharp
// 创建Addressable池
var pool = new AddressableGameObjectPool(prefabReference);
// 预热池
await pool.PrewarmAsync(5);
// 使用池
var instance = pool.Rent();
// ... 使用GameObject
pool.Return(instance);
```

## 最佳实践

1. **对象生命周期管理**
   - 始终在不需要对象时调用Return。
   - 使用using语句或try-finally确保对象返回。
   - 在场景切换时清理池。

2. **性能优化**
   - 适当预热池以避免运行时分配。
   - 设置合理的maxSize以防止内存泄漏。
   - 使用对象池管理频繁创建销毁的对象。

3. **线程安全**
   - `ObjectPool<T>`是线程安全的。
   - `GameObjectPool`应在主线程使用。
   - 共享池访问需要适当的同步。

## 注意事项

1. **GameObject池**
   - 返回对象时会自动设置`SetActive(false)`。
   - 获取对象时会自动设置`SetActive(true)`。
   - 父级`Transform`可选，但建议设置以便组织层级。

2. **Addressable池**
   - 需要Unity Addressable系统支持。
   - 异步操作需要正确处理。
   - 记得在适当时机调用`Dispose()`。

3. **通用建议**
   - 避免在池对象中保存池的引用。
   - 注意清理池对象的状态。
   - 合理设置池的容量。