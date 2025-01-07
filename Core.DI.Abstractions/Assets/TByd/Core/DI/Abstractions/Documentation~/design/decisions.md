# 设计决策

本文档记录了Core.DI.Abstractions包在开发过程中的关键设计决策。

## 1. 接口设计决策

### 1.1 分离容器接口

**决策**: 将容器接口分为 `IContainer` 和 `IContainerBuilder`

**原因**:
- 遵循单一职责原则
- 分离构建期和运行期的职责
- 提供更清晰的API界面

**替代方案**:
- 使用单一接口
- 使用静态工厂方法

### 1.2 生命周期接口设计

**决策**: 创建独立的生命周期接口族

```csharp
public interface ILifetime
public interface IInitializable
public interface IAsyncInitializable
public interface IStartable
public interface IAsyncStartable
```

**原因**:
- 支持不同的初始化需求
- 允许异步操作
- 便于扩展新的生命周期行为

**影响**:
- 增加了接口数量
- 需要更多的文档说明
- 提供了更大的灵活性

## 2. 特性设计决策

### 2.1 特性命名

**决策**: 使用简洁的特性名称

```csharp
[Inject]  // 而不是 [DependencyInjection]
[Named]   // 而不是 [DependencyName]
```

**原因**:
- 提高代码可读性
- 减少样板代码
- 符合行业惯例

### 2.2 特性组合

**决策**: 允许特性组合使用

```csharp
[Inject, Optional]
[Inject, Named("logger")]
```

**原因**:
- 提供更灵活的注入控制
- 支持复杂的注入场景
- 保持API的简洁性

## 3. 生命周期管理决策

### 3.1 作用域设计

**决策**: 使用显式作用域管理

```csharp
using (var scope = container.CreateScope())
{
    var service = scope.Resolve<IService>();
}
```

**原因**:
- 明确的资源管理
- 避免内存泄漏
- 支持嵌套作用域

### 3.2 实例缓存策略

**决策**: 使用 `ConditionalWeakTable` 进行实例缓存

**原因**:
- 避免内存泄漏
- 支持垃圾回收
- 提供线程安全性

## 4. 性能优化决策

### 4.1 反射缓存

**决策**: 缓存反射结果

```csharp
private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache;
```

**原因**:
- 提高性能
- 减少反射开销
- 支持AOT场景

### 4.2 表达式树优化

**决策**: 使用表达式树生成委托

**原因**:
- 提供接近原生的性能
- 支持运行时代码生成
- 便于调试和维护

## 5. API设计决策

### 5.1 泛型API

**决策**: 优先使用泛型API

```csharp
T Resolve<T>() 而不是 object Resolve(Type type)
```

**原因**:
- 类型安全
- 更好的开发体验
- 减少类型转换

### 5.2 异常处理

**决策**: 提供Try方法变体

```csharp
bool TryResolve<T>(out T instance)
```

**原因**:
- 支持优雅降级
- 避免异常开销
- 提供更好的控制流

## 6. 扩展性决策

### 6.1 插件架构

**决策**: 采用基于接口的插件架构

**原因**:
- 支持自定义扩展
- 保持核心简洁
- 便于维护

### 6.2 配置选项

**决策**: 使用选项模式

```csharp
public class ContainerOptions
{
    public bool EnableDiagnostics { get; set; }
    public bool ValidateScopes { get; set; }
}
```

**原因**:
- 提供灵活的配置
- 支持运行时调整
- 便于测试

## 7. 兼容性决策

### 7.1 Unity版本支持

**决策**: 支持Unity 2021.3+

**原因**:
- 利用新特性
- 保持长期支持
- 减少维护负担

### 7.2 框架兼容性

**决策**: 提供框架无关的抽象

**原因**:
- 支持多个DI框架
- 避免供应商锁定
- 便于迁移

## 8. 未来考虑

### 8.1 待定决策

- 属性过滤器支持
- 条件注入机制
- 编译时验证

### 8.2 潜在改进

- 性能优化机会
- API简化可能
- 新功能需求 