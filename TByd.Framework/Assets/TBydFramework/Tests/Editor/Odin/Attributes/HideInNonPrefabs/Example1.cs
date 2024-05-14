using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInNonPrefabs
{
    public class Example1 : MonoBehaviour
    {
        [HideInNonPrefabs] //非预制体时隐藏属性
        public GameObject HiddenInNonPrefabs;
    }
}