using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("m_Type")] [SerializeField]
        private string mType;

        /// <summary>
        /// 作用域，如ui、core等
        /// </summary>
        [FormerlySerializedAs("m_Scope")] [SerializeField]
        private string mScope;

        /// <summary>
        /// 简短描述，提交的主要内容
        /// </summary>
        [FormerlySerializedAs("m_Subject")] [SerializeField]
        private string mSubject;

        /// <summary>
        /// 详细描述，提交的详细说明
        /// </summary>
        [FormerlySerializedAs("m_Body")] [SerializeField]
        private string mBody;

        /// <summary>
        /// 页脚，如关闭的问题、破坏性变更说明等
        /// </summary>
        [FormerlySerializedAs("m_Footer")] [SerializeField]
        private string mFooter;

        /// <summary>
        /// 原始提交消息文本
        /// </summary>
        [FormerlySerializedAs("m_RawMessage")] [SerializeField]
        private string mRawMessage;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommitMessage() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">提交类型</param>
        /// <param name="scope">作用域</param>
        /// <param name="subject">简短描述</param>
        /// <param name="body">详细描述</param>
        /// <param name="footer">页脚</param>
        /// <param name="rawMessage">原始提交消息文本</param>
        public CommitMessage(string type, string scope, string subject, string body, string footer, string rawMessage)
        {
            mType = type;
            mScope = scope;
            mSubject = subject;
            mBody = body;
            mFooter = footer;
            mRawMessage = rawMessage;
        }

        /// <summary>
        /// 提交类型
        /// </summary>
        public string Type
        {
            get => mType;
            set => mType = value;
        }

        /// <summary>
        /// 作用域
        /// </summary>
        public string Scope
        {
            get => mScope;
            set => mScope = value;
        }

        /// <summary>
        /// 简短描述
        /// </summary>
        public string Subject
        {
            get => mSubject;
            set => mSubject = value;
        }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Body
        {
            get => mBody;
            set => mBody = value;
        }

        /// <summary>
        /// 页脚
        /// </summary>
        public string Footer
        {
            get => mFooter;
            set => mFooter = value;
        }

        /// <summary>
        /// 原始提交消息文本
        /// </summary>
        public string RawMessage
        {
            get => mRawMessage;
            set => mRawMessage = value;
        }

        /// <summary>
        /// 原始提交消息头部
        /// </summary>
        public string RawHeader
        {
            get
            {
                if (string.IsNullOrEmpty(mRawMessage))
                    return string.Empty;

                // 获取第一行作为头部
                var newlineIndex = mRawMessage.IndexOf('\n');
                return newlineIndex > 0 ? mRawMessage.Substring(0, newlineIndex) : mRawMessage;
            }
        }

        /// <summary>
        /// 获取格式化的提交消息
        /// </summary>
        /// <returns>格式化的提交消息</returns>
        public string GetFormattedMessage()
        {
            var header = string.IsNullOrEmpty(mScope) ?
                $"{mType}: {mSubject}" :
                $"{mType}({mScope}): {mSubject}";

            if (string.IsNullOrEmpty(mBody) && string.IsNullOrEmpty(mFooter))
            {
                return header;
            }

            var message = header;

            if (!string.IsNullOrEmpty(mBody))
            {
                message += $"\n\n{mBody}";
            }

            if (!string.IsNullOrEmpty(mFooter))
            {
                message += $"\n\n{mFooter}";
            }

            return message;
        }

        /// <summary>
        /// 获取提交消息的头部（类型、作用域和简短描述）
        /// </summary>
        /// <returns>提交消息头部</returns>
        public string GetHeader()
        {
            return string.IsNullOrEmpty(mScope) ?
                $"{mType}: {mSubject}" :
                $"{mType}({mScope}): {mSubject}";
        }

        /// <summary>
        /// 检查提交消息是否为空
        /// </summary>
        /// <returns>是否为空</returns>
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(mType) &&
                string.IsNullOrEmpty(mSubject) &&
                string.IsNullOrEmpty(mBody) &&
                string.IsNullOrEmpty(mFooter);
        }
    }
}
