using System;
using System.Threading.Tasks;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Interactivity;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Runtime.Views.InterationActions
{
    public abstract class AsyncLoadableInteractionActionBase<TNotification> : AsyncInteractionActionBase<TNotification>
    {
        private string viewName;
        private IUIViewLocator locator;
        private IWindowManager windowManager;

        public AsyncLoadableInteractionActionBase(string viewName, IUIViewLocator locator) : this(viewName, locator, null)
        {
        }

        public AsyncLoadableInteractionActionBase(string viewName, IWindowManager windowManager) : this(viewName, null, windowManager)
        {
        }

        public AsyncLoadableInteractionActionBase(string viewName, IUIViewLocator locator, IWindowManager windowManager)
        {
            this.viewName = viewName;
            this.locator = locator;
            this.windowManager = windowManager;
        }

        protected string ViewName { get { return this.viewName; } }

        protected IUIViewLocator Locator
        {
            get
            {
                if (locator == null)
                {
                    ApplicationContext context = Context.GetApplicationContext();
                    locator = context.GetService<IUIViewLocator>();
                }
                return this.locator;
            }
        }

        protected async Task<T> LoadViewAsync<T>() where T : IView
        {
            var locator = this.Locator;
            if (locator == null)
                throw new NotFoundException("Not found the \"IUIViewLocator\".");

            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentNullException("The view name is null.");

            return await locator.LoadViewAsync<T>(viewName);
        }

        protected async Task<T> LoadWindowAsync<T>() where T : IWindow
        {
            var locator = this.Locator;
            if (locator == null)
                throw new NotFoundException("Not found the \"IUIViewLocator\".");

            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentNullException("The view name is null.");

            return await locator.LoadWindowAsync<T>(windowManager, viewName);
        }
    }
}
