using TBydFramework.Runtime.Binding.Contexts;

namespace TBydFramework.Runtime.Binding
{
    public interface IBindingFactory
    {
        IBinding Create(IBindingContext bindingContext, object source, object target, BindingDescription bindingDescription);
    }
}
