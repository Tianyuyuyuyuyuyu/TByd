using System.IO;
using log4net.Appender;
using UnityEngine;

namespace TBydFramework.Log4Net.Runtime.Appender
{
    public class UnityFileAppender : FileAppender
    {
        public override string File
        {
            set
            {
                string path;
                if (Application.isEditor)
                    path = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                else
                    path = Path.Combine(Application.temporaryCachePath, "Logs");

                base.File = Path.Combine(path, value); 
            }
        }
    }
}
