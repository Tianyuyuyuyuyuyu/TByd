using System;
using System.Reflection;
using TBydFramework.FairyGUI.Runtime.Event;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Binding.Proxy.Targets;
using TBydFramework.Runtime.Binding.Reflection;
using TBydFramework.Runtime.Observables;

namespace TBydFramework.FairyGUI.Runtime.Binding.Proxy
{
    public class FairyTargetProxyFactory : ITargetProxyFactory
    {
        public ITargetProxy CreateProxy(object target, BindingDescription description)
        {
            IProxyType type = target.GetType().AsProxy();
            IProxyMemberInfo memberInfo = type.GetMember(description.TargetName);
            if (memberInfo == null)
                memberInfo = type.GetMember(description.TargetName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (memberInfo == null)
                throw new MissingMemberException(type.Type.FullName, description.TargetName);

            EventListener updateTrigger = null;
            if (!string.IsNullOrEmpty(description.UpdateTrigger))
            {
                IProxyPropertyInfo updateTriggerPropertyInfo = type.GetProperty(description.UpdateTrigger);
                IProxyFieldInfo updateTriggerFieldInfo = updateTriggerPropertyInfo == null ? type.GetField(description.UpdateTrigger) : null;
                if (updateTriggerPropertyInfo != null)
                    updateTrigger = updateTriggerPropertyInfo.GetValue(target) as EventListener;

                if (updateTriggerFieldInfo != null)
                    updateTrigger = updateTriggerFieldInfo.GetValue(target) as EventListener;

                if (updateTriggerPropertyInfo == null && updateTriggerFieldInfo == null)
                    throw new MissingMemberException(type.Type.FullName, description.UpdateTrigger);

                //Other Property Type
                if (updateTrigger == null) /* by UniversalTargetProxyFactory */
                    return null;
            }

            var propertyInfo = memberInfo as IProxyPropertyInfo;
            if (propertyInfo != null)
            {
                if (typeof(IObservableProperty).IsAssignableFrom(propertyInfo.ValueType))
                    return null;

                if (typeof(EventListener).IsAssignableFrom(propertyInfo.ValueType))
                {
                    //Event Type
                    object listener = propertyInfo.GetValue(target);
                    if (listener == null)
                        throw new NullReferenceException(propertyInfo.Name);

                    return new FairyEventProxy(target, (EventListener)listener);
                }

                //Other Property Type
                if (updateTrigger == null)/* by UniversalTargetProxyFactory */
                    return null;

                return new FairyPropertyProxy(target, propertyInfo, updateTrigger);
            }

            var fieldInfo = memberInfo as IProxyFieldInfo;
            if (fieldInfo != null)
            {
                if (typeof(IObservableProperty).IsAssignableFrom(fieldInfo.ValueType))
                    return null;

                if (typeof(EventListener).IsAssignableFrom(fieldInfo.ValueType))
                {
                    //Event Type
                    object listener = fieldInfo.GetValue(target);
                    if (listener == null)
                        throw new NullReferenceException(fieldInfo.Name);

                    return new FairyEventProxy(target, (EventListener)listener);
                }

                //Other Property Type
                if (updateTrigger == null)/* by UniversalTargetProxyFactory */
                    return null;

                return new FairyFieldProxy(target, fieldInfo, updateTrigger);
            }

            return null;
        }

    }
}