using System;
using System.Collections.Generic;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 验证消息级别
    /// </summary>
    public enum ValidationMessageLevel
    {
        /// <summary>
        /// 信息
        /// </summary>
        Info,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        Error
    }

    /// <summary>
    /// 验证消息
    /// </summary>
    [Serializable]
    public class ValidationMessage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息级别
        /// </summary>
        public ValidationMessageLevel Level { get; set; }

        /// <summary>
        /// 相关字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 创建一个新的验证消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="level">消息级别</param>
        /// <param name="field">相关字段</param>
        public ValidationMessage(string message, ValidationMessageLevel level, string field = "")
        {
            Message = message;
            Level = level;
            Field = field;
        }
    }

    /// <summary>
    /// 验证结果，包含验证消息和是否通过验证
    /// </summary>
    [Serializable]
    public class ValidationResult
    {
        /// <summary>
        /// 验证消息列表
        /// </summary>
        public List<ValidationMessage> Messages { get; private set; } = new List<ValidationMessage>();

        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool IsValid => !HasErrors;

        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasErrors { get; private set; }

        /// <summary>
        /// 是否有警告
        /// </summary>
        public bool HasWarnings { get; private set; }

        /// <summary>
        /// 创建一个新的验证结果
        /// </summary>
        public ValidationResult()
        {
        }

        /// <summary>
        /// 添加信息消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="field">相关字段</param>
        public void AddInfo(string message, string field = "")
        {
            Messages.Add(new ValidationMessage(message, ValidationMessageLevel.Info, field));
        }

        /// <summary>
        /// 添加警告消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="field">相关字段</param>
        public void AddWarning(string message, string field = "")
        {
            Messages.Add(new ValidationMessage(message, ValidationMessageLevel.Warning, field));
            HasWarnings = true;
        }

        /// <summary>
        /// 添加错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="field">相关字段</param>
        public void AddError(string message, string field = "")
        {
            Messages.Add(new ValidationMessage(message, ValidationMessageLevel.Error, field));
            HasErrors = true;
        }

        /// <summary>
        /// 合并另一个验证结果
        /// </summary>
        /// <param name="other">要合并的验证结果</param>
        public void Merge(ValidationResult other)
        {
            if (other == null) return;

            Messages.AddRange(other.Messages);
            HasErrors |= other.HasErrors;
            HasWarnings |= other.HasWarnings;
        }

        /// <summary>
        /// 获取特定级别的消息
        /// </summary>
        /// <param name="level">消息级别</param>
        /// <returns>消息列表</returns>
        public List<ValidationMessage> GetMessages(ValidationMessageLevel level)
        {
            return Messages.FindAll(m => m.Level == level);
        }

        /// <summary>
        /// 获取特定字段的消息
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>消息列表</returns>
        public List<ValidationMessage> GetMessagesForField(string field)
        {
            return Messages.FindAll(m => m.Field == field);
        }
    }
}
