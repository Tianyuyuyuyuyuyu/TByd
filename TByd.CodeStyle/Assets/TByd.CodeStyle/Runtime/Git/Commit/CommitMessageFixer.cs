using System;
using System.Text;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Git.Commit
{
    /// <summary>
    /// 提交消息修复器，用于修复提交消息中的问题
    /// </summary>
    public class CommitMessageFixer
    {
        // Git提交配置
        private readonly GitCommitConfig m_Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_config">Git提交配置</param>
        public CommitMessageFixer(GitCommitConfig _config)
        {
            m_Config = _config;
        }

        /// <summary>
        /// 修复提交消息
        /// </summary>
        /// <param name="_message">提交消息</param>
        /// <returns>修复后的提交消息</returns>
        public CommitMessage Fix(CommitMessage _message)
        {
            if (_message == null)
            {
                return new CommitMessage();
            }

            try
            {
                // 创建新的提交消息对象
                var fixedMessage = new CommitMessage(
                    _message.Type,
                    _message.Scope,
                    _message.Subject,
                    _message.Body,
                    _message.Footer,
                    _message.RawMessage);

                // 修复类型
                fixedMessage.Type = FixType(fixedMessage.Type);

                // 修复作用域
                fixedMessage.Scope = FixScope(fixedMessage.Scope);

                // 修复简短描述
                fixedMessage.Subject = FixSubject(fixedMessage.Subject);

                // 更新原始消息
                fixedMessage.RawMessage = fixedMessage.GetFormattedMessage();

                return fixedMessage;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 修复提交消息失败: {e.Message}");
                return _message;
            }
        }

        /// <summary>
        /// 修复提交类型
        /// </summary>
        /// <param name="_type">提交类型</param>
        /// <returns>修复后的提交类型</returns>
        private string FixType(string _type)
        {
            if (string.IsNullOrEmpty(_type))
            {
                // 如果类型为空，则使用默认类型
                return m_Config.CommitTypes.Count > 0 ? m_Config.CommitTypes[0].Type : "feat";
            }

            // 检查类型是否在配置的有效类型列表中
            foreach (var commitType in m_Config.CommitTypes)
            {
                if (commitType.Enabled && commitType.Type.Equals(_type, StringComparison.OrdinalIgnoreCase))
                {
                    // 返回正确大小写的类型
                    return commitType.Type;
                }
            }

            // 如果类型无效，则使用默认类型
            return m_Config.CommitTypes.Count > 0 ? m_Config.CommitTypes[0].Type : "feat";
        }

        /// <summary>
        /// 修复作用域
        /// </summary>
        /// <param name="_scope">作用域</param>
        /// <returns>修复后的作用域</returns>
        private string FixScope(string _scope)
        {
            if (string.IsNullOrEmpty(_scope))
            {
                // 如果作用域为空且不要求作用域，则返回空
                if (!m_Config.RequireScope)
                {
                    return string.Empty;
                }

                // 如果作用域为空但要求作用域，则使用默认作用域
                return m_Config.Scopes.Count > 0 ? m_Config.Scopes[0] : "core";
            }

            // 检查作用域是否在配置的有效作用域列表中
            foreach (var scope in m_Config.Scopes)
            {
                if (scope.Equals(_scope, StringComparison.OrdinalIgnoreCase))
                {
                    // 返回正确大小写的作用域
                    return scope;
                }
            }

            // 如果作用域无效，则使用默认作用域
            return m_Config.Scopes.Count > 0 ? m_Config.Scopes[0] : "core";
        }

        /// <summary>
        /// 修复简短描述
        /// </summary>
        /// <param name="_subject">简短描述</param>
        /// <returns>修复后的简短描述</returns>
        private string FixSubject(string _subject)
        {
            if (string.IsNullOrEmpty(_subject))
            {
                return string.Empty;
            }

            // 移除末尾的句号
            var subject = _subject.TrimEnd('.');

            // 截断过长的简短描述
            if (subject.Length > m_Config.SubjectMaxLength)
            {
                subject = subject.Substring(0, m_Config.SubjectMaxLength);
            }

            return subject;
        }

        /// <summary>
        /// 修复提交消息文本
        /// </summary>
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>修复后的提交消息文本</returns>
        public string FixText(string _messageText)
        {
            var message = CommitMessageParser.Parse(_messageText);
            var fixedMessage = Fix(message);
            return fixedMessage.GetFormattedMessage();
        }

        /// <summary>
        /// 修复提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>是否修复成功</returns>
        public bool FixFile(string _filePath)
        {
            try
            {
                if (!System.IO.File.Exists(_filePath))
                {
                    Debug.LogError($"[TByd.CodeStyle] 提交消息文件不存在: {_filePath}");
                    return false;
                }

                var messageText = System.IO.File.ReadAllText(_filePath);
                var fixedMessageText = FixText(messageText);

                System.IO.File.WriteAllText(_filePath, fixedMessageText);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 修复提交消息文件失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 生成提交消息模板
        /// </summary>
        /// <returns>提交消息模板</returns>
        public string GenerateTemplate()
        {
            var sb = new StringBuilder();

            // 添加模板说明
            sb.AppendLine("# 请按照以下格式填写提交信息:");
            sb.AppendLine("# <type>(<scope>): <subject>");
            sb.AppendLine("# <空行>");
            sb.AppendLine("# <body>");
            sb.AppendLine("# <空行>");
            sb.AppendLine("# <footer>");
            sb.AppendLine("#");

            // 添加类型说明
            sb.AppendLine("# 类型(type)说明:");
            foreach (var commitType in m_Config.CommitTypes)
            {
                if (commitType.Enabled)
                {
                    sb.AppendLine($"#   {commitType.Type}:\t{commitType.Description}");
                }
            }
            sb.AppendLine("#");

            // 添加作用域说明
            sb.AppendLine("# 作用域(scope)说明:");
            foreach (var scope in m_Config.Scopes)
            {
                sb.AppendLine($"#   {scope}");
            }
            sb.AppendLine("#");

            // 添加简短描述说明
            sb.AppendLine("# 简短描述(subject)说明:");
            sb.AppendLine("#   - 使用祈使句（如\"添加\"而不是\"添加了\"）");
            sb.AppendLine("#   - 不要以句号结尾");
            sb.AppendLine($"#   - 不要超过{m_Config.SubjectMaxLength}个字符");
            sb.AppendLine("#");

            // 添加详细描述说明
            sb.AppendLine("# 详细描述(body)说明:");
            sb.AppendLine("#   - 解释为什么进行此次修改");
            sb.AppendLine("#   - 描述与之前行为的区别");
            sb.AppendLine("#");

            // 添加页脚说明
            sb.AppendLine("# 关闭问题(footer)说明:");
            sb.AppendLine("#   - 关闭的问题: Closes #123, #456");
            sb.AppendLine("#   - 破坏性变更说明");
            sb.AppendLine("#");

            return sb.ToString();
        }

        /// <summary>
        /// 将模板写入提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>是否写入成功</returns>
        public bool WriteTemplateToFile(string _filePath)
        {
            try
            {
                var template = GenerateTemplate();

                // 如果文件已存在，则读取原始内容
                var originalContent = string.Empty;
                if (System.IO.File.Exists(_filePath))
                {
                    originalContent = System.IO.File.ReadAllText(_filePath);
                }

                // 将模板和原始内容写入文件
                System.IO.File.WriteAllText(_filePath, template + originalContent);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 写入提交消息模板失败: {e.Message}");
                return false;
            }
        }
    }
}
