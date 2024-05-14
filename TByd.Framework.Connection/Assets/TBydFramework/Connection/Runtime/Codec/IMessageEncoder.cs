using System.Threading.Tasks;
using TBydFramework.Connection.Runtime.IO;

namespace TBydFramework.Connection.Runtime.Codec
{
    public interface IMessageEncoder<TMessage>
    {
        Task Encode(IMessage message, BinaryWriter writer);
    }
}
