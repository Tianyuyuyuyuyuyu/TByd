# TByd.PackageCreator 模板目录

此目录包含 TByd.PackageCreator 包使用的模板 JSON 文件。

## 添加新模板

要添加新模板，只需在此目录中创建一个新的 JSON 文件，遵循以下格式：

```json
{
  "id": "com.example.template-id",
  "name": "模板名称",
  "description": "模板描述",
  "version": "1.0.0",
  "author": "作者名称",
  "category": "模板分类",
  "iconPath": "图标路径（可选）",
  "directories": [
    {
      "path": "目录路径",
      "description": "目录描述"
    }
    // 更多目录...
  ],
  "files": [
    {
      "path": "文件路径",
      "content": "文件内容（可以包含变量占位符，如${variableName}）",
      "description": "文件描述"
    }
    // 更多文件...
  ],
  "options": [
    {
      "name": "变量名称",
      "displayName": "显示名称",
      "description": "变量描述",
      "defaultValue": "默认值",
      "required": true/false
    }
    // 更多选项...
  ]
}
```

## 模板变量

在文件内容中，可以使用`${variableName}`格式的占位符，这些占位符将在创建包时被替换为用户提供的值。

## 贡献模板

如果您想贡献新模板，请遵循以下步骤：

1. 创建一个新的 JSON 文件，按照上述格式定义您的模板
2. 确保模板 ID 是唯一的，建议使用反向域名格式（如`com.company.template-name`）
3. 提交 PR 到 TByd.PackageCreator 仓库

## 现有模板

当前目录包含以下模板：

-   `RuntimeLibraryTemplate.json` - 运行时库包模板
-   `EditorToolTemplate.json` - 编辑器工具包模板

## 刷新模板

在编辑器中，您可以通过点击模板选择页面上的"刷新模板"按钮来重新加载所有模板。
