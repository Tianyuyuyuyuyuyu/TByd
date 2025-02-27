# TByd Template API 参考

## 概述
此文档提供了TByd Template的API参考，包括可用的类、方法和属性。

## 命名空间
```csharp
using Company.Package;
```

## 核心类

### MyManager
主要管理器类，负责核心功能的协调。

```csharp
public class MyManager : MonoBehaviour
{
    // 属性
    public static MyManager Instance { get; private set; }
    
    // 方法
    public void Initialize();
    public void DoSomething();
}
```

### MyUtility
实用工具类，提供常用功能。

```csharp
public static class MyUtility
{
    public static void HelperMethod();
    public static T GetComponent<T>();
}
```

## 接口

### IMyInterface
自定义功能接口。

```csharp
public interface IMyInterface
{
    void OnProcess();
    bool IsValid { get; }
}
```

## 枚举

### MyState
状态枚举。

```csharp
public enum MyState
{
    None,
    Loading,
    Ready,
    Error
}
```

## 事件

### MyEventArgs
自定义事件参数。

```csharp
public class MyEventArgs : EventArgs
{
    public string Message { get; set; }
    public int Value { get; set; }
}
```

## 使用示例

### 基本用法
```csharp
// 初始化
MyManager.Instance.Initialize();

// 使用工具类
MyUtility.HelperMethod();

// 实现接口
public class MyClass : IMyInterface
{
    public void OnProcess()
    {
        // 实现逻辑
    }
    
    public bool IsValid => true;
}
```

### 事件处理
```csharp
MyManager.Instance.OnSomethingHappened += (sender, args) =>
{
    Debug.Log($"收到事件: {args.Message}");
};
```

## 注意事项
1. 确保在使用前正确初始化
2. 注意性能开销
3. 正确处理事件订阅和取消订阅

## 性能考虑
- 缓存组件引用
- 避免频繁的GC
- 使用对象池管理频繁创建销毁的对象

## 线程安全
说明哪些API是线程安全的，哪些需要在主线程调用。

## 错误处理
描述可能的异常情况和处理方法。

## 更多资源
- [示例代码](../Examples~/README.md)
- [性能优化指南](../DevDocs/Performance.md)
- [设计文档](../DevDocs/DesignDoc.md)

## 类
### ClassName
- **描述**: 这是一个示例类，用于演示API文档的结构。
- **方法**:
  - `MethodName()`: 描述此方法的功能。

## 方法
### MethodName()
- **参数**:
  - `_argumentName`: 描述参数的功能。
- **返回值**: 描述返回值的类型和含义。

## 示例
```csharp
// 示例代码
ClassName instance = new ClassName();
instance.MethodName();
``` 