using System;
using TBydFramework.Runtime.Binding.Proxy;

namespace TBydFramework.Runtime.Binding.Parameters
{
    public class ParameterWrapScriptInvoker : ParameterWrapBase, IInvoker
    {
        private readonly IScriptInvoker invoker;

        public ParameterWrapScriptInvoker(IScriptInvoker invoker, ICommandParameter commandParameter) : base(commandParameter)
        {
            if (invoker == null)
                throw new ArgumentNullException("invoker");

            this.invoker = invoker;
        }

        public object Invoke(params object[] args)
        {
            return this.invoker.Invoke(GetParameterValue());
        }
    }
}
