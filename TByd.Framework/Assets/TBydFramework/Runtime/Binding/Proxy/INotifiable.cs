using System;

namespace TBydFramework.Runtime.Binding.Proxy
{

    public interface INotifiable
    {
        event EventHandler ValueChanged;
    }
}
