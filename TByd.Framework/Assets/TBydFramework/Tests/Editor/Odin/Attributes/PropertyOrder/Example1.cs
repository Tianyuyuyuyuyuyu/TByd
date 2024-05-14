using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.PropertyOrder
{
    public class Example1 : MonoBehaviour
    {
        [PropertyOrder(1)]
        public int Second;

        [InfoBox("PropertyOrder用于更改inspector中属性的顺序")]
        [PropertyOrder(-1)]
        public int First;
    }
}