using TBydFramework.FairyGUI.Runtime.Event;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Binding.Proxy.Targets.Universal;
using TBydFramework.Runtime.Binding.Reflection;

namespace TBydFramework.FairyGUI.Runtime.Binding.Proxy
{
    public class FairyPropertyProxy : PropertyTargetProxy
    {

        private EventListener listener;
        public FairyPropertyProxy(object target, IProxyPropertyInfo propertyInfo, EventListener listener) : base(target, propertyInfo)
        {
            this.listener = listener;
        }

        public override BindingMode DefaultMode { get { return BindingMode.TwoWay; } }

        protected override void DoSubscribeForValueChange(object target)
        {
            if (this.listener == null || target == null)
                return;

            listener.Add(RaiseValueChanged);
        }

        protected override void DoUnsubscribeForValueChange(object target)
        {
            if (listener != null)
                listener.Remove(RaiseValueChanged);
        }
    }
}