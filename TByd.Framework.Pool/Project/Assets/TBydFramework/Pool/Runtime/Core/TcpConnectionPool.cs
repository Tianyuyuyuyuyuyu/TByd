using System;
using System.Net.Sockets;
using TBydFramework.Pool.Runtime.Base;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// TCP连接池的具体实现
    /// </summary>
    public sealed class TcpConnectionPool : ConnectionPoolBase<TcpClient>
    {
        private readonly string _host;
        private readonly int _port;
        private readonly int _receiveTimeout;
        private readonly int _sendTimeout;

        /// <summary>
        /// 初始化TCP连接池
        /// </summary>
        /// <param name="host">服务器主机地址</param>
        /// <param name="port">服务器端口</param>
        /// <param name="maxSize">池的最大容量</param>
        /// <param name="connectionTimeout">连接超时时间</param>
        /// <param name="idleTimeout">空闲超时时间</param>
        /// <param name="receiveTimeout">接收超时时间(毫秒)</param>
        /// <param name="sendTimeout">发送超时时间(毫秒)</param>
        public TcpConnectionPool(
            string host,
            int port,
            int maxSize = 10,
            TimeSpan? connectionTimeout = null,
            TimeSpan? idleTimeout = null,
            int receiveTimeout = 30000,
            int sendTimeout = 30000) 
            : base(maxSize, connectionTimeout, idleTimeout)
        {
            _host = host;
            _port = port;
            _receiveTimeout = receiveTimeout;
            _sendTimeout = sendTimeout;
        }

        protected override TcpClient CreateConnection()
        {
            var client = new TcpClient();
            client.ReceiveTimeout = _receiveTimeout;
            client.SendTimeout = _sendTimeout;
            
            try
            {
                client.Connect(_host, _port);
                return client;
            }
            catch
            {
                client.Dispose();
                throw;
            }
        }

        protected override bool ValidateConnection(TcpClient connection)
        {
            if (connection == null) return false;
            
            try
            {
                return connection.Connected && connection.Client.Poll(0, SelectMode.SelectWrite);
            }
            catch
            {
                return false;
            }
        }

        protected override void CloseConnection(TcpClient connection)
        {
            try
            {
                connection?.Close();
                connection?.Dispose();
            }
            catch
            {
                // 记录日志但不抛出异常
            }
        }
    }
} 