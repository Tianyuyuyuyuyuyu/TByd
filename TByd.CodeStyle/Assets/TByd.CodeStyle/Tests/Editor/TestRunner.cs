using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// 测试运行器，用于在Unity编辑器中运行所有测试
    /// </summary>
    public static class TestRunner
    {
        /// <summary>
        /// 运行所有编辑器测试
        /// </summary>
        [MenuItem("TByd/CodeStyle/运行所有测试", false, 100)]
        public static void RunAllTests()
        {
            Debug.Log("开始运行所有TByd.CodeStyle测试...");

            // 创建测试运行器API
            var testRunnerApi = ScriptableObject.CreateInstance<TestRunnerApi>();

            // 创建测试运行器请求
            var request = new ExecutionSettings();
            request.filters = new[] { new Filter { testMode = TestMode.EditMode } };

            // 注册回调
            testRunnerApi.RegisterCallbacks(new TestCallbacks());

            // 执行测试
            testRunnerApi.Execute(request);
        }

        /// <summary>
        /// 获取测试总数
        /// </summary>
        /// <param name="_result">测试结果</param>
        /// <returns>测试总数</returns>
        private static int GetTestCount(this ITestResultAdaptor _result)
        {
            return _result.PassCount + _result.FailCount + _result.SkipCount + _result.InconclusiveCount;
        }

        /// <summary>
        /// 测试回调
        /// </summary>
        private class TestCallbacks : ICallbacks
        {
            public void RunStarted(ITestAdaptor testsToRun)
            {
                Debug.Log($"开始运行测试: {testsToRun.Name}");
            }

            public void RunFinished(ITestResultAdaptor result)
            {
                int testCount = result.GetTestCount();
                if (result.PassCount == testCount)
                {
                    Debug.Log($"所有测试通过! 通过: {result.PassCount}, 总计: {testCount}");
                }
                else
                {
                    Debug.LogWarning($"测试完成，但有失败项。通过: {result.PassCount}, 失败: {result.FailCount}, 总计: {testCount}");
                }
            }

            public void TestStarted(ITestAdaptor test)
            {
                if (!test.IsSuite)
                {
                    Debug.Log($"开始测试: {test.FullName}");
                }
            }

            public void TestFinished(ITestResultAdaptor result)
            {
                if (!result.Test.IsSuite)
                {
                    if (result.TestStatus == TestStatus.Passed)
                    {
                        Debug.Log($"测试通过: {result.Test.FullName}");
                    }
                    else
                    {
                        Debug.LogError($"测试失败: {result.Test.FullName}\n错误信息: {result.Message}\n堆栈跟踪: {result.StackTrace}");
                    }
                }
            }
        }
    }
}
