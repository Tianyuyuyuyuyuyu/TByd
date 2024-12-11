# TBydFramework Pool

TBydFramework Pool 是一个专业级的对象池管理系统，专为 Unity 项目设计优化。它提供了全面的内存管理和对象复用解决方案，可显著提升项目性能，减少内存碎片和 GC 压力。

## 核心特性

### 🚀 全面的对象池支持

- **GameObject 对象池**
  - 高效管理和复用 GameObject 实例
  - 支持预制体实例化优化
  - 自动处理激活/禁用状态
  
- **组件对象池**
  - 智能组件引用管理
  - 支持多组件协同工作
  - 自动组件状态维护
  
- **资源对象池**
  - Unity 资源（Asset）智能缓存
  - 自动资源加载和卸载
  - 内存占用优化
  
- **共享对象池**
  - 全局共享实例管理
  - 跨场景对象复用
  - 智能引用计数
  
- **事件对象池**
  - 事件对象复用
  - 减少事件系统内存分配
  - 高性能消息传递
  
- **缓存池**
  - 通用数据结构缓存
  - 自动过期清理
  - 内存使用优化

### 💪 高级功能

- **智能预热系统**
  - 基于使用情况的自动预热
  - 可配置的预热策略
  - 场景加载优化

- **动态容量管理**
  - 自适应池大小调整
  - 基于使用率的容量优化
  - 内存占用控制

- **全面的对象验证**
  - 完整性检查
  - 状态验证
  - 异常检测和报告

- **实时性能监控**
  - 详细的性能指标统计
  - 内存使用分析
  - 性能瓶颈检测

- **诊断系统**
  - 错误追踪和日志
  - 性能问题诊断
  - 运行时分析工具

### 🔌 扩展功能

- **Addressables 集成**
  - 异步资源加载支持
  - 智能资源释放
  - 内存管理优化

- **UniTask 支持**
  - 异步操作优化
  - 非阻塞式资源加载
  - 性能优化

- **自定义扩展**
  - 灵活的扩展接口
  - 自定义池类型支持
  - 插件化架构

## 系统要求

- Unity 2019.4 或更高版本
- .NET Standard 2.0+

## 安装指南

### 方法 1：通过 Unity Package Manager

1. 打开 Package Manager (Window > Package Manager)
2. 点击 "+" > "Add package from git URL"
3. 输入: `https://github.com/Tianyuyuyuyuyuyu/TByd.git#master`

### 方法 2：手动安装

1. 下载最新版本
2. 解压到项目的 Assets 目录

### 可选依赖

- Addressables Package (启用 `TBYD_ADDRESSABLES_SUPPORT`)
- UniTask Package (启用 `TBYD_UNITASK_SUPPORT`)

## 快速入门

### 基础对象池

```csharp
// 创建对象池
var pool = new ObjectPool<MyClass>(() => new MyClass());
// 获取对象
var obj = pool.Get();
// 返回对象
pool.Return(obj);
```

### GameObject 对象池使用

```csharp
// 创建 GameObject 池
var pool = new GameObjectPool(prefab);
// 预热池
pool.Prewarm(5);
// 获取实例
var instance = pool.Get();
// 返回实例
pool.Return(instance);
```

### 使用 PoolManager

```csharp
// 注册预制体
PoolManager.Instance.RegisterPrefab<MyComponent>(prefab);
// 获取对象池
var pool = PoolFactory.CreatePool<MyComponent>();
// 获取实例
var component = pool.Get();
```

## 高级功能

### 对象池配置

```csharp
var settings = ScriptableObject.CreateInstance<PoolSettings>();
settings.DefaultPoolSize = 32;
settings.EnableAutoPrewarm = true;
var pool = new GameObjectPool(prefab, settings);
```

### 对象回调

```csharp
public class MyPooledObject : PoolCallbacksBehaviour
{
    public override void OnGet()
    {
        // 对象被获取时调用
    }
    
    public override void OnReturn()
    {
        // 对象被返回时调用
    }
}
```

### 性能监控

```csharp
var report = PoolPerformanceMonitor.Instance.GenerateReport("MyPool");
Debug.Log(report.ToString());
```

## 最佳实践

### 1. 预热池

- 在场景加载时预热常用对象池
- 根据实际需求设置合适的预热数量

### 2. 配置优化

- 使用 PoolSettings 配置池参数
- 启用自动清理以防止内存泄漏
- 合理设置池容量

### 3. 性能考虑

- 使用共享池处理相同预制体
- 启用对象验证以提前发现问题
- 监控池性能以便及时优化

## 注意事项

### 1. 对象回收

- 确保返回对象到正确的池
- 不要重复返回同一对象
- 返回前重置对象状态

### 2. 线程安全

- 主线程操作 GameObject
- 使用 AsyncPool 处理异步操作
- 注意并发访问的同步问题

## 故障排除

### 常见问题及解决方案

#### 1. 对象未正确回收

- 检查是否正确调用 Return 方法
- 确认对象未被销毁

#### 2. 内存泄漏

- 检查是否及时清理不用的池
- 确保正确处理池的生命周期

#### 3. 性能问题

- 查看性能监控报告
- 优化池配置参数
- 考虑使用共享池

## 贡献指南

欢迎提交 Issue 和 Pull Request 来改进本项目。请确保：

1. 遵循现有代码风格
2. 添加适当的单元测试
3. 更新相关文档

## 许可证

[MIT License](https://opensource.org/licenses/MIT)

## 支持

如有问题或建议，请通过以下方式联系：

- GitHub Issues: [[Issues](https://github.com/Tianyuyuyuyuyuyu/TByd/issues)]
- 邮件支持: [[Google Mail](tianyulovecars@gmail.com)]
- Discord: [[Discord](https://discord.gg/dq3QGsF7)]

---
© 2024 TBydFramework. All rights reserved.