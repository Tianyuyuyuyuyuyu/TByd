using System.Collections.Generic;
using TBydFramework.Runtime.Views.Animations;
using UnityEngine;

namespace TBydFramework.Runtime.Views
{
    public class WindowView : UIView, IWindowView
    {
        private IAnimation activationAnimation;
        private IAnimation passivationAnimation;

        public virtual IAnimation ActivationAnimation
        {
            get { return this.activationAnimation; }
            set { this.activationAnimation = value; }
        }

        public virtual IAnimation PassivationAnimation
        {
            get { return this.passivationAnimation; }
            set { this.passivationAnimation = value; }
        }

        public virtual List<IUIView> Views
        {
            get
            {
                var transform = this.Transform;
                List<IUIView> views = new List<IUIView>();
                int count = transform.childCount;
                for (int i = 0; i < count; i++)
                {
                    var child = transform.GetChild(i);
                    var view = child.GetComponent<IUIView>();
                    if (view != null)
                        views.Add(view);
                }
                return views;
            }
        }

        public virtual IUIView GetView(string name)
        {
            return this.Views.Find(v => v.Name.Equals(name));
        }

        public virtual void AddView(IUIView view, bool worldPositionStays = false)
        {
            if (view == null)
                return;

            Transform t = view.Transform;
            if (t == null || t.parent == this.transform)
                return;

            view.Owner.layer = this.gameObject.layer;
            t.SetParent(this.transform, worldPositionStays);
        }

        public virtual void AddView(IUIView view, UILayout layout)
        {
            if (view == null)
                return;

            Transform t = view.Transform;
            if (t == null)
                return;

            if (t.parent == this.transform)
            {
                if (layout != null)
                    layout(view.RectTransform);
                return;
            }

            view.Owner.layer = this.gameObject.layer;
            t.SetParent(this.transform, false);
            if (layout != null)
                layout(view.RectTransform);
        }

        public virtual void RemoveView(IUIView view, bool worldPositionStays = false)
        {
            if (view == null)
                return;

            Transform t = view.Transform;
            if (t == null || t.parent != this.transform)
                return;

            t.SetParent(null, worldPositionStays);
        }
    }
}
