using System.Threading;
using System.Threading.Tasks;

namespace TBydFramework.Connection.Runtime
{
    public interface IChannel<T>
    {
        /// <summary>
        /// Whether a connection has been established.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Connects the Client to the specified port on the specified host.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <returns></returns>
        Task Connect(string hostname, int port, int timeoutMilliseconds);

        /// <summary>
        /// Connects the Client to the specified port on the specified host.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Connect(string hostname, int port, int timeoutMilliseconds, CancellationToken cancellationToken);

        /// <summary>
        /// Read a message asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<T> ReadAsync();

        /// <summary>
        /// Write a message asynchronously.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task WriteAsync(T message);

        /// <summary>
        ///  Forces a channel to close.
        /// </summary>
        /// <returns></returns>
        Task Close();
    }
}
