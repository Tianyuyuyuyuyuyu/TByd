# TByd Core Utils

提供一系列常用的工具函数和扩展方法，用于简化Unity游戏开发中的常见操作，提高开发效率。该包被设计为高性能、低GC压力，适用于任何Unity项目。

## 功能特性

- **字符串操作工具**：提供高效的字符串处理方法，减少GC分配
- **数学和几何工具**：扩展Unity数学库，提供更丰富的数学运算功能
- **类型转换和反射助手**：简化反射操作，提供类型安全的转换方法
- **时间和日期工具**：处理游戏内和现实世界的时间计算和格式化
- **随机数生成器**：增强的随机功能，支持各种分布和权重随机
- **文件和路径工具**：简化文件操作和路径处理
- **扩展方法集合**：为Unity常用类型提供实用扩展方法
- **属性验证工具**：参数验证和错误检查工具

## 安装方法

### 通过Unity Package Manager

1. 打开Unity编辑器，选择 Window > Package Manager
2. 点击 "+" 按钮，选择 "Add package from git URL..."
3. 输入 `https://github.com/tbyd/TByd.Core.Utils.git`
4. 点击 "Add" 按钮

### 通过修改manifest.json

1. 打开项目目录下的 `Packages/manifest.json` 文件
2. 在 `dependencies` 部分添加以下内容：
   ```json
   "com.tbyd.core.utils": "https://github.com/tbyd/TByd.Core.Utils.git"
   ```

## 基本用法

### 字符串工具

```csharp
using TByd.Core.Utils;

// 检查字符串是否为空或仅包含空白字符
bool isEmpty = StringUtils.IsNullOrWhiteSpace(myString);

// 生成随机字符串
string randomId = StringUtils.GenerateRandom(8);

// 截断字符串
string truncated = StringUtils.Truncate(longText, 100, "...");

// 高性能分割字符串
foreach (var part in StringUtils.Split(csvData, ','))
{
    // 使用part...
}
```

### 数学工具

```csharp
using TByd.Core.Utils;

// 平滑阻尼插值
transform.position = MathUtils.SmoothDamp(
    transform.position, 
    targetPosition, 
    ref velocity, 
    0.3f
);

// 值重映射
float normalized = MathUtils.Remap(value, 0f, 100f, 0f, 1f);
```

### 扩展方法

```csharp
using TByd.Core.Utils.Extensions;

// Transform扩展
transform.ResetLocal();
transform.SetLocalX(5f);

// GameObject扩展
GameObject child = gameObject.FindOrCreateChild("UI");
child.SetLayerRecursively(LayerMask.NameToLayer("UI"));

// 集合扩展
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
numbers.Shuffle();
```

## 技术规格

- **.NET版本**：.NET Standard 2.1
- **Unity最低版本**：Unity 2021.3.8f1
- **代码覆盖率**：95%+
- **内存分配**：关键方法零GC分配

## 许可证

本项目采用 MIT 许可证 - 详情请参阅 [LICENSE](LICENSE.md) 文件

## 贡献

欢迎通过 Pull Request 贡献代码。请确保遵循项目的代码风格和贡献指南。 