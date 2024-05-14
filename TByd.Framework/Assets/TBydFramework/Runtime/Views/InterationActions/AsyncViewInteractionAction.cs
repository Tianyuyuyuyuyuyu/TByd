using System;
using System.Threading.Tasks;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Interactivity;
using TBydFramework.Runtime.Views.Locators;
using UnityEngine;

namespace TBydFramework.Runtime.Views.InterationActions
{
    public class AsyncViewInteractionAction : AsyncLoadableInteractionActionBase<VisibilityNotification>
    {
        private IViewGroup viewGroup;
        private UIView view;
        private bool autoDestroy;
        public AsyncViewInteractionAction(string viewName, IViewGroup viewGroup, bool autoDestroy = true) : this(viewName, viewGroup, null, autoDestroy)
        {
        }

        public AsyncViewInteractionAction(string viewName, IViewGroup viewGroup, IUIViewLocator locator, bool autoDestroy = true) : base(viewName, locator)
        {
            this.viewGroup = viewGroup;
            this.autoDestroy = autoDestroy;
        }

        public AsyncViewInteractionAction(UIView view, bool autoDestroy = false) : base(null, null, null)
        {
            this.view = view;
            this.autoDestroy = autoDestroy;
        }

        public UIView View { get { return this.view; } }

        public override Task Action(VisibilityNotification notification)
        {
            if (notification.Visible)
                return Show(notification.ViewModel, notification.WaitDisabled);
            else
                return Hide();
        }

        protected virtual async Task Show(object viewModel, bool waitDisabled)
        {
            try
            {
                if (view == null)
                    view = await this.LoadViewAsync<UIView>();

                if (view == null)
                    throw new NotFoundException(string.Format("Not found the view named \"{0}\".", ViewName));

                if (this.viewGroup != null)
                    this.viewGroup.AddView(view);

                if (autoDestroy)
                {
                    view.WaitDisabled().Callbackable().OnCallback(r =>
                    {
                        this.view = null;
                    });
                }

                if (viewModel != null)
                    view.SetDataContext(viewModel);

                view.Visibility = true;

                if (waitDisabled)
                    await view.WaitDisabled();
            }
            catch (Exception e)
            {
                if (autoDestroy)
                    Destroy();
                throw e;
            }
        }

        protected Task Hide()
        {
            if (view != null)
            {
                this.view.Visibility = false;
                if (autoDestroy)
                    Destroy();
            }
            return Task.CompletedTask;
        }

        private void Destroy()
        {
            if (view == null)
                return;

            GameObject go = view.Owner;
            if (go != null)
                GameObject.Destroy(go);
            this.view = null;
        }
    }
}
