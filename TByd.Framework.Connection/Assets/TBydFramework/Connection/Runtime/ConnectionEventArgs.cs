using System;

namespace TBydFramework.Connection.Runtime
{
    public class ConnectionEventArgs : EventArgs
    {
        public static readonly ConnectionEventArgs ConnectingEventArgs = new ConnectionEventArgs("Connecting");
        public static readonly ConnectionEventArgs ReconnectingEventArgs = new ConnectionEventArgs("Reconnecting");
        public static readonly ConnectionEventArgs ConnectedEventArgs = new ConnectionEventArgs("Connected");
        public static readonly ConnectionEventArgs FailedEventArgs = new ConnectionEventArgs("Failed");
        public static readonly ConnectionEventArgs ExceptionEventArgs = new ConnectionEventArgs("Exception");
        public static readonly ConnectionEventArgs ClosingEventArgs = new ConnectionEventArgs("Closing");
        public static readonly ConnectionEventArgs ClosedEventArgs = new ConnectionEventArgs("Closed");

        public ConnectionEventArgs(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return string.Format("ConnectionEvent:[{0}]", this.Name);
        }
    }
}
