using System;
using TBydFramework.Log.Editor.Log4Net.Views;
using TBydFramework.Log.Runtime.Serialization;

namespace TBydFramework.Log.Editor.Log4Net.LogReceiver
{
    public delegate void MessageHandler(TerminalInfo terminalInfo, LoggingData loggingData);

    public interface ILogReceiver : IDisposable
    {
        event MessageHandler MessageReceived;

        bool Started { get; }

        void Start();

        void Stop();
    }
}