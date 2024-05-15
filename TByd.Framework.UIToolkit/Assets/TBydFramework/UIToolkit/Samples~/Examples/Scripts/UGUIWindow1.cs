using UnityEngine;
using UnityEngine.UIElements;
using XFramework.Runtime.Binding;
using XFramework.Runtime.Commands;
using XFramework.Runtime.Interactivity;
using XFramework.Runtime.ViewModels;
using XFramework.Runtime.Views;
using XFramework.Runtime.Views.UI;
using XFramework.UIToolkit.Runtime;

namespace XFramework.Examples
{
    public class UGUIWindow1 : UIToolkitWindow
    {
        protected override void OnCreate(IBundle bundle)
        {
            var bindingSet = this.CreateBindingSet(new UGUIWindowViewMode());
            bindingSet.Bind(this.Q<Button>()).For(v=>v.clickable).To(vm => vm.Click);
            //bindingSet.Bind<Button>().For(v => v.clickable).To(vm => vm.Click);
            bindingSet.Bind().For(v => v.OnOpenDialogWindow).To(vm => vm.OpenRequest);
            bindingSet.Build();
        }

        protected void OnOpenDialogWindow(object sender, InteractionEventArgs args)
        {
            var callback = args.Callback;
            AlertDialog.ShowMessage("测试", "标题", "OK", r =>
            {
                if (callback != null)
                    callback();
            });
        }
    }

    public class UGUIWindowViewMode : ViewModelBase
    {
        private string name;
        private SimpleCommand command;
        private InteractionRequest openRequest;
        public UGUIWindowViewMode()
        {
            this.openRequest = new InteractionRequest(this);
            this.command = new SimpleCommand(() =>
            {
                this.command.Enabled = false;
                this.openRequest.Raise(()=> {
                    this.command.Enabled = true;
                });               
            });
        }

        public string Name
        {
            get { return this.name; }
            set { this.Set<string>(ref name, value); }
        }

        public IInteractionRequest OpenRequest
        {
            get { return this.openRequest; }
        }

        public ICommand Click
        {
            get { return this.command; }
        }

        public void OnClick()
        {
            Debug.LogFormat("Button OnClick");
        }
    }
}
