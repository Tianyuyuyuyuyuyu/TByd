using System;
using TBydFramework.Runtime.Commands;

namespace TBydFramework.Runtime.Binding.Parameters
{
    public class ParameterWrapCommand : ParameterWrapBase, ICommand
    {
        private readonly object _lock = new object();
        private readonly ICommand wrappedCommand;
        public ParameterWrapCommand(ICommand wrappedCommand, ICommandParameter commandParameter) : base(commandParameter)
        {
            if (wrappedCommand == null)
                throw new ArgumentNullException("wrappedCommand");

            this.wrappedCommand = wrappedCommand;
        }

        public event EventHandler CanExecuteChanged
        {
            add { lock (_lock) { this.wrappedCommand.CanExecuteChanged += value; } }
            remove { lock (_lock) { this.wrappedCommand.CanExecuteChanged -= value; } }
        }

        public bool CanExecute(object parameter)
        {
            return wrappedCommand.CanExecute(GetParameterValue());
        }

        public void Execute(object parameter)
        {
            var param = GetParameterValue();
            if (wrappedCommand.CanExecute(param))
                wrappedCommand.Execute(param);
        }
    }

    public class ParameterWrapCommand<T> : ICommand
    {
        private readonly object _lock = new object();
        private readonly ICommand<T> wrappedCommand;
        private readonly ICommandParameter<T> commandParameter;
        public ParameterWrapCommand(ICommand<T> wrappedCommand, ICommandParameter<T> commandParameter)
        {
            if (wrappedCommand == null)
                throw new ArgumentNullException("wrappedCommand");
            if (commandParameter == null)
                throw new ArgumentNullException("commandParameter");

            this.commandParameter = commandParameter;
            this.wrappedCommand = wrappedCommand;
        }

        public event EventHandler CanExecuteChanged
        {
            add { lock (_lock) { this.wrappedCommand.CanExecuteChanged += value; } }
            remove { lock (_lock) { this.wrappedCommand.CanExecuteChanged -= value; } }
        }

        public bool CanExecute(object parameter)
        {
            return wrappedCommand.CanExecute(commandParameter.GetValue());
        }

        public void Execute(object parameter)
        {
            var param = commandParameter.GetValue();
            if (wrappedCommand.CanExecute(param))
                wrappedCommand.Execute(param);
        }
    }
}
