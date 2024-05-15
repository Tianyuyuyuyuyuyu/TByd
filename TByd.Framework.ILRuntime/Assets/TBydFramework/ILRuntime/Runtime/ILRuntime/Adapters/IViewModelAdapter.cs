using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace TBydFramework.ILRuntime.Runtime.ILRuntime.Adapters
{   
    public class IViewModelAdapter : CrossBindingAdaptor
    {
        static CrossBindingMethodInfo mDispose_0 = new CrossBindingMethodInfo("Dispose");
        public override Type BaseCLRType
        {
            get
            {
                return typeof(TBydFramework.Runtime.ViewModels.IViewModel);
            }
        }

        public override Type AdaptorType
        {
            get
            {
                return typeof(Adapter);
            }
        }

        public override object CreateCLRInstance(global::ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new Adapter(appdomain, instance);
        }

        public class Adapter : TBydFramework.Runtime.ViewModels.IViewModel, CrossBindingAdaptorType
        {
            ILTypeInstance instance;
            global::ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adapter()
            {

            }

            public Adapter(global::ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance { get { return instance; } }

            public void Dispose()
            {
                mDispose_0.Invoke(this.instance);
            }

            public override string ToString()
            {
                IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
                m = instance.Type.GetVirtualMethod(m);
                if (m == null || m is ILMethod)
                {
                    return instance.ToString();
                }
                else
                    return instance.Type.FullName;
            }
        }
    }
}

