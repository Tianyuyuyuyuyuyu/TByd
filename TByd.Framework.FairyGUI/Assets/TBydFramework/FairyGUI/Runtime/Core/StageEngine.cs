using TBydFramework.FairyGUI.Runtime.UI;
using UnityEngine;

namespace TBydFramework.FairyGUI.Runtime.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class StageEngine : MonoBehaviour
    {
        public int ObjectsOnStage;
        public int GraphicsOnStage;

        public static bool beingQuit;

        void Start()
        {
            useGUILayout = false;
        }

        void LateUpdate()
        {
            Stage.inst.InternalUpdate();

            ObjectsOnStage = Stats.ObjectCount;
            GraphicsOnStage = Stats.GraphicsCount;
        }

        void OnGUI()
        {
            Stage.inst.HandleGUIEvents(UnityEngine.Event.current);
        }

#if !UNITY_5_4_OR_NEWER
        void OnLevelWasLoaded()
        {
            StageCamera.CheckMainCamera();
        }
#endif

        void OnApplicationQuit()
        {
            if (Application.isEditor)
            {
                beingQuit = true;
                UIPackage.RemoveAllPackages();
            }
        }
    }
}