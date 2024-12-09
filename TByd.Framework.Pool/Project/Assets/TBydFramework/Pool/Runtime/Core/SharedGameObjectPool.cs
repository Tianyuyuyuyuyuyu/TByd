using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Internal;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 共享的GameObject对象池，用于管理和复用多个不同类型的GameObject实例。
    /// </summary>
    public static class SharedGameObjectPool
    {
        private static readonly Dictionary<GameObject, Stack<GameObject>> _pools = new();
        private static readonly Dictionary<GameObject, Stack<GameObject>> _cloneReferences = new();

        /// <summary>
        /// 在场景加载之前初始化对象池。
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            _pools.Clear();
            _cloneReferences.Clear();
        }

        /// <summary>
        /// 从池中租用一个GameObject。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <returns>租用的GameObject</returns>
        public static GameObject Rent(GameObject original)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);

            GameObject obj;
            while (true)
            {
                if (!pool.TryPop(out obj))
                {
                    obj = UnityEngine.Object.Instantiate(original);
                    break;
                }
                else if (obj != null)
                {
                    obj.SetActive(true);
                    break;
                }
            }

            _cloneReferences.Add(obj, pool);
            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其父级。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public static GameObject Rent(GameObject original, Transform parent)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);

            GameObject obj;
            while (true)
            {
                if (!pool.TryPop(out obj))
                {
                    obj = UnityEngine.Object.Instantiate(original, parent);
                    break;
                }
                else if (obj != null)
                {
                    obj.transform.SetParent(parent);
                    obj.SetActive(true);
                    break;
                }
            }

            _cloneReferences.Add(obj, pool);
            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其位置和旋转。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns>租用的GameObject</returns>
        public static GameObject Rent(GameObject original, Vector3 position, Quaternion rotation)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);

            GameObject obj;
            while (true)
            {
                if (!pool.TryPop(out obj))
                {
                    obj = UnityEngine.Object.Instantiate(original, position, rotation);
                    break;
                }
                else if (obj != null)
                {
                    obj.transform.SetPositionAndRotation(position, rotation);
                    obj.SetActive(true);
                    break;
                }
            }

            _cloneReferences.Add(obj, pool);
            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其位置、旋转和父级。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public static GameObject Rent(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);

            GameObject obj;
            while (true)
            {
                if (!pool.TryPop(out obj))
                {
                    obj = UnityEngine.Object.Instantiate(original, position, rotation, parent);
                    break;
                }
                else if (obj != null)
                {
                    obj.transform.SetParent(parent);
                    obj.transform.SetPositionAndRotation(position, rotation);
                    obj.SetActive(true);
                    break;
                }
            }

            _cloneReferences.Add(obj, pool);
            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个指定类型的Component。
        /// </summary>
        /// <typeparam name="TComponent">要租用的Component类型</typeparam>
        /// <param name="original">用于创建新实例的原始Component</param>
        /// <returns>租用的Component</returns>
        public static TComponent Rent<TComponent>(TComponent original) where TComponent : Component
        {
            return Rent(original.gameObject).GetComponent<TComponent>();
        }

        /// <summary>
        /// 从池中租用一个指定类型的Component并设置其父级。
        /// </summary>
        /// <typeparam name="TComponent">要租用的Component类型</typeparam>
        /// <param name="original">用于创建新实例的原始Component</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的Component</returns>
        public static TComponent Rent<TComponent>(TComponent original, Transform parent) where TComponent : Component
        {
            return Rent(original.gameObject, parent).GetComponent<TComponent>();
        }

        /// <summary>
        /// 从池中租用一个指定类型的Component并设置其位置和旋转。
        /// </summary>
        /// <typeparam name="TComponent">要租用的Component类型</typeparam>
        /// <param name="original">用于创建新实例的原始Component</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns>租用的Component</returns>
        public static TComponent Rent<TComponent>(TComponent original, Vector3 position, Quaternion rotation) where TComponent : Component
        {
            return Rent(original.gameObject, position, rotation).GetComponent<TComponent>();
        }

        /// <summary>
        /// 从池中租用一个指定类型的Component并设置其位置、旋转和父级。
        /// </summary>
        /// <typeparam name="TComponent">要租用的Component类型</typeparam>
        /// <param name="original">用于创建新实例的原始Component</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的Component</returns>
        public static TComponent Rent<TComponent>(TComponent original, Vector3 position, Quaternion rotation, Transform parent) where TComponent : Component
        {
            return Rent(original.gameObject, position, rotation, parent).GetComponent<TComponent>();
        }

        /// <summary>
        /// 将GameObject归还到池中。
        /// </summary>
        /// <param name="instance">要归还的GameObject</param>
        public static void Return(GameObject instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            if (_cloneReferences.TryGetValue(instance, out var pool))
            {
                instance.SetActive(false);
                pool.Push(instance);
                _cloneReferences.Remove(instance);

                PoolCallbackHelper.InvokeOnReturn(instance);
            }
            else
            {
                Debug.LogWarning($"Trying to return an object that was not rented from the pool: {instance.name}");
                UnityEngine.Object.Destroy(instance);
            }
        }

        /// <summary>
        /// 预热池，创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <param name="count">要预热的对象数量</param>
        public static void Prewarm(GameObject original, int count)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);

            for (int i = 0; i < count; i++)
            {
                var obj = UnityEngine.Object.Instantiate(original);
                obj.SetActive(false);
                pool.Push(obj);

                PoolCallbackHelper.InvokeOnReturn(obj);
            }
        }

        /// <summary>
        /// 获取指定预制体对应的池中当前的对象数量。
        /// </summary>
        /// <param name="original">原始GameObject预制体</param>
        /// <returns>池中当前的对象数量</returns>
        public static int GetPoolSize(GameObject original)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            return _pools.TryGetValue(original, out var pool) ? pool.Count : 0;
        }

        /// <summary>
        /// 清空所有对象池。
        /// </summary>
        public static void ClearAll()
        {
            foreach (var pool in _pools.Values)
            {
                while (pool.Count > 0)
                {
                    if (pool.TryPop(out var obj))
                    {
                        UnityEngine.Object.Destroy(obj);
                    }
                }
            }
            _pools.Clear();
            _cloneReferences.Clear();
        }

        private static Stack<GameObject> GetOrCreatePool(GameObject original)
        {
            if (!_pools.TryGetValue(original, out var pool))
            {
                pool = new Stack<GameObject>();
                _pools.Add(original, pool);
            }
            return pool;
        }
    }
}
