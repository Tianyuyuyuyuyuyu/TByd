using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DisableInPlayMode
{
    public class Example1 : MonoBehaviour
    {
        [Title("运行模式下禁用属性")]
        [DisableInPlayMode]
        public int A;

        [DisableInPlayMode]
        public Material B;
    }
}