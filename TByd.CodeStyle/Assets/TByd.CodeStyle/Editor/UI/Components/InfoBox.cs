using UnityEditor;
using UnityEngine;

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
            Info,
            Warning,
            Error
        }
        
        // 信息类型
        private InfoType m_Type;
        
        // 信息内容
        private string m_Message;
        
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Message
        {
            get => m_Message;
            set => m_Message = value;
        }
        
        /// <summary>
        /// 信息类型
        /// </summary>
        public InfoType Type
        {
            get => m_Type;
            set => m_Type = value;
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_title">标题</param>
        /// <param name="_message">信息内容</param>
        /// <param name="_type">信息类型</param>
        /// <param name="_isCollapsible">是否可折叠</param>
        public InfoBox(string _title, string _message, InfoType _type = InfoType.Info, bool _isCollapsible = false)
            : base(_title, "", _isCollapsible)
        {
            m_Message = _message;
            m_Type = _type;
        }
        
        /// <summary>
        /// 绘制内容
        /// </summary>
        protected override void DrawContent()
        {
            MessageType messageType = GetMessageType();
            EditorGUILayout.HelpBox(m_Message, messageType);
        }
        
        /// <summary>
        /// 获取对应的Unity消息类型
        /// </summary>
        /// <returns>Unity消息类型</returns>
        private MessageType GetMessageType()
        {
            switch (m_Type)
            {
                case InfoType.Info:
                    return MessageType.Info;
                case InfoType.Warning:
                    return MessageType.Warning;
                case InfoType.Error:
                    return MessageType.Error;
                default:
                    return MessageType.None;
            }
        }
        
        /// <summary>
        /// 设置信息内容
        /// </summary>
        /// <param name="_message">信息内容</param>
        public void SetMessage(string _message)
        {
            m_Message = _message;
        }
        
        /// <summary>
        /// 设置信息类型
        /// </summary>
        /// <param name="_type">信息类型</param>
        public void SetType(InfoType _type)
        {
            m_Type = _type;
        }
    }
} 