using System;

namespace TBydFramework.Module.Pool.Runtime
{
    public interface IObjectPool<T> : IDisposable
    {
        T Rent();
        void Return(T obj);
    }
}