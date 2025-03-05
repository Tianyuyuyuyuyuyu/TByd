# TByd.CodeAnalyzer 需求文档

## 1. 项目概述

### 1.1 项目简介

TByd.CodeAnalyzer 是一个静态代码分析和检查工具，专为 Unity C#项目设计。它提供了代码质量检查、命名规范验证、Unity 特定规则检查、自动修复建议等功能，帮助开发者提高代码质量，减少潜在 Bug，保持代码风格一致性。

### 1.2 目标用户

- 注重代码质量的 Unity 开发团队
- 需要代码审查自动化的开发者
- 希望遵循最佳实践的 Unity 项目
- 需要统一命名规范的团队

### 1.3 核心功能

- 代码命名规范检查
- 代码质量和可维护性分析
- Unity 特定最佳实践检查
- 代码问题自动修复建议
- 分析结果可视化
- 与 Unity 编译管道集成

## 2. 技术规格

### 2.1 系统架构

```
TByd.CodeAnalyzer
├── Core (核心分析框架)
│   ├── 分析器引擎
│   ├── 结果收集与报告
│   └── 规则管理
├── Rules (规则集)
│   ├── 命名规则
│   ├── 质量规则
│   └── Unity规则
├── Fixes (修复机制)
│   ├── 修复提供者
│   └── 自动修复执行
└── Editor UI (编辑器界面)
    ├── 分析窗口
    ├── 结果展示
    └── 规则配置
```

### 2.2 模块职责

#### 2.2.1 Core 模块

- **CodeAnalyzer**：分析引擎，协调规则执行和结果收集
- **AnalysisResult**：封装分析结果，包括问题列表和统计信息
- **RuleBase**：规则基类，定义规则接口和通用功能
- **AnalyzerConfig**：分析器配置，控制启用的规则和严重级别

#### 2.2.2 Rules 模块

- **命名规则**：检查变量、方法、类等命名是否符合规范
- **质量规则**：检查代码质量问题，如未使用变量、冗余代码
- **Unity 规则**：检查 Unity 特定的最佳实践和常见错误

#### 2.2.3 Fixes 模块

- **CodeFixer**：代码修复引擎，协调修复提供者
- **FixProvider**：修复提供者，为特定问题生成修复方案

#### 2.2.4 Editor UI 模块

- **AnalyzerWindow**：主分析窗口，提供分析控制和结果展示
- **ResultsPanel**：结果展示面板，列出问题并提供导航
- **RuleEditor**：规则配置界面，控制规则启用和严重级别

### 2.3 接口定义

#### 2.3.1 分析器接口

```csharp
public class CodeAnalyzer
{
    // 分析单个文件
    public AnalysisResult AnalyzeFile(string filePath);

    // 分析整个项目
    public AnalysisResult AnalyzeProject(string projectPath = null, AnalyzerFilter filter = null);

    // 注册规则
    public void RegisterRule(IRule rule);

    // 加载规则集
    public void LoadRuleSet(string ruleSetPath);

    // 获取注册的规则
    public IEnumerable<IRule> GetRegisteredRules();
}
```

#### 2.3.2 规则接口

```csharp
public interface IRule
{
    // 规则唯一标识符
    string Id { get; }

    // 规则显示名称
    string DisplayName { get; }

    // 规则描述
    string Description { get; }

    // 规则类别
    RuleCategory Category { get; }

    // 默认严重程度
    RuleSeverity DefaultSeverity { get; }

    // 分析代码
    IEnumerable<CodeIssue> Analyze(SourceCode code, AnalyzerConfig config);

    // 规则是否提供修复
    bool ProvidesFix { get; }
}
```

#### 2.3.3 修复提供者接口

```csharp
public interface IFixProvider
{
    // 支持的规则ID
    IEnumerable<string> SupportedRuleIds { get; }

    // 生成修复方案
    IEnumerable<CodeFix> ProvideFixes(CodeIssue issue, SourceCode code);
}
```

## 3. 实现细节

### 3.1 代码分析实现

#### 3.1.1 命名规则实现示例

```csharp
[Serializable]
public class PrivateFieldNamingRule : RegexRuleBase
{
    public override string Id => "CS0001";
    public override string DisplayName => "私有字段命名规则";
    public override string Description => "私有字段应使用m_前缀加帕斯卡命名法";
    public override RuleCategory Category => RuleCategory.Naming;
    public override RuleSeverity DefaultSeverity => RuleSeverity.Warning;

    public PrivateFieldNamingRule()
    {
        // 正则表达式匹配不符合m_PascalCase的私有字段
        Pattern = @"private\s+\w+\s+(?!m_[A-Z])[a-zA-Z0-9_]+\s*[;=]";
    }

    public override string FormatMessage(Match match)
    {
        // 从匹配中提取字段名
        string fieldName = ExtractFieldName(match.Value);
        return $"私有字段 '{fieldName}' 应使用m_前缀加帕斯卡命名法，例如: m_{ToPascalCase(fieldName.TrimStart('_'))}'";
    }

    public override bool ProvidesFix => true;

    // 提取字段名的辅助方法
    private string ExtractFieldName(string declaration)
    {
        // 实现提取字段名的逻辑
        // ...
    }

    // 转换为帕斯卡命名法的辅助方法
    private string ToPascalCase(string name)
    {
        // 实现转换为帕斯卡命名法的逻辑
        // ...
    }
}
```

#### 3.1.2 Unity 规则实现示例

```csharp
[Serializable]
public class FindObjectRule : RuleBase
{
    public override string Id => "UN0001";
    public override string DisplayName => "避免使用GameObject.Find";
    public override string Description => "使用GameObject.Find方法会导致性能问题，建议使用引用或其他方式查找对象";
    public override RuleCategory Category => RuleCategory.Unity;
    public override RuleSeverity DefaultSeverity => RuleSeverity.Warning;

    public override IEnumerable<CodeIssue> Analyze(SourceCode code, AnalyzerConfig config)
    {
        // 查找代码中的GameObject.Find调用
        var issues = new List<CodeIssue>();

        // 使用正则表达式查找GameObject.Find调用
        var regex = new Regex(@"GameObject\.Find\s*\(");
        var matches = regex.Matches(code.Content);

        foreach (Match match in matches)
        {
            issues.Add(new CodeIssue
            {
                Id = Id,
                FilePath = code.FilePath,
                Line = code.GetLineNumber(match.Index),
                Column = code.GetColumnNumber(match.Index),
                Message = "避免使用GameObject.Find，这会导致性能问题。建议使用序列化字段、查找API或依赖注入",
                Severity = config.GetRuleSeverity(Id) ?? DefaultSeverity,
                CanFix = true
            });
        }

        return issues;
    }
}
```

### 3.2 代码修复实现

```csharp
public class PrivateFieldNamingFixProvider : IFixProvider
{
    public IEnumerable<string> SupportedRuleIds => new[] { "CS0001" };

    public IEnumerable<CodeFix> ProvideFixes(CodeIssue issue, SourceCode code)
    {
        // 从问题中提取字段名
        string fieldName = ExtractFieldNameFromIssue(issue);
        if (string.IsNullOrEmpty(fieldName))
            yield break;

        // 生成符合规范的新名称
        string newFieldName = "m_" + ToPascalCase(fieldName.TrimStart('_'));

        // 创建修复操作
        yield return new CodeFix
        {
            Title = $"重命名为 '{newFieldName}'",
            EditsProvider = () => new[]
            {
                new CodeEdit
                {
                    FilePath = issue.FilePath,
                    Replacement = newFieldName,
                    StartOffset = issue.StartOffset,
                    Length = fieldName.Length
                }
            }
        };
    }

    // 从问题中提取字段名的辅助方法
    private string ExtractFieldNameFromIssue(CodeIssue issue)
    {
        // 实现从问题中提取字段名的逻辑
        // ...
    }

    // 转换为帕斯卡命名法的辅助方法
    private string ToPascalCase(string name)
    {
        // 实现转换为帕斯卡命名法的逻辑
        // ...
    }
}
```

### 3.3 配置存储与管理

- 使用 ScriptableObject 存储分析器配置
- 保存在 ProjectSettings 目录
- 支持规则的启用/禁用
- 支持自定义规则参数
- 支持规则集导入/导出

### 3.4 与 Unity 编译管道集成

- 使用 Unity 的 CompilationPipeline API
- 在脚本编译后触发分析
- 可选择性地在构建前执行完整分析
- 将分析结果集成到 Unity 控制台

## 4. 用户界面

### 4.1 分析器窗口

- 主要分析控制面板
- 项目范围和文件过滤选项
- 一键分析按钮
- 分析进度显示
- 结果摘要视图

### 4.2 结果面板

- 问题列表，按文件和严重程度分组
- 双击导航到问题源代码
- 显示问题描述和修复建议
- 一键应用修复功能
- 忽略特定问题的选项

### 4.3 规则配置界面

- 按类别组织规则列表
- 启用/禁用单个规则
- 调整规则严重程度
- 配置规则特定参数
- 规则集保存和加载

## 5. 开发与测试

### 5.1 开发环境

- Unity 2021.3.8f1 LTS 或更高版本
- .NET Standard 2.0
- 可能依赖 Roslyn (Microsoft.CodeAnalysis)用于深度分析

### 5.2 测试策略

- 单元测试各类规则
- 针对常见代码模式的集成测试
- 性能基准测试
- UI 功能测试

### 5.3 性能考量

- 增量分析，只分析已更改的文件
- 分析结果缓存
- 后台线程分析
- 对大型项目的优化

## 6. 发布计划

### 6.1 版本路线图

- v0.1.0: 核心分析框架和基本命名规则
- v0.2.0: Unity 特定规则和质量规则
- v0.3.0: 代码修复功能
- v0.4.0: UI 界面和用户体验改进
- v1.0.0: 完整功能发布

### 6.2 发布渠道

- Unity Package Manager (UPM)
- GitHub 发布
- OpenUPM 注册
