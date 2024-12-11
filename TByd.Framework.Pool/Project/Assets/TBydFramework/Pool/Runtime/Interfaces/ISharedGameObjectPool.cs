using UnityEngine;

namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 共享GameObject对象池接口
    /// </summary>
    public interface ISharedGameObjectPool
    {
        GameObject Get();
        void Release(GameObject obj);
        void Clear();
        void Prewarm(int count);
    }
}