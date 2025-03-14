# 更新日志

本文档记录了 `com.tbyd.core.utils` 包的所有重要变更。

## [0.1.0-preview] - 2023-11-15

### 新增

- 初始版本发布
- 核心工具类：
  - `MathUtils`: 提供数学相关的实用函数
    - SmoothDamp: 平滑过渡函数（支持float、Vector2、Vector3）
    - Remap: 值域重映射函数
    - DirectionToRotation: 方向向量转旋转
    - IsPointInPolygon: 点在多边形内判断
  - `StringUtils`: 提供字符串处理的实用函数
    - IsNullOrWhiteSpace: 检查字符串是否为null或仅包含空白字符
    - GenerateRandomString: 生成指定长度的随机字符串
    - Slugify: 将字符串转换为URL友好的格式
    - Truncate: 将字符串截断为指定长度并添加后缀
    - Split: 按指定分隔符分割字符串并返回数组
  - `TransformExtensions`: 提供Transform组件的扩展方法
    - ResetLocal: 重置本地变换（位置、旋转、缩放）
    - SetLocalX/Y/Z: 设置本地坐标的单个分量
    - GetAllChildren: 获取所有子对象
    - FindOrCreateChild: 查找子对象，如不存在则创建
    - FindRecursive: 递归查找子对象
- 单元测试：
  - MathUtilsTests
  - TransformExtensionsTests
- 示例：
  - StringUtilsExample: 字符串工具使用示例
  - MathUtilsExample: 数学工具使用示例
  - TransformExtensionsExample: Transform扩展方法使用示例
  - 示例场景设置说明

### 已知问题

- TransformExtensions.FindRecursive 在处理大型层次结构时可能性能较低
- MathUtils.IsPointInPolygon 仅支持2D多边形判断 