# TByd.CodeFormatter 需求文档

## 1. 项目概述

### 1.1 项目简介

TByd.CodeFormatter 是一个专注于代码格式化和样式定义的 Unity 编辑器扩展包。本包提供了基于 EditorConfig 的代码格式规范定义、验证和应用功能，旨在帮助团队统一代码风格，提高代码可读性和一致性。

### 1.2 目标用户

- Unity 开发团队，特别是多人协作的项目
- 对代码格式和风格有严格要求的开发者
- 需要与现代 IDE 集成代码风格的 Unity 项目

### 1.3 核心功能

- EditorConfig 配置文件的解析与生成
- 代码格式规则的定义与管理
- IDE 配置导出（支持 Visual Studio、Rider、VS Code）
- 编辑器 UI 界面用于规则配置和管理

## 2. 技术规格

### 2.1 系统架构

```
TByd.CodeFormatter
├── Core (核心功能层)
│   ├── EditorConfig解析与生成
│   ├── 格式规则定义
│   └── 配置管理
├── Rules (规则层)
│   ├── 缩进规则
│   ├── 行尾规则
│   ├── 空格规则
│   └── 括号规则
└── IDE (IDE集成层)
    ├── Visual Studio配置
    ├── Rider配置
    └── VS Code配置
```

### 2.2 模块职责

#### 2.2.1 Core 模块

- **EditorConfigParser**：负责解析.editorconfig 文件，提取配置项
- **EditorConfigGenerator**：根据配置生成.editorconfig 文件
- **FormatRuleBase**：格式规则的基类，定义通用属性和方法
- **FormatConfigManager**：管理格式化配置，提供加载、保存功能

#### 2.2.2 Rules 模块

- **IndentationRule**：缩进相关规则（空格 vs 制表符，缩进大小）
- **LineEndingRule**：行尾风格规则（CRLF vs LF）
- **SpacingRule**：空格使用规则（操作符周围，括号内外等）
- **BracesRule**：花括号位置规则（同行 vs 新行）

#### 2.2.3 IDE 模块

- **RiderExporter**：导出 Rider IDE 配置
- **VisualStudioExporter**：导出 Visual Studio 配置
- **VSCodeExporter**：导出 VS Code 配置

### 2.3 接口定义

#### 2.3.1 EditorConfigParser 接口

```csharp
public class EditorConfigParser
{
    // 解析配置文件并返回配置数据
    public EditorConfigData Parse(string filePath);

    // 验证配置数据的有效性
    public ValidationResult Validate(EditorConfigData data);
}
```

#### 2.3.2 FormatRule 接口

```csharp
public interface IFormatRule
{
    // 规则标识符
    string Id { get; }

    // 规则显示名称
    string DisplayName { get; }

    // 规则描述
    string Description { get; }

    // 规则类别
    FormatRuleCategory Category { get; }

    // 生成EditorConfig配置项
    Dictionary<string, string> GenerateEditorConfigSettings();

    // 从EditorConfig配置项中应用设置
    void ApplyFromEditorConfigSettings(Dictionary<string, string> settings);
}
```

#### 2.3.3 IDE 导出器接口

```csharp
public interface IIDEExporter
{
    // 检查IDE是否已安装
    bool IsIDEInstalled();

    // 导出配置到IDE
    bool ExportConfig(FormatConfig config, string outputPath = null);

    // 获取IDE配置文件路径
    string GetConfigFilePath();

    // IDE名称
    string IDEName { get; }
}
```

## 3. 实现细节

### 3.1 EditorConfig 处理

- 支持标准 EditorConfig 规范
- 支持层级目录配置覆盖
- 文件通配符模式匹配
- C#特定格式配置项支持

### 3.2 配置存储

- 使用 ScriptableObject 存储配置
- 配置文件位于 ProjectSettings 目录
- 支持配置版本迁移
- 支持导入/导出配置

### 3.3 规则定义示例

```csharp
// 缩进规则示例
[Serializable]
public class IndentationRule : FormatRuleBase
{
    public override string Id => "indent_style";
    public override string DisplayName => "缩进样式";
    public override string Description => "控制是使用空格还是制表符进行缩进";
    public override FormatRuleCategory Category => FormatRuleCategory.Indentation;

    // 属性
    public enum IndentStyle { Spaces, Tabs }
    public IndentStyle Style = IndentStyle.Spaces;
    public int TabWidth = 4;
    public int IndentSize = 4;

    // 生成EditorConfig设置
    public override Dictionary<string, string> GenerateEditorConfigSettings()
    {
        var settings = new Dictionary<string, string>();
        settings["indent_style"] = Style == IndentStyle.Spaces ? "space" : "tab";
        settings["tab_width"] = TabWidth.ToString();
        settings["indent_size"] = IndentSize.ToString();
        return settings;
    }

    // 应用EditorConfig设置
    public override void ApplyFromEditorConfigSettings(Dictionary<string, string> settings)
    {
        if (settings.TryGetValue("indent_style", out string style))
        {
            Style = style.ToLower() == "space" ? IndentStyle.Spaces : IndentStyle.Tabs;
        }

        if (settings.TryGetValue("tab_width", out string tabWidth) && int.TryParse(tabWidth, out int tw))
        {
            TabWidth = tw;
        }

        if (settings.TryGetValue("indent_size", out string indentSize) && int.TryParse(indentSize, out int size))
        {
            IndentSize = size;
        }
    }
}
```

### 3.4 IDE 集成

#### 3.4.1 Rider 集成

- 生成.editorconfig 文件
- 可选：生成 Rider 特定的.idea 目录和配置文件

#### 3.4.2 Visual Studio 集成

- 生成.editorconfig 文件
- 可选：生成.vs 目录和配置文件

#### 3.4.3 VS Code 集成

- 生成.editorconfig 文件
- 生成.vscode/settings.json 配置

## 4. 用户界面

### 4.1 格式设置窗口

- 提供分类的规则编辑界面
- 使用 Unity EditorGUI 系统
- 提供预览功能
- 支持重置为默认值

### 4.2 设置提供者

- 集成到 Unity Settings 窗口
- 按类别组织规则
- 提供搜索功能
- 提供导入/导出功能

### 4.3 IDE 导出界面

- 检测已安装的 IDE
- 提供一键导出按钮
- 显示导出状态和结果
- 提供覆盖确认

## 5. 开发与测试

### 5.1 开发环境

- Unity 2021.3.8f1 LTS 或更高版本
- .NET Standard 2.0

### 5.2 测试策略

- 单元测试 EditorConfig 解析和生成
- 单元测试规则行为
- 编辑器测试 UI 功能
- 集成测试 IDE 导出功能

### 5.3 性能考量

- EditorConfig 解析缓存
- 延迟加载 UI 资源
- 避免在编辑器启动时执行重型操作

## 6. 发布计划

### 6.1 版本路线图

- v0.1.0: 基本的 EditorConfig 解析和生成
- v0.2.0: 核心格式规则实现
- v0.3.0: IDE 导出功能
- v0.4.0: UI 界面优化
- v1.0.0: 完整功能发布

### 6.2 发布渠道

- Unity Package Manager (UPM)
- GitHub 发布
- OpenUPM 注册
