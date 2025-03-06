using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 模板选项类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TemplateOptionType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String,

        /// <summary>
        /// 布尔值
        /// </summary>
        Boolean,

        /// <summary>
        /// 整数
        /// </summary>
        Integer,

        /// <summary>
        /// 枚举
        /// </summary>
        Enum
    }

    /// <summary>
    /// 模板选项，定义模板可配置的选项
    /// </summary>
    [Serializable]
    public class TemplateOption
    {
        /// <summary>
        /// 选项键
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 选项显示名称
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 选项描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 选项类型
        /// </summary>
        [JsonProperty("type")]
        public TemplateOptionType Type { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否必需
        /// </summary>
        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 可选值（用于枚举类型）
        /// </summary>
        [JsonProperty("possibleValues")]
        public List<string> PossibleValues { get; set; } = new List<string>();

        /// <summary>
        /// 创建一个新的模板选项
        /// </summary>
        /// <param name="key">选项键</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="description">描述</param>
        /// <param name="type">类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="isRequired">是否必需</param>
        public TemplateOption(string key, string displayName, string description, TemplateOptionType type,
            string defaultValue = "", bool isRequired = true)
        {
            Key = key;
            DisplayName = displayName;
            Description = description;
            Type = type;
            DefaultValue = defaultValue;
            IsRequired = isRequired;
        }

        /// <summary>
        /// 添加可选值（用于枚举类型）
        /// </summary>
        /// <param name="value">可选值</param>
        public void AddPossibleValue(string value)
        {
            PossibleValues.Add(value);
        }

        /// <summary>
        /// 验证选项值是否有效
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateValue(string value)
        {
            var result = new ValidationResult();

            // 检查必需性
            if (IsRequired && string.IsNullOrEmpty(value))
            {
                result.AddError($"选项 '{DisplayName}' 是必需的", Key);
                return result;
            }

            // 如果值为空且不是必需的，则视为有效
            if (string.IsNullOrEmpty(value) && !IsRequired)
            {
                return result;
            }

            // 根据类型验证
            switch (Type)
            {
                case TemplateOptionType.Boolean:
                    if (!bool.TryParse(value, out _))
                    {
                        result.AddError($"选项 '{DisplayName}' 必须是布尔值", Key);
                    }
                    break;

                case TemplateOptionType.Integer:
                    if (!int.TryParse(value, out _))
                    {
                        result.AddError($"选项 '{DisplayName}' 必须是整数", Key);
                    }
                    break;

                case TemplateOptionType.Enum:
                    if (!PossibleValues.Contains(value))
                    {
                        result.AddError($"选项 '{DisplayName}' 必须是以下值之一: {string.Join(", ", PossibleValues)}", Key);
                    }
                    break;
            }

            return result;
        }
    }
}
