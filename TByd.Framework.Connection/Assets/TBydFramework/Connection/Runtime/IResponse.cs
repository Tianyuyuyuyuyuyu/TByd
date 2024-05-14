namespace TBydFramework.Connection.Runtime
{
    public interface IResponse : IMessage
    {
        uint Sequence { get; }
    }
}
