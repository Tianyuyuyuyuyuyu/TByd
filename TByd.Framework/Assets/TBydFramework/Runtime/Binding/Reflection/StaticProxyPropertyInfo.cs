﻿using System;
using System.Reflection;
using TBydFramework.Runtime.Log;

namespace TBydFramework.Runtime.Binding.Reflection
{
    public class StaticProxyPropertyInfo<T, TValue> : ProxyPropertyInfo, IProxyPropertyInfo<T, TValue>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StaticProxyPropertyInfo<T, TValue>));

        private Func<TValue> getter;
        private Action<TValue> setter;

        public StaticProxyPropertyInfo(string propertyName) : this(typeof(T).GetProperty(propertyName))
        {
        }

        public StaticProxyPropertyInfo(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            if (!typeof(TValue).Equals(this.propertyInfo.PropertyType) || !propertyInfo.DeclaringType.IsAssignableFrom(typeof(T)))
                throw new ArgumentException("The property types do not match!");

            if (!this.IsStatic)
                throw new ArgumentException($"The property \"{propertyInfo.DeclaringType}.{Name}\" isn't static.");

            this.getter = this.MakeGetter(propertyInfo);
            this.setter = this.MakeSetter(propertyInfo);
        }

        public StaticProxyPropertyInfo(string propertyName, Func<TValue> getter, Action<TValue> setter) : this(typeof(T).GetProperty(propertyName), getter, setter)
        {
        }

        public StaticProxyPropertyInfo(PropertyInfo propertyInfo, Func<TValue> getter, Action<TValue> setter) : base(propertyInfo)
        {
            if (!typeof(TValue).Equals(this.propertyInfo.PropertyType) || !propertyInfo.DeclaringType.IsAssignableFrom(typeof(T)))
                throw new ArgumentException("The property types do not match!");

            if (!this.IsStatic)
                throw new ArgumentException($"The property \"{propertyInfo.DeclaringType}.{Name}\" isn't static.");

            this.getter = getter;
            this.setter = setter;
        }

        public override Type DeclaringType { get { return typeof(T); } }

        private Action<TValue> MakeSetter(PropertyInfo propertyInfo)
        {
            try
            {
                if (this.IsValueType)
                    return null;

                var setMethod = propertyInfo.GetSetMethod();
                if (setMethod == null)
                    return null;
                return (Action<TValue>)setMethod.CreateDelegate(typeof(Action<TValue>));
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("{0}", e);
            }

            return null;
        }

        private Func<TValue> MakeGetter(PropertyInfo propertyInfo)
        {
            try
            {
                if (this.IsValueType)
                    return null;

                var getMethod = propertyInfo.GetGetMethod();
                if (getMethod == null)
                    return null;
                return (Func<TValue>)getMethod.CreateDelegate(typeof(Func<TValue>));
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("{0}", e);
            }
            return null;
        }

        public TValue GetValue(T target)
        {
            if (this.getter != null)
                return this.getter();

            return (TValue)base.GetValue(null);
        }

        TValue IProxyPropertyInfo<TValue>.GetValue(object target)
        {
            return this.GetValue((T)target);
        }

        public override object GetValue(object target)
        {
            if (this.getter != null)
                return this.getter();

            return base.GetValue(target);
        }

        public void SetValue(T target, TValue value)
        {
            if (setter != null)
            {
                setter(value);
                return;
            }

            base.SetValue(null,value);
        }

        public void SetValue(object target, TValue value)
        {
            this.SetValue((T)target, value);
        }

        public override void SetValue(object target, object value)
        {
            if (setter != null)
            {
                setter((TValue)value);
                return;
            }

            base.SetValue(null, value);
        }
    }
}
