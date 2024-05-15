using TBydFramework.NLog.Runtime;
using UnityEngine;
using TBydFramework.Runtime.Log;

namespace TBydFramework.Tutorials
{
    public class NLogManager : MonoBehaviour
    {
        void Awake()
        {
            ////Load the NLog configuration file from the StreamingAssets directory
            //X.Log.LogManager.Registry(NLogFactory.Load(Application.streamingAssetsPath + "/config.xml"));

            //Load the NLog configuration file from the Resources directory
            LogManager.Registry(NLogFactory.LoadInResources("config"));

            DontDestroyOnLoad(this.gameObject);
        }

        void OnDestroy()
        {
            NLogFactory.Shutdown();
        }
    }
}
