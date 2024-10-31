using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;
using TBydFramework.Pool.Runtime.Internal;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// GameObject对象池，用于管理和复用GameObject实例。
    /// </summary>
    public sealed class GameObjectPool : IObjectPool<GameObject>
    {
        private readonly GameObject _original;
        private readonly Stack<GameObject> _stack = new(32);
        private bool _isDisposed;

        /// <summary>
        /// 初始化GameObject对象池。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        public GameObjectPool(GameObject original)
        {
            _original = original ? original : throw new ArgumentNullException(nameof(original));
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
                obj = UnityEngine.Object.Instantiate(_original);
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
                obj = UnityEngine.Object.Instantiate(_original, parent);
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
                obj = UnityEngine.Object.Instantiate(_original, position, rotation);
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
                obj = UnityEngine.Object.Instantiate(_original, position, rotation, parent);
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
                UnityEngine.Object.Destroy(obj);
            }
        }

        /// <summary>
        /// 预热池，创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public void Prewarm(int count)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var obj = UnityEngine.Object.Instantiate(_original);

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
        /// 检查池是否已被释放，如果已释放则抛出异常。
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
