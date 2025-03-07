using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Utils.FileSystem
{
    /// <summary>
    /// 安全文件操作类，提供备份、验证和回滚机制
    /// </summary>
    public static class SecureFileOperations
    {
        // 备份文件后缀
        private const string BackupExtension = ".backup";

        // 临时文件后缀
        private const string TempExtension = ".temp";

        // 备份目录名称
        private const string BackupDirectoryName = "TByd.PackageCreator.Backups";

        // 最大备份保留时间（7天）
        private static readonly TimeSpan SMaxBackupAge = TimeSpan.FromDays(7);

        /// <summary>
        /// 获取备份目录路径
        /// </summary>
        /// <returns>备份目录路径</returns>
        public static string GetBackupDirectory()
        {
            var tempPath = Path.GetTempPath();
            var backupPath = Path.Combine(tempPath,BackupDirectoryName);

            // 确保目录存在
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            return backupPath;
        }

        /// <summary>
        /// 创建文件备份
        /// </summary>
        /// <param name="filePath">要备份的文件路径</param>
        /// <returns>备份文件路径，如果备份失败则返回null</returns>
        public static string CreateBackup(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Debug.LogWarning($"无法创建备份，文件不存在: {filePath}");
                return null;
            }

            try
            {
                var backupDirectory = GetBackupDirectory();
                var fileName = Path.GetFileName(filePath);
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var backupFileName = $"{fileName}.{timestamp}{BackupExtension}";
                var backupPath = Path.Combine(backupDirectory, backupFileName);

                File.Copy(filePath, backupPath, true);
                Debug.Log($"已创建备份: {backupPath}");

                // 清理旧备份
                CleanupOldBackups();

                return backupPath;
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建备份失败: {filePath}, 错误: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 恢复文件备份
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="targetPath">目标恢复路径，如果为null则使用原始路径</param>
        /// <returns>是否成功恢复</returns>
        public static bool RestoreBackup(string backupPath, string targetPath = null)
        {
            if (string.IsNullOrEmpty(backupPath) || !File.Exists(backupPath))
            {
                Debug.LogWarning($"无法恢复备份，备份文件不存在: {backupPath}");
                return false;
            }

            try
            {
                if (string.IsNullOrEmpty(targetPath))
                {
                    // 从备份文件名中提取原始文件名
                    var fileName = Path.GetFileName(backupPath);
                    var dotIndex = fileName.IndexOf('.');
                    if (dotIndex > 0)
                    {
                        fileName = fileName.Substring(0, dotIndex);
                    }

                    // 获取文件的原始目录
                    var originalDirectory = Path.GetDirectoryName(backupPath);
                    if (originalDirectory.EndsWith(BackupDirectoryName))
                    {
                        // 如果是在备份目录中，需要询问用户
                        Debug.LogWarning($"无法确定原始文件路径，请指定目标路径");
                        return false;
                    }

                    targetPath = Path.Combine(originalDirectory, fileName);
                }

                // 恢复文件
                File.Copy(backupPath, targetPath, true);
                Debug.Log($"已恢复备份: {backupPath} -> {targetPath}");

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"恢复备份失败: {backupPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地写入文件（先写入临时文件，成功后再替换目标文件）
        /// </summary>
        /// <param name="filePath">目标文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="createBackup">是否创建备份</param>
        /// <returns>是否成功写入</returns>
        public static bool SafeWriteFile(string filePath, string content, bool createBackup = true)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogWarning("无法写入文件，路径为空");
                return false;
            }

            var tempPath = filePath + TempExtension;
            string backupPath = null;

            try
            {
                // 确保目录存在
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 如果文件已存在且需要备份，先创建备份
                if (createBackup && File.Exists(filePath))
                {
                    backupPath = CreateBackup(filePath);
                }

                // 写入临时文件
                File.WriteAllText(tempPath, content);

                // 验证临时文件是否成功写入
                if (!File.Exists(tempPath))
                {
                    Debug.LogError($"写入临时文件失败: {tempPath}");
                    return false;
                }

                // 替换目标文件（如果目标文件存在，会被覆盖）
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                File.Move(tempPath, filePath);

                Debug.Log($"安全写入文件成功: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"安全写入文件失败: {filePath}, 错误: {ex.Message}");

                // 如果临时文件存在，删除它
                if (File.Exists(tempPath))
                {
                    try
                    {
                        File.Delete(tempPath);
                    }
                    catch { /* 忽略删除临时文件时的错误 */ }
                }

                // 如果有备份且原文件丢失，恢复备份
                if (backupPath != null && !File.Exists(filePath))
                {
                    try
                    {
                        RestoreBackup(backupPath, filePath);
                    }
                    catch { /* 忽略恢复备份时的错误 */ }
                }

                return false;
            }
        }

        /// <summary>
        /// 批量安全写入多个文件，支持事务性操作（要么全部成功，要么全部失败）
        /// </summary>
        /// <param name="fileContents">文件路径和内容的字典</param>
        /// <param name="createBackups">是否创建备份</param>
        /// <returns>是否全部成功</returns>
        public static bool SafeWriteFiles(Dictionary<string, string> fileContents, bool createBackups = true)
        {
            if (fileContents == null || fileContents.Count == 0)
            {
                Debug.LogWarning("无文件需要写入");
                return false;
            }

            // 保存原始文件的备份路径
            var backups = new Dictionary<string, string>();
            // 保存临时文件路径
            var tempFiles = new List<string>();

            try
            {
                // 第一阶段：创建备份和临时文件
                foreach (var kvp in fileContents)
                {
                    var filePath = kvp.Key;
                    var content = kvp.Value;

                    if (string.IsNullOrEmpty(filePath))
                        continue;

                    // 确保目录存在
                    var directory = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // 如果文件已存在且需要备份，创建备份
                    if (createBackups && File.Exists(filePath))
                    {
                        var backupPath = CreateBackup(filePath);
                        if (backupPath != null)
                        {
                            backups[filePath] = backupPath;
                        }
                    }

                    // 创建临时文件
                    var tempPath = filePath + TempExtension;
                    File.WriteAllText(tempPath, content);
                    tempFiles.Add(tempPath);
                }

                // 第二阶段：验证所有临时文件都写入成功
                foreach (var tempPath in tempFiles)
                {
                    if (!File.Exists(tempPath))
                    {
                        throw new Exception($"写入临时文件失败: {tempPath}");
                    }
                }

                // 第三阶段：替换目标文件
                foreach (var kvp in fileContents)
                {
                    var filePath = kvp.Key;
                    var tempPath = filePath + TempExtension;

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    File.Move(tempPath, filePath);
                }

                Debug.Log($"批量安全写入 {fileContents.Count} 个文件成功");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"批量安全写入文件失败: {ex.Message}");

                // 清理临时文件
                foreach (var tempPath in tempFiles)
                {
                    if (File.Exists(tempPath))
                    {
                        try
                        {
                            File.Delete(tempPath);
                        }
                        catch { /* 忽略删除临时文件时的错误 */ }
                    }
                }

                // 恢复备份
                foreach (var kvp in backups)
                {
                    var filePath = kvp.Key;
                    var backupPath = kvp.Value;

                    if (!File.Exists(filePath) && File.Exists(backupPath))
                    {
                        try
                        {
                            RestoreBackup(backupPath, filePath);
                        }
                        catch { /* 忽略恢复备份时的错误 */ }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 安全地删除文件（先创建备份，然后才删除）
        /// </summary>
        /// <param name="filePath">要删除的文件路径</param>
        /// <returns>是否成功删除</returns>
        public static bool SafeDeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Debug.LogWarning($"无法删除文件，文件不存在: {filePath}");
                return false;
            }

            try
            {
                // 创建备份
                var backupPath = CreateBackup(filePath);

                // 删除文件
                File.Delete(filePath);
                Debug.Log($"安全删除文件: {filePath}");

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"安全删除文件失败: {filePath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 清理旧备份文件
        /// </summary>
        public static void CleanupOldBackups()
        {
            try
            {
                var backupDirectory = GetBackupDirectory();
                if (!Directory.Exists(backupDirectory))
                    return;

                var now = DateTime.Now;
                var backupFiles = Directory.GetFiles(backupDirectory, $"*{BackupExtension}");

                foreach (var backupFile in backupFiles)
                {
                    try
                    {
                        var fileInfo = new FileInfo(backupFile);
                        if (now - fileInfo.CreationTime > SMaxBackupAge)
                        {
                            File.Delete(backupFile);
                            Debug.Log($"已删除过期备份: {backupFile}");
                        }
                    }
                    catch
                    {
                        // 忽略单个文件的清理错误
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"清理旧备份失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取目录下所有备份文件
        /// </summary>
        /// <returns>备份文件列表</returns>
        public static List<string> GetAllBackups()
        {
            try
            {
                var backupDirectory = GetBackupDirectory();
                if (!Directory.Exists(backupDirectory))
                    return new List<string>();

                return Directory.GetFiles(backupDirectory, $"*{BackupExtension}").ToList();
            }
            catch (Exception ex)
            {
                Debug.LogError($"获取备份文件失败: {ex.Message}");
                return new List<string>();
            }
        }

        /// <summary>
        /// 验证文件是否可写入
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否可写入</returns>
        public static bool IsFileWritable(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                // 检查目录是否存在且可写
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    return false;

                // 如果文件已存在，检查是否可写
                if (File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath);
                    return !fileInfo.IsReadOnly;
                }

                // 如果文件不存在，尝试创建临时文件
                var tempPath = filePath + TempExtension;
                using (var fs = File.Create(tempPath))
                {
                    fs.Close();
                }

                File.Delete(tempPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证路径是否在安全区域内（避免访问系统关键目录）
        /// </summary>
        /// <param name="path">要验证的路径</param>
        /// <param name="safeRoots">安全根目录列表，如果为null则使用默认值</param>
        /// <returns>是否在安全区域内</returns>
        public static bool IsPathInSafeZone(string path, List<string> safeRoots = null)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            try
            {
                // 规范化路径
                path = Path.GetFullPath(path).ToLowerInvariant();

                // 默认安全根目录列表
                if (safeRoots == null || safeRoots.Count == 0)
                {
                    safeRoots = new List<string>
                    {
                        Application.dataPath.ToLowerInvariant(), // Assets目录
                        Application.persistentDataPath.ToLowerInvariant(), // 持久化数据目录
                        Application.temporaryCachePath.ToLowerInvariant(), // 临时缓存目录
                        Path.GetTempPath().ToLowerInvariant() // 系统临时目录
                    };
                }

                // 检查路径是否在任一安全根目录下
                foreach (var safeRoot in safeRoots)
                {
                    if (string.IsNullOrEmpty(safeRoot))
                        continue;

                    var normalizedRoot = Path.GetFullPath(safeRoot).ToLowerInvariant();
                    if (path.StartsWith(normalizedRoot))
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 安全地移动文件（带备份和回滚功能）
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        /// <returns>是否移动成功</returns>
        public static bool SafeMoveFile(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destinationPath))
                return false;

            if (!File.Exists(sourcePath))
            {
                Debug.LogWarning($"无法移动文件，源文件不存在: {sourcePath}");
                return false;
            }

            string sourceBackupPath = null;
            string destBackupPath = null;

            try
            {
                // 创建源文件的备份
                sourceBackupPath = CreateBackup(sourcePath);

                // 如果目标文件已存在，创建备份
                if (File.Exists(destinationPath))
                {
                    destBackupPath = CreateBackup(destinationPath);
                }

                // 确保目标目录存在
                var destDirectory = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }

                // 移动文件
                File.Move(sourcePath, destinationPath);
                Debug.Log($"安全移动文件: {sourcePath} -> {destinationPath}");

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"安全移动文件失败: {sourcePath} -> {destinationPath}, 错误: {ex.Message}");

                // 尝试回滚
                var sourceMissing = !File.Exists(sourcePath);
                var destChanged = File.Exists(destinationPath);

                // 如果源文件丢失，尝试恢复
                if (sourceMissing && sourceBackupPath != null)
                {
                    try
                    {
                        RestoreBackup(sourceBackupPath, sourcePath);
                    }
                    catch { /* 忽略恢复备份时的错误 */ }
                }

                // 如果目标文件被修改，尝试恢复
                if (destChanged && destBackupPath != null)
                {
                    try
                    {
                        RestoreBackup(destBackupPath, destinationPath);
                    }
                    catch { /* 忽略恢复备份时的错误 */ }
                }

                return false;
            }
        }

        /// <summary>
        /// 将源目录中的所有文件安全地复制到目标目录
        /// </summary>
        /// <param name="sourceDirectory">源目录</param>
        /// <param name="targetDirectory">目标目录</param>
        /// <param name="recursive">是否递归复制子目录</param>
        /// <returns>是否全部复制成功</returns>
        public static bool SafeCopyDirectory(string sourceDirectory, string targetDirectory, bool recursive = true)
        {
            if (string.IsNullOrEmpty(sourceDirectory) || string.IsNullOrEmpty(targetDirectory))
                return false;

            if (!Directory.Exists(sourceDirectory))
            {
                Debug.LogWarning($"无法复制目录，源目录不存在: {sourceDirectory}");
                return false;
            }

            // 备份目标目录中的文件
            var backups = new Dictionary<string, string>();

            try
            {
                // 确保目标目录存在
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // 复制所有文件
                var files = Directory.GetFiles(sourceDirectory);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var targetFile = Path.Combine(targetDirectory, fileName);

                    // 如果目标文件已存在，创建备份
                    if (File.Exists(targetFile))
                    {
                        var backupPath = CreateBackup(targetFile);
                        if (backupPath != null)
                        {
                            backups[targetFile] = backupPath;
                        }
                    }

                    File.Copy(file, targetFile, true);
                }

                // 如果需要递归复制
                if (recursive)
                {
                    var directories = Directory.GetDirectories(sourceDirectory);
                    foreach (var directory in directories)
                    {
                        var dirName = Path.GetFileName(directory);
                        var targetSubDir = Path.Combine(targetDirectory, dirName);

                        if (!SafeCopyDirectory(directory, targetSubDir, true))
                        {
                            throw new Exception($"复制子目录失败: {directory}");
                        }
                    }
                }

                Debug.Log($"安全复制目录成功: {sourceDirectory} -> {targetDirectory}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"安全复制目录失败: {sourceDirectory} -> {targetDirectory}, 错误: {ex.Message}");

                // 尝试恢复备份
                foreach (var kvp in backups)
                {
                    var targetFile = kvp.Key;
                    var backupPath = kvp.Value;

                    try
                    {
                        RestoreBackup(backupPath, targetFile);
                    }
                    catch { /* 忽略恢复备份时的错误 */ }
                }

                return false;
            }
        }
    }
}
