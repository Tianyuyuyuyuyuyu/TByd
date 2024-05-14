namespace TBydFramework.Runtime.Binding.Reflection
{
    public interface IProxyInvoker: IInvoker
    {
        IProxyMethodInfo ProxyMethodInfo { get; }
    }
}
