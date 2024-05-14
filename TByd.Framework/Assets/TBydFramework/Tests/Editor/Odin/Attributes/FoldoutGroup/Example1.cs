using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.FoldoutGroup
{
    public class Example1 : MonoBehaviour
    {
        [FoldoutGroup("Group")]
        public int A;

        [FoldoutGroup("Group")]
        public int B;

        [FoldoutGroup("Group")]
        public int C;

        [FoldoutGroup("Collapsed group", expanded: true)]
        public int D;

        [FoldoutGroup("Collapsed group")]
        public int E;

        [FoldoutGroup("$GroupTitle", expanded: true)]
        public int One;

        [FoldoutGroup("$GroupTitle")]
        public int Two;

        public string GroupTitle = "Dynamic group title";
    }
}