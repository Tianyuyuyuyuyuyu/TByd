using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace TBydFramework.ILRuntime.Runtime.ILRuntime.Adapters
{   
    public class ViewAdapter : CrossBindingAdaptor
    {
        static CrossBindingFunctionInfo<System.String> mget_Name_0 = new CrossBindingFunctionInfo<System.String>("get_Name");
        static CrossBindingMethodInfo<System.String> mset_Name_1 = new CrossBindingMethodInfo<System.String>("set_Name");
        static CrossBindingFunctionInfo<UnityEngine.Transform> mget_Parent_2 = new CrossBindingFunctionInfo<UnityEngine.Transform>("get_Parent");
        static CrossBindingFunctionInfo<UnityEngine.GameObject> mget_Owner_3 = new CrossBindingFunctionInfo<UnityEngine.GameObject>("get_Owner");
        static CrossBindingFunctionInfo<UnityEngine.Transform> mget_Transform_4 = new CrossBindingFunctionInfo<UnityEngine.Transform>("get_Transform");
        static CrossBindingFunctionInfo<System.Boolean> mget_Visibility_5 = new CrossBindingFunctionInfo<System.Boolean>("get_Visibility");
        static CrossBindingMethodInfo<System.Boolean> mset_Visibility_6 = new CrossBindingMethodInfo<System.Boolean>("set_Visibility");
        static CrossBindingMethodInfo mOnEnable_7 = new CrossBindingMethodInfo("OnEnable");
        static CrossBindingMethodInfo mOnDisable_8 = new CrossBindingMethodInfo("OnDisable");
        static CrossBindingFunctionInfo<TBydFramework.Runtime.Views.IAttributes> mget_ExtraAttributes_9 = new CrossBindingFunctionInfo<TBydFramework.Runtime.Views.IAttributes>("get_ExtraAttributes");
        static CrossBindingMethodInfo mOnVisibilityChanged_10 = new CrossBindingMethodInfo("OnVisibilityChanged");
        public override Type BaseCLRType
        {
            get
            {
                return typeof(TBydFramework.Runtime.Views.View);
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

        public class Adapter : TBydFramework.Runtime.Views.View, CrossBindingAdaptorType, IBehaviourAdapter
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

            public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

            public AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }

            protected override void OnEnable()
            {
                if (this.instance == null)
                    return;

                if (mOnEnable_7.CheckShouldInvokeBase(this.instance))
                    base.OnEnable();
                else
                    mOnEnable_7.Invoke(this.instance);
            }

            protected override void OnDisable()
            {
                if (mOnDisable_8.CheckShouldInvokeBase(this.instance))
                    base.OnDisable();
                else
                    mOnDisable_8.Invoke(this.instance);
            }

            protected override void OnVisibilityChanged()
            {
                if (mOnVisibilityChanged_10.CheckShouldInvokeBase(this.instance))
                    base.OnVisibilityChanged();
                else
                    mOnVisibilityChanged_10.Invoke(this.instance);
            }

            public override System.String Name
            {
            get
            {
                if (mget_Name_0.CheckShouldInvokeBase(this.instance))
                    return base.Name;
                else
                    return mget_Name_0.Invoke(this.instance);

            }
            set
            {
                if (mset_Name_1.CheckShouldInvokeBase(this.instance))
                    base.Name = value;
                else
                    mset_Name_1.Invoke(this.instance, value);

            }
            }

            public override UnityEngine.Transform Parent
            {
            get
            {
                if (mget_Parent_2.CheckShouldInvokeBase(this.instance))
                    return base.Parent;
                else
                    return mget_Parent_2.Invoke(this.instance);

            }
            }

            public override UnityEngine.GameObject Owner
            {
            get
            {
                if (mget_Owner_3.CheckShouldInvokeBase(this.instance))
                    return base.Owner;
                else
                    return mget_Owner_3.Invoke(this.instance);

            }
            }

            public override UnityEngine.Transform Transform
            {
            get
            {
                if (mget_Transform_4.CheckShouldInvokeBase(this.instance))
                    return base.Transform;
                else
                    return mget_Transform_4.Invoke(this.instance);

            }
            }

            public override System.Boolean Visibility
            {
            get
            {
                if (mget_Visibility_5.CheckShouldInvokeBase(this.instance))
                    return base.Visibility;
                else
                    return mget_Visibility_5.Invoke(this.instance);

            }
            set
            {
                if (mset_Visibility_6.CheckShouldInvokeBase(this.instance))
                    base.Visibility = value;
                else
                    mset_Visibility_6.Invoke(this.instance, value);

            }
            }

            public override TBydFramework.Runtime.Views.IAttributes ExtraAttributes
            {
            get
            {
                if (mget_ExtraAttributes_9.CheckShouldInvokeBase(this.instance))
                    return base.ExtraAttributes;
                else
                    return mget_ExtraAttributes_9.Invoke(this.instance);

            }
            }

            void IBehaviourAdapter.Awake()
            {                
                this.OnEnable();
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

