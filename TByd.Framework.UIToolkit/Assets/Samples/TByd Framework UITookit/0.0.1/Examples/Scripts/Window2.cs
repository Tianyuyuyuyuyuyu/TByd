using TBydFramework.UIToolkit.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.ViewModels;
using TBydFramework.Runtime.Views;

namespace TBydFramework.Examples
{
    public class Window2 : UIToolkitWindow
    {
        protected override void OnCreate(IBundle bundle)
        {
            var bindingSet = this.CreateBindingSet(new Window2ViewMode());
            bindingSet.Bind(this.Q<Toggle>("toggle")).For(v => v.value).To(vm => vm.Toggle);
            bindingSet.Bind(this.Q<TextField>("username")).For(v => v.value, v => v.RegisterValueChangedCallback).To(vm => vm.Name);
            bindingSet.Build();
            this.Q<Button>("close").clicked += () => this.Dismiss();

        }

        public class Window2ViewMode : ViewModelBase
        {
            private string name = "TianYu";
            private bool toggle = true;
            public string Name
            {
                get { return this.name; }
                set
                {
                    if (this.Set<string>(ref name, value))
                        Debug.LogFormat("Name:{0}", value);
                }
            }

            public bool Toggle
            {
                get { return this.toggle; }
                set { this.Set<bool>(ref toggle, value); }
            }


            public void OnClick()
            {
                Debug.LogFormat("Button OnClick");
            }
        }
    }
}
