#if TBYD_ADDRESSABLES_SUPPORT && TBYD_UNITASK_SUPPORT
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TBydFramework.Module.Pool.Runtime.External.UniTask;
using TBydFramework.Module.Pool.Runtime.Internal;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TBydFramework.Module.Pool.Runtime.External.Addressables
{
    public sealed class AsyncAddressableGameObjectPool : IAsyncObjectPool<GameObject>
    {
        public AsyncAddressableGameObjectPool(object key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            this.key = key;
        }
        
        public AsyncAddressableGameObjectPool(AssetReferenceGameObject reference)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            this.key = reference.RuntimeKey;
        }

        readonly object key;
        readonly Stack<GameObject> stack = new(32);
        bool isDisposed;

        public int Count => stack.Count;
        public bool IsDisposed => isDisposed;

        public async UniTask<GameObject> RentAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!stack.TryPop(out var obj))
            {
                obj = await UnityEngine.AddressableAssets.Addressables.InstantiateAsync(key).ToUniTask(cancellationToken: cancellationToken);
            }
            else
            {
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        public async UniTask<GameObject> RentAsync(Transform parent, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!stack.TryPop(out var obj))
            {
                obj = await UnityEngine.AddressableAssets.Addressables.InstantiateAsync(key, parent).ToUniTask(cancellationToken: cancellationToken);
            }
            else
            {
                obj.transform.SetParent(parent);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        public async UniTask<GameObject> RentAsync(Vector3 position, Quaternion rotation, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!stack.TryPop(out var obj))
            {
                obj = await UnityEngine.AddressableAssets.Addressables.InstantiateAsync(key, position, rotation).ToUniTask(cancellationToken: cancellationToken);
            }
            else
            {
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        public async UniTask<GameObject> RentAsync(Vector3 position, Quaternion rotation, Transform parent)
        {
            ThrowIfDisposed();

            if (!stack.TryPop(out var obj))
            {
                obj = await UnityEngine.AddressableAssets.Addressables.InstantiateAsync(key, position, rotation, parent);
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

            stack.Push(obj);
            obj.SetActive(false);

            PoolCallbackHelper.InvokeOnReturn(obj);
        }

        public void Clear()
        {
            ThrowIfDisposed();

            while (stack.TryPop(out var obj))
            {
                UnityEngine.AddressableAssets.Addressables.ReleaseInstance(obj);
            }
        }

        public async Cysharp.Threading.Tasks.UniTask PrewarmAsync(int count, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var obj = await UnityEngine.AddressableAssets.Addressables.InstantiateAsync(key).ToUniTask(cancellationToken: cancellationToken);

                stack.Push(obj);
                obj.SetActive(false);

                PoolCallbackHelper.InvokeOnReturn(obj);
            }
        }

        public void Dispose()
        {
            ThrowIfDisposed();
            Clear();
            isDisposed = true;
        }

        void ThrowIfDisposed()
        {
            if (isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
#endif