using System;
using TBydFramework.Runtime.Observables;
using UnityEngine;

namespace TBydFramework.Runtime.Localizations.UI
{
    [DefaultExecutionOrder(100)]
    public abstract class AbstractLocalized<T> : MonoBehaviour where T : Component
    {
        [SerializeField]
        private string key;
        protected T target;
        protected IObservableProperty value;

        protected virtual void OnKeyChanged()
        {
            if (this.value != null)
                this.value.ValueChanged -= OnValueChanged;

            if (!this.enabled || this.target == null || string.IsNullOrEmpty(key))
                return;

            Localization localization = Localization.Current;
            this.value = localization.GetValue(key);
            this.value.ValueChanged += OnValueChanged;
            this.OnValueChanged(this.value, EventArgs.Empty);
        }

        public string Key
        {
            get { return this.key; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Equals(this.key))
                    return;

                this.key = value;
                this.OnKeyChanged();
            }
        }

        protected virtual void OnEnable()
        {
            if (this.target == null)
                this.target = this.GetComponent<T>();

            if (this.target == null)
                return;

            this.OnKeyChanged();
        }

        protected virtual void OnDisable()
        {
            if (this.value != null)
            {
                this.value.ValueChanged -= OnValueChanged;
                this.value = null;
            }
        }

        protected abstract void OnValueChanged(object sender, EventArgs e);
    }
}
