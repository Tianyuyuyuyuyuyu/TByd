﻿using System;
using System.Collections.Generic;
using TBydFramework.Runtime.Binding.Proxy;
using TBydFramework.Runtime.Binding.Proxy.Sources;
using TBydFramework.Runtime.Binding.Proxy.Sources.Expressions;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding.Proxy.Sources.Expressions
{
    public class LuaExpressionSourceProxy : NotifiableSourceProxyBase, IExpressionSourceProxy
    {
        private bool disposed = false;
        private LuaFunction func;
        private List<ISourceProxy> inners = new List<ISourceProxy>();

        public LuaExpressionSourceProxy(object source, LuaFunction func, List<ISourceProxy> inners) : base(source)
        {
            this.func = func;
            this.inners = inners;

            if (this.inners == null || this.inners.Count <= 0)
                return;

            foreach (ISourceProxy proxy in this.inners)
            {
                if (proxy is INotifiable)
                    ((INotifiable)proxy).ValueChanged += OnValueChanged;
            }
        }

        public override Type Type { get { return typeof(object); } }

        public virtual object GetValue()
        {
            if (this.source == null)
                return null;

            object[] results = func.Call(this.source);
            if (results == null || results.Length <= 0)
                return null;
            return results[0];
        }

        public virtual TValue GetValue<TValue>()
        {
            return (TValue)this.GetValue();
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            RaiseValueChanged();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (this.inners != null && this.inners.Count > 0)
                    {
                        foreach (ISourceProxy proxy in this.inners)
                        {
                            if (proxy is INotifiable)
                                ((INotifiable)proxy).ValueChanged -= OnValueChanged;
                            proxy.Dispose();
                        }
                        this.inners.Clear();
                    }
                }
                disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}