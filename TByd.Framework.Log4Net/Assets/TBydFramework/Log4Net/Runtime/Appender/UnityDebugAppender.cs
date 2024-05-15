using log4net.Appender;
using log4net.Core;
using UnityEngine;

namespace TBydFramework.Log4Net.Runtime.Appender
{
    public class UnityDebugAppender: AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            Level level = loggingEvent.Level;
            if (Level.Fatal.Equals(level) || Level.Error.Equals(level))
                Debug.LogError(RenderLoggingEvent(loggingEvent));
            else if(Level.Warn.Equals(level))
                Debug.LogWarning(RenderLoggingEvent(loggingEvent));
            else
                Debug.Log(RenderLoggingEvent(loggingEvent));
        }
    }
}