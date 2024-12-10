using System;
using System.Collections.Generic;
using System.Threading;

namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 连接池的抽象基类，提供连接池的基本实现
    /// </summary>
    /// <typeparam name="TConnection">连接对象类型</typeparam>
    public abstract class ConnectionPoolBase<TConnection> : IConnectionPool<TConnection>
        where TConnection : class
    {
        protected readonly Stack<TConnection> IdleConnections;
        protected readonly HashSet<TConnection> ActiveConnections;
        protected readonly object SyncRoot = new object();
        
        private readonly int _maxSize;
        private readonly TimeSpan _connectionTimeout;
        private readonly TimeSpan _idleTimeout;
        private bool _isDisposed;

        protected ConnectionPoolBase(int maxSize = 10, TimeSpan? connectionTimeout = null, TimeSpan? idleTimeout = null)
        {
            _maxSize = maxSize;
            _connectionTimeout = connectionTimeout ?? TimeSpan.FromSeconds(30);
            _idleTimeout = idleTimeout ?? TimeSpan.FromMinutes(5);
            
            IdleConnections = new Stack<TConnection>(_maxSize);
            ActiveConnections = new HashSet<TConnection>();
            
            StartMaintenanceTimer();
        }

        /// <summary>
        /// 创建新的连接实例
        /// </summary>
        protected abstract TConnection CreateConnection();

        /// <summary>
        /// 验证连接是否有效
        /// </summary>
        protected abstract bool ValidateConnection(TConnection connection);

        /// <summary>
        /// 关闭并清理连接
        /// </summary>
        protected abstract void CloseConnection(TConnection connection);

        public virtual TConnection Acquire()
        {
            ThrowIfDisposed();

            lock (SyncRoot)
            {
                TConnection connection = null;
                
                while (IdleConnections.Count > 0)
                {
                    connection = IdleConnections.Pop();
                    if (ValidateConnection(connection))
                    {
                        break;
                    }
                    CloseConnection(connection);
                    connection = null;
                }

                if (connection == null && ActiveConnections.Count < _maxSize)
                {
                    connection = CreateConnection();
                }

                if (connection != null)
                {
                    ActiveConnections.Add(connection);
                    return connection;
                }

                throw new InvalidOperationException("无法获取可用连接，连接池已满");
            }
        }

        public virtual void Release(TConnection connection)
        {
            ThrowIfDisposed();
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            lock (SyncRoot)
            {
                if (!ActiveConnections.Remove(connection))
                {
                    throw new InvalidOperationException("试图释放一个不属于此池的连接");
                }

                if (ValidateConnection(connection))
                {
                    IdleConnections.Push(connection);
                }
                else
                {
                    CloseConnection(connection);
                }
            }
        }

        public int Count => IdleConnections.Count;
        
        public int ActiveCount => ActiveConnections.Count;
        
        public bool IsDisposed => _isDisposed;

        public virtual void Clear()
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                foreach (var connection in IdleConnections)
                {
                    CloseConnection(connection);
                }
                IdleConnections.Clear();

                foreach (var connection in ActiveConnections)
                {
                    CloseConnection(connection);
                }
                ActiveConnections.Clear();
            }
        }

        public virtual void Prewarm(int count)
        {
            ThrowIfDisposed();
            
            count = Math.Min(count, _maxSize);
            for (int i = 0; i < count; i++)
            {
                var connection = CreateConnection();
                if (connection != null)
                {
                    IdleConnections.Push(connection);
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            
            Clear();
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        protected void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }

        private Timer _maintenanceTimer;
        
        private void StartMaintenanceTimer()
        {
            _maintenanceTimer = new Timer(MaintenanceCallback, null, 
                TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        private void MaintenanceCallback(object state)
        {
            if (_isDisposed) return;

            try
            {
                RemoveStaleConnections();
            }
            catch (Exception)
            {
                // 记录日志但不抛出异常
            }
        }

        private void RemoveStaleConnections()
        {
            lock (SyncRoot)
            {
                var connectionsToRemove = new List<TConnection>();
                
                foreach (var connection in IdleConnections)
                {
                    if (!ValidateConnection(connection))
                    {
                        connectionsToRemove.Add(connection);
                    }
                }

                foreach (var connection in connectionsToRemove)
                {
                    IdleConnections.Pop();
                    CloseConnection(connection);
                }
            }
        }
    }
} 