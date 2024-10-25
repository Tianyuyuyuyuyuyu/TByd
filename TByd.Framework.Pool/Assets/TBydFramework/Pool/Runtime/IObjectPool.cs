using System;

namespace TBydFramework.Pool.Runtime
{
    public interface IObjectPool<T> : IDisposable
    {
        T Rent();
        void Return(T obj);
    }
}