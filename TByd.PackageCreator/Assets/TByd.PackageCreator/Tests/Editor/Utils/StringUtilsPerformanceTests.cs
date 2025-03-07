using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Utils.String;
using Debug = UnityEngine.Debug;

namespace TByd.PackageCreator.Tests.Editor.Utils
{
    /// <summary>
    /// StringUtils 工具类性能测试，验证优化效果
    /// </summary>
    public class StringUtilsPerformanceTests
    {
        private const int IterationCount = 1000; // 迭代次数
        private const int WarmupCount = 10; // 预热次数

        [Test]
        public void StringTruncate_PerformanceTest()
        {
            // 准备测试数据：中英文混合文本
            string chineseText = "这是一段中文测试文本，包含标点符号和数字123。测试文本需要足够长以便进行截断操作。";
            string englishText = "This is an English test text, containing punctuation and numbers 123. The test text needs to be long enough for truncation operations.";
            string mixedText = chineseText + englishText;

            // 预热
            for (int i = 0; i < WarmupCount; i++)
            {
                StringUtils.Truncate(mixedText, 20);
                StringUtils.TruncateByWidth(mixedText, 40);
            }

            // 测试Truncate性能
            Stopwatch swTruncate = new Stopwatch();
            swTruncate.Start();

            for (int i = 0; i < IterationCount; i++)
            {
                string result = StringUtils.Truncate(mixedText, 20);
            }

            swTruncate.Stop();
            double truncateTime = swTruncate.ElapsedMilliseconds;

            // 测试TruncateByWidth性能
            Stopwatch swTruncateByWidth = new Stopwatch();
            swTruncateByWidth.Start();

            for (int i = 0; i < IterationCount; i++)
            {
                string result = StringUtils.TruncateByWidth(mixedText, 40);
            }

            swTruncateByWidth.Stop();
            double truncateByWidthTime = swTruncateByWidth.ElapsedMilliseconds;

            // 输出性能数据
            Debug.Log($"Truncate ({IterationCount}次): {truncateTime}ms");
            Debug.Log($"TruncateByWidth ({IterationCount}次): {truncateByWidthTime}ms");
            Debug.Log($"性能差异: {truncateByWidthTime / Math.Max(1, truncateTime):F2}倍");

            // 不做断言，只是记录性能数据
        }

        [Test]
        public void StringIntern_PerformanceTest()
        {
            // 准备测试数据：多个重复字符串
            List<string> testStrings = new List<string>();
            for (int i = 0; i < IterationCount; i++)
            {
                // 每个字符串重复生成10次，总共100种不同的字符串
                testStrings.Add($"TestString_{i % 100}");
            }

            // 清空字符串池以确保测试准确性
            StringUtils.ClearStringPool();

            // 预热
            for (int i = 0; i < WarmupCount; i++)
            {
                string.Intern(testStrings[i]);
                StringUtils.Intern(testStrings[i]);
            }

            // 测试原生string.Intern性能
            Stopwatch swNativeIntern = new Stopwatch();
            swNativeIntern.Start();

            List<string> nativeInternResults = new List<string>(IterationCount);
            for (int i = 0; i < IterationCount; i++)
            {
                nativeInternResults.Add(string.Intern(testStrings[i]));
            }

            swNativeIntern.Stop();
            double nativeInternTime = swNativeIntern.ElapsedMilliseconds;

            // 测试自定义Intern性能
            StringUtils.ClearStringPool();
            Stopwatch swCustomIntern = new Stopwatch();
            swCustomIntern.Start();

            List<string> customInternResults = new List<string>(IterationCount);
            for (int i = 0; i < IterationCount; i++)
            {
                customInternResults.Add(StringUtils.Intern(testStrings[i]));
            }

            swCustomIntern.Stop();
            double customInternTime = swCustomIntern.ElapsedMilliseconds;

            // 输出性能数据
            Debug.Log($"Native string.Intern ({IterationCount}次): {nativeInternTime}ms");
            Debug.Log($"Custom StringUtils.Intern ({IterationCount}次): {customInternTime}ms");

            // 验证结果正确性：对于相同的输入字符串，池化后应返回相同的实例
            for (int i = 0; i < 100; i++)
            {
                int firstIndex = i;
                int secondIndex = i + 100;

                if (secondIndex < customInternResults.Count)
                {
                    // 如果是相同的输入字符串，池化后应该是同一个实例
                    Assert.AreSame(
                        customInternResults[firstIndex],
                        customInternResults[secondIndex],
                        $"相同的字符串 '{testStrings[firstIndex]}' 池化后应该返回相同实例"
                    );
                }
            }
        }
    }
}
