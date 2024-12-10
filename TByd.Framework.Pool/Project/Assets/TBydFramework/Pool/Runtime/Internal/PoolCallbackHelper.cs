using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;
using UnityEngine;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Internal
{
    /// <summary>
    /// 对象池回调辅助类
    /// </summary>
    internal static class PoolCallbackHelper
    {
        public static void InvokeOnCreate(GameObject obj)
        {
            var callbacks = obj.GetComponents<IPoolCallbacks>();
            foreach (var callback in callbacks)
            {
                callback.OnCreate();
            }
        }

        public static void InvokeOnGet(GameObject obj)
        {
            var callbacks = obj.GetComponents<IPoolCallbacks>();
            foreach (var callback in callbacks)
            {
                callback.OnGet();
            }
        }

        public static void InvokeOnReturn(GameObject obj)
        {
            var callbacks = obj.GetComponents<IPoolCallbacks>();
            foreach (var callback in callbacks)
            {
                callback.OnReturn();
            }
        }

        public static void InvokeOnDestroy(GameObject obj)
        {
            var callbacks = obj.GetComponents<IPoolCallbacks>();
            foreach (var callback in callbacks)
            {
                callback.OnDestroy();
            }
        }
    }
}
