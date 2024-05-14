using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.EnableGUI
{
    public class Example1 : MonoBehaviour
    {
        [ShowInInspector]
        public int GUIDisabledProperty { get { return 20; } }

        [ShowInInspector, EnableGUI]
        public int GUIEnabledProperty { get { return 10; } }
    }
}