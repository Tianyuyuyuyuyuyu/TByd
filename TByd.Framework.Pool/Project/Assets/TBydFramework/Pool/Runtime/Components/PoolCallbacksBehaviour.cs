using UnityEngine;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Components
{
    /// <summary>
    /// 对象池回调基础组件
    /// </summary>
    public abstract class PoolCallbacksBehaviour : MonoBehaviour, IPoolCallbacks
    {
        public virtual void OnCreate() { }
        public virtual void OnGet() { }
        public virtual void OnReturn() { }
        public virtual void OnDestroy() { }
    }
} 