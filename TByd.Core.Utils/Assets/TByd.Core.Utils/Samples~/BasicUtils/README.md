# TByd.Core.Utils 基础工具示例

本示例展示了 TByd.Core.Utils 包中各种实用工具类的使用方法。由于UPM包中不能直接包含场景文件，本文档将指导您如何设置和使用示例场景。

## 场景设置

1. 创建一个新的空场景
2. 按照以下层次结构创建游戏对象：

```
BasicUtilsExample (空物体)
├── UI
│   ├── Canvas (Canvas组件)
│   │   ├── StringUtilsPanel (Panel)
│   │   │   ├── Title (Text)
│   │   │   ├── InputField (InputField)
│   │   │   ├── CheckButton (Button)
│   │   │   ├── GenerateButton (Button)
│   │   │   ├── SlugifyButton (Button)
│   │   │   ├── TruncateButton (Button)
│   │   │   ├── SplitButton (Button)
│   │   │   ├── ResultText (Text)
│   │   │   └── LengthSlider (Slider)
│   │   ├── TransformPanel (Panel)
│   │   │   ├── Title (Text)
│   │   │   ├── ChildNameInput (InputField)
│   │   │   ├── ResetLocalButton (Button)
│   │   │   ├── CreateChildButton (Button)
│   │   │   ├── FindChildButton (Button)
│   │   │   ├── GetAllChildrenButton (Button)
│   │   │   └── ResultText (Text)
│   │   └── EventSystem
├── MathDemo
│   ├── TargetObject (Cube)
│   ├── RemapObject (Sphere)
│   ├── RotationObject (Cylinder)
│   │   └── Arrow (Cube, 缩放为 0.1, 1, 0.1)
│   ├── PolygonCenter (空物体)
│   └── TestPoint (Sphere, 缩放为 0.5)
└── TransformDemo
    ├── DemoObject (Cube)
    └── (子对象将通过脚本动态创建)
```

3. 添加组件：
   - 将 `StringUtilsExample.cs` 脚本添加到 `BasicUtilsExample` 游戏对象
   - 将 `MathUtilsExample.cs` 脚本添加到 `MathDemo` 游戏对象
   - 将 `TransformExtensionsExample.cs` 脚本添加到 `TransformDemo` 游戏对象

4. 配置引用：
   - 配置 `StringUtilsExample` 脚本的所有UI引用
   - 配置 `MathUtilsExample` 脚本的引用：
     - TargetObject: MathDemo/TargetObject
     - RemapObject: MathDemo/RemapObject
     - RotationObject: MathDemo/RotationObject
     - LookTarget: 可以是场景中的任何对象或主摄像机
     - PolygonCenter: MathDemo/PolygonCenter
     - TestPoint: MathDemo/TestPoint
     - TestPointRenderer: TestPoint的Renderer组件
   - 配置 `TransformExtensionsExample` 脚本的引用：
     - 所有UI按钮和输入字段
     - DemoObject: TransformDemo/DemoObject

## 功能说明

### StringUtils 示例

此示例展示了字符串处理工具的功能：

- **检查空白字符串**：检测字符串是否为null或仅包含空白字符
- **生成随机字符串**：生成指定长度的随机字符串
- **转换为Slug**：将字符串转换为URL友好的格式
- **截断字符串**：将长字符串截断为指定长度，并添加后缀
- **分割字符串**：按指定分隔符分割字符串

### MathUtils 示例

此示例展示了数学工具的功能：

- **SmoothDamp**：平滑地将一个值过渡到目标值
- **Remap**：将一个范围内的值重新映射到另一个范围
- **DirectionToRotation**：将方向向量转换为旋转
- **IsPointInPolygon**：检测点是否在多边形内

### TransformExtensions 示例

此示例展示了Transform扩展方法的功能：

- **ResetLocal**：重置本地变换（位置、旋转、缩放）
- **FindOrCreateChild**：查找子对象，如不存在则创建
- **FindRecursive**：递归查找子对象
- **GetAllChildren**：获取所有子对象

## 使用说明

1. **StringUtils 测试**：
   - 在输入框中输入文本
   - 使用各个按钮测试不同的字符串操作
   - 使用滑块调整生成的随机字符串长度或截断长度

2. **MathUtils 测试**：
   - 移动 TargetObject 观察 MathDemo 对象平滑跟随
   - 观察 RemapObject 的缩放随时间变化
   - 移动 LookTarget 观察 RotationObject 朝向变化
   - 移动 TestPoint 观察其颜色变化（在多边形内为绿色，外部为红色）

3. **TransformExtensions 测试**：
   - 使用 ResetLocal 按钮重置 DemoObject 的变换
   - 输入名称并使用 CreateChild 按钮创建子对象
   - 输入名称并使用 FindChild 按钮查找子对象
   - 使用 GetAllChildren 按钮列出所有子对象

## 注意事项

- 确保场景中有一个主摄像机
- 确保UI Canvas的渲染模式设置为"Screen Space - Overlay"
- 如果对象不可见，请检查它们是否在摄像机视野范围内
- 确保所有引用都已正确设置 