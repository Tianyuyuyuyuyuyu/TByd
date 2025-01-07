# 性能优化指南

## 性能目标

1. 服务解析性能
   - 首次解析：< 1ms
   - 缓存解析：< 0.1ms
   - 批量解析：每个实例 < 0.05ms

2. 内存占用
   - 基础容器：< 1MB
   - 每个注册：< 1KB
   - 类型缓存：< 5MB

3. 启动时间
   - 容器初始化：< 100ms
   - 服务启动：< 500ms
   - 总体启动：< 1s

## 优化建议

### 1. 代码级优化

```csharp
// 1. 使用类型缓存
private static readonly ConcurrentDictionary<Type, TypeInfo> TypeInfoCache 
    = new ConcurrentDictionary<Type, TypeInfo>();

// 2. 使用对象池
public class ServicePool<T>
{
    private readonly ConcurrentBag<T> _pool;
    private readonly Func<T> _factory;

    public T Get() => _pool.TryTake(out var item) ? item : _factory();
    public void Return(T item) => _pool.Add(item);
}

// 3. 避免装箱
public struct ServiceKey
{
    public Type Type { get; }
    public string Name { get; }
}
```

### 2. 架构级优化

1. 依赖图优化
   - 减少依赖深度
   - 避免循环依赖
   - 合理使用单例

2. 注册优化
   - 使用批量注册
   - 延迟注册
   - 条件注册

3. 生命周期优化
   - 合理使用作用域
   - 资源及时释放
   - 避免内存泄漏

### 3. 运行时优化

1. 启动优化
   - 延迟初始化
   - 异步启动
   - 预热关键路径

2. 解析优化
   - 缓存解析结果
   - 批量解析
   - 表达式树编译

## 性能监控

### 1. 监控指标

- 服务解析时间
- 内存占用
- GC压力
- CPU使用率

### 2. 监控工具

- Unity Profiler
- 自定义计数器
- 日志追踪

## 基准测试

### 1. 解析性能

```csharp
[Benchmark]
public void ResolveService()
{
    var service = _container.Resolve<IMyService>();
}

// 结果
| Method         | Mean     | Error    | StdDev   |
|---------------|----------|----------|----------|
| FirstResolve  | 856.3 μs | 16.95 μs | 23.47 μs |
| CachedResolve | 42.31 μs | 0.843 μs | 1.167 μs |
```

### 2. 内存分析

```
内存快照
- 类型缓存：2.3MB
- 服务实例：1.5MB
- 注册信息：0.8MB
总计：4.6MB
```

## 最佳实践

1. 注册建议
   - 合理使用生命周期
   - 避免过度注册
   - 使用命名注册区分服务

2. 解析建议
   - 缓存常用服务
   - 使用批量解析
   - 避免循环解析

3. 生命周期建议
   - 及时释放资源
   - 使用异步初始化
   - 控制作用域范围 