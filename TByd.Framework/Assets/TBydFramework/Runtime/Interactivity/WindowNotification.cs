namespace TBydFramework.Runtime.Interactivity
{
    public class WindowNotification
    {
        public static WindowNotification CreateShowNotification(bool ignoreAnimation = true, bool waitDismissed = false)
        {
            return new WindowNotification(ActionType.SHOW, ignoreAnimation, null, waitDismissed);
        }

        public static WindowNotification CreateShowNotification(object viewModel, bool ignoreAnimation = true, bool waitDismissed = false)
        {
            return new WindowNotification(ActionType.SHOW, ignoreAnimation, viewModel, waitDismissed);
        }

        public static WindowNotification CreateHideNotification(bool ignoreAnimation = true)
        {
            return new WindowNotification(ActionType.HIDE, ignoreAnimation);
        }

        public static WindowNotification CreateDismissNotification(bool ignoreAnimation = true)
        {
            return new WindowNotification(ActionType.DISMISS, ignoreAnimation);
        }

        public WindowNotification(ActionType actionType) : this(actionType, true, null)
        {
        }

        public WindowNotification(ActionType actionType, bool ignoreAnimation) : this(actionType, ignoreAnimation, null)
        {
        }

        public WindowNotification(ActionType actionType, object viewModel, bool waitDismissed = false) : this(actionType, true, viewModel, waitDismissed)
        {
        }

        public WindowNotification(ActionType actionType, bool ignoreAnimation, object viewModel, bool waitDismissed = false)
        {
            this.IgnoreAnimation = ignoreAnimation;
            this.ActionType = actionType;
            this.ViewModel = viewModel;
            this.WaitDismissed = waitDismissed;
        }

        public bool IgnoreAnimation { get; private set; }

        public ActionType ActionType { get; private set; }

        public object ViewModel { get; private set; }

        public bool WaitDismissed { get; private set; }
    }

    public enum ActionType
    {
        CREATE,
        SHOW,
        HIDE,
        DISMISS
    }
}
