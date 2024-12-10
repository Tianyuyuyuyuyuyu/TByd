using System;
using UnityEngine.Events;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public static class PoolDiagnosticEvents
    {
        public static event Action<string, PoolOperationType> OnPoolOperation;
        public static event Action<string, Exception> OnPoolError;
        public static event Action<string, PerformanceReport> OnPerformanceReport;
        
        public static void RaisePoolOperation(string poolName, PoolOperationType operationType)
        {
            OnPoolOperation?.Invoke(poolName, operationType);
        }

        public static void RaisePoolError(string poolName, Exception exception)
        {
            OnPoolError?.Invoke(poolName, exception);
        }

        public static void RaisePerformanceReport(string poolName, PerformanceReport report)
        {
            OnPerformanceReport?.Invoke(poolName, report);
        }
    }

    public enum PoolOperationType
    {
        Allocation,
        Reuse,
        Return,
        Destroy,
        Validation,
        Maintenance
    }
} 