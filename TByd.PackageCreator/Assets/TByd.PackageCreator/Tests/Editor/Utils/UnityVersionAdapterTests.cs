using System;
using System.Reflection;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Utils
{
    /// <summary>
    /// Unity版本适配器测试
    /// </summary>
    [TestFixture]
    public class UnityVersionAdapterTests
    {
        [Test]
        public void GetCurrentUnityVersion_ReturnsValidVersion()
        {
            // 获取当前Unity版本
            Version unityVersion = UnityVersionAdapter.GetCurrentUnityVersion();

            // 验证版本号是否有效
            Assert.NotNull(unityVersion, "Unity版本不应为空");
            Assert.Greater(unityVersion.Major, 0, "主版本号应大于0");

            // 验证与Unity编辑器报告的版本一致
            string editorVersion = Application.unityVersion;
            Debug.Log($"编辑器版本: {editorVersion}, 适配器版本: {unityVersion}");

            // 检查主版本号一致
            Assert.AreEqual(int.Parse(editorVersion.Split('.')[0]), unityVersion.Major, "主版本号应一致");
        }

        [Test]
        public void IsVersionAtLeast_ComparesVersionsCorrectly()
        {
            // 假设当前版本为Unity 2021.3.8f1
            Version currentVersion = UnityVersionAdapter.GetCurrentUnityVersion();

            // 当前版本应该至少是它自己的版本
            Assert.IsTrue(UnityVersionAdapter.IsVersionAtLeast(currentVersion.Major, currentVersion.Minor),
                "当前版本应至少为自身版本");

            // 未来的版本号
            Assert.IsFalse(UnityVersionAdapter.IsVersionAtLeast(9999, 0),
                "当前版本不应至少为未来版本");

            // Unity 2017.1 (很旧的版本)
            Assert.IsTrue(UnityVersionAdapter.IsVersionAtLeast(2017, 1),
                "当前版本应至少为Unity 2017.1");
        }

        [Test]
        public void SpecificVersionChecks_AreConsistent()
        {
            Version currentVersion = UnityVersionAdapter.GetCurrentUnityVersion();

            // 检查版本特定方法与通用方法的一致性
            bool isAtLeast2021_3 = UnityVersionAdapter.IsVersionAtLeast(2021, 3);
            bool is2021_3OrNewer = UnityVersionAdapter.IsUnity2021_3OrNewer();
            Assert.AreEqual(isAtLeast2021_3, is2021_3OrNewer, "IsUnity2021_3OrNewer应与IsVersionAtLeast(2021,3)一致");

            bool isAtLeast2022_1 = UnityVersionAdapter.IsVersionAtLeast(2022, 1);
            bool is2022_1OrNewer = UnityVersionAdapter.IsUnity2022_1OrNewer();
            Assert.AreEqual(isAtLeast2022_1, is2022_1OrNewer, "IsUnity2022_1OrNewer应与IsVersionAtLeast(2022,1)一致");

            bool isAtLeast2023_1 = UnityVersionAdapter.IsVersionAtLeast(2023, 1);
            bool is2023_1OrNewer = UnityVersionAdapter.IsUnity2023_1OrNewer();
            Assert.AreEqual(isAtLeast2023_1, is2023_1OrNewer, "IsUnity2023_1OrNewer应与IsVersionAtLeast(2023,1)一致");
        }

        [Test]
        public void SafeCall_HandlesVersionRequirements()
        {
            // 当前版本
            Version currentVersion = UnityVersionAdapter.GetCurrentUnityVersion();

            // 测试当需求版本低于当前版本时能正常调用
            bool lowerVersionCalled = false;
            bool lowerVersionResult = UnityVersionAdapter.SafeCall(() => { lowerVersionCalled = true; }, new Version(2017, 1));
            Assert.IsTrue(lowerVersionCalled, "当需求版本低于当前版本时，应调用操作");
            Assert.IsTrue(lowerVersionResult, "对于低版本要求，SafeCall应返回true");

            // 测试当需求版本高于当前版本时不调用
            bool higherVersionCalled = false;
            bool higherVersionResult = UnityVersionAdapter.SafeCall(() => { higherVersionCalled = true; }, new Version(9999, 0));
            Assert.IsFalse(higherVersionCalled, "当需求版本高于当前版本时，不应调用操作");
            Assert.IsFalse(higherVersionResult, "对于高版本要求，SafeCall应返回false");

            // 测试泛型版本的SafeCall
            string lowerVersionValue = UnityVersionAdapter.SafeCall(() => "Success", "Fallback", new Version(2017, 1));
            Assert.AreEqual("Success", lowerVersionValue, "低版本要求时应返回实际值");

            string higherVersionValue = UnityVersionAdapter.SafeCall(() => "Success", "Fallback", new Version(9999, 0));
            Assert.AreEqual("Fallback", higherVersionValue, "高版本要求时应返回后备值");
        }

        [Test]
        public void ReflectionMethods_WorkCorrectly()
        {
            // 创建测试对象
            TestClass testObj = new TestClass();

            // 测试方法调用
            bool methodCalled = UnityVersionAdapter.InvokeMethodByReflection(testObj, "TestMethod", new object[] { "param" });
            Assert.IsTrue(methodCalled, "应成功调用测试方法");
            Assert.AreEqual("param", testObj.LastParameter, "方法参数应正确传递");

            // 测试获取属性
            string propValue = UnityVersionAdapter.GetPropertyByReflection(testObj, "TestProperty", "fallback");
            Assert.AreEqual("TestValue", propValue, "应成功获取属性值");

            // 测试设置属性
            bool setPropResult = UnityVersionAdapter.SetPropertyByReflection(testObj, "TestProperty", "NewValue");
            Assert.IsTrue(setPropResult, "属性设置应成功");
            Assert.AreEqual("NewValue", testObj.TestProperty, "属性值应被正确设置");

            // 测试获取字段
            int fieldValue = UnityVersionAdapter.GetFieldByReflection(testObj, "testField", -1);
            Assert.AreEqual(42, fieldValue, "应成功获取字段值");

            // 测试设置字段
            bool setFieldResult = UnityVersionAdapter.SetFieldByReflection(testObj, "testField", 100);
            Assert.IsTrue(setFieldResult, "字段设置应成功");
            Assert.AreEqual(100, testObj.testField, "字段值应被正确设置");
        }

        [Test]
        public void EditorUI_IsDarkTheme_ReturnsValidResult()
        {
            // 此测试主要确保方法不会抛出异常，结果取决于用户的Unity主题设置
            bool isDarkTheme = UnityVersionAdapter.EditorUI.IsDarkTheme();
            Debug.Log($"当前编辑器主题: {(isDarkTheme ? "深色" : "浅色")}");

            // 结果应该是布尔值，无需断言具体值
            // 这个测试只是确保方法能正常运行
        }

        [Test]
        public void BuildSystem_GetScriptingBackend_ReturnsValidResult()
        {
            // 获取当前脚本后端
            string backend = UnityVersionAdapter.BuildSystem.GetScriptingBackend();
            Debug.Log($"当前脚本后端: {backend}");

            // 结果不应为空
            Assert.IsNotEmpty(backend, "脚本后端不应为空");

            // 结果应该是已知的值之一: Mono、IL2CPP或Unknown (错误情况)
            Assert.IsTrue(
                backend == "Mono" || backend == "IL2CPP" || backend == "Unknown",
                $"脚本后端应为Mono、IL2CPP或Unknown，而非{backend}"
            );

            // 如果返回Unknown，记录警告但不使测试失败
            if (backend == "Unknown")
            {
                Debug.LogWarning("获取脚本后端返回了Unknown值，这可能是由于运行环境限制或Unity版本不兼容导致的。");
            }
        }

        [Test]
        public void BuildSystem_IsCodeOptimizationEnabled_ReturnsValidResult()
        {
            // 获取代码优化状态
            bool isOptimized = UnityVersionAdapter.BuildSystem.IsCodeOptimizationEnabled();
            Debug.Log($"代码优化状态: {(isOptimized ? "已启用" : "已禁用")}");

            // 结果应该是布尔值，无需断言具体值
            // Debug模式下通常为false，Release模式下通常为true
        }

        [Test]
        public void PackageManager_APIs_ExistAndMeetRequirements()
        {
            // 这主要测试方法存在并且可以调用，但不实际执行打开Package Manager的操作

            // 确认方法存在
            var methodInfo = typeof(UnityVersionAdapter.PackageManager).GetMethod("OpenPackageManagerAndSelectPackage",
                BindingFlags.Public | BindingFlags.Static);
            Assert.NotNull(methodInfo, "OpenPackageManagerAndSelectPackage方法应存在");

            // 检查方法参数
            var parameters = methodInfo.GetParameters();
            Assert.AreEqual(1, parameters.Length, "方法应有一个参数");
            Assert.AreEqual(typeof(string), parameters[0].ParameterType, "参数类型应为string");

            // 检查返回类型
            Assert.AreEqual(typeof(bool), methodInfo.ReturnType, "返回类型应为bool");

            // 检查方法是否支持当前Unity版本
            bool isSupported = UnityVersionAdapter.SupportsNewPackageManagerAPI();
            Debug.Log($"当前Unity版本{(isSupported ? "支持" : "不支持")}新的Package Manager API");
        }

        // 用于测试反射方法的辅助类
        private class TestClass
        {
            public int testField = 42;
            public string TestProperty { get; set; } = "TestValue";
            public string LastParameter { get; private set; }

            public bool TestMethod(string param)
            {
                LastParameter = param;
                return true;
            }
        }
    }
}
