# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

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