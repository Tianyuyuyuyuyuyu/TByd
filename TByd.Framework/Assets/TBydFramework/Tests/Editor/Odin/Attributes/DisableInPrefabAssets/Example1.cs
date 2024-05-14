using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DisableInPrefabAssets
{
    public class Example1 : MonoBehaviour
    {
        [DisableInPrefabAssets]//在asset中且为预制体时，这个属性被警用
        public GameObject DisabledInPrefabAssets;
    }
}