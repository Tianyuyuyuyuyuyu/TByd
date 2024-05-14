using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInPrefabInstances
{
    public class Example1 : MonoBehaviour
    {
        [HideInPrefabInstances]
        public GameObject HiddenInPrefabInstances;
    }
}