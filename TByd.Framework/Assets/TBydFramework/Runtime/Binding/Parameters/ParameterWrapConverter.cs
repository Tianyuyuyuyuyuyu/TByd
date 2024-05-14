using System;
using TBydFramework.Runtime.Binding.Converters;
using TBydFramework.Runtime.Binding.Proxy;
using TBydFramework.Runtime.Binding.Reflection;
using TBydFramework.Runtime.Commands;

namespace TBydFramework.Runtime.Binding.Parameters
{
    public class ParameterWrapConverter : AbstractConverter
    {
        private readonly ICommandParameter commandParameter;
        public ParameterWrapConverter(ICommandParameter commandParameter)
        {
            if (commandParameter == null)
                throw new ArgumentNullException("commandParameter");

            this.commandParameter = commandParameter;
        }

        public override object Convert(object value)
        {
            if (value == null)
                return null;

            if (value is Delegate)
                return new ParameterWrapDelegateInvoker(value as Delegate, commandParameter);

            if (value is ICommand)
                return new ParameterWrapCommand(value as ICommand, commandParameter);

            if (value is IScriptInvoker)
                return new ParameterWrapScriptInvoker(value as IScriptInvoker, commandParameter);

            if (value is IProxyInvoker)
                return new ParameterWrapProxyInvoker(value as IProxyInvoker, commandParameter);

            if (value is IInvoker)
                return new ParameterWrapInvoker(value as IInvoker, commandParameter);

            throw new NotSupportedException(string.Format("Unsupported type \"{0}\".", value.GetType()));
        }

        public override object ConvertBack(object value)
        {
            throw new NotSupportedException();
        }
    }

    public class ParameterWrapConverter<T> : AbstractConverter
    {
        private readonly ICommandParameter<T> commandParameter;
        public ParameterWrapConverter(ICommandParameter<T> commandParameter)
        {
            if (commandParameter == null)
                throw new ArgumentNullException("commandParameter");

            this.commandParameter = commandParameter;
        }

        public override object Convert(object value)
        {
            if (value == null)
                return null;

            if (value is IInvoker<T> invoker)
                return new ParameterWrapInvoker<T>(invoker, commandParameter);

            if (value is ICommand<T> command)
                return new ParameterWrapCommand<T>(command, commandParameter);

            if (value is Action<T> action)
                return new ParameterWrapActionInvoker<T>(action, commandParameter);

            if (value is Delegate)
                return new ParameterWrapDelegateInvoker(value as Delegate, commandParameter);

            if (value is ICommand)
                return new ParameterWrapCommand(value as ICommand, commandParameter);

            if (value is IScriptInvoker)
                return new ParameterWrapScriptInvoker(value as IScriptInvoker, commandParameter);

            if (value is IProxyInvoker)
                return new ParameterWrapProxyInvoker(value as IProxyInvoker, commandParameter);

            if (value is IInvoker)
                return new ParameterWrapInvoker(value as IInvoker, commandParameter);

            throw new NotSupportedException(string.Format("Unsupported type \"{0}\".", value.GetType()));
        }

        public override object ConvertBack(object value)
        {
            throw new NotSupportedException();
        }
    }
}
