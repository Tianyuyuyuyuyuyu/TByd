# 贡献指南

感谢你考虑为TBydFramework Pool做出贡献！

## 开发流程

1. Fork本仓库
2. 创建你的特性分支 (`git checkout -b feature/amazing-feature`)
3. 提交你的修改 (`git commit -m '添加一些很棒的功能'`)
4. 推送到分支 (`git push origin feature/amazing-feature`)
5. 开启一个Pull Request

## 代码规范

### 命名规范

- 使用PascalCase命名类和方法
- 使用camelCase命名私有字段，前缀下划线
- 使用有意义的描述性名称

### 代码风格

- 使用4个空格缩进
- 大括号单独一行
- 添加适当的空行提高可读性
- 所有公开API添加XML文档注释

### 注释规范

```csharp
/// <summary>
/// 类/方法的简要说明
/// </summary>
/// <param name="paramName">参数说明</param>
/// <returns>返回值说明</returns>
public class/method...
```

## 提交规范

- feat: 新功能
- fix: 修复Bug
- docs: 文档更新
- style: 代码格式调整
- refactor: 重构
- test: 测试相关
- chore: 构建过程或辅助工具的变动

## 测试要求

- 为所有新功能添加单元测试
- 确保所有测试通过
- 保持测试覆盖率

## 文档要求

- 更新API文档
- 添加/更新示例代码
- 更新CHANGELOG.md
- 必要时更新README.md

## 版本规范

遵循语义化版本规范：

- 主版本号：不兼容的API修改
- 次版本号：向下兼容的功能性新增
- 修订号：向下兼容的问题修正

## 许可

贡献的代码将采用与项目相同的MIT许可证。

## 联系方式

如有任何问题，请通过以下方式联系：

- 提交Issue
- 发送邮件
- 其他联系方式 