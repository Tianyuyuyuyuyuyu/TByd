using TBydFramework.Runtime.Views.Animations;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace TBydFramework.UIToolkit.Runtime
{
    public class UIToolkitAlphaAnimation : UIAnimation
    {
        [Range(0f, 1f)]
        public float from = 1f;
        [Range(0f, 1f)]
        public float to = 1f;

        public float duration = 2f;

        private UIToolkitWindow window;

        void OnEnable()
        {
            this.window = this.GetComponent<UIToolkitWindow>();
            switch (this.AnimationType)
            {
                case AnimationType.EnterAnimation:
                    this.window.EnterAnimation = this;
                    break;
                case AnimationType.ExitAnimation:
                    this.window.ExitAnimation = this;
                    break;
                case AnimationType.ActivationAnimation:
                    this.window.ActivationAnimation = this;
                    break;
                case AnimationType.PassivationAnimation:
                    this.window.PassivationAnimation = this;
                    break;
            }

            if (this.AnimationType == AnimationType.ActivationAnimation || this.AnimationType == AnimationType.EnterAnimation)
            {
                this.window.RootVisualElement.style.opacity = from;
            }
        }

        public override IAnimation Play()
        {
            this.OnStart();
            (this.window.RootVisualElement as ITransitionAnimations).Start(from, to, (int)(duration * 1000), (e, value) =>
            {
                this.window.RootVisualElement.style.opacity = value;
                if (value == to)
                    this.OnEnd();
            });
            return this;
        }
    }
}