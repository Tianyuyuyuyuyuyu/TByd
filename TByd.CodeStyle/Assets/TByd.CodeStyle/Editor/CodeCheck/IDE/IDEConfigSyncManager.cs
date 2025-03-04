using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using UnityEditor;
using UnityEngine;

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

        // 用于测试的同步配置路径，允许测试修改同步配置的存储位置
        private static string s_SyncConfigPathForTesting = null;

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
        /// 同步IDE配置
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <returns>同步结果</returns>
        public static SyncResult SynchronizeConfig(IDEType _ideType)
        {
            var result = new SyncResult
            {
                Success = false,
                UpdatedFiles = new List<string>(),
                ConflictFiles = new List<string>()
            };

            try
            {
                // 获取配置路径
                string configPath = GetConfigPath(_ideType);

                // 验证配置目录是否存在
                if (!Directory.Exists(configPath))
                {
                    result.Error = $"配置目录不存在: {configPath}";
                    return result;
                }

                // 尝试获取同步锁
                if (!AcquireSyncLock())
                {
                    result.Error = "无法获取同步锁，可能有其他同步操作正在进行";
                    return result;
                }

                try
                {
                    // 加载同步配置
                    var config = LoadSyncConfig();
                    bool hasChanges = false;

                    // 获取需要同步的文件
                    var filesToSync = GetFilesToSync(_ideType, configPath);

                    // 检查是否有新文件
                    var existingFiles = new HashSet<string>(config.FileHashes.Keys);
                    var currentFiles = new HashSet<string>();

                    // 检查.idea目录是否存在
                    string ideaDirectory = Path.Combine(configPath, ".idea");
                    if (_ideType == IDEType.Rider && !Directory.Exists(ideaDirectory))
                    {
                        // 如果是Rider但.idea目录不存在，则创建它
                        try
                        {
                            Directory.CreateDirectory(ideaDirectory);
                            Debug.Log($"[TByd.CodeStyle] 已创建.idea目录: {ideaDirectory}");
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[TByd.CodeStyle] 创建.idea目录失败: {ex.Message}");
                            result.Error = "无法创建必要的配置目录";
                            return result;
                        }
                    }

                    // 检查文件变化
                    foreach (var file in filesToSync)
                    {
                        if (File.Exists(file))
                        {
                            // 计算相对路径和文件哈希
                            string relativePath = GetRelativePath(file, configPath);
                            currentFiles.Add(relativePath);

                            // 计算文件哈希
                            string fileHash = CalculateFileHash(file);
                            string previousHash;

                            // 检查文件是否有变化
                            if (!config.FileHashes.TryGetValue(relativePath, out previousHash) || previousHash != fileHash)
                            {
                                // 检查是否有冲突
                                if (HasConflict(file))
                                {
                                    result.ConflictFiles.Add(file);
                                }
                                else
                                {
                                    // 更新文件
                                    UpdateFile(file, _ideType);
                                    result.UpdatedFiles.Add(file);
                                    config.FileHashes[relativePath] = fileHash;
                                    hasChanges = true;
                                }
                            }
                        }
                    }

                    // 检查删除的文件
                    var deletedFiles = existingFiles.Except(currentFiles).ToList();
                    foreach (var deletedFile in deletedFiles)
                    {
                        config.FileHashes.Remove(deletedFile);
                        hasChanges = true;
                    }

                    // 只有在有变化时才更新同步时间和保存配置
                    if (hasChanges || result.ConflictFiles.Count > 0)
                    {
                        // 更新同步时间
                        config.LastSyncTime = DateTime.Now;

                        // 保存同步配置
                        SaveSyncConfig(config);
                    }

                    // 设置结果
                    result.Success = true;
                }
                finally
                {
                    // 释放同步锁
                    ReleaseSyncLock();
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
                Debug.LogError($"[TByd.CodeStyle] 同步IDE配置失败: {e.Message}");
            }

            return result;
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
            if (_conflictFiles == null || _conflictFiles.Count == 0)
            {
                return true;
            }

            try
            {
                string configPath = GetConfigPath(_ideType);

                // 加载同步配置
                var config = LoadSyncConfig();

                // 在解决冲突前创建备份
                string backupId = IDEConfigBackupManager.CreateBackup(_ideType, "冲突解决前的自动备份");
                if (string.IsNullOrEmpty(backupId))
                {
                    Debug.LogWarning("[TByd.CodeStyle] 无法创建冲突解决前的备份");
                }

                // 解决冲突
                foreach (var file in _conflictFiles)
                {
                    if (File.Exists(file))
                    {
                        string relativePath = GetRelativePath(file, configPath);

                        if (_useLocal)
                        {
                            // 使用本地版本，更新同步配置
                            string fileHash = CalculateFileHash(file);
                            config.FileHashes[relativePath] = fileHash;
                        }
                        else
                        {
                            try
                            {
                                // 获取远程文件路径
                                string remoteFilePath = GetRemoteFilePath(file, _ideType);

                                if (File.Exists(remoteFilePath))
                                {
                                    // 读取远程文件内容
                                    string remoteContent = File.ReadAllText(remoteFilePath);

                                    // 写入本地文件
                                    File.WriteAllText(file, remoteContent);

                                    // 重新计算文件哈希
                                    string fileHash = CalculateFileHash(file);
                                    config.FileHashes[relativePath] = fileHash;
                                }
                                else
                                {
                                    Debug.LogWarning($"[TByd.CodeStyle] 远程文件不存在: {remoteFilePath}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError($"[TByd.CodeStyle] 更新文件失败: {file}, 错误: {ex.Message}");
                            }
                        }
                    }
                }

                // 更新同步时间
                config.LastSyncTime = DateTime.Now;

                // 保存同步配置
                SaveSyncConfig(config);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 解决冲突失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取远程文件路径
        /// </summary>
        /// <param name="_localFilePath">本地文件路径</param>
        /// <param name="_ideType">IDE类型</param>
        /// <returns>远程文件路径</returns>
        private static string GetRemoteFilePath(string _localFilePath, IDEType _ideType)
        {
            // 获取配置路径
            string configPath = GetConfigPath(_ideType);

            // 计算相对路径
            string relativePath = GetRelativePath(_localFilePath, configPath);

            // 获取远程配置路径
            string remoteConfigPath = GetRemoteConfigPath(_ideType);

            // 组合远程文件路径
            return Path.Combine(remoteConfigPath, relativePath);
        }

        /// <summary>
        /// 获取远程配置路径
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <returns>远程配置路径</returns>
        private static string GetRemoteConfigPath(IDEType _ideType)
        {
            // 这里可以根据实际需求修改，例如从配置中读取远程路径
            // 当前简单实现为使用预定义的远程路径
            string basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TByd", "CodeStyle", "RemoteConfigs", _ideType.ToString());

            Directory.CreateDirectory(basePath);
            return basePath;
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
                        // 获取.idea目录下的所有XML和JSON文件
                        files.AddRange(Directory.GetFiles(ideaPath, "*.xml", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(ideaPath, "*.json", SearchOption.AllDirectories));

                        // 特别处理常见的配置文件
                        string codeStyleConfig = Path.Combine(ideaPath, "codeStyleConfig.xml");
                        if (File.Exists(codeStyleConfig) && !files.Contains(codeStyleConfig))
                        {
                            files.Add(codeStyleConfig);
                        }

                        string csharpierConfig = Path.Combine(ideaPath, "csharpier.json");
                        if (File.Exists(csharpierConfig) && !files.Contains(csharpierConfig))
                        {
                            files.Add(csharpierConfig);
                        }
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
            try
            {
                byte[] fileBytes = File.ReadAllBytes(_filePath);
                var md5 = System.Security.Cryptography.MD5.Create();
                byte[] hashBytes = md5.ComputeHash(fileBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 检查文件是否有冲突
        /// </summary>
        private static bool HasConflict(string _filePath)
        {
            // 这里可以实现更复杂的冲突检测逻辑
            // 简单起见，我们认为如果文件被修改且本地也有修改，就是冲突
            return false;
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        private static void UpdateFile(string _filePath, IDEType _ideType)
        {
            // 在实际应用中，这里会实现复杂的文件更新逻辑
            // 例如读取远程存储的文件版本并应用
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
        /// 获取同步配置路径
        /// </summary>
        private static string GetSyncConfigPath()
        {
            // 如果设置了测试用的路径，则使用测试路径
            if (!string.IsNullOrEmpty(s_SyncConfigPathForTesting))
            {
                return s_SyncConfigPathForTesting;
            }

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
                    // 尝试将JSON反序列化为配置对象，对于由带字符串键的字典的序列化，需要实现自定义方法
                    // 这里简化为创建一个新的配置对象，实际应用中需要完整实现
                    return new SyncConfig { LastSyncTime = DateTime.Now };
                }
                catch
                {
                    // 如果配置损坏，返回新的配置
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
            // 确保目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(configPath));

            // 简化版的配置保存，实际应用中需要实现完整的序列化
            File.WriteAllText(configPath, $"{{ \"LastSyncTime\": \"{_config.LastSyncTime}\" }}");
        }

        /// <summary>
        /// 保存同步配置（仅测试用，传入字典）
        /// </summary>
        private static void SaveSyncConfig(Dictionary<string, string> _fileHashes)
        {
            var config = new SyncConfig
            {
                LastSyncTime = DateTime.Now,
                FileHashes = _fileHashes
            };
            SaveSyncConfig(config);
        }

        /// <summary>
        /// 获取同步锁
        /// </summary>
        private static bool AcquireSyncLock()
        {
            string lockPath = Path.Combine(GetSyncConfigPath(), c_SyncLockFileName);

            // 检查锁文件是否存在
            if (File.Exists(lockPath))
            {
                try
                {
                    // 读取锁文件内容
                    string content = File.ReadAllText(lockPath);
                    DateTime lockTime;

                    // 尝试解析锁时间
                    if (DateTime.TryParse(content, out lockTime))
                    {
                        // 检查锁是否过期（10分钟超时）
                        if ((DateTime.Now - lockTime).TotalMinutes < 10)
                        {
                            Debug.LogWarning("[TByd.CodeStyle] 另一个同步进程正在运行");
                            return false;
                        }
                    }
                }
                catch
                {
                    // 忽略读取错误
                }

                // 删除过期的锁文件
                try
                {
                    File.Delete(lockPath);
                }
                catch
                {
                    Debug.LogError("[TByd.CodeStyle] 无法删除过期的锁文件");
                    return false;
                }
            }

            // 创建锁文件
            try
            {
                // 确保目录存在
                Directory.CreateDirectory(Path.GetDirectoryName(lockPath));
                File.WriteAllText(lockPath, DateTime.Now.ToString());
                return true;
            }
            catch
            {
                Debug.LogError("[TByd.CodeStyle] 无法创建锁文件");
                return false;
            }
        }

        /// <summary>
        /// 释放同步锁
        /// </summary>
        private static void ReleaseSyncLock()
        {
            string lockPath = Path.Combine(GetSyncConfigPath(), c_SyncLockFileName);

            // 删除锁文件
            if (File.Exists(lockPath))
            {
                try
                {
                    File.Delete(lockPath);
                }
                catch
                {
                    Debug.LogWarning("[TByd.CodeStyle] 无法释放同步锁");
                }
            }
        }

        /// <summary>
        /// 从文件路径获取IDE类型
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <returns>IDE类型</returns>
        private static IDEType GetIDETypeFromPath(string _filePath)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                throw new ArgumentException("文件路径不能为空", nameof(_filePath));
            }

            // 根据路径判断IDE类型
            if (_filePath.Contains("Rider"))
            {
                return IDEType.Rider;
            }
            else if (_filePath.Contains("VisualStudio"))
            {
                return IDEType.VisualStudio;
            }
            else if (_filePath.Contains("VSCode"))
            {
                return IDEType.VSCode;
            }

            // 如果无法判断，则根据扩展名尝试判断
            string extension = Path.GetExtension(_filePath).ToLower();
            if (extension == ".xml" || extension == ".dotsettings")
            {
                return IDEType.Rider;
            }
            else if (extension == ".vssettings" || extension == ".vsconfig")
            {
                return IDEType.VisualStudio;
            }
            else if (extension == ".json")
            {
                return IDEType.VSCode;
            }

            // 默认返回Rider
            Debug.LogWarning($"[TByd.CodeStyle] 无法从路径确定IDE类型: {_filePath}，默认使用Rider");
            return IDEType.Rider;
        }
    }
}
