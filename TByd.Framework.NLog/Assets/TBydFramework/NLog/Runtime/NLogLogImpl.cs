using System;
using NLog;
using TBydFramework.Runtime.Log;

namespace TBydFramework.NLog.Runtime
{
    public class NLogLogImpl : Logger, ILog
    {
        public void Debug(object message, Exception exception)
        {
            Debug(exception, (string)message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Debug(format, args);
        }

        public void Error(object message, Exception exception)
        {
            Error(exception, (string)message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Error(format, args);
        }

        public void Fatal(object message, Exception exception)
        {
            Fatal(exception, (string)message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Fatal(format, args);
        }

        public void Info(object message, Exception exception)
        {
            Info(exception, (string)message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Info(format, args);
        }

        public void Warn(object message, Exception exception)
        {
            Warn(exception, (string)message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Warn(format, args);
        }
    }
}
