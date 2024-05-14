using System;
using System.Globalization;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.OnInspectorInit
{
    public class Example2 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        // Display current time for reference.
        [ShowInInspector, DisplayAsString, PropertyOrder(-1)]
        public string CurrentTime
        {
            get
            {
                GUIHelper.RequestRepaint();
                return DateTime.Now.ToString(CultureInfo.InvariantCulture);
            }
        }

        //当这个字符串第一次被绘制到检查器中时，OnInspectorInit执行。
        //当实例被重新编译时，会再次执行一次。
        [OnInspectorInit("@TimeWhenExampleWasOpened = DateTime.Now.ToString(CultureInfo.InvariantCulture)")]
        public string TimeWhenExampleWasOpened;

        // OnInspectorInit在属性在检查器中被实际解析之前不会执行。
        //Odin的属性系统是延迟计算的，所以直到折叠展开才会进行OnInspectorInit初始化。
        [FoldoutGroup("Delayed Initialization", Expanded = false, HideWhenChildrenAreInvisible = false)]
        [OnInspectorInit("@TimeFoldoutWasOpened = DateTime.Now.ToString(CultureInfo.InvariantCulture)")]
        public string TimeFoldoutWasOpened;
    }
}