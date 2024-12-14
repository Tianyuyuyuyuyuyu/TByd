# NPM Registry Package Manager

基于 Flutter 开发的 Verdaccio NPM 仓库管理工具，支持 Windows 和 iOS 平台。

## 功能特性

- **仓库连接管理**
  - 支持 Verdaccio NPM 仓库的安全认证连接
  - 支持 HTTPS 协议加密通信
  - 支持多仓库配置和切换

- **包管理操作**
  - 包发布与更新
    - 支持单个包发布和批量发布
    - 版本冲突检测和处理
    - 发布进度实时显示
  - 包删除
    - 支持单个版本和整个包删除
    - 删除确认机制
    - 批量删除功能
  - 信息查看
    - 包详细信息展示
    - 版本历史记录
    - 依赖关系图谱
    - 下载统计数据

- **界面功能**
  - 包搜索和过滤
  - 批量操作支持
  - 实时状态更新
  - 操作进度显示

- **系统功能**
  - 操作日志记录
  - 数据导出功能
  - 多语言支持（中文/英文）
  - 主题切换

## 环境要求

### 开发环境
- Flutter SDK (>=3.1.0)
- Dart SDK (>=3.1.0)
- Windows 10/11 (用于 Windows 构建)
- macOS 与 Xcode (用于 iOS 构建)

### 运行环境
- Windows 测试版
  - Windows 10/11 64位系统
  - 最小 4GB RAM
  - 500MB 可用磁盘空间
  - 网络连接（用于包管理）

## 安装说明

### 开发者安装

1. 克隆仓库：
   ```bash
   git clone [repository-url]
   cd npm-registry-manager
   ```

2. 安装依赖：
   ```bash
   flutter pub get
   ```

3. 运行应用：
   ```bash
   # 开发模式运行
   flutter run

   # 指定平台运行
   flutter run -d windows  # Windows平台
   flutter run -d ios      # iOS平台
   ```

### Windows 测试版安装

1. 下载最新测试版安装包：
   - 从 Release 页面下载最新的 `npm-registry-manager-windows-{version}.zip`

2. 解压安装：
   - 解压下载的 ZIP 文件到目标目录
   - 运行 `setup.exe` 进行安装
   - 按照安装向导完成安装

3. 首次运行配置：
   - 启动应用程序
   - 按照首次运行向导完成初始设置
   - 配置 Verdaccio 仓库连接信息

## 使用指南

### 快速开始

1. **仓库连接**
   - 点击"添加仓库"按钮
   - 输入 Verdaccio 仓库地址和认证信息
   - 测试连接并保存

2. **包管理**
   - 浏览包列表：主界面默认显示所有包
   - 搜索包：使用顶部搜索栏
   - 发布包：点击"发布"按钮，选择包文件
   - 删除包：选择包后点击"删除"按钮

3. **查看信息**
   - 点击包名查看详细信息
   - 查看版本历史
   - 检视依赖关系

### 常见问题解决

1. **连接问题**
   - 检查网络连接
   - 验证认证信息
   - 确认仓库地址正确性

2. **发布失败**
   - 检查包的完整性
   - 确认版本号未被使用
   - 验证发布权限

3. **性能优化**
   - 定期清理缓存
   - 及时更新客户端
   - 避免同时进行多个大型操作

## 开发指南

### 项目结构

```
lib/
  ├── src/
  │   ├── core/           # 核心功能和工具
  │   │   ├── auth/       # 认证相关
  │   │   ├── network/    # 网络请求
  │   │   └── utils/      # 通用工具
  │   ├── features/       # 功能模块
  │   │   ├── packages/   # 包管理
  │   │   ├── registry/   # 仓库管理
  │   │   └── settings/   # 设置
  │   ├── l10n/          # 本地化文件
  │   └── shared/        # 共享组件
  └── main.dart          # 应用入口
```

### 构建说明

- Windows 测试版构建：
  ```bash
  # 清理构建缓存
  flutter clean

  # 获取依赖
  flutter pub get

  # 构建 Windows 版本
  flutter build windows --release

  # 生成的文件位于
  build/windows/runner/Release/
  ```

### 测试

```bash
# 运行所有测试
flutter test

# 运行特定测试文件
flutter test test/feature_test.dart

# 带覆盖率的测试
flutter test --coverage
```

## 更新日志

### v0.1.0-beta (2024-01)
- 初始测试版本发布
- 基础包管理功能
- Windows 平台支持

## 反馈与支持

- 问题报告：通过 GitHub Issues
- 功能建议：通过 GitHub Discussions
- 技术支持：通过 Email 联系

## 许可证

本项目采用 MIT 许可证。详见 [LICENSE](LICENSE) 文件。 