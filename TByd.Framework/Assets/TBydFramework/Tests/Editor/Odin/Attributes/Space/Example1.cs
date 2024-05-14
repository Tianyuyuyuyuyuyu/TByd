using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.Space
{
    public class Example1 : MonoBehaviour
    {

        [Space]
        public int Space;

        // 但是正如其名称所示，PropertySpace也可以应用于属性。.
        [ShowInInspector, PropertySpace]
        public string Property { get; set; }

        // 您还可以控制PropertySpace属性前后的间距。
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 60), PropertyOrder(2)]
        public int BeforeAndAfter;
    }
}