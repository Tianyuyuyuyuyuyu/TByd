# TByd Core Utils

## 简介

`TByd.Core.Utils` 是一个Unity工具库，提供了一系列常用的工具类和扩展方法，旨在简化Unity开发过程中的常见任务。该库设计为轻量级、高性能且易于使用，可用于各种类型的Unity项目。

## 功能特性

本库包含以下核心工具类：

### MathUtils
- 平滑阻尼插值（支持float、Vector2、Vector3）
- 值重映射
- 方向向量转旋转四元数
- 点-多边形碰撞检测

### StringUtils
- 空字符串检测
- 随机字符串生成
- 字符串截断
- URL友好的Slug生成
- 高效字符串分割

### TransformExtensions
- Transform本地/世界坐标操作
- 子物体查找与创建
- 子物体批量操作
- 递归子物体查找

## 安装说明

### 通过UPM安装（推荐）

1. 打开Unity的Package Manager (Window > Package Manager)
2. 点击左上角的"+"按钮，选择"Add package from git URL..."
3. 输入以下URL：`https://github.com/tbyd/tbyd.core.utils.git#0.0.1-preview`
4. 点击"Add"按钮

### 手动安装

1. 下载此仓库的最新版本
2. 将`Assets/TByd.Core.Utils`文件夹拷贝到你的Unity项目的Assets文件夹中

## 基本用法

### MathUtils示例

```csharp
using TByd.Core.Utils.Runtime;
using UnityEngine;

// 平滑阻尼插值（相机跟随）
private Vector3 _velocity = Vector3.zero;
void Update()
{
    transform.position = MathUtils.SmoothDamp(
        transform.position, 
        target.position, 
        ref _velocity, 
        smoothTime);
}

// 值重映射（输入处理）
float mappedValue = MathUtils.Remap(joystickInput, -1f, 1f, 0f, 100f);
```

### StringUtils示例

```csharp
using TByd.Core.Utils.Runtime;

// 生成随机字符串
string randomId = StringUtils.GenerateRandom(8);

// 创建URL友好的字符串
string urlSlug = StringUtils.ToSlug("Hello World! 示例");  // "hello-world-示例"

// 字符串截断
string truncated = StringUtils.Truncate("这是一段很长的文本", 10);  // "这是一段..."
```

### TransformExtensions示例

```csharp
using TByd.Core.Utils.Runtime.Extensions;
using UnityEngine;

// 重置局部变换
transform.ResetLocal();

// 设置单个坐标分量
transform.SetLocalY(5f);

// 查找或创建子物体
Transform child = transform.FindOrCreateChild("UI_Panel");

// 获取所有激活的子物体
var activeChildren = transform.GetAllChildren(includeInactive: false);
```

## 依赖项

- Unity 2021.3.8f1 或更高版本

## 版本信息

当前版本: 0.0.1-preview

请查看 [CHANGELOG.md](CHANGELOG.md) 获取完整的版本历史。

## 许可证

本项目使用 MIT 许可证。详情请参阅 [LICENSE.md](LICENSE.md) 文件。 