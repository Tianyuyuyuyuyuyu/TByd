using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 连接池的抽象基类，提供连接池的基本实现
    /// </summary>
    /// <typeparam name="TConnection">连接对象类型</typeparam>
    public abstract class ConnectionPoolBase<TConnection> : IConnectionPool<TConnection>
        where TConnection : class
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ConcurrentQueue<TConnection> _idleConnections;
        private readonly ConcurrentDictionary<TConnection, DateTime> _activeConnections;
        private readonly int _maxSize;
        private readonly TimeSpan _connectionTimeout;
        private readonly TimeSpan _idleTimeout;
        private bool _isDisposed;
        private readonly object _syncRoot = new object();

        protected ConnectionPoolBase(int maxSize, TimeSpan? connectionTimeout = null, TimeSpan? idleTimeout = null)
        {
            _maxSize = maxSize;
            _connectionTimeout = connectionTimeout ?? TimeSpan.FromSeconds(30);
            _idleTimeout = idleTimeout ?? TimeSpan.FromMinutes(5);
            
            _semaphore = new SemaphoreSlim(maxSize, maxSize);
            _idleConnections = new ConcurrentQueue<TConnection>();
            _activeConnections = new ConcurrentDictionary<TConnection, DateTime>();
            
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

        /// <summary>
        /// 同步获取连接（不推荐使用，建议使用 AcquireAsync）
        /// </summary>
        public virtual TConnection Acquire()
        {
            ThrowIfDisposed();
            _semaphore.Wait();

            try
            {
                TConnection connection;
                while (_idleConnections.TryDequeue(out connection))
                {
                    if (ValidateConnection(connection))
                    {
                        _activeConnections.TryAdd(connection, DateTime.UtcNow);
                        return connection;
                    }
                    CloseConnection(connection);
                }

                connection = CreateConnection();
                _activeConnections.TryAdd(connection, DateTime.UtcNow);
                return connection;
            }
            catch
            {
                _semaphore.Release();
                throw;
            }
        }

        /// <summary>
        /// 异步获取连接（推荐使用此方法）
        /// </summary>
        public virtual async Task<TConnection> AcquireAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                TConnection connection;
                while (_idleConnections.TryDequeue(out connection))
                {
                    if (ValidateConnection(connection))
                    {
                        _activeConnections.TryAdd(connection, DateTime.UtcNow);
                        return connection;
                    }
                    CloseConnection(connection);
                }

                connection = CreateConnection();
                _activeConnections.TryAdd(connection, DateTime.UtcNow);
                return connection;
            }
            catch
            {
                _semaphore.Release();
                throw;
            }
        }

        public virtual void Release(TConnection connection)
        {
            ThrowIfDisposed();
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            DateTime lastUsed;
            if (!_activeConnections.TryRemove(connection, out lastUsed))
            {
                throw new InvalidOperationException("试图释放一个不属于此池的连接");
            }

            if (ValidateConnection(connection))
            {
                _idleConnections.Enqueue(connection);
            }
            else
            {
                CloseConnection(connection);
            }

            _semaphore.Release();
        }

        public int Count => _idleConnections.Count;
        
        public int ActiveCount => _activeConnections.Count;
        
        public bool IsDisposed => _isDisposed;

        public virtual void Clear()
        {
            ThrowIfDisposed();
            
            lock (_syncRoot)
            {
                while (_idleConnections.TryDequeue(out var connection))
                {
                    CloseConnection(connection);
                }

                foreach (var kvp in _activeConnections)
                {
                    CloseConnection(kvp.Key);
                }
                _activeConnections.Clear();
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
                    _idleConnections.Enqueue(connection);
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
            lock (_syncRoot)
            {
                var staleConnections = new List<TConnection>();
                
                foreach (var connection in _idleConnections)
                {
                    if (!ValidateConnection(connection))
                    {
                        staleConnections.Add(connection);
                    }
                }

                foreach (var connection in staleConnections)
                {
                    if (_idleConnections.TryDequeue(out _))
                    {
                        CloseConnection(connection);
                    }
                }
            }
        }
    }
} 