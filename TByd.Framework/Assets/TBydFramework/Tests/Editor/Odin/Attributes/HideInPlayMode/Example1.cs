using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInPlayMode
{
    public class Example1 : MonoBehaviour
    {
        [Title("Hidden in play mode")]
        [HideInPlayMode]
        public int A;

        [HideInPlayMode]
        public int B;
    }
}