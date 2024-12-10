using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 资源回收池，用于管理和复用Unity资源(如纹理、音频等)。
    /// </summary>
    /// <typeparam name="T">资源类型，必须继承自UnityEngine.Object</typeparam>
    public sealed class ResourcePool<T> where T : Object
    {
        private readonly Dictionary<int, Stack<T>> _resourceStacks = new();
        private readonly Dictionary<int, T> _originalResources = new();
        private readonly int _maxSize;
        private bool _isDisposed;

        /// <summary>
        /// 初始化资源回收池
        /// </summary>
        /// <param name="maxSize">每个资源类型的最大缓存数量，默认为8</param>
        public ResourcePool(int maxSize = 8)
        {
            _maxSize = maxSize;
        }

        /// <summary>
        /// 获取指定资源的实例
        /// </summary>
        /// <param name="original">原始资源</param>
        /// <returns>资源实例</returns>
        public T Get(T original)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            ThrowIfDisposed();

            int id = original.GetInstanceID();
            
            // 记录原始资源
            if (!_originalResources.ContainsKey(id))
            {
                _originalResources[id] = original;
            }

            // 尝试从对应的栈中获取资源
            if (!_resourceStacks.TryGetValue(id, out var stack))
            {
                stack = new Stack<T>();
                _resourceStacks[id] = stack;
            }

            if (stack.Count > 0)
            {
                return stack.Pop();
            }

            // 创建新的资源副本
            return Object.Instantiate(original);
        }

        /// <summary>
        /// 回收资源实例
        /// </summary>
        /// <param name="resource">要回收的资源实例</param>
        public void Release(T resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            ThrowIfDisposed();

            // 查找原始资源
            T original = null;
            int originalId = -1;
            
            foreach (var kvp in _originalResources)
            {
                if (CompareResources(resource, kvp.Value))
                {
                    original = kvp.Value;
                    originalId = kvp.Key;
                    break;
                }
            }

            if (original == null)
            {
                Debug.LogWarning($"无法找到资源 {resource.name} 的原始资源，将直接销毁");
                Object.Destroy(resource);
                return;
            }

            // 获取对应的资源栈
            if (!_resourceStacks.TryGetValue(originalId, out var stack))
            {
                stack = new Stack<T>();
                _resourceStacks[originalId] = stack;
            }

            // 如果栈未满，则回收资源
            if (stack.Count < _maxSize)
            {
                stack.Push(resource);
            }
            else
            {
                Object.Destroy(resource);
            }
        }

        /// <summary>
        /// 预热指定资源的实例
        /// </summary>
        /// <param name="original">原始资源</param>
        /// <param name="count">预热数量</param>
        public void Prewarm(T original, int count)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                var resource = Get(original);
                Release(resource);
            }
        }

        /// <summary>
        /// 清理指定资源的所有实例
        /// </summary>
        /// <param name="original">原始资源</param>
        public void Clear(T original)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            ThrowIfDisposed();

            int id = original.GetInstanceID();
            if (_resourceStacks.TryGetValue(id, out var stack))
            {
                while (stack.Count > 0)
                {
                    var resource = stack.Pop();
                    Object.Destroy(resource);
                }
            }
        }

        /// <summary>
        /// 清理所有资源
        /// </summary>
        public void ClearAll()
        {
            ThrowIfDisposed();

            foreach (var stack in _resourceStacks.Values)
            {
                while (stack.Count > 0)
                {
                    var resource = stack.Pop();
                    Object.Destroy(resource);
                }
            }
            
            _resourceStacks.Clear();
            _originalResources.Clear();
        }

        /// <summary>
        /// 获取指定资源当前缓存的实例数量
        /// </summary>
        public int GetCount(T original)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            
            int id = original.GetInstanceID();
            return _resourceStacks.TryGetValue(id, out var stack) ? stack.Count : 0;
        }

        /// <summary>
        /// 比较两个资源是否相关
        /// </summary>
        private bool CompareResources(T resource1, T resource2)
        {
            // 对于不同类型的资源可能需要不同的比较方法
            if (resource1 is Texture2D || resource1 is AudioClip)
            {
                return resource1.name == resource2.name;
            }
            
            return resource1.GetInstanceID() == resource2.GetInstanceID();
        }

        /// <summary>
        /// 释放资源池
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            
            ClearAll();
            _isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
} 