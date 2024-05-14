using System;
using System.Threading.Tasks;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Interactivity;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Runtime.Views.InterationActions
{
    public class AsyncWindowInteractionAction : AsyncLoadableInteractionActionBase<WindowNotification>
    {
        private Window window;
        public AsyncWindowInteractionAction(string viewName) : this(viewName, null,null)
        {
        }

        public AsyncWindowInteractionAction(string viewName, IUIViewLocator locator) : base(viewName, locator)
        {
        }

        public AsyncWindowInteractionAction(string viewName, IWindowManager windowManager) : base(viewName, windowManager)
        {
        }

        public AsyncWindowInteractionAction(string viewName, IUIViewLocator locator, IWindowManager windowManager) : base(viewName, locator, windowManager)
        {
        }

        public Window Window { get { return this.window; } }

        public override Task Action(WindowNotification notification)
        {
            bool ignoreAnimation = notification.IgnoreAnimation;
            switch (notification.ActionType)
            {
                case Interactivity.ActionType.CREATE:
                    return Create(notification.ViewModel);
                case Interactivity.ActionType.SHOW:
                    return Show(notification.ViewModel, notification.WaitDismissed, ignoreAnimation);
                case Interactivity.ActionType.HIDE:
                    return Hide(ignoreAnimation);
                case Interactivity.ActionType.DISMISS:
                    return Dismiss(ignoreAnimation);
            }
            return Task.CompletedTask;
        }

        protected async Task Create(object viewModel)
        {
            try
            {
                window = await LoadWindowAsync<Window>();
                if (window == null)
                    throw new NotFoundException(string.Format("Not found the window named \"{0}\".", ViewName));

                if (viewModel != null)
                    window.SetDataContext(viewModel);

                window.Create();
            }
            catch (Exception e)
            {
                window = null;
                throw e;
            }
        }

        protected async Task Show(object viewModel, bool waitDismissed, bool ignoreAnimation = false)
        {
            try
            {
                if (window == null)
                    await Create(viewModel);

                window.WaitDismissed().Callbackable().OnCallback(r =>
                {
                    this.window = null;
                });

                await window.Show(ignoreAnimation);

                if (waitDismissed)
                    await window.WaitDismissed();
            }
            catch (Exception e)
            {
                if (window != null)
                    await window.Dismiss(ignoreAnimation);

                this.window = null;
                throw e;
            }
        }

        protected async Task Hide(bool ignoreAnimation = false)
        {
            if (window != null)
                await window.Hide(ignoreAnimation);
        }

        protected async Task Dismiss(bool ignoreAnimation = false)
        {
            if (window != null)
                await window.Dismiss(ignoreAnimation);
        }
    }
}
