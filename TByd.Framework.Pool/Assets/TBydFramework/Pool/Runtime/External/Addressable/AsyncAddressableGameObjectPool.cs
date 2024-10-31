#if TBYDPOOL_ADDRESSABLES_SUPPORT && TBYDPOOL_UNITASK_SUPPORT
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.External.UniTask;
using TBydFramework.Pool.Runtime.Internal;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TBydFramework.Pool.Runtime.External.Addressable
{
    /// <summary>
    /// 异步可寻址资源游戏对象池,用于异步管理通过Addressable加载的GameObject实例。
    /// </summary>
    public sealed class AsyncAddressableGameObjectPool : IAsyncObjectPool<GameObject>
    {
        private readonly object _key;
        private readonly Stack<GameObject> _stack = new(32);
        private bool _isDisposed;

        /// <summary>
        /// 使用资源键初始化对象池。
        /// </summary>
        /// <param name="key">Addressable资源的键</param>
        public AsyncAddressableGameObjectPool(object key)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
        }
        
        /// <summary>
        /// 使用AssetReferenceGameObject初始化对象池。
        /// </summary>
        /// <param name="reference">AssetReferenceGameObject引用</param>
        public AsyncAddressableGameObjectPool(AssetReferenceGameObject reference)
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
        /// 异步从池中租用一个GameObject。
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>表示异步操作的UniTask,其结果为租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = await Addressables.InstantiateAsync(_key).ToUniTask(cancellationToken: cancellationToken);
            }
            else
            {
                obj.SetActive(true);
            }

            PoolCallbackHelper.InvokeOnRent(obj);
            return obj;
        }

        /// <summary>
        /// 异步从池中租用一个GameObject并设置其父级。
        /// </summary>
        /// <param name="parent">父级Transform</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>表示异步操作的UniTask,其结果为租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(Transform parent, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = await Addressables.InstantiateAsync(_key, parent).ToUniTask(cancellationToken: cancellationToken);
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
        /// 异步从池中租用一个GameObject并设置其位置和旋转。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>表示异步操作的UniTask,其结果为租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(Vector3 position, Quaternion rotation, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = await Addressables.InstantiateAsync(_key, position, rotation).ToUniTask(cancellationToken: cancellationToken);
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
        /// 异步从池中租用一个GameObject并设置其位置、旋转和父级。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父级Transform</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>表示异步操作的UniTask,其结果为租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(Vector3 position, Quaternion rotation, Transform parent, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = await Addressables.InstantiateAsync(_key, position, rotation, parent).ToUniTask(cancellationToken: cancellationToken);
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
        /// 异步预热池,创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>表示异步操作的UniTask</returns>
        public async Cysharp.Threading.Tasks.UniTask PrewarmAsync(int count, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var obj = await Addressables.InstantiateAsync(_key).ToUniTask(cancellationToken: cancellationToken);

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

        /// <summary>
        /// 检查池是否已被释放,如果已释放则抛出异常。
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
#endif
