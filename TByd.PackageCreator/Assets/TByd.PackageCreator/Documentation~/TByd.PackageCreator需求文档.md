# TByd.PackageCreator 需求文档

## 1. 项目概述

### 1.1 项目简介

TByd.PackageCreator 是一个 Unity 编辑器扩展工具，专门用于快速创建和初始化 UPM（Unity Package Manager）包项目。它提供了标准化的模板、目录结构生成、配置文件创建和基础代码脚手架，帮助开发者减少重复工作，专注于实现具体的功能逻辑，提高 UPM 包开发效率。

### 1.2 目标用户

- Unity 插件和工具开发者
- 需要频繁创建 UPM 包的团队
- 希望标准化包结构的项目负责人
- Unity 编辑器扩展开发者
- 希望将现有功能模块化为 UPM 包的开发者

### 1.3 核心功能

- 多种 UPM 包模板（基础包、编辑器工具、运行时库等）
- 标准目录结构生成（Runtime、Editor、Tests 等）
- 配置文件自动创建（package.json、README、CHANGELOG 等）
- 程序集定义文件（.asmdef）自动配置
- 基础脚本模板生成
- 版本控制初始化
- 包依赖关系配置
- 自定义模板保存和加载

## 2. 技术规格

### 2.1 系统架构

```
TByd.PackageCreator
├── Core (核心功能层)
│   ├── 模板管理
│   ├── 文件生成
│   └── 配置管理
├── Templates (模板层)
│   ├── 基础包模板
│   ├── 编辑器工具模板
│   ├── 运行时库模板
│   └── 自定义模板
├── Utils (工具层)
│   ├── 文件操作
│   ├── 字符串处理
│   └── Unity资源管理
└── Editor UI (编辑器界面)
    ├── 创建向导
    ├── 模板编辑器
    └── 设置管理
```

### 2.2 模块职责

#### 2.2.1 Core 模块

- **TemplateManager**：管理包模板，提供注册、获取和验证功能
- **FileGenerator**：负责根据模板生成文件和目录结构
- **ConfigManager**：管理工具配置，提供保存和加载功能

#### 2.2.2 Templates 模块

- **PackageTemplateBase**：所有模板的基类，定义通用方法和属性
- **BasicPackageTemplate**：最基础的 UPM 包模板
- **EditorToolTemplate**：编辑器工具包模板，包含编辑器 UI 相关结构
- **RuntimeLibraryTemplate**：运行时库模板，专注于运行时功能

#### 2.2.3 Utils 模块

- **FileUtils**：文件和目录操作工具类
- **StringUtils**：字符串处理和模板变量替换
- **AssetDatabaseUtils**：Unity 资源数据库操作辅助类

#### 2.2.4 Editor UI 模块

- **PackageCreatorWindow**：主创建向导窗口
- **TemplateEditorWindow**：模板编辑窗口
- **PackageCreatorSettings**：设置提供者，集成到 Unity 设置界面

### 2.3 接口定义

#### 2.3.1 模板接口

```csharp
public interface IPackageTemplate
{
    // 模板唯一标识符
    string Id { get; }

    // 模板显示名称
    string DisplayName { get; }

    // 模板描述
    string Description { get; }

    // 模板图标
    Texture2D Icon { get; }

    // 获取模板的目录结构
    List<TemplateDirectory> GetDirectoryStructure();

    // 获取模板文件
    List<TemplateFile> GetTemplateFiles();

    // 验证包配置
    ValidationResult ValidatePackageConfig(PackageConfig config);

    // 获取额外配置选项
    List<TemplateOption> GetOptions();
}
```

#### 2.3.2 文件生成器接口

```csharp
public class FileGenerator
{
    // 生成包结构
    public bool GeneratePackage(PackageConfig config, IPackageTemplate template, string outputPath);

    // 生成单个目录
    public bool GenerateDirectory(string path, TemplateDirectory directory);

    // 生成单个文件
    public bool GenerateFile(string path, TemplateFile file, Dictionary<string, string> variables);

    // 替换模板变量
    public string ReplaceTemplateVariables(string content, Dictionary<string, string> variables);
}
```

#### 2.3.3 包配置接口

```csharp
public class PackageConfig
{
    // 包名称 (com.company.package)
    public string Name { get; set; }

    // 显示名称
    public string DisplayName { get; set; }

    // 版本号
    public string Version { get; set; }

    // 包描述
    public string Description { get; set; }

    // 作者信息
    public AuthorInfo Author { get; set; }

    // 依赖项
    public List<PackageDependency> Dependencies { get; set; }

    // 关键词
    public List<string> Keywords { get; set; }

    // 程序集命名空间
    public string Namespace { get; set; }

    // 生成选项
    public Dictionary<string, object> Options { get; set; }

    // 验证配置
    public ValidationResult Validate();

    // 生成package.json内容
    public string GeneratePackageJson();
}
```

## 3. 实现细节

### 3.1 模板系统实现

#### 3.1.1 模板加载

```csharp
public class TemplateManager
{
    private Dictionary<string, IPackageTemplate> m_Templates = new Dictionary<string, IPackageTemplate>();

    // 加载内置模板
    public void LoadBuiltinTemplates()
    {
        RegisterTemplate(new BasicPackageTemplate());
        RegisterTemplate(new EditorToolTemplate());
        RegisterTemplate(new RuntimeLibraryTemplate());
    }

    // 从JSON加载自定义模板
    public IPackageTemplate LoadCustomTemplateFromJson(string jsonPath)
    {
        try
        {
            string json = File.ReadAllText(jsonPath);
            var template = JsonUtility.FromJson<CustomPackageTemplate>(json);
            return template;
        }
        catch (Exception ex)
        {
            Debug.LogError($"加载自定义模板出错: {ex.Message}");
            return null;
        }
    }

    // 注册模板
    public void RegisterTemplate(IPackageTemplate template)
    {
        if (template != null && !string.IsNullOrEmpty(template.Id))
        {
            m_Templates[template.Id] = template;
        }
    }

    // 获取所有模板
    public IEnumerable<IPackageTemplate> GetAllTemplates()
    {
        return m_Templates.Values;
    }

    // 获取指定模板
    public IPackageTemplate GetTemplate(string id)
    {
        if (m_Templates.TryGetValue(id, out var template))
            return template;
        return null;
    }
}
```

#### 3.1.2 基础包模板示例

```csharp
public class BasicPackageTemplate : IPackageTemplate
{
    public string Id => "basic_package";
    public string DisplayName => "基础UPM包";
    public string Description => "创建基本的UPM包结构，包含标准目录和基础文件";
    public Texture2D Icon => EditorGUIUtility.FindTexture("Package");

    public List<TemplateDirectory> GetDirectoryStructure()
    {
        return new List<TemplateDirectory>
        {
            new TemplateDirectory { Path = "Runtime", Description = "运行时代码目录" },
            new TemplateDirectory { Path = "Editor", Description = "编辑器代码目录" },
            new TemplateDirectory { Path = "Tests", Description = "测试代码目录" },
            new TemplateDirectory { Path = "Tests/Editor", Description = "编辑器测试目录" },
            new TemplateDirectory { Path = "Tests/Runtime", Description = "运行时测试目录" },
            new TemplateDirectory { Path = "Documentation~", Description = "文档目录" },
        };
    }

    public List<TemplateFile> GetTemplateFiles()
    {
        return new List<TemplateFile>
        {
            new TemplateFile
            {
                Path = "package.json",
                TemplatePath = "Templates/Basic/package.json.template"
            },
            new TemplateFile
            {
                Path = "README.md",
                TemplatePath = "Templates/Basic/README.md.template"
            },
            new TemplateFile
            {
                Path = "CHANGELOG.md",
                TemplatePath = "Templates/Basic/CHANGELOG.md.template"
            },
            new TemplateFile
            {
                Path = "LICENSE.md",
                TemplatePath = "Templates/Basic/LICENSE.md.template"
            },
            new TemplateFile
            {
                Path = "Runtime/{PACKAGE_NAME}.asmdef",
                TemplatePath = "Templates/Basic/runtime-asmdef.template"
            },
            new TemplateFile
            {
                Path = "Editor/{PACKAGE_NAME}.Editor.asmdef",
                TemplatePath = "Templates/Basic/editor-asmdef.template"
            }
        };
    }

    public ValidationResult ValidatePackageConfig(PackageConfig config)
    {
        // 验证包名称
        if (string.IsNullOrEmpty(config.Name) || !config.Name.Contains('.'))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "包名称必须使用反向域名格式，例如：com.company.package"
            };
        }

        // 验证版本号
        if (string.IsNullOrEmpty(config.Version) || !Regex.IsMatch(config.Version, @"^\d+\.\d+\.\d+$"))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "版本号必须使用语义化版本格式，例如：1.0.0"
            };
        }

        return new ValidationResult { IsValid = true };
    }

    public List<TemplateOption> GetOptions()
    {
        return new List<TemplateOption>
        {
            new TemplateOption
            {
                Id = "includeDocSamples",
                DisplayName = "包含文档示例",
                Description = "生成示例文档文件",
                Type = OptionType.Boolean,
                DefaultValue = true
            },
            new TemplateOption
            {
                Id = "includeTestSamples",
                DisplayName = "包含测试示例",
                Description = "生成示例测试文件",
                Type = OptionType.Boolean,
                DefaultValue = true
            }
        };
    }
}
```

### 3.2 文件生成实现

```csharp
public bool GeneratePackage(PackageConfig config, IPackageTemplate template, string outputPath)
{
    try
    {
        // 创建包根目录
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        // 创建目录结构
        foreach (var directory in template.GetDirectoryStructure())
        {
            string directoryPath = Path.Combine(outputPath, directory.Path);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        // 生成变量字典
        var variables = new Dictionary<string, string>
        {
            { "PACKAGE_NAME", config.Name },
            { "DISPLAY_NAME", config.DisplayName },
            { "PACKAGE_VERSION", config.Version },
            { "PACKAGE_DESCRIPTION", config.Description },
            { "AUTHOR_NAME", config.Author?.Name ?? "" },
            { "AUTHOR_EMAIL", config.Author?.Email ?? "" },
            { "NAMESPACE", config.Namespace },
            { "CURRENT_YEAR", DateTime.Now.Year.ToString() },
            { "CREATION_DATE", DateTime.Now.ToString("yyyy-MM-dd") }
        };

        // 创建文件
        foreach (var file in template.GetTemplateFiles())
        {
            // 处理文件路径中的变量
            string filePath = ReplaceTemplateVariables(file.Path, variables);
            string fullPath = Path.Combine(outputPath, filePath);

            // 确保文件目录存在
            string fileDirectory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            // 读取模板内容
            string templateContent;
            var templateAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(file.TemplatePath);
            if (templateAsset != null)
            {
                templateContent = templateAsset.text;
            }
            else
            {
                // 尝试从绝对路径加载
                string templateFilePath = Path.Combine(
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this))),
                    file.TemplatePath);

                if (File.Exists(templateFilePath))
                    templateContent = File.ReadAllText(templateFilePath);
                else
                    continue; // 跳过不存在的模板
            }

            // 替换变量
            string content = ReplaceTemplateVariables(templateContent, variables);

            // 写入文件
            File.WriteAllText(fullPath, content);
        }

        // 刷新资源数据库
        AssetDatabase.Refresh();

        return true;
    }
    catch (Exception ex)
    {
        Debug.LogError($"生成包出错: {ex.Message}");
        return false;
    }
}
```

### 3.3 配置存储与管理

- 使用 EditorPrefs 存储用户偏好
- 使用 ScriptableObject 存储模板和默认设置
- 支持配置导入/导出功能

### 3.4 模板变量替换

```csharp
public string ReplaceTemplateVariables(string content, Dictionary<string, string> variables)
{
    if (string.IsNullOrEmpty(content) || variables == null)
        return content;

    // 替换所有变量
    foreach (var variable in variables)
    {
        content = content.Replace($"{{{variable.Key}}}", variable.Value);
    }

    return content;
}
```

## 4. 用户界面

### 4.1 包创建向导

- 步骤式向导界面
- 模板选择页面
- 基本信息配置页面
- 依赖项配置页面
- 高级选项页面
- 创建确认和结果页面

### 4.2 模板编辑器

- 模板属性编辑
- 目录结构可视化
- 文件模板编辑器
- 变量定义界面
- 导入/导出功能

### 4.3 设置提供者

- 集成到 Unity 设置窗口
- 默认值配置
- 模板管理
- 用户偏好设置

## 5. 开发与测试

### 5.1 开发环境

- Unity 2021.3.8f1 LTS 或更高版本
- .NET Standard 2.0
- 纯编辑器扩展，不依赖运行时环境

### 5.2 测试策略

- 单元测试模板系统
- 集成测试文件生成功能
- 编辑器测试 UI 功能
- 端到端测试包创建流程

### 5.3 性能考量

- 异步文件操作
- 模板缓存
- 资源按需加载
- 大型模板的内存优化

## 6. 发布计划

### 6.1 版本路线图

- v0.1.0: 基本的包创建功能和内置模板
- v0.2.0: 自定义模板支持和高级选项
- v0.3.0: 完整的 UI 界面和用户体验改进
- v0.4.0: 与其他 TByd 工具的集成
- v1.0.0: 完整功能发布

### 6.2 发布渠道

- Unity Package Manager (UPM)
- GitHub 发布
- OpenUPM 注册
