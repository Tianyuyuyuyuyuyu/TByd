﻿using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;

namespace Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._08_MonoBehaviour
{
    public class MonoBehaviourAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(MonoBehaviour);
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
        //为了完整实现MonoBehaviour的所有特性，这个Adapter还得扩展，这里只抛砖引玉，只实现了最常用的Awake, Start和Update
        public class Adapter : MonoBehaviour, CrossBindingAdaptorType
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

            public global::ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }

            IMethod mAwakeMethod;
            bool mAwakeMethodGot;
            public void Awake()
            {
                //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
                if (instance != null)
                {
                    if (!mAwakeMethodGot)
                    {
                        mAwakeMethod = instance.Type.GetMethod("Awake", 0);
                        mAwakeMethodGot = true;
                    }

                    if (mAwakeMethod != null)
                    {
                        appdomain.Invoke(mAwakeMethod, instance, null);
                    }
                }
            }

            IMethod mStartMethod;
            bool mStartMethodGot;
            void Start()
            {
                if (!mStartMethodGot)
                {
                    mStartMethod = instance.Type.GetMethod("Start", 0);
                    mStartMethodGot = true;
                }

                if (mStartMethod != null)
                {
                    appdomain.Invoke(mStartMethod, instance, null);
                }
            }

            IMethod mUpdateMethod;
            bool mUpdateMethodGot;
            void Update()
            {
                if (!mUpdateMethodGot)
                {
                    mUpdateMethod = instance.Type.GetMethod("Update", 0);
                    mUpdateMethodGot = true;
                }

                if (mUpdateMethod != null)
                {
                    appdomain.Invoke(mUpdateMethod, instance, null);
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
