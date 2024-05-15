using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.Messaging;
using TBydFramework.Runtime.ViewModels;

namespace TBydFramework.ILRuntime.ILScripts.Framework.ILScripts
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IViewModel
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ViewModelBase));

        private IMessenger messenger;
        private readonly object _lock = new object();
        private PropertyChangedEventHandler propertyChanged;

        public ViewModelBase() : this(null)
        {
        }

        public ViewModelBase(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public virtual IMessenger Messenger
        {
            get { return this.messenger; }
            set { this.messenger = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { lock (_lock) { this.propertyChanged += value; } }
            remove { lock (_lock) { this.propertyChanged -= value; } }
        }

        protected void Broadcast<T>(T oldValue, T newValue, string propertyName)
        {
            try
            {
                var messenger = this.Messenger;
                if (messenger != null)
                    messenger.Publish(new PropertyChangedMessage<T>(this, oldValue, newValue, propertyName));
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("Set property '{0}', broadcast messages failure.Exception:{1}", propertyName, e);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="eventArgs">Property changed event.</param>
        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            try
            {
                if (propertyChanged != null)
                    propertyChanged(this, eventArgs);
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("Set property '{0}', raise PropertyChanged failure.Exception:{1}", eventArgs.PropertyName, e);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void RaisePropertyChanged(params PropertyChangedEventArgs[] eventArgs)
        {
            foreach (var args in eventArgs)
            {
                try
                {
                    if (propertyChanged != null)
                        propertyChanged(this, args);
                }
                catch (Exception e)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("Set property '{0}', raise PropertyChanged failure.Exception:{1}", args.PropertyName, e);
                }
            }
        }

        /// <summary>
        ///  Set the specified propertyName, field, newValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///  Set the specified propertyName, field, newValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        protected bool Set<T>(ref T field, T newValue, PropertyChangedEventArgs eventArgs)
        {
            if (object.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(eventArgs);
            return true;
        }


        /// <summary>
        ///  Set the specified propertyName, field, newValue and broadcast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <param name="broadcast"></param>
        /// <returns></returns>
        protected bool Set<T>(ref T field, T newValue, bool broadcast, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, newValue))
                return false;

            var oldValue = field;
            field = newValue;
            RaisePropertyChanged(propertyName);

            if (broadcast)
                Broadcast(oldValue, newValue, propertyName);
            return true;
        }

        /// <summary>
        ///  Set the specified propertyName, field, newValue and broadcast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="eventArgs"></param>
        /// <param name="broadcast"></param>
        /// <returns></returns>
        protected bool Set<T>(ref T field, T newValue, bool broadcast, PropertyChangedEventArgs eventArgs)
        {
            if (object.Equals(field, newValue))
                return false;

            var oldValue = field;
            field = newValue;
            RaisePropertyChanged(eventArgs);

            if (broadcast)
                Broadcast(oldValue, newValue, eventArgs.PropertyName);
            return true;
        }

        #region IDisposable Support
        ~ViewModelBase()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

