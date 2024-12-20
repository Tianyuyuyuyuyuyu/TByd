# NPM Registry Package Manage Tool

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-blue.svg)]()
[![Flutter](https://img.shields.io/badge/Flutter-3.0+-blue.svg)]()

基于 Flutter 开发的 NPM 私有仓库管理工具，专门用于管理和维护基于 Verdaccio 的私有 NPM 仓库。

## 作者与维护者

- **主要开发者**: TianYu - TByd Team
- **技术支持**: TByd Technical Support
- **联系邮箱**: [tianyulovecars@gmail.com](mailto:tianyulovecars@gmail.com)
- **项目主页**: [GitHub](https://github.com/Tianyuyuyuyuyuyu/TByd/tree/master/TByd.VisualizationTools/NpmRegistryPackageManageTool)

## Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.0-beta.2] - 2024-12-20

### Added
- 新增包上传功能
  - 一键上传功能
  - 实时显示上传进度和状态
  - 命令执行结果实时展示
  - 自动刷新包列表
- 新增包搜索功能
  - 实时搜索
  - 搜索状态保持
- 新增包详情展示
  - README 和 CHANGELOG 查看
  - 包信息完整展示
  - 版本历史记录

### Changed
- 优化用户界面
  - 改进包列表显示
  - 优化版本号展示样式
  - 调整导航栏图标
  - 优化终端输出显示
- 改进包管理功能
  - 使用 displayName 显示包名
  - 添加许可证信息显示
  - 优化作者信息展示

### Fixed
- 修复终端输出乱码问题
- 修复搜索状态丢失问题
- 修复版本徽章加载失败问题
- 修复包列表刷新问题
- 修复中文显示问题

### Security
- 加强身份验证机制
- 改进 JWT Token 处理
- 优化权限控制

## [0.1.0-beta.1] - 2024-12-14

### Added
- 初始版本发布
- NPM 包管理基础功能
  - 连接阿里云 Verdaccio 仓库
  - 包的查看、搜索功能
  - 包版本管理功能
  - 包推送和删除功能
  - 包元数据查看与编辑
  - 版本历史追踪
- 用户界面功能
  - 现代化的 Material Design 界面
  - 响应式布局，支持不同窗口大小
  - 深色/浅色主题支持
  - 可自定义的工作区布局
  - 快捷键支持
- 安全特性
  - 安全的身份验证机制
  - 加密的凭证存储
  - 操作审计日志
  - 权限管理系统
- 多语言支持
  - 中文界面
  - 英文界面
- 性能优化
  - 异步加载机制
  - 智能缓存系统
  - 后台任务队列

### Known Issues
- 首次连接可能需要多次尝试
- 某些特殊字符在包名中可能导致显示问题
- 大型包上传时可能需要较长时间，暂无进度显示
- 在网络不稳定情况下可能需要重试操作

### Security
- 请确保使用最新版本的 Windows 系统
- 建议在内部网络环境中使用
- 定期更新访问凭证
- 遵循最小权限原则
- 建议启用防火墙保护

### Technical Notes
- 基于 Flutter 框架开发
- 支持 Windows 10 及以上版本
- 需要稳定的网络连接
- 系统要求：
  - CPU: 双核及以上
  - 内存: 4GB 及以上
  - 存储: 500MB 可用空间
  - 显示: 1280x720 及以上分辨率

### Upcoming Features
- 包管理增强
  - 断点续传支持
  - 批量操作功能
  - 包依赖关系可视化
  - 版本冲突检测
  - 包大小优化建议
- 用户体验提升
  - 详细的操作日志
  - 自动更新检查
  - 自定义主题支持
  - 多窗口支持
- 高级功能
  - 命令行界面 (CLI) 支持
  - API 集成支持
  - 插件系统
  - 自动化工作流
  - 数据备份与恢复

## 贡献指南

我们欢迎社区贡献，请遵循以下步骤：

1. Fork 项目
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 开启 Pull Request

## 版权说明

Copyright © 2024 TByd Team. All rights reserved.

## 支持

如果您在使用过程中遇到任何问题，请通过以下方式获取支持：

1. 发送邮件至 [tianyulovecars@gmail.com](mailto:tianyulovecars@gmail.com)
2. 在 GitHub 上提交 [Issue](https://github.com/tbyd/npm-registry-manager/issues)
3. 加入我们的技术支持群组（请通过邮件获取邀请链接）

### 反馈建议

我们非常重视您的使用体验和建议。如果您有任何改进意见或功能需求，欢迎通过以下方式告诉我们：

1. 发送详细的建议说明至邮箱
2. 在 GitHub Issues 中使用 "enhancement" 标签提交建议
3. 参与我们的用户调研（定期开展）

### 问题报告指南

当您遇到问题时，请提供以下信息以便我们更好地帮助您：

1. 软件版本号
2. 操作系统版本
3. 问题的详细描述
4. 复现步骤
5. 错误信息或截图（如果有）
6. 期望的正确行为

## 常见问题解答（FAQ）

### 1. 安装与配置

#### Q: 如何安装 NPM Registry Manager？
A: 下载最新的安装包，双击运行安装程序即可。安装过程会自动配置必要的环境。

#### Q: 首次使用需要什么配置？
A: 首次使用需要配置以下信息：
- Verdaccio 服务器地址
- 用户认证信息
- （可选）代理设置

#### Q: 如何更新到最新版本？
A: 软件会自动检查更新，当有新版本时会提示。您也可以在"设置"中手动检查更新。

### 2. 连接问题

#### Q: 为什么连接到服务器失败？
A: 常见原因包括：
- 网络连接不稳定
- 服务器地址错误
- 认证信息过期
- 防火墙限制

#### Q: 如何解决认证失败问题？
A: 请尝试以下步骤：
1. 确认用户名和密码正确
2. 检查认证令牌是否过期
3. 重新登录系统

### 3. 包管理操作

#### Q: 如何上传新的包？
A: 在主界面点击"上传包"按钮，选择包文件或输入包信息，确认后即可上传。

#### Q: 如何处理版本冲突？
A: 系统会自动检测版本冲突，并提供以下选项：
- 覆盖现有版本
- 使用新的版本号
- 取消上传

#### Q: 批量操作如何使用？
A: 选择多个包后，可以进行：
- 批量更新
- 批量删除
- 批量导出

### 4. 性能优化

#### Q: 如何提高大型包的上传速度？
A: 建议：
- 使用有线网络连接
- 确保足够的磁盘空间
- 关闭不必要的后台程序

#### Q: 如何优化本地缓存？
A: 在"设置 > 缓存管理"中可以：
- 设置缓存大小
- 清理过期缓存
- 设置缓存策略

### 5. 安全性

#### Q: 如何确保包的安全性？
A: 系统提供多重安全保障：
- 包完整性校验
- 签名验证
- 漏洞扫描

#### Q: 凭证信息如何保护？
A: 所有敏感信息都经过加密存储，且：
- 定期轮换密钥
- 支持多因素认证
- 本地加密存储

### 6. 其他问题

#### Q: 支持哪些快捷键？
A: 常用快捷键包括：
- Ctrl + U：上传包
- Ctrl + F：搜索
- Ctrl + R：刷新列表
- Ctrl + S：保存更改

#### Q: 数据如何备份？
A: 系统提供多种备份方式：
- 自动定时备份
- 手动备份
- 导出配置

#### Q: 如何迁移到新设备？
A: 使用导出/导入功能：
1. 在原设备导出配置
2. 在新设备安装软件
3. 导入配置文件

注意：FAQ 会定期更新，如果没有找到您需要的答案，请通过上述支持渠道联系我们。