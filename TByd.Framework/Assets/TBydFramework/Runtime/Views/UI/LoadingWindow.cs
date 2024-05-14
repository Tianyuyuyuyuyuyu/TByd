namespace TBydFramework.Runtime.Views.UI
{
    public class LoadingWindow : Window
    {
        protected override void OnCreate(IBundle bundle)
        {
            this.WindowType = WindowType.PROGRESS;
        }
    }
}
