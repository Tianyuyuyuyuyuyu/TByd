using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DisableInPrefabs
{
    public class Example1 : MonoBehaviour
    {
        [DisableInPrefabs]//只要是预制体，就隐藏此属性，不管是否在asset还是hierarchy
        public GameObject DisabledInPrefabs;
    }
}