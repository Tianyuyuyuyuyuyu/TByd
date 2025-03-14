using System;
using System.Text;

namespace TByd.Core.Utils.Runtime
{
    /// <summary>
    /// 提供高性能、低GC压力的字符串操作工具
    /// </summary>
    public static class StringUtils
    {
        private static readonly Random _random = new Random();
        private static readonly char[] _alphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private static readonly char[] _alphanumericAndSpecialChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=<>?".ToCharArray();

        /// <summary>
        /// 检查字符串是否为空或仅包含空白字符
        /// </summary>
        /// <param name="value">要检查的字符串</param>
        /// <returns>如果字符串为null、空或仅包含空白字符，则返回true；否则返回false</returns>
        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value == null) return true;
            if (value.Length == 0) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">随机字符串的长度</param>
        /// <param name="includeSpecialChars">是否包含特殊字符</param>
        /// <returns>生成的随机字符串</returns>
        /// <exception cref="ArgumentOutOfRangeException">当length小于0时抛出</exception>
        public static string GenerateRandom(int length, bool includeSpecialChars = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "长度不能小于0");

            if (length == 0)
                return string.Empty;

            char[] chars = new char[length];
            char[] sourceChars = includeSpecialChars ? _alphanumericAndSpecialChars : _alphanumericChars;

            lock (_random)
            {
                for (int i = 0; i < length; i++)
                {
                    chars[i] = sourceChars[_random.Next(0, sourceChars.Length)];
                }
            }

            return new string(chars);
        }

        /// <summary>
        /// 将字符串转换为URL友好的slug格式
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns>转换后的slug字符串</returns>
        /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
        public static string ToSlug(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // 转换为小写
            value = value.ToLowerInvariant();

            // 替换非字母数字字符为连字符
            StringBuilder sb = new StringBuilder(value.Length);
            bool lastWasHyphen = false;

            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    lastWasHyphen = false;
                }
                else if (!lastWasHyphen && (c == ' ' || c == '-' || c == '_' || c == '.' || c == ','))
                {
                    sb.Append('-');
                    lastWasHyphen = true;
                }
            }

            // 移除开头和结尾的连字符
            string result = sb.ToString().Trim('-');
            return result;
        }

        /// <summary>
        /// 截断字符串到指定长度，添加省略号
        /// </summary>
        /// <param name="value">要截断的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">截断后添加的后缀，默认为"..."</param>
        /// <returns>截断后的字符串</returns>
        /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
        /// <exception cref="ArgumentOutOfRangeException">当maxLength小于0时抛出</exception>
        public static string Truncate(string value, int maxLength, string suffix = "...")
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "最大长度不能小于0");

            if (suffix == null)
                suffix = string.Empty;

            if (value.Length <= maxLength)
                return value;

            if (maxLength <= suffix.Length)
                return suffix.Substring(0, maxLength);

            return value.Substring(0, maxLength - suffix.Length) + suffix;
        }

        /// <summary>
        /// 高性能分割字符串，减少GC压力
        /// </summary>
        /// <param name="value">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的字符串枚举器</returns>
        /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
        public static StringSplitEnumerator Split(string value, char separator)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new StringSplitEnumerator(value, separator);
        }

        /// <summary>
        /// 零分配字符串分割器
        /// </summary>
        public ref struct StringSplitEnumerator
        {
            private readonly string _str;
            private readonly char _separator;
            private int _index;

            /// <summary>
            /// 初始化StringSplitEnumerator的新实例
            /// </summary>
            /// <param name="str">要分割的字符串</param>
            /// <param name="separator">分隔符</param>
            public StringSplitEnumerator(string str, char separator)
            {
                _str = str;
                _separator = separator;
                _index = 0;
                Current = default;
            }

            /// <summary>
            /// 获取枚举器
            /// </summary>
            /// <returns>当前枚举器</returns>
            public StringSplitEnumerator GetEnumerator() => this;

            /// <summary>
            /// 移动到下一个元素
            /// </summary>
            /// <returns>如果还有更多元素，则返回true；否则返回false</returns>
            public bool MoveNext()
            {
                if (_index > _str.Length)
                    return false;

                int start = _index;
                int end = _str.IndexOf(_separator, start);

                if (end == -1)
                {
                    if (_index < _str.Length)
                    {
                        Current = _str.Substring(_index);
                        _index = _str.Length + 1;
                        return true;
                    }
                    return false;
                }

                Current = _str.Substring(start, end - start);
                _index = end + 1;
                return true;
            }

            /// <summary>
            /// 获取当前元素
            /// </summary>
            public string Current { get; private set; }
        }
    }
} 