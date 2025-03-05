# TByd.CodeStyle 测试文档

本文档介绍了TByd.CodeStyle包的测试策略、测试覆盖率和如何运行测试。

## 测试策略

TByd.CodeStyle包采用以下测试策略：

1. **单元测试**：测试各个组件的独立功能
2. **集成测试**：测试组件之间的交互
3. **UI测试**：测试编辑器UI组件的功能

## 测试覆盖率

当前测试覆盖了以下核心功能：

| 模块 | 测试文件 | 覆盖率 |
|------|----------|--------|
| 配置管理 | ConfigProviderTests.cs | 80% |
| Git提交消息检查 | CommitMessageCheckerTests.cs | 85% |
| Git钩子管理 | GitHookMonitorTests.cs | 75% |
| UI组件 | UIComponentTests.cs | 70% |
| 通知系统 | NotificationSystemTests.cs | 90% |

## 如何运行测试

### 方法1：使用Unity Test Runner

1. 在Unity编辑器中，打开 **Window > General > Test Runner**
2. 在Test Runner窗口中，选择 **EditMode** 标签页
3. 点击 **Run All** 按钮运行所有测试，或选择特定测试运行

### 方法2：使用菜单项

1. 在Unity编辑器中，点击 **TByd > CodeStyle > 运行所有测试**
2. 测试结果将在Console窗口中显示

### 方法3：通过代码运行

```csharp
using TByd.CodeStyle.Tests.Editor;

// 运行所有测试
TestRunner.RunAllTests();
```

## 添加新测试

要添加新测试，请按照以下步骤操作：

1. 在 `Tests/Editor` 或 `Tests/Runtime` 目录中创建新的测试类
2. 使用 `[Test]` 特性标记测试方法
3. 使用 `Assert` 类验证测试结果
4. 使用 `[SetUp]` 和 `[TearDown]` 特性设置和清理测试环境

示例：

```csharp
using NUnit.Framework;
using TByd.CodeStyle.Editor;

namespace TByd.CodeStyle.Tests.Editor
{
    public class MyTests
    {
        [SetUp]
        public void Setup()
        {
            // 设置测试环境
        }
        
        [TearDown]
        public void TearDown()
        {
            // 清理测试环境
        }
        
        [Test]
        public void MyTest_Condition_ExpectedResult()
        {
            // 准备
            var myObject = new MyClass();
            
            // 执行
            var result = myObject.MyMethod();
            
            // 验证
            Assert.AreEqual(expectedValue, result);
        }
    }
}
```

## 测试最佳实践

1. **测试命名**：使用 `[测试对象]_[条件]_[期望结果]` 格式命名测试方法
2. **独立测试**：每个测试应该独立运行，不依赖其他测试的状态
3. **测试边界条件**：测试正常情况、边界情况和错误情况
4. **使用模拟对象**：使用模拟对象隔离被测试的代码
5. **保持测试简单**：每个测试只测试一个功能点

## 持续集成

TByd.CodeStyle包的测试已集成到CI/CD流程中，每次提交都会自动运行测试。测试失败将阻止合并到主分支。

## 测试报告

测试报告可以在以下位置查看：

1. Unity Test Runner窗口
2. CI/CD流程的测试报告页面
3. 通过 `TByd > CodeStyle > 生成测试报告` 菜单生成的HTML报告 