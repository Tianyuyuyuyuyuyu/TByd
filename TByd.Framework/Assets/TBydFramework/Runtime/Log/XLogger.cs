using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace TBydFramework.Runtime.Log
{
    public static class XLogger
    {
        public static void Init(string uploadUrl)
        {
            _sUploadUrl = uploadUrl;
        
            // 日期
            var t = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            _sLOGFileSavePath = $"{Application.persistentDataPath}/output_{t}.log";
            Application.logMessageReceived += OnLogCallBack;
        }

        #region 公开方法

        public static void Log(object message, Object context = null)
        {
            XDebug.Log(message, context);
        }

        public static void Log(params object[] messags)
        {
            string message = string.Empty;
            foreach (var it in messags)
            {
                message += it.ToString();
            }

            XDebug.Log(message, null);
        }

        public static void LogFormat(string format, params object[] args)
        {
            XDebug.LogFormat(format, args);
        }

        public static void LogWithColor(object message, string color, Object context = null)
        {
            XDebug.Log(FmtColor(color, message), context);
        }

        public static void LogRed(object message, Object context = null)
        {
            XDebug.Log(FmtColor("red", message), context);
        }

        public static void LogGreen(object message, Object context = null)
        {
            XDebug.Log(FmtColor("green", message), context);
        }

        public static void LogYellow(object message, Object context = null)
        {
            XDebug.Log(FmtColor("yellow", message), context);
        }

        public static void LogCyan(object message, Object context = null)
        {
            XDebug.Log(FmtColor("#00ffff", message), context);
        }

        public static void LogFormatWithColor(string format, string color, params object[] args)
        {
            XDebug.LogFormat((string)FmtColor(color, format), args);
        }

        public static void LogWarning(object message, Object context = null)
        {
            XDebug.LogWarning(message, context);
        }

        public static void LogError(object message, Object context = null)
        {
            XDebug.LogError(message, context);
        }

        #endregion

        #region 收集日志

        // 使用StringBuilder来优化字符串的重复构造
        private static readonly StringBuilder SLogSbBuilder = new StringBuilder();
        
        private static void OnLogCallBack(string condition, string stackTrace, LogType type)
        {
            SLogSbBuilder.Append(condition);
            SLogSbBuilder.Append("\n");
            SLogSbBuilder.Append(stackTrace);
            SLogSbBuilder.Append("\n");

            if (SLogSbBuilder.Length <= 0) return;
            if (!File.Exists(_sLOGFileSavePath))
            {
                var fs = File.Create(_sLOGFileSavePath);
                fs.Close();
            }
            using (var sw = File.AppendText(_sLOGFileSavePath))
            {
                sw.WriteLine(SLogSbBuilder.ToString());
            }
            SLogSbBuilder.Remove(0, SLogSbBuilder.Length);
        }

        #endregion
    
        #region 上传到日志服务器
        
        // 日志文件存储位置
        private static string _sLOGFileSavePath;
        
        //上传日志url
        private static string _sUploadUrl;
        
    
        public static void UploadLog(string desc)
        {
            UpLoadLogMethod(desc).Forget();
        }

        private static async UniTask UpLoadLogMethod(string desc)
        {
            var fileName = Path.GetFileName(_sLOGFileSavePath);
            var data = ReadLogFile(_sLOGFileSavePath);
            var form = new WWWForm();
            // 塞入描述字段，字段名与服务端约定好
            form.AddField("desc", desc);
            // 塞入日志字节流字段，字段名与服务端约定好
            form.AddBinaryData("logfile", data, fileName, "application/x-gzip");
            // 使用UnityWebRequest
            var request = UnityWebRequest.Post(_sUploadUrl, form);
            var result = request.SendWebRequest();

            while (!result.isDone)
            {
                await UniTask.NextFrame();
                Log ("上传进度: " + request.uploadProgress);
            }
            if (!string.IsNullOrEmpty(request.error))
            {
                LogError(request.error);
            }
            else
            {
                Log("日志上传完毕, 服务器返回信息: " + request.downloadHandler.text);
            }
            request.Dispose();
        }

        private static byte[] ReadLogFile(string logFilePath)
        {
            byte[] data;

            using (var fs = File.OpenRead(logFilePath))
            {
                var index = 0;
                var len = fs.Length;
                data = new byte[len];
                // 根据需求进行限流读取
                var offset = data.Length > 1024 ? 1024 : data.Length;
                while (index < len)
                {
                    var readByteCnt = fs.Read(data, index, offset);
                    index += readByteCnt;
                    var leftByteCnt = len - index;
                    offset = leftByteCnt > offset ? offset : (int)leftByteCnt;
                }
            }
            return data;
        }

        #endregion
        
        #region Tool

        private static object FmtColor(string color, object obj)
        {
            if (obj is string)
            {
#if !UNITY_EDITOR
            return obj;
#else
                return FmtColor(color, (string)obj);
#endif
            }
            else
            {
#if !UNITY_EDITOR
            return obj;
#else
                return $"<color={color}>{obj}</color>";
#endif
            }
        }

        private static object FmtColor(string color, string msg)
        {
#if !UNITY_EDITOR
        return msg;
#else
            int p = msg.IndexOf('\n');
            if (p >= 0) p = msg.IndexOf('\n', p + 1);// 可以同时显示两行
            if (p < 0 || p >= msg.Length - 1) return $"<color={color}>{msg}</color>";
            if (p > 2 && msg[p - 1] == '\r') p--;
            return $"<color={color}>{msg.Substring(0, p)}</color>{msg.Substring(p)}";
#endif
        }

        #endregion
    }
}
