using System;
using System.Collections.Generic;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Binding.Binders;
using TBydFramework.Runtime.Binding.Contexts;
using TBydFramework.Runtime.Contexts;
using TBydFramework.XLua.Runtime.Binding.Builder;
using UnityEngine;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding
{
    [LuaCallCSharp]
    public static class LuaGameObjectBindingExtension
    {
        private static IBinder binder;
        public static IBinder Binder
        {
            get
            {
                if (binder == null)
                    binder = Context.GetApplicationContext().GetService<IBinder>();

                if (binder == null)
                    throw new Exception("Data binding service is not initialized,please create a LuaBindingServiceBundle service before using it.");

                return binder;
            }
        }

        public static IBindingContext BindingContext(this GameObject gameObject)
        {
            if (gameObject == null)
                return null;

            BindingContextLifecycle bindingContextLifecycle = gameObject.GetComponent<BindingContextLifecycle>();
            if (bindingContextLifecycle == null)
                bindingContextLifecycle = gameObject.AddComponent<BindingContextLifecycle>();

            IBindingContext bindingContext = bindingContextLifecycle.BindingContext;
            if (bindingContext == null)
            {
                bindingContext = new BindingContext(gameObject, Binder);
                bindingContextLifecycle.BindingContext = bindingContext;
            }
            return bindingContext;
        }

        public static LuaBindingSet CreateBindingSet(this GameObject gameObject)
        {
            IBindingContext context = gameObject.BindingContext();
            return new LuaBindingSet(context, gameObject);
        }

        public static void SetDataContext(this GameObject gameObject, object dataContext)
        {
            gameObject.BindingContext().DataContext = dataContext;
        }

        public static void AddBinding(this GameObject gameObject, BindingDescription bindingDescription)
        {
            gameObject.BindingContext().Add(gameObject, bindingDescription);
        }

        public static void AddBindings(this GameObject gameObject, IEnumerable<BindingDescription> bindingDescriptions)
        {
            gameObject.BindingContext().Add(gameObject, bindingDescriptions);
        }

        public static void AddBinding(this GameObject gameObject, object target, BindingDescription bindingDescription, object key = null)
        {
            gameObject.BindingContext().Add(target, bindingDescription, key);
        }

        public static void AddBindings(this GameObject gameObject, object target, IEnumerable<BindingDescription> bindingDescriptions, object key = null)
        {
            gameObject.BindingContext().Add(target, bindingDescriptions, key);
        }

        public static void ClearBindings(this GameObject gameObject, object key)
        {
            gameObject.BindingContext().Clear(key);
        }

        public static void ClearAllBindings(this GameObject gameObject)
        {
            gameObject.BindingContext().Clear();
        }
    }
}
