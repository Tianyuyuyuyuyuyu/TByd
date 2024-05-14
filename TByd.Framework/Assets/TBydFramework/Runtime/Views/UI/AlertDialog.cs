using System;
using System.Threading.Tasks;
using TBydFramework.Runtime.Execution;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.ViewModels.UI;
using TBydFramework.Runtime.Views.Locators;
using UnityEngine;

namespace TBydFramework.Runtime.Views.UI
{
    public class AlertDialog : UIBase, IDialog
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AlertDialog));

        public const int BUTTON_POSITIVE = -1;
        public const int BUTTON_NEGATIVE = -2;
        public const int BUTTON_NEUTRAL = -3;

        private const string DEFAULT_VIEW_NAME = "UI/AlertDialog";

        private static string viewName;
        public static string ViewName
        {
            get { return string.IsNullOrEmpty(viewName) ? DEFAULT_VIEW_NAME : viewName; }
            set { viewName = value; }
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(
            string message,
            string title)
        {
            return ShowMessage(message, title, null, null, null, true, null);
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(
            string message,
            string title,
            string buttonText,
            Action<int> afterHideCallback)
        {
            return ShowMessage(message, title, buttonText, null, null, false, afterHideCallback);
        }

        /// <summary>
        /// Displays information to the user.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="confirmButtonText">The text shown in the "confirm" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="cancelButtonText">The text shown in the "cancel" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button
        /// (false) was pressed by the user.</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(
            string message,
            string title,
            string confirmButtonText,
            string cancelButtonText,
            Action<int> afterHideCallback)
        {
            return ShowMessage(message, title, confirmButtonText, null, cancelButtonText, false, afterHideCallback);
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="confirmButtonText">The text shown in the "confirm" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="neutralButtonText">The text shown in the "neutral" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="cancelButtonText">The text shown in the "cancel" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="canceledOnTouchOutside">Whether the dialog box is canceled when 
        /// touched outside the window's bounds. </param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button
        /// (false) was pressed by the user.</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(
            string message,
            string title,
            string confirmButtonText,
            string neutralButtonText,
            string cancelButtonText,
            bool canceledOnTouchOutside,
            Action<int> afterHideCallback)
        {
            AlertDialogViewModel viewModel = new AlertDialogViewModel();
            viewModel.Message = message;
            viewModel.Title = title;
            viewModel.ConfirmButtonText = confirmButtonText;
            viewModel.NeutralButtonText = neutralButtonText;
            viewModel.CancelButtonText = cancelButtonText;
            viewModel.CanceledOnTouchOutside = canceledOnTouchOutside;
            viewModel.Click = afterHideCallback;

            return ShowMessage(ViewName, viewModel);
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="contentView">The custom content view to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="confirmButtonText">The text shown in the "confirm" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="neutralButtonText">The text shown in the "neutral" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="cancelButtonText">The text shown in the "cancel" button
        /// in the dialog box. If left null, the button will be invisible.</param>
        /// <param name="canceledOnTouchOutside">Whether the dialog box is canceled when 
        /// touched outside the window's bounds. </param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button
        /// (false) was pressed by the user.</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(
            IUIView contentView,
            string title,
            string confirmButtonText,
            string neutralButtonText,
            string cancelButtonText,
            bool canceledOnTouchOutside,
            Action<int> afterHideCallback)
        {
            AlertDialogViewModel viewModel = new AlertDialogViewModel();
            viewModel.Title = title;
            viewModel.ConfirmButtonText = confirmButtonText;
            viewModel.NeutralButtonText = neutralButtonText;
            viewModel.CancelButtonText = cancelButtonText;
            viewModel.CanceledOnTouchOutside = canceledOnTouchOutside;
            viewModel.Click = afterHideCallback;

            IUIViewLocator locator = GetUIViewLocator();
            AlertDialogWindowBase window = locator.LoadView<AlertDialogWindowBase>(ViewName);
            if (window == null)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("Not found the dialog window named \"{0}\".", viewName);

                throw new NotFoundException(string.Format("Not found the dialog window named \"{0}\".", viewName));
            }

            AlertDialog dialog = new AlertDialog(window, contentView, viewModel);
            dialog.Show();
            return dialog;
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="viewModel">The view model of the dialog box</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(AlertDialogViewModel viewModel)
        {
            return ShowMessage(ViewName, null, viewModel);
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="viewName">The view name of the dialog box,if it is null, use the default view name</param>
        /// <param name="viewModel">The view model of the dialog box</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(string viewName, AlertDialogViewModel viewModel)
        {
            return ShowMessage(viewName, null, viewModel);
        }

        /// <summary>
        /// Displays information to the user. 
        /// </summary>
        /// <param name="viewName">The view name of the dialog box,if it is null, use the default view name</param>
        /// <param name="contentViewName">The custom content view name to be shown to the user.</param>
        /// <param name="viewModel">The view model of the dialog box</param>
        /// <returns>A AlertDialog.</returns>
        public static AlertDialog ShowMessage(string viewName, string contentViewName, AlertDialogViewModel viewModel)
        {
            AlertDialogWindowBase window = null;
            IUIView contentView = null;
            try
            {
                if (string.IsNullOrEmpty(viewName))
                    viewName = ViewName;

                IUIViewLocator locator = GetUIViewLocator();
                window = locator.LoadView<AlertDialogWindowBase>(viewName);
                if (window == null)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("Not found the dialog window named \"{0}\".", viewName);

                    throw new NotFoundException(string.Format("Not found the dialog window named \"{0}\".", viewName));
                }

                if (!string.IsNullOrEmpty(contentViewName))
                    contentView = locator.LoadView<IUIView>(contentViewName);

                AlertDialog dialog = new AlertDialog(window, contentView, viewModel);
                dialog.Show();
                return dialog;
            }
            catch (Exception e)
            {
                if (window != null)
                    window.Dismiss();
                if (contentView != null)
                    GameObject.Destroy(contentView.Owner);

                throw e;
            }
        }

        private TaskCompletionSource<int> source;
        private AlertDialogWindowBase window;
        private IUIView contentView;
        private AlertDialogViewModel viewModel;

        public AlertDialog(AlertDialogWindowBase window, AlertDialogViewModel viewModel) : this(window, null, viewModel)
        {
        }

        public AlertDialog(AlertDialogWindowBase window, IUIView contentView, AlertDialogViewModel viewModel)
        {
            this.source = new TaskCompletionSource<int>();
            this.window = window;
            this.contentView = contentView;
            this.viewModel = viewModel;

            EventHandler handler = null;
            handler = (sender, e) =>
            {
                this.window.OnDismissed -= handler;
                source.SetResult(viewModel.Result);
            };
            this.window.OnDismissed += handler;
        }

        public virtual object WaitForClosed()
        {
            return Executors.WaitWhile(() => !this.viewModel.Closed);
        }

        public virtual Task<int> WaitForResult()
        {
            return source.Task;
        }

        public void Show()
        {
            this.window.ViewModel = this.viewModel;
            if (this.contentView != null)
                this.window.ContentView = this.contentView;
            this.window.Create();
            this.window.Show();
        }

        public void Cancel()
        {
            this.window.Cancel();
        }
    }
}
