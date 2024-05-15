using System;
using TBydFramework.FairyGUI.Runtime.Binding.Proxy;
using TBydFramework.Runtime.Binding.Proxy.Targets;
using TBydFramework.Runtime.Services;

namespace TBydFramework.FairyGUI.Runtime.Binding
{
    public class FairyGUIBindingServiceBundle : AbstractServiceBundle
    {
        public FairyGUIBindingServiceBundle(IServiceContainer container) : base(container)
        {
        }

        protected override void OnStart(IServiceContainer container)
        {
            var targetFactory = container.Resolve<ITargetProxyFactoryRegister>();
            if (targetFactory == null)
                throw new Exception("Data binding service is not initialized,please create a BindingServiceBundle service before using it.");

            targetFactory.Register(new FairyTargetProxyFactory(), 20);
        }

        protected override void OnStop(IServiceContainer container)
        {
        }
    }
}