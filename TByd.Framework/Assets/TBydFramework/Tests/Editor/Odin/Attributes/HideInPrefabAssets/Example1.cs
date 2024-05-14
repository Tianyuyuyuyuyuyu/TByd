using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInPrefabAssets
{
    public class Example1 : MonoBehaviour
    {
        [HideInPrefabAssets] //在Asset中且是预制体时隐藏
        public GameObject HiddenInPrefabAssets;
    }
}