﻿using System.Collections.Generic;
using TBydFramework.UIToolkit.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using PointerType = UnityEngine.UIElements.PointerType;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.ViewModels;
using TBydFramework.Runtime.Views;

namespace TBydFramework.Examples
{
    public class TestWindow : UIToolkitWindow
    {
        protected override void OnCreate(IBundle bundle)
        {
            //VisualTree.Query(null, "elem").ForEach(e => { e.RegisterCallback<PointerDownEvent>(OnFingerDown); });
            //VisualTree.Query(null, "elem").ForEach(e => { e.RegisterCallback<PointerUpEvent>(OnFingerUp); });
            //VisualTree.Query(null, "elem").ForEach(e => { e.RegisterCallback<MouseDownEvent>(OnMouseDownEvent); });
            //VisualTree.Query(null, "elem").ForEach(e => { e.RegisterCallback<MouseUpEvent>(OnMouseUpEvent); });

            //RootVisualElement.Query<Button>().ForEach((button) =>
            //{
            //    button.clicked += () => { button.text = button.text.EndsWith("Button") ? "Button (Clicked)" : "Button"; };
            //});

            var bindingSet = this.CreateBindingSet(new TestWindowViewMode());
            bindingSet.Bind(this.Q<Button>()).For(v => v.clickable).To(vm => vm.OnClick);

            //bindingSet.Bind<Button>().For(v => v.clickable).To(vm => vm.OnClick);
            bindingSet.Build();

        }

        private void OnFingerDown(PointerDownEvent evt)
        {
            if (evt.pointerType == PointerType.touch && evt.currentTarget == evt.target)
            {
                var ve = evt.target as VisualElement;
                OnDown(ve);

                // Prevent compatibility mouse events from being fired for this pointer.
                evt.PreventDefault();
            }
        }

        private void OnMouseDownEvent(MouseDownEvent evt)
        {
            if (evt.currentTarget == evt.target)
            {
                var ve = evt.target as VisualElement;
                OnDown(ve);
            }
        }

        private void OnFingerUp(PointerUpEvent evt)
        {
            if (evt.pointerType == PointerType.touch && evt.currentTarget == evt.target)
            {
                var ve = evt.target as VisualElement;
                OnUp(ve);
            }
        }

        private void OnMouseUpEvent(MouseUpEvent evt)
        {
            if (evt.currentTarget == evt.target)
            {
                var ve = evt.target as VisualElement;
                OnUp(ve);
            }
        }

        private void OnDown(VisualElement ve)
        {
            if (m_State != GameState.WaitUserAnswer)
                return;

            ve.AddToClassList("active");
            var root = this.RootVisualElement;
            root.Q("runtime-panel-container").RemoveFromClassList("error");
            root.Q("runtime-panel-container").RemoveFromClassList("success");
        }

        private void OnUp(VisualElement ve)
        {
            if (m_State != GameState.WaitUserAnswer)
                return;

            ve.RemoveFromClassList("active");
            var root = this.RootVisualElement;
            if (m_SequenceIndex < 3)
            {
                var classes = ve.GetClasses();
                foreach (var c in classes)
                {
                    if (c.StartsWith("elem_"))
                    {
                        var idStr = c.Substring(5, 1);
                        var id = int.Parse(idStr);
                        if (m_Sequence[m_SequenceIndex + 1] == id)
                        {
                            m_SequenceIndex++;
                        }
                        else
                        {
                            root.Q("runtime-panel-container").AddToClassList("error");
                        }
                    }

                }
            }

            if (m_SequenceIndex == 3)
            {
                root.Q("runtime-panel-container").RemoveFromClassList("error");
                root.Q("runtime-panel-container").AddToClassList("success");
            }

        }

        enum GameState
        {
            ShowSequence,
            WaitUserAnswer
        }

        private System.Random m_Random = new System.Random();

        private float m_Update;
        private GameState m_State = GameState.ShowSequence;
        private List<int> m_Sequence = new List<int> { 1, 1, 1, 1 };
        private int m_SequenceIndex = -1;

        //public void Update()
        //{

        //    if (m_State == GameState.ShowSequence && m_SequenceIndex == -1)
        //    {
        //        var root = this.RootVisualElement;
        //        root.Q("runtime-panel-container").RemoveFromClassList("error");
        //        root.Q("runtime-panel-container").RemoveFromClassList("success");

        //        for (var i = 0; i < 4; i++)
        //        {
        //            m_Sequence[i] = m_Random.Next((4)) + 1;
        //        }

        //        m_SequenceIndex = 0;
        //        m_Update = 0;
        //        return;
        //    }

        //    if (m_State == GameState.ShowSequence && m_SequenceIndex > -1)
        //    {
        //        var root = this.RootVisualElement;
        //        if (m_Update == 0)
        //        {
        //            if (m_SequenceIndex - 1 >= 0)
        //            {
        //                var ve = root.Q(null, "elem_" + (m_Sequence[m_SequenceIndex - 1]));
        //                ve.RemoveFromClassList("active");
        //            }
        //            if (m_SequenceIndex < 4)
        //            {
        //                var ve = root.Q(null, "elem_" + (m_Sequence[m_SequenceIndex]));
        //                ve.AddToClassList("active");
        //            }
        //        }

        //        if (m_Update > 0.4)
        //        {
        //            var ve = root.Q(null, "elem_" + (m_Sequence[m_SequenceIndex]));
        //            ve.RemoveFromClassList("active");
        //        }

        //        if (m_Update > 0.5)
        //        {
        //            m_SequenceIndex++;

        //            if (m_SequenceIndex == 4)
        //            {
        //                m_SequenceIndex = -1;
        //                m_State = GameState.WaitUserAnswer;
        //            }

        //            m_Update = 0;
        //            return;
        //        }
        //    }

        //    if (m_State == GameState.WaitUserAnswer)
        //    {
        //        if (m_Update > 5.0)
        //        {
        //            m_State = GameState.ShowSequence;
        //            m_SequenceIndex = -1;
        //            m_Update = 0;
        //            return;
        //        }
        //    }

        //    m_Update += Time.deltaTime;
        //}
    }

    public class TestWindowViewMode : ViewModelBase
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.Set<string>(ref name, value); }
        }


        public void OnClick()
        {
            Debug.LogFormat("Button OnClick");
        }
    }
}
