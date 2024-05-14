namespace TBydFramework.Runtime.Binding.Proxy.Targets
{
    public interface ITargetProxyFactory
    {
        ITargetProxy CreateProxy(object target, BindingDescription description);
    }
}
