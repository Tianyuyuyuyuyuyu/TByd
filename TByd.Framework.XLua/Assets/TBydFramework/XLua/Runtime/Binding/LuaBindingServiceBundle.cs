using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Binding.Paths;
using TBydFramework.Runtime.Binding.Proxy.Sources;
using TBydFramework.Runtime.Binding.Proxy.Sources.Object;
using TBydFramework.Runtime.Binding.Proxy.Targets;
using TBydFramework.Runtime.Services;
using TBydFramework.XLua.Runtime.Binding.Proxy.Sources.Expressions;
using TBydFramework.XLua.Runtime.Binding.Proxy.Sources.Object;
using TBydFramework.XLua.Runtime.Binding.Proxy.Targets.Lua;

namespace TBydFramework.XLua.Runtime.Binding
{
    public class LuaBindingServiceBundle : BindingServiceBundle
    {
        public LuaBindingServiceBundle(IServiceContainer container) : base(container)
        {
        }

        protected override void OnStart(IServiceContainer container)
        {
            base.OnStart(container);

            /* Support XLua */
            INodeProxyFactoryRegister objectSourceProxyFactoryRegistry = container.Resolve<INodeProxyFactoryRegister>();
            objectSourceProxyFactoryRegistry.Register(new LuaNodeProxyFactory(), 20);

            IPathParser pathParser = container.Resolve<IPathParser>();
            ISourceProxyFactory sourceFactoryService = container.Resolve<ISourceProxyFactory>();
            ISourceProxyFactoryRegistry sourceProxyFactoryRegistry = container.Resolve<ISourceProxyFactoryRegistry>();
            sourceProxyFactoryRegistry.Register(new LuaExpressionSourceProxyFactory(sourceFactoryService, pathParser), 20);

            ITargetProxyFactoryRegister targetProxyFactoryRegister = container.Resolve<ITargetProxyFactoryRegister>();
            targetProxyFactoryRegister.Register(new LuaTargetProxyFactory(), 30);
        }

        protected override void OnStop(IServiceContainer container)
        {
            base.OnStop(container);
        }
    }
}