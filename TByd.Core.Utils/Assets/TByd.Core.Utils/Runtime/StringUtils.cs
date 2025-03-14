using System;
using System.Text;

namespace TByd.Core.Utils.Runtime
{
    /// <summary>
    /// 提供高性能、低GC压力的字符串操作工具
    /// </summary>
    /// <remarks>
    /// StringUtils类包含一系列经过优化的字符串处理方法，设计目标是在Unity项目中提供高效的字符串操作。
    /// 所有方法都经过精心设计，尽量减少垃圾回收(GC)压力和内存分配，适合在性能敏感的场景中使用。
    /// 
    /// <para>主要功能：</para>
    /// <list type="bullet">
    ///   <item>字符串验证（空值检查等）</item>
    ///   <item>随机字符串生成</item>
    ///   <item>字符串格式转换（如URL友好的slug）</item>
    ///   <item>字符串处理（截断、分割等）</item>
    /// </list>
    /// </remarks>
    public static class StringUtils
    {
        /// <summary>
        /// 用于生成随机字符串的随机数生成器
        /// </summary>
        private static readonly Random Random = new Random();
        
        /// <summary>
        /// 用于生成随机字符串的字母数字字符集
        /// </summary>
        private static readonly char[] AlphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        
        /// <summary>
        /// 用于生成包含特殊字符的随机字符串的字符集
        /// </summary>
        private static readonly char[] AlphanumericAndSpecialChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=<>?".ToCharArray();

        /// <summary>
        /// 检查字符串是否为空或仅包含空白字符
        /// </summary>
        /// <param name="value">要检查的字符串</param>
        /// <returns>如果字符串为null、空或仅包含空白字符，则返回true；否则返回false</returns>
        /// <remarks>
        /// 此方法是 .NET 中 string.IsNullOrWhiteSpace 方法的替代实现，兼容所有Unity版本。
        /// 
        /// <para>性能注意事项：</para>
        /// 此方法会逐字符检查，对于非常长的字符串可能影响性能。但在大多数使用场景中，
        /// 字符串长度通常较短，不会造成明显的性能问题。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 检查用户输入是否有效
        /// string userInput = GetUserInput();
        /// if (StringUtils.IsNullOrWhiteSpace(userInput))
        /// {
        ///     Debug.LogWarning("输入不能为空!");
        ///     return;
        /// }
        /// 
        /// // 在数据处理前验证
        /// if (!StringUtils.IsNullOrWhiteSpace(dataField))
        /// {
        ///     ProcessData(dataField);
        /// }
        /// </code>
        /// </remarks>
        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value == null) return true;
            if (value.Length == 0) return true;

            foreach (var charStr in value)
            {
                if (!char.IsWhiteSpace(charStr))
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
        /// <remarks>
        /// 根据参数 includeSpecialChars 的值，生成的字符串可以只包含字母和数字，
        /// 或者同时包含特殊字符（如!@#$%^等）。
        /// 
        /// <para>线程安全：</para>
        /// 此方法使用 lock 机制确保在多线程环境下的安全使用。
        /// 
        /// <para>性能说明：</para>
        /// 此方法优化了内存分配，直接创建目标长度的字符数组，而不是使用StringBuilder逐字符追加。
        /// 对于短字符串（长度&lt;1000），性能表现良好。对于极长字符串，可能需要考虑专用的随机数生成方案。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 生成简单的随机ID
        /// string randomId = StringUtils.GenerateRandom(8);
        /// 
        /// // 生成包含特殊字符的随机密码
        /// string securePassword = StringUtils.GenerateRandom(12, includeSpecialChars: true);
        /// 
        /// // 生成会话令牌
        /// string sessionToken = StringUtils.GenerateRandom(32);
        /// </code>
        /// </remarks>
        public static string GenerateRandom(int length, bool includeSpecialChars = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "长度不能小于0");

            if (length == 0)
                return string.Empty;

            var chars = new char[length];
            var sourceChars = includeSpecialChars ? AlphanumericAndSpecialChars : AlphanumericChars;

            lock (Random)
            {
                for (var i = 0; i < length; i++)
                {
                    chars[i] = sourceChars[Random.Next(0, sourceChars.Length)];
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
        /// <remarks>
        /// Slug是一种URL友好的字符串格式，通常用于创建SEO友好的URL。
        /// 此方法执行以下转换：
        /// <list type="bullet">
        ///   <item>将字母转换为小写</item>
        ///   <item>将空格、连字符、下划线、点和逗号替换为单个连字符</item>
        ///   <item>保留字母、数字和中文等非ASCII字符</item>
        ///   <item>删除开头和结尾的连字符</item>
        /// </list>
        /// 
        /// <para>多语言支持：</para>
        /// 此方法支持中文、日文等非ASCII字符，使其适用于国际化应用程序。
        /// 非字母数字的Unicode字符将被保留，确保多语言URL的可读性和SEO友好性。
        /// 
        /// <para>性能说明：</para>
        /// 此方法使用StringBuilder进行字符串构建，避免了字符串连接操作导致的过多内存分配。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 将文章标题转换为URL slug
        /// string articleTitle = "How to Use String Utils in Unity?";
        /// string urlSlug = StringUtils.ToSlug(articleTitle);
        /// // 结果: "how-to-use-string-utils-in-unity"
        /// 
        /// // 处理包含特殊字符的文本
        /// string complexText = "Product: Gaming Mouse (Black) - $59.99";
        /// string productSlug = StringUtils.ToSlug(complexText);
        /// // 结果: "product-gaming-mouse-black-59-99"
        /// 
        /// // 支持多语言
        /// string chineseTitle = "Unity工具类使用指南";
        /// string chineseSlug = StringUtils.ToSlug(chineseTitle);
        /// // 结果: "unity工具类使用指南"
        /// </code>
        /// </remarks>
        public static string ToSlug(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // 转换为小写
            value = value.ToLowerInvariant();

            // 替换非字母数字字符为连字符
            var sb = new StringBuilder(value.Length);
            var lastWasHyphen = false;

            foreach (var c in value)
            {
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || 
                    char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
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
            var result = sb.ToString().Trim('-');
            return result;
        }

        /// <summary>
        /// 截断字符串到指定长度，添加省略号或自定义后缀
        /// </summary>
        /// <param name="value">要截断的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">截断后添加的后缀，默认为"..."</param>
        /// <returns>截断后的字符串</returns>
        /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
        /// <exception cref="ArgumentOutOfRangeException">当maxLength小于0时抛出</exception>
        /// <remarks>
        /// 此方法确保结果字符串（包括后缀）不会超过指定的最大长度。
        /// 如果原始字符串长度已经小于或等于最大长度，则返回原始字符串。
        /// 
        /// <para>边界情况处理：</para>
        /// <list type="bullet">
        ///   <item>如果maxLength小于或等于suffix长度，返回截断的suffix</item>
        ///   <item>如果suffix为null，使用空字符串</item>
        /// </list>
        /// 
        /// <para>常见用途：</para>
        /// <list type="bullet">
        ///   <item>UI显示长文本时截断</item>
        ///   <item>日志输出时限制长度</item>
        ///   <item>创建预览或摘要</item>
        /// </list>
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 基本用法
        /// string longText = "这是一段很长的文本，需要被截断以适应UI显示";
        /// string truncated = StringUtils.Truncate(longText, 10);
        /// // 结果: "这是一段很长..."
        /// 
        /// // 自定义后缀
        /// string preview = StringUtils.Truncate(longText, 12, "[更多]");
        /// // 结果: "这是一段很长[更多]"
        /// 
        /// // 处理极短的maxLength
        /// string tiny = StringUtils.Truncate(longText, 2, "...");
        /// // 结果: ".."
        /// </code>
        /// </remarks>
        public static string Truncate(string value, int maxLength, string suffix = "...")
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "最大长度不能小于0");

            suffix ??= string.Empty;

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
        /// <remarks>
        /// 此方法实现了零分配（zero-allocation）的字符串分割，避免了创建字符串数组和不必要的内存分配。
        /// 它返回一个特殊的枚举器结构，而不是分配一个完整的字符串数组。
        /// 
        /// <para>性能优势：</para>
        /// <list type="bullet">
        ///   <item>避免了一次性预分配整个结果数组</item>
        ///   <item>按需计算和返回每个子字符串</item>
        ///   <item>显著减少了内存使用和GC压力</item>
        /// </list>
        /// 
        /// <para>使用注意事项：</para>
        /// <list type="bullet">
        ///   <item>返回的是一个ref struct，只能在局部范围使用，不能作为字段或返回值</item>
        ///   <item>每次迭代仍然会创建一个新的子字符串对象</item>
        ///   <item>如果您只需要遍历而不存储结果，这种方法特别高效</item>
        /// </list>
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 基本用法
        /// string csvLine = "value1,value2,value3,value4";
        /// foreach (string item in StringUtils.Split(csvLine, ','))
        /// {
        ///     ProcessItem(item);
        /// }
        /// 
        /// // 处理大文件中的一行
        /// string largeFileLine = GetVeryLongLine();
        /// int count = 0;
        /// foreach (string token in StringUtils.Split(largeFileLine, ';'))
        /// {
        ///     count++;
        ///     if (IsTargetToken(token))
        ///     {
        ///         return token; // 提前退出，避免处理整行
        ///     }
        /// }
        /// </code>
        /// </remarks>
        public static StringSplitEnumerator Split(string value, char separator)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new StringSplitEnumerator(value, separator);
        }

        /// <summary>
        /// 零分配字符串分割器
        /// </summary>
        /// <remarks>
        /// 这是一个高效的字符串分割实现，设计用于减少内存分配和GC压力。
        /// 使用ref struct确保不会被装箱或分配到堆上，只能作为局部变量使用。
        /// 
        /// <para>性能说明：</para>
        /// 虽然枚举过程中仍会创建子字符串，但避免了一次性分配整个字符串数组，
        /// 对于大型文本处理尤其有效。
        /// 
        /// <para>限制：</para>
        /// <list type="bullet">
        ///   <item>作为ref struct，不能用作字段、属性或异步方法的一部分</item>
        ///   <item>不能存储在数组或集合中</item>
        ///   <item>不能用作方法的返回类型（除了作为泛型类型参数）</item>
        /// </list>
        /// </remarks>
        public ref struct StringSplitEnumerator
        {
            /// <summary>
            /// 要分割的原始字符串
            /// </summary>
            private readonly string _str;
            
            /// <summary>
            /// 用于分割的分隔符
            /// </summary>
            private readonly char _separator;
            
            /// <summary>
            /// 当前处理的字符串索引位置
            /// </summary>
            private int _index;

            /// <summary>
            /// 初始化StringSplitEnumerator的新实例
            /// </summary>
            /// <param name="str">要分割的字符串</param>
            /// <param name="separator">分隔符</param>
            /// <remarks>
            /// 构造函数初始化分割器，但不执行任何实际分割操作。
            /// 分割操作将在调用MoveNext方法时逐步执行。
            /// </remarks>
            public StringSplitEnumerator(string str, char separator)
            {
                _str = str;
                _separator = separator;
                _index = 0;
                Current = null;
            }

            /// <summary>
            /// 获取枚举器
            /// </summary>
            /// <returns>当前枚举器</returns>
            /// <remarks>
            /// 此方法支持foreach语法，返回枚举器自身。
            /// </remarks>
            public StringSplitEnumerator GetEnumerator() => this;

            /// <summary>
            /// 移动到下一个元素
            /// </summary>
            /// <returns>如果还有更多元素，则返回true；否则返回false</returns>
            /// <remarks>
            /// 此方法查找下一个分隔符并提取子字符串。
            /// 如果已到达字符串末尾或没有更多元素，返回false。
            /// </remarks>
            public bool MoveNext()
            {
                if (_index > _str.Length)
                    return false;

                var start = _index;
                var end = _str.IndexOf(_separator, start);

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
            /// <remarks>
            /// 此属性返回当前处理的子字符串。
            /// 在首次调用MoveNext之前，其值为默认值（null）。
            /// </remarks>
            public string Current { get; private set; }
        }
    }
} 