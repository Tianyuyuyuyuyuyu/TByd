using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideIf
{
    public class Example1 : MonoBehaviour
    {
        public UnityEngine.Object SomeObject;

        [EnumToggleButtons]
        public InfoMessageType SomeEnum;

        public bool IsToggled;

        [HideIf("SomeEnum", InfoMessageType.Info)]
        public Vector3 Info;

        [HideIf("IsToggled")]
        public Vector3 HiddenWhenToggled;

        [HideIf("SomeObject")]
        public Vector3 ShowWhenNull;

        [HideIf("@this.IsToggled && this.SomeObject != null")]
        public int HideWithExpression;
    }
}