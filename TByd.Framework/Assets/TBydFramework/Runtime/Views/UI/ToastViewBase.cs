namespace TBydFramework.Runtime.Views.UI
{
    public abstract class ToastViewBase : UIView
    {
        protected string content;
        public string Content 
        {
            get { return this.content; }
            set 
            {
                if (string.Equals(content,value))
                    return;

                this.content = value;
                this.OnContentChanged();
            }
        }

        protected abstract void OnContentChanged();
    }
}
