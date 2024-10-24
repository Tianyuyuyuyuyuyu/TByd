using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using TBydFramework.Log.Editor.Log4Net.Views;
using TBydFramework.Log.Runtime.Serialization;

namespace TBydFramework.Log.Editor.Log4Net.LogReceiver
{
    /// <summary>
    /// UDP日志接收器,用于接收通过UDP发送的日志消息。
    /// </summary>
    public sealed class UdpLogReceiver : ILogReceiver
    {
        private readonly int _port;

        private UdpClient _udpClient;

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        private readonly Dictionary<string, TerminalInfo> _terminalInfos = new Dictionary<string, TerminalInfo>();

        /// <summary>
        /// 当接收到消息时触发的事件。
        /// </summary>
        public event MessageHandler MessageReceived;

        /// <summary>
        /// 获取一个值,指示接收器是否已启动。
        /// </summary>
        public bool Started { get; private set; }

        /// <summary>
        /// 获取接收器使用的端口。
        /// </summary>
        public int Port => _port;

        /// <summary>
        /// 初始化 UdpLogReceiver 的新实例。
        /// </summary>
        /// <param name="port">要监听的端口。</param>
        public UdpLogReceiver(int port)
        {
            _port = port;
        }

        /// <summary>
        /// 获取本地终结点。
        /// </summary>
        public IPEndPoint LocalEndPoint => (IPEndPoint)_udpClient?.Client.LocalEndPoint;

        /// <summary>
        /// 启动日志接收器。
        /// </summary>
        public void Start()
        {
            try
            {
                _udpClient = new UdpClient(_port);
                _udpClient.BeginReceive(ReceiveUdpMessage, _udpClient);
                Started = true;
            }
            catch (Exception e)
            {
                Started = false;
                UnityEngine.Debug.LogError($"启动UDP日志接收器时发生错误: {e}");
                throw;
            }
        }

        /// <summary>
        /// 停止日志接收器。
        /// </summary>
        public void Stop()
        {
            try
            {
                if (_udpClient != null)
                {
                    _udpClient.Close();
                    _udpClient = null;
                }

                Started = false;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"停止UDP日志接收器时发生错误: {e}");
            }
        }

        private void ReceiveUdpMessage(IAsyncResult result)
        {
            var client = (UdpClient)result.AsyncState;
            if (client == null) return;

            try
            {
                var remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                var buffer = client.EndReceive(result, ref remoteIPEndPoint);
                if (buffer != null)
                {
                    LoggingData loggingData = (LoggingData)_binaryFormatter.Deserialize(new MemoryStream(buffer));
                    try
                    {
                        if (MessageReceived != null)
                        {
                            var key = remoteIPEndPoint.ToString();
                            if (!_terminalInfos.TryGetValue(key, out var terminalInfo))
                            {
                                terminalInfo = new TerminalInfo(loggingData.UserName,
                                    remoteIPEndPoint.Address.ToString(), remoteIPEndPoint.Port);
                                _terminalInfos.Add(key, terminalInfo);
                            }

                            MessageReceived(terminalInfo, loggingData);
                        }
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError($"处理接收到的日志消息时发生错误: {e}");
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"接收UDP消息时发生错误: {e}");
            }

            client.BeginReceive(ReceiveUdpMessage, result.AsyncState);
        }

        #region IDisposable Support

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// 释放 UdpLogReceiver 使用的所有资源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
