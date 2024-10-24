using UnityEngine;
using System.IO;
using TBydFramework.Log.Runtime;
using LogManager = TBydFramework.Log.Runtime.LogManager;

namespace TBydFramework.Framework.Tutorials
{
    public class Log4NetManager : MonoBehaviour
    {
        void Awake()
        {
            InitializeLog();
            DontDestroyOnLoad(gameObject);
        }

        private void InitializeLog()
        {
            /* Initialize the log4net */
            string configFilename = "Log4NetConfig";
            TextAsset configText = Resources.Load<TextAsset>(configFilename);
            if (configText != null)
            {
                using (MemoryStream memStream = new MemoryStream(configText.bytes))
                {
                    log4net.Config.XmlConfigurator.Configure(memStream);
                }
            }

            /* Initialize the Loxodon.Log.LogManager */
            LogManager.Registry(new Log4NetFactory());
        }

        void OnDestroy()
        {
            log4net.LogManager.Shutdown();
        }
    }
}
