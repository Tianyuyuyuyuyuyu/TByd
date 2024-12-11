using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Internal;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 共享的GameObject对象池，用于管理和复用多个不同类型的GameObject实例。
    /// </summary>
    public class SharedGameObjectPool : ISharedGameObjectPool
    {
        private static readonly Dictionary<GameObject, Stack<GameObject>> _pools = new();
        private static readonly Dictionary<GameObject, Transform> _parents = new();
        
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly Stack<GameObject> _pool = new();

        public SharedGameObjectPool(GameObject prefab, Transform parent)
        {
            _prefab = prefab ? prefab : throw new ArgumentNullException(nameof(prefab));
            _parent = parent;
        }

        #region Static Methods
        
        public static void Prewarm(GameObject original, int count)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);
            for (int i = 0; i < count; i++)
            {
                var obj = UnityEngine.Object.Instantiate(original, GetParentTransform(original));
                obj.SetActive(false);
                pool.Push(obj);
                PoolCallbackHelper.InvokeOnReturn(obj);
            }
        }

        public static GameObject Rent(GameObject original)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            var pool = GetOrCreatePool(original);
            GameObject obj;

            while (true)
            {
                if (!pool.TryPop(out obj))
                {
                    obj = UnityEngine.Object.Instantiate(original, GetParentTransform(original));
                    PoolCallbackHelper.InvokeOnCreate(obj);
                    break;
                }
                else if (obj != null)
                {
                    obj.SetActive(true);
                    break;
                }
            }

            PoolCallbackHelper.InvokeOnGet(obj);
            return obj;
        }

        public static GameObject Rent(GameObject original, Transform parent)
        {
            var obj = Rent(original);
            obj.transform.SetParent(parent);
            return obj;
        }

        public static GameObject Rent(GameObject original, Vector3 position, Quaternion rotation)
        {
            var obj = Rent(original);
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        public static GameObject Rent(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = Rent(original);
            obj.transform.SetParent(parent);
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        public static TComponent Rent<TComponent>(TComponent original) where TComponent : Component
        {
            return Rent(original.gameObject).GetComponent<TComponent>();
        }

        public static TComponent Rent<TComponent>(TComponent original, Transform parent) where TComponent : Component
        {
            return Rent(original.gameObject, parent).GetComponent<TComponent>();
        }

        public static TComponent Rent<TComponent>(TComponent original, Vector3 position, Quaternion rotation) where TComponent : Component
        {
            return Rent(original.gameObject, position, rotation).GetComponent<TComponent>();
        }

        public static TComponent Rent<TComponent>(TComponent original, Vector3 position, Quaternion rotation, Transform parent) where TComponent : Component
        {
            return Rent(original.gameObject, position, rotation, parent).GetComponent<TComponent>();
        }

        public static void Return(GameObject instance)
        {
            if (instance == null) return;

            foreach (var pair in _pools)
            {
                if (instance.name.StartsWith(pair.Key.name))
                {
                    instance.SetActive(false);
                    instance.transform.SetParent(GetParentTransform(pair.Key));
                    pair.Value.Push(instance);
                    PoolCallbackHelper.InvokeOnReturn(instance);
                    return;
                }
            }

            Debug.LogWarning($"尝试归还一个不是从对象池租用的对象: {instance.name}");
            UnityEngine.Object.Destroy(instance);
        }

        public static int GetPoolSize(GameObject original)
        {
            return _pools.TryGetValue(original, out var pool) ? pool.Count : 0;
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

        private static Transform GetParentTransform(GameObject original)
        {
            if (!_parents.TryGetValue(original, out var parent))
            {
                var go = new GameObject($"[Pool]{original.name}");
                parent = go.transform;
                _parents.Add(original, parent);
            }
            return parent;
        }

        public static void ClearAll()
        {
            foreach (var pair in _pools)
            {
                while (pair.Value.Count > 0)
                {
                    var obj = pair.Value.Pop();
                    if (obj != null)
                    {
                        UnityEngine.Object.Destroy(obj);
                    }
                }
            }
            _pools.Clear();

            foreach (var pair in _parents)
            {
                if (pair.Value != null)
                {
                    UnityEngine.Object.Destroy(pair.Value.gameObject);
                }
            }
            _parents.Clear();
        }

        #endregion

        #region Instance Methods

        public GameObject Get()
        {
            return Rent(_prefab);
        }

        public void Release(GameObject obj)
        {
            Return(obj);
        }

        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
        }

        public void Prewarm(int count)
        {
            Prewarm(_prefab, count);
        }

        #endregion
    }
}
