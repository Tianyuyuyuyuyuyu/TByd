using UnityEngine;
using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public class PoolDiagnosticsManager : MonoBehaviour
    {
        private static PoolDiagnosticsManager _instance;
        private readonly Dictionary<string, PoolDiagnosticInfo> _diagnosticData = new();
        private readonly List<DiagnosticEvent> _eventLog = new();
        private const int MaxEventLogSize = 1000;

        [Serializable]
        public class DiagnosticEvent
        {
            public string PoolName;
            public string EventType;
            public string Message;
            public DateTime Timestamp;
            public DiagnosticSeverity Severity;
        }

        public enum DiagnosticSeverity
        {
            Info,
            Warning,
            Error
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void RecordDiagnostics(string poolName, PoolDiagnosticInfo info)
        {
            if (_instance == null) return;
            _instance._diagnosticData[poolName] = info;
            
            // 检查性能问题
            if (info.AverageGetTime > 1.0f || info.AverageReturnTime > 1.0f)
            {
                _instance.LogEvent(poolName, "Performance", 
                    $"Slow pool operations detected. Get: {info.AverageGetTime:F2}ms, Return: {info.AverageReturnTime:F2}ms",
                    DiagnosticSeverity.Warning);
            }

            // 检查容量问题
            if (info.CurrentActive >= info.PeakActive * 0.9f)
            {
                _instance.LogEvent(poolName, "Capacity", 
                    $"Pool near capacity limit ({info.CurrentActive}/{info.PeakActive})",
                    DiagnosticSeverity.Warning);
            }

            // 检查内存使用
            if (info.MemoryUsage > 100 * 1024 * 1024) // 100MB
            {
                _instance.LogEvent(poolName, "Memory", 
                    $"High memory usage: {info.MemoryUsage / (1024 * 1024)}MB",
                    DiagnosticSeverity.Warning);
            }
        }

        private void LogEvent(string poolName, string eventType, string message, DiagnosticSeverity severity)
        {
            var evt = new DiagnosticEvent
            {
                PoolName = poolName,
                EventType = eventType,
                Message = message,
                Timestamp = DateTime.Now,
                Severity = severity
            };

            _eventLog.Add(evt);
            if (_eventLog.Count > MaxEventLogSize)
            {
                _eventLog.RemoveAt(0);
            }

            // 根据严重程度输出日志
            switch (severity)
            {
                case DiagnosticSeverity.Info:
                    Debug.Log($"[Pool Diagnostics] {message}");
                    break;
                case DiagnosticSeverity.Warning:
                    Debug.LogWarning($"[Pool Diagnostics] {message}");
                    break;
                case DiagnosticSeverity.Error:
                    Debug.LogError($"[Pool Diagnostics] {message}");
                    break;
            }
        }

        public static IReadOnlyList<DiagnosticEvent> GetEventLog()
        {
            return _instance?._eventLog;
        }

        public static PoolDiagnosticInfo GetPoolDiagnostics(string poolName)
        {
            if (_instance != null && _instance._diagnosticData.TryGetValue(poolName, out var info))
            {
                return info;
            }
            
            return default;
        }

        public static void ClearDiagnostics()
        {
            if (_instance == null) return;
            _instance._diagnosticData.Clear();
            _instance._eventLog.Clear();
        }

        private void OnDestroy()
        {
            ClearDiagnostics();
        }
    }
} 