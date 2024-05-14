using System;

namespace TBydFramework.Runtime.ViewModels.UI
{
    public class AlertDialogViewModel : ViewModelBase
    {
        protected string title;
        protected string message;
        protected string confirmButtonText;
        protected string neutralButtonText;
        protected string cancelButtonText;
        protected bool canceledOnTouchOutside;
        protected bool closed;
        protected int result;
        protected Action<int> click;

        /// <summary>
        /// The title of the dialog box. This may be null.
        /// </summary>
        public virtual string Title
        {
            get { return this.title; }
            set { this.Set(ref this.title, value); }
        }

        /// <summary>
        /// The message to be shown to the user.
        /// </summary>
        public virtual string Message
        {
            get { return this.message; }
            set { this.Set(ref this.message, value); }
        }

        /// <summary>
        /// The text shown in the "confirm" button in the dialog box. 
        /// If left null, the button will be invisible.
        /// </summary>
        public virtual string ConfirmButtonText
        {
            get { return this.confirmButtonText; }
            set { this.Set(ref this.confirmButtonText, value); }
        }

        /// <summary>
        /// The text shown in the "neutral" button in the dialog box. 
        /// If left null, the button will be invisible.
        /// </summary>
        public virtual string NeutralButtonText
        {
            get { return this.neutralButtonText; }
            set { this.Set(ref this.neutralButtonText, value); }
        }

        /// <summary>
        /// The text shown in the "cancel" button in the dialog box. 
        /// If left null, the button will be invisible.
        /// </summary>
        public virtual string CancelButtonText
        {
            get { return this.cancelButtonText; }
            set { this.Set(ref this.cancelButtonText, value); }
        }

        /// <summary>
        /// Whether the dialog box is canceled when 
        /// touched outside the window's bounds. 
        /// </summary>
        public virtual bool CanceledOnTouchOutside
        {
            get { return this.canceledOnTouchOutside; }
            set { this.Set(ref this.canceledOnTouchOutside, value); }
        }

        /// <summary>
        /// A callback that should be executed after
        /// the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button
        /// (false) was pressed by the user.
        /// </summary>
        public virtual Action<int> Click
        {
            get { return this.click; }
            set { this.Set(ref this.click, value); }
        }

        /// <summary>
        /// The dialog box has been closed.
        /// </summary>
        public virtual bool Closed
        {
            get { return this.closed; }
            protected set { this.Set(ref this.closed, value); }
        }

        /// <summary>
        /// result
        /// </summary>
        public virtual int Result
        {
            get { return this.result; }
        }

        public virtual void OnClick(int which)
        {
            try
            {
                this.result = which;
                var click = this.Click;
                if (click != null)
                    click(which);
            }
            catch (Exception) { }
            finally
            {
                this.Closed = true;
            }
        }
    }
}
