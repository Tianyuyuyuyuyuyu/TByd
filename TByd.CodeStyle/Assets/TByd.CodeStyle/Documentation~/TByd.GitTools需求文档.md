# TByd.GitTools 需求文档

## 1. 项目概述

### 1.1 项目简介

TByd.GitTools 是一个用于 Unity 项目的 Git 集成工具包。它提供了 Git 提交规范管理、钩子自动化、提交消息验证、分支管理等功能，旨在帮助团队规范 Git 工作流，减少版本控制冲突，提高协作效率。

### 1.2 目标用户

- 使用 Git 进行版本控制的 Unity 开发团队
- 需要规范化提交消息的项目
- 希望自动化 Git 工作流的开发者
- 需要集成代码审查到提交流程的团队

### 1.3 核心功能

- Git 仓库检测和基本操作
- Git 钩子管理（安装、卸载、更新）
- 提交消息格式化和验证
- 提交前代码检查集成
- 编辑器 Git 提交界面
- 分支策略配置和验证

## 2. 技术规格

### 2.1 系统架构

```
TByd.GitTools
├── Core (核心功能层)
│   ├── Git仓库管理
│   ├── Git钩子管理
│   └── 配置管理
├── Commit (提交功能层)
│   ├── 提交消息解析
│   ├── 提交消息验证
│   └── 提交消息格式化
├── Hooks (钩子层)
│   ├── pre-commit钩子
│   ├── commit-msg钩子
│   └── post-checkout钩子
└── Editor UI (编辑器界面)
    ├── 提交窗口
    ├── 钩子管理界面
    └── 设置界面
```

### 2.2 模块职责

#### 2.2.1 Core 模块

- **GitRepository**：Git 仓库检测、路径管理和基本操作
- **GitHookManager**：钩子安装、卸载和状态管理
- **GitConfigManager**：配置加载、保存和迁移

#### 2.2.2 Commit 模块

- **CommitMessage**：提交消息数据结构和格式化
- **CommitMessageParser**：解析提交消息
- **CommitMessageValidator**：验证提交消息
- **CommitMessageRule**：提交消息规则

#### 2.2.3 Hooks 模块

- **HookBase**：钩子基类，定义通用功能
- **PreCommitHook**：提交前检查钩子
- **CommitMsgHook**：提交消息验证钩子
- **PostCheckoutHook**：检出后钩子

#### 2.2.4 Editor UI 模块

- **CommitWindow**：提交编辑窗口
- **HookManagerUI**：钩子管理界面
- **GitSettingsProvider**：设置提供者

### 2.3 接口定义

#### 2.3.1 Git 仓库接口

```csharp
public class GitRepository
{
    // 尝试查找仓库
    public static bool TryFindRepository(string path, out GitRepository repository);

    // 获取仓库路径
    public string GetRepositoryPath();

    // 获取钩子目录
    public string GetHooksDirectory();

    // 检查文件状态
    public List<GitFileStatus> GetStatus();

    // 获取当前分支
    public string GetCurrentBranch();

    // 检查是否有未提交的更改
    public bool HasUncommittedChanges();
}
```

#### 2.3.2 Git 钩子管理器接口

```csharp
public class GitHookManager
{
    // 安装钩子
    public bool InstallHook(HookType hookType, string templatePath = null);

    // 卸载钩子
    public bool UninstallHook(HookType hookType);

    // 检查钩子状态
    public HookStatus GetHookStatus(HookType hookType);

    // 启用/禁用钩子
    public bool EnableHook(HookType hookType, bool enable);

    // 获取钩子路径
    public string GetHookPath(HookType hookType);

    // 重置所有钩子
    public bool ResetAllHooks();
}
```

#### 2.3.3 提交消息接口

```csharp
public class CommitMessage
{
    // 提交类型
    public string Type { get; set; }

    // 作用域
    public string Scope { get; set; }

    // 主题
    public string Subject { get; set; }

    // 正文
    public string Body { get; set; }

    // 尾注
    public string Footer { get; set; }

    // 格式化完整提交消息
    public string FormatMessage();

    // 解析提交消息
    public static CommitMessage Parse(string message);

    // 验证提交消息
    public ValidationResult Validate(GitCommitConfig config);
}
```

#### 2.3.4 提交消息规则接口

```csharp
public interface ICommitMessageRule
{
    // 规则ID
    string Id { get; }

    // 规则名称
    string Name { get; }

    // 规则描述
    string Description { get; }

    // 验证提交消息
    CommitMessageRuleResult Validate(CommitMessage message, GitCommitConfig config);
}
```

## 3. 实现细节

### 3.1 Git 仓库检测与操作

- 使用 `.git` 目录检测仓库位置
- 支持子模块和嵌套仓库
- 直接文件系统操作或使用命令行调用 Git
- 可选：使用 LibGit2Sharp 进行更高级的操作

### 3.2 Git 钩子实现

#### 3.2.1 钩子安装

```csharp
public bool InstallHook(HookType hookType, string templatePath = null)
{
    // 确保仓库存在
    if (!Directory.Exists(GetHooksDirectory()))
        return false;

    // 获取钩子路径
    string hookPath = GetHookPath(hookType);

    // 确定模板路径
    string template = templatePath ?? GetDefaultTemplatePath(hookType);
    if (!File.Exists(template))
        return false;

    try
    {
        // 读取模板内容
        string hookContent = File.ReadAllText(template);

        // 替换模板变量
        hookContent = ReplaceTemplateVariables(hookContent);

        // 写入钩子文件
        File.WriteAllText(hookPath, hookContent);

        // 设置执行权限
        SetExecutablePermission(hookPath);

        return true;
    }
    catch (Exception ex)
    {
        Debug.LogError($"安装Git钩子时出错: {ex.Message}");
        return false;
    }
}
```

#### 3.2.2 Pre-Commit 钩子示例

```bash
#!/bin/sh
#
# Pre-commit hook installed by TByd.GitTools
# This hook runs code analysis before commit
#

# 获取Unity项目路径
UNITY_PROJECT_PATH="{UNITY_PROJECT_PATH}"

# 检查是否安装了dotnet
if ! command -v dotnet >/dev/null 2>&1; then
    echo "错误: 未找到dotnet命令。请安装.NET SDK."
    exit 1
fi

# 调用分析工具
echo "运行代码分析..."
dotnet "$UNITY_PROJECT_PATH/Library/TBydTools/CodeAnalyzer.dll" --pre-commit

# 检查分析结果
if [ $? -ne 0 ]; then
    echo "代码分析发现错误。请修复后再提交。"
    exit 1
fi

echo "代码分析通过！"
exit 0
```

### 3.3 提交消息规则实现

#### 3.3.1 提交消息格式规则

```csharp
[Serializable]
public class CommitFormatRule : ICommitMessageRule
{
    public string Id => "GIT001";
    public string Name => "提交格式规则";
    public string Description => "验证提交消息是否符合格式: <type>(<scope>): <subject>";

    public CommitMessageRuleResult Validate(CommitMessage message, GitCommitConfig config)
    {
        // 默认格式正则：<type>(<scope>): <subject>
        // 或 <type>: <subject> 如果scope不是必需的
        string pattern = config.RequireScope ?
            @"^([a-z]+)\(([a-zA-Z0-9-_]+)\):\s(.+)$" :
            @"^([a-z]+)(\([a-zA-Z0-9-_]+\))?:\s(.+)$";

        Regex regex = new Regex(pattern);

        // 构建头部
        string header = message.Type;
        if (!string.IsNullOrEmpty(message.Scope))
            header += $"({message.Scope})";
        header += $": {message.Subject}";

        // 验证头部格式
        if (!regex.IsMatch(header))
        {
            return new CommitMessageRuleResult
            {
                IsValid = false,
                ErrorMessage = "提交消息格式不正确。应为: <type>[(scope)]: <subject>",
                FixSuggestion = "示例: feat(login): 添加用户登录功能"
            };
        }

        return new CommitMessageRuleResult { IsValid = true };
    }
}
```

#### 3.3.2 提交类型规则

```csharp
[Serializable]
public class CommitTypeRule : ICommitMessageRule
{
    public string Id => "GIT002";
    public string Name => "提交类型规则";
    public string Description => "验证提交类型是否在允许的类型列表中";

    public CommitMessageRuleResult Validate(CommitMessage message, GitCommitConfig config)
    {
        // 检查提交类型是否为空
        if (string.IsNullOrEmpty(message.Type))
        {
            return new CommitMessageRuleResult
            {
                IsValid = false,
                ErrorMessage = "提交类型不能为空",
                FixSuggestion = "添加有效的提交类型，例如: feat, fix, docs等"
            };
        }

        // 检查提交类型是否在允许的列表中
        if (config.AllowedTypes != null && config.AllowedTypes.Count > 0 &&
            !config.AllowedTypes.Any(t => t.Type.Equals(message.Type, StringComparison.OrdinalIgnoreCase)))
        {
            return new CommitMessageRuleResult
            {
                IsValid = false,
                ErrorMessage = $"提交类型 '{message.Type}' 不在允许的类型列表中",
                FixSuggestion = $"使用以下类型之一: {string.Join(", ", config.AllowedTypes.Select(t => t.Type))}"
            };
        }

        return new CommitMessageRuleResult { IsValid = true };
    }
}
```

### 3.4 提交窗口实现

- 基于 Unity EditorWindow
- 提供表单式的提交消息编辑
- 类型和作用域下拉选择
- 主题、正文和尾注多行编辑
- 实时验证和反馈
- 显示当前已更改文件

## 4. 用户界面

### 4.1 提交窗口

- 类型选择下拉框（带描述）
- 作用域选择下拉框（可选）
- 主题输入框（带字数限制）
- 正文多行文本框
- 尾注多行文本框
- 提交按钮
- 实时验证结果显示
- 已修改文件列表

### 4.2 钩子管理窗口

- 钩子状态概览
- 安装/卸载按钮
- 启用/禁用切换
- 钩子日志查看
- 手动触发钩子选项

### 4.3 Git 设置面板

- 集成到 Unity 设置窗口
- 提交类型配置
- 作用域配置
- 提交消息规则配置
- 分支规则配置
- 钩子路径配置

## 5. 开发与测试

### 5.1 开发环境

- Unity 2021.3.8f1 LTS 或更高版本
- .NET Standard 2.0
- Git 2.20 或更高版本

### 5.2 测试策略

- 单元测试提交消息解析和验证
- 集成测试钩子安装和执行
- 模拟 Git 操作的测试
- 跨平台测试（Windows, macOS, Linux）

### 5.3 平台兼容性

- Windows：使用.bat 文件作为钩子
- macOS/Linux：使用 bash 脚本作为钩子
- 处理不同的行尾风格
- 文件权限管理

## 6. 发布计划

### 6.1 版本路线图

- v0.1.0: 基本的 Git 仓库检测和提交消息验证
- v0.2.0: 钩子管理和简单提交窗口
- v0.3.0: 完整的提交界面和规则配置
- v0.4.0: 分支策略和高级集成
- v1.0.0: 完整功能发布

### 6.2 发布渠道

- Unity Package Manager (UPM)
- GitHub 发布
- OpenUPM 注册
