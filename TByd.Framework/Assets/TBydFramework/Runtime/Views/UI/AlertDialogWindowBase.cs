using TBydFramework.Runtime.ViewModels.UI;
using UnityEngine;

namespace TBydFramework.Runtime.Views.UI
{
    public abstract class AlertDialogWindowBase : Window
    {
        public GameObject Content;

        protected IUIView contentView;

        protected AlertDialogViewModel viewModel;

        public virtual IUIView ContentView
        {
            get { return this.contentView; }
            set
            {
                if (this.contentView == value)
                    return;

                if (this.contentView != null)
                    GameObject.Destroy(this.contentView.Owner);

                this.contentView = value;
                if (this.contentView != null && this.contentView.Owner != null && this.Content != null)
                {
                    this.contentView.Visibility = true;
                    this.contentView.Transform.SetParent(this.Content.transform, false);
                }
            }
        }

        public virtual AlertDialogViewModel ViewModel
        {
            get { return this.viewModel; }
            set
            {
                if (this.viewModel == value)
                    return;

                this.viewModel = value;
                this.OnChangeViewModel();
            }
        }
        protected override void OnCreate(IBundle bundle)
        {
            this.WindowType = WindowType.DIALOG;
        }

        protected abstract void OnChangeViewModel();

        public abstract void Cancel();
    }
}
