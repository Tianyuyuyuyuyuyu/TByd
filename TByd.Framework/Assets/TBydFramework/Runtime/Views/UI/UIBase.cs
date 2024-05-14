using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Runtime.Views.UI
{
    public abstract class UIBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UIBase));
        private const string DEFAULT_VIEW_LOCATOR_KEY = "_DEFAULT_VIEW_LOCATOR";

        protected static IUIViewLocator GetUIViewLocator()
        {
            ApplicationContext context = Context.GetApplicationContext();
            IUIViewLocator locator = context.GetService<IUIViewLocator>();
            if (locator != null)
                return locator;

            if (log.IsWarnEnabled)
                log.Warn("Not found the \"IUIViewLocator\" in the ApplicationContext.Try loading the Tips using the DefaultUIViewLocator.");

            locator = context.GetService<IUIViewLocator>(DEFAULT_VIEW_LOCATOR_KEY);
            if (locator == null)
            {
                locator = new DefaultUIViewLocator();
                context.GetContainer().Register(DEFAULT_VIEW_LOCATOR_KEY, locator);
            }
            return locator;
        }

        protected static IUIViewGroup GetCurrentViewGroup()
        {
            GlobalWindowManagerBase windowManager = GlobalWindowManagerBase.Root;
            IWindow window = windowManager.Current;
            while (window is WindowContainer windowContainer)
                window = windowContainer.Current;
            return window as IUIViewGroup;
        }
    }
}
