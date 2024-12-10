using UnityEngine;
using System.Collections.Generic;

namespace TBydFramework.Pool.Runtime.Config
{
    public static class PoolConfigValidator
    {
        public static List<string> ValidateConfig(PoolConfig config)
        {
            var issues = new List<string>();

            if (config.DefaultMaxSize < config.PrewarmSize)
            {
                issues.Add($"默认最大大小({config.DefaultMaxSize})小于预热大小({config.PrewarmSize})");
            }

            if (config.InitialCapacity > config.DefaultMaxSize)
            {
                issues.Add($"初始容量({config.InitialCapacity})大于默认最大大小({config.DefaultMaxSize})");
            }

            if (config.EnableAutoRelease && config.AutoReleaseInterval < config.MaintenanceInterval)
            {
                issues.Add("自动释放间隔应大于维护间隔");
            }

            return issues;
        }
    }
} 