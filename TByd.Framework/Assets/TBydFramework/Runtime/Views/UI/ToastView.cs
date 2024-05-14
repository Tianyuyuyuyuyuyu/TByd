using UnityEngine.UI;

namespace TBydFramework.Runtime.Views.UI
{
    public class ToastView : ToastViewBase
    {
        public Text text;

        protected override void OnContentChanged()
        {
            if (this.text != null)
                this.text.text = this.content;
        }
    }
}
