namespace TBydFramework.Runtime.Execution
{
    public abstract class AbstractExecutor
    {
        static AbstractExecutor()
        {
            Executors.Create();
        }
    }
}
