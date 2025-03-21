﻿using System.Collections;
using UnityEngine;

namespace TBydFramework.Runtime.Views.Animations
{
    public class AlphaAnimation : UIAnimation
    {
        [Range(0f, 1f)]
        public float from = 1f;
        [Range(0f, 1f)]
        public float to = 1f;

        public float duration = 2f;

        private IUIView view;

        void OnEnable()
        {
            this.view = this.GetComponent<IUIView>();
            switch (this.AnimationType)
            {
                case AnimationType.EnterAnimation:
                    this.view.EnterAnimation = this;
                    break;
                case AnimationType.ExitAnimation:
                    this.view.ExitAnimation = this;
                    break;
                case AnimationType.ActivationAnimation:
                    if (this.view is IWindowView)
                        (this.view as IWindowView).ActivationAnimation = this;
                    break;
                case AnimationType.PassivationAnimation:
                    if (this.view is IWindowView)
                        (this.view as IWindowView).PassivationAnimation = this;
                    break;
            }

            if (this.AnimationType == AnimationType.ActivationAnimation || this.AnimationType == AnimationType.EnterAnimation)
            {
                this.view.CanvasGroup.alpha = from;
            }
        }

        public override IAnimation Play()
        {
            ////use the DoTween
            //this.view.CanvasGroup.DOFade (this.to, this.duration).OnStart (this.OnStart).OnComplete (this.OnEnd).Play ();		

            this.StartCoroutine(DoPlay());
            return this;
        }

        IEnumerator DoPlay()
        {
            this.OnStart();

            var delta = (to - from) / duration;
            var alpha = from;
            this.view.Alpha = alpha;
            if (delta > 0f)
            {
                while (alpha < to)
                {
                    alpha += delta * Time.deltaTime;
                    if (alpha > to)
                    {
                        alpha = to;
                    }
                    this.view.Alpha = alpha;
                    yield return null;
                }
            }
            else
            {
                while (alpha > to)
                {
                    alpha += delta * Time.deltaTime;
                    if (alpha < to)
                    {
                        alpha = to;
                    }
                    this.view.Alpha = alpha;
                    yield return null;
                }
            }

            this.OnEnd();
        }

    }
}
