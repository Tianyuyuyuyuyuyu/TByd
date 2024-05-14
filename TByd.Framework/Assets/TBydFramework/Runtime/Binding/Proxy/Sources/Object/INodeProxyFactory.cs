using TBydFramework.Runtime.Binding.Paths;

namespace TBydFramework.Runtime.Binding.Proxy.Sources.Object
{
    public interface INodeProxyFactory
    {
        ISourceProxy Create(object source, PathToken token);
    }
}
