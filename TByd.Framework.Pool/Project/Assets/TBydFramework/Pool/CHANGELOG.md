# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.5.0] - 2024-04-05
### Added
- 添加池状态持久化系统
  - 实现状态序列化和反序列化
  - 支持运行时状态保存和恢复
  - 添加自动保存机制
  - 实现跨会话状态维护
- 优化池预热系统
  - 支持异步预热操作
  - 实现分帧预热机制
  - 添加预热进度追踪
  - 支持预热优先级设置

### Changed
- 改进池注册表性能
  - 优化查询操作
  - 减少内存分配
- 增强监控系统
  - 添加详细性能指标
  - 优化数据收集机制

### Fixed
- 修复预热时的内存分配问题
- 修复状态恢复时的空引用异常

## [0.4.0] - 2024-04-01
### Added
- 添加池配置系统
  - 实现ScriptableObject配置文件
  - 支持运行时配置修改
  - 添加配置模板功能
  - 实现配置继承机制
- 增强对象池管理
  - 添加池容量自动调节
  - 支持对象存活时间控制
  - 实现池性能监控
  - 添加池事件系统

### Changed
- 优化内存管理
  - 改进对象回收策略
  - 添加内存使用限制
- 增强类型安全性
  - 添加泛型约束
  - 改进错误处理

### Fixed
- 修复多线程访问问题
- 修复对象重用时的状态残留

## [0.3.0] - 2024-03-28
### Added
- 添加PoolMemoryTracker内存追踪系统
  - 支持GameObject内存估算
  - 支持Mesh数据大小计算
  - 支持材质内存估算
  - 提供格式化内存使用显示
- 添加PoolPerformanceProfiler性能分析器
  - 支持操作时间追踪
  - 提供平均性能统计
  - 计算每秒操作数
  - 性能数据采样系统

### Changed
- 优化内存追踪算法
- 改进性能数据采集

## [0.2.0] - 2024-03-22
### Added
- 添加PoolAnalyzer分析工具窗口
  - 性能监控面板
  - 内存使用分析
  - 活动池列表显示
  - 实时数据更新
- 添加调试工具
  - 池状态可视化
  - 性能瓶颈分析
  - 内存泄漏检测

### Changed
- 改进分析工具性能
- 优化数据展示方式

## [0.1.0] - 2024-03-20
### Added
- 添加PoolPreviewWindow预览窗口
  - 3D预览功能
  - 网格显示系统
  - 相机控制系统
  - 实例预览功能
- 添加基础编辑器工具
  - PoolManagerWindow管理器窗口
  - PoolMonitor监视器
  - 编辑器设置系统

### Changed
- 优化编辑器UI布局
- 改进预览系统的性能

### Fixed
- 修复预览窗口中实例清理的问题
- 修复内存追踪时的数值计算错误

