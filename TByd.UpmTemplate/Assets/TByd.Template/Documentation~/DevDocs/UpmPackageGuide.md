# UPM包开发指南

本指南将帮助您使用TByd.Template模板创建、开发和发布自己的Unity Package Manager (UPM)包。

## 目录

- [创建新包](#创建新包)
- [包结构说明](#包结构说明)
- [开发流程](#开发流程)
- [测试](#测试)
- [文档编写](#文档编写)
- [版本控制](#版本控制)
- [发布](#发布)
- [最佳实践](#最佳实践)

## 创建新包

### 使用模板向导

1. 在Unity编辑器中，打开菜单 `TByd > Template > 创建新的UPM包`
2. 在弹出的向导窗口中填写以下信息：
   - 包名称（格式：`com.yourcompany.yourpackagename`）
   - 显示名称
   - 版本号（建议从`0.1.0`开始）
   - 描述
   - 作者信息
   - 依赖项
3. 选择输出路径
4. 点击"创建"按钮

### 手动创建

1. 复制`TByd.Template`目录到您的项目中
2. 重命名目录为您的包名
3. 修改`package.json`文件中的相关信息
4. 更新`README.md`和其他文档文件
5. 修改程序集定义文件(asmdef)的名称和引用

## 包结构说明

UPM包的标准结构如下：

```
YourPackageName/
├── package.json            # 包的元数据
├── README.md               # 包的说明文档
├── CHANGELOG.md            # 版本更新记录
├── LICENSE.md              # 许可证文件
├── Third-Party Notices.md  # 第三方通知
├── Editor/                 # 编辑器相关代码
│   └── YourPackage.Editor.asmdef
├── Runtime/                # 运行时代码
│   └── YourPackage.Runtime.asmdef
├── Tests/                  # 测试代码
│   ├── Editor/             # 编辑器测试
│   │   └── YourPackage.Editor.Tests.asmdef
│   └── Runtime/            # 运行时测试
│       └── YourPackage.Runtime.Tests.asmdef
├── Documentation~/         # 文档（不会被导入Unity）
│   ├── index.md            # 文档主页
│   └── API.md              # API文档
├── Samples~/              # 示例（不会被导入Unity，但可通过Package Manager导入）
└── EditorResources/       # 编辑器资源
```

### 关键文件说明

#### package.json

包含包的元数据，包括名称、版本、依赖项等。示例：

```json
{
  "name": "com.yourcompany.yourpackage",
  "displayName": "Your Package",
  "version": "0.1.0",
  "unity": "2021.3",
  "description": "Description of your package",
  "keywords": ["keyword1", "keyword2"],
  "category": "YourCategory",
  "dependencies": {
    "com.unity.some-package": "1.0.0"
  },
  "author": {
    "name": "Your Name",
    "email": "your.email@example.com",
    "url": "https://your-website.com"
  }
}
```

#### 程序集定义文件(asmdef)

定义代码的编译单元，控制代码的可见性和依赖关系。

- `Runtime/YourPackage.Runtime.asmdef`：运行时代码
- `Editor/YourPackage.Editor.asmdef`：编辑器代码（依赖运行时程序集）
- `Tests/Runtime/YourPackage.Runtime.Tests.asmdef`：运行时测试
- `Tests/Editor/YourPackage.Editor.Tests.asmdef`：编辑器测试

## 开发流程

### 1. 规划API

在开始编码前，先规划您的API：
- 确定公共接口
- 设计类层次结构
- 规划命名空间
- 考虑扩展性和兼容性

### 2. 实现核心功能

从核心功能开始实现：
- 创建基础类和接口
- 实现核心逻辑
- 确保代码可测试

### 3. 添加编辑器扩展

如果需要，添加编辑器扩展：
- 自定义编辑器
- 属性绘制器
- 编辑器窗口
- 菜单项

### 4. 创建示例

在`Samples~`目录中创建示例：
- 基本用法示例
- 高级功能示例
- 集成示例

### 5. 编写文档

详细编写文档：
- 更新README.md
- 编写API文档
- 创建使用教程
- 添加代码注释

### 6. 测试

全面测试您的包：
- 单元测试
- 集成测试
- 性能测试
- 兼容性测试

## 测试

### 编写测试

使用Unity Test Framework编写测试：

```csharp
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using YourNamespace;

public class YourTests
{
    [Test]
    public void SimpleTest()
    {
        // 准备
        var yourClass = new YourClass();
        
        // 执行
        var result = yourClass.YourMethod();
        
        // 验证
        Assert.AreEqual(expectedValue, result);
    }
    
    [UnityTest]
    public IEnumerator AsyncTest()
    {
        // 准备
        var yourClass = new YourClass();
        
        // 执行异步操作
        yourClass.YourAsyncMethod();
        
        // 等待
        yield return new WaitForSeconds(0.1f);
        
        // 验证
        Assert.IsTrue(yourClass.IsComplete);
    }
}
```

### 运行测试

1. 打开Test Runner窗口（`Window > General > Test Runner`）
2. 选择PlayMode或EditMode选项卡
3. 点击"Run All"或选择特定测试运行

## 文档编写

### README.md

包含以下内容：
- 包的简介
- 安装说明
- 基本用法
- 功能列表
- 依赖项
- 许可证信息

### API文档

在`Documentation~/API.md`中详细描述：
- 命名空间
- 类和接口
- 方法和属性
- 枚举和常量
- 使用示例

### 代码注释

使用XML文档注释：

```csharp
/// <summary>
/// 描述类的功能。
/// </summary>
public class YourClass
{
    /// <summary>
    /// 描述方法的功能。
    /// </summary>
    /// <param name="parameter">参数说明</param>
    /// <returns>返回值说明</returns>
    /// <exception cref="System.Exception">可能抛出的异常</exception>
    public ReturnType YourMethod(ParameterType parameter)
    {
        // 实现
    }
}
```

## 版本控制

### 语义化版本控制

遵循[语义化版本控制](https://semver.org/lang/zh-CN/)规范：

- **主版本号**：不兼容的API变更
- **次版本号**：向下兼容的功能性新增
- **修订号**：向下兼容的问题修正

### 更新CHANGELOG.md

每次发布新版本时更新CHANGELOG.md：

```markdown
# 更新日志

## [0.2.0] - 2023-03-15

### 添加
- 新增功能A
- 新增功能B

### 修改
- 改进功能C
- 优化性能

### 修复
- 修复问题D
- 修复问题E

## [0.1.0] - 2023-02-01

### 添加
- 初始版本
```

## 发布

### 本地测试

1. 在测试项目中，通过Package Manager添加本地包：
   - 点击"+"按钮
   - 选择"Add package from disk..."
   - 选择包的根目录

### 发布到Git仓库

1. 创建Git仓库
2. 将包内容推送到仓库
3. 创建版本标签（如`v0.1.0`）
4. 通过Git URL安装：
   ```
   https://github.com/YourUsername/YourRepository.git#v0.1.0
   ```

### 发布到OpenUPM

1. 在[OpenUPM](https://openupm.com/)上注册包
2. 按照OpenUPM的指南配置包
3. 用户可以通过以下命令安装：
   ```
   openupm add com.yourcompany.yourpackage
   ```

### 发布到Unity Asset Store

1. 在[Unity Asset Store](https://assetstore.unity.com/)上注册为发布者
2. 创建新的资源
3. 上传包内容
4. 等待审核

## 最佳实践

### 代码质量

- 遵循C#编码规范
- 使用适当的命名约定
- 编写单元测试
- 避免使用反射和动态代码生成
- 避免在Update方法中执行重量级操作

### 性能优化

- 使用对象池减少垃圾回收
- 缓存频繁访问的组件和值
- 使用协程或UniTask处理异步操作
- 优化资源加载和卸载
- 使用性能分析工具检测瓶颈

### 兼容性

- 明确支持的Unity版本
- 测试不同平台的兼容性
- 使用预处理指令处理版本差异
- 避免使用已弃用的API
- 考虑向后兼容性

### 文档和示例

- 提供详细的API文档
- 创建简单易懂的示例
- 添加注释解释复杂逻辑
- 更新CHANGELOG.md
- 提供故障排除指南

### 依赖管理

- 最小化外部依赖
- 明确声明所有依赖
- 使用版本范围而非固定版本
- 考虑可选依赖的处理方式
- 使用程序集定义文件控制依赖关系 