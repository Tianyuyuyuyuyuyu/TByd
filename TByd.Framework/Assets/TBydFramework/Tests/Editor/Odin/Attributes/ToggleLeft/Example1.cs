using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.ToggleLeft
{
    public class Example1 : MonoBehaviour
    {
        [ToggleLeft]
        public bool LeftToggled;

        public bool normalBool;
    }
}