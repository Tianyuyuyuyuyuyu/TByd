﻿using System;
using TBydFramework.Runtime.Log;

namespace TBydFramework.Runtime.Binding.Proxy.Sources
{
    public abstract class SourceProxyBase : BindingProxyBase, ISourceProxy
    {
        protected TypeCode typeCode = TypeCode.Empty;
        protected readonly object source;
        public SourceProxyBase(object source)
        {
            this.source = source;
        }

        public abstract Type Type { get; }

        public virtual TypeCode TypeCode
        {
            get
            {
                if (typeCode == TypeCode.Empty)
                {
#if NETFX_CORE
                    typeCode = WinRTLegacy.TypeExtensions.GetTypeCode(Type);
#else
                    typeCode = Type.GetTypeCode(Type);
#endif
                }
                return typeCode;
            }
        }

        public virtual object Source { get { return this.source; } }
    }

    public abstract class NotifiableSourceProxyBase : SourceProxyBase, INotifiable
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NotifiableSourceProxyBase));

        protected readonly object _lock = new object();
        protected EventHandler valueChanged;

        public NotifiableSourceProxyBase(object source) : base(source)
        {
        }

        public virtual event EventHandler ValueChanged
        {
            add
            {
                lock (_lock) { this.valueChanged += value; }
            }

            remove
            {
                lock (_lock) { this.valueChanged -= value; }
            }
        }

        protected virtual void RaiseValueChanged()
        {
            try
            {
                if (this.valueChanged != null)
                    this.valueChanged(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.Warn(e);
            }
        }
    }
}
