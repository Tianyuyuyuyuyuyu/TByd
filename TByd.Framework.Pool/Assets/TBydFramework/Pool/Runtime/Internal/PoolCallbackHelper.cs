using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Internal
{
    /// <summary>
    /// 提供对象池回调辅助方法的内部静态类。
    /// </summary>
    internal static class PoolCallbackHelper
    {
        /// <summary>
        /// 用于存储组件的缓冲列表,避免重复分配。
        /// </summary>
        private static readonly List<IPoolCallbackReceiver> ComponentsBuffer = new();

        /// <summary>
        /// 调用GameObject及其子对象上所有IPoolCallbackReceiver接口的OnRent方法。
        /// </summary>
        /// <param name="obj">要处理的GameObject</param>
        public static void InvokeOnRent(GameObject obj)
        {
            obj.GetComponentsInChildren(ComponentsBuffer);
            foreach (var receiver in ComponentsBuffer)
            {
                receiver.OnRent();
            }
        }

        /// <summary>
        /// 调用GameObject及其子对象上所有IPoolCallbackReceiver接口的OnReturn方法。
        /// </summary>
        /// <param name="obj">要处理的GameObject</param>
        public static void InvokeOnReturn(GameObject obj)
        {
            obj.GetComponentsInChildren(ComponentsBuffer);
            foreach (var receiver in ComponentsBuffer)
            {
                receiver.OnReturn();
            }
        }
    }
}
