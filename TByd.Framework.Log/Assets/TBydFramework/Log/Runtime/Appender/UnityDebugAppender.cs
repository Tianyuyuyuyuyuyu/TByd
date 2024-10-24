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
            if (Level.Fatal.Equals(level) || Level.Error.Equals(level))
                Debug.LogError(RenderLoggingEvent(loggingEvent));
            else if(Level.Warn.Equals(level))
                Debug.LogWarning(RenderLoggingEvent(loggingEvent));
            else
                Debug.Log(RenderLoggingEvent(loggingEvent));
        }
    }
}