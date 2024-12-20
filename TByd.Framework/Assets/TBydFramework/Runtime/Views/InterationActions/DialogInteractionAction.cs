﻿using System;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Interactivity;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.ViewModels.UI;
using TBydFramework.Runtime.Views.Locators;
using TBydFramework.Runtime.Views.UI;

namespace TBydFramework.Runtime.Views.InterationActions
{
    public class DialogInteractionAction : InteractionActionBase<object>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogInteractionAction));

        private string viewName;
        public DialogInteractionAction(string viewName)
        {
            this.viewName = viewName;
        }

        public override void Action(object viewModel, Action callback)
        {
            Window window = null;
            try
            {
                ApplicationContext context = Context.GetApplicationContext();
                IUIViewLocator locator = context.GetService<IUIViewLocator>();
                if (locator == null)
                    throw new NotFoundException("Not found the \"IUIViewLocator\".");

                if (string.IsNullOrEmpty(viewName))
                    throw new ArgumentNullException("The view name is null.");

                window = locator.LoadView<Window>(viewName);
                if (window == null)
                    throw new NotFoundException(string.Format("Not found the dialog window named \"{0}\".", viewName));

                if (window is AlertDialogWindowBase && viewModel is AlertDialogViewModel)
                {
                    (window as AlertDialogWindowBase).ViewModel = viewModel as AlertDialogViewModel;
                }
                else if (window is AlertDialogWindowBase && viewModel is DialogNotification notification)
                {
                    AlertDialogViewModel dialogViewModel = new AlertDialogViewModel();
                    dialogViewModel.Message = notification.Message;
                    dialogViewModel.Title = notification.Title;
                    dialogViewModel.ConfirmButtonText = notification.ConfirmButtonText;
                    dialogViewModel.NeutralButtonText = notification.NeutralButtonText;
                    dialogViewModel.CancelButtonText = notification.CancelButtonText;
                    dialogViewModel.CanceledOnTouchOutside = notification.CanceledOnTouchOutside;
                    dialogViewModel.Click = (result) => notification.DialogResult = result;
                    (window as AlertDialogWindowBase).ViewModel = dialogViewModel;
                }
                else
                {
                    window.SetDataContext(viewModel);
                }
                                
                window.Create();
                window.WaitDismissed().Callbackable().OnCallback((r) =>
                {
                    callback?.Invoke();
                    callback = null;
                });
                window.Show(true);
            }
            catch (Exception e)
            {
                callback?.Invoke();
                callback = null;

                if (window != null)
                    window.Dismiss();

                if (log.IsWarnEnabled)
                    log.Error("", e);
            }
        }
    }
}
