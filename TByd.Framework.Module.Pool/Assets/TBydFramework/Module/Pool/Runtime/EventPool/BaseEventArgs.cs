using TBydFramework.Runtime.Base;

namespace TBydFramework.Module.Pool.Runtime.EventPool
{
    /// <summary>
    /// 事件基类。
    /// </summary>
    public abstract class BaseEventArgs : TBydFrameworkEventArgs
    {
        /// <summary>
        /// 获取类型编号。
        /// </summary>
        public abstract int Id
        {
            get;
        }
    }
}
