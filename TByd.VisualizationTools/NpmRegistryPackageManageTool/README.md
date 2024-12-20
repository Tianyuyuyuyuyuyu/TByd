# NPM Registry Package Manage Tool

基于 Flutter 开发的 NPM 私有仓库管理工具，专门用于管理和维护基于 Verdaccio 的私有 NPM 仓库。

## 功能特性

### 包管理
- 📦 浏览和搜索仓库中的所有包
- 🔄 显示包的详细信息（版本、作者、许可证等）
- 📝 查看包的 README 和 CHANGELOG
- 🚀 一键上传新版本包
- 🗑️ 删除不需要的包版本

### 用户界面
- 🎨 现代化的 Material Design 3 界面
- 🌙 支持浅色/深色主题
- 📱 响应式设计，支持桌面和移动设备
- 🔍 实时搜索功能
- 📊 版本历史可视化

### 安全性
- 🔐 安全的身份验证机制
- 🔑 支持 JWT Token 认证
- 👤 用户权限管理

## 系统要求

- Windows 10 或更高版本
- iOS 11.0 或更高版本
- Node.js 14.0 或更高版本
- npm 6.0 或更高版本

## 安装说明

1. 确保你的系统已安装 Flutter SDK
```bash
flutter --version
```

2. 克隆项目
```bash
git clone https://github.com/Tianyuyuyuyuyuyu/TByd
cd TByd/TByd.VisualizationTools/NpmRegistryPackageManageTool
```

3. 安装依赖
```bash
flutter pub get
```

4. 运行应用
```bash
flutter run
```

## 配置说明

1. 在首次启动时，需要配置 Verdaccio 服务器地址
2. 输入你的用户名和密码进行身份验证
3. 登录成功后即可开始使用所有功能

## 使用方法

### 包管理
1. 在主界面可以浏览所有已发布的包
2. 使用搜索框快速查找特定的包
3. 点击包名查看详细信息
4. 在详情页面可以：
   - 查看 README 文档
   - 查看版本历史
   - 查看依赖关系
   - 管理包版本

### 上传新包
1. 点击上传按钮
2. 选择要上传的包
3. 确认版本信息
4. 点击确认上传

### 版本管理
1. 在包详情页面可以查看所有版本
2. 支持删除特定版本
3. 可以查看每个版本的具体变更

## 技术栈

- 🎯 Dart
- 💙 Flutter
- 🔄 Riverpod (状态管理)
- 🌐 http (网络请求)
- 🎨 Material 3 (UI设计)

## 贡献指南

1. Fork 项目
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 创建 Pull Request

## 许可证

MIT License - 详见 [LICENSE](LICENSE) 文件

## 联系方式

- 项目维护者：TByd Team
- 邮箱：tianyulovecars@gmail.com
- 项目主页：https://github.com/Tianyuyuyuyuyuyu/TByd/tree/master/TByd.VisualizationTools/NpmRegistryPackageManageTool

## 致谢

感谢所有为这个项目做出贡献的开发者！ 