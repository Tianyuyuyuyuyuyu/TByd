using System.Threading.Tasks;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 表示一个可异步启动的服务。
    /// 实现此接口的服务将在容器构建完成后自动启动。
    /// </summary>
    public interface IAsyncStartable
    {
        /// <summary>
        /// 异步启动服务。
        /// 此方法将在容器构建完成后被调用。
        /// </summary>
        Task StartAsync();
    }
} 