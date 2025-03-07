using System.IO;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Utils.AssetDatabase;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace TByd.PackageCreator.Tests.Editor.Utils
{
    /// <summary>
    /// AssetDatabaseUtils工具类的测试
    /// </summary>
    public class AssetDatabaseUtilsTests
    {
        private string _testFolderPath;
        private string _testFolderAssetPath;
        private string _testFilePath;
        private string _testFileAssetPath;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录和文件
            _testFolderPath = Path.Combine(Application.dataPath, "AssetDatabaseUtilsTests");
            _testFolderAssetPath = "Assets/AssetDatabaseUtilsTests";
            _testFilePath = Path.Combine(_testFolderPath, "test.asset");
            _testFileAssetPath = "Assets/AssetDatabaseUtilsTests/test.asset";

            // 确保测试目录存在
            if (!Directory.Exists(_testFolderPath))
            {
                Directory.CreateDirectory(_testFolderPath);
                AssetDatabase.Refresh();
            }
        }

        [TearDown]
        public void TearDown()
        {
            // 清理资源数据库
            AssetDatabase.Refresh();

            // 删除测试目录（绝对路径）
            if (Directory.Exists(_testFolderPath))
            {
                Directory.Delete(_testFolderPath, true);
            }

            // 确保删除Assets下的目录（Unity资产路径）
            if (Directory.Exists("Assets/AssetDatabaseUtilsTests"))
            {
                AssetDatabase.DeleteAsset("Assets/AssetDatabaseUtilsTests");
            }

            // 删除meta文件（如果存在，使用绝对路径）
            var metaFilePath = _testFolderPath + ".meta";
            if (File.Exists(metaFilePath))
            {
                File.Delete(metaFilePath);
            }

            // 确保所有资源变更都已应用
            AssetDatabase.Refresh();
        }

        [Test]
        public void AbsolutePathToAssetPath_ConvertsCorrectly()
        {
            // 执行
            var result = AssetDatabaseUtils.AbsolutePathToAssetPath(_testFilePath);

            // 验证
            Assert.AreEqual(_testFileAssetPath, result, "应正确转换为资产路径");
        }

        [Test]
        public void AbsolutePathToAssetPath_ReturnsEmpty_ForInvalidPath()
        {
            // 执行
            var result = AssetDatabaseUtils.AbsolutePathToAssetPath("C:/InvalidPath/test.asset");

            // 验证
            Assert.AreEqual(string.Empty, result, "无效路径应返回空字符串");
        }

        [Test]
        public void AssetPathToAbsolutePath_ConvertsCorrectly()
        {
            // 执行
            var result = AssetDatabaseUtils.AssetPathToAbsolutePath(_testFileAssetPath);

            // 验证 - 使用 Path.GetFullPath 处理路径分隔符差异
            Assert.AreEqual(
                Path.GetFullPath(_testFilePath).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(result).TrimEnd(Path.DirectorySeparatorChar),
                "应正确转换为绝对路径"
            );
        }

        [Test]
        public void AssetPathToAbsolutePath_ReturnsPathForNonExistentAsset()
        {
            // 执行
            var result = AssetDatabaseUtils.AssetPathToAbsolutePath("Assets/NonExistent/test.asset");

            // 验证
            Assert.IsNotNull(result, "对于非存在但格式正确的路径应返回绝对路径");
        }

        [Test]
        public void EnsureDirectoryExists_CreatesDirectory()
        {
            // 准备
            var testSubFolder = _testFolderAssetPath + "/SubFolder";
            var testSubFolderPath = Path.Combine(_testFolderPath, "SubFolder");

            // 确保目录不存在
            if (Directory.Exists(testSubFolderPath))
            {
                Directory.Delete(testSubFolderPath, true);
                AssetDatabase.Refresh();
            }

            // 执行
            // 更新期望的日志消息，使其匹配实际输出
            LogAssert.Expect(LogType.Log, $"创建目录: {testSubFolder}");
            var result = AssetDatabaseUtils.EnsureDirectoryExists(testSubFolder);

            // 刷新资源数据库
            AssetDatabase.Refresh();

            // 验证
            Assert.IsTrue(result, "应返回true表示目录创建成功");
            Assert.IsTrue(Directory.Exists(testSubFolderPath), "目录应该被创建");
        }

        [Test]
        public void EnsureDirectoryExists_ReturnsTrue_WhenDirectoryAlreadyExists()
        {
            // 准备
            // 注意：当目录已存在时，EnsureDirectoryExists方法不会记录任何日志

            // 执行
            var result = AssetDatabaseUtils.EnsureDirectoryExists(_testFolderAssetPath);

            // 验证
            Assert.IsTrue(result, "对于已存在的目录应返回true");
            Assert.IsTrue(Directory.Exists(_testFolderPath), "目录应该存在");
        }

        [Test]
        public void GetSubFolders_ReturnsCorrectFolders()
        {
            // 准备
            var subFolders = new string[] { "SubFolder1", "SubFolder2", "SubFolder3" };
            foreach (var folder in subFolders)
            {
                var path = Path.Combine(_testFolderPath, folder);
                Directory.CreateDirectory(path);
            }
            AssetDatabase.Refresh();

            // 执行
            var result = AssetDatabaseUtils.GetSubFolders(_testFolderAssetPath);

            // 验证
            Assert.IsNotNull(result, "结果不应为null");
            Assert.AreEqual(subFolders.Length, result.Length, "应返回正确数量的子文件夹");
            foreach (var folder in subFolders)
            {
                var expectedPath = _testFolderAssetPath + "/" + folder;
                Assert.Contains(expectedPath, result, "结果应包含所有子文件夹");
            }
        }

        [Test]
        public void GetSubFolders_ReturnsEmptyArray_WhenNoSubFolders()
        {
            // 执行
            var result = AssetDatabaseUtils.GetSubFolders(_testFolderAssetPath);

            // 验证
            Assert.IsNotNull(result, "结果不应为null");
            Assert.AreEqual(0, result.Length, "应返回空数组");
        }

        [Test]
        public void DeleteAsset_RemovesAsset_WhenAssetExists()
        {
            // 准备 - 创建测试资产
            var testAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            AssetDatabase.CreateAsset(testAsset, _testFileAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // 确认资产已创建
            Assert.IsTrue(File.Exists(_testFilePath), "测试资产应该已创建");

            // 执行
            // 更新期望的日志消息，使其匹配实际输出
            LogAssert.Expect(LogType.Log, $"删除资源: {_testFileAssetPath}");
            var result = AssetDatabaseUtils.DeleteAsset(_testFileAssetPath);

            // 验证
            Assert.IsTrue(result, "应返回true表示删除成功");
            Assert.IsFalse(File.Exists(_testFilePath), "资产应该被删除");
        }

        [Test]
        public void DeleteAsset_ReturnsFalse_WhenAssetDoesNotExist()
        {
            // 准备
            var nonExistentAssetPath = _testFolderAssetPath + "/nonexistent.asset";
            // 更新期望的日志消息，使其匹配实际输出
            LogAssert.Expect(LogType.Warning, $"删除资源失败: {nonExistentAssetPath}");

            // 执行
            var result = AssetDatabaseUtils.DeleteAsset(nonExistentAssetPath);

            // 验证
            Assert.IsFalse(result, "应返回false表示删除失败");
        }

        [Test]
        public void CreateAsset_CreatesAndSavesAsset()
        {
            // 准备
            var testAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            testAsset.testValue = "测试值";

            // 执行
            // 更新期望的日志消息，使其匹配实际输出
            LogAssert.Expect(LogType.Log, $"创建资源: {_testFileAssetPath}");
            var result = AssetDatabaseUtils.CreateAsset(testAsset, _testFileAssetPath);

            // 验证
            Assert.IsTrue(result, "应返回true表示创建成功");
            Assert.IsTrue(File.Exists(_testFilePath), "资产文件应该被创建");

            // 验证资产可以被加载
            var loadedAsset = AssetDatabase.LoadAssetAtPath<TestScriptableObject>(_testFileAssetPath);
            Assert.IsNotNull(loadedAsset, "应能加载创建的资产");
            Assert.AreEqual("测试值", loadedAsset.testValue, "资产值应被正确保存");
        }

        [Test]
        public void LoadAsset_LoadsAsset_WhenAssetExists()
        {
            // 准备 - 创建测试资产
            var testAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            testAsset.testValue = "测试值";
            AssetDatabase.CreateAsset(testAsset, _testFileAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // 执行
            var loadedAsset = AssetDatabaseUtils.LoadAsset<TestScriptableObject>(_testFileAssetPath);

            // 验证
            Assert.IsNotNull(loadedAsset, "应能加载资产");
            Assert.AreEqual("测试值", loadedAsset.testValue, "资产值应被正确加载");
        }

        [Test]
        public void LoadAsset_ReturnsNull_WhenAssetDoesNotExist()
        {
            // 准备
            var nonExistentAssetPath = _testFolderAssetPath + "/nonexistent.asset";
            // LoadAsset方法在文件不存在时不会记录警告日志，所以移除期望的日志消息

            // 执行
            var loadedAsset = AssetDatabaseUtils.LoadAsset<TestScriptableObject>(nonExistentAssetPath);

            // 验证
            Assert.IsNull(loadedAsset, "非存在资产应返回null");
        }

        [Test]
        public void FindAssets_FindsAssetsOfSpecifiedType()
        {
            // 准备 - 创建几个测试资产
            for (var i = 1; i <= 3; i++)
            {
                var testAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
                testAsset.testValue = $"测试值{i}";
                AssetDatabase.CreateAsset(testAsset, $"{_testFolderAssetPath}/test{i}.asset");
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // 执行
            var assets = AssetDatabaseUtils.FindAssets<TestScriptableObject>(_testFolderAssetPath);

            // 验证
            Assert.IsNotNull(assets, "结果不应为null");
            Assert.AreEqual(3, assets.Count, "应找到所有创建的资产");

            // 验证所有资产都被正确加载
            foreach (var asset in assets)
            {
                Assert.IsTrue(asset.testValue.StartsWith("测试值"), "资产值应被正确加载");
            }
        }

        [Test]
        public void FindAssets_ReturnsEmptyList_WhenNoAssetsFound()
        {
            // 执行
            var assets = AssetDatabaseUtils.FindAssets<TestScriptableObject>(_testFolderAssetPath);

            // 验证
            Assert.IsNotNull(assets, "结果不应为null");
            Assert.AreEqual(0, assets.Count, "应返回空列表");
        }
    }
}
