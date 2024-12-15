using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TByd.DirtyWord
{
    public enum LanguageType
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        CNS = 0,        //中文简体
        /// <summary>
        /// 中文繁体
        /// </summary>
        CNT = 1,        //中文繁体
        KOR = 2,        //韩文
        EN = 3,         //英语
        JP = 4,         //日语
        VN = 5,        //越南语
        TH = 6,        //泰语
    }
    public interface IDirtyWordContentProvider
    {
        LanguageType lang { get; }
        IEnumerable<string> contents { get;}
    }
    //有时候需要替换文本中的一些特殊符号等再进行检测
    public interface IDirtyWordReplace
    {
        string GetReplaceContent(string content);
    }
    public static class DirtyWordHelper
    {
        private static readonly Dictionary<LanguageType, DirtyWordNode> _LANG_NODES = new Dictionary<LanguageType, DirtyWordNode>();

        private static readonly DirtyWordNode _ALL = new DirtyWordNode();

        private static LanguageType _CURRENT=LanguageType.CNS;
        static IDirtyWordReplace _replacer;
        static DirtyWordHelper()
        {
        }
        public static void Initialize(IDirtyWordContentProvider provider, IDirtyWordReplace replacer=null)
        {
            ChangeCurrentLanguage(provider.lang);
            _InitializeContent(provider.contents);
            _replacer = replacer;
        }

        public static void ChangeCurrentLanguage(LanguageType type)
        {

        }

        public static void _InitializeContent(IEnumerable<string> contents)
        {
            _LANG_NODES.Clear();
            _LANG_NODES.Add(_CURRENT, new DirtyWordNode());
            foreach (var content in contents)
            {
                var dbc_word = _ToDBC(content);
                if (dbc_word.Length <= 0)
                {
                    continue;
                }
                var str = dbc_word.ToString();
                _LANG_NODES[_CURRENT].Add(str);
                _ALL.Add(str);

            }
        }
        public static void AddDirtyWord(string content)
        {
            _LANG_NODES[_CURRENT].Add(content);
        }
        public static void RemoveDirtyWord(string content)
        {
            _LANG_NODES[_CURRENT].Remove(content);
        }

        public static bool IsDirtyInAll(string source_text, int filter_num = 0, bool only_check_content_again = false)
        {
            _ThorwIfNotInit();

            return _ALL._IsDirty(source_text, only_check_content_again, filter_num);
        }
        /// <summary>
        /// 原文不是敏感词时，再次检测只检测各国语言，排除特殊字符和数字
        /// </summary>
        /// <param name="source_text"></param>
        /// <param name="only_check_content_again"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDirtyCarefullyCheck(string source_text)
        {
            _ThorwIfNotInit();
            if (null != _replacer)
            {
                source_text = _replacer.GetReplaceContent(source_text);
            }
            bool ori_dirty = IsDirty(_CURRENT, source_text, 0);
            if (ori_dirty)
            {
                return true;
            }
            else
            {
                return IsDirty(_CURRENT, source_text, 0, true);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDirty(string source_text, int filter_num = 0)
        {
            _ThorwIfNotInit();
            return IsDirty(_CURRENT, source_text, filter_num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDirty(LanguageType language, string source_text, int filter_num = 0, bool only_check_content_again = false)
        {
            _ThorwIfNotInit();
            _LANG_NODES.TryGetValue(language, out var node);
            
            return node?._IsDirty(source_text, only_check_content_again,filter_num) ?? IsDirtyInAll(source_text, filter_num, only_check_content_again);
        }

        private static bool _IsDirty(this DirtyWordNode node, string source_text, bool only_check_content_again, int filter_num = 0)
        {
            if(string.IsNullOrEmpty(source_text))
            {
                return false;
            }

            var source_dbc_text = _ToDBC(source_text);
            for(int i = 0; i < source_dbc_text.Length; i++)
            {
                int bad_word_len;
                if(filter_num > 0 && _IsNum(source_dbc_text[i]))
                {
                    bad_word_len = _CheckNumberSeq(source_dbc_text, i, filter_num);
                    if(bad_word_len > 0)
                    {
                        return true;
                    }
                }

                //查询以该字为首字符的词组  
                bad_word_len = node._Check(source_dbc_text, i, only_check_content_again);
                if(bad_word_len > 0)
                {
                    return true;
                }
            }
            return false;
        }


        public static string ReplaceInAll(string source_text,
                                          char   replace_char,
                                          int    filter_num  = 0,
                                          string num_replace = null)
        {
            _ThorwIfNotInit();

            foreach(var node in _LANG_NODES.Values)
            {
                var result = node._Replace(
                    source_text,
                    replace_char,
                    filter_num,
                    num_replace
                );

                if(!string.Equals(result, source_text))
                {
                    return result;
                }
            }

            return source_text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Replace(string source_text,
                                     char   replace_char,
                                     int    filter_num  = 0,
                                     string num_replace = null)
        {
            _ThorwIfNotInit();
            return _LANG_NODES[_CURRENT]?.
                   _Replace(
                       source_text,
                       replace_char,
                       filter_num,
                       num_replace
                   ) ??
                   source_text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Replace(LanguageType language,
                                     string         source_text,
                                     char           replace_char,
                                     int            filter_num  = 0,
                                     string         num_replace = null)
        {
            _ThorwIfNotInit();
            return _LANG_NODES[language]?.
                   _Replace(
                       source_text,
                       replace_char,
                       filter_num,
                       num_replace
                   ) ??
                   source_text;
        }

        private static string _Replace(this DirtyWordNode node,
                                       string             source_text,
                                       char               replace_char,
                                       int                filter_num  = 0,
                                       string             num_replace = null)
        {
            if(string.IsNullOrEmpty(source_text))
            {
                return string.Empty;
            }

            var source_dbc_text = _ToDBC(source_text);

            char[] temp_string = source_text.ToCharArray();

            var replace_list = new List<DirtyWordRNode>();
            for(int i = 0; i < source_dbc_text.Length; i++)
            {
                int bad_word_len;
                if(filter_num > 0 && _IsNum(source_dbc_text[i]))
                {
                    bad_word_len = _CheckNumberSeq(source_dbc_text, i, filter_num);
                    if(bad_word_len > 0)
                    {
                        bad_word_len += 1;
                        if(num_replace == null)
                        {
                            for(int pos = 0; pos < bad_word_len; pos++)
                            {
                                temp_string[pos + i] = replace_char;
                            }
                        }
                        else
                        {
                            replace_list.Add(
                                new DirtyWordRNode {start = i, len = bad_word_len, type = DirtyWordType.IndexReplace}
                            );
                        }

                        i = i + bad_word_len - 1;
                        continue;
                    }
                }

                //查询以该字为首字符的词组  
                bad_word_len = node._Check(source_dbc_text, i);
                if(bad_word_len > 0)
                {
                    for(int pos = 0; pos < bad_word_len; pos++)
                    {
                        temp_string[pos + i] = replace_char;
                    }

                    i = i + bad_word_len - 1;
                }
            }

            string result;
            if(replace_list.Count > 0)
            {
                result = _ReplaceString(
                    temp_string,
                    replace_list,
                    null,
                    num_replace
                );
            }
            else
            {
                result = new string(temp_string);
            }

            return result;
        }

        public static string ReplaceInAll(string source_text,
                                          string replace_str,
                                          int    filter_num  = 0,
                                          string num_replace = null)
        {
            _ThorwIfNotInit();

            foreach(var node in _LANG_NODES.Values)
            {
                var result = node._Replace(
                    source_text,
                    replace_str,
                    filter_num,
                    num_replace
                );

                if(!string.Equals(result, source_text))
                {
                    return result;
                }
            }

            return source_text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Replace(string source_text,
                                     string replace_str,
                                     int    filter_num  = 0,
                                     string num_replace = null)
        {
            _ThorwIfNotInit();

            return _LANG_NODES[_CURRENT]?.
                   _Replace(
                       source_text,
                       replace_str,
                       filter_num,
                       num_replace
                   ) ??
                   source_text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Replace(LanguageType language,
                                     string         source_text,
                                     string         replace_str,
                                     int            filter_num  = 0,
                                     string         num_replace = null)
        {
            _ThorwIfNotInit();

            return _LANG_NODES[language]?.
                   _Replace(
                       source_text,
                       replace_str,
                       filter_num,
                       num_replace
                   ) ??
                   source_text;
        }

        private static string _Replace(this DirtyWordNode node,
                                       string             source_text,
                                       string             replace_str,
                                       int                filter_num  = 0,
                                       string             num_replace = null)
        {
            if(string.IsNullOrEmpty(source_text))
            {
                return string.Empty;
            }

            var source_dbc_text = _ToDBC(source_text);

            var replace_list = new List<DirtyWordRNode>();

            if(filter_num > 0 && num_replace == null)
            {
                num_replace = replace_str;
            }

            for(int i = 0; i < source_dbc_text.Length; i++)
            {
                int bad_word_len;
                if(filter_num > 0 && _IsNum(source_dbc_text[i]))
                {
                    bad_word_len = _CheckNumberSeq(source_dbc_text, i, filter_num);
                    if(bad_word_len > 0)
                    {
                        bad_word_len += 1;
                        int start = i;
                        replace_list.Add(
                            new DirtyWordRNode {start = start, len = bad_word_len, type = DirtyWordType.IndexReplace}
                        );
                        i = i + bad_word_len - 1;
                        continue;
                    }
                }

                bad_word_len = node._Check(source_dbc_text, i);

                if(bad_word_len <= 0)
                {
                    continue;
                }

                replace_list.Add(new DirtyWordRNode {start = i, len = bad_word_len, type = DirtyWordType.StrReplace});

                i = i + bad_word_len - 1;
            }

            string temp_str = _ReplaceString(
                source_text.ToCharArray(),
                replace_list,
                replace_str,
                num_replace
            );

            return temp_str;
        }

        private static string _ReplaceString(IEnumerable<char>             char_array,
                                             List<DirtyWordRNode> nodes,
                                             string                        replace_str,
                                             string                        num_replace)
        {
            num_replace ??= replace_str;

            replace_str ??= num_replace;

             var char_list =new List<char>(char_array);
            int offset = 0;
            for(int i = 0, i_max = nodes.Count; i < i_max; i++)
            {
                int    start     = nodes[i].start + offset;
                int    len       = nodes[i].len;
                string str       = nodes[i].type == 0 ? replace_str : num_replace;
                int    end_index = start + len - 1;

                if(str.Length < len)
                {
                    char_list.RemoveRange(start, len - str.Length);
                }

                for(int j = 0, j_max = str.Length; j < j_max; j++)
                {
                    char ch    = str[j];
                    int  index = start + j;


                    if(index <= end_index)
                    {
                        char_list[index] = ch;
                    }
                    else
                    {
                        char_list.Insert(index, ch);
                    }
                }

                offset += str.Length - len;
            }

            return new string(char_list.ToArray());
        }

        private static int _Check(this DirtyWordNode node, ReadOnlySpan<char> source_text, int cursor,bool only_check_content_again=false)
        {
            Func<char, bool> check_special_sym = null;
            if (only_check_content_again)
            {
                check_special_sym = _CheckNumAndSpecialSym;
            }
            else
            {
                check_special_sym = _CheckSpecialSym;
            }
            int endsor      = node.CheckAndGetEndIndex(source_text, cursor, check_special_sym);
            int word_length = endsor >= cursor ? endsor - cursor + 1 : 0;
            return word_length;
        }

  

        private static int _CheckNumberSeq(ReadOnlySpan<char> source_text, int cursor, int filter_num)
        {
            int count  = 0;
            int offset = 0;
            if(cursor + 1 >= source_text.Length)
            {
                return 0;
            }

            //检测下位字符如果不是汉字 数字 字符 偏移量加1  
            for(int i = cursor + 1; i < source_text.Length; i++)
            {
                if(!_IsNum(source_text[i]))
                {
                    break;
                }

                count++;
            }

            if(count + 1 >= filter_num)
            {
                int word_length = count + offset;
                return word_length;
            }

            return 0;
        }

        #region helper

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Conditional("DEBUG")]
        private static void _ThorwIfNotInit()
        {
            if(_LANG_NODES is null || _LANG_NODES.Count <= 0)
            {
                throw new Exception("Init first!");
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _CheckNumAndSpecialSym(char character)
        {
            return !_IsValidI18NChar(character) && !_IsAlphabet(character);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _CheckSpecialSym(char character)
        {
            return!_IsValidI18NChar(character) && !_IsNum(character) && !_IsAlphabet(character);
        }

        /// <summary>
        /// 是否为各国字符
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _IsValidI18NChar(char character)
        {
            int char_val = character;

            // 参考链接 https://unicode-table.com/cn/alphabets/
            return
                // 中文
                    (char_val >= 0x4e00 && char_val <= 0x9fa5)
                    // 日文
                    || (char_val >= 0x0800 && char_val <= 0x4e00)
                    // 韩文
                    ||(char_val >= 0xac00 && char_val <= 0xd7ff)
                    // 泰语 & 越南
                    ||( char_val >= 0x0e00 && char_val <= 0x0e4f)
                    // 俄语
                    ||(char_val >= 0x0410 && char_val <= 0x044f);
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _IsNum(char character)
        {
            int char_val = character;
            return char_val >= 48 && char_val <= 57;
        }

        /// <summary>
        /// 是否为英文字母
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _IsAlphabet(char character)
        {
            int char_val = character;
            return (char_val >= 97 && char_val <= 122) ||(char_val >= 65 && char_val <= 90);
        }


        /// <summary>
        /// 转半角小写的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        private static ReadOnlySpan<char> _ToDBC(string input) { return _ToDBC(input.AsSpan()); }

        private static ReadOnlySpan<char> _ToDBC(char[] c) { return _ToDBC(c.AsSpan()); }

        private static ReadOnlySpan<char> _ToDBC(ReadOnlySpan<char> input)
        {
            Span<char> c = new char[input.Length];
            input.CopyTo(c);

            for(int i = 0; i < c.Length; i++)
            {
                if(c[i] == 12288)
                {
                    c[i] = (char) 32;
                    continue;
                }

                if(c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char) (c[i] - 65248);
                    continue;
                }

                if(char.IsUpper(c[i]))
                {
                    c[i] = char.ToLowerInvariant(c[i]);
                }
            }

            return c;
        }

        #endregion
    }
}
