using System;

namespace TBydFramework.Runtime.Binding.Reflection
{
    public interface IProxyFactory
    {
        IProxyType Create(Type type);
    }
}
