using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Utils
{
    /// <summary>
    /// 文件操作工具类，提供安全的文件读写、复制、删除等功能
    /// </summary>
    public static class FileUtils
    {
        private static readonly Encoding DefaultEncoding = new UTF8Encoding(false);

        /// <summary>
        /// 安全地创建目录，如果目录已存在则不会抛出异常
        /// </summary>
        /// <param name="directoryPath">要创建的目录路径</param>
        /// <returns>是否成功创建或目录已存在</returns>
        public static bool EnsureDirectoryExists(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Debug.Log($"创建目录: {directoryPath}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建目录失败: {directoryPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地删除目录，如果目录不存在则不会抛出异常
        /// </summary>
        /// <param name="directoryPath">要删除的目录路径</param>
        /// <param name="recursive">是否递归删除子目录和文件</param>
        /// <returns>是否成功删除或目录不存在</returns>
        public static bool SafeDeleteDirectory(string directoryPath, bool recursive = true)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, recursive);
                    Debug.Log($"删除目录: {directoryPath}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"删除目录失败: {directoryPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地写入文本文件，会自动创建目录
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="encoding">文件编码，默认为不带BOM的UTF8</param>
        /// <returns>是否成功写入</returns>
        public static bool WriteTextFile(string filePath, string content, Encoding encoding = null)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!EnsureDirectoryExists(directory))
                {
                    return false;
                }

                encoding = encoding ?? DefaultEncoding;
                File.WriteAllText(filePath, content, encoding);
                Debug.Log($"写入文件: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"写入文件失败: {filePath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地异步写入文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="encoding">文件编码，默认为不带BOM的UTF8</param>
        /// <returns>表示异步操作的任务</returns>
        public static async Task<bool> WriteTextFileAsync(string filePath, string content, Encoding encoding = null)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!EnsureDirectoryExists(directory))
                {
                    return false;
                }

                encoding = encoding ?? DefaultEncoding;
                using (var writer = new StreamWriter(filePath, false, encoding))
                {
                    await writer.WriteAsync(content);
                }
                Debug.Log($"异步写入文件: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"异步写入文件失败: {filePath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地读取文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">输出文件内容</param>
        /// <param name="encoding">文件编码，默认为不带BOM的UTF8</param>
        /// <returns>是否成功读取</returns>
        public static bool ReadTextFile(string filePath, out string content, Encoding encoding = null)
        {
            content = string.Empty;
            try
            {
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning($"文件不存在: {filePath}");
                    return false;
                }

                encoding = encoding ?? DefaultEncoding;
                content = File.ReadAllText(filePath, encoding);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"读取文件失败: {filePath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地异步读取文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">文件编码，默认为不带BOM的UTF8</param>
        /// <returns>表示异步操作的任务，任务结果为文件内容或null（如果读取失败）</returns>
        public static async Task<string> ReadTextFileAsync(string filePath, Encoding encoding = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning($"文件不存在: {filePath}");
                    return null;
                }

                encoding = encoding ?? DefaultEncoding;
                using (var reader = new StreamReader(filePath, encoding))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"异步读取文件失败: {filePath}, 错误: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 安全地复制文件，会自动创建目标目录
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        /// <returns>是否成功复制</returns>
        public static bool CopyFile(string sourcePath, string destinationPath, bool overwrite = true)
        {
            try
            {
                if (!File.Exists(sourcePath))
                {
                    Debug.LogWarning($"源文件不存在: {sourcePath}");
                    return false;
                }

                var directory = Path.GetDirectoryName(destinationPath);
                if (!EnsureDirectoryExists(directory))
                {
                    return false;
                }

                File.Copy(sourcePath, destinationPath, overwrite);
                Debug.Log($"复制文件: {sourcePath} -> {destinationPath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"复制文件失败: {sourcePath} -> {destinationPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全地删除文件，如果文件不存在则不会抛出异常
        /// </summary>
        /// <param name="filePath">要删除的文件路径</param>
        /// <returns>是否成功删除或文件不存在</returns>
        public static bool SafeDeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Debug.Log($"删除文件: {filePath}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"删除文件失败: {filePath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查路径是否为目录
        /// </summary>
        /// <param name="path">要检查的路径</param>
        /// <returns>如果是目录则返回true，否则返回false</returns>
        public static bool IsDirectory(string path)
        {
            try
            {
                var attr = File.GetAttributes(path);
                return (attr & FileAttributes.Directory) == FileAttributes.Directory;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查路径是否合法（不含非法字符）
        /// </summary>
        /// <param name="path">要检查的路径</param>
        /// <returns>如果合法则返回true，否则返回false</returns>
        public static bool IsValidPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                // 检查路径是否包含无效字符
                Path.GetFullPath(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取目录下所有文件（可选择是否递归）
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式（如"*.txt"）</param>
        /// <param name="recursive">是否递归搜索子目录</param>
        /// <returns>文件路径列表，如果目录不存在或发生错误则返回空列表</returns>
        public static List<string> GetFiles(string directoryPath, string searchPattern = "*.*", bool recursive = false)
        {
            var files = new List<string>();
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Debug.LogWarning($"目录不存在: {directoryPath}");
                    return files;
                }

                var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                files.AddRange(Directory.GetFiles(directoryPath, searchPattern, option));
            }
            catch (Exception ex)
            {
                Debug.LogError($"获取文件列表失败: {directoryPath}, 错误: {ex.Message}");
            }
            return files;
        }

        /// <summary>
        /// 检查文件是否是文本文件（通过尝试读取前几个字节来判断）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>如果可能是文本文件则返回true，否则返回false</returns>
        public static bool IsTextFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return false;
                }

                // 读取文件前8KB检查是否包含空字节（二进制文件通常包含空字节）
                var buffer = new byte[8192];
                using (var fs = File.OpenRead(filePath))
                {
                    var bytesRead = fs.Read(buffer, 0, buffer.Length);
                    for (var i = 0; i < bytesRead; i++)
                    {
                        if (buffer[i] == 0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 规范化路径（处理路径分隔符和相对路径）
        /// </summary>
        /// <param name="path">要规范化的路径</param>
        /// <returns>规范化后的路径</returns>
        public static string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            try
            {
                // 将所有路径分隔符转换为当前平台的分隔符
                var normalizedPath = path.Replace('\\', Path.DirectorySeparatorChar)
                                           .Replace('/', Path.DirectorySeparatorChar);

                // 解析相对路径
                return Path.GetFullPath(normalizedPath);
            }
            catch
            {
                // 如果无法解析，返回原始路径
                return path;
            }
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="basePath">基准路径</param>
        /// <param name="fullPath">完整路径</param>
        /// <returns>相对于基准路径的相对路径</returns>
        public static string GetRelativePath(string basePath, string fullPath)
        {
            try
            {
                // 确保路径有一致的格式
                basePath = NormalizePath(basePath);
                fullPath = NormalizePath(fullPath);

                if (!basePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    basePath += Path.DirectorySeparatorChar;
                }

                var baseUri = new Uri(basePath);
                var fullUri = new Uri(fullPath);

                var relativeUri = baseUri.MakeRelativeUri(fullUri);
                var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

                // 将URL风格的路径分隔符转换为平台特定的分隔符
                return relativePath.Replace('/', Path.DirectorySeparatorChar);
            }
            catch
            {
                // 如果无法计算相对路径，则返回完整路径
                return fullPath;
            }
        }
    }
}
