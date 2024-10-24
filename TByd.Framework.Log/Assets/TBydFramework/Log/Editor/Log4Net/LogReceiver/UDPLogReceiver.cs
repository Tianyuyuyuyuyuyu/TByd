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
    public sealed class UdpLogReceiver : ILogReceiver
    {
        private readonly int _port;

        private UdpClient _udpClient;

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        private readonly Dictionary<string, TerminalInfo> _terminalInfos = new Dictionary<string, TerminalInfo>();

        public event MessageHandler MessageReceived;

        public bool Started { get; private set; }

        public int Port => _port;

        public UdpLogReceiver(int port)
        {
            _port = port;
        }

        public IPEndPoint LocalEndPoint => (IPEndPoint)_udpClient.Client.LocalEndPoint;

        public void Start()
        {
            try
            {
                _udpClient = new UdpClient(this._port);
                _udpClient.BeginReceive(ReceiveUdpMessage, _udpClient);
                Started = true;
            }
            catch (Exception e)
            {
                Started = false;
                UnityEngine.Debug.LogError(e);
                throw;
            }
        }

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
                UnityEngine.Debug.LogError(e);
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
                        UnityEngine.Debug.LogError(e);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
            }

            client.BeginReceive(ReceiveUdpMessage, result.AsyncState);
        }

        #region IDisposable Support

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Stop();
                _disposed = true;
            }
        }

        ~UdpLogReceiver()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}