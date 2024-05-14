using System.Diagnostics;
using UnityEngine;

namespace TBydFramework.Runtime.Log
{
    public class XDebug
    {
        [Conditional("EnableLog")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        [Conditional("EnableLog")]
        public static void Log(object message, Object context)
        {
            UnityEngine.Debug.Log(message, context);
        }

        [Conditional("EnableLog")]
        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        [Conditional("EnableLog")]
        public static void LogFormat(UnityEngine.Object message, string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(message, format, args);
        }

        [Conditional("EnableLog")]
        public static void LogFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(format, args);
        }

        [Conditional("EnableLog")]
        public static void LogWarningFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }

        [Conditional("EnableLog")]
        public static void LogErrorFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }

        [Conditional("EnableLog")]
        public static void LogWarning(object message, Object context)
        {
            UnityEngine.Debug.LogWarning(message, context);
        }

        [Conditional("EnableLog")]
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }

        [Conditional("EnableLog")]
        public static void LogError(object message, Object context)
        {
            UnityEngine.Debug.LogError(message, context);
        }

        public static void LogException(System.Exception ex)
        {
            UnityEngine.Debug.LogException(ex);
        }

        [Conditional("EnableLog")]
        public static void Break()
        {
            UnityEngine.Debug.Break();
        }

        [Conditional("EnableLog")]
        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.0f, bool depthTest = true)
        {
            UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
        }
    }
}