using System;

namespace TBydFramework.Log.Runtime
{
    public class Log4NetFactory : Interface.ILogFactory
    {
        public Interface.ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public Interface.ILog GetLogger(Type type)
        {
            var log = log4net.LogManager.GetLogger(type);
            return new Log4NetLogImpl(log);
        }

        public Interface.ILog GetLogger(string name)
        {
            var log = log4net.LogManager.GetLogger(name);
            return new Log4NetLogImpl(log);
        }
    }
}