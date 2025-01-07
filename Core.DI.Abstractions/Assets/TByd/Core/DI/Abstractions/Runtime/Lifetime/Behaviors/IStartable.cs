namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 表示一个可启动的服务。
    /// 实现此接口的服务将在容器构建完成后自动启动。
    /// </summary>
    public interface IStartable
    {
        /// <summary>
        /// 启动服务。
        /// 此方法将在容器构建完成后被调用。
        /// </summary>
        void Start();
    }
} 