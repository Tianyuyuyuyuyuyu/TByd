using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public sealed class PoolPerformanceMonitor : MonoBehaviour
    {
        private readonly Dictionary<string, Stopwatch> _operationTimers = new();
        private readonly Dictionary<string, Queue<double>> _recentOperationTimes = new();
        private const int SampleSize = 100;
        
        [SerializeField] private float _warningThresholdMs = 100f;
        [SerializeField] private float _criticalThresholdMs = 500f;
        [SerializeField] private bool _enableLogging = true;
        
        public void BeginOperation(string poolName)
        {
            var timer = new Stopwatch();
            timer.Start();
            _operationTimers[poolName] = timer;
        }

        public void EndOperation(string poolName)
        {
            if (_operationTimers.TryGetValue(poolName, out var timer))
            {
                timer.Stop();
                var elapsed = timer.Elapsed.TotalMilliseconds;
                
                if (!_recentOperationTimes.TryGetValue(poolName, out var times))
                {
                    times = new Queue<double>();
                    _recentOperationTimes[poolName] = times;
                }

                if (times.Count >= SampleSize)
                {
                    times.Dequeue();
                }
                times.Enqueue(elapsed);

                if (_enableLogging)
                {
                    LogPerformance(poolName, elapsed);
                }

                _operationTimers.Remove(poolName);
            }
        }

        public double GetAverageOperationTime(string poolName)
        {
            if (_recentOperationTimes.TryGetValue(poolName, out var times) && times.Count > 0)
            {
                double sum = 0;
                foreach (var time in times)
                {
                    sum += time;
                }
                return sum / times.Count;
            }
            return 0;
        }

        private void LogPerformance(string poolName, double elapsedMs)
        {
            if (elapsedMs > _criticalThresholdMs)
            {
                Debug.LogError($"严重性能问题: {poolName} 操作耗时 {elapsedMs:F2}ms");
            }
            else if (elapsedMs > _warningThresholdMs)
            {
                Debug.LogWarning($"性能警告: {poolName} 操作耗时 {elapsedMs:F2}ms");
            }
        }

        public PerformanceReport GenerateReport(string poolName)
        {
            var report = new PerformanceReport
            {
                PoolName = poolName,
                AverageOperationTime = GetAverageOperationTime(poolName),
                SampleCount = _recentOperationTimes.TryGetValue(poolName, out var times) ? times.Count : 0
            };

            return report;
        }
    }

    public struct PerformanceReport
    {
        public string PoolName { get; set; }
        public double AverageOperationTime { get; set; }
        public int SampleCount { get; set; }
        public override string ToString() => 
            $"Pool: {PoolName}, Avg Time: {AverageOperationTime:F2}ms, Samples: {SampleCount}";
    }
} 