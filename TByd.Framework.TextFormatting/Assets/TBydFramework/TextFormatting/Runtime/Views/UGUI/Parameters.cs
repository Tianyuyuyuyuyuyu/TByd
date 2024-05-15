﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TBydFramework.TextFormatting.Runtime.Views.UGUI.FormattableText;

namespace TBydFramework.TextFormatting.Runtime.Views.UGUI
{
    [Serializable]
    public abstract class Parameters
    {
        [SerializeField]
        protected FormattableText m_Text;
        public FormattableText Text
        {
            get { return this.m_Text; }
            set { this.m_Text = value; }
        }

        protected internal virtual void OnParameterChanged()
        {
            if (m_Text != null && m_Text.m_Parameters == null)
                this.m_Text.m_Parameters = this;
        }
    }

    [Serializable]
    public class ArrayParameters<T> : Parameters, IList<T>
    {
        static readonly T[] EMPTY = new T[0];
        [SerializeField]
        private int m_Capacity;
        private T[] m_Items;
        public ArrayParameters()
        {
        }

        internal ArrayParameters(FormattableText text, int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("The capacity cannot be less than 0.");

            this.m_Text = text;
            this.m_Capacity = capacity;
            this.Initialize();
        }

        private void Initialize()
        {
            if (m_Items == null || m_Items.Length != m_Capacity)
                this.m_Items = m_Capacity == 0 ? EMPTY : new T[m_Capacity];
        }

        public T this[int index]
        {
            get
            {
                if (index >= m_Capacity)
                    throw new IndexOutOfRangeException();
                this.Initialize();
                return m_Items[index];
            }
            set
            {
                if (index >= m_Capacity)
                    throw new IndexOutOfRangeException();
                this.Initialize();
                this.m_Items[index] = value;
                this.OnParameterChanged();
            }
        }

        protected internal override void OnParameterChanged()
        {
            try
            {
                base.OnParameterChanged();
                if (m_Text == null || !m_Text.enabled)
                    return;

                this.Initialize();
                m_Text.SetText(BUFFER.Clear().AppendFormat<T>(m_Text.Format, m_Items));
            }
            catch (Exception e)
            {
#if DEBUG
                if (Application.isEditor)
                    Debug.LogWarning(e);
#endif
                m_Text.SetText(BUFFER.Clear().Append(m_Text.Format));
            }
        }

        public int Count => m_Capacity;

        #region IList<T>
        bool ICollection<T>.IsReadOnly => throw new NotSupportedException();

        int IList<T>.IndexOf(T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Contains(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }
        #endregion
    }


    [Serializable]
    public class GenericParameters<P1> : Parameters
    {
        private P1 parameter1;
        public P1 Parameter1
        {
            get { return this.parameter1; }
            set
            {
                this.parameter1 = value;
                OnParameterChanged();
            }
        }

        protected internal override void OnParameterChanged()
        {
            try
            {
                base.OnParameterChanged();
                if (m_Text == null || !m_Text.enabled)
                    return;
                m_Text.SetText(BUFFER.Clear().AppendFormat<P1>(m_Text.Format, parameter1));
            }
            catch (Exception e)
            {
#if DEBUG
                if (Application.isEditor)
                    Debug.LogWarning(e);
#endif
                m_Text.SetText(BUFFER.Clear().Append(m_Text.Format));
            }
        }
    }

    [Serializable]
    public class GenericParameters<P1, P2> : Parameters
    {
        private P1 parameter1;
        private P2 parameter2;
        public P1 Parameter1
        {
            get { return this.parameter1; }
            set
            {
                this.parameter1 = value;
                OnParameterChanged();
            }
        }

        public P2 Parameter2
        {
            get { return this.parameter2; }
            set
            {
                this.parameter2 = value;
                OnParameterChanged();
            }
        }

        protected internal override void OnParameterChanged()
        {
            try
            {
                base.OnParameterChanged();
                if (m_Text == null || !m_Text.enabled)
                    return;

                m_Text.SetText(BUFFER.Clear().AppendFormat<P1, P2>(m_Text.Format, parameter1, parameter2));
            }
            catch (Exception e)
            {
#if DEBUG
                if (Application.isEditor)
                    Debug.LogWarning(e);
#endif
                m_Text.SetText(BUFFER.Clear().Append(m_Text.Format));
            }
        }
    }

    [Serializable]
    public class GenericParameters<P1, P2, P3> : Parameters
    {
        private P1 parameter1;
        private P2 parameter2;
        private P3 parameter3;
        public P1 Parameter1
        {
            get { return this.parameter1; }
            set
            {
                this.parameter1 = value;
                OnParameterChanged();
            }
        }

        public P2 Parameter2
        {
            get { return this.parameter2; }
            set
            {
                this.parameter2 = value;
                OnParameterChanged();
            }
        }

        public P3 Parameter3
        {
            get { return this.parameter3; }
            set
            {
                this.parameter3 = value;
                OnParameterChanged();
            }
        }

        protected internal override void OnParameterChanged()
        {
            try
            {
                base.OnParameterChanged();
                if (m_Text == null || !m_Text.enabled)
                    return;

                m_Text.SetText(BUFFER.Clear().AppendFormat<P1, P2, P3>(m_Text.Format, parameter1, parameter2, Parameter3));
            }
            catch (Exception e)
            {
#if DEBUG
                if (Application.isEditor)
                    Debug.LogWarning(e);
#endif
                m_Text.SetText(BUFFER.Clear().Append(m_Text.Format));
            }
        }
    }

    [Serializable]
    public class GenericParameters<P1, P2, P3, P4> : Parameters
    {
        private P1 parameter1;
        private P2 parameter2;
        private P3 parameter3;
        private P4 parameter4;
        public P1 Parameter1
        {
            get { return this.parameter1; }
            set
            {
                this.parameter1 = value;
                OnParameterChanged();
            }
        }

        public P2 Parameter2
        {
            get { return this.parameter2; }
            set
            {
                this.parameter2 = value;
                OnParameterChanged();
            }
        }

        public P3 Parameter3
        {
            get { return this.parameter3; }
            set
            {
                this.parameter3 = value;
                OnParameterChanged();
            }
        }

        public P4 Parameter4
        {
            get { return this.parameter4; }
            set
            {
                this.parameter4 = value;
                OnParameterChanged();
            }
        }

        protected internal override void OnParameterChanged()
        {
            try
            {
                base.OnParameterChanged();
                if (m_Text == null || !m_Text.enabled)
                    return;

                m_Text.SetText(BUFFER.Clear().AppendFormat<P1, P2, P3, P4>(m_Text.Format, parameter1, parameter2, Parameter3, parameter4));
            }
            catch (Exception e)
            {
#if DEBUG
                if (Application.isEditor)
                    Debug.LogWarning(e);
#endif
                m_Text.SetText(BUFFER.Clear().Append(m_Text.Format));
            }
        }
    }
}
