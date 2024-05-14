using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DisableIf
{
    public class Example1 : MonoBehaviour
    {
        public UnityEngine.Object SomeObject;

        [EnumToggleButtons]
        public InfoMessageType SomeEnum;

        public bool IsToggled;

        //指定的属性的值是否与给定的值一致，如果结果为true，则灰态对应的属性
        [DisableIf("SomeEnum", InfoMessageType.Info)]
        public Vector2 Info;

        [DisableIf("SomeEnum", InfoMessageType.Error)]
        public Vector2 Error;

        [DisableIf("SomeEnum", InfoMessageType.Warning)]
        public Vector2 Warning;

        //默认判断bool或者是否为null 为null则是false
        [DisableIf("IsToggled")]
        public int DisableIfToggled;

        [DisableIf("SomeObject")]
        public Vector3 EnabledWhenNull;

        [DisableIf("@this.IsToggled && this.SomeObject != null || this.SomeEnum == InfoMessageType.Error")]
        public int DisableWithExpression;
    }
}