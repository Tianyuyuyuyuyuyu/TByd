using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideLabel
{
    public class Example1 : MonoBehaviour
    {
        public string showLabel = "菜鸟";

        [HideLabel]
        public string hideLabel = "隐藏标题";

        [ShowInInspector]
        public string ShowPropertyLabel { get; set; }

        [HideLabel][ShowInInspector]
        public string HidePropertyLabel { get; set; }
    }
}