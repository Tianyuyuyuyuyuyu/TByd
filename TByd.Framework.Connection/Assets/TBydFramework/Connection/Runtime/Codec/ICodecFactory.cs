namespace TBydFramework.Connection.Runtime.Codec
{
    public interface ICodecFactory<TMessage>
    {
        IMessageEncoder<TMessage> CreateEncoder();

        IMessageDecoder<TMessage> CreateDecoder();

    }
}
