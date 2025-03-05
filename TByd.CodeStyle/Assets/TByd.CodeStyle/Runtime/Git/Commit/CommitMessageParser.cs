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
        /// <param name="message">提交消息文本</param>
        /// <returns>解析后的提交消息对象</returns>
        public static CommitMessage Parse(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return new CommitMessage();
            }

            try
            {
                // 移除注释行
                var cleanMessage = RemoveComments(message);

                // 分割消息为头部、正文和页脚
                var parts = SplitMessage(cleanMessage);

                var header = parts[0];
                var body = parts.Length > 1 ? parts[1] : string.Empty;
                var footer = parts.Length > 2 ? parts[2] : string.Empty;

                // 解析头部
                var headerMatch = s_HeaderRegex.Match(header);

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

                var type = headerMatch.Groups["type"].Value;
                var scope = headerMatch.Groups["scope"].Success ? headerMatch.Groups["scope"].Value : string.Empty;
                var subject = headerMatch.Groups["subject"].Value.Trim();

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
                return new CommitMessage(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, message);
            }
        }

        /// <summary>
        /// 移除提交消息中的注释行
        /// </summary>
        /// <param name="message">原始提交消息</param>
        /// <returns>移除注释后的提交消息</returns>
        private static string RemoveComments(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return string.Empty;
            }

            var lines = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new System.Text.StringBuilder();

            foreach (var line in lines)
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
        /// <param name="message">提交消息</param>
        /// <returns>分割后的部分</returns>
        private static string[] SplitMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return new[] { string.Empty };
            }

            // 按照空行分割消息
            var parts = Regex.Split(message, @"(?:\r?\n){2,}");

            return parts;
        }

        /// <summary>
        /// 检查提交消息是否符合格式要求
        /// </summary>
        /// <param name="message">提交消息文本</param>
        /// <returns>是否符合格式要求</returns>
        public static bool IsValidFormat(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return false;
            }

            var cleanMessage = RemoveComments(message);
            var parts = SplitMessage(cleanMessage);

            if (parts.Length == 0 || string.IsNullOrEmpty(parts[0]))
            {
                return false;
            }

            return s_HeaderRegex.IsMatch(parts[0]);
        }

        /// <summary>
        /// 从提交消息文件中读取提交消息
        /// </summary>
        /// <param name="filePath">提交消息文件路径</param>
        /// <returns>提交消息对象</returns>
        public static CommitMessage ParseFromFile(string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    Debug.LogError($"[TByd.CodeStyle] 提交消息文件不存在: {filePath}");
                    return new CommitMessage();
                }

                var message = System.IO.File.ReadAllText(filePath);
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
