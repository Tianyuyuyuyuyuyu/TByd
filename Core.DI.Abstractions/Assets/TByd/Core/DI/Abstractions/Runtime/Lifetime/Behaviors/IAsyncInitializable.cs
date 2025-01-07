using System.Threading.Tasks;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 表示一个可异步初始化的组件。
    /// 实现此接口的组件将在从容器解析后自动调用InitializeAsync方法。
    /// </summary>
    public interface IAsyncInitializable
    {
        /// <summary>
        /// 异步初始化组件。
        /// 此方法将在所有依赖注入完成后被调用。
        /// </summary>
        Task InitializeAsync();
    }
} 