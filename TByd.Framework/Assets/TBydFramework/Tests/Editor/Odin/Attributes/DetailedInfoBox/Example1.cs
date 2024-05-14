using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.DetailedInfoBox
{
    public class Example1 : MonoBehaviour
    {
        [DetailedInfoBox("详情请点击...", "地址为：https://github.com/Tianyuyuyuyuyuyu")]
        public string message = "无";

        [DetailedInfoBox("简介消息", "默认情况下VisibleIf为True，所以此消息框可见", InfoMessageType = InfoMessageType.None)]
        public string NoneMessage = "无";
        [DetailedInfoBox("简介消息", "默认情况下VisibleIf为True，所以此消息框可见", InfoMessageType = InfoMessageType.Info)]
        public string InfoMessage = "无";
        [DetailedInfoBox("简介消息", "默认情况下VisibleIf为True，所以此消息框可见", InfoMessageType = InfoMessageType.Warning)]
        public string WarningMessage = "无";
        [DetailedInfoBox("简介消息", "默认情况下VisibleIf为True，所以此消息框可见", InfoMessageType = InfoMessageType.Error)]
        public string ErrorMessage = "无";

        [DetailedInfoBox("简介消息", "默认情况下VisibleIf为True。" +
                                 "所以此消息框可见.还可以通过一个方法的返回值（bool）来控制消息框是否显示，" +
                                 "例如在这个函数中判断此字段是否为null，如果为null在出现弹窗提示等。", 
            InfoMessageType = InfoMessageType.None, VisibleIf = "VisibleFunction")]
        public string haveVisibleIfMessage = "";

        [DetailedInfoBox("简介消息", "还可以通过一个方法的返回值（bool）来控制消息框是否显示，例如在这个函数中判断此字段是否为null，如果为null在出现弹窗提示等。", InfoMessageType = InfoMessageType.None, VisibleIf = "NoVisibleFunction")]
        public string noVisibleIfMessage = "";

        public  bool VisibleFunction()
        {
            /*
             * 根据情况下选择返回true或者false，让对应的消息框显示或者不显示
             */
            return true;
        }

        public bool NoVisibleFunction()
        {
            return string.IsNullOrEmpty(noVisibleIfMessage);
        }
    }
}

