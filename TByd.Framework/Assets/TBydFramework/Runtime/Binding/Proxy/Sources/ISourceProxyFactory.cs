namespace TBydFramework.Runtime.Binding.Proxy.Sources
{
    public interface ISourceProxyFactory
    {
        ISourceProxy CreateProxy(object source, SourceDescription description);
    }
}
