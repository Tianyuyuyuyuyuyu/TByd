using UnityEngine;
using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public static class PoolDiagnostics
    {
        private static readonly Dictionary<string, PoolHealthInfo> _healthInfo = new Dictionary<string, PoolHealthInfo>();
        private static readonly List<PoolWarning> _warnings = new List<PoolWarning>();
        private const int MaxWarnings = 100;

        public static void RecordOperation(string poolName, PoolOperation operation, float duration)
        {
            if (!_healthInfo.TryGetValue(poolName, out var info))
            {
                info = new PoolHealthInfo();
                _healthInfo[poolName] = info;
            }

            info.RecordOperation(operation, duration);
            CheckHealth(poolName, info);
        }

        private static void CheckHealth(string poolName, PoolHealthInfo info)
        {
            var pool = PoolRegistry.GetPoolInfo(poolName);
            if (pool == null) return;

            // 检查池容量
            if (pool.Count >= pool.MaxSize * 0.9f)
            {
                AddWarning(new PoolWarning
                {
                    PoolName = poolName,
                    Type = WarningType.CapacityNearLimit,
                    Message = $"Pool {poolName} is near capacity limit ({pool.Count}/{pool.MaxSize})"
                });
            }

            // 检查性能
            if (info.AverageOperationTime > 1.0f) // 1ms阈值
            {
                AddWarning(new PoolWarning
                {
                    PoolName = poolName,
                    Type = WarningType.PerformanceIssue,
                    Message = $"Pool {poolName} operations are slow (avg: {info.AverageOperationTime:F2}ms)"
                });
            }
        }

        private static void AddWarning(PoolWarning warning)
        {
            _warnings.Add(warning);
            if (_warnings.Count > MaxWarnings)
            {
                _warnings.RemoveAt(0);
            }

            Debug.LogWarning($"[Pool Diagnostics] {warning.Message}");
        }

        public static IReadOnlyList<PoolWarning> GetWarnings()
        {
            return _warnings;
        }

        public static void ClearWarnings()
        {
            _warnings.Clear();
        }
    }

    public class PoolHealthInfo
    {
        private readonly Queue<float> _operationTimes = new Queue<float>();
        private const int MaxSamples = 100;

        public float AverageOperationTime { get; private set; }
        public int OperationCount { get; private set; }

        public void RecordOperation(PoolOperation operation, float duration)
        {
            _operationTimes.Enqueue(duration);
            if (_operationTimes.Count > MaxSamples)
            {
                _operationTimes.Dequeue();
            }

            UpdateStats();
            OperationCount++;
        }

        private void UpdateStats()
        {
            if (_operationTimes.Count == 0) return;

            float sum = 0;
            foreach (var time in _operationTimes)
            {
                sum += time;
            }
            AverageOperationTime = sum / _operationTimes.Count;
        }
    }

    public enum PoolOperation
    {
        Get,
        Return,
        Cleanup
    }

    public enum WarningType
    {
        CapacityNearLimit,
        PerformanceIssue,
        ResourceLeak
    }

    public struct PoolWarning
    {
        public string PoolName;
        public WarningType Type;
        public string Message;
        public DateTime Timestamp;
    }
} 