using TBydFramework.ILRuntime.Runtime.Binding.Proxy.Sources.Object;
using TBydFramework.ILRuntime.Runtime.Binding.Proxy.Targets.ILRuntime;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Binding.Proxy.Sources.Object;
using TBydFramework.Runtime.Binding.Proxy.Targets;
using TBydFramework.Runtime.Services;

namespace TBydFramework.ILRuntime.Runtime.Binding
{
    public class ILRuntimeBindingServiceBundle : BindingServiceBundle
    {
        public ILRuntimeBindingServiceBundle(IServiceContainer container) : base(container)
        {
        }

        protected override void OnStart(IServiceContainer container)
        {
            base.OnStart(container);

            /* Support ILruntime */
            INodeProxyFactoryRegister objectSourceProxyFactoryRegistry = container.Resolve<INodeProxyFactoryRegister>();
            objectSourceProxyFactoryRegistry.Register(new ILRuntimeNodeProxyFactory(), 20);

            ITargetProxyFactoryRegister targetProxyFactoryRegister = container.Resolve<ITargetProxyFactoryRegister>();
            targetProxyFactoryRegister.Register(new ILRuntimeTargetProxyFactory(), 30);
        }
    }
}
