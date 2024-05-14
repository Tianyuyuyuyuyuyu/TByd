using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.EnableIf
{
    public class Example1 : MonoBehaviour
    {
        public UnityEngine.Object SomeObject;

        [EnumToggleButtons]
        public InfoMessageType SomeEnum;

        public bool IsToggled;

        [EnableIf("SomeEnum", InfoMessageType.Info)]
        public Vector2 Info;

        [EnableIf("SomeEnum", InfoMessageType.Error)]
        public Vector2 Error;

        [EnableIf("SomeEnum", InfoMessageType.Warning)]
        public Vector2 Warning;

        [EnableIf("IsToggled")]
        public int EnableIfToggled;

        [EnableIf("SomeObject")]
        public Vector3 EnabledWhenHasReference;

        [EnableIf("@this.IsToggled && this.SomeObject != null || this.SomeEnum == InfoMessageType.Error")]
        public int EnableWithExpression;
    }
}