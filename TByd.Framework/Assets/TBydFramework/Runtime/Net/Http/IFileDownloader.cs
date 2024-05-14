using System;
using System.IO;
using TBydFramework.Runtime.Asynchronous;

namespace TBydFramework.Runtime.Net.Http
{
    public interface IFileDownloader
    {
        IProgressResult<ProgressInfo, FileInfo> DownloadFileAsync(Uri path, string fileName);

        IProgressResult<ProgressInfo, FileInfo> DownloadFileAsync(Uri path, FileInfo fileInfo);

        IProgressResult<ProgressInfo, ResourceInfo[]> DownloadFileAsync(ResourceInfo[] infos);
    }

    public class ResourceInfo
    {
        public ResourceInfo(Uri path, FileInfo fileInfo) : this(path, fileInfo, -1)
        {
        }

        public ResourceInfo(Uri path, FileInfo fileInfo, long fileSize)
        {
            this.Path = path;
            this.FileInfo = fileInfo;
            this.FileSize = fileSize;
        }

        public Uri Path { get; private set; }

        public FileInfo FileInfo { get; private set; }

        public long FileSize { get; set; }
    }
}
