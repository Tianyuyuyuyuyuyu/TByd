using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE配置同步管理器
    /// </summary>
    public static class IDEConfigSyncManager
    {
        // 同步配置文件名
        private const string c_SyncConfigFileName = "sync_config.json";

        // 同步锁文件名
        private const string c_SyncLockFileName = "sync.lock";

        /// <summary>
        /// 同步配置
        /// </summary>
        [Serializable]
        private class SyncConfig
        {
            /// <summary>
            /// 上次同步时间
            /// </summary>
            public DateTime LastSyncTime;

            /// <summary>
            /// 同步状态
            /// </summary>
            public Dictionary<string, string> FileHashes = new Dictionary<string, string>();
        }

        /// <summary>
        /// 同步结果
        /// </summary>
        public class SyncResult
        {
            /// <summary>
            /// 是否成功
            /// </summary>
            public bool Success;

            /// <summary>
            /// 更新的文件列表
            /// </summary>
            public List<string> UpdatedFiles = new List<string>();

            /// <summary>
            /// 冲突的文件列表
            /// </summary>
            public List<string> ConflictFiles = new List<string>();

            /// <summary>
            /// 错误信息
            /// </summary>
            public string Error;
        }

        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <returns>同步结果</returns>
        public static SyncResult SynchronizeConfig(IDEType _ideType)
        {
            var result = new SyncResult();

            try
            {
                // 获取配置目录
                string configPath = GetConfigPath(_ideType);
                if (!Directory.Exists(configPath))
                {
                    result.Error = $"配置目录不存在: {configPath}";
                    result.Success = false;
                    return result;
                }

                // 获取同步锁
                if (!AcquireSyncLock())
                {
                    result.Error = "另一个同步进程正在运行";
                    result.Success = false;
                    return result;
                }

                try
                {
                    // 加载同步配置
                    var config = LoadSyncConfig();

                    // 获取需要同步的文件
                    var filesToSync = GetFilesToSync(_ideType, configPath);

                    // 检查文件变更
                    foreach (var file in filesToSync)
                    {
                        if (File.Exists(file))
                        {
                            string hash = CalculateFileHash(file);
                            string relativePath = GetRelativePath(file, configPath);

                            // 检查文件是否已同步
                            if (config.FileHashes.TryGetValue(relativePath, out string lastHash))
                            {
                                if (hash != lastHash)
                                {
                                    // 文件已更改，检查是否有冲突
                                    if (HasConflict(file))
                                    {
                                        result.ConflictFiles.Add(relativePath);
                                    }
                                    else
                                    {
                                        // 更新文件
                                        UpdateFile(file, _ideType);
                                        result.UpdatedFiles.Add(relativePath);
                                        config.FileHashes[relativePath] = hash;
                                    }
                                }
                            }
                            else
                            {
                                // 新文件，直接添加
                                config.FileHashes[relativePath] = hash;
                                result.UpdatedFiles.Add(relativePath);
                            }
                        }
                    }

                    // 更新同步时间
                    config.LastSyncTime = DateTime.Now;

                    // 保存同步配置
                    SaveSyncConfig(config);

                    result.Success = true;
                }
                finally
                {
                    // 释放同步锁
                    ReleaseSyncLock();
                }

                return result;
            }
            catch (Exception e)
            {
                result.Error = $"同步配置失败: {e.Message}";
                result.Success = false;
                return result;
            }
        }

        /// <summary>
        /// 解决配置冲突
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <param name="_conflictFiles">冲突文件列表</param>
        /// <param name="_useLocal">是否使用本地版本</param>
        /// <returns>是否成功</returns>
        public static bool ResolveConflicts(IDEType _ideType, List<string> _conflictFiles, bool _useLocal)
        {
            try
            {
                string configPath = GetConfigPath(_ideType);

                foreach (var relativePath in _conflictFiles)
                {
                    string filePath = Path.Combine(configPath, relativePath);
                    if (File.Exists(filePath))
                    {
                        if (_useLocal)
                        {
                            // 使用本地版本，更新同步配置
                            var config = LoadSyncConfig();
                            config.FileHashes[relativePath] = CalculateFileHash(filePath);
                            SaveSyncConfig(config);
                        }
                        else
                        {
                            // 使用远程版本，更新文件
                            UpdateFile(filePath, _ideType);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 解决配置冲突失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取需要同步的文件
        /// </summary>
        private static List<string> GetFilesToSync(IDEType _ideType, string _configPath)
        {
            var files = new List<string>();

            switch (_ideType)
            {
                case IDEType.Rider:
                    // Rider配置文件
                    string ideaPath = Path.Combine(_configPath, ".idea");
                    if (Directory.Exists(ideaPath))
                    {
                        files.AddRange(Directory.GetFiles(ideaPath, "*.xml", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(ideaPath, "*.json", SearchOption.AllDirectories));
                    }
                    break;

                case IDEType.VisualStudio:
                    // Visual Studio配置文件
                    files.Add(Path.Combine(_configPath, ".vssettings"));
                    break;

                case IDEType.VSCode:
                    // VS Code配置文件
                    string vscodePath = Path.Combine(_configPath, ".vscode");
                    if (Directory.Exists(vscodePath))
                    {
                        files.AddRange(Directory.GetFiles(vscodePath, "*.json", SearchOption.AllDirectories));
                    }
                    break;
            }

            // EditorConfig文件
            string editorConfigPath = Path.Combine(_configPath, ".editorconfig");
            if (File.Exists(editorConfigPath))
            {
                files.Add(editorConfigPath);
            }

            return files;
        }

        /// <summary>
        /// 获取配置路径
        /// </summary>
        private static string GetConfigPath(IDEType _ideType)
        {
            switch (_ideType)
            {
                case IDEType.Rider:
                    return Path.GetDirectoryName(Application.dataPath);
                case IDEType.VisualStudio:
                    return Path.GetDirectoryName(Application.dataPath);
                case IDEType.VSCode:
                    return Path.GetDirectoryName(Application.dataPath);
                default:
                    return Path.GetDirectoryName(Application.dataPath);
            }
        }

        /// <summary>
        /// 计算文件哈希
        /// </summary>
        private static string CalculateFileHash(string _filePath)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            using (var stream = File.OpenRead(_filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// 检查文件是否有冲突
        /// </summary>
        private static bool HasConflict(string _filePath)
        {
            // 这里可以添加更复杂的冲突检测逻辑
            // 例如，检查文件是否被其他用户修改
            // 或者检查文件内容是否有冲突标记
            return false;
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        private static void UpdateFile(string _filePath, IDEType _ideType)
        {
            // 这里可以添加更新文件的逻辑
            // 例如，从远程仓库获取最新版本
            // 或者应用团队的标准配置
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        private static string GetRelativePath(string _fullPath, string _basePath)
        {
            Uri baseUri = new Uri(_basePath + Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(_fullPath);
            return Uri.UnescapeDataString(baseUri.MakeRelativeUri(fullUri).ToString());
        }

        /// <summary>
        /// 获取同步配置目录
        /// </summary>
        private static string GetSyncConfigPath()
        {
            return Path.Combine(Application.dataPath, "..", "Library", "TByd.CodeStyle");
        }

        /// <summary>
        /// 加载同步配置
        /// </summary>
        private static SyncConfig LoadSyncConfig()
        {
            string configPath = Path.Combine(GetSyncConfigPath(), c_SyncConfigFileName);
            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    return JsonUtility.FromJson<SyncConfig>(json);
                }
                catch
                {
                    // 如果配置文件损坏，返回新的配置
                    return new SyncConfig();
                }
            }
            return new SyncConfig();
        }

        /// <summary>
        /// 保存同步配置
        /// </summary>
        private static void SaveSyncConfig(SyncConfig _config)
        {
            string configPath = Path.Combine(GetSyncConfigPath(), c_SyncConfigFileName);
            string json = JsonUtility.ToJson(_config, true);
            File.WriteAllText(configPath, json);
        }

        /// <summary>
        /// 获取同步锁
        /// </summary>
        private static bool AcquireSyncLock()
        {
            string lockPath = Path.Combine(GetSyncConfigPath(), c_SyncLockFileName);

            try
            {
                if (File.Exists(lockPath))
                {
                    // 检查锁是否过期（超过5分钟）
                    var lastWriteTime = File.GetLastWriteTime(lockPath);
                    if ((DateTime.Now - lastWriteTime).TotalMinutes < 5)
                    {
                        return false;
                    }
                }

                // 创建或更新锁文件
                File.WriteAllText(lockPath, DateTime.Now.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 释放同步锁
        /// </summary>
        private static void ReleaseSyncLock()
        {
            string lockPath = Path.Combine(GetSyncConfigPath(), c_SyncLockFileName);
            if (File.Exists(lockPath))
            {
                try
                {
                    File.Delete(lockPath);
                }
                catch
                {
                    // 忽略删除锁文件失败的错误
                }
            }
        }
    }
} 