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
    /// IDE配置备份管理器
    /// </summary>
    public static class IDEConfigBackupManager
    {
        // 备份目录名
        private const string c_BackupDirectory = "Backups";

        // 备份配置文件名
        private const string c_BackupConfigFileName = "backup_config.json";

        /// <summary>
        /// 备份信息
        /// </summary>
        [Serializable]
        public class BackupInfo
        {
            /// <summary>
            /// 备份ID
            /// </summary>
            public string Id;

            /// <summary>
            /// 备份时间
            /// </summary>
            public DateTime Timestamp;

            /// <summary>
            /// IDE类型
            /// </summary>
            public IDEType IDEType;

            /// <summary>
            /// 备份描述
            /// </summary>
            public string Description;

            /// <summary>
            /// 备份文件列表
            /// </summary>
            public List<string> Files;
        }

        /// <summary>
        /// 备份配置
        /// </summary>
        [Serializable]
        private class BackupConfig
        {
            /// <summary>
            /// 备份列表
            /// </summary>
            public List<BackupInfo> Backups = new List<BackupInfo>();
        }

        /// <summary>
        /// 创建配置备份
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <param name="_description">备份描述</param>
        /// <returns>备份ID</returns>
        public static string CreateBackup(IDEType _ideType, string _description = "")
        {
            try
            {
                // 确保备份目录存在
                string backupRoot = GetBackupRootPath();
                Directory.CreateDirectory(backupRoot);

                // 创建备份信息
                var backupInfo = new BackupInfo
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    IDEType = _ideType,
                    Description = _description,
                    Files = new List<string>()
                };

                // 创建备份目录
                string backupDir = Path.Combine(backupRoot, backupInfo.Id);
                Directory.CreateDirectory(backupDir);

                // 获取需要备份的文件
                string configPath = GetConfigPath(_ideType);
                var filesToBackup = GetFilesToBackup(_ideType, configPath);

                // 复制文件到备份目录
                foreach (var file in filesToBackup)
                {
                    if (File.Exists(file))
                    {
                        string relativePath = GetRelativePath(file, configPath);
                        string backupPath = Path.Combine(backupDir, relativePath);

                        // 确保目标目录存在
                        Directory.CreateDirectory(Path.GetDirectoryName(backupPath));

                        // 复制文件
                        File.Copy(file, backupPath, true);
                        backupInfo.Files.Add(relativePath);
                    }
                }

                // 更新备份配置
                var config = LoadBackupConfig();
                config.Backups.Add(backupInfo);
                SaveBackupConfig(config);

                Debug.Log($"[TByd.CodeStyle] 成功创建配置备份: {backupInfo.Id}");
                return backupInfo.Id;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 创建配置备份失败: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 恢复配置备份
        /// </summary>
        /// <param name="_backupId">备份ID</param>
        /// <returns>是否成功</returns>
        public static bool RestoreBackup(string _backupId)
        {
            try
            {
                // 加载备份配置
                var config = LoadBackupConfig();
                var backup = config.Backups.FirstOrDefault(b => b.Id == _backupId);
                if (backup == null)
                {
                    Debug.LogError($"[TByd.CodeStyle] 未找到备份: {_backupId}");
                    return false;
                }

                // 获取备份目录
                string backupDir = Path.Combine(GetBackupRootPath(), backup.Id);
                if (!Directory.Exists(backupDir))
                {
                    Debug.LogError($"[TByd.CodeStyle] 备份目录不存在: {backupDir}");
                    return false;
                }

                // 获取目标配置目录
                string configPath = GetConfigPath(backup.IDEType);
                Directory.CreateDirectory(configPath);

                // 恢复文件
                foreach (var relativePath in backup.Files)
                {
                    string backupPath = Path.Combine(backupDir, relativePath);
                    string targetPath = Path.Combine(configPath, relativePath);

                    if (File.Exists(backupPath))
                    {
                        // 确保目标目录存在
                        Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

                        // 复制文件
                        File.Copy(backupPath, targetPath, true);
                    }
                }

                Debug.Log($"[TByd.CodeStyle] 成功恢复配置备份: {_backupId}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 恢复配置备份失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除配置备份
        /// </summary>
        /// <param name="_backupId">备份ID</param>
        /// <returns>是否成功</returns>
        public static bool DeleteBackup(string _backupId)
        {
            try
            {
                // 加载备份配置
                var config = LoadBackupConfig();
                var backup = config.Backups.FirstOrDefault(b => b.Id == _backupId);
                if (backup == null)
                {
                    Debug.LogError($"[TByd.CodeStyle] 未找到备份: {_backupId}");
                    return false;
                }

                // 删除备份目录
                string backupDir = Path.Combine(GetBackupRootPath(), backup.Id);
                if (Directory.Exists(backupDir))
                {
                    Directory.Delete(backupDir, true);
                }

                // 更新备份配置
                config.Backups.Remove(backup);
                SaveBackupConfig(config);

                Debug.Log($"[TByd.CodeStyle] 成功删除配置备份: {_backupId}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 删除配置备份失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取备份列表
        /// </summary>
        /// <returns>备份列表</returns>
        public static List<BackupInfo> GetBackups()
        {
            try
            {
                var config = LoadBackupConfig();
                return config.Backups.OrderByDescending(b => b.Timestamp).ToList();
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 获取备份列表失败: {e.Message}");
                return new List<BackupInfo>();
            }
        }

        /// <summary>
        /// 获取需要备份的文件
        /// </summary>
        private static List<string> GetFilesToBackup(IDEType _ideType, string _configPath)
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
        /// 获取备份根目录路径
        /// </summary>
        private static string GetBackupRootPath()
        {
            return Path.Combine(Application.dataPath, "..", "Library", "TByd.CodeStyle", c_BackupDirectory);
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
        /// 加载备份配置
        /// </summary>
        private static BackupConfig LoadBackupConfig()
        {
            string configPath = Path.Combine(GetBackupRootPath(), c_BackupConfigFileName);
            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    return JsonUtility.FromJson<BackupConfig>(json);
                }
                catch
                {
                    // 如果配置文件损坏，返回新的配置
                    return new BackupConfig();
                }
            }
            return new BackupConfig();
        }

        /// <summary>
        /// 保存备份配置
        /// </summary>
        private static void SaveBackupConfig(BackupConfig _config)
        {
            string configPath = Path.Combine(GetBackupRootPath(), c_BackupConfigFileName);
            string json = JsonUtility.ToJson(_config, true);
            File.WriteAllText(configPath, json);
        }
    }
} 