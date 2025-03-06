using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TByd.PackageCreator.Editor.Utils
{
    /// <summary>
    /// 字符串处理工具类，提供常用的字符串操作和转换功能
    /// </summary>
    public static class StringUtils
    {
        // C#关键字集合，用于标识符验证和转换
        private static readonly HashSet<string> CSharpKeywords = new HashSet<string>
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
            "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
            "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
            "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
            "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
            "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
            "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw",
            "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
            "virtual", "void", "volatile", "while"
        };

        // 常用字符串池缓存，用于减少相同字符串的内存分配
        private static readonly Dictionary<string, string> StringPool = new Dictionary<string, string>();

        // 最大缓存大小，防止内存泄漏
        private const int MaxCacheSize = 10000;

        // 字符串池中缓存的最大字符串长度
        private const int MaxPoolStringLength = 128;

        /// <summary>
        /// 将字符串添加到字符串池中，如果池中已存在相同字符串，则返回池中的实例
        /// 这有助于减少内存分配，特别是对于频繁使用的短字符串
        /// </summary>
        /// <param name="input">要池化的字符串</param>
        /// <returns>池化后的字符串实例</returns>
        public static string Intern(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 对于过长的字符串，不进行池化
            if (input.Length > MaxPoolStringLength)
                return input;

            lock (StringPool)
            {
                // 如果池已满，清空一半的内容
                if (StringPool.Count >= MaxCacheSize)
                {
                    // 随机清除一半的缓存以避免占用过多内存
                    var keysToRemove = StringPool.Keys.Take(MaxCacheSize / 2).ToList();
                    foreach (var key in keysToRemove)
                    {
                        StringPool.Remove(key);
                    }
                }

                // 检查池中是否已有此字符串
                if (StringPool.TryGetValue(input, out string pooledString))
                    return pooledString;

                // 将字符串添加到池中
                StringPool[input] = input;
                return input;
            }
        }

        /// <summary>
        /// 清空字符串池，释放内存
        /// </summary>
        public static void ClearStringPool()
        {
            lock (StringPool)
            {
                StringPool.Clear();
            }
        }

        /// <summary>
        /// 清空字符宽度缓存，释放内存（兼容性方法，无实际功能）
        /// </summary>
        public static void ClearWidthCache()
        {
            // 空方法，保留兼容性
        }

        /// <summary>
        /// 将普通字符串转换为驼峰命名（第一个单词首字母小写，其他单词首字母大写）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>驼峰命名的字符串</returns>
        public static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 先分割为单词
            var words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 第一个单词首字母小写，其他单词首字母大写
            var sb = new StringBuilder();
            sb.Append(words[0].ToLowerInvariant());

            for (var i = 1; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    sb.Append(char.ToUpperInvariant(words[i][0]));
                    if (words[i].Length > 1)
                    {
                        sb.Append(words[i].Substring(1).ToLowerInvariant());
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将普通字符串转换为帕斯卡命名（所有单词首字母大写）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>帕斯卡命名的字符串</returns>
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 先分割为单词
            var words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 所有单词首字母大写
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    sb.Append(char.ToUpperInvariant(word[0]));
                    if (word.Length > 1)
                    {
                        sb.Append(word.Substring(1).ToLowerInvariant());
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将普通字符串转换为蛇形命名（小写字母，单词之间用下划线连接）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>蛇形命名的字符串</returns>
        public static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 先分割为单词
            var words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 所有单词小写，用下划线连接
            return string.Join("_", words.Select(w => w.ToLowerInvariant()));
        }

        /// <summary>
        /// 将普通字符串转换为短横线命名（小写字母，单词之间用短横线连接）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>短横线命名的字符串</returns>
        public static string ToKebabCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 先分割为单词
            var words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 所有单词小写，用短横线连接
            return string.Join("-", words.Select(w => w.ToLowerInvariant()));
        }

        /// <summary>
        /// 将字符串分割成单词数组
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>单词数组</returns>
        private static string[] SplitIntoWords(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Array.Empty<string>();

            // 处理常见分隔符
            input = input.Replace('-', ' ')
                        .Replace('_', ' ')
                        .Replace('.', ' ');

            // 在大写字母前添加空格（保留连续大写字母，如缩写词）
            input = Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");

            // 分割并移除空字符串
            return input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 截断字符串到指定长度，并添加省略号（如果需要）。
        /// 截断后的总长度（包括省略号）将等于或小于指定的最大长度。
        /// 此方法针对频繁调用进行了性能优化。
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="maxLength">截断后的最大总长度（包括省略号）</param>
        /// <param name="ellipsis">省略号字符串</param>
        /// <returns>截断后的字符串</returns>
        public static string Truncate(string input, int maxLength, string ellipsis = "...")
        {
            // 处理null或空字符串
            if (string.IsNullOrEmpty(input))
                return input;

            // 处理maxLength小于等于0的情况
            if (maxLength <= 0)
                return string.Empty;

            // 如果输入长度小于等于最大长度，直接返回
            // 注意：这里有个特殊情况，当包含中文等宽字符时，即使长度相等也可能需要截断
            if (input.Length < maxLength || (input.Length == maxLength && !ContainsWideChar(input)))
                return input;

            // 使用内联计算避免额外的变量，计算需要保留的字符数
            int charsToKeep = maxLength - ellipsis.Length;

            // 处理边界情况
            if (charsToKeep < 0)
            {
                // 如果maxLength小于ellipsis的长度，则返回ellipsis的前maxLength个字符
                // 确保不会传递负数给Substring
                int length = Math.Max(0, Math.Min(maxLength, ellipsis.Length));
                return length > 0 ? ellipsis.Substring(0, length) : string.Empty;
            }

            if (charsToKeep == 0)
                return ellipsis;

            // 使用StringBuilder可以避免额外的字符串分配
            StringBuilder sb = new StringBuilder(maxLength);
            sb.Append(input, 0, charsToKeep);
            sb.Append(ellipsis);

            return sb.ToString();
        }

        /// <summary>
        /// 检查字符串是否包含宽字符（如中文、日文、韩文等）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果包含宽字符则返回true，否则返回false</returns>
        private static bool ContainsWideChar(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            foreach (char c in input)
            {
                if (IsWideChar(c))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 基于显示宽度截断字符串，并添加省略号（如果需要）。
        /// 此方法考虑了不同字符的显示宽度，例如中文字符通常是英文字符的两倍宽。
        /// 截断后的总显示宽度（包括省略号）将等于或小于指定的最大宽度。
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="maxWidth">截断后的最大显示宽度（包括省略号）</param>
        /// <param name="ellipsis">省略号字符串</param>
        /// <returns>截断后的字符串</returns>
        public static string TruncateByWidth(string input, int maxWidth, string ellipsis = "...")
        {
            // 处理null或空字符串
            if (string.IsNullOrEmpty(input))
                return input;

            // 处理maxWidth小于等于0的情况
            if (maxWidth <= 0)
                return string.Empty;

            // 计算省略号的宽度
            int ellipsisWidth = GetStringDisplayWidth(ellipsis);

            // 计算输入字符串的总宽度
            int inputWidth = GetStringDisplayWidth(input);

            // 如果输入宽度小于等于最大宽度，直接返回
            if (inputWidth <= maxWidth)
                return input;

            // 计算可以保留的最大宽度
            int widthToKeep = maxWidth - ellipsisWidth;

            // 处理边界情况
            if (widthToKeep < 0)
                return string.Empty;

            if (widthToKeep == 0)
                return ellipsis;

            // 逐字符计算累计宽度，找到合适的截断位置
            int accumulatedWidth = 0;
            int charIndex = 0;

            StringBuilder sb = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                int charWidth = IsWideChar(c) ? 2 : 1;

                if (accumulatedWidth + charWidth > widthToKeep)
                    break;

                accumulatedWidth += charWidth;
                charIndex++;
                sb.Append(c);
            }

            // 添加省略号
            sb.Append(ellipsis);

            return sb.ToString();
        }

        /// <summary>
        /// 计算字符串的显示宽度。
        /// 东亚文字（如中文、日文、韩文）的宽度通常是西文字符的两倍。
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>字符串的显示宽度</returns>
        private static int GetStringDisplayWidth(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            int width = 0;
            foreach (char c in input)
            {
                width += IsWideChar(c) ? 2 : 1;
            }

            return width;
        }

        /// <summary>
        /// 判断字符是否为宽字符（如中文、日文、韩文等）
        /// </summary>
        /// <param name="c">要判断的字符</param>
        /// <returns>如果是宽字符则返回true，否则返回false</returns>
        private static bool IsWideChar(char c)
        {
            return IsWideChar((int)c);
        }

        /// <summary>
        /// 判断字符是否为宽字符（如中文、日文、韩文等）
        /// </summary>
        /// <param name="codePoint">要判断的字符编码点</param>
        /// <returns>如果是宽字符则返回true，否则返回false</returns>
        private static bool IsWideChar(int codePoint)
        {
            // 基本拉丁字母和拉丁-1补充
            if (codePoint <= 0x00FF)
                return false;

            // ASCII控制字符
            if (codePoint < 0x0020)
                return false;

            // 半角字符
            if (codePoint >= 0xFF00 && codePoint <= 0xFFEF)
            {
                // 全角ASCII、全角标点
                if (codePoint >= 0xFF01 && codePoint <= 0xFF5E)
                    return true;

                // 半角片假名、半角标点符号等
                return false;
            }

            // 组合字符（例如变音符号）
            if (codePoint >= 0x0300 && codePoint <= 0x036F)
                return false;

            // CJK统一表意文字
            if (codePoint >= 0x4E00 && codePoint <= 0x9FFF)
                return true;

            // 日文平假名
            if (codePoint >= 0x3040 && codePoint <= 0x309F)
                return true;

            // 日文片假名
            if (codePoint >= 0x30A0 && codePoint <= 0x30FF)
                return true;

            // 朝鲜文/韩文音节
            if (codePoint >= 0xAC00 && codePoint <= 0xD7AF)
                return true;

            // 朝鲜文/韩文字母
            if (codePoint >= 0x1100 && codePoint <= 0x11FF)
                return true;

            // 扩展CJK
            if (codePoint >= 0x20000 && codePoint <= 0x2A6DF)
                return true;

            // 中文标点
            if (codePoint >= 0x3000 && codePoint <= 0x303F)
                return true;

            // 其他情况：默认为非宽字符
            return false;
        }

        /// <summary>
        /// 将字符串中的参数占位符替换为给定值（格式为 {paramName}）
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="parameters">参数字典</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceParameters(string template, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(template) || parameters == null || parameters.Count == 0)
                return template;

            var sb = new StringBuilder(template);
            foreach (var param in parameters)
            {
                sb.Replace($"{{{param.Key}}}", param.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将字符串中的全部指定子字符串替换为新字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="replacements">替换字典，键为要替换的子字符串，值为替换后的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceAll(string input, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(input) || replacements == null || replacements.Count == 0)
                return input;

            var sb = new StringBuilder(input);
            foreach (var replacement in replacements)
            {
                sb.Replace(replacement.Key, replacement.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 检查字符串是否为有效的标识符（只包含字母、数字和下划线，且不以数字开头，且不是C#关键字）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果是有效标识符则返回true，否则返回false</returns>
        public static bool IsValidIdentifier(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // 第一个字符必须是字母或下划线
            if (!char.IsLetter(input[0]) && input[0] != '_')
                return false;

            // 其余字符必须是字母、数字或下划线
            for (var i = 1; i < input.Length; i++)
            {
                if (!char.IsLetterOrDigit(input[i]) && input[i] != '_')
                    return false;
            }

            // 检查是否为C#关键字
            return !CSharpKeywords.Contains(input);
        }

        /// <summary>
        /// 将字符串转换为有效的标识符（替换无效字符为下划线，确保第一个字符不是数字，为关键字添加前缀）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>有效的标识符</returns>
        public static string ToValidIdentifier(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "_";

            var sb = new StringBuilder();

            // 处理第一个字符
            if (char.IsDigit(input[0]))
            {
                sb.Append('_');
                sb.Append(input[0]); // 保留原始数字字符
            }
            else if (char.IsLetter(input[0]) || input[0] == '_')
            {
                sb.Append(input[0]);
            }
            else
            {
                sb.Append('_');
            }

            // 处理剩余字符
            for (var i = 1; i < input.Length; i++)
            {
                if (char.IsLetterOrDigit(input[i]) || input[i] == '_')
                {
                    sb.Append(input[i]);
                }
                else
                {
                    sb.Append('_');
                }
            }

            // 检查是否为C#关键字
            string result = sb.ToString();
            if (CSharpKeywords.Contains(result))
            {
                return "_" + result;
            }

            return result;
        }

        /// <summary>
        /// 将字符串转换为有效的命名空间（替换无效字符为点，移除连续点）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>有效的命名空间</returns>
        public static string ToValidNamespace(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // 替换常见分隔符为点
            var result = input.Replace('-', '.')
                               .Replace('_', '.')
                               .Replace(' ', '.');

            // 多个连续点替换为单个点
            result = Regex.Replace(result, "\\.+", ".");

            // 分割成命名空间部分
            var parts = result.Split('.');
            var validParts = new List<string>();

            foreach (var part in parts)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    // 每个部分必须是有效标识符
                    validParts.Add(ToValidIdentifier(part));
                }
            }

            // 如果没有有效部分，返回默认命名空间
            if (validParts.Count == 0)
                return "DefaultNamespace";

            return string.Join(".", validParts);
        }

        /// <summary>
        /// 将字符串转换为标题形式（每个单词首字母大写）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>标题形式的字符串</returns>
        public static string ToTitleCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 分割为单词
            var words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 将每个单词转换为标题形式
            for (var i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpperInvariant(words[i][0]) +
                              (words[i].Length > 1 ? words[i].Substring(1).ToLowerInvariant() : string.Empty);
                }
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// 生成指定长度的随机字母数字字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
                return string.Empty;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            var result = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 计算字符串的哈希码（使用SHA256算法）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>哈希字符串</returns>
        public static string ComputeHash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // 计算哈希
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 转换为十六进制字符串
                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 检查字符串是否包含任何无效路径字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果包含无效路径字符则返回true，否则返回false</returns>
        public static bool ContainsInvalidPathChars(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var invalidChars = System.IO.Path.GetInvalidPathChars();
            return input.IndexOfAny(invalidChars) >= 0;
        }

        /// <summary>
        /// 检查字符串是否包含任何无效文件名字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果包含无效文件名字符则返回true，否则返回false</returns>
        public static bool ContainsInvalidFileNameChars(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var invalidChars = System.IO.Path.GetInvalidFileNameChars();
            return input.IndexOfAny(invalidChars) >= 0;
        }

        /// <summary>
        /// 将字符串中的换行符规范化为指定的换行符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="newLine">新的换行符（默认为环境的换行符）</param>
        /// <returns>规范化后的字符串</returns>
        public static string NormalizeLineEndings(string input, string newLine = null)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 如果未指定新的换行符，则使用环境的换行符
            newLine = newLine ?? Environment.NewLine;

            // 首先将所有换行符转换为单个\n
            var normalized = input.Replace("\r\n", "\n").Replace("\r", "\n");

            // 然后将所有\n转换为指定的换行符
            if (newLine != "\n")
            {
                normalized = normalized.Replace("\n", newLine);
            }

            return normalized;
        }

        /// <summary>
        /// 提取字符串中指定前缀和后缀之间的内容
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns>提取到的内容，如果未找到则返回空字符串</returns>
        public static string ExtractBetween(string input, string prefix, string suffix)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(suffix))
                return string.Empty;

            var startIndex = input.IndexOf(prefix);
            if (startIndex < 0)
                return string.Empty;

            startIndex += prefix.Length;
            var endIndex = input.IndexOf(suffix, startIndex);
            if (endIndex < 0)
                return string.Empty;

            return input.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 提取字符串中所有指定前缀和后缀之间的内容
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns>提取到的内容列表</returns>
        public static List<string> ExtractAllBetween(string input, string prefix, string suffix)
        {
            var results = new List<string>();

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(suffix))
                return results;

            var currentIndex = 0;
            while (currentIndex < input.Length)
            {
                var startIndex = input.IndexOf(prefix, currentIndex);
                if (startIndex < 0)
                    break;

                startIndex += prefix.Length;
                var endIndex = input.IndexOf(suffix, startIndex);
                if (endIndex < 0)
                    break;

                results.Add(input.Substring(startIndex, endIndex - startIndex));
                currentIndex = endIndex + suffix.Length;
            }

            return results;
        }
    }
}
