# IDE集成

## 功能概述

TByd.CodeStyle提供了完整的IDE集成支持，可以帮助您在不同的IDE中保持一致的代码风格。目前支持以下IDE：

- JetBrains Rider
- Visual Studio
- Visual Studio Code

## 主要功能

### 1. IDE检测和自动配置

- 自动检测当前使用的IDE
- 自动配置IDE的代码风格设置
- 支持重置IDE配置
- 提供IDE配置状态检查

### 2. 配置验证和冲突检测

- 验证IDE配置的完整性
- 检查配置项的有效性
- 检测配置冲突
- 提供详细的错误和警告信息

### 3. 配置备份和恢复

- 创建配置备份
- 管理备份历史
- 恢复历史配置
- 删除旧备份

### 4. 配置同步

- 同步团队配置
- 检测配置冲突
- 解决配置冲突
- 自动更新配置

## 使用说明

### IDE配置

1. 打开Unity编辑器，选择 `TByd/CodeStyle/代码风格工具`
2. 切换到"IDE集成"标签页
3. 点击"配置IDE"按钮进行自动配置
4. 根据需要调整IDE特定设置

### 配置验证

1. 在"IDE集成"标签页中，点击"验证配置"按钮
2. 查看验证结果，包括：
   - 错误：需要立即修复的问题
   - 警告：可能影响使用的问题
   - 建议：改进建议

### 配置备份

1. 点击"创建备份"按钮
2. 输入备份描述（可选）
3. 在备份列表中可以：
   - 查看备份历史
   - 恢复历史配置
   - 删除旧备份

### 配置同步

1. 点击"同步配置"按钮
2. 查看同步结果：
   - 更新的文件列表
   - 存在冲突的文件
3. 如有冲突，选择：
   - 使用本地版本
   - 使用标准版本

## IDE特定设置

### Rider设置

- 代码分析：启用/禁用代码分析功能
- StyleCop：启用/禁用StyleCop支持
- ReSharper：启用/禁用ReSharper功能

### Visual Studio设置

- Roslyn分析器：启用/禁用Roslyn代码分析
- StyleCop：启用/禁用StyleCop支持
- 代码分析：启用/禁用Visual Studio代码分析

### VS Code设置

- OmniSharp：启用/禁用OmniSharp支持
- Roslyn分析器：启用/禁用Roslyn分析器
- EditorConfig：启用/禁用EditorConfig支持

## 配置文件

### 1. EditorConfig

```editorconfig
# 示例配置
root = true

[*]
charset = utf-8
end_of_line = lf
indent_style = space
indent_size = 4
insert_final_newline = true
trim_trailing_whitespace = true

[*.cs]
csharp_style_var_for_built_in_types = true:suggestion
csharp_style_var_when_type_is_apparent = true:suggestion
csharp_style_var_elsewhere = true:suggestion
```

### 2. Rider配置

```xml
<!-- codeStyleConfig.xml -->
<code_scheme name="TByd.CodeStyle" version="173">
    <option name="LINE_SEPARATOR" value="&#10;" />
    <option name="RIGHT_MARGIN" value="120" />
    <CSharpCodeStyleSettings>
        <!-- 配置项 -->
    </CSharpCodeStyleSettings>
</code_scheme>
```

### 3. VS Code配置

```json
// settings.json
{
    "editor.formatOnSave": true,
    "editor.formatOnType": true,
    "omnisharp.enableEditorConfigSupport": true,
    "omnisharp.enableRoslynAnalyzers": true
}
```

## 常见问题

### 1. IDE未被正确检测

- 确保IDE已正确安装
- 检查Unity的外部脚本编辑器设置
- 尝试重新检测IDE

### 2. 配置未生效

- 检查IDE是否支持EditorConfig
- 验证配置文件是否正确
- 重新应用配置

### 3. 配置冲突

- 使用配置验证功能检查冲突
- 选择合适的冲突解决方案
- 同步团队配置

## 最佳实践

1. 定期创建配置备份
2. 在团队中统一使用相同的配置
3. 及时解决配置冲突
4. 定期验证配置有效性
5. 保持IDE和插件更新 