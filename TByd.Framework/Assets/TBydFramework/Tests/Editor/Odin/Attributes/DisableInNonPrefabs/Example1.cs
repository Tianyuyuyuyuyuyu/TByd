using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DisableInNonPrefabs
{
    public class Example1 : MonoBehaviour
    {
        [InfoBox("这些属性只有在检查GameObject的组件时才会起作用。")]

        [DisableInNonPrefabs] // 当不是预制体是灰态此属性
        public GameObject DisabledInNonPrefabs;
    }
}