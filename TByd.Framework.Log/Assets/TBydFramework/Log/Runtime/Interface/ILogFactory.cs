using System;

namespace TBydFramework.Log
{
    public interface ILogFactory
    {
        ILog GetLogger<T>();

        ILog GetLogger(Type type);

        ILog GetLogger(string name);
    }
}
