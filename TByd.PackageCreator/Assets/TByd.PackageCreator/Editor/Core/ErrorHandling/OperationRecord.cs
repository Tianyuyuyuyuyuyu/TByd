using System;

namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 操作记录类，用于存储可回滚的操作信息
    /// </summary>
    public class OperationRecord
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 操作目标路径
        /// </summary>
        public string TargetPath { get; set; }

        /// <summary>
        /// 操作发生的时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 操作的备份数据，用于回滚（如文件原始内容）
        /// </summary>
        public byte[] BackupData { get; set; }

        /// <summary>
        /// 操作的源路径（适用于移动、复制等操作）
        /// </summary>
        public string SourcePath { get; set; }

        /// <summary>
        /// 操作的附加信息
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// 自定义回滚处理函数
        /// </summary>
        public Func<OperationRecord, bool> CustomRollbackHandler { get; set; }

        /// <summary>
        /// 操作记录构造函数
        /// </summary>
        public OperationRecord()
        {
            Timestamp = DateTime.Now;
            OperationId = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 创建基本的操作记录
        /// </summary>
        /// <param name="operationType">操作类型</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>操作记录实例</returns>
        public static OperationRecord Create(OperationType operationType, string targetPath)
        {
            return new OperationRecord
            {
                OperationType = operationType,
                TargetPath = targetPath
            };
        }

        /// <summary>
        /// 创建移动或复制操作的记录
        /// </summary>
        /// <param name="operationType">操作类型（应为Move或Copy）</param>
        /// <param name="sourcePath">源路径</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>操作记录实例</returns>
        public static OperationRecord CreateMoveOrCopy(OperationType operationType, string sourcePath, string targetPath)
        {
            if (operationType != OperationType.Move && operationType != OperationType.Copy)
            {
                throw new ArgumentException("Operation type must be Move or Copy", nameof(operationType));
            }

            return new OperationRecord
            {
                OperationType = operationType,
                SourcePath = sourcePath,
                TargetPath = targetPath
            };
        }

        /// <summary>
        /// 创建带有自定义回滚处理函数的操作记录
        /// </summary>
        /// <param name="operationType">操作类型</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="customRollbackHandler">自定义回滚处理函数</param>
        /// <returns>操作记录实例</returns>
        public static OperationRecord CreateWithCustomHandler(OperationType operationType, string targetPath, Func<OperationRecord, bool> customRollbackHandler)
        {
            return new OperationRecord
            {
                OperationType = operationType,
                TargetPath = targetPath,
                CustomRollbackHandler = customRollbackHandler ?? throw new ArgumentNullException(nameof(customRollbackHandler))
            };
        }

        /// <summary>
        /// 将操作记录转换为易读的字符串
        /// </summary>
        /// <returns>包含操作记录详情的字符串</returns>
        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{OperationType}] Target: {TargetPath}" +
                   (string.IsNullOrEmpty(SourcePath) ? "" : $", Source: {SourcePath}");
        }
    }
}
