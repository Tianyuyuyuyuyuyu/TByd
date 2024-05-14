using System;
using TBydFramework.Runtime.Binding.Contexts;

namespace TBydFramework.Runtime.Binding
{
    public interface IBinding : IDisposable
    {
        IBindingContext BindingContext { get; set; }

        object Target { get; }

        object DataContext { get; set; }
    }
}
