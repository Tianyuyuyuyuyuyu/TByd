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

### 3.5 错误处理机制

为了确保工具的稳定性和用户体验，TByd.PackageCreator 实现了全面的错误处理机制：

```csharp
public class ErrorHandler
{
    // 错误级别定义
    public enum ErrorLevel
    {
        Info,
        Warning,
        Error,
        Critical
    }

    // 错误类型定义
    public enum ErrorType
    {
        TemplateLoadFailure,
        FileAccessDenied,
        InvalidConfiguration,
        FileGenerationFailure,
        ValidationError,
        UnhandledException
    }

    // 错误记录结构
    public class ErrorRecord
    {
        public ErrorLevel Level { get; set; }
        public ErrorType Type { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public DateTime Timestamp { get; set; }
    }

    private static List<ErrorRecord> s_ErrorHistory = new List<ErrorRecord>();

    // 记录错误
    public static void LogError(ErrorType type, string message, Exception ex = null, ErrorLevel level = ErrorLevel.Error)
    {
        var record = new ErrorRecord
        {
            Level = level,
            Type = type,
            Message = message,
            Exception = ex,
            Timestamp = DateTime.Now
        };

        s_ErrorHistory.Add(record);

        // 根据错误级别输出到 Unity 控制台
        switch (level)
        {
            case ErrorLevel.Info:
                Debug.Log($"[PackageCreator] {message}");
                break;
            case ErrorLevel.Warning:
                Debug.LogWarning($"[PackageCreator] {message}");
                break;
            case ErrorLevel.Error:
            case ErrorLevel.Critical:
                Debug.LogError($"[PackageCreator] {message}");
                if (ex != null)
                {
                    Debug.LogException(ex);
                }
                break;
        }

        // 对于关键错误，可能需要显示对话框
        if (level == ErrorLevel.Critical)
        {
            EditorUtility.DisplayDialog("严重错误", $"发生严重错误: {message}", "确定");
        }
    }

    // 获取错误历史
    public static IEnumerable<ErrorRecord> GetErrorHistory()
    {
        return s_ErrorHistory.AsReadOnly();
    }

    // 清除错误历史
    public static void ClearErrorHistory()
    {
        s_ErrorHistory.Clear();
    }

    // 导出错误日志
    public static bool ExportErrorLog(string filePath)
    {
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("# PackageCreator 错误日志");
            sb.AppendLine($"导出时间: {DateTime.Now}");
            sb.AppendLine();

            foreach (var error in s_ErrorHistory)
            {
                sb.AppendLine($"## [{error.Level}] {error.Type} - {error.Timestamp}");
                sb.AppendLine($"消息: {error.Message}");
                if (error.Exception != null)
                {
                    sb.AppendLine($"异常: {error.Exception.GetType().Name}");
                    sb.AppendLine($"堆栈跟踪: \n{error.Exception.StackTrace}");
                }
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"导出错误日志失败: {ex.Message}");
            return false;
        }
    }
}
```

错误处理策略包括：

1. **分层错误处理**：每个模块内部处理特定于该模块的错误，而通用错误由 ErrorHandler 统一处理。
2. **用户友好消息**：对技术性错误提供用户友好的解释和可能的解决方案。
3. **操作恢复**：在发生错误后提供恢复机制，避免用户丢失已配置的内容。
4. **错误日志**：记录详细的错误信息用于问题排查。
5. **用户反馈**：严重错误时提供明确的用户界面反馈。

典型的错误场景处理：

```csharp
public bool GeneratePackage(PackageConfig config, IPackageTemplate template, string outputPath)
{
    try
    {
        // 验证参数
        if (config == null)
        {
            ErrorHandler.LogError(
                ErrorHandler.ErrorType.InvalidConfiguration,
                "包配置为空，无法生成包结构",
                level: ErrorHandler.ErrorLevel.Error);
            return false;
        }

        if (template == null)
        {
            ErrorHandler.LogError(
                ErrorHandler.ErrorType.TemplateLoadFailure,
                "模板为空，无法生成包结构",
                level: ErrorHandler.ErrorLevel.Error);
            return false;
        }

        // 验证输出路径
        if (string.IsNullOrEmpty(outputPath))
        {
            ErrorHandler.LogError(
                ErrorHandler.ErrorType.InvalidConfiguration,
                "输出路径为空，无法生成包结构",
                level: ErrorHandler.ErrorLevel.Error);
            return false;
        }

        // 验证包配置
        var validationResult = template.ValidatePackageConfig(config);
        if (!validationResult.IsValid)
        {
            ErrorHandler.LogError(
                ErrorHandler.ErrorType.ValidationError,
                $"包配置验证失败: {validationResult.ErrorMessage}",
                level: ErrorHandler.ErrorLevel.Warning);

            // 根据策略决定是否继续
            bool shouldContinue = EditorUtility.DisplayDialog(
                "配置验证警告",
                $"配置验证发现问题: {validationResult.ErrorMessage}\n是否仍要继续?",
                "继续", "取消");

            if (!shouldContinue)
                return false;
        }

        // ... 执行实际的包生成过程 ...

        return true;
    }
    catch (UnauthorizedAccessException ex)
    {
        ErrorHandler.LogError(
            ErrorHandler.ErrorType.FileAccessDenied,
            $"无权限访问路径: {outputPath}，请检查文件权限",
            ex,
            ErrorHandler.ErrorLevel.Error);
        return false;
    }
    catch (IOException ex)
    {
        ErrorHandler.LogError(
            ErrorHandler.ErrorType.FileGenerationFailure,
            $"文件系统操作失败: {ex.Message}",
            ex,
            ErrorHandler.ErrorLevel.Error);
        return false;
    }
    catch (Exception ex)
    {
        ErrorHandler.LogError(
            ErrorHandler.ErrorType.UnhandledException,
            $"生成包时发生未知错误: {ex.Message}",
            ex,
            ErrorHandler.ErrorLevel.Critical);
        return false;
    }
}
```

### 3.6 国际化支持

TByd.PackageCreator 提供了完整的国际化支持，允许工具界面和生成的文档模板适应不同语言环境：

```csharp
public class LocalizationManager
{
    private static LocalizationManager s_Instance;
    public static LocalizationManager Instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = new LocalizationManager();
            return s_Instance;
        }
    }

    // 当前语言
    private SystemLanguage m_CurrentLanguage;
    // 语言文本映射
    private Dictionary<string, Dictionary<string, string>> m_Localization;

    // 支持的语言列表
    public readonly SystemLanguage[] SupportedLanguages = new[]
    {
        SystemLanguage.English,
        SystemLanguage.ChineseSimplified,
        SystemLanguage.Japanese,
        SystemLanguage.Korean,
        SystemLanguage.German,
        SystemLanguage.French,
        SystemLanguage.Spanish
    };

    public LocalizationManager()
    {
        m_Localization = new Dictionary<string, Dictionary<string, string>>();
        LoadDefaultLanguage();
        LoadUserLanguage();
    }

    // 初始化默认语言（英语）
    private void LoadDefaultLanguage()
    {
        var defaultLang = SystemLanguage.English;
        var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/TByd.PackageCreator/Localization/en.json");
        if (textAsset != null)
        {
            var texts = JsonUtility.FromJson<LocalizationData>(textAsset.text);
            m_Localization[defaultLang.ToString()] = texts.items.ToDictionary(i => i.key, i => i.value);
        }
    }

    // 加载用户语言设置
    private void LoadUserLanguage()
    {
        // 尝试获取编辑器语言
        m_CurrentLanguage = Application.systemLanguage;

        // 如果存在用户偏好，使用用户偏好
        if (EditorPrefs.HasKey("TByd.PackageCreator.Language"))
        {
            string langName = EditorPrefs.GetString("TByd.PackageCreator.Language");
            if (Enum.TryParse<SystemLanguage>(langName, out var lang))
            {
                m_CurrentLanguage = lang;
            }
        }

        // 如果当前语言不是英语，加载对应语言文件
        if (m_CurrentLanguage != SystemLanguage.English)
        {
            string langCode = GetLanguageCode(m_CurrentLanguage);
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/TByd.PackageCreator/Localization/{langCode}.json");
            if (textAsset != null)
            {
                var texts = JsonUtility.FromJson<LocalizationData>(textAsset.text);
                m_Localization[m_CurrentLanguage.ToString()] = texts.items.ToDictionary(i => i.key, i => i.value);
            }
        }
    }

    // 获取语言代码
    private string GetLanguageCode(SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.ChineseSimplified: return "zh-CN";
            case SystemLanguage.ChineseTraditional: return "zh-TW";
            case SystemLanguage.Japanese: return "ja";
            case SystemLanguage.Korean: return "ko";
            case SystemLanguage.German: return "de";
            case SystemLanguage.French: return "fr";
            case SystemLanguage.Spanish: return "es";
            default: return "en";
        }
    }

    // 切换语言
    public void SwitchLanguage(SystemLanguage language)
    {
        if (Array.IndexOf(SupportedLanguages, language) >= 0)
        {
            m_CurrentLanguage = language;
            EditorPrefs.SetString("TByd.PackageCreator.Language", language.ToString());

            // 如果语言包尚未加载，尝试加载
            if (!m_Localization.ContainsKey(language.ToString()) && language != SystemLanguage.English)
            {
                string langCode = GetLanguageCode(language);
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/TByd.PackageCreator/Localization/{langCode}.json");
                if (textAsset != null)
                {
                    var texts = JsonUtility.FromJson<LocalizationData>(textAsset.text);
                    m_Localization[language.ToString()] = texts.items.ToDictionary(i => i.key, i => i.value);
                }
            }
        }
    }

    // 获取本地化文本
    public string GetText(string key)
    {
        // 先尝试从当前语言获取
        if (m_Localization.TryGetValue(m_CurrentLanguage.ToString(), out var texts))
        {
            if (texts.TryGetValue(key, out var text))
                return text;
        }

        // 如果当前语言没有，尝试从英语获取
        if (m_CurrentLanguage != SystemLanguage.English &&
            m_Localization.TryGetValue(SystemLanguage.English.ToString(), out var enTexts))
        {
            if (enTexts.TryGetValue(key, out var text))
                return text;
        }

        // 如果仍未找到，返回键名
        return key;
    }

    // 语言数据结构
    [Serializable]
    private class LocalizationData
    {
        public LocalizationItem[] items;
    }

    [Serializable]
    private class LocalizationItem
    {
        public string key;
        public string value;
    }
}
```

语言配置文件示例 (zh-CN.json)：

```json
{
  "items": [
    {
      "key": "PackageCreator.Title",
      "value": "TByd UPM包创建工具"
    },
    {
      "key": "PackageCreator.SelectTemplate",
      "value": "选择模板"
    },
    {
      "key": "PackageCreator.BasicInfo",
      "value": "基本信息"
    },
    {
      "key": "PackageCreator.PackageName",
      "value": "包名称"
    },
    {
      "key": "PackageCreator.PackageNameTooltip",
      "value": "使用反向域名格式，例如：com.company.package"
    },
    {
      "key": "PackageCreator.Generate",
      "value": "生成包"
    },
    {
      "key": "Error.InvalidPackageName",
      "value": "包名称必须使用反向域名格式，例如：com.company.package"
    }
  ]
}
```

模板内容国际化：

```csharp
public class MultiLanguageTemplateProvider
{
    // 根据当前语言获取相应的模板文件
    public string GetTemplateContent(string templateName)
    {
        var locManager = LocalizationManager.Instance;
        string langCode = GetLanguageCode(locManager.CurrentLanguage);

        // 首先尝试加载特定语言的模板
        string localizedPath = $"Templates/{langCode}/{templateName}";
        var localizedAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(localizedPath);

        if (localizedAsset != null)
        {
            return localizedAsset.text;
        }

        // 如果没有找到，回退到默认英文模板
        string defaultPath = $"Templates/en/{templateName}";
        var defaultAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(defaultPath);

        if (defaultAsset != null)
        {
            return defaultAsset.text;
        }

        // 如果仍未找到，返回空字符串
        return string.Empty;
    }
}
```

### 3.7 安全性考虑

TByd.PackageCreator 在设计和实现时考虑了多种安全性问题：

#### 3.7.1 文件操作安全

```csharp
public class SecureFileOperations
{
    // 安全地写入文件，避免可能的竞争条件
    public static bool SafeWriteAllText(string filePath, string content)
    {
        try
        {
            // 创建临时文件
            string tempFilePath = filePath + ".tmp";

            // 写入临时文件
            File.WriteAllText(tempFilePath, content);

            // 如果目标文件存在，先创建备份
            if (File.Exists(filePath))
            {
                string backupPath = filePath + ".bak";
                if (File.Exists(backupPath))
                    File.Delete(backupPath);
                File.Move(filePath, backupPath);
            }

            // 将临时文件改名为目标文件
            File.Move(tempFilePath, filePath);

            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.LogError(
                ErrorHandler.ErrorType.FileGenerationFailure,
                $"安全写入文件失败: {ex.Message}",
                ex,
                ErrorHandler.ErrorLevel.Error);
            return false;
        }
    }

    // 验证路径安全性，防止路径遍历攻击
    public static bool IsPathSafe(string basePath, string relativePath)
    {
        try
        {
            // 规范化路径
            string fullPath = Path.GetFullPath(Path.Combine(basePath, relativePath));

            // 检查最终路径是否仍在基础路径下
            return fullPath.StartsWith(Path.GetFullPath(basePath));
        }
        catch
        {
            return false;
        }
    }

    // 安全地删除目录，防止意外删除重要文件
    public static bool SafeDeleteDirectory(string path, bool preserveTopDirectory = false)
    {
        try
        {
            // 验证路径存在
            if (!Directory.Exists(path))
                return true; // 已经不存在，视为成功

            // 检查是否为Unity项目的关键目录
            string dirName = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            string[] protectedDirs = { "Assets", "ProjectSettings", "Library", "Packages" };

            if (Array.IndexOf(protectedDirs, dirName) >= 0)
            {
                ErrorHandler.LogError(
                    ErrorHandler.ErrorType.FileAccessDenied,
                    $"安全限制：不能删除Unity项目的核心目录: {dirName}",
                    level: ErrorHandler.ErrorLevel.Error);
                return false;
            }

            if (preserveTopDirectory)
            {
                // 只删除内容，保留顶层目录
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }

                foreach (string dir in Directory.GetDirectories(path))
                {
                    Directory.Delete(dir, true);
                }

                return true;
            }
            else
            {
                // 完全删除目录及内容
                Directory.Delete(path, true);
                return true;
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.LogError(
                ErrorHandler.ErrorType.FileAccessDenied,
                $"安全删除目录失败: {ex.Message}",
                ex,
                ErrorHandler.ErrorLevel.Error);
            return false;
        }
    }
}
```

#### 3.7.2 用户输入验证

所有用户输入在使用前都经过严格验证，特别是：

1. **包名验证**：确保使用标准反向域名格式
2. **版本号验证**：遵循语义化版本规范
3. **路径安全检查**：防止路径遍历和不安全的文件操作
4. **变量值过滤**：防止模板变量中包含不安全内容

#### 3.7.3 模板文件验证

```csharp
public class TemplateSecurityChecker
{
    // 检查模板文件的安全性
    public static bool ValidateTemplateFile(TemplateFile file)
    {
        // 检查路径安全性，不允许访问父目录
        if (file.Path.Contains("..") || file.TemplatePath.Contains(".."))
        {
            return false;
        }

        // 检查文件扩展名，禁止某些可执行文件类型
        string extension = Path.GetExtension(file.Path).ToLowerInvariant();
        string[] forbiddenExtensions = { ".exe", ".dll", ".so", ".dylib", ".bat", ".sh", ".com" };

        if (Array.IndexOf(forbiddenExtensions, extension) >= 0)
        {
            return false;
        }

        return true;
    }

    // 验证模板内容
    public static (bool isValid, string errorMessage) ValidateTemplateContent(string content)
    {
        // 检查是否包含可疑脚本
        if (content.Contains("<script>") || content.Contains("eval("))
        {
            return (false, "模板包含可疑JavaScript代码");
        }

        // 其他安全性检查...

        return (true, string.Empty);
    }
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

#### 5.1.1 兼容性考量

TByd.PackageCreator 注重跨版本兼容性和稳定性：

1. **Unity 版本兼容性**：

   - **主要支持版本**：Unity 2021.3.8f1 LTS 及更高版本
   - **最低兼容版本**：Unity 2020.3 LTS
   - **向前兼容**：设计时考虑了与未来 Unity 版本的兼容性

2. **API 兼容性**：

   - 避免使用实验性或预览版 API
   - 使用条件编译处理不同 Unity 版本间的 API 差异：

   ```csharp
   #if UNITY_2020_3_OR_NEWER
       // 2020.3及更新版本特有代码
   #elif UNITY_2019_4_OR_NEWER
       // 2019.4兼容代码
   #endif
   ```

3. **平台兼容性**：

   - 支持所有 Unity 支持的操作系统：Windows、macOS、Linux
   - 文件路径处理使用跨平台 API，确保在不同操作系统间正常工作：

   ```csharp
   string combinedPath = Path.Combine(basePath, relativePath);
   // 而不是 basePath + "/" + relativePath
   ```

4. **向后兼容性策略**：

   - 在重大 API 更改时保留废弃 API 至少一个完整版本周期
   - 使用`[Obsolete]`标记废弃 API，提供明确的迁移路径
   - 维护详细的升级指南和变更日志

5. **包依赖管理**：

   - 明确指定依赖包的版本范围，避免兼容性问题
   - 对 Unity 内置包使用最低兼容版本要求

6. **版本适配器**：

   ```csharp
   public static class UnityVersionAdapter
   {
       // 检查当前Unity版本是否支持某些特性
       public static bool SupportsNewerPackageManagerAPI()
       {
           // 解析当前Unity版本
           var version = Application.unityVersion;
           var parts = version.Split('.');
           if (parts.Length >= 2)
           {
               int major = int.Parse(parts[0]);
               int minor = int.Parse(parts[1]);

               // 2020.3及更高版本支持新包管理器API
               return (major > 2020 || (major == 2020 && minor >= 3));
           }
           return false;
       }

       // 获取适合当前Unity版本的功能接口
       public static IPackageManagerService GetPackageManagerService()
       {
           if (SupportsNewerPackageManagerAPI())
           {
               return new ModernPackageManagerService();
           }
           else
           {
               return new LegacyPackageManagerService();
           }
       }
   }
   ```

7. **功能降级策略**：
   在较旧版本 Unity 上禁用或简化某些功能，而不是完全不兼容

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

### 6.3 扩展功能规划

以下功能将在未来版本中实现，根据用户反馈和优先级进行开发：

#### 6.3.1 包升级支持

允许用户对现有 UPM 包进行升级和结构优化，而不仅限于创建新包。

**主要功能点：**

- 分析现有包结构，识别目录和文件组织
- 根据最新模板更新包结构，保留用户代码
- 升级 package.json 和其他配置文件
- 自动更新版本号（major、minor、patch）
- 更新 CHANGELOG.md 和文档

**计划版本：** v0.3.0

```csharp
// 主要类和接口预览
public class PackageUpgrader
{
    public PackageAnalysisResult AnalyzeExistingPackage(string packagePath);
    public bool UpgradePackage(string packagePath, IPackageTemplate template, PackageUpgradeOptions options);
    public bool BumpVersion(string packagePath, VersionBumpType bumpType);
}

public enum VersionBumpType { Major, Minor, Patch }
```

#### 6.3.2 包发布辅助功能

帮助开发者将包发布到各种平台，自动化发布流程。

**主要功能点：**

- 一键发布到 GitHub（创建 release 分支和 tag）
- 集成 OpenUPM 发布流程
- 自动生成发布文档和发布说明
- 版本一致性检查和验证

**计划版本：** v0.4.0

```csharp
// 主要类和接口预览
public class PackagePublisher
{
    public async Task<bool> PublishToGitHub(string packagePath, GitHubPublishOptions options);
    public async Task<bool> PublishToOpenUPM(string packagePath, OpenUPMPublishOptions options);
    public bool GenerateReleaseDocumentation(string packagePath);
}
```

#### 6.3.3 增强依赖管理

提供更强大的依赖关系配置和验证功能。

**主要功能点：**

- 从 Unity Registry 查询最新包版本
- 依赖冲突检测和解决
- 自动优化依赖配置
- 可视化依赖关系图
- 扫描项目中现有依赖并导入

**计划版本：** v0.5.0

```csharp
// 主要类和接口预览
public class EnhancedDependencyManager
{
    public async Task<PackageInfo> QueryPackageFromRegistry(string packageName);
    public DependencyValidationResult ValidateDependencies(List<PackageDependency> dependencies);
    public List<PackageDependency> ScanProjectDependencies();
    public List<PackageDependency> OptimizeDependencies(List<PackageDependency> dependencies);
}
```

#### 6.3.4 模板共享机制

允许团队间共享自定义模板，建立模板仓库。

**主要功能点：**

- 导出模板为可共享文件（JSON/ZIP）
- 导入共享的模板文件
- 连接到在线模板库
- 上传和下载模板
- 模板版本管理和更新机制

**计划版本：** v0.5.0

```csharp
// 主要类和接口预览
public class TemplateShareSystem
{
    public bool ExportTemplate(IPackageTemplate template, string exportPath);
    public IPackageTemplate ImportTemplate(string importPath);
    public async Task<List<TemplateInfo>> GetTemplatesFromRepository(string repositoryUrl);
    public async Task<IPackageTemplate> DownloadAndInstallTemplate(string repositoryUrl, string templateId);
    public async Task<bool> UploadTemplateToRepository(IPackageTemplate template, string repositoryUrl, string apiKey);
}
```

## 7. MVP 定义与范围

为了能够快速迭代并收集用户反馈，我们将明确定义第一个版本（v0.1.0）的最小可行产品(MVP)范围。

### 7.1 MVP 核心功能

MVP 版本将专注于最核心的功能，确保基础用例的完整实现：

1. **基础模板系统**

   - 提供基础 UPM 包模板
   - 简单的编辑器工具包模板
   - 运行时库模板

2. **标准目录结构生成**

   - Runtime 目录
   - Editor 目录
   - Tests 目录（基础结构）
   - Documentation~ 目录（基础结构）

3. **基础配置文件生成**

   - package.json
   - README.md
   - CHANGELOG.md
   - LICENSE.md
   - 基础 .asmdef 程序集定义文件

4. **简单的包创建向导**

   - 单一窗口界面
   - 包基本信息输入
   - 输出路径选择
   - 简单的模板选择

5. **基础配置验证**
   - 包名称格式验证
   - 版本号格式验证
   - 输出路径验证

### 7.2 MVP 限制范围

为了确保快速交付，以下功能将明确排除在 MVP 之外：

1. **高级模板功能**

   - 自定义模板创建和保存
   - 模板编辑器
   - 模板共享

2. **复杂依赖管理**

   - 依赖冲突检测
   - 依赖图可视化
   - 自动优化依赖

3. **高级用户界面**

   - 多步骤向导
   - 复杂配置选项
   - 自定义主题

4. **包升级和发布功能**

   - 现有包分析和升级
   - 发布自动化
   - 版本管理

5. **国际化**
   - 多语言支持（初始版本仅支持英文和简体中文）

### 7.3 MVP 交付标准

MVP 版本必须满足以下交付标准：

1. **质量要求**

   - 核心功能全部通过测试
   - 无严重错误或崩溃问题
   - 包含基本错误处理

2. **文档要求**

   - 基本用户指南
   - 安装指南
   - API 参考文档
   - 示例用法

3. **用户体验要求**
   - 界面简洁明了
   - 操作流程不超过 3 步
   - 提供有意义的错误提示
   - 基本视觉样式符合 Unity 编辑器风格

### 7.4 MVP 后反馈收集计划

1. **用户反馈渠道**

   - GitHub Issues
   - 内置反馈表单
   - Unity 论坛专用帖

2. **反馈指标**

   - 功能使用频率
   - 错误报告分类
   - 功能请求优先级
   - 用户满意度评分

3. **迭代计划**
   - MVP 发布后 2 周进行首次反馈分析
   - 根据反馈调整 v0.2.0 的优先级
   - 确定关键改进点和新功能
