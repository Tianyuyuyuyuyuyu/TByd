namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 定义对象池回调接收器的接口。
    /// 实现此接口的对象可以接收从对象池租用和归还时的回调。
    /// </summary>
    public interface IPoolCallbackReceiver
    {
        /// <summary>
        /// 当对象从池中被租用时调用。
        /// 可以在此方法中进行对象的初始化或重置操作。
        /// </summary>
        void OnRent();

        /// <summary>
        /// 当对象被归还到池中时调用。
        /// 可以在此方法中进行对象的清理操作。
        /// </summary>
        void OnReturn();
    }
}
