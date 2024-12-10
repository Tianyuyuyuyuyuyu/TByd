namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 对象池回调接口
    /// </summary>
    public interface IPoolCallbacks
    {
        /// <summary>
        /// 当对象被创建时调用
        /// </summary>
        void OnCreate();

        /// <summary>
        /// 当对象从池中获取时调用
        /// </summary>
        void OnGet();

        /// <summary>
        /// 当对象返回池中时调用
        /// </summary>
        void OnReturn();

        /// <summary>
        /// 当对象被销毁时调用
        /// </summary>
        void OnDestroy();
    }
} 