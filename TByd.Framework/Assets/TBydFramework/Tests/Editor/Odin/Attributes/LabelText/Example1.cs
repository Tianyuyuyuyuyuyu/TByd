using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.LabelText
{
    public class Example1 : MonoBehaviour
    {
        [LabelText("1")]
        public int MyInt1;

        [LabelText("2")]
        public int MyInt2;

        [LabelText("3")]
        public int MyInt3;

        [InfoBox("Use $ to refer to a member string.")]
        [LabelText("$LabelText")]
        public string LabelText = "Dynamic label text";

        [InfoBox("Use @ to execute an expression.")]
        [LabelText("@DateTime.Now.ToString(\"HH:mm:ss\")")]
        public string DateTimeLabel;
    }
}