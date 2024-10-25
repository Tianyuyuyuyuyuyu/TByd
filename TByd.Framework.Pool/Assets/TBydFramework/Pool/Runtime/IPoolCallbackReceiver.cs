namespace TBydFramework.Pool.Runtime
{
    public interface IPoolCallbackReceiver
    {
        void OnRent();
        void OnReturn();
    }
}