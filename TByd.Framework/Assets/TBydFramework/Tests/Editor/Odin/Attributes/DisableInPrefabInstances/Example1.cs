using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DisableInPrefabInstances
{
    public class Example1 : MonoBehaviour
    {
        [DisableInPrefabInstances]//在hierarchy中为预制体时则禁用此属性
        public GameObject DisabledInPrefabInstances;
    }
}