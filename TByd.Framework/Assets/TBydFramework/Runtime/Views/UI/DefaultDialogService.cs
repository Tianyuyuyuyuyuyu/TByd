﻿using System;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Interactivity;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.ViewModels;
using TBydFramework.Runtime.ViewModels.UI;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Runtime.Views.UI
{
    public class DefaultDialogService : IDialogService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DefaultDialogService));

        public virtual IAsyncResult<int> ShowDialog(string title, string message)
        {
            return this.ShowDialog(title, message, null, null, null, true);
        }

        public virtual IAsyncResult<int> ShowDialog(string title, string message, string buttonText)
        {
            return this.ShowDialog(title, message, buttonText, null, null, false);
        }

        public virtual IAsyncResult<int> ShowDialog(string title, string message, string confirmButtonText, string cancelButtonText)
        {
            return this.ShowDialog(title, message, confirmButtonText, cancelButtonText, null, false);
        }

        public virtual IAsyncResult<int> ShowDialog(string title, string message, string confirmButtonText, string cancelButtonText, string neutralButtonText)
        {
            return this.ShowDialog(title, message, confirmButtonText, cancelButtonText, neutralButtonText, false);
        }

        public virtual IAsyncResult<int> ShowDialog(string title, string message, string confirmButtonText, string cancelButtonText, string neutralButtonText, bool canceledOnTouchOutside)
        {
            AsyncResult<int> result = new AsyncResult<int>();
            try
            {
                AlertDialog.ShowMessage(message, title, confirmButtonText, neutralButtonText, cancelButtonText, canceledOnTouchOutside, (which) => { result.SetResult(which); });
            }
            catch (Exception e)
            {
                result.SetException(e);
            }
            return result;
        }

        public virtual IAsyncResult<TViewModel> ShowDialog<TViewModel>(string viewName, TViewModel viewModel) where TViewModel : IViewModel
        {
            AsyncResult<TViewModel> result = new AsyncResult<TViewModel>();
            Window window = null;
            try
            {
                ApplicationContext context = Context.GetApplicationContext();
                IUIViewLocator locator = context.GetService<IUIViewLocator>();
                if (locator == null)
                {
                    if (log.IsWarnEnabled)
                        log.Warn("Not found the \"IUIViewLocator\".");

                    throw new NotFoundException("Not found the \"IUIViewLocator\".");
                }

                if (string.IsNullOrEmpty(viewName))
                    throw new ArgumentNullException("The view name is null.");

                window = locator.LoadView<Window>(viewName);
                if (window == null)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("Not found the dialog window named \"{0}\".", viewName);

                    throw new NotFoundException(string.Format("Not found the dialog window named \"{0}\".", viewName));
                }

                if (window is AlertDialogWindowBase && viewModel is AlertDialogViewModel)
                    (window as AlertDialogWindowBase).ViewModel = viewModel as AlertDialogViewModel;
                else
                    window.SetDataContext(viewModel);

                EventHandler handler = null;
                handler = (sender, eventArgs) =>
                {
                    window.OnDismissed -= handler;
                    result.SetResult(viewModel);
                };
                window.Create();
                window.OnDismissed += handler;
                window.Show(true);
            }
            catch (Exception e)
            {
                result.SetException(e);
                if (window != null)
                    window.Dismiss();
            }
            return result;
        }

        public IAsyncResult<IViewModel> ShowDialog(string viewName, IViewModel viewModel)
        {
            return this.ShowDialog<IViewModel>(viewName, viewModel);
        }
    }
}
