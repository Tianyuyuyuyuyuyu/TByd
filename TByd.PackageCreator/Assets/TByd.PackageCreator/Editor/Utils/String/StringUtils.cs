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
            string[] words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 第一个单词首字母小写，其他单词首字母大写
            StringBuilder sb = new StringBuilder();
            sb.Append(words[0].ToLowerInvariant());

            for (int i = 1; i < words.Length; i++)
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
            string[] words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 所有单词首字母大写
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
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
            string[] words = SplitIntoWords(input);

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
            string[] words = SplitIntoWords(input);

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
        /// 截断字符串到指定长度，并添加省略号（如果需要）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="ellipsis">省略号字符串</param>
        /// <returns>截断后的字符串</returns>
        public static string Truncate(string input, int maxLength, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;

            int truncateLength = maxLength - ellipsis.Length;
            if (truncateLength <= 0)
                return ellipsis.Substring(0, maxLength);

            return input.Substring(0, truncateLength) + ellipsis;
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

            StringBuilder sb = new StringBuilder(template);
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

            StringBuilder sb = new StringBuilder(input);
            foreach (var replacement in replacements)
            {
                sb.Replace(replacement.Key, replacement.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 检查字符串是否为有效的标识符（只包含字母、数字和下划线，且不以数字开头）
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
            for (int i = 1; i < input.Length; i++)
            {
                if (!char.IsLetterOrDigit(input[i]) && input[i] != '_')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 将字符串转换为有效的标识符（替换无效字符为下划线，确保第一个字符不是数字）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>有效的标识符</returns>
        public static string ToValidIdentifier(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "_";

            StringBuilder sb = new StringBuilder();

            // 处理第一个字符
            if (char.IsDigit(input[0]))
            {
                sb.Append('_');
            }
            else if (char.IsLetter(input[0]) || input[0] == '_')
            {
                sb.Append(input[0]);
            }
            else
            {
                sb.Append('_');
            }

            // 处理其余字符
            for (int i = 1; i < input.Length; i++)
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

            return sb.ToString();
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
            string result = input.Replace('-', '.')
                               .Replace('_', '.')
                               .Replace(' ', '.');

            // 多个连续点替换为单个点
            result = Regex.Replace(result, "\\.+", ".");

            // 分割成命名空间部分
            string[] parts = result.Split('.');
            List<string> validParts = new List<string>();

            foreach (string part in parts)
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
            string[] words = SplitIntoWords(input);

            if (words.Length == 0)
                return input;

            // 将每个单词转换为标题形式
            for (int i = 0; i < words.Length; i++)
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
            for (int i = 0; i < length; i++)
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
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 转换为十六进制字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
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

            char[] invalidChars = System.IO.Path.GetInvalidPathChars();
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

            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
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
            string normalized = input.Replace("\r\n", "\n").Replace("\r", "\n");

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

            int startIndex = input.IndexOf(prefix);
            if (startIndex < 0)
                return string.Empty;

            startIndex += prefix.Length;
            int endIndex = input.IndexOf(suffix, startIndex);
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
            List<string> results = new List<string>();

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(suffix))
                return results;

            int currentIndex = 0;
            while (currentIndex < input.Length)
            {
                int startIndex = input.IndexOf(prefix, currentIndex);
                if (startIndex < 0)
                    break;

                startIndex += prefix.Length;
                int endIndex = input.IndexOf(suffix, startIndex);
                if (endIndex < 0)
                    break;

                results.Add(input.Substring(startIndex, endIndex - startIndex));
                currentIndex = endIndex + suffix.Length;
            }

            return results;
        }
    }
}
