# TByd Template Documentation

欢迎使用TByd Template！本文档提供了关于如何使用和配置此UPM包的详细信息。

## 目录
- [API 参考](API.md)
- [安装指南](#安装指南)
- [基本使用](#基本使用)
- [高级功能](#高级功能)
- [开发指南](#开发指南)
- [常见问题](#常见问题)
- [版本历史](../CHANGELOG.md)

## 安装指南
1. 打开Unity Package Manager。
2. 点击"Add package from git URL..."。
3. 输入以下URL：
   ```
   https://github.com/YourOrganization/YourPackageName.git
   ```
   > 注意：发布时请将上述URL替换为您的实际仓库URL。

### 通过OpenUPM安装
如果您使用OpenUPM，可以通过以下命令安装：
```
openupm add com.yourorganization.yourpackagename
```

### 通过Unity Package Manager手动安装
1. 打开您的Unity项目
2. 打开Package Manager (Window > Package Manager)
3. 点击"+"按钮，选择"Add package from disk..."
4. 浏览并选择包的根目录

## 基本使用

### 初始化
```csharp
// 获取实例并初始化
var manager = TemplateManager.Instance;
await manager.Initialize();

// 使用基本功能
string result = manager.ProcessTemplate("示例数据");
```

### 配置
您可以通过以下方式自定义配置：
```csharp
var config = new TemplateConfig
{
    EnableLogging = true,
    LogLevel = LogLevel.Info,
    AutoInitialize = false
};

await TemplateManager.Instance.Initialize(config);
```

## 高级功能

### 异步操作
本包支持异步操作，推荐在性能敏感场景中使用：
```csharp
string result = await manager.ProcessTemplateAsync("异步处理数据");
```

### 事件系统
您可以订阅包内的事件来响应状态变化：
```csharp
TemplateManager.Instance.OnInitialized += HandleInitialized;
TemplateManager.Instance.OnProcessCompleted += HandleProcessCompleted;
```

## 开发指南

如果您想扩展此包的功能，请参考以下资源：
- [开发者文档](DevDocs/References/index.md)
- [架构图](DevDocs/UML/architecture.md)
- [贡献指南](../CONTRIBUTING.md)

## 常见问题

### 如何更新包？
在Unity Package Manager中，选择此包并点击"Update"按钮。

### 如何报告问题？
请在[GitHub Issues](https://github.com/YourOrganization/YourPackageName/issues)中报告问题。

### 如何在项目中测试此包？
1. 克隆仓库到本地
2. 在Unity中创建新项目
3. 通过Package Manager添加本地包
4. 使用示例场景进行测试

### 性能优化建议
- 使用异步API减少主线程阻塞
- 适当缓存结果避免重复计算
- 在不需要时释放资源 