using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Utils
{
    /// <summary>
    /// Unity版本兼容适配器，提供不同Unity版本间的API兼容层
    /// </summary>
    public static class UnityVersionAdapter
    {
        // 当前Unity版本
        private static readonly Version s_CurrentUnityVersion;

        // Unity 2021.3版本（我们支持的最低版本）
        private static readonly Version s_Unity2021_3 = new Version(2021, 3);

        // Unity 2022.1版本
        private static readonly Version s_Unity2022_1 = new Version(2022, 1);

        // Unity 2023.1版本
        private static readonly Version s_Unity2023_1 = new Version(2023, 1);

        /// <summary>
        /// 静态构造函数，初始化当前Unity版本号
        /// </summary>
        static UnityVersionAdapter()
        {
            string[] versionComponents = Application.unityVersion.Split('.');
            int major = 0, minor = 0, patch = 0;

            if (versionComponents.Length >= 1)
                int.TryParse(versionComponents[0], out major);

            if (versionComponents.Length >= 2)
                int.TryParse(versionComponents[1], out minor);

            if (versionComponents.Length >= 3)
            {
                // 处理形如"2021.3.8f1"的版本号，提取数字部分
                string patchString = versionComponents[2];
                for (int i = 0; i < patchString.Length; i++)
                {
                    if (!char.IsDigit(patchString[i]))
                    {
                        patchString = patchString.Substring(0, i);
                        break;
                    }
                }
                int.TryParse(patchString, out patch);
            }

            s_CurrentUnityVersion = new Version(major, minor, patch);

            Debug.Log($"初始化Unity版本适配器: 当前版本 {s_CurrentUnityVersion}");
        }

        /// <summary>
        /// 检查当前Unity版本是否至少为指定版本
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="minor">次版本号</param>
        /// <param name="patch">补丁版本号（可选）</param>
        /// <returns>如果当前版本大于或等于指定版本则返回true</returns>
        public static bool IsVersionAtLeast(int major, int minor, int patch = 0)
        {
            return s_CurrentUnityVersion >= new Version(major, minor, patch);
        }

        /// <summary>
        /// 检查当前Unity版本是否支持最新的Package Manager API
        /// </summary>
        /// <returns>如果支持则返回true</returns>
        public static bool SupportsNewPackageManagerAPI()
        {
            return IsVersionAtLeast(2021, 3);
        }

        /// <summary>
        /// 检查当前Unity版本是否至少为Unity 2021.3
        /// </summary>
        /// <returns>如果是则返回true</returns>
        public static bool IsUnity2021_3OrNewer()
        {
            return s_CurrentUnityVersion >= s_Unity2021_3;
        }

        /// <summary>
        /// 检查当前Unity版本是否至少为Unity 2022.1
        /// </summary>
        /// <returns>如果是则返回true</returns>
        public static bool IsUnity2022_1OrNewer()
        {
            return s_CurrentUnityVersion >= s_Unity2022_1;
        }

        /// <summary>
        /// 检查当前Unity版本是否至少为Unity 2023.1
        /// </summary>
        /// <returns>如果是则返回true</returns>
        public static bool IsUnity2023_1OrNewer()
        {
            return s_CurrentUnityVersion >= s_Unity2023_1;
        }

        /// <summary>
        /// 获取当前Unity版本号
        /// </summary>
        /// <returns>版本号</returns>
        public static Version GetCurrentUnityVersion()
        {
            return s_CurrentUnityVersion;
        }

        /// <summary>
        /// 安全地调用仅在特定Unity版本中存在的方法
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">要执行的操作</param>
        /// <param name="fallbackValue">如果操作失败时的返回值</param>
        /// <param name="minRequiredVersion">最低需要的Unity版本</param>
        /// <returns>操作的返回值，或者失败时的回退值</returns>
        public static T SafeCall<T>(Func<T> action, T fallbackValue, Version minRequiredVersion)
        {
            if (s_CurrentUnityVersion < minRequiredVersion)
            {
                Debug.LogWarning($"尝试调用需要Unity {minRequiredVersion} 的API，但当前版本为 {s_CurrentUnityVersion}");
                return fallbackValue;
            }

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Debug.LogError($"调用高版本Unity API时出错: {ex.Message}");
                return fallbackValue;
            }
        }

        /// <summary>
        /// 安全地调用仅在特定Unity版本中存在的方法（无返回值）
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="minRequiredVersion">最低需要的Unity版本</param>
        /// <returns>是否成功执行</returns>
        public static bool SafeCall(Action action, Version minRequiredVersion)
        {
            if (s_CurrentUnityVersion < minRequiredVersion)
            {
                Debug.LogWarning($"尝试调用需要Unity {minRequiredVersion} 的API，但当前版本为 {s_CurrentUnityVersion}");
                return false;
            }

            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"调用高版本Unity API时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 通过反射调用可能不存在的方法
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数数组</param>
        /// <param name="fallbackValue">失败时的返回值</param>
        /// <returns>方法的返回值，或失败时的回退值</returns>
        public static TResult InvokeMethodByReflection<T, TResult>(T target, string methodName, object[] parameters, TResult fallbackValue)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo method = type.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (method == null)
                {
                    Debug.LogWarning($"方法 {methodName} 在类型 {type.Name} 中不存在");
                    return fallbackValue;
                }

                object result = method.Invoke(target, parameters);
                if (result == null)
                    return fallbackValue;

                return (TResult)result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过反射调用方法 {methodName} 时出错: {ex.Message}");
                return fallbackValue;
            }
        }

        /// <summary>
        /// 通过反射调用可能不存在的方法（无返回值）
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>是否成功执行</returns>
        public static bool InvokeMethodByReflection<T>(T target, string methodName, object[] parameters)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo method = type.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (method == null)
                {
                    Debug.LogWarning($"方法 {methodName} 在类型 {type.Name} 中不存在");
                    return false;
                }

                method.Invoke(target, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过反射调用方法 {methodName} 时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取属性值（通过反射）
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="fallbackValue">失败时的返回值</param>
        /// <returns>属性值，或失败时的回退值</returns>
        public static TResult GetPropertyByReflection<T, TResult>(T target, string propertyName, TResult fallbackValue)
        {
            try
            {
                Type type = typeof(T);
                PropertyInfo property = type.GetProperty(propertyName,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (property == null)
                {
                    Debug.LogWarning($"属性 {propertyName} 在类型 {type.Name} 中不存在");
                    return fallbackValue;
                }

                object value = property.GetValue(target);
                if (value == null)
                    return fallbackValue;

                return (TResult)value;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过反射获取属性 {propertyName} 时出错: {ex.Message}");
                return fallbackValue;
            }
        }

        /// <summary>
        /// 设置属性值（通过反射）
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否成功设置</returns>
        public static bool SetPropertyByReflection<T>(T target, string propertyName, object value)
        {
            try
            {
                Type type = typeof(T);
                PropertyInfo property = type.GetProperty(propertyName,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (property == null)
                {
                    Debug.LogWarning($"属性 {propertyName} 在类型 {type.Name} 中不存在");
                    return false;
                }

                property.SetValue(target, value);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过反射设置属性 {propertyName} 时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取字段值（通过反射）
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fallbackValue">失败时的返回值</param>
        /// <returns>字段值，或失败时的回退值</returns>
        public static TResult GetFieldByReflection<T, TResult>(T target, string fieldName, TResult fallbackValue)
        {
            try
            {
                Type type = typeof(T);
                FieldInfo field = type.GetField(fieldName,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (field == null)
                {
                    Debug.LogWarning($"字段 {fieldName} 在类型 {type.Name} 中不存在");
                    return fallbackValue;
                }

                object value = field.GetValue(target);
                if (value == null)
                    return fallbackValue;

                return (TResult)value;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过反射获取字段 {fieldName} 时出错: {ex.Message}");
                return fallbackValue;
            }
        }

        /// <summary>
        /// 设置字段值（通过反射）
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否成功设置</returns>
        public static bool SetFieldByReflection<T>(T target, string fieldName, object value)
        {
            try
            {
                Type type = typeof(T);
                FieldInfo field = type.GetField(fieldName,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (field == null)
                {
                    Debug.LogWarning($"字段 {fieldName} 在类型 {type.Name} 中不存在");
                    return false;
                }

                field.SetValue(target, value);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过反射设置字段 {fieldName} 时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Package Manager UI 相关适配
        /// </summary>
        public static class PackageManager
        {
            /// <summary>
            /// 打开Package Manager窗口并选择指定的包
            /// </summary>
            /// <param name="packageName">包名称</param>
            /// <returns>是否成功操作</returns>
            public static bool OpenPackageManagerAndSelectPackage(string packageName)
            {
                try
                {
                    // 打开Package Manager窗口
                    Type windowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.PackageManager.UI.PackageManagerWindow");
                    if (windowType == null)
                    {
                        Debug.LogWarning("找不到PackageManagerWindow类型");
                        return false;
                    }

                    // 打开窗口
                    MethodInfo showMethod = windowType.GetMethod("ShowPackageManager",
                        BindingFlags.Public | BindingFlags.Static);
                    if (showMethod == null)
                    {
                        Debug.LogWarning("找不到ShowPackageManager方法");
                        return false;
                    }

                    EditorWindow window = (EditorWindow)showMethod.Invoke(null, null);

                    // 在Unity 2022.1及更高版本中，尝试使用新的API选择包
                    if (IsUnity2022_1OrNewer())
                    {
                        // 尝试获取SetPackageSelection方法（Unity 2022.1+）
                        MethodInfo setSelectionMethod = windowType.GetMethod("SetPackageSelection",
                            BindingFlags.Public | BindingFlags.Instance);
                        if (setSelectionMethod != null)
                        {
                            setSelectionMethod.Invoke(window, new object[] { packageName });
                            return true;
                        }
                    }

                    // 在低版本中尝试使用旧的API
                    MethodInfo selectMethod = windowType.GetMethod("SelectPackage",
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.Static);
                    if (selectMethod != null)
                    {
                        selectMethod.Invoke(window, new object[] { packageName });
                        return true;
                    }

                    Debug.LogWarning("无法找到适合当前Unity版本的方法来选择包");
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"打开Package Manager并选择包时出错: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Editor UI 相关适配
        /// </summary>
        public static class EditorUI
        {
            /// <summary>
            /// 获取当前Unity编辑器的皮肤是否为深色模式
            /// </summary>
            /// <returns>如果是深色模式则返回true，否则返回false</returns>
            public static bool IsDarkTheme()
            {
                try
                {
                    // Unity 2019.3及更高版本支持EditorGUIUtility.isProSkin
                    PropertyInfo proSkinProperty = typeof(EditorGUIUtility).GetProperty("isProSkin",
                        BindingFlags.Public | BindingFlags.Static);
                    if (proSkinProperty != null)
                    {
                        return (bool)proSkinProperty.GetValue(null);
                    }

                    // 低版本使用EditorGUIUtility.GetBuiltinSkin
                    MethodInfo getSkinMethod = typeof(EditorGUIUtility).GetMethod("GetBuiltinSkin",
                        BindingFlags.Public | BindingFlags.Static);
                    if (getSkinMethod == null)
                        return false;

                    object skin = getSkinMethod.Invoke(null, new object[] { 1 }); // 1 = Dark
                    PropertyInfo nameProperty = skin.GetType().GetProperty("name");
                    if (nameProperty == null)
                        return false;

                    string name = (string)nameProperty.GetValue(skin);
                    return name.Contains("Dark") || name.Contains("Pro");
                }
                catch
                {
                    // 默认假设为浅色主题
                    return false;
                }
            }
        }

        /// <summary>
        /// 构建系统相关适配
        /// </summary>
        public static class BuildSystem
        {
            /// <summary>
            /// 获取当前的脚本后端
            /// </summary>
            /// <returns>脚本后端字符串</returns>
            public static string GetScriptingBackend()
            {
                try
                {
                    // 获取当前构建目标
                    BuildTarget target = EditorUserBuildSettings.activeBuildTarget;

                    // 获取脚本后端（通过反射，因为ScriptingImplementation枚举可能在不同版本中不同）
                    Type playerSettingsType = typeof(PlayerSettings);
                    MethodInfo getScriptingBackendMethod = playerSettingsType.GetMethod("GetScriptingBackend",
                        BindingFlags.Public | BindingFlags.Static);

                    if (getScriptingBackendMethod == null)
                    {
                        Debug.LogWarning("找不到GetScriptingBackend方法");
                        return "Unknown";
                    }

                    object backend = getScriptingBackendMethod.Invoke(null, new object[] { GetBuildTargetGroup(target) });
                    return backend.ToString();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"获取脚本后端时出错: {ex.Message}");
                    return "Unknown";
                }
            }

            /// <summary>
            /// 获取BuildTarget对应的BuildTargetGroup
            /// </summary>
            /// <param name="target">构建目标</param>
            /// <returns>对应的构建目标组</returns>
            private static BuildTargetGroup GetBuildTargetGroup(BuildTarget target)
            {
                switch (target)
                {
                    case BuildTarget.StandaloneWindows:
                    case BuildTarget.StandaloneWindows64:
                    case BuildTarget.StandaloneLinux64:
                    case BuildTarget.StandaloneOSX:
                        return BuildTargetGroup.Standalone;
                    case BuildTarget.iOS:
                        return BuildTargetGroup.iOS;
                    case BuildTarget.Android:
                        return BuildTargetGroup.Android;
                    case BuildTarget.WebGL:
                        return BuildTargetGroup.WebGL;
                    case BuildTarget.WSAPlayer:
                        return BuildTargetGroup.WSA;
                    case BuildTarget.PS4:
                        return BuildTargetGroup.PS4;
                    case BuildTarget.PS5:
                        return BuildTargetGroup.PS5;
                    case BuildTarget.XboxOne:
                        return BuildTargetGroup.XboxOne;
                    case BuildTarget.tvOS:
                        return BuildTargetGroup.tvOS;
                    case BuildTarget.Switch:
                        return BuildTargetGroup.Switch;
                    default:
                        return BuildTargetGroup.Unknown;
                }
            }

            /// <summary>
            /// 检查是否启用了代码优化
            /// </summary>
            /// <returns>如果启用了代码优化则返回true</returns>
            public static bool IsCodeOptimizationEnabled()
            {
                try
                {
                    // 获取当前构建目标
                    BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
                    BuildTargetGroup targetGroup = GetBuildTargetGroup(target);

                    // 检查代码优化设置
                    Type playerSettingsType = typeof(PlayerSettings);
                    MethodInfo getScriptingOptimizationMethod = playerSettingsType.GetMethod("GetScriptingOptimizationLevel",
                        BindingFlags.Public | BindingFlags.Static);

                    if (getScriptingOptimizationMethod != null)
                    {
                        object level = getScriptingOptimizationMethod.Invoke(null, new object[] { targetGroup });
                        return !level.ToString().Contains("Debug");
                    }

                    // 如果找不到方法，尝试通过其他方式检查
                    return !Debug.isDebugBuild;
                }
                catch
                {
                    // 默认假设代码优化已启用
                    return true;
                }
            }
        }
    }
}
