# 贡献指南

感谢您对 NPM Registry Manager 项目的关注！我们欢迎各种形式的贡献，包括但不限于代码贡献、文档改进、问题报告和功能建议。

## 目录

1. [行为准则](#行为准则)
2. [开始贡献](#开始贡献)
3. [开发流程](#开发流程)
4. [提交指南](#提交指南)
5. [开发规范](#开发规范)

## 行为准则

### 我们的承诺

为了营造开放和友好的环境，我们作为贡献者和维护者承诺：
- 友善和包容的参与体验
- 尊重不同观点和经验
- 耐心接受建设性批评
- 关注最有利于社区的事情
- 对其他社区成员表示同理心

### 我们的责任

项目维护者有责任：
- 明确贡献标准
- 采取适当和公平的纠正措施
- 删除、编辑或拒绝不符合行为准则的内容

## 开始贡献

### 1. 准备工作

#### 开发环境
- Flutter SDK (>=3.0.0)
- Dart SDK (>=3.0.0)
- Visual Studio Code 或其他 IDE
- Git

#### 克隆项目
```bash
git clone https://github.com/tbyd/npm-registry-manager.git
cd npm-registry-manager
flutter pub get
```

### 2. 选择任务

您可以从以下方面入手：
- 修复 [Issues](https://github.com/tbyd/npm-registry-manager/issues) 中的 bug
- 改进文档
- 添加新功能
- 优化性能
- 改进用户体验

## 开发流程

### 1. 创建分支

```bash
# 更新主分支
git checkout main
git pull origin main

# 创建新分支
git checkout -b feature/your-feature
# 或
git checkout -b fix/your-bugfix
```

### 2. 开发

- 遵循代码规范
- 编写测试用例
- 保持提交信息清晰

### 3. 测试

```bash
# 运行测试
flutter test

# 代码格式化
flutter format .

# 静态分析
flutter analyze
```

### 4. 提交变更

```bash
git add .
git commit -m "feat: add new feature"
# 或
git commit -m "fix: fix some bug"
```

### 5. 推送和创建 Pull Request

```bash
git push origin feature/your-feature
```

然后在 GitHub 上创建 Pull Request。

## 提交指南

### 提交信息格式

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Type 类型

- feat: 新功能
- fix: 修复
- docs: 文档改变
- style: 代码格式改变
- refactor: 重构代码
- perf: 性能优化
- test: 测试相关
- build: 构建系统或外部依赖变更
- ci: CI 配置变更

### 示例

```
feat(auth): add user authentication

- Add login page
- Implement JWT authentication
- Add user session management

Closes #123
```

## 开发规范

### 代码风格

- 使用 2 空格缩进
- 遵循 Dart 风格指南
- 使用有意义的变量名
- 添加必要的注释

### 文件组织

```
lib/
  ├── src/
  │   ├── core/           # 核心功能
  │   ├── features/       # 功能模块
  │   ├── shared/        # 共享组件
  │   └── utils/         # 工具类
  └── main.dart
```

### 命名规范

- 类名：PascalCase
- 变量和函数：camelCase
- 常量：SCREAMING_SNAKE_CASE
- 私有成员：_camelCase

### 测试规范

- 单元测试覆盖核心功能
- 集成测试覆盖主要流程
- 测试文件命名：`*_test.dart`
- 使用有意义的测试描述

## 文档贡献

### 文档结构
```
docs/
  ├── user-guide.md      # 用户指南
  ├── api/              # API 文档
  ├── development/      # 开发文档
  └── faq.md           # 常见问题
```

### 文档风格
- 使用清晰的语言
- 提供具体的示例
- 保持文档最新
- 添加必要的截图

## 问题报告

### 报告 Bug
- 使用问题模板
- 提供复现步骤
- 附加错误日志
- 说明环境信息

### 功能建议
- 描述具体需求
- 解释使用场景
- 提供实现建议

## 发布流程

### 版本号规范
- 遵循语义化版本
- 更新 CHANGELOG.md
- 标记版本标签

### 发布检查清单
- 所有测试通过
- 文档已更新
- CHANGELOG 已更新
- 版本号已更新

## 获取帮助

如果您在贡献过程中需要帮助：

- 📧 Email: [tianyulovecars@gmail.com](mailto:tianyulovecars@gmail.com)
- 💬 Discussions: [GitHub Discussions](https://github.com/tbyd/npm-registry-manager/discussions)
- 🐛 Issues: [GitHub Issues](https://github.com/tbyd/npm-registry-manager/issues)

## 致谢

感谢所有贡献者的付出！ 