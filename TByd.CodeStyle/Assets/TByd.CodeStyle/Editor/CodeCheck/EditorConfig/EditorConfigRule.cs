using System;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] 
        private string m_Pattern;
        
        /// <summary>
        /// 规则属性
        /// </summary>
        [SerializeField] 
        private Dictionary<string, string> m_Properties = new Dictionary<string, string>();
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public EditorConfigRule()
        {
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_pattern">规则模式</param>
        public EditorConfigRule(string _pattern)
        {
            m_Pattern = _pattern;
        }
        
        /// <summary>
        /// 规则模式
        /// </summary>
        public string Pattern
        {
            get => m_Pattern;
            set => m_Pattern = value;
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
        /// <param name="_key">属性名</param>
        /// <param name="_value">属性值</param>
        public void SetProperty(string _key, string _value)
        {
            if (string.IsNullOrEmpty(_key))
            {
                return;
            }
            
            // EditorConfig属性名不区分大小写
            _key = _key.ToLowerInvariant();
            
            if (m_Properties.ContainsKey(_key))
            {
                m_Properties[_key] = _value;
            }
            else
            {
                m_Properties.Add(_key, _value);
            }
        }
        
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="_key">属性名</param>
        /// <param name="_defaultValue">默认值</param>
        /// <returns>属性值</returns>
        public string GetProperty(string _key, string _defaultValue = "")
        {
            if (string.IsNullOrEmpty(_key))
            {
                return _defaultValue;
            }
            
            // EditorConfig属性名不区分大小写
            _key = _key.ToLowerInvariant();
            
            if (m_Properties.TryGetValue(_key, out string value))
            {
                return value;
            }
            
            return _defaultValue;
        }
        
        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="_key">属性名</param>
        public void RemoveProperty(string _key)
        {
            if (string.IsNullOrEmpty(_key))
            {
                return;
            }
            
            // EditorConfig属性名不区分大小写
            _key = _key.ToLowerInvariant();
            
            if (m_Properties.ContainsKey(_key))
            {
                m_Properties.Remove(_key);
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