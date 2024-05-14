using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideMonoScript
{
    [CreateAssetMenu(fileName = "HideMonoScript_ScriptableObject", menuName = "CreatScriptableObject/HideMonoScript")]
    [HideMonoScript]
    public class HideMonoScript : ScriptableObject
    {
        public string Value;
    }

    [CreateAssetMenu(fileName = "ShowMonoScript_ScriptableObject", menuName = "CreatScriptableObject/ShowMonoScript")]
    public class ShowMonoScript : ScriptableObject
    {
        public string Value;
    }
}