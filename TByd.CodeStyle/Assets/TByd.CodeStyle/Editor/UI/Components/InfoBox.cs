using UnityEditor;

namespace TByd.CodeStyle.Editor.UI.Components
{
    /// <summary>
    /// 信息框组件，用于显示信息、警告或错误消息
    /// </summary>
    public class InfoBox : UIComponent
    {
        /// <summary>
        /// 信息类型
        /// </summary>
        public enum InfoType
        {
            k_Info,
            k_Warning,
            k_Error
        }

        // 信息内容

        // 信息类型

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">信息内容</param>
        /// <param name="type">信息类型</param>
        /// <param name="isCollapsible">是否可折叠</param>
        public InfoBox(string title, string message, InfoType type = InfoType.k_Info, bool isCollapsible = false)
            : base(title, "", isCollapsible)
        {
            Message = message;
            Type = type;
        }

        /// <summary>
        /// 信息内容
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// 信息类型
        /// </summary>
        public InfoType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 绘制内容
        /// </summary>
        protected override void DrawContent()
        {
            var messageType = GetMessageType();
            EditorGUILayout.HelpBox(Message, messageType);
        }

        /// <summary>
        /// 获取对应的Unity消息类型
        /// </summary>
        /// <returns>Unity消息类型</returns>
        private MessageType GetMessageType()
        {
            return Type switch
            {
                InfoType.k_Info => MessageType.Info,
                InfoType.k_Warning => MessageType.Warning,
                InfoType.k_Error => MessageType.Error,
                _ => MessageType.None
            };
        }
    }
}
