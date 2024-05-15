using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._04_Inheritance
{   
    public class TestClassBaseAdapter : CrossBindingAdaptor
    {
        static CrossBindingFunctionInfo<System.Int32> mget_Value_0 = new CrossBindingFunctionInfo<System.Int32>("get_Value");
        static CrossBindingMethodInfo<System.Int32> mset_Value_1 = new CrossBindingMethodInfo<System.Int32>("set_Value");
        static CrossBindingMethodInfo<System.String> mTestVirtual_2 = new CrossBindingMethodInfo<System.String>("TestVirtual");
        static CrossBindingMethodInfo<System.Int32> mTestAbstract_3 = new CrossBindingMethodInfo<System.Int32>("TestAbstract");
        public override Type BaseCLRType
        {
            get
            {
                return typeof(global::Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._04_Inheritance.TestClassBase);
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

        public class Adapter : global::Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._04_Inheritance.TestClassBase, CrossBindingAdaptorType
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

            public override void TestVirtual(System.String str)
            {
                if (mTestVirtual_2.CheckShouldInvokeBase(this.instance))
                    base.TestVirtual(str);
                else
                    mTestVirtual_2.Invoke(this.instance, str);
            }

            public override void TestAbstract(System.Int32 gg)
            {
                mTestAbstract_3.Invoke(this.instance, gg);
            }

            public override System.Int32 Value
            {
            get
            {
                if (mget_Value_0.CheckShouldInvokeBase(this.instance))
                    return base.Value;
                else
                    return mget_Value_0.Invoke(this.instance);

            }
            set
            {
                if (mset_Value_1.CheckShouldInvokeBase(this.instance))
                    base.Value = value;
                else
                    mset_Value_1.Invoke(this.instance, value);

            }
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

