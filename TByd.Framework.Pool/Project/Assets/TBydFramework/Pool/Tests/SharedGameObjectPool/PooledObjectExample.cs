using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    /// <summary>
    /// 用于对象池测试的示例组件
    /// </summary>
    public class PooledObjectExample : MonoBehaviour
    {
        /// <summary>
        /// 对象被启用的次数
        /// </summary>
        public int EnableCount { get; private set; }

        /// <summary>
        /// 对象被禁用的次数
        /// </summary>
        public int DisableCount { get; private set; }

        /// <summary>
        /// 最后一次启用的时间戳
        /// </summary>
        public float LastEnableTime { get; private set; }

        /// <summary>
        /// 最后一次禁用的时间戳
        /// </summary>
        public float LastDisableTime { get; private set; }

        /// <summary>
        /// 对象当前是否正在使用中
        /// </summary>
        public bool IsInUse { get; private set; }

        private void OnEnable()
        {
            EnableCount++;
            LastEnableTime = Time.time;
            IsInUse = true;
            
            Debug.Log($"[PooledObjectExample] {gameObject.name} 被启用 - 启用次数: {EnableCount}");
        }

        private void OnDisable()
        {
            DisableCount++;
            LastDisableTime = Time.time;
            IsInUse = false;
            
            Debug.Log($"[PooledObjectExample] {gameObject.name} 被禁用 - 禁用次数: {DisableCount}");
        }

        /// <summary>
        /// 重置组件状态
        /// </summary>
        public void Reset()
        {
            EnableCount = 0;
            DisableCount = 0;
            LastEnableTime = 0f;
            LastDisableTime = 0f;
            IsInUse = false;
            
            Debug.Log($"[PooledObjectExample] {gameObject.name} 状态已重置");
        }

        /// <summary>
        /// 获取对象的使用时长
        /// </summary>
        public float GetCurrentUsageTime()
        {
            if (!IsInUse) return 0f;
            return Time.time - LastEnableTime;
        }
    }
} 