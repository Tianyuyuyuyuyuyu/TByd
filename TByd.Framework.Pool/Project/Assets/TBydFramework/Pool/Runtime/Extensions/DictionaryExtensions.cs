using System;
using System.Collections.Generic;

namespace TBydFramework.Pool.Runtime.Extensions
{
    /// <summary>
    /// Dictionary的扩展方法
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 获取指定键的值，如果键不存在则添加新值
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典实例</param>
        /// <param name="key">键</param>
        /// <param name="valueFactory">创建新值的工厂方法</param>
        /// <returns>获取到的值或新创建的值</returns>
        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TKey, TValue> valueFactory)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            if (valueFactory == null) throw new ArgumentNullException(nameof(valueFactory));

            if (!dictionary.TryGetValue(key, out var value))
            {
                value = valueFactory(key);
                dictionary[key] = value;
            }
            return value;
        }

        /// <summary>
        /// 获取指定键的值，如果键不存在则添加新值
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典实例</param>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <returns>获取到的值或新添加的值</returns>
        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            if (!dictionary.TryGetValue(key, out var existingValue))
            {
                dictionary[key] = value;
                return value;
            }
            return existingValue;
        }
    }
} 