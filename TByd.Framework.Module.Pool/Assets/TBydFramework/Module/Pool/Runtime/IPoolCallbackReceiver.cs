namespace TBydFramework.Module.Pool.Runtime
{
    public interface IPoolCallbackReceiver
    {
        void OnRent();
        void OnReturn();
    }
}