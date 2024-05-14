namespace TBydFramework.Runtime.Interactivity
{
    public interface IInteractionAction
    {
        void OnRequest(object sender, InteractionEventArgs args);
    }
}
