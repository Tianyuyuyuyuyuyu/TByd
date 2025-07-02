# TByd Template

<div align="center">

![版本](https://img.shields.io/badge/版本-0.1.0-blue.svg)
![Unity](https://img.shields.io/badge/Unity-2021.3.8f1+-brightgreen.svg)
![许可证](https://img.shields.io/badge/许可证-MIT-green.svg)

</div>

## 简介

TByd Template 是一个专业的 Unity Package Manager (UPM) 包开发模板项目。它提供了完整的 UPM 包结构和丰富的基础功能，帮助开发者快速创建符合标准的高质量 Unity 包。本模板遵循 Unity 官方推荐的最佳实践，确保您的包具有良好的兼容性、可维护性和扩展性。

## 特性

- **标准化结构** - 完整的 UPM 包目录结构和文件组织
- **程序集定义** - 预配置的 Assembly Definition 文件，支持运行时和编辑器分离
- **文档模板** - 包含 API 参考、开发指南和使用说明的文档框架
- **示例工程** - 演示包功能的示例代码和场景
- **测试框架** - 集成 Unity Test Framework，支持编辑器和运行时测试
- **编辑器工具** - 包含创建新包的编辑器扩展工具
- **异步支持** - 集成 UniTask 提供高性能的异步操作支持
- **CI/CD 配置** - 预设的持续集成和部署配置文件

## 安装

### 通过 Git URL 安装

在 Unity 编辑器中，打开 Package Manager (菜单: Window > Package Manager)，点击左上角的 "+" 按钮，选择 "Add package from git URL..."，然后输入以下 URL：

```
https://github.com/YourOrganization/YourPackageName.git
```

> **注意**：发布时请将上述 URL 替换为您的实际仓库 URL。

### 通过本地文件安装

1. 克隆或下载此仓库到本地
2. 在 Unity 编辑器中，打开 Package Manager
3. 点击左上角的 "+" 按钮，选择 "Add package from disk..."
4. 浏览并选择包含 `package.json` 文件的 `TByd.Template` 文件夹

## 使用方法

### 创建新的 UPM 包

#### 使用编辑器工具（推荐）

1. 在 Unity 编辑器中，打开菜单 `TByd > Template > 创建新的UPM包`
2. 在弹出的向导窗口中填写包信息：
   - 包名称（格式：`com.company.package`）
   - 显示名称
   - 版本号
   - 描述
   - 作者信息
   - 依赖项
3. 点击 "创建" 按钮，系统将自动生成包结构

#### 手动创建

1. 复制 `TByd.Template` 目录到您的项目中
2. 重命名目录为您的包名（建议使用反向域名格式，如 `com.yourcompany.yourpackage`）
3. 修改 `package.json` 文件中的相关信息：
   - `name`: 包的唯一标识符
   - `displayName`: 在 Package Manager 中显示的名称
   - `version`: 遵循语义化版本规范的版本号
   - `description`: 包的简短描述
   - `author`: 作者信息
   - `dependencies`: 依赖项列表
4. 更新 `README.md` 和其他文档文件
5. 修改程序集定义文件 (asmdef) 的名称和引用

### 开发流程

1. **规划 API** - 设计清晰、一致且易于使用的 API
2. **实现核心功能** - 开发包的核心功能和特性
3. **添加编辑器扩展** - 为包添加编辑器工具和自定义检视器（如需）
4. **创建示例** - 开发演示包功能的示例场景和代码
5. **编写文档** - 完善 API 参考文档和使用指南
6. **测试** - 编写并运行单元测试和集成测试
7. **发布** - 更新版本号和变更日志，发布包

## 示例

`Samples~` 文件夹中包含以下示例，展示如何使用此模板：

- **基础用法** - 演示包的基本功能和 API 使用方法
- **高级特性** - 展示包的高级功能和最佳实践
- **完整示例** - 包含所有功能的综合演示场景

要导入示例，请在 Package Manager 中选择此包，然后点击 "Samples" 下的 "Import" 按钮。

## 文档

详细文档位于 `Documentation~` 文件夹：

- [API 参考](Documentation~/API.md) - 详细的 API 文档和使用示例
- [开发指南](Documentation~/DevDocs/UpmPackageGuide.md) - UPM 包开发的完整指南
- [参考资料](Documentation~/DevDocs/References/index.md) - 相关资源和参考链接

## 依赖项

- **Unity** - 2021.3.8f1 或更高版本
- **[UniTask](https://github.com/Cysharp/UniTask)** - 2.5.0 或更高版本（用于高性能异步操作）

## 贡献

我们欢迎并感谢社区成员的贡献！您可以通过以下方式参与项目：

- 提交代码改进和新功能
- 报告 Bug 和提出建议
- 改进文档和示例
- 分享使用经验和最佳实践

请参阅 [贡献指南](CONTRIBUTING.md) 了解详细的贡献流程和规范。

## 许可证

本项目采用 MIT 许可证。详情请参阅 [LICENSE](LICENSE.md) 文件。

## 作者

- **TByd** - *项目创建者* - [GitHub](https://github.com/Tianyuyuyuyuyuyu) - tianyulovecars@gmail.com
- **BeiDa** - *核心贡献者* - [GitHub](https://github.com/YourUsername) - zhaoqing343@gmail.com

## 版本历史

有关所有版本的详细更改记录，请参阅 [CHANGELOG.md](CHANGELOG.md) 文件。

---

<div align="center">
  <p>Made with ❤️ by TByd Team</p>
  <p>
    <a href="https://github.com/Tianyuyuyuyuyuyu/TByd/tree/master/TByd.UpmTemplate">
      <img src="https://img.shields.io/badge/GitHub-查看仓库-blue?style=flat&logo=github" alt="GitHub Repository">
    </a>
  </p>
</div> 