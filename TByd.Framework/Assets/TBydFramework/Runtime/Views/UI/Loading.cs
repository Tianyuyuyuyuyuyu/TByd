using System;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Runtime.Views.UI
{
    public class Loading : UIBase, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Loading));

        private const string DEFAULT_VIEW_NAME = "UI/Loading";
        private static object _lock = new object();
        private static int refCount = 0;
        private static LoadingWindow window;
        private static string viewName;
        private bool ignoreAnimation;
        public static string ViewName
        {
            get { return string.IsNullOrEmpty(viewName) ? DEFAULT_VIEW_NAME : viewName; }
            set { viewName = value; }
        }

        public static Loading Show(bool ignoreAnimation = false)
        {
            return new Loading(ignoreAnimation);
        }

        protected Loading(bool ignoreAnimation)
        {
            this.ignoreAnimation = ignoreAnimation;
            lock (_lock)
            {
                if (refCount <= 0)
                {
                    IUIViewLocator locator = GetUIViewLocator();
                    window = locator.LoadWindow<LoadingWindow>(ViewName);
                    window.Create();
                    window.Show(this.ignoreAnimation);
                }
                refCount++;
            }
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
                Execution.Executors.RunOnMainThread(() =>
                {
                    lock (_lock)
                    {
                        refCount--;
                        if (refCount <= 0)
                        {
                            window.Dismiss(this.ignoreAnimation);
                            window = null;
                        }
                    }
                });
            }
        }

        ~Loading()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion      
    }
}
