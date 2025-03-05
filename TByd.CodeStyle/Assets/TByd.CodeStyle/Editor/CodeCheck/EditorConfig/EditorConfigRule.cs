using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TByd.CodeStyle.Editor.CodeCheck.EditorConfig
{
    /// <summary>
    /// EditorConfig规则
    /// </summary>
    [Serializable]
    public class EditorConfigRule
    {
        /// <summary>
        /// 规则模式（文件匹配模式）
        /// </summary>
        [FormerlySerializedAs("m_Pattern")] [SerializeField]
        private string mPattern;

        /// <summary>
        /// 规则属性
        /// </summary>
        [SerializeField]
        private Dictionary<string, string> m_Properties = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 构造函数
        /// </summary>
        public EditorConfigRule()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pattern">规则模式</param>
        public EditorConfigRule(string pattern)
        {
            mPattern = pattern;
        }

        /// <summary>
        /// 规则模式
        /// </summary>
        public string Pattern
        {
            get => mPattern;
            set => mPattern = value;
        }

        /// <summary>
        /// 规则属性
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get => m_Properties;
            set => m_Properties = value;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        public void SetProperty(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            // 使用大小写不敏感的字典，无需转换为小写
            m_Properties[key] = value;
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <returns>属性值，如果不存在则返回空字符串</returns>
        public string GetProperty(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            // 使用大小写不敏感的字典，无需转换为小写
            if (m_Properties.TryGetValue(key, out var value))
            {
                return value;
            }

            return string.Empty;
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="key">属性名</param>
        public void RemoveProperty(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            // 使用大小写不敏感的字典，无需转换为小写
            if (m_Properties.ContainsKey(key))
            {
                m_Properties.Remove(key);
            }
        }

        /// <summary>
        /// 清空所有属性
        /// </summary>
        public void ClearProperties()
        {
            m_Properties.Clear();
        }
    }
}
