#if TBYD_ADDRESSABLES_SUPPORT
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TBydFramework.Pool.Runtime.External
{
    /// <summary>
    /// Addressable资源对象池,用于管理通过Addressables加载的GameObject实例。
    /// </summary>
    public sealed class AddressableGameObjectPool : IObjectPool<GameObject>
    {
        private readonly object _key;
        private readonly Stack<GameObject> _stack = new(32);
        private bool _isDisposed;

        /// <summary>
        /// 使用资源key初始化Addressable对象池。
        /// </summary>
        /// <param name="key">Addressable资源的key</param>
        public AddressableGameObjectPool(object key)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        /// 使用AssetReference初始化Addressable对象池。
        /// </summary>
        /// <param name="reference">GameObject的AssetReference</param>
        public AddressableGameObjectPool(AssetReferenceGameObject reference)
        {
            if (reference == null) throw new ArgumentNullException(nameof(reference));
            _key = reference.RuntimeKey;
        }

        /// <summary>
        /// 获取池中当前的对象数量。
        /// </summary>
        public int Count => _stack.Count;

        /// <summary>
        /// 获取池是否已被释放。
        /// </summary>
        public bool IsDisposed => _isDisposed;

        /// <summary>
        /// 从池中租用一个GameObject。
        /// </summary>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent()
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key).WaitForCompletion();
            }
            else
            {
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其父级。
        /// </summary>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent(Transform parent)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key, parent).WaitForCompletion();
            }
            else
            {
                obj.transform.SetParent(parent);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其位置和旋转。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent(Vector3 position, Quaternion rotation)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key, position, rotation).WaitForCompletion();
            }
            else
            {
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其位置、旋转和父级。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent(Vector3 position, Quaternion rotation, Transform parent)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key, position, rotation, parent).WaitForCompletion();
            }
            else
            {
                obj.transform.SetParent(parent);
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 将GameObject归还到池中。
        /// </summary>
        /// <param name="obj">要归还的GameObject</param>
        public void Return(GameObject obj)
        {
            ThrowIfDisposed();

            _stack.Push(obj);
            obj.SetActive(false);

            PoolCallbackHelper.InvokeOnReturn(obj);
        }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            
            while (_stack.TryPop(out var obj))
            {
                Addressables.ReleaseInstance(obj);
            }
        }

        /// <summary>
        /// 预热池,创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public void Prewarm(int count)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var obj = Addressables.InstantiateAsync(_key).WaitForCompletion();

                _stack.Push(obj);
                obj.SetActive(false);

                PoolCallbackHelper.InvokeOnReturn(obj);
            }
        }

        /// <summary>
        /// 释放池中的所有资源。
        /// </summary>
        public void Dispose()
        {
            ThrowIfDisposed();
            Clear();
            _isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
#endif 