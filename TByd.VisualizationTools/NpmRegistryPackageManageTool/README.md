# NPM Registry 包管理工具

基于 Flutter 开发的 Verdaccio NPM 仓库管理工具，支持 Windows 和 iOS 平台。

## 功能特点

- 连接阿里云仓库，支持安全认证
- 包管理操作（推送更新、删除包）
- 详细的包信息查看
- 可视化界面
- 日志与审计功能
- 多语言支持

## 开发环境要求

- Flutter SDK
- Dart SDK
- Windows 开发环境（用于 Windows 平台开发）
- macOS 和 Xcode（用于 iOS 平台开发）

## 安装与运行

1. 克隆项目
```bash
git clone [项目地址]
```

2. 安装依赖
```bash
flutter pub get
```

3. 运行项目
```bash
# Windows
flutter run -d windows

# iOS
flutter run -d ios
```

## 技术栈

- Flutter
- Dart
- HTTP/HTTPS 协议
- SQLite（本地数据存储）
- Provider（状态管理）

## 项目结构

```
lib/
├── api/          # API 接口
├── models/       # 数据模型
├── screens/      # 页面
├── widgets/      # 组件
├── utils/        # 工具类
└── main.dart     # 入口文件
```

## 贡献指南

欢迎提交 Issue 和 Pull Request

## 许可证

MIT License 