﻿using TBydFramework.Runtime.Binding.Builder;
using TBydFramework.Runtime.Binding.Contexts;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding.Builder
{
    [LuaCallCSharp]
    public class LuaBindingSet : BindingSetBase
    {
        private object target;
        public LuaBindingSet(IBindingContext context, object target) : base(context)
        {
            this.target = target;
        }

        public virtual LuaBindingBuilder Bind()
        {
            var builder = new LuaBindingBuilder(this.context, this.target);
            this.builders.Add(builder);
            return builder;
        }

        public virtual LuaBindingBuilder Bind(object target)
        {
            var builder = new LuaBindingBuilder(context, target);
            this.builders.Add(builder);
            return builder;
        }
    }
}