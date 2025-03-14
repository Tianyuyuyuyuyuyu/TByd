# 🧰 TByd.Core.Utils 基础工具示例

<div align="center">
  <br>
  <em>📊 通过实际案例学习工具的使用方法 📊</em>
</div>

<div align="center">

![Unity兼容性](https://img.shields.io/badge/Unity-2021.3.8f1+-brightgreen)
![示例数量](https://img.shields.io/badge/示例数量-3-blue)
![状态](https://img.shields.io/badge/状态-活跃-success)

</div>

## 📑 目录结构

```
BasicUtils/
├── 📁 Scenes/
│   └── 📄 BasicToolsDemo.unity     # 主示例场景
├── 📁 Scripts/
│   ├── 📄 MathUtilsExample.cs      # 数学工具演示
│   ├── 📄 StringUtilsExample.cs    # 字符串工具演示
│   └── 📄 TransformExtensionsExample.cs  # 变换扩展演示
├── 📁 Prefabs/
│   ├── 📄 DemoPanel.prefab         # 演示UI面板
│   └── 📄 ExampleObjects.prefab    # 演示对象容器
└── 📄 README.md                    # 当前文档
```

## 🧩 核心演示模块

本示例项目展示了 `TByd.Core.Utils` 包中三个核心工具类的实际应用场景，帮助您快速掌握它们的使用方法。

### 🧮 MathUtils 演示

<table>
<tr>
<th width="40%">功能</th>
<th width="60%">代码示例</th>
</tr>
<tr>
<td>

#### 🔄 平滑阻尼插值

实现平滑的移动过渡效果，避免生硬的线性插值，常用于:
- 相机跟随
- UI元素动画
- 物体移动

</td>
<td>

```csharp
using TByd.Core.Utils.Runtime;
using UnityEngine;

/// <summary>
/// 演示MathUtils类的核心功能
/// </summary>
public class MathUtilsExample : MonoBehaviour
{
    [Header("插值演示")]
    public Transform targetObject;
    public float smoothTime = 0.3f;
    
    [Header("重映射演示")]
    public float inputMin = 0f;
    public float inputMax = 100f;
    public float outputMin = 0f;
    public float outputMax = 1f;
    
    private Vector3 _velocity = Vector3.zero;
    
    void Update()
    {
        // 演示1: 平滑阻尼插值
        if (targetObject != null)
        {
            transform.position = MathUtils.SmoothDamp(
                transform.position, 
                targetObject.position, 
                ref _velocity, 
                smoothTime);
        }
        
        // 演示2: 值范围重映射
        float inputValue = Mathf.Sin(Time.time) * 50f + 50f; // 产生0-100之间的值
        float mappedValue = MathUtils.Remap(
            inputValue, 
            inputMin, inputMax, 
            outputMin, outputMax);
            
        // 使用映射后的值来改变对象缩放
        transform.localScale = Vector3.one * mappedValue;
        
        // 演示3: 方向向量转旋转
        Vector3 direction = (targetObject != null) 
            ? (targetObject.position - transform.position).normalized 
            : Vector3.forward;
            
        transform.rotation = MathUtils.DirectionToRotation(direction);
        
        // 演示4: 点在多边形内检测
        Vector3[] polygonPoints = new Vector3[]
        {
            new Vector3(-5, 0, -5),
            new Vector3(5, 0, -5),
            new Vector3(5, 0, 5),
            new Vector3(-5, 0, 5)
        };
        
        bool isInside = MathUtils.IsPointInPolygon(
            new Vector2(transform.position.x, transform.position.z),
            System.Array.ConvertAll(polygonPoints, p => new Vector2(p.x, p.z)));
            
        // 根据是否在多边形内改变颜色
        GetComponent<Renderer>().material.color = isInside 
            ? Color.green 
            : Color.red;
    }
}
```

</td>
</tr>
<tr>
<td>

#### 📊 值重映射

将值从一个范围线性映射到另一个范围，适用于:
- UI滑动条数值转换
- 角度转弧度
- 输入设备数值标准化

</td>
<td>

```csharp
// 将健康值(0-100)映射到进度条填充比例(0-1)
float healthBarFill = MathUtils.Remap(
    currentHealth,  // 当前值
    0f,            // 原始最小值
    maxHealth,     // 原始最大值
    0f,            // 目标最小值
    1f);           // 目标最大值

healthBar.fillAmount = healthBarFill;
```

</td>
</tr>
<tr>
<td>

#### 🧭 方向向量转旋转

快速将方向向量转换为对应的旋转，适用于:
- 角色朝向目标
- 投射物朝向
- 箭头指向

</td>
<td>

```csharp
// 计算朝向目标的方向向量
Vector3 direction = (targetPosition - transform.position).normalized;

// 转换为旋转
Quaternion rotation = MathUtils.DirectionToRotation(
    direction,
    upVector: Vector3.up);
    
// 应用旋转
transform.rotation = rotation;
```

</td>
</tr>
<tr>
<td>

#### 📐 点在多边形内检测

高效检测一个点是否在多边形内部，适用于:
- 自定义区域触发
- 地图边界检测
- 区域限制

</td>
<td>

```csharp
// 多边形顶点数组(2D多边形)
Vector2[] polygonVertices = new[] {
    new Vector2(0, 0),
    new Vector2(10, 0),
    new Vector2(10, 10),
    new Vector2(0, 10)
};

// 检测点是否在多边形内
bool isInside = MathUtils.IsPointInPolygon(
    playerPosition,
    polygonVertices);
    
if (isInside) {
    Debug.Log("玩家在安全区域内");
}
```

</td>
</tr>
</table>

### 📝 StringUtils 演示


<table>
<tr>
<th width="40%">功能</th>
<th width="60%">代码示例</th>
</tr>
<tr>
<td>

#### 🔍 空字符串检查

安全检查字符串是否为null或空值，适用于:
- 表单验证
- 用户输入检查
- 配置检验

</td>
<td>

```csharp
// 检查用户名是否有效
public bool ValidateUsername(string username)
{
    if (StringUtils.IsNullOrWhiteSpace(username))
    {
        ShowError("用户名不能为空");
        return false;
    }
    return true;
}
```

</td>
</tr>
<tr>
<td>

#### 🎲 随机字符串生成

生成指定长度的随机字符串，适用于:
- 临时密码生成
- 唯一ID创建
- 会话令牌

</td>
<td>

```csharp
// 生成安全的临时密码
string tempPassword = StringUtils.GenerateRandom(
    length: 12,              // 12个字符
    includeSpecialChars: true // 包含特殊字符
);

// 创建会话ID (仅字母数字)
string sessionId = StringUtils.GenerateRandom(32);

Debug.Log($"临时密码: {tempPassword}");
```

</td>
</tr>
<tr>
<td>

#### 🔗 URL友好的Slug生成

将文本转换为URL友好格式，适用于:
- 网址生成
- 文件命名
- 标识符创建

</td>
<td>

```csharp
// 将标题转换为URL slug
string articleTitle = "Unity 3D 游戏开发教程 2025!";
string urlSlug = StringUtils.ToSlug(articleTitle);

// 结果: "unity-3d-游戏开发教程-2025"
// 注意:保留了中文字符

string url = $"https://example.com/articles/{urlSlug}";
```

</td>
</tr>
<tr>
<td>

#### ✂️ 字符串截断

智能截断长文本并添加后缀，适用于:
- 消息预览
- UI文本显示
- 摘要生成

</td>
<td>

```csharp
// 原始长消息
string message = "这是一条非常长的消息，需要在UI中显示，但是UI空间有限，所以需要进行智能截断处理...";

// 截断为50个字符，添加省略号
string preview = StringUtils.Truncate(
    message,
    maxLength: 50,
    suffix: "..."
);

// 在UI中显示截断后的文本
messagePreview.text = preview;
```

</td>
</tr>
<tr>
<td>

#### 📋 高效字符串分割

低GC压力的字符串分割，适用于:
- 配置文件解析
- CSV数据处理
- 命令解析

</td>
<td>

```csharp
// 要分析的CSV行
string csvLine = "John,Doe,35,New York,Engineer";

// 使用最小内存分配分割字符串
string[] parts = StringUtils.Split(
    csvLine,
    separator: ','
);

// 使用分割结果
string firstName = parts[0];
string lastName = parts[1];
int age = int.Parse(parts[2]);
```

</td>
</tr>
</table>

### 🎮 TransformExtensions 演示


<table>
<tr>
<th width="40%">功能</th>
<th width="60%">代码示例</th>
</tr>
<tr>
<td>

#### 🔄 重置本地变换

一行代码重置对象的本地变换，适用于:
- UI元素重置
- 预制体初始化
- 对象池重用

</td>
<td>

```csharp
// 重置对象到默认状态
// 位置归零，旋转归零，缩放为1
transform.ResetLocal();

// 链式调用 - 重置后再调整Y轴
transform
    .ResetLocal()
    .SetLocalY(2f);
```

</td>
</tr>
<tr>
<td>

#### 📊 单独设置坐标分量

分别设置变换的单个坐标轴，适用于:
- UI元素对齐
- 物体高度调整
- 2.5D游戏开发

</td>
<td>

```csharp
// 仅修改Y轴高度，保持XZ不变
transform.SetY(groundHeight + 1f);

// 在本地坐标系中设置X坐标
uiElement.SetLocalX(Screen.width * 0.5f);

// 多轴链式调用
transform
    .SetLocalX(5f)
    .SetLocalZ(3f);
```

</td>
</tr>
<tr>
<td>

#### 📦 子物体管理

高效管理场景层级中的子物体，适用于:
- UI系统构建
- 动态场景组织
- 对象组管理

</td>
<td>

```csharp
// 查找或创建UI面板
Transform uiPanel = transform.FindOrCreateChild("UI_Panel");

// 获取所有子项(可选择包含非激活对象)
var children = transform.GetAllChildren(includeInactive: true);

// 销毁所有子物体
transform.DestroyAllChildren();

// 获取子对象数量
int count = transform.GetChildCount();
```

</td>
</tr>
<tr>
<td>

#### 🌳 递归查找子物体

深度优先搜索查找子物体，适用于:
- 复杂UI层级
- 预制体引用获取
- 场景对象查找

</td>
<td>

```csharp
// 递归查找深层次嵌套的对象
Transform healthBar = transform.FindRecursive("PlayerHealthBar");

if (healthBar != null)
{
    // 找到了目标对象
    healthBar.gameObject.SetActive(true);
}
else 
{
    Debug.LogWarning("未找到健康条UI元素");
}
```

</td>
</tr>
</table>

## 🚀 如何使用示例

<table>
<tr>
<th>步骤</th>
<th>操作说明</th>
</tr>
<tr>
<td width="20%">

### 1️⃣ 导入示例

</td>
<td width="80%">

1. 在Unity中打开 **Window > Package Manager**
2. 选择 **TByd.Core.Utils** 包
3. 在包详情中，找到 **Samples** 部分
4. 点击 **Import** 按钮导入 **BasicUtils** 示例

</td>
</tr>
<tr>
<td>

### 2️⃣ 打开示例场景

</td>
<td>

1. 导航到项目视图中的 **Assets/Samples/TByd.Core.Utils/[版本号]/BasicUtils/Scenes**
2. 双击 **BasicToolsDemo.unity** 打开示例场景

</td>
</tr>
<tr>
<td>

### 3️⃣ 运行示例

</td>
<td>

1. 点击 Unity 编辑器顶部的 **Play** 按钮
2. 使用示例场景中的UI与不同工具进行交互
3. 查看控制台输出了解更多细节

</td>
</tr>
<tr>
<td>

### 4️⃣ 学习代码

</td>
<td>

1. 打开 **Scripts** 文件夹中的示例脚本
2. 研究每个示例脚本的实现
3. 查看注释了解最佳实践和性能考虑

</td>
</tr>
</table>

## 🔍 自定义与扩展

这些示例是入门的基础，您可以通过以下方式进一步探索:

1. **修改参数值** - 尝试调整示例中的参数，观察结果变化
2. **组合多个工具** - 在同一场景中组合使用不同的工具类
3. **创建游戏原型** - 使用这些工具快速构建游戏原型
4. **添加新功能** - 基于示例代码扩展实现自己的工具方法

## 💡 获取帮助

- 查阅 [使用手册](../../Documentation~/使用手册.md) 了解更详细的API文档
- 阅读 [使用入门](../../Documentation~/使用入门.md) 获取快速入门指南
- 如有问题，请通过GitHub Issues或邮件联系我们

---

<div align="center">
  <img src="https://github.com/Tianyuyuyuyuyuyu/TByd/blob/master/tbyd-resources/icons/questions-icon.jpg" width="30" />
  <p><b>有问题？</b> 联系我们获取帮助: <a href="mailto:support@tbyd.com">support@tbyd.com</a></p>
</div> 