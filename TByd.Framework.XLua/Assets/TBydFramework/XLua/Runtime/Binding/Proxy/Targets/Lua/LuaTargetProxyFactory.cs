using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Binding.Proxy.Targets;
using TBydFramework.Runtime.Binding.Proxy.Targets.Universal;
using TBydFramework.Runtime.Interactivity;
using TBydFramework.Runtime.Observables;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding.Proxy.Targets.Lua
{
    public class LuaTargetProxyFactory : ITargetProxyFactory
    {
        public ITargetProxy CreateProxy(object target, BindingDescription description)
        {
            if (target == null || !(target is ILuaExtendable))
                return null;

            LuaTable metatable = (target as ILuaExtendable).GetMetatable();
            if (metatable == null || !metatable.ContainsKey(description.TargetName))
                return null;

            var obj = metatable.Get<object>(description.TargetName);
            if (obj != null)
            {
                LuaFunction function = obj as LuaFunction;
                if (function != null)
                    return new LuaMethodTargetProxy(target, function);

                IObservableProperty observableValue = obj as IObservableProperty;
                if (observableValue != null)
                    return new ObservableTargetProxy(target, observableValue);

                IInteractionAction interactionAction = obj as IInteractionAction;
                if (interactionAction != null)
                    return new InteractionTargetProxy(target, interactionAction);
            }
            return new LuaTableTargetProxy(target, description.TargetName);
        }
    }
}