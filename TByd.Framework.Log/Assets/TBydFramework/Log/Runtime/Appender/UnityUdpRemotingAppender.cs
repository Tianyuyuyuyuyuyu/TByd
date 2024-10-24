using log4net.Appender;
using log4net.Core;
using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using TBydFramework.Log.Runtime.Serialization;

namespace TBydFramework.Log.Runtime.Appender
{
    /// <summary>
    /// Unity UDP远程日志追加器,用于将日志通过UDP发送到远程服务器。
    /// </summary>
    public class UnityUdpRemotingAppender : UdpAppender
    {
        private static readonly BinaryFormatter _formatter = new BinaryFormatter();

        /// <summary>
        /// 初始化 UnityUdpRemotingAppender 的新实例。
        /// </summary>
        public UnityUdpRemotingAppender()
        {
#if UNITY_IOS
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
        }

        /// <summary>
        /// 获取一个值,指示此追加器是否需要布局。
        /// </summary>
        protected override bool RequiresLayout => false;

        /// <summary>
        /// 将日志事件追加到输出。
        /// </summary>
        /// <param name="loggingEvent">要记录的日志事件。</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                using (MemoryStream writer = new MemoryStream())
                {
                    var fix = FixFlags.Domain | FixFlags.Exception | FixFlags.Identity | 
                              FixFlags.LocationInfo | FixFlags.Message | FixFlags.ThreadName | 
                              FixFlags.UserName;
                    _formatter.Serialize(writer, new LoggingData(loggingEvent.GetLoggingEventData(fix)));
                    byte[] buffer = writer.ToArray();
                    Client.Send(buffer, buffer.Length, RemoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("无法将日志事件发送到远程主机 {0} 的端口 {1}。错误:{2}", 
                                     RemoteAddress, RemotePort, ex);
            }
        }
    }
}
