using System;
using TBydFramework.Pool.Runtime.Core;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class CustomEventArgs : EventArgs
    {
        public string Message { get; set; }
        public int Value { get; set; }
    }

    public class EventPoolExample : MonoBehaviour
    {
        private EventPool<CustomEventArgs> _eventPool;

        private void Start()
        {
            // 初始化事件池，设置重置操作
            _eventPool = EventPoolManager.Instance.GetPool<CustomEventArgs>(
                maxSize: 32,
                resetAction: args =>
                {
                    args.Message = null;
                    args.Value = 0;
                }
            );

            // 预热池
            _eventPool.Prewarm(5);

            // 使用事件池
            var args = _eventPool.Get();
            args.Message = "Hello";
            args.Value = 42;

            // 处理事件...

            // 归还到池中
            _eventPool.Return(args);
        }

        private void OnDestroy()
        {
            _eventPool?.Dispose();
        }
    }
} 