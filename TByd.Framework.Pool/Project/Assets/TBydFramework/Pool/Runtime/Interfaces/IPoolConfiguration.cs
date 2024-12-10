using TBydFramework.Pool.Runtime.Config;

namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 对象池配置接口
    /// </summary>
    public interface IPoolConfiguration
    {
        /// <summary>
        /// 获取当前池的配置
        /// </summary>
        PoolConfig GetCurrentConfig();
    }
} 