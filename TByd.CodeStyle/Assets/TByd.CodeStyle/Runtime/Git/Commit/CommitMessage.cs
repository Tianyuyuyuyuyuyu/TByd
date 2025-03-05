using System;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Git.Commit
{
    /// <summary>
    /// 提交消息结构，表示一个Git提交消息的各个部分
    /// </summary>
    [Serializable]
    public class CommitMessage
    {
        /// <summary>
        /// 提交类型，如feat、fix等
        /// </summary>
        [SerializeField]
        private string m_Type;

        /// <summary>
        /// 作用域，如ui、core等
        /// </summary>
        [SerializeField]
        private string m_Scope;

        /// <summary>
        /// 简短描述，提交的主要内容
        /// </summary>
        [SerializeField]
        private string m_Subject;

        /// <summary>
        /// 详细描述，提交的详细说明
        /// </summary>
        [SerializeField]
        private string m_Body;

        /// <summary>
        /// 页脚，如关闭的问题、破坏性变更说明等
        /// </summary>
        [SerializeField]
        private string m_Footer;

        /// <summary>
        /// 原始提交消息文本
        /// </summary>
        [SerializeField]
        private string m_RawMessage;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommitMessage() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_type">提交类型</param>
        /// <param name="_scope">作用域</param>
        /// <param name="_subject">简短描述</param>
        /// <param name="_body">详细描述</param>
        /// <param name="_footer">页脚</param>
        /// <param name="_rawMessage">原始提交消息文本</param>
        public CommitMessage(string _type, string _scope, string _subject, string _body, string _footer, string _rawMessage)
        {
            m_Type = _type;
            m_Scope = _scope;
            m_Subject = _subject;
            m_Body = _body;
            m_Footer = _footer;
            m_RawMessage = _rawMessage;
        }

        /// <summary>
        /// 提交类型
        /// </summary>
        public string Type
        {
            get => m_Type;
            set => m_Type = value;
        }

        /// <summary>
        /// 作用域
        /// </summary>
        public string Scope
        {
            get => m_Scope;
            set => m_Scope = value;
        }

        /// <summary>
        /// 简短描述
        /// </summary>
        public string Subject
        {
            get => m_Subject;
            set => m_Subject = value;
        }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Body
        {
            get => m_Body;
            set => m_Body = value;
        }

        /// <summary>
        /// 页脚
        /// </summary>
        public string Footer
        {
            get => m_Footer;
            set => m_Footer = value;
        }

        /// <summary>
        /// 原始提交消息文本
        /// </summary>
        public string RawMessage
        {
            get => m_RawMessage;
            set => m_RawMessage = value;
        }

        /// <summary>
        /// 原始提交消息头部
        /// </summary>
        public string RawHeader
        {
            get
            {
                if (string.IsNullOrEmpty(m_RawMessage))
                    return string.Empty;

                // 获取第一行作为头部
                var newlineIndex = m_RawMessage.IndexOf('\n');
                return newlineIndex > 0 ? m_RawMessage.Substring(0, newlineIndex) : m_RawMessage;
            }
        }

        /// <summary>
        /// 获取格式化的提交消息
        /// </summary>
        /// <returns>格式化的提交消息</returns>
        public string GetFormattedMessage()
        {
            var header = string.IsNullOrEmpty(m_Scope) ?
                $"{m_Type}: {m_Subject}" :
                $"{m_Type}({m_Scope}): {m_Subject}";

            if (string.IsNullOrEmpty(m_Body) && string.IsNullOrEmpty(m_Footer))
            {
                return header;
            }

            var message = header;

            if (!string.IsNullOrEmpty(m_Body))
            {
                message += $"\n\n{m_Body}";
            }

            if (!string.IsNullOrEmpty(m_Footer))
            {
                message += $"\n\n{m_Footer}";
            }

            return message;
        }

        /// <summary>
        /// 获取提交消息的头部（类型、作用域和简短描述）
        /// </summary>
        /// <returns>提交消息头部</returns>
        public string GetHeader()
        {
            return string.IsNullOrEmpty(m_Scope) ?
                $"{m_Type}: {m_Subject}" :
                $"{m_Type}({m_Scope}): {m_Subject}";
        }

        /// <summary>
        /// 检查提交消息是否为空
        /// </summary>
        /// <returns>是否为空</returns>
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(m_Type) &&
                string.IsNullOrEmpty(m_Subject) &&
                string.IsNullOrEmpty(m_Body) &&
                string.IsNullOrEmpty(m_Footer);
        }
    }
}
