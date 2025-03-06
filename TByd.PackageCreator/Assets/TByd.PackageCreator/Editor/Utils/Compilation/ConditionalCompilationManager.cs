using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Utils
{
    /// <summary>
    /// 条件编译指令管理系统，用于管理和操作条件编译符号
    /// </summary>
    public static class ConditionalCompilationManager
    {
        /// <summary>
        /// 获取当前构建目标的条件编译符号
        /// </summary>
        /// <returns>条件编译符号列表</returns>
        public static HashSet<string> GetDefineSymbols()
        {
            BuildTargetGroup targetGroup = GetCurrentBuildTargetGroup();
            string defineSymbolsStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            return new HashSet<string>(defineSymbolsStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// 获取特定构建目标的条件编译符号
        /// </summary>
        /// <param name="targetGroup">构建目标组</param>
        /// <returns>条件编译符号列表</returns>
        public static HashSet<string> GetDefineSymbols(BuildTargetGroup targetGroup)
        {
            string defineSymbolsStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            return new HashSet<string>(defineSymbolsStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// 添加条件编译符号到当前构建目标
        /// </summary>
        /// <param name="symbol">要添加的符号</param>
        /// <returns>是否成功添加</returns>
        public static bool AddDefineSymbol(string symbol)
        {
            return AddDefineSymbols(new[] { symbol });
        }

        /// <summary>
        /// 添加多个条件编译符号到当前构建目标
        /// </summary>
        /// <param name="symbols">要添加的符号数组</param>
        /// <returns>是否成功添加</returns>
        public static bool AddDefineSymbols(string[] symbols)
        {
            try
            {
                if (symbols == null || symbols.Length == 0)
                    return false;

                // 验证符号有效性
                foreach (string symbol in symbols)
                {
                    if (!IsValidDefineSymbol(symbol))
                    {
                        Debug.LogError($"无效的条件编译符号: {symbol}");
                        return false;
                    }
                }

                BuildTargetGroup targetGroup = GetCurrentBuildTargetGroup();
                HashSet<string> currentSymbols = GetDefineSymbols(targetGroup);
                bool changed = false;

                foreach (string symbol in symbols)
                {
                    if (currentSymbols.Add(symbol)) // 如果添加成功，表示之前不存在
                    {
                        changed = true;
                        Debug.Log($"添加条件编译符号: {symbol}");
                    }
                }

                if (changed)
                {
                    SetDefineSymbols(currentSymbols, targetGroup);
                    return true;
                }

                return false; // 没有变化
            }
            catch (Exception ex)
            {
                Debug.LogError($"添加条件编译符号时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 移除条件编译符号从当前构建目标
        /// </summary>
        /// <param name="symbol">要移除的符号</param>
        /// <returns>是否成功移除</returns>
        public static bool RemoveDefineSymbol(string symbol)
        {
            return RemoveDefineSymbols(new[] { symbol });
        }

        /// <summary>
        /// 移除多个条件编译符号从当前构建目标
        /// </summary>
        /// <param name="symbols">要移除的符号数组</param>
        /// <returns>是否成功移除</returns>
        public static bool RemoveDefineSymbols(string[] symbols)
        {
            try
            {
                if (symbols == null || symbols.Length == 0)
                    return false;

                BuildTargetGroup targetGroup = GetCurrentBuildTargetGroup();
                HashSet<string> currentSymbols = GetDefineSymbols(targetGroup);
                bool changed = false;

                foreach (string symbol in symbols)
                {
                    if (currentSymbols.Remove(symbol)) // 如果移除成功，表示之前存在
                    {
                        changed = true;
                        Debug.Log($"移除条件编译符号: {symbol}");
                    }
                }

                if (changed)
                {
                    SetDefineSymbols(currentSymbols, targetGroup);
                    return true;
                }

                return false; // 没有变化
            }
            catch (Exception ex)
            {
                Debug.LogError($"移除条件编译符号时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查当前构建目标是否包含指定的条件编译符号
        /// </summary>
        /// <param name="symbol">要检查的符号</param>
        /// <returns>如果包含则返回true</returns>
        public static bool HasDefineSymbol(string symbol)
        {
            try
            {
                if (string.IsNullOrEmpty(symbol))
                    return false;

                HashSet<string> currentSymbols = GetDefineSymbols();
                return currentSymbols.Contains(symbol);
            }
            catch (Exception ex)
            {
                Debug.LogError($"检查条件编译符号时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 为所有构建目标添加条件编译符号
        /// </summary>
        /// <param name="symbol">要添加的符号</param>
        /// <returns>是否成功添加</returns>
        public static bool AddDefineSymbolToAllTargets(string symbol)
        {
            return AddDefineSymbolsToAllTargets(new[] { symbol });
        }

        /// <summary>
        /// 为所有构建目标添加多个条件编译符号
        /// </summary>
        /// <param name="symbols">要添加的符号数组</param>
        /// <returns>是否成功添加</returns>
        public static bool AddDefineSymbolsToAllTargets(string[] symbols)
        {
            try
            {
                if (symbols == null || symbols.Length == 0)
                    return false;

                // 验证符号有效性
                foreach (string symbol in symbols)
                {
                    if (!IsValidDefineSymbol(symbol))
                    {
                        Debug.LogError($"无效的条件编译符号: {symbol}");
                        return false;
                    }
                }

                bool anyChanged = false;
                foreach (BuildTargetGroup targetGroup in GetSupportedBuildTargetGroups())
                {
                    HashSet<string> currentSymbols = GetDefineSymbols(targetGroup);
                    bool changed = false;

                    foreach (string symbol in symbols)
                    {
                        if (currentSymbols.Add(symbol)) // 如果添加成功，表示之前不存在
                        {
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        SetDefineSymbols(currentSymbols, targetGroup);
                        anyChanged = true;
                    }
                }

                if (anyChanged)
                {
                    Debug.Log($"向所有构建目标添加条件编译符号: {string.Join(", ", symbols)}");
                }

                return anyChanged;
            }
            catch (Exception ex)
            {
                Debug.LogError($"向所有构建目标添加条件编译符号时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 从所有构建目标移除条件编译符号
        /// </summary>
        /// <param name="symbol">要移除的符号</param>
        /// <returns>是否成功移除</returns>
        public static bool RemoveDefineSymbolFromAllTargets(string symbol)
        {
            return RemoveDefineSymbolsFromAllTargets(new[] { symbol });
        }

        /// <summary>
        /// 从所有构建目标移除多个条件编译符号
        /// </summary>
        /// <param name="symbols">要移除的符号数组</param>
        /// <returns>是否成功移除</returns>
        public static bool RemoveDefineSymbolsFromAllTargets(string[] symbols)
        {
            try
            {
                if (symbols == null || symbols.Length == 0)
                    return false;

                bool anyChanged = false;
                foreach (BuildTargetGroup targetGroup in GetSupportedBuildTargetGroups())
                {
                    HashSet<string> currentSymbols = GetDefineSymbols(targetGroup);
                    bool changed = false;

                    foreach (string symbol in symbols)
                    {
                        if (currentSymbols.Remove(symbol)) // 如果移除成功，表示之前存在
                        {
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        SetDefineSymbols(currentSymbols, targetGroup);
                        anyChanged = true;
                    }
                }

                if (anyChanged)
                {
                    Debug.Log($"从所有构建目标移除条件编译符号: {string.Join(", ", symbols)}");
                }

                return anyChanged;
            }
            catch (Exception ex)
            {
                Debug.LogError($"从所有构建目标移除条件编译符号时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置条件编译符号（替换现有符号）
        /// </summary>
        /// <param name="symbols">符号集合</param>
        /// <param name="targetGroup">目标构建组</param>
        private static void SetDefineSymbols(HashSet<string> symbols, BuildTargetGroup targetGroup)
        {
            string defineSymbolsStr = string.Join(";", symbols);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defineSymbolsStr);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// 获取当前活动的构建目标组
        /// </summary>
        /// <returns>当前构建目标组</returns>
        private static BuildTargetGroup GetCurrentBuildTargetGroup()
        {
            return BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
        }

        /// <summary>
        /// 获取支持的构建目标组列表
        /// </summary>
        /// <returns>构建目标组数组</returns>
        private static BuildTargetGroup[] GetSupportedBuildTargetGroups()
        {
            // 只返回常用的构建目标组，避免使用已废弃的目标组
            return new BuildTargetGroup[]
            {
                BuildTargetGroup.Standalone,
                BuildTargetGroup.iOS,
                BuildTargetGroup.Android,
                BuildTargetGroup.WebGL,
                BuildTargetGroup.WSA
            };
        }

        /// <summary>
        /// 检查符号是否是有效的条件编译符号
        /// </summary>
        /// <param name="symbol">要检查的符号</param>
        /// <returns>如果有效则返回true</returns>
        private static bool IsValidDefineSymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return false;

            // 条件编译符号必须遵循C#标识符规则
            // 首字符必须是字母或下划线，后续可以包含字母、数字或下划线
            Regex validSymbolRegex = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*$");
            return validSymbolRegex.IsMatch(symbol);
        }

        /// <summary>
        /// 检查指定的代码文件是否包含特定条件编译区域
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="symbol">条件编译符号</param>
        /// <returns>如果包含则返回true</returns>
        public static bool FileHasConditionalRegion(string filePath, string symbol)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                    return false;

                string content = System.IO.File.ReadAllText(filePath);

                // 检查 #if SYMBOL 形式
                if (content.Contains($"#if {symbol}") || content.Contains($"#elif {symbol}"))
                    return true;

                // 检查 #if !SYMBOL 形式
                if (content.Contains($"#if !{symbol}") || content.Contains($"#elif !{symbol}"))
                    return true;

                // 检查复杂条件中的符号，如 #if (SYMBOL && OTHER) || ANOTHER
                // 这里简化处理，检查是否在 #if 或 #elif 后的同一行中包含该符号
                string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if ((trimmedLine.StartsWith("#if ") || trimmedLine.StartsWith("#elif ")) &&
                        trimmedLine.Contains(symbol))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.LogError($"检查文件条件编译区域时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 在解决方案中搜索使用了特定条件编译符号的文件
        /// </summary>
        /// <param name="symbol">条件编译符号</param>
        /// <param name="searchPath">搜索路径，默认为Assets文件夹</param>
        /// <returns>使用了该符号的文件路径列表</returns>
        public static List<string> FindFilesUsingSymbol(string symbol, string searchPath = "Assets")
        {
            try
            {
                List<string> results = new List<string>();
                if (string.IsNullOrEmpty(symbol))
                    return results;

                // 查找所有C#脚本
                string[] csharpFiles = System.IO.Directory.GetFiles(searchPath, "*.cs", System.IO.SearchOption.AllDirectories);

                foreach (string file in csharpFiles)
                {
                    if (FileHasConditionalRegion(file, symbol))
                    {
                        results.Add(file);
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                Debug.LogError($"搜索使用条件编译符号的文件时出错: {ex.Message}");
                return new List<string>();
            }
        }

        /// <summary>
        /// 读取项目的自定义条件编译符号配置文件
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <returns>定义的符号集合</returns>
        public static HashSet<string> ReadSymbolsFromConfigFile(string configPath = "Assets/TByd.PackageCreator/PackageResources/Config/DefineSymbols.txt")
        {
            try
            {
                HashSet<string> symbols = new HashSet<string>();

                if (!System.IO.File.Exists(configPath))
                {
                    Debug.LogWarning($"条件编译符号配置文件不存在: {configPath}");
                    return symbols;
                }

                string[] lines = System.IO.File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    // 跳过空行和注释行
                    if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("//") || trimmedLine.StartsWith("#"))
                        continue;

                    // 处理格式为: SYMBOL // 描述 的行
                    int commentIndex = trimmedLine.IndexOf("//");
                    if (commentIndex > 0)
                    {
                        trimmedLine = trimmedLine.Substring(0, commentIndex).Trim();
                    }

                    if (IsValidDefineSymbol(trimmedLine))
                    {
                        symbols.Add(trimmedLine);
                    }
                    else
                    {
                        Debug.LogWarning($"配置文件中存在无效的条件编译符号: {trimmedLine}");
                    }
                }

                return symbols;
            }
            catch (Exception ex)
            {
                Debug.LogError($"读取条件编译符号配置文件时出错: {ex.Message}");
                return new HashSet<string>();
            }
        }

        /// <summary>
        /// 应用配置文件中定义的所有条件编译符号
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <returns>是否成功应用</returns>
        public static bool ApplySymbolsFromConfigFile(string configPath = "Assets/TByd.PackageCreator/PackageResources/Config/DefineSymbols.txt")
        {
            try
            {
                HashSet<string> symbols = ReadSymbolsFromConfigFile(configPath);
                if (symbols.Count == 0)
                {
                    Debug.LogWarning("配置文件中未找到有效的条件编译符号");
                    return false;
                }

                return AddDefineSymbolsToAllTargets(symbols.ToArray());
            }
            catch (Exception ex)
            {
                Debug.LogError($"应用条件编译符号配置时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 使用指定的条件编译符号生成新的配置文件
        /// </summary>
        /// <param name="symbols">条件编译符号集合</param>
        /// <param name="configPath">配置文件路径</param>
        /// <returns>是否成功生成</returns>
        public static bool GenerateConfigFile(IEnumerable<string> symbols, string configPath = "Assets/TByd.PackageCreator/PackageResources/Config/DefineSymbols.txt")
        {
            try
            {
                if (symbols == null || !symbols.Any())
                {
                    Debug.LogWarning("没有符号可以写入配置文件");
                    return false;
                }

                // 确保目录存在
                string directory = System.IO.Path.GetDirectoryName(configPath);
                if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                // 生成配置文件内容
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("// TByd.PackageCreator 条件编译符号配置文件");
                sb.AppendLine("// 每行一个符号，可以添加注释");
                sb.AppendLine();

                foreach (string symbol in symbols.OrderBy(s => s))
                {
                    if (IsValidDefineSymbol(symbol))
                    {
                        sb.AppendLine(symbol);
                    }
                    else
                    {
                        Debug.LogWarning($"跳过无效的条件编译符号: {symbol}");
                    }
                }

                // 写入文件
                System.IO.File.WriteAllText(configPath, sb.ToString());

                Debug.Log($"成功生成条件编译符号配置文件: {configPath}");
                AssetDatabase.Refresh();

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"生成条件编译符号配置文件时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 导出当前所有平台的条件编译符号到配置文件
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <returns>是否成功导出</returns>
        public static bool ExportCurrentSymbolsToConfigFile(string configPath = "Assets/TByd.PackageCreator/PackageResources/Config/DefineSymbols.txt")
        {
            try
            {
                HashSet<string> allSymbols = new HashSet<string>();

                foreach (BuildTargetGroup targetGroup in GetSupportedBuildTargetGroups())
                {
                    HashSet<string> symbols = GetDefineSymbols(targetGroup);
                    foreach (string symbol in symbols)
                    {
                        allSymbols.Add(symbol);
                    }
                }

                return GenerateConfigFile(allSymbols, configPath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"导出当前条件编译符号时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取项目中所有使用的条件编译符号
        /// </summary>
        /// <param name="searchPath">搜索路径，默认为Assets文件夹</param>
        /// <returns>条件编译符号集合</returns>
        public static HashSet<string> ScanProjectForSymbols(string searchPath = "Assets")
        {
            try
            {
                HashSet<string> symbols = new HashSet<string>();

                // 查找所有C#脚本
                string[] csharpFiles = System.IO.Directory.GetFiles(searchPath, "*.cs", System.IO.SearchOption.AllDirectories);

                // 匹配条件编译指令中的符号
                Regex ifRegex = new Regex(@"#if\s+(!?\s*[A-Za-z_][A-Za-z0-9_]*|.*&&.*|\|\|.*)");
                Regex elifRegex = new Regex(@"#elif\s+(!?\s*[A-Za-z_][A-Za-z0-9_]*|.*&&.*|\|\|.*)");
                Regex symbolRegex = new Regex(@"[A-Za-z_][A-Za-z0-9_]*");

                foreach (string file in csharpFiles)
                {
                    string content = System.IO.File.ReadAllText(file);

                    // 查找所有 #if 和 #elif 指令
                    MatchCollection ifMatches = ifRegex.Matches(content);
                    MatchCollection elifMatches = elifRegex.Matches(content);

                    // 处理 #if 指令
                    foreach (Match match in ifMatches)
                    {
                        string condition = match.Groups[1].Value.Trim();
                        ExtractSymbolsFromCondition(condition, symbols);
                    }

                    // 处理 #elif 指令
                    foreach (Match match in elifMatches)
                    {
                        string condition = match.Groups[1].Value.Trim();
                        ExtractSymbolsFromCondition(condition, symbols);
                    }
                }

                return symbols;
            }
            catch (Exception ex)
            {
                Debug.LogError($"扫描项目条件编译符号时出错: {ex.Message}");
                return new HashSet<string>();
            }
        }

        /// <summary>
        /// 从条件表达式中提取条件编译符号
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="symbols">符号集合</param>
        private static void ExtractSymbolsFromCondition(string condition, HashSet<string> symbols)
        {
            try
            {
                // 移除所有!符号和括号，以便更容易地提取符号
                condition = condition.Replace("!", " ").Replace("(", " ").Replace(")", " ");

                // 分割复合条件
                string[] parts = condition.Split(new[] { "&&", "||" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    string trimmedPart = part.Trim();

                    // 如果是有效的符号，直接添加
                    if (IsValidDefineSymbol(trimmedPart))
                    {
                        symbols.Add(trimmedPart);
                        continue;
                    }

                    // 否则尝试从部分中提取有效符号
                    Regex symbolRegex = new Regex(@"[A-Za-z_][A-Za-z0-9_]*");
                    MatchCollection matches = symbolRegex.Matches(trimmedPart);

                    foreach (Match symbolMatch in matches)
                    {
                        string symbol = symbolMatch.Value;

                        // 排除C#关键字
                        if (!IsCSharpKeyword(symbol) && IsValidDefineSymbol(symbol))
                        {
                            symbols.Add(symbol);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"从条件表达式提取符号时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 检查字符串是否是C#关键字
        /// </summary>
        /// <param name="word">要检查的字符串</param>
        /// <returns>如果是C#关键字则返回true</returns>
        private static bool IsCSharpKeyword(string word)
        {
            // C#关键字列表
            HashSet<string> keywords = new HashSet<string>
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

            return keywords.Contains(word);
        }
    }
}
