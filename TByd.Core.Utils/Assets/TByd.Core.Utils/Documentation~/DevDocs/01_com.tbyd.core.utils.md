# com.tbyd.core.utils 开发文档

## 包概述

`com.tbyd.core.utils` 包提供了一系列常用的工具函数和扩展方法，用于简化Unity游戏开发中的常见操作，提高开发效率。该包被设计为高性能、低GC压力，适用于任何Unity项目。本包是TByd UPM包体系中最基础的包之一，许多其他包都依赖于它。

## 功能规格

### 1. 核心功能

1. **字符串操作工具**：提供高效的字符串处理方法，减少GC分配
2. **数学和几何工具**：扩展Unity数学库，提供更丰富的数学运算功能
3. **类型转换和反射助手**：简化反射操作，提供类型安全的转换方法
4. **时间和日期工具**：处理游戏内和现实世界的时间计算和格式化
5. **随机数生成器**：增强的随机功能，支持各种分布和权重随机
6. **文件和路径工具**：简化文件操作和路径处理
7. **扩展方法集合**：为Unity常用类型提供实用扩展方法
8. **属性验证工具**：参数验证和错误检查工具

### 2. 技术规格

- **.NET版本**：.NET Standard 2.1
- **Unity最低版本**：Unity 2021.3.8f1
- **代码覆盖率目标**：95%+
- **内存分配要求**：关键方法零GC分配
- **异常处理**：关键方法处理所有可能的异常，提供明确的错误信息
- **线程安全性**：明确标记线程安全的方法，提供线程安全版本

### 3. 性能目标

- 字符串操作比System.String原生方法至少减少50%的GC分配
- 数学运算性能至少与Unity.Mathematics匹配
- 所有工具方法的性能开销不应成为应用程序的瓶颈

## 架构设计

### 1. 包结构

```
com.tbyd.core.utils/
├── Runtime/
│   ├── StringUtils.cs
│   ├── MathUtils.cs
│   ├── ReflectionUtils.cs
│   ├── TimeUtils.cs
│   ├── RandomUtils.cs
│   ├── FileUtils.cs
│   ├── ValidationUtils.cs
│   ├── Extensions/
│   │   ├── TransformExtensions.cs
│   │   ├── GameObjectExtensions.cs
│   │   ├── VectorExtensions.cs
│   │   ├── CollectionExtensions.cs
│   │   ├── ComponentExtensions.cs
│   │   └── StringExtensions.cs
│   └── Attributes/
│       ├── ReadOnlyAttribute.cs
│       └── ConditionalFieldAttribute.cs
├── Editor/
│   └── PropertyDrawers/
│       ├── ReadOnlyPropertyDrawer.cs
│       └── ConditionalFieldDrawer.cs
└── Tests/
    ├── StringUtilsTests.cs
    ├── MathUtilsTests.cs
    └── ... (其他测试文件)
```

### 2. 类设计

#### 2.1 基本原则

- **静态工具类**：主要工具函数集中在静态类中，便于调用
- **扩展方法**：为Unity现有类型提供自然的API扩展
- **模块化**：按功能领域清晰分割不同工具集
- **无状态**：工具函数应无状态，避免副作用
- **异常安全**：公开API提供适当的参数验证和错误处理

#### 2.2 核心类

##### StringUtils

```csharp
namespace TByd.Core.Utils
{
    /// <summary>
    /// 提供高性能、低GC压力的字符串操作工具
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// 检查字符串是否为空或仅包含空白字符
        /// </summary>
        public static bool IsNullOrWhiteSpace(string value);

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        public static string GenerateRandom(int length, bool includeSpecialChars = false);

        /// <summary>
        /// 将字符串转换为URL友好的slug格式
        /// </summary>
        public static string ToSlug(string value);

        /// <summary>
        /// 截断字符串到指定长度，添加省略号
        /// </summary>
        public static string Truncate(string value, int maxLength, string suffix = "...");

        /// <summary>
        /// 高性能分割字符串，减少GC压力
        /// </summary>
        public static StringSplitEnumerator Split(string value, char separator);

        // 更多字符串工具方法...
    }
}
```

##### MathUtils

```csharp
namespace TByd.Core.Utils
{
    /// <summary>
    /// 提供扩展的数学和几何运算工具
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// 平滑阻尼插值，适用于相机跟随等场景
        /// </summary>
        public static float SmoothDamp(float current, float target, 
                                       ref float velocity, float smoothTime, 
                                       float maxSpeed = Mathf.Infinity, float deltaTime = -1f);

        /// <summary>
        /// 将值重映射到新范围
        /// </summary>
        public static float Remap(float value, float fromMin, float fromMax, 
                                  float toMin, float toMax);

        /// <summary>
        /// 计算方向向量的四元数旋转
        /// </summary>
        public static Quaternion DirectionToRotation(Vector3 direction, Vector3 up = default);

        /// <summary>
        /// 检查点是否在多边形内部
        /// </summary>
        public static bool IsPointInPolygon(Vector2 point, Vector2[] polygon);

        // 更多数学工具方法...
    }
}
```

##### RandomUtils

```csharp
namespace TByd.Core.Utils
{
    /// <summary>
    /// 提供增强的随机功能
    /// </summary>
    public static class RandomUtils
    {
        /// <summary>
        /// 获取随机布尔值，可指定true的概率
        /// </summary>
        public static bool Bool(float trueChance = 0.5f);

        /// <summary>
        /// 根据权重随机选择数组中的一个元素
        /// </summary>
        public static T WeightedRandom<T>(T[] items, float[] weights);

        /// <summary>
        /// 生成符合正态分布的随机值
        /// </summary>
        public static float Gaussian(float mean = 0f, float standardDeviation = 1f);

        /// <summary>
        /// 生成随机HSV颜色
        /// </summary>
        public static Color ColorHSV(float saturationMin = 0f, float saturationMax = 1f,
                                    float valueMin = 0f, float valueMax = 1f);

        /// <summary>
        /// 生成指定长度的随机ID
        /// </summary>
        public static string GenerateId(int length, bool includeSpecialChars = false);

        // 更多随机工具方法...
    }
}
```

#### 2.3 扩展方法设计

扩展方法将遵循与Unity API相似的风格，提供自然、直观的接口：

```csharp
namespace TByd.Core.Utils.Extensions
{
    /// <summary>
    /// Transform组件的扩展方法
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// 重置Transform的本地位置、旋转和缩放
        /// </summary>
        public static void ResetLocal(this Transform transform);

        /// <summary>
        /// 设置本地位置的x坐标，保持y和z不变
        /// </summary>
        public static void SetLocalX(this Transform transform, float x);

        /// <summary>
        /// 设置本地位置的y坐标，保持x和z不变
        /// </summary>
        public static void SetLocalY(this Transform transform, float y);

        /// <summary>
        /// 设置本地位置的z坐标，保持x和y不变
        /// </summary>
        public static void SetLocalZ(this Transform transform, float z);

        /// <summary>
        /// 获取所有子物体（包括非激活的）
        /// </summary>
        public static Transform[] GetAllChildren(this Transform transform, bool includeInactive = true);

        /// <summary>
        /// 销毁所有子物体
        /// </summary>
        public static void DestroyAllChildren(this Transform transform, bool immediate = false);

        // 更多Transform扩展方法...
    }
}
```

### 3. API设计原则

1. **简单直观**：方法名清晰表达功能，减少学习成本
2. **一致性**：保持与Unity API风格一致，熟悉感
3. **可发现性**：相关功能分组，便于查找和使用
4. **参数最小化**：核心参数必选，额外参数有合理默认值
5. **方法重载**：提供多种参数组合的重载方法
6. **返回值一致性**：类似操作有一致的返回值约定
7. **异常处理**：明确文档API可能抛出的异常
8. **性能说明**：在文档中标注性能特性和内存分配情况

## 实现细节

### 1. 字符串工具实现策略

为减少GC压力，字符串操作将采用以下策略：

- 使用`StringBuilder`处理字符串拼接
- 实现基于`Span<T>`的无分配字符串分割
- 提供字符串池缓存常用字符串
- 使用`StringComparer`优化字符串比较

示例实现：

```csharp
public static StringSplitEnumerator Split(string value, char separator)
{
    return new StringSplitEnumerator(value.AsSpan(), separator);
}

// 零分配字符串分割器
public ref struct StringSplitEnumerator
{
    private ReadOnlySpan<char> _str;
    private readonly char _separator;

    public StringSplitEnumerator(ReadOnlySpan<char> str, char separator)
    {
        _str = str;
        _separator = separator;
        Current = default;
    }

    public StringSplitEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
        var span = _str;
        if (span.Length == 0) return false;

        var index = span.IndexOf(_separator);
        if (index == -1)
        {
            Current = span;
            _str = ReadOnlySpan<char>.Empty;
            return true;
        }

        Current = span.Slice(0, index);
        _str = span.Slice(index + 1);
        return true;
    }

    public ReadOnlySpan<char> Current { get; private set; }
}
```

### 2. 反射工具实现策略

反射操作通常会带来性能开销，我们将采用以下策略优化：

- 缓存反射结果，避免重复获取
- 使用表达式树生成动态调用
- 提供AOT兼容的反射替代方案
- 条件编译支持IL2CPP平台

示例实现：

```csharp
private static readonly Dictionary<(Type, string), PropertyInfo> PropertyCache = 
    new Dictionary<(Type, string), PropertyInfo>();

public static PropertyInfo GetCachedProperty(Type type, string propertyName)
{
    var key = (type, propertyName);
    if (PropertyCache.TryGetValue(key, out var property))
    {
        return property;
    }

    property = type.GetProperty(propertyName, 
                BindingFlags.Public | BindingFlags.NonPublic | 
                BindingFlags.Instance | BindingFlags.Static);
    
    if (property != null)
    {
        PropertyCache[key] = property;
    }
    
    return property;
}
```

### 3. 数学工具实现策略

数学运算将重点关注精度和性能：

- 避免不必要的类型转换
- 使用查表法优化三角函数计算
- 利用SIMD指令加速向量运算（在支持平台）
- 提供精确和近似两种版本的算法

示例实现：

```csharp
// 高性能平滑插值算法
public static float SmoothDamp(float current, float target, ref float velocity, 
                             float smoothTime, float maxSpeed = Mathf.Infinity, 
                             float deltaTime = -1f)
{
    if (deltaTime < 0f)
        deltaTime = Time.deltaTime;
    
    smoothTime = Mathf.Max(0.0001f, smoothTime);
    float omega = 2f / smoothTime;

    float x = omega * deltaTime;
    float exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
    
    float change = current - target;
    float originalTo = target;
    
    // 限制最大速度
    float maxChange = maxSpeed * smoothTime;
    change = Mathf.Clamp(change, -maxChange, maxChange);
    target = current - change;
    
    float temp = (velocity + omega * change) * deltaTime;
    velocity = (velocity - omega * temp) * exp;
    float output = target + (change + temp) * exp;
    
    // 防止过冲
    if (originalTo - current > 0f == output > originalTo)
    {
        output = originalTo;
        velocity = (output - originalTo) / deltaTime;
    }
    
    return output;
}
```

### 4. 随机数工具实现策略

提供高质量、多样化的随机数生成：

- 使用更现代的随机数生成器
- 实现常见概率分布
- 线程安全的随机数实例
- 允许设置随机种子，便于重现结果

示例实现：

```csharp
private static System.Random _random = new System.Random();
private static readonly object _lock = new object();

// 线程安全的随机范围函数
public static int Range(int minInclusive, int maxExclusive)
{
    lock (_lock)
    {
        return _random.Next(minInclusive, maxExclusive);
    }
}

// 高斯分布随机数生成
public static float Gaussian(float mean = 0f, float standardDeviation = 1f)
{
    lock (_lock)
    {
        float u1 = 1.0f - (float)_random.NextDouble();
        float u2 = 1.0f - (float)_random.NextDouble();
        
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * 
                              Mathf.Sin(2.0f * Mathf.PI * u2);
        
        return mean + standardDeviation * randStdNormal;
    }
}
```

## 测试计划

### 1. 单元测试

单元测试将覆盖所有公共API和关键内部函数：

- **验证功能正确性**：每个方法的预期行为
- **边界条件测试**：极端输入值处理
- **异常测试**：确保适当抛出和处理异常
- **性能测试**：验证关键方法的性能特征

### 2. 测试组织

```
Tests/
├── StringUtilsTests.cs
├── MathUtilsTests.cs  
├── ReflectionUtilsTests.cs
├── TimeUtilsTests.cs
├── RandomUtilsTests.cs
├── FileUtilsTests.cs
├── ValidationUtilsTests.cs
├── Extensions/
│   ├── TransformExtensionsTests.cs
│   ├── GameObjectExtensionsTests.cs
│   ├── VectorExtensionsTests.cs
│   ├── CollectionExtensionsTests.cs
│   ├── ComponentExtensionsTests.cs
│   └── StringExtensionsTests.cs
└── Performance/
    ├── StringPerformanceTests.cs
    ├── MathPerformanceTests.cs
    └── ReflectionPerformanceTests.cs
```

### 3. 测试方法命名约定

使用描述性的测试方法名称，遵循`Method_Scenario_ExpectedResult`模式：

```csharp
[Test]
public void Truncate_StringLongerThanMaxLength_ReturnsCorrectlyTruncatedString()
{
    string result = StringUtils.Truncate("Hello World", 5);
    Assert.AreEqual("Hello...", result);
}

[Test]
public void Truncate_StringShorterThanMaxLength_ReturnsOriginalString()
{
    string result = StringUtils.Truncate("Hi", 5);
    Assert.AreEqual("Hi", result);
}

[Test]
public void Truncate_NullString_ThrowsArgumentNullException()
{
    Assert.Throws<ArgumentNullException>(() => StringUtils.Truncate(null, 5));
}
```

### 4. 性能测试基准

为关键方法定义性能基准，确保优化目标：

```csharp
[Test, Performance]
public void StringSplit_Performance_ShouldBeFasterThanStandardSplit()
{
    string testString = "This,is,a,test,string,with,many,commas";
    
    Measure.Method(() => 
    {
        foreach (var part in StringUtils.Split(testString, ','))
        {
            // 使用part避免编译器优化
            _ = part.Length;
        }
    })
    .WarmupCount(5)
    .MeasurementCount(100)
    .GC()
    .SetUp(() => { /* 准备工作 */ })
    .Run();
    
    Measure.Method(() => 
    {
        string[] parts = testString.Split(',');
        foreach (string part in parts)
        {
            // 使用part避免编译器优化
            _ = part.Length;
        }
    })
    .WarmupCount(5)
    .MeasurementCount(100)
    .GC()
    .SetUp(() => { /* 准备工作 */ })
    .Run();
    
    // 确保我们的实现至少快20%并且减少50%的GC分配
    Assert.That(Measure.GetCurrentResult().SampleGroups.First(x => x.Name == "Time").Median, 
                Is.LessThan(Measure.GetCurrentResult().SampleGroups.Last(x => x.Name == "Time").Median * 0.8));
                
    Assert.That(Measure.GetCurrentResult().SampleGroups.First(x => x.Name == "Allocated Memory").Median,
                Is.LessThan(Measure.GetCurrentResult().SampleGroups.Last(x => x.Name == "Allocated Memory").Median * 0.5));
}
```

## 依赖项

本包作为基础工具包，尽量减少外部依赖。当前依赖项：

- **Unity Core Modules**：Unity 2021.3.8f1 或更高版本
- **.NET Standard 2.1**：提供现代C#语言功能支持

## 兼容性考虑

### 1. 平台兼容性

包需要在所有Unity支持的平台上正常工作，特别注意：

- **WebGL**：避免使用不兼容的线程操作
- **IL2CPP**：避免依赖JIT编译的高级反射技术
- **移动平台**：优化计算密集型操作

### 2. Unity版本兼容性

- 保证与Unity 2021.3.8f1及更高版本兼容
- 使用`#if UNITY_X_Y_OR_NEWER`预处理指令处理版本差异
- 通过Unity PackageManager的versionDefines定义条件编译符号

### 3. .NET版本兼容性

- 利用.NET Standard 2.1特性提高性能
- 提供向后兼容版本的关键算法

## 优化策略

### 1. 性能优化重点

- **热路径优化**：识别和优化频繁调用的方法
- **内存分配减少**：使用对象池、Span<T>和值类型减少GC压力
- **算法选择**：选择适合游戏开发的权衡算法
- **并行计算**：适当使用Job System处理计算密集型任务

### 2. 代码大小优化

- 避免代码膨胀
- 合理组织类和方法，去除冗余
- 使用条件编译减少移动平台的代码体积

## 使用示例

### 1. 字符串工具使用示例

```csharp
public class StringUtilsExample : MonoBehaviour
{
    void Start()
    {
        // 生成随机ID
        string randomId = StringUtils.GenerateRandom(8);
        Debug.Log($"Random ID: {randomId}");
        
        // 转换为URL友好格式
        string originalTitle = "Hello World! This is a Test";
        string slug = StringUtils.ToSlug(originalTitle);
        Debug.Log($"Slug: {slug}"); // 输出: "hello-world-this-is-a-test"
        
        // 截断字符串
        string description = "This is a very long description that needs to be truncated";
        string truncated = StringUtils.Truncate(description, 20);
        Debug.Log($"Truncated: {truncated}"); // 输出: "This is a very long..."
        
        // 高性能分割字符串
        string tags = "unity,gamedev,indiedev,gaming";
        foreach (var tag in StringUtils.Split(tags, ','))
        {
            Debug.Log($"Tag: {tag.ToString()}");
        }
    }
}
```

### 2. 数学工具使用示例

```csharp
public class MathUtilsExample : MonoBehaviour
{
    public Transform target;
    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        // 平滑插值移动
        transform.position = MathUtils.SmoothDamp(
            transform.position, 
            target.position, 
            ref velocity, 
            0.3f, 
            10f
        );
        
        // 重映射值
        float normalizedTime = MathUtils.Remap(
            Time.time % 5f, // 值
            0f, 5f,        // 原始范围
            0f, 1f         // 目标范围
        );
        
        // 使用重映射值控制缩放
        transform.localScale = Vector3.one * Mathf.Sin(normalizedTime * Mathf.PI);
        
        // 朝向目标方向旋转
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = MathUtils.DirectionToRotation(direction);
    }
}
```

### 3. 扩展方法使用示例

```csharp
using TByd.Core.Utils.Extensions;

public class ExtensionsExample : MonoBehaviour
{
    void Start()
    {
        // Transform扩展方法
        transform.ResetLocal();
        transform.SetLocalX(5f);
        
        // 清理所有子物体
        transform.DestroyAllChildren();
        
        // GameObject扩展方法
        GameObject child = this.gameObject.FindOrCreateChild("UI");
        child.SetLayerRecursively(LayerMask.NameToLayer("UI"));
        
        // 集合扩展方法
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        numbers.Shuffle();
        
        foreach (var batch in numbers.Batch(2))
        {
            Debug.Log($"Batch: {string.Join(", ", batch)}");
        }
    }
}
```

## 文档计划

### 1. API文档

- 所有公共API提供完整的XML文档注释
- 包括参数说明、返回值解释、异常情况
- 使用示例代码说明典型用法

### 2. 教程和指南

- **快速入门**：核心功能简介和基本用法
- **最佳实践**：性能优化、适当使用场景
- **深入指南**：复杂功能的详细教程
- **示例项目**：展示在真实场景中的应用

### 3. 发布计划

| 里程碑 | 版本 | 内容 | 预计日期 |
|---|---|---|---|
| 概念验证 | 0.1.0-preview | 核心功能原型：字符串工具、数学工具的基础实现 | 2024年Q2 |
| 内部测试版 | 0.2.0-preview | 完善核心功能，添加基本扩展方法，初步单元测试 | 2024年Q3初 |
| 功能完善版 | 0.3.0-preview | 添加反射工具、时间工具和随机数工具 | 2024年Q3中 |
| 公开预览版 | 0.4.0-preview | 完整功能集，API稳定性提升，测试覆盖率达到80% | 2024年Q3末 |
| 候选发布版 | 0.5.0-rc.1 | 性能优化，完整文档和示例，API冻结 | 2024年Q4初 |
| 候选发布版2 | 0.5.0-rc.2 | 修复RC1反馈的问题，最终性能调优 | 2024年Q4中 |
| 正式版本 | 1.0.0 | 生产就绪版本，修复预览期反馈的问题 | 2024年Q4末 |
| 功能更新 | 1.1.0 | 基于用户反馈的功能增强和性能优化 | 2025年Q1 |

> 注：
> - 预览版(preview)：功能不完整，API可能变动，适合内部测试
> - 候选发布版(rc)：功能完整，API已冻结，仅修复问题，适合受控环境测试
> - 版本号遵循语义化版本控制(SemVer)规范，确保版本升级路径清晰

## 开发团队

- **主要开发者**: 1名（负责整体架构设计和核心功能实现）
- **外部贡献者**: 根据需要邀请社区贡献特定功能
- **自动化测试**: 使用GitHub Actions实现CI/CD和自动化测试
- **文档**: 由主要开发者维护，使用文档生成工具自动化处理

### 资源分配

| 阶段 | 开发时间分配 | 主要任务 |
|---|---|---|
| 设计阶段 | 20% | 架构设计、API规划、性能目标制定 |
| 实现阶段 | 40% | 核心功能编码、单元测试编写 |
| 测试阶段 | 20% | 性能测试、边界测试、跨平台验证 |
| 文档阶段 | 10% | API文档、示例代码、使用指南 |
| 维护阶段 | 10% | 问题修复、社区反馈处理 |

### 开发工作流

1. **功能规划**: 使用GitHub Issues跟踪功能请求和计划
2. **开发**: 遵循TDD原则，先编写测试再实现功能
3. **审查**: 通过自我审查和自动化工具确保代码质量
4. **测试**: 自动化测试确保功能正确性和性能达标
5. **发布**: 使用GitHub Actions自动化构建和发布流程

## 风险评估

| 风险 | 可能性 | 影响 | 缓解策略 |
|---|---|---|---|
| 性能不达标 | 中 | 高 | 早期性能测试、基准比较、优化关键路径 |
| API设计不合理 | 低 | 高 | 前期充分调研、内部评审、早期用户反馈 |
| 跨平台兼容性问题 | 中 | 中 | 全平台测试、条件编译、平台特定实现 |
| 依赖版本冲突 | 低 | 中 | 最小化依赖、明确版本范围、兼容性测试 |
| 单人开发资源有限 | 高 | 中 | 合理规划优先级、自动化测试、分阶段发布 |
| 文档不完善 | 中 | 中 | 使用文档生成工具、代码即文档、示例驱动开发 |
| 进度延迟 | 中 | 中 | 设置缓冲时间、优先实现核心功能、灵活调整范围 |

## 维护计划

1. **版本更新策略**:
   - 遵循语义化版本控制规范
   - 严格维护API向后兼容性
   - 明确的废弃策略和过渡期
   - 每季度至少一次小版本更新

2. **Bug修复策略**:
   - 关键错误一周内修复
   - 常规问题按优先级排序处理
   - 使用GitHub Issues跟踪和管理问题
   - 鼓励社区提交问题报告和修复建议

3. **长期支持计划**:
   - 主要版本(1.x)提供12个月支持期
   - 每月安全和关键bug修复更新
   - 兼容性维护和适配新Unity版本
   - 提供迁移指南支持版本升级

4. **社区参与**:
   - 欢迎通过Pull Request贡献代码
   - 提供贡献指南和代码风格规范
   - 定期审查和合并社区贡献
   - 对活跃贡献者提供适当认可 