using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// 测试报告生成器，用于生成HTML格式的测试报告
    /// </summary>
    public static class TestReportGenerator
    {
        // 报告保存路径
        private const string c_ReportPath = "TestReports";

        // 报告文件名
        private const string c_ReportFileName = "TestReport_{0}.html";

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
        /// 获取测试状态
        /// </summary>
        /// <param name="_result">测试结果</param>
        /// <returns>测试状态</returns>
        private static TestStatus GetPassState(this ITestResultAdaptor _result)
        {
            if (_result.TestStatus == TestStatus.Passed)
                return TestStatus.Passed;
            else if (_result.TestStatus == TestStatus.Failed)
                return TestStatus.Failed;
            else
                return TestStatus.Skipped;
        }

        /// <summary>
        /// 生成测试报告
        /// </summary>
        [MenuItem("TByd/CodeStyle/生成测试报告", false, 101)]
        public static void GenerateTestReport()
        {
            Debug.Log("开始生成测试报告...");

            // 创建测试运行器API
            var testRunnerApi = ScriptableObject.CreateInstance<TestRunnerApi>();

            // 创建测试运行器请求
            var request = new ExecutionSettings();
            request.filters = new[] { new Filter { testMode = TestMode.EditMode } };

            // 注册回调
            testRunnerApi.RegisterCallbacks(new TestReportCallbacks());

            // 执行测试
            testRunnerApi.Execute(request);
        }

        /// <summary>
        /// 测试报告回调
        /// </summary>
        private class TestReportCallbacks : ICallbacks
        {
            // 测试结果列表
            private List<TestResult> m_TestResults = new List<TestResult>();

            // 开始时间
            private DateTime m_StartTime;

            public void RunStarted(ITestAdaptor testsToRun)
            {
                m_StartTime = DateTime.Now;
                Debug.Log($"开始运行测试: {testsToRun.Name}");
            }

            public void RunFinished(ITestResultAdaptor result)
            {
                // 计算测试时间
                TimeSpan duration = DateTime.Now - m_StartTime;

                // 生成报告
                string reportContent = GenerateHtmlReport(result, m_TestResults, duration);

                // 确保报告目录存在
                string reportDir = Path.Combine(Application.dataPath, "..", c_ReportPath);
                Directory.CreateDirectory(reportDir);

                // 保存报告
                string reportFileName = string.Format(c_ReportFileName, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                string reportPath = Path.Combine(reportDir, reportFileName);
                File.WriteAllText(reportPath, reportContent);

                Debug.Log($"测试报告已生成: {reportPath}");

                // 打开报告
                EditorUtility.RevealInFinder(reportPath);
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
                    // 记录测试结果
                    m_TestResults.Add(new TestResult
                    {
                        FullName = result.Test.FullName,
                        Name = result.Test.Name,
                        Duration = result.Duration,
                        PassState = result.GetPassState(),
                        Message = result.Message,
                        StackTrace = result.StackTrace
                    });
                }
            }

            /// <summary>
            /// 生成HTML报告
            /// </summary>
            /// <param name="_result">测试结果</param>
            /// <param name="_testResults">测试结果列表</param>
            /// <param name="_duration">测试时间</param>
            /// <returns>HTML报告内容</returns>
            private string GenerateHtmlReport(ITestResultAdaptor _result, List<TestResult> _testResults, TimeSpan _duration)
            {
                StringBuilder sb = new StringBuilder();

                // HTML头部
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"zh-CN\">");
                sb.AppendLine("<head>");
                sb.AppendLine("    <meta charset=\"UTF-8\">");
                sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.AppendLine("    <title>TByd.CodeStyle 测试报告</title>");
                sb.AppendLine("    <style>");
                sb.AppendLine("        body { font-family: 'Microsoft YaHei', Arial, sans-serif; margin: 0; padding: 20px; color: #333; }");
                sb.AppendLine("        h1, h2 { color: #0078d7; }");
                sb.AppendLine("        .summary { background-color: #f5f5f5; padding: 15px; border-radius: 5px; margin-bottom: 20px; }");
                sb.AppendLine("        .summary-item { margin-bottom: 10px; }");
                sb.AppendLine("        .passed { color: #5cb85c; }");
                sb.AppendLine("        .failed { color: #d9534f; }");
                sb.AppendLine("        .ignored { color: #f0ad4e; }");
                sb.AppendLine("        table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }");
                sb.AppendLine("        th, td { padding: 10px; text-align: left; border-bottom: 1px solid #ddd; }");
                sb.AppendLine("        th { background-color: #0078d7; color: white; }");
                sb.AppendLine("        tr:hover { background-color: #f5f5f5; }");
                sb.AppendLine("        .details { margin-top: 5px; padding: 10px; background-color: #f8f8f8; border-left: 3px solid #d9534f; display: none; }");
                sb.AppendLine("        .test-row { cursor: pointer; }");
                sb.AppendLine("        .test-row.failed { background-color: #ffeeee; }");
                sb.AppendLine("        .test-row.passed { background-color: #eeffee; }");
                sb.AppendLine("        .test-row.ignored { background-color: #ffffee; }");
                sb.AppendLine("    </style>");
                sb.AppendLine("    <script>");
                sb.AppendLine("        function toggleDetails(id) {");
                sb.AppendLine("            var details = document.getElementById('details-' + id);");
                sb.AppendLine("            if (details.style.display === 'block') {");
                sb.AppendLine("                details.style.display = 'none';");
                sb.AppendLine("            } else {");
                sb.AppendLine("                details.style.display = 'block';");
                sb.AppendLine("            }");
                sb.AppendLine("        }");
                sb.AppendLine("    </script>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");

                // 标题
                sb.AppendLine("    <h1>TByd.CodeStyle 测试报告</h1>");

                // 摘要
                sb.AppendLine("    <div class=\"summary\">");
                sb.AppendLine("        <h2>测试摘要</h2>");
                sb.AppendLine($"        <div class=\"summary-item\">总测试数: <strong>{_result.GetTestCount()}</strong></div>");
                sb.AppendLine($"        <div class=\"summary-item\">通过: <strong class=\"passed\">{_result.PassCount}</strong></div>");
                sb.AppendLine($"        <div class=\"summary-item\">失败: <strong class=\"failed\">{_result.FailCount}</strong></div>");
                sb.AppendLine($"        <div class=\"summary-item\">忽略: <strong class=\"ignored\">{_result.SkipCount}</strong></div>");
                sb.AppendLine($"        <div class=\"summary-item\">测试时间: <strong>{_duration.TotalSeconds:F2}秒</strong></div>");
                sb.AppendLine($"        <div class=\"summary-item\">生成时间: <strong>{DateTime.Now}</strong></div>");
                sb.AppendLine("    </div>");

                // 测试结果表格
                sb.AppendLine("    <h2>测试结果</h2>");
                sb.AppendLine("    <table>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <th>测试名称</th>");
                sb.AppendLine("            <th>状态</th>");
                sb.AppendLine("            <th>时间(秒)</th>");
                sb.AppendLine("        </tr>");

                // 测试结果行
                for (int i = 0; i < _testResults.Count; i++)
                {
                    var testResult = _testResults[i];
                    string rowClass = "test-row ";
                    string statusText = "";

                    switch (testResult.PassState)
                    {
                        case UnityEditor.TestTools.TestRunner.Api.TestStatus.Passed:
                            rowClass += "passed";
                            statusText = "<span class=\"passed\">通过</span>";
                            break;
                        case UnityEditor.TestTools.TestRunner.Api.TestStatus.Failed:
                            rowClass += "failed";
                            statusText = "<span class=\"failed\">失败</span>";
                            break;
                        default:
                            rowClass += "ignored";
                            statusText = "<span class=\"ignored\">忽略</span>";
                            break;
                    }

                    sb.AppendLine($"        <tr class=\"{rowClass}\" onclick=\"toggleDetails({i})\">");
                    sb.AppendLine($"            <td>{testResult.Name}</td>");
                    sb.AppendLine($"            <td>{statusText}</td>");
                    sb.AppendLine($"            <td>{testResult.Duration:F3}</td>");
                    sb.AppendLine("        </tr>");

                    // 测试详情
                    if (testResult.PassState != UnityEditor.TestTools.TestRunner.Api.TestStatus.Passed)
                    {
                        sb.AppendLine($"        <tr>");
                        sb.AppendLine($"            <td colspan=\"3\">");
                        sb.AppendLine($"                <div id=\"details-{i}\" class=\"details\">");
                        sb.AppendLine($"                    <div><strong>完整名称:</strong> {testResult.FullName}</div>");
                        if (!string.IsNullOrEmpty(testResult.Message))
                        {
                            sb.AppendLine($"                    <div><strong>错误信息:</strong> {testResult.Message}</div>");
                        }
                        if (!string.IsNullOrEmpty(testResult.StackTrace))
                        {
                            sb.AppendLine($"                    <div><strong>堆栈跟踪:</strong><pre>{testResult.StackTrace}</pre></div>");
                        }
                        sb.AppendLine($"                </div>");
                        sb.AppendLine($"            </td>");
                        sb.AppendLine($"        </tr>");
                    }
                }

                sb.AppendLine("    </table>");

                // HTML尾部
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                return sb.ToString();
            }
        }

        /// <summary>
        /// 测试结果类
        /// </summary>
        private class TestResult
        {
            public string FullName { get; set; }
            public string Name { get; set; }
            public double Duration { get; set; }
            public UnityEditor.TestTools.TestRunner.Api.TestStatus PassState { get; set; }
            public string Message { get; set; }
            public string StackTrace { get; set; }
        }
    }
}
