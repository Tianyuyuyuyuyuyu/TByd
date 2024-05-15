using TBydFramework.Runtime.Views.UI;
using TMPro;

namespace TBydFramework.TextMeshPro.Runtime.Views.UI
{
    public class TMPToastView : ToastViewBase
    {
        public TextMeshProUGUI text;

        protected override void OnContentChanged()
        {
            if (this.text != null)
                this.text.text = this.content;
        }
    }
}
