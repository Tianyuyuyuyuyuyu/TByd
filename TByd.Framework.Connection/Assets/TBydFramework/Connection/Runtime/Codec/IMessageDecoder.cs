using System.Threading.Tasks;
using TBydFramework.Connection.Runtime.IO;

namespace TBydFramework.Connection.Runtime.Codec
{
    public interface IMessageDecoder<TMessage>
    {
        Task<TMessage> Decode(BinaryReader reader);

    }
}
