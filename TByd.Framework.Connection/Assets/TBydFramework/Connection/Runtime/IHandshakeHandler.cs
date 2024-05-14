using System.Threading.Tasks;

namespace TBydFramework.Connection.Runtime
{
    public interface IHandshakeHandler
    {
        /// <summary>
        /// Send and receive handshake messages, 
        /// return after completing the handshake.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        Task OnHandshake(IChannel<IMessage> channel);
    }
}
