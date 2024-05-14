using System;

namespace TBydFramework.Runtime.Binding.Parameters
{
    public class ParameterWrapInvoker : IInvoker
    {
        protected readonly IInvoker invoker;
        protected readonly ICommandParameter commandParameter;
        public ParameterWrapInvoker(IInvoker invoker, ICommandParameter commandParameter)
        {
            if (invoker == null)
                throw new ArgumentNullException("invoker");

            if (commandParameter == null)
                throw new ArgumentNullException("commandParameter");

            this.invoker = invoker;
            this.commandParameter = commandParameter;
        }

        public object Invoke(params object[] args)
        {
            return this.invoker.Invoke(commandParameter.GetValue());
        }
    }

    public class ParameterWrapInvoker<T> : IInvoker
    {
        protected readonly IInvoker<T> invoker;
        protected readonly ICommandParameter<T> commandParameter;
        public ParameterWrapInvoker(IInvoker<T> invoker, ICommandParameter<T> commandParameter)
        {
            if (invoker == null)
                throw new ArgumentNullException("invoker");

            if (commandParameter == null)
                throw new ArgumentNullException("commandParameter");

            this.invoker = invoker;
            this.commandParameter = commandParameter;
        }

        public object Invoke(params object[] args)
        {
            return this.invoker.Invoke(commandParameter.GetValue());
        }
    }
}
