using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace TBydFramework.ILRuntime.Runtime.ILRuntime.Adapters
{
    public interface IBehaviourAdapter: CrossBindingAdaptorType
    {
        new ILTypeInstance ILInstance { get; set; }

        AppDomain AppDomain { get; set; }

        void Awake();

    }
}
