﻿#if NETFX_CORE
using System.Reflection;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NotifyCollectionChangedEventHandler = System.Collections.Specialized.NotifyCollectionChangedEventHandler;
using INotifyCollectionChanged = System.Collections.Specialized.INotifyCollectionChanged;
using NotifyCollectionChangedAction = System.Collections.Specialized.NotifyCollectionChangedAction;
using NotifyCollectionChangedEventArgs = System.Collections.Specialized.NotifyCollectionChangedEventArgs;
using INotifyPropertyChanged = System.ComponentModel.INotifyPropertyChanged;
using PropertyChangedEventArgs = System.ComponentModel.PropertyChangedEventArgs;
using PropertyChangedEventHandler = System.ComponentModel.PropertyChangedEventHandler;

namespace TBydFramework.Runtime.Observables
{
    [Serializable]
    public class ObservableList<T> : IList<T>, IList, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private static readonly PropertyChangedEventArgs CountEventArgs = new PropertyChangedEventArgs("Count");
        private static readonly PropertyChangedEventArgs IndexerEventArgs = new PropertyChangedEventArgs("Item[]");

        private readonly object propertyChangedLock = new object();
        private readonly object collectionChangedLock = new object();
        private PropertyChangedEventHandler propertyChanged;
        private NotifyCollectionChangedEventHandler collectionChanged;

        private SimpleMonitor monitor = new SimpleMonitor();
        private List<T> items;

        [NonSerialized]
        private Object syncRoot;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { lock (propertyChangedLock) { this.propertyChanged += value; } }
            remove { lock (propertyChangedLock) { this.propertyChanged -= value; } }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { lock (collectionChangedLock) { this.collectionChanged += value; } }
            remove { lock (collectionChangedLock) { this.collectionChanged -= value; } }
        }

        public ObservableList()
        {
            items = new List<T>();
        }

        public ObservableList(List<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            items = new List<T>();
            foreach (T item in list)
            {
                items.Add(item);
            }
        }

        public int Count { get { return items.Count; } }

        protected IList<T> Items { get { return items; } }

        public T this[int index]
        {
            get { return items[index]; }
            set
            {
                if (IsReadOnly)
                    throw new NotSupportedException("ReadOnlyCollection");

                if (index < 0 || index >= items.Count)
                    throw new ArgumentOutOfRangeException(string.Format("ArgumentOutOfRangeException:{0}", index));

                SetItem(index, value);
            }
        }

        public void Add(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            int index = items.Count;
            InsertItem(index, item);
        }

        public void Clear()
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            ClearItems();
        }

        public void CopyTo(T[] array, int index)
        {
            items.CopyTo(array, index);
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (index < 0 || index > items.Count)
                throw new ArgumentOutOfRangeException(string.Format("ArgumentOutOfRangeException:{0}", index));

            InsertItem(index, item);
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            int index = items.IndexOf(item);
            if (index < 0)
                return false;
            RemoveItem(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (index < 0 || index >= items.Count)
                throw new ArgumentOutOfRangeException(string.Format("ArgumentOutOfRangeException:{0}", index));

            RemoveItem(index);
        }

        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            int index = items.Count;
            InsertItem(index, collection);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (index < 0 || index > items.Count)
                throw new ArgumentOutOfRangeException(string.Format("ArgumentOutOfRangeException:{0}", index));

            InsertItem(index, collection);
        }

        public void RemoveRange(int index, int count)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (index < 0 || index >= items.Count)
                throw new ArgumentOutOfRangeException(string.Format("ArgumentOutOfRangeException:{0}", index));

            RemoveItem(index, count);
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }

        bool IsReadOnly { get { return Items.IsReadOnly; } }

        bool ICollection<T>.IsReadOnly
        {
            get { return IsReadOnly; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)items).GetEnumerator();
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    ICollection c = items as ICollection;
                    if (c != null)
                    {
                        this.syncRoot = c.SyncRoot;
                    }
                    else
                    {
#if UNITY_WEBGL
                        this.syncRoot = new object();
#else
                        Interlocked.CompareExchange(ref this.syncRoot, new object(), null);
#endif
                    }
                }
                return this.syncRoot;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (array.Rank != 1)
                throw new ArgumentException("RankMultiDimNotSupported");

            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("NonZeroLowerBound");

            if (index < 0)
                throw new ArgumentOutOfRangeException(string.Format("ArgumentOutOfRangeException:{0}", index));

            if (array.Length - index < Count)
                throw new ArgumentException("ArrayPlusOffTooSmall");

            T[] tArray = array as T[];
            if (tArray != null)
            {
                items.CopyTo(tArray, index);
            }
            else
            {
                Type targetType = array.GetType().GetElementType();
                Type sourceType = typeof(T);
                if (!(targetType.IsAssignableFrom(sourceType) || sourceType.IsAssignableFrom(targetType)))
                    throw new ArgumentException("InvalidArrayType");

                object[] objects = array as object[];
                if (objects == null)
                    throw new ArgumentException("InvalidArrayType");

                int count = items.Count;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        objects[index++] = items[i];
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException("InvalidArrayType");
                }
            }
        }

        object IList.this[int index]
        {
            get { return items[index]; }
            set
            {
                if (value == null && !(default(T) == null))
                    throw new ArgumentNullException("value");

                try
                {
                    this[index] = (T)value;
                }
                catch (InvalidCastException e)
                {
                    throw new ArgumentException("", e);
                }
            }
        }

        bool IList.IsReadOnly { get { return IsReadOnly; } }

        bool IList.IsFixedSize
        {
            get
            {
                IList list = items as IList;
                if (list != null)
                {
                    return list.IsFixedSize;
                }
                return IsReadOnly;
            }
        }

        int IList.Add(object value)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (value == null && !(default(T) == null))
                throw new ArgumentNullException("value");

            try
            {
                Add((T)value);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("", e);
            }

            return this.Count - 1;
        }

        bool IList.Contains(object value)
        {
            if (IsCompatibleObject(value))
            {
                return Contains((T)value);
            }
            return false;
        }

        int IList.IndexOf(object value)
        {
            if (IsCompatibleObject(value))
            {
                return IndexOf((T)value);
            }
            return -1;
        }

        void IList.Insert(int index, object value)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (value == null && !(default(T) == null))
                throw new ArgumentNullException("value");

            try
            {
                Insert(index, (T)value);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("", e);
            }

        }

        void IList.Remove(object value)
        {
            if (IsReadOnly)
                throw new NotSupportedException("ReadOnlyCollection");

            if (IsCompatibleObject(value))
            {
                Remove((T)value);
            }
        }

        protected virtual void ClearItems()
        {
            CheckReentrancy();
            items.Clear();
            OnPropertyChanged(CountEventArgs);
            OnPropertyChanged(IndexerEventArgs);
            OnCollectionReset();
        }

        protected virtual void RemoveItem(int index)
        {
            CheckReentrancy();
            T removedItem = this[index];

            items.RemoveAt(index);

            OnPropertyChanged(CountEventArgs);
            OnPropertyChanged(IndexerEventArgs);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, index);
        }

        protected virtual void RemoveItem(int index, int count)
        {
            CheckReentrancy();

            List<T> list = items as List<T>;
            List<T> changedItems = list.GetRange(index, count);
            list.RemoveRange(index, count);

            OnPropertyChanged(CountEventArgs);
            OnPropertyChanged(IndexerEventArgs);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, changedItems, index);
        }

        protected virtual void InsertItem(int index, T item)
        {
            CheckReentrancy();

            items.Insert(index, item);

            OnPropertyChanged(CountEventArgs);
            OnPropertyChanged(IndexerEventArgs);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        protected virtual void InsertItem(int index, IEnumerable<T> collection)
        {
            CheckReentrancy();

            (items as List<T>).InsertRange(index, collection);

            OnPropertyChanged(CountEventArgs);
            OnPropertyChanged(IndexerEventArgs);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, ToList(collection), index);
        }

        protected virtual void SetItem(int index, T item)
        {
            CheckReentrancy();
            T originalItem = this[index];

            items[index] = item;

            OnPropertyChanged(IndexerEventArgs);
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, originalItem, item, index);
        }

        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            CheckReentrancy();

            T removedItem = this[oldIndex];

            items.RemoveAt(oldIndex);
            items.Insert(newIndex, removedItem);

            OnPropertyChanged(IndexerEventArgs);
            OnCollectionChanged(NotifyCollectionChangedAction.Move, removedItem, newIndex, oldIndex);
        }


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.propertyChanged != null)
            {
                this.propertyChanged(this, e);
            }
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.collectionChanged != null)
            {
                using (BlockReentrancy())
                {
                    this.collectionChanged(this, e);
                }
            }
        }

        protected IDisposable BlockReentrancy()
        {
            this.monitor.Enter();
            return this.monitor;
        }

        protected void CheckReentrancy()
        {
            if (this.monitor.Busy)
            {
                if ((this.collectionChanged != null) && (this.collectionChanged.GetInvocationList().Length > 1))
                    throw new InvalidOperationException();
            }
        }

        private IList ToList(IEnumerable<T> collection)
        {
            if (collection is IList)
                return (IList)collection;

            List<T> list = new List<T>();
            list.AddRange(collection);
            return list;
        }

        private static bool IsCompatibleObject(object value)
        {
            return ((value is T) || (value == null && default(T) == null));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            if (this.collectionChanged != null)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IList changedItems, int index)
        {
            if (this.collectionChanged != null)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItems, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            if (this.collectionChanged != null)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        {
            if (this.collectionChanged != null)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset()
        {
            if (this.collectionChanged != null)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        [Serializable()]
        private class SimpleMonitor : IDisposable
        {
            private int _busyCount;
            public bool Busy { get { return _busyCount > 0; } }

            public void Enter()
            {
                ++_busyCount;
            }

            public void Dispose()
            {
                --_busyCount;
            }
        }
    }
}