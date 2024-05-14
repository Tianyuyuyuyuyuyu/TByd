using TBydFramework.Runtime.Views.Animations;

namespace TBydFramework.Runtime.Views
{
    public interface IWindowView : IUIViewGroup
    {
        /// <summary>
        /// Activation animation
        /// </summary>
        IAnimation ActivationAnimation { get; set; }

        /// <summary>
        /// Passivation animation
        /// </summary>
        IAnimation PassivationAnimation { get; set; }
    }
}
