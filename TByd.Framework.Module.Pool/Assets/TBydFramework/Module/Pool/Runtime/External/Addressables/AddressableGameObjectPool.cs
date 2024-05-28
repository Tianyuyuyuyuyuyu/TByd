#if TBYD_ADDRESSABLES_SUPPORT
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TBydFramework.Module.Pool.Runtime
{
    public sealed class AddressableGameObjectPool : IObjectPool<GameObject>
    {
        public AddressableGameObjectPool(object key)
        {
            this._key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public AddressableGameObjectPool(AssetReferenceGameObject reference)
        {
            if (reference == null) throw new ArgumentNullException(nameof(reference));
            _key = reference.RuntimeKey;
        }

        private readonly object _key;
        readonly Stack<GameObject> _stack = new(32);
        private bool _isDisposed;

        public int Count => _stack.Count;
        public bool IsDisposed => _isDisposed;

        public GameObject Rent()
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(_key).WaitForCompletion();
            }
            else
            {
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        public GameObject Rent(Transform parent)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(_key, parent).WaitForCompletion();
            }
            else
            {
                obj.transform.SetParent(parent);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        public GameObject Rent(Vector3 position, Quaternion rotation)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(_key, position, rotation).WaitForCompletion();
            }
            else
            {
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        public GameObject Rent(Vector3 position, Quaternion rotation, Transform parent)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(_key, position, rotation, parent).WaitForCompletion();
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

        public void Return(GameObject obj)
        {
            ThrowIfDisposed();

            _stack.Push(obj);
            obj.SetActive(false);

            PoolCallbackHelper.InvokeOnReturn(obj);
        }

        public void Clear()
        {
            ThrowIfDisposed();
            
            while (_stack.TryPop(out var obj))
            {
                UnityEngine.AddressableAssets.Addressables.ReleaseInstance(obj);
            }
        }

        public void Prewarm(int count)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var obj = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(_key).WaitForCompletion();

                _stack.Push(obj);
                obj.SetActive(false);

                PoolCallbackHelper.InvokeOnReturn(obj);
            }
        }

        public void Dispose()
        {
            ThrowIfDisposed();
            Clear();
            _isDisposed = true;
        }

        void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
#endif