using System;
using TBydFramework.Runtime.Binding.Proxy.Targets;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding.Proxy.Targets.Lua
{
    public class LuaTableTargetProxy : ValueTargetProxyBase
    {
        protected readonly string key;
        protected readonly LuaTable metatable;
        public LuaTableTargetProxy(object target, string key) : base(target)
        {
            if (target is ILuaExtendable)
                this.metatable = (target as ILuaExtendable).GetMetatable();
            this.key = key;
        }

        public override Type Type { get { return typeof(object); } }

        public override object GetValue()
        {
            return this.metatable.Get<string, object>(this.key);
        }

        public override TValue GetValue<TValue>()
        {
            return this.metatable.Get<string, TValue>(this.key);
        }

        public override void SetValue(object value)
        {
            this.metatable.Set<string, object>(this.key, value);
        }

        public override void SetValue<TValue>(TValue value)
        {
            this.metatable.Set<string, TValue>(this.key, value);
        }
    }
}
