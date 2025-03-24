# 贡献指南

感谢您对TByd Template的关注！我们欢迎并鼓励社区成员为项目做出贡献。本文档将指导您如何参与项目开发。

## 行为准则

参与本项目的所有贡献者都应遵循以下行为准则：
- 尊重所有参与者，不论经验、性别、性取向、残疾、种族或宗教信仰
- 使用友好和包容的语言
- 接受建设性批评，并优雅地处理反馈
- 专注于对社区最有利的事情
- 对其他社区成员表示同理心和支持

## 如何贡献

### 报告Bug

如果您发现了Bug，请通过GitHub Issues报告，并使用Bug报告模板，包含以下信息：
1. Bug的简短描述和影响范围
2. 详细的重现步骤，尽可能提供最小复现示例
3. 期望行为
4. 实际行为
5. 环境信息（Unity版本、操作系统、设备型号等）
6. 相关日志、截图或视频
7. 可能的解决方案（如果您有建议）

### 提出新功能

如果您有新功能建议，请通过GitHub Issues提交，并使用功能请求模板，包含以下信息：
1. 功能的详细描述
2. 使用场景和解决的问题
3. 预期的实现方式（如果有）
4. 替代方案（如果有）
5. 该功能如何使项目受益

### 提交代码

1. 在开始工作前，请先检查Issues和Pull Requests，确保没有人已经在处理相同的问题
2. Fork项目仓库到您的GitHub账户
3. 克隆您的Fork到本地：`git clone https://github.com/您的用户名/TByd.Template.git`
4. 创建您的特性分支：`git checkout -b feature/amazing-feature`
5. 进行必要的更改并添加测试
6. 确保所有测试通过
7. 提交您的更改：`git commit -m 'feat: add some amazing feature'`（遵循[约定式提交](https://www.conventionalcommits.org/zh-hans/v1.0.0/)）
8. 推送到分支：`git push origin feature/amazing-feature`
9. 创建Pull Request，填写详细的描述

### 代码风格

请遵循以下代码风格指南，确保代码的一致性和可读性：

#### 命名规范
- 变量：`m_VariableName`（私有成员）
- 常量：`c_ConstantName`
- 静态变量：`s_StaticName`
- 类/结构体：`ClassName`（PascalCase）
- 接口：`IInterfaceName`（前缀I，PascalCase）
- 属性：`PropertyName`（PascalCase）
- 方法：`MethodName()`（PascalCase）
- 参数：`_argumentName`（前缀下划线）
- 临时变量：`temporaryVariable`（camelCase）
- 枚举：`EnumName`（PascalCase）
- 枚举值：`EnumValue`（PascalCase）

#### 代码格式
- 使用4个空格缩进（不使用Tab）
- 大括号放在新行
- 每个文件末尾留一个空行
- 使用UTF-8编码（无BOM）
- 行宽限制在120个字符以内
- 使用显式类型而非var（除非类型明显）
- 避免不必要的注释
- 删除未使用的using语句

#### 注释规范
- 使用XML文档注释
- 为所有公共API添加文档注释
- 对复杂逻辑添加内联注释
- 使用TODO、HACK、FIXME等标记注明需要改进的地方

```csharp
/// <summary>
/// 处理模板数据。
/// </summary>
/// <param name="_templateData">要处理的模板数据，不能为null</param>
/// <returns>处理后的结果字符串</returns>
/// <exception cref="ArgumentNullException">当<paramref name="_templateData"/>为null时抛出</exception>
/// <exception cref="FormatException">当模板格式无效时抛出</exception>
public string ProcessTemplate(string _templateData)
{
    if (_templateData == null)
    {
        throw new ArgumentNullException(nameof(_templateData));
    }
    
    // 实现逻辑
    return ProcessedResult;
}
```

### 测试

良好的测试是确保代码质量的关键：

- 为所有新功能添加单元测试
- 为修复的Bug添加回归测试
- 确保所有测试通过
- 测试覆盖率应达到80%以上
- 使用Unity Test Framework编写测试
- 包括编辑器模式和播放模式测试
- 测试应该是独立的，不依赖于其他测试的执行顺序

```csharp
[Test]
public void ProcessTemplate_WithValidInput_ReturnsExpectedResult()
{
    // 准备
    var processor = new TemplateProcessor();
    var input = "Valid template data";
    var expected = "Expected result";
    
    // 执行
    var result = processor.ProcessTemplate(input);
    
    // 验证
    Assert.AreEqual(expected, result);
}
```

## 开发流程

我们采用以下开发流程，确保代码质量和项目进展：

1. **选择任务**：从Issues中选择一个任务，或创建新Issue描述您要解决的问题
2. **认领任务**：在Issue上留言表明您将处理此问题
3. **讨论方案**：与维护者和社区讨论实现方案
4. **开发**：按照上述提交代码流程进行开发
5. **测试**：编写测试并确保通过
6. **提交PR**：创建Pull Request并等待审查
7. **代码审查**：维护者会审查您的代码并提供反馈
8. **修改**：根据反馈进行必要的修改
9. **合并**：通过审查后，您的代码将被合并到主分支

## 版本控制

我们使用[语义化版本控制](https://semver.org/lang/zh-CN/)。版本格式为：`主版本号.次版本号.修订号`

- **主版本号**：不兼容的API变更（例如：2.0.0）
- **次版本号**：向下兼容的功能性新增（例如：1.1.0）
- **修订号**：向下兼容的问题修正（例如：1.0.1）

开发版本可能会使用预发布标识符，如`1.0.0-alpha.1`、`1.0.0-beta.2`等。

## 提交信息规范

我们使用[约定式提交](https://www.conventionalcommits.org/zh-hans/v1.0.0/)规范，提交信息应遵循以下格式：

```
<类型>[可选的作用域]: <描述>

[可选的正文]

[可选的脚注]
```

常用的类型包括：
- **feat**: 新功能
- **fix**: 修复Bug
- **docs**: 文档更新
- **style**: 代码风格变更（不影响功能）
- **refactor**: 代码重构
- **perf**: 性能优化
- **test**: 添加或修改测试
- **chore**: 构建过程或辅助工具的变动

示例：
```
feat(runtime): 添加模板处理器的异步支持

添加了基于UniTask的异步处理方法，提高了大型模板处理的性能。

关闭 #123
```

## 发布流程

新版本的发布遵循以下流程：

1. 更新CHANGELOG.md，记录所有变更
2. 更新package.json中的版本号
3. 创建版本提交：`git commit -m 'chore(release): v1.0.0'`
4. 创建Git标签：`git tag v1.0.0`
5. 推送到远程仓库：`git push && git push --tags`
6. 在GitHub上创建Release，详细描述变更
7. 发布到npm或OpenUPM（如适用）

## 分支策略

- **main**：主分支，包含稳定版本的代码
- **develop**：开发分支，包含最新开发版本的代码
- **feature/***：功能分支，用于开发新功能
- **fix/***：修复分支，用于修复Bug
- **release/***：发布分支，用于准备新版本发布
- **hotfix/***：热修复分支，用于紧急修复生产环境问题

## 许可证

通过贡献代码，您同意您的贡献将根据项目的[LICENSE](LICENSE.md)许可。这意味着您的代码将以相同的许可证提供给其他人使用。

## 联系方式

如果您有任何问题或需要帮助，可以通过以下方式联系我们：

- GitHub Issues：用于报告问题和讨论功能
- 电子邮件：tianyulovecars@gmail.com（主要维护者）

感谢您对TByd Template的贡献！ 