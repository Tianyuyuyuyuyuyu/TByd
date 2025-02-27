# TByd.Template 高级功能示例

本示例展示了 TByd.Template 包的高级功能和集成方法。

## 高级功能

* 自定义配置和扩展
* 与Unity其他系统集成
* 性能优化技巧
* 错误处理和调试

## 场景说明

示例包括以下场景：

### 1. CustomIntegration.unity

展示如何将包与其他Unity系统(如UI、动画等)集成。

### 2. PerformanceOptimization.unity

展示性能优化技巧和最佳实践。

### 3. ErrorHandling.unity

展示错误捕获和处理方法。

## 使用方式

1. 在Unity中打开示例场景
2. 按照场景中的UI指引操作
3. 查看不同功能的示例实现
4. 研究脚本代码了解实现细节

## 示例脚本

* `CustomIntegrationExample.cs` - 与Unity系统集成示例
* `PerformanceExample.cs` - 性能优化示例
* `ErrorHandlingExample.cs` - 错误处理示例
* `AdvancedConfigExample.cs` - 高级配置示例

## 关键代码

```csharp
// 错误处理示例
using UnityEngine;
using TByd.Template;
using TByd.Template.Utilities;
using System;
using Cysharp.Threading.Tasks;

public class ErrorHandlingExample : MonoBehaviour
{
    [SerializeField] private GameObject m_ErrorUi;
    
    private async void Start()
    {
        try
        {
            // 模拟可能发生错误的操作
            await SimulateFunctionWithError();
        }
        catch (Exception ex)
        {
            // 使用日志工具记录异常
            TemplateLogger.Exception("操作失败", ex);
            
            // 显示错误UI
            if (m_ErrorUi != null)
            {
                m_ErrorUi.SetActive(true);
            }
        }
    }
    
    private async UniTask SimulateFunctionWithError()
    {
        await UniTask.Delay(1000);
        
        // 模拟随机错误
        if (UnityEngine.Random.value > 0.5f)
        {
            throw new InvalidOperationException("示例错误：操作失败");
        }
        
        Debug.Log("操作成功完成");
    }
    
    // 重试机制示例
    public async UniTask<T> RetryOperation<T>(Func<UniTask<T>> _operation, int _maxRetries = 3)
    {
        Exception lastException = null;
        
        for (int i = 0; i < _maxRetries; i++)
        {
            try
            {
                return await _operation();
            }
            catch (Exception ex)
            {
                lastException = ex;
                TemplateLogger.Warning($"操作失败，尝试重试 ({i+1}/{_maxRetries})");
                await UniTask.Delay(1000 * (i + 1)); // 延迟增加
            }
        }
        
        throw new AggregateException($"在 {_maxRetries} 次尝试后失败", lastException);
    }
}
```

## 扩展与自定义

本示例还展示了如何扩展和自定义模板功能：

1. 创建自定义配置类
2. 扩展现有功能
3. 添加新的管理器和系统

## 性能考虑

* 对象池的使用
* 异步操作优化
* 内存分配优化
* 批处理操作 