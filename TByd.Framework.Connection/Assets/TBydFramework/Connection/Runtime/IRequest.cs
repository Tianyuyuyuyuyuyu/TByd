namespace TBydFramework.Connection.Runtime
{
    public interface IRequest : IMessage
    {
        uint Sequence { get; }
    }
}
