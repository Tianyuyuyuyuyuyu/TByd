using System;
using System.Collections.Generic;
#if TBYD_UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#endif
using TBydFramework.Pool.Runtime.Base;
using TBydFramework.Pool.Runtime.Internal;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 异步共享GameObject对象池，用于在多个位置共享和复用GameObject实例。
    /// </summary>
#if TBYD_UNITASK_SUPPORT
    public sealed class AsyncSharedGameObjectPool : IObjectPool<GameObject>
    {
        private readonly GameObject _original;
        private readonly Stack<GameObject> _stack = new(32);
        private readonly Transform _root;
        private bool _isDisposed;

        /// <summary>
        /// 初始化异步共享GameObject对象池。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <param name="parent">对象池根节点的父级Transform</param>
        /// <param name="name">对象池根节点的名称</param>
        public AsyncSharedGameObjectPool(GameObject original, Transform parent = null, string name = null)
        {
            _original = original ? original : throw new ArgumentNullException(nameof(original));
            
            // 创建对象池的根节点
            _root = new GameObject(name ?? $"{original.name} Pool").transform;
            _root.SetParent(parent);
            
            if (parent == null)
            {
                UnityEngine.Object.DontDestroyOnLoad(_root.gameObject);
            }
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
        /// <returns>租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync()
        {
            ThrowIfDisposed();

            GameObject obj;
            if (!_stack.TryPop(out obj))
            {
                obj = await CreateInstanceAsync();
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
        /// <returns>租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(Transform parent)
        {
            var obj = await RentAsync();
            obj.transform.SetParent(parent);
            return obj;
        }

        /// <summary>
        /// 异步从池中租用一个GameObject并设置其位置和旋转。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns>租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(Vector3 position, Quaternion rotation)
        {
            var obj = await RentAsync();
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        /// <summary>
        /// 异步从池中租用一个GameObject并设置其位置、旋转和父级。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public async UniTask<GameObject> RentAsync(Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = await RentAsync();
            obj.transform.SetParent(parent);
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        /// <summary>
        /// 将GameObject归还到池中。
        /// </summary>
        /// <param name="obj">要归还的GameObject</param>
        public void Return(GameObject obj)
        {
            ThrowIfDisposed();
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            _stack.Push(obj);
            obj.transform.SetParent(_root);
            obj.SetActive(false);

            PoolCallbackHelper.InvokeOnReturn(obj);
        }

        /// <summary>
        /// 从池中租用一个GameObject（同步版本，建议使用RentAsync）。
        /// </summary>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent()
        {
            return RentAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            
            while (_stack.TryPop(out var obj))
            {
                UnityEngine.Object.Destroy(obj);
            }
        }

        /// <summary>
        /// 异步预热池，创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public async UniTask PrewarmAsync(int count)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var obj = await CreateInstanceAsync();
                Return(obj);
            }
        }

        /// <summary>
        /// 预热池，创建指定数量的对象并添加到池中（同步版本，建议使用PrewarmAsync）。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public void Prewarm(int count)
        {
            PrewarmAsync(count).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 释放池中的所有资源。
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;

            Clear();
            if (_root != null)
            {
                UnityEngine.Object.Destroy(_root.gameObject);
            }
            _isDisposed = true;
        }

        private async UniTask<GameObject> CreateInstanceAsync()
        {
            await UniTask.NextFrame();
            var obj = UnityEngine.Object.Instantiate(_original, _root);
            return obj;
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
#endif
} 