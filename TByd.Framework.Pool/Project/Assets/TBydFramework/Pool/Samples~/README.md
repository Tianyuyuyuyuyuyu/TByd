# TBydFramework Pool 示例

本示例包展示了如何使用 TBydFramework Pool 系统的各种功能。每个示例都包含完整的场景、脚本和测试。

## 示例列表

### 1. 基础对象池 (Basic Object Pool)
展示如何使用 `ObjectPool<T>` 来管理普通C#对象。
- 基本的池创建和配置
- 对象的获取和返回
- 池的预热和清理
- 完整的单元测试

### 2. GameObject对象池 (GameObject Pool)
展示如何使用 `GameObjectPool` 来管理Unity游戏对象。
- 预制体实例的池化管理
- 对象的生命周期控制
- 层级结构组织
- 运行时可视化示例

### 3. 共享对象池 (Shared Pool)
展示如何在多个组件间共享同一个对象池。
- 静态池的实现
- 多用户访问控制
- 线程安全处理
- 资源的生命周期管理

### 4. Addressable对象池 (Addressable Pool)
展示如何使用 `AddressableGameObjectPool` 来管理Addressable资源。
- Addressable资源的池化管理
- 异步加载和预热
- 资源的正确释放
- 条件编译支持

## 运行示例

1. 在Unity编辑器中打开示例场景
2. 每个示例场景都包含一个示例制器对象
3. 运行场景并使用屏幕上的GUI按钮进行交互
4. 查看控制台输出了解池的运行状态

## 示例结构

每个示例都遵循以下结构： 

Ctrl+K to generate a command
 @Codebase  阅读当前工程代码，希望参照@TestFrameworkSamples 里的文件以及文件结构，在@Samples~ 文件夹下添加当前项目的类似的文件及文件结构

我来帮你在 Samples~ 文件夹下创建示例代码和结构。基于当前的对象池项目，我们可以创建以下示例结构：
让我们逐个创建这些文件：
这些示例展示了对象池系统的主要功能：
基础对象池的使用
GameObject对象池的使用
共享对象池的实现
Addressable资源对象池的使用
每个示例都包含了：
基本的池创建和配置
对象的获取和返回
GUI界面用于交互测试
适当的资源清理
这些示例可以帮助用户快速理解和使用对象池系统的各种功能。用户可以直接在Unity中运行这些示例来查看效果。
@TestFrameworkSamples 仔细对比里面的文件和结构，现在@Samples~ 里的文件和结构不是很一致，这是unity custom package的sample文件夹，你可以自由发挥，要求规范专业

我明白了,让我重新组织一下示例的结构,使其更符合Unity Package的标准结构。参考TestFrameworkSamples的组织方式,我们应该这样组��:
让我们从.sample.json开始重写:
然后是第一个示例的核心文件:
对应的程序集定义文件:
测试程序集定义:
这样的结构更加规范和专业,每个示例都:
有独立的场景文件
2. 有自己的程序集定义
包含测试
遵循Unity包的标准目录结构
提供完整的文档和注释
我可以继续完成其他示例的详细实现,你觉得这个结构如何?
继续

好的,让我们继续完成示例的其他部分。首先添加BasicObjectPool的测试文件:
然后创建示例场景:
这样我们就完成了第一个示例的完整结构。每个示例都包含:
场景文件 (.unity)
运行时代码 (Runtime/Scripts/)
程序集定义文件 (.asmdef)
测试代码 (Tests/)
测试程序集定义文件 (Tests/.asmdef)
需要我继续完成其他示例的实现吗？
继续

好的,让我们继续完成第二个示例 - GameObject Pool。首先创建测试文件:
创建程序集定义文件:
测试程序集定义:
更新示例脚本,添加更多功能和注释:
这个示例展示了:
如何正确设置和管理GameObject对象池
如何处理对象的生命周期
如何组织池化对象的层级结构
如何添加简单的运行时交互
如何编写相应的测试
需要我继续完成其他示例吗？
继续

好的,让我们继续完成第三个示例 - SharedPool。这个���例将展示如何在多个组件之间共享同一个对象池。
首先创建测试文件:
创建程序集定义文件:
测试程序集定义:
更新示例脚本:
这个示例展示了:
如何使用静态字段实现对象池共享
如何安全地处理多个组件访问同一个池
如何管理共享资源的生命周期
如何在多用户场景下组织和显示UI
如何使用颜色区分不同用户的对象
需要我继续完成最后一个示例(Addressable Pool)吗？
继续

好的,让我们完成最后一个示例 - Addressable Pool。这个示例将展示如何使用对象池来管理Addressable资源。
首先创建测试文件:
创建程序集定义文件:
测试程序集定义:
更新示例脚本:
这个示例展示了:
如何使用AddressableGameObjectPool管理Addressable资源
如何处理异步加载和预热
如何正确清理和释放Addressable资源
如何使用条件编译确保Addressable支持
如何编写相应的测试
所有示例都遵循了相同的结构和最佳实践:
1. 清晰的命名空间组织
完整的程序集定义
全面的测试覆盖
详细的注释和文档
一致的代码风格
这样就完成了所有四个示例的实现。需要我为整个示例包添加一个README文档吗？
需要

好的,让我们为整个示例包添加一个README文档:
ExampleName/
├── Runtime/
│ ├── Scripts/
│ │ └── ExampleScript.cs
│ └── ExampleName.asmdef
├── Tests/
│ ├── ExampleTests.cs
│ └── ExampleName.Tests.asmdef
└── ExampleName.unity

## 测试

所有示例都包含完整的测试套件：
1. 使用Unity Test Framework
2. 包含编辑器和运行时测试
3. 测试覆盖主要功能点
4. 提供测试最佳实践示例

## 注意事项

1. Addressable示例需要Unity Addressable系统支持
2. 部分示例可能需要额外的设置（如创建预制体）
3. 示例代码包含详细注释，建议配合源码阅读

## 扩展示例

这些示例展示了基本用法，你可以基于它们扩展：
1. 添加自定义对象类型
2. 实现特定的池化策略
3. 集成到具体的游戏系统中
4. 添加更多的性能优化

## 相关文档

- [API文档](../Documentation~/api.md)
- [使用指南](../Documentation~/manual.md)
- [更新日志](../CHANGELOG.md)

## 反馈

如果你有任何问题或建议，请通过以下方式反馈：
1. 提交Issue
2. 发送Pull Request
3. 联系作者

## 许可

这些示例代码遵循与主项目相同的许可协议。

## 版本兼容性
- 这些示例与TBydFramework.Pool v0.2.0及以上版本兼容
- 建议使用Unity 2021.3或更高版本

### Addressable支持
1. Addressable示例需要Unity Addressable系统支持