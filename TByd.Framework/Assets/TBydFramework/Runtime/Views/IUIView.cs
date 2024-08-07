﻿using TBydFramework.Runtime.Views.Animations;
using UnityEngine;

namespace TBydFramework.Runtime.Views
{
    /// <summary>
    /// UI view
    /// </summary>
    public interface IUIView : IView
    {
        RectTransform RectTransform { get; }

        float Alpha { get; set; }

        bool Interactable { get; set; }

        CanvasGroup CanvasGroup { get; }

        IAnimation EnterAnimation { get; set; }

        IAnimation ExitAnimation { get; set; }

    }
}
