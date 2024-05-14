namespace TBydFramework.Connection.Runtime
{
    public interface IChannelFactory
    {
        IChannel<IMessage> CreateChannel();
    }
}
