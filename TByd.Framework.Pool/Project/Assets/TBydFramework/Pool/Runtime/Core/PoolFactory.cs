using System;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    public static class PoolFactory
    {
        public static IPool<T> CreatePool<T>(PoolSettings settings = null) where T : class
        {
            var type = typeof(T);
            
            if (typeof(T).IsSubclassOf(typeof(Component)))
            {
                var prefab = PoolManager.Instance.GetPrefab(type);
                if (prefab == null)
                    throw new ArgumentException($"找不到类型 {type} 的预制体");
                    
                var pool = new GameObjectPool(prefab, settings);
                return (IPool<T>)CreateComponentPool((Type)type, prefab, settings);
            }
            
            if (typeof(T).IsSubclassOf(typeof(Object)))
            {
                var resourcePath = PoolManager.Instance.GetResourcePath(type);
                if (string.IsNullOrEmpty(resourcePath))
                    throw new ArgumentException($"找不到类型 {type} 的资源路径");
                    
                var resourcePool = typeof(ResourcePool<>)
                    .MakeGenericType(type)
                    .GetConstructor(new[] { typeof(string), typeof(PoolSettings) })
                    ?.Invoke(new object[] { resourcePath, settings });
                    
                return (IPool<T>)resourcePool;
            }
            
            return new ObjectPool<T>(null, settings);
        }

        public static IPool<T> CreatePool<T>(Func<T> factory, PoolSettings settings = null) where T : class
        {
            return new ObjectPool<T>(factory, settings);
        }

        public static IPool<GameObject> CreateGameObjectPool(GameObject prefab, PoolSettings settings = null)
        {
            return new GameObjectPool(prefab, settings);
        }

        public static IPool<T> CreateComponentPool<T>(GameObject prefab, PoolSettings settings = null) where T : Component
        {
            if (!prefab.GetComponent<T>())
                throw new ArgumentException($"预制体上没有找到组件 {typeof(T)}");

            var pool = new GameObjectPool(prefab, settings);
            return new ComponentPoolWrapper<T>(pool);
        }

        private static IPool<object> CreateComponentPool(Type componentType, GameObject prefab, PoolSettings settings)
        {
            if (!prefab.GetComponent(componentType))
                throw new ArgumentException($"预制体上没有找到组件 {componentType}");

            var pool = new GameObjectPool(prefab, settings);
            var wrapperType = typeof(ComponentPoolWrapper<>).MakeGenericType(componentType);
            return (IPool<object>)Activator.CreateInstance(wrapperType, pool);
        }
    }

    internal class ComponentPoolWrapper<T> : IPool<T> where T : Component
    {
        private readonly GameObjectPool _pool;
        private readonly PoolStatistics _statistics = new();

        public ComponentPoolWrapper(GameObjectPool pool)
        {
            _pool = pool;
        }

        public T Get()
        {
            var go = _pool.Get();
            return go.GetComponent<T>();
        }

        public void Return(T item)
        {
            if (item != null)
            {
                _pool.Return(item.gameObject);
            }
        }

        public void Clear() => _pool.Clear();
        public int Count => _pool.Count;
        public int Capacity => _pool.Capacity;
        public void Prewarm(int count) => _pool.Prewarm(count);
        public PoolStatistics GetStatistics() => _pool.GetStatistics();
    }
} 