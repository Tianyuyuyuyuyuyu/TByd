using UnityEngine;
using log4net.Core;
using log4net.Appender;

namespace TBydFramework.Log.Runtime.Appender
{
    public class UnityDebugAppender: AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var level = loggingEvent.Level;
            if (log4net.Core.Level.Fatal.Equals(level) || log4net.Core.Level.Error.Equals(level))
                Debug.LogError(RenderLoggingEvent(loggingEvent));
            else if(log4net.Core.Level.Warn.Equals(level))
                Debug.LogWarning(RenderLoggingEvent(loggingEvent));
            else
                Debug.Log(RenderLoggingEvent(loggingEvent));
        }
    }
}