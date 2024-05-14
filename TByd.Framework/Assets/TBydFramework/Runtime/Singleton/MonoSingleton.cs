using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBydFramework.Runtime.Singleton
{
    /// <summary>
    /// 静态类：MonoBehaviour类的单例
    /// 泛型类：Where约束表示T类型必须继承MonoSingleton<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        protected static T mInstance;

        /// <summary>
        /// 静态属性：封装相关实例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                if (mInstance == null && !mOnApplicationQuit)
                {
                    mInstance = SingletonCreator.CreateMonoSingleton<T>();
                }

                return mInstance;
            }
        }

        /// <summary>
        /// 实现接口的单例初始化
        /// </summary>
        public virtual void OnSingletonInit()
        {
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            if (SingletonCreator.IsUnitTestMode)
            {
                var curTrans = transform;
                do
                {
                    var parent = curTrans.parent;
                    DestroyImmediate(curTrans.gameObject);
                    curTrans = parent;
                } while (curTrans != null);

                mInstance = null;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 当前应用程序是否结束 标签
        /// </summary>
        protected static bool mOnApplicationQuit = false;

        /// <summary>
        /// 应用程序退出：释放当前对象并销毁相关GameObject
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            mOnApplicationQuit = true;
            if (mInstance == null) return;
            Destroy(mInstance.gameObject);
            mInstance = null;
        }

        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected virtual void OnDestroy()
        {
            mInstance = null;
        }

        /// <summary>
        /// 判断当前应用程序是否退出
        /// </summary>
        public static bool IsApplicationQuit => mOnApplicationQuit;
    }

    public class MonoSingletonListenDispose<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        private HashSet<IDisposable> mDisposables;

        private Action mOnDispose;
        
        public void AddDispose(IDisposable disposable)
        {
            if (mDisposables == null)
            {
                mDisposables = new HashSet<IDisposable>();
            }
            
            if (!mDisposables.Contains(disposable))
            {
                mDisposables.Add(disposable);
            }
        }
        
        public void AddDispose(Action onDispose)
        {
            if (mOnDispose == null)
            {
                mOnDispose = onDispose;
                return;
            }

            mOnDispose += onDispose;
        }
        
        protected override void OnDestroy()
        {
            if (Application.isPlaying)
            {
                foreach (var disposable in mDisposables)
                {
                    disposable.Dispose();
                }

                mDisposables.Clear();
                mDisposables = null;
                
                mOnDispose?.Invoke();
                mOnDispose = null;
            }
            
            base.OnDestroy();
        }
    } 
}