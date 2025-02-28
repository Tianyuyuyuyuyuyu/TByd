using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Git.Commit
{
    /// <summary>
    /// 提交消息解析器，用于解析提交消息文本
    /// </summary>
    public static class CommitMessageParser
    {
        // 提交消息头部正则表达式
        // 格式: <type>(<scope>): <subject>
        // 例如: feat(ui): 添加登录界面
        private static readonly Regex s_HeaderRegex = new Regex(
            @"^(?<type>[a-z]+)(?:\((?<scope>[a-z0-9_\-\.]+)\))?:\s*(?<subject>.*)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// 解析提交消息文本
        /// </summary>
        /// <param name="_message">提交消息文本</param>
        /// <returns>解析后的提交消息对象</returns>
        public static CommitMessage Parse(string _message)
        {
            if (string.IsNullOrEmpty(_message))
            {
                return new CommitMessage();
            }

            try
            {
                // 移除注释行
                string cleanMessage = RemoveComments(_message);

                // 分割消息为头部、正文和页脚
                string[] parts = SplitMessage(cleanMessage);

                string header = parts[0];
                string body = parts.Length > 1 ? parts[1] : string.Empty;
                string footer = parts.Length > 2 ? parts[2] : string.Empty;

                // 解析头部
                Match headerMatch = s_HeaderRegex.Match(header);

                if (!headerMatch.Success)
                {
                    // 如果头部格式不正确，返回只包含原始消息的对象
                    return new CommitMessage(
                        string.Empty,
                        string.Empty,
                        header,
                        body,
                        footer,
                        cleanMessage);
                }

                string type = headerMatch.Groups["type"].Value;
                string scope = headerMatch.Groups["scope"].Success ? headerMatch.Groups["scope"].Value : string.Empty;
                string subject = headerMatch.Groups["subject"].Value.Trim();

                // 添加调试日志
                Debug.Log($"[TByd.CodeStyle] 解析提交消息: Type={type}, Scope={scope}, Subject='{subject}', 原始主题='{headerMatch.Groups["subject"].Value}'");

                // 确保空主题被正确处理
                if (string.IsNullOrWhiteSpace(subject))
                {
                    Debug.Log($"[TByd.CodeStyle] 检测到空主题，设置为空字符串");
                    subject = string.Empty;
                }

                return new CommitMessage(type, scope, subject, body, footer, cleanMessage);
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 解析提交消息失败: {e.Message}");
                return new CommitMessage(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, _message);
            }
        }

        /// <summary>
        /// 移除提交消息中的注释行
        /// </summary>
        /// <param name="_message">原始提交消息</param>
        /// <returns>移除注释后的提交消息</returns>
        private static string RemoveComments(string _message)
        {
            if (string.IsNullOrEmpty(_message))
            {
                return string.Empty;
            }

            string[] lines = _message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string line in lines)
            {
                if (!line.TrimStart().StartsWith("#"))
                {
                    sb.AppendLine(line);
                }
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// 分割提交消息为头部、正文和页脚
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <returns>分割后的部分</returns>
        private static string[] SplitMessage(string _message)
        {
            if (string.IsNullOrEmpty(_message))
            {
                return new[] { string.Empty };
            }

            // 按照空行分割消息
            string[] parts = Regex.Split(_message, @"(?:\r?\n){2,}");

            return parts;
        }

        /// <summary>
        /// 检查提交消息是否符合格式要求
        /// </summary>
        /// <param name="_message">提交消息文本</param>
        /// <returns>是否符合格式要求</returns>
        public static bool IsValidFormat(string _message)
        {
            if (string.IsNullOrEmpty(_message))
            {
                return false;
            }

            string cleanMessage = RemoveComments(_message);
            string[] parts = SplitMessage(cleanMessage);

            if (parts.Length == 0 || string.IsNullOrEmpty(parts[0]))
            {
                return false;
            }

            return s_HeaderRegex.IsMatch(parts[0]);
        }

        /// <summary>
        /// 从提交消息文件中读取提交消息
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>提交消息对象</returns>
        public static CommitMessage ParseFromFile(string _filePath)
        {
            try
            {
                if (!System.IO.File.Exists(_filePath))
                {
                    Debug.LogError($"[TByd.CodeStyle] 提交消息文件不存在: {_filePath}");
                    return new CommitMessage();
                }

                string message = System.IO.File.ReadAllText(_filePath);
                return Parse(message);
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 读取提交消息文件失败: {e.Message}");
                return new CommitMessage();
            }
        }
    }
}
