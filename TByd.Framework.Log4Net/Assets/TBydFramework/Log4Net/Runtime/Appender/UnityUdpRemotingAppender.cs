using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net.Appender;
using log4net.Core;
using UnityEngine;

namespace TBydFramework.Log4Net.Runtime.Appender
{
    public class UnityUdpRemotingAppender : UdpAppender
    {
        private static readonly BinaryFormatter formatter = new BinaryFormatter();

        public UnityUdpRemotingAppender()
        {
#if UNITY_IOS
			Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
        }

        override protected bool RequiresLayout
        {
            get { return false; }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                using (MemoryStream writer = new MemoryStream())
                {
                    //var fix = FixFlags.All; //FixFlags.Domain | FixFlags.Exception | FixFlags.Identity | FixFlags.LocationInfo | FixFlags.Message | FixFlags.ThreadName | FixFlags.UserName;
                    var fix = FixFlags.Domain | FixFlags.Exception | FixFlags.Identity | FixFlags.LocationInfo | FixFlags.Message | FixFlags.ThreadName | FixFlags.UserName;
                    formatter.Serialize(writer, new LoggingData(loggingEvent.GetLoggingEventData(fix)));
                    byte[] buffer = writer.ToArray();
                    this.Client.Send(buffer, buffer.Length, this.RemoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Unable to send logging event to remote host {0} on port {1}.Error:{2}", this.RemoteAddress.ToString(), this.RemotePort, ex);
            }
        }
    }
}