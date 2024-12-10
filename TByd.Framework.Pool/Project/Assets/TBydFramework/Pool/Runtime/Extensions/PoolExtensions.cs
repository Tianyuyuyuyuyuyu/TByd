using UnityEngine;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Extensions
{
    public static class PoolExtensions
    {
        public static T GetAndInit<T>(this IPool<T> pool, System.Action<T> initializer) where T : class
        {
            var item = pool.Get();
            initializer?.Invoke(item);
            return item;
        }

        public static void ReturnAndReset<T>(this IPool<T> pool, T item, System.Action<T> resetAction) where T : class
        {
            resetAction?.Invoke(item);
            pool.Return(item);
        }

        public static GameObject GetAndPosition(this IPool<GameObject> pool, Vector3 position, Quaternion rotation)
        {
            var go = pool.Get();
            go.transform.SetPositionAndRotation(position, rotation);
            return go;
        }

        public static void ReturnWithDelay<T>(this IPool<T> pool, T item, float delay) where T : class
        {
            if (item is GameObject go)
            {
                UnityEngine.Object.Destroy(go, delay);
            }
            else
            {
                pool.Return(item);
            }
        }
    }
} 