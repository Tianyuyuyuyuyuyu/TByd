using System;
using System.Collections.Generic;
using System.Globalization;
using TBydFramework.Runtime.Observables;

namespace TBydFramework.Runtime.Localizations.Data
{
    public class LocalizedObject<T> : Dictionary<string, T>, IObservableProperty<T>, IDisposable
    {
        private readonly object _lock = new object();
        private EventHandler valueChanged;
        private Localization localization;

        public LocalizedObject() : this(null, Localization.Current)
        {
        }

        public LocalizedObject(IDictionary<string, T> source) : this(source, Localization.Current)
        {
        }

        public LocalizedObject(IDictionary<string, T> source, Localization localization) : base()
        {
            if (source != null)
            {
                foreach (var kv in source)
                {
                    this.Add(kv.Key, kv.Value);
                }
            }
            this.localization = localization;
            if (localization != null)
                localization.CultureInfoChanged += OnCultureInfoChanged;
        }

        public event EventHandler ValueChanged
        {
            add { lock (_lock) { this.valueChanged += value; } }
            remove { lock (_lock) { this.valueChanged -= value; } }
        }

        public virtual Type Type { get { return typeof(T); } }

        protected void RaiseValueChanged()
        {
            this.valueChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual T Value
        {
            get { return this.GetValue(localization != null ? localization.CultureInfo : CultureInfo.CurrentUICulture); }
            set { throw new NotSupportedException(); }
        }

        object IObservableProperty.Value
        {
            get { return this.GetValue(localization != null ? localization.CultureInfo : CultureInfo.CurrentUICulture); }
            set { throw new NotSupportedException(); }
        }

        private void OnCultureInfoChanged(object sender, EventArgs e)
        {
            try
            {
                this.RaiseValueChanged();
            }
            catch (Exception) { }
        }

        protected virtual T GetValue(CultureInfo cultureInfo)
        {
            T value = default(T);
            if (this.TryGetValue(cultureInfo.Name, out value))
                return value;

            if (this.TryGetValue(cultureInfo.TwoLetterISOLanguageName, out value))
                return value;

            var ie = this.Values.GetEnumerator();
            if (ie.MoveNext())
                return ie.Current;

            return value;
        }

        public static implicit operator T(LocalizedObject<T> localized)
        {
            return localized.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LocalizedObject<T>))
                return false;

            if (this == obj)
                return true;

            LocalizedObject<T> localized = (LocalizedObject<T>)obj;
            if (this.Equals(localized))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            var value = this.Value;
            if (value == null)
                return 0;

            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            var value = this.Value;
            if (value == null)
                return string.Empty;

            return value.ToString();
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (localization != null)
                    localization.CultureInfoChanged -= OnCultureInfoChanged;

                disposed = true;
            }
        }

        ~LocalizedObject()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
