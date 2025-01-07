# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.0.2] - 2025-01-08
### Added
- 添加生命周期行为接口
  - IInitializable：同步初始化接口
  - IAsyncInitializable：异步初始化接口
  - IStartable：同步启动接口
  - IAsyncStartable：异步启动接口
- 增强特性支持
  - OptionalAttribute：可选依赖特性
  - NamedAttribute：命名注入特性
- 添加工具类
  - TypeUtility：类型操作工具
  - DIUtility：依赖注入工具

### Changed
- 优化容器接口
  - 添加TryResolve方法
  - 支持命名解析
  - 增加批量解析功能
- 改进生命周期管理
  - 支持嵌套作用域
  - 优化实例缓存策略
  - 增强资源释放机制

### Fixed
- 修复循环依赖检测
- 改进异常信息提示
- 优化性能和内存使用

### Documentation
- 添加UML类图文档
- 完善API使用说明
- 增加最佳实践指南

## [0.0.1] - 2025-01-07
### Added
- 实现依赖注入核心抽象接口
  - IContainer：容器基本操作接口
  - IContainerBuilder：容器构建接口
  - IRegistration：注册信息接口
  - ILifetime：生命周期管理接口
  - ILifetimeScope：作用域管理接口
- 添加InjectAttribute用于依赖注入标记
- 实现异常处理体系（DIException、ResolutionException、RegistrationException）
- 支持多种生命周期类型（Transient、Scoped、Singleton） 