namespace TBydFramework.Pool.Runtime.Enums
{
    /// <summary>
    /// 对象池类型枚举
    /// </summary>
    public enum PoolType
    {
        /// <summary>
        /// 普通GameObject对象池
        /// </summary>
        GameObject,

        /// <summary>
        /// 共享GameObject对象池
        /// </summary>
        SharedGameObject,

        /// <summary>
        /// Addressable资源对象池
        /// </summary>
        Addressable,

        /// <summary>
        /// 通用对象池
        /// </summary>
        Generic
    }
} 