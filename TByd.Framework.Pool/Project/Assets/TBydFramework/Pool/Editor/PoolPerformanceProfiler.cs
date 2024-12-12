using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TBydFramework.Pool.Editor
{
    public class PoolPerformanceProfiler
    {
        private static readonly Dictionary<string, List<float>> _operationTimes = new Dictionary<string, List<float>>();
        private static readonly int _maxSamples = 100;
        private static readonly Stopwatch _stopwatch = new Stopwatch();

        public static void BeginSample(string poolName, string operation)
        {
            _stopwatch.Restart();
        }

        public static void EndSample(string poolName, string operation)
        {
            _stopwatch.Stop();
            var key = $"{poolName}_{operation}";
            
            if (!_operationTimes.ContainsKey(key))
            {
                _operationTimes[key] = new List<float>();
            }

            var times = _operationTimes[key];
            times.Add(_stopwatch.ElapsedTicks / (float)Stopwatch.Frequency * 1000f);
            
            if (times.Count > _maxSamples)
            {
                times.RemoveAt(0);
            }
        }

        public static float GetAverageTime(string poolName, string operation)
        {
            var key = $"{poolName}_{operation}";
            if (!_operationTimes.ContainsKey(key) || _operationTimes[key].Count == 0)
            {
                return 0f;
            }

            return _operationTimes[key].Average();
        }

        public static void Clear()
        {
            _operationTimes.Clear();
        }

        public static PoolPerformanceData GetPerformanceData(string poolName)
        {
            return new PoolPerformanceData
            {
                GetTime = GetAverageTime(poolName, "Get"),
                ReturnTime = GetAverageTime(poolName, "Return"),
                OperationsPerSecond = CalculateOperationsPerSecond(poolName)
            };
        }

        private static float CalculateOperationsPerSecond(string poolName)
        {
            var getKey = $"{poolName}_Get";
            var returnKey = $"{poolName}_Return";

            if (!_operationTimes.ContainsKey(getKey) || !_operationTimes.ContainsKey(returnKey))
            {
                return 0f;
            }

            var totalOperations = _operationTimes[getKey].Count + _operationTimes[returnKey].Count;
            var timeSpan = Mathf.Max(Time.realtimeSinceStartup, 1f);
            return totalOperations / timeSpan;
        }
    }

    public struct PoolPerformanceData
    {
        public float GetTime;        // 毫秒
        public float ReturnTime;     // 毫秒
        public float OperationsPerSecond;
    }
} 