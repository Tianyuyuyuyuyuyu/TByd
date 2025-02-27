# TByd.Template 基础用法示例

本示例展示了 TByd.Template 包的基础功能和使用方法。

## 示例内容

* 基本初始化
* 配置设置
* 日志系统使用
* 异步API的使用

## 场景说明

示例包括以下场景：

### 1. BasicExample.unity

演示包的基本初始化和使用流程。

### 2. ConfigurationExample.unity

展示如何配置和自定义包的各项设置。

## 使用方式

1. 在Unity中打开示例场景
2. 按照场景中的指引操作
3. 查看控制台输出以了解执行流程

## 示例脚本

* `BasicUsageExample.cs` - 基本使用流程
* `ConfigurationExample.cs` - 配置示例
* `LoggingExample.cs` - 日志系统使用示例

## 关键代码

```csharp
// 初始化示例
using UnityEngine;
using TByd.Template;
using Cysharp.Threading.Tasks;

public class BasicUsageExample : MonoBehaviour
{
    private async void Start()
    {
        // 获取管理器实例
        var manager = TemplateManager.Instance;
        
        // 初始化
        await manager.Initialize();
        
        // 使用功能
        string result = manager.ProcessTemplate("示例输入");
        Debug.Log($"处理结果: {result}");
        
        // 使用异步API
        string asyncResult = await manager.ProcessTemplateAsync("异步示例输入");
        Debug.Log($"异步处理结果: {asyncResult}");
    }
}
```

## 注意事项

* 确保在使用任何功能前先初始化管理器
* 查看控制台输出了解执行流程
* 异步方法需要在支持异步的环境中使用 