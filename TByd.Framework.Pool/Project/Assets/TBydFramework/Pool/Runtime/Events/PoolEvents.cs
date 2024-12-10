using System;
using TBydFramework.Pool.Runtime.Diagnostics;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Events
{
    public static class PoolEvents
    {
        public static event Action<PoolEventArgs> OnPoolOperation;
        public static event Action<PoolErrorEventArgs> OnPoolError;

        public static void RaisePoolOperation(object sender, PoolOperationType operationType, string details = null)
        {
            OnPoolOperation?.Invoke(new PoolEventArgs(sender, operationType, details));
        }

        public static void RaisePoolError(object sender, Exception exception, string details = null)
        {
            OnPoolError?.Invoke(new PoolErrorEventArgs(sender, exception, details));
            Debug.LogError($"Pool Error [{sender}]: {exception.Message} - {details}");
        }
    }

    public class PoolEventArgs : EventArgs
    {
        public object Sender { get; }
        public PoolOperationType OperationType { get; }
        public string Details { get; }
        public DateTime Timestamp { get; }

        public PoolEventArgs(object sender, PoolOperationType operationType, string details = null)
        {
            Sender = sender;
            OperationType = operationType;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class PoolErrorEventArgs : EventArgs
    {
        public object Sender { get; }
        public Exception Exception { get; }
        public string Details { get; }
        public DateTime Timestamp { get; }

        public PoolErrorEventArgs(object sender, Exception exception, string details = null)
        {
            Sender = sender;
            Exception = exception;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }
} 