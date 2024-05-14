using System.Threading.Tasks;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.ViewModels;
using TBydFramework.Runtime.Views.Locators;
using UnityEngine;

namespace TBydFramework.Runtime.Views.UI
{
    public class Tips : UIBase
    {
        public static Tips Create(UIView view, IUIViewGroup viewGroup = null)
        {
            if (viewGroup == null)
                viewGroup = GetCurrentViewGroup();

            view.Visibility = false;
            return new Tips(view, viewGroup);
        }
        public static Tips Create(string viewName, IUIViewGroup viewGroup = null)
        {
            IUIViewLocator locator = GetUIViewLocator();
            UIView view = locator.LoadView<UIView>(viewName);
            if (view == null)
                throw new NotFoundException("Not found the \"UIView\".");

            if (viewGroup == null)
                viewGroup = GetCurrentViewGroup();

            view.Visibility = false;
            return new Tips(view, viewGroup);
        }

        public static async Task<Tips> CreateAsync(string viewName, IUIViewGroup viewGroup = null)
        {
            IUIViewLocator locator = GetUIViewLocator();
            UIView view = await locator.LoadViewAsync<UIView>(viewName);
            if (view == null)
                throw new NotFoundException("Not found the \"UIView\".");

            if (viewGroup == null)
                viewGroup = GetCurrentViewGroup();

            view.Visibility = false;
            return new Tips(view, viewGroup);
        }

        private readonly IUIViewGroup viewGroup;
        private readonly UIView view;

        protected Tips(UIView view, IUIViewGroup viewGroup)
        {
            this.view = view;
            this.viewGroup = viewGroup;
        }

        public UIView View { get { return this.view; } }

        public void Show(IViewModel viewModel, UILayout layout = null)
        {
            this.viewGroup.AddView(this.view, layout);
            this.view.SetDataContext(viewModel);
            this.view.Visibility = true;

            if (this.view.EnterAnimation != null)
                this.view.EnterAnimation.Play();
        }

        public void Hide()
        {
            if (this.view == null || this.view.Owner == null)
                return;

            if (!this.view.Visibility)
                return;

            if (this.view.ExitAnimation != null)
            {
                this.view.ExitAnimation.OnEnd(() =>
                {
                    this.view.Visibility = false;
                }).Play();
            }
            else
            {
                this.view.Visibility = false;
            }
        }

        public void Dismiss()
        {
            if (this.view == null || this.view.Owner == null)
                return;

            if (!this.view.Visibility)
            {
                Object.Destroy(this.view.Owner);
                return;
            }

            if (this.view.ExitAnimation != null)
            {
                this.view.ExitAnimation.OnEnd(() =>
                {
                    this.view.Visibility = false;
                    Object.Destroy(this.view.Owner);
                }).Play();
            }
            else
            {
                this.view.Visibility = false;
                Object.Destroy(this.view.Owner);
            }
        }
    }
}
