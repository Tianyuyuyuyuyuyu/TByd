using System;

namespace TBydFramework.Log.Runtime
{
    public interface ILogFactory
    {
        ILog GetLogger<T>();

        ILog GetLogger(Type type);

        ILog GetLogger(string name);
    }
}
