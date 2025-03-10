using System;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// UI状态管理器，处理UI状态和数据流
    /// </summary>
    public class UIStateManager
    {
        // 单例实例
        private static UIStateManager _instance;
        public static UIStateManager Instance => _instance ?? (_instance = new UIStateManager());

        // 私有构造函数，确保单例模式
        private UIStateManager()
        {
            CreationState = new PackageCreationState();
        }

        // 全局状态
        public PackageCreationState CreationState { get; private set; }

        // 状态变更事件
        public event Action<PackageCreationState> OnStateChanged;

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="updateAction">更新操作</param>
        public void UpdateState(Action<PackageCreationState> updateAction)
        {
            if (updateAction != null)
            {
                updateAction.Invoke(CreationState);
                OnStateChanged?.Invoke(CreationState);
            }
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        public void ResetState()
        {
            CreationState = new PackageCreationState();
            OnStateChanged?.Invoke(CreationState);
        }
    }

    /// <summary>
    /// 包创建状态，存储整个创建过程的状态数据
    /// </summary>
    public class PackageCreationState
    {
        // 选中的模板
        public IPackageTemplate SelectedTemplate { get; set; }

        // 包配置
        public PackageConfig PackageConfig { get; set; } = new PackageConfig();

        // 验证结果
        public ValidationResult ValidationResult { get; set; }

        // 创建结果
        public bool IsCreationSuccessful { get; set; }

        // 是否正在创建
        public bool IsCreating { get; set; }

        // 错误信息
        public string ErrorMessage { get; set; }

        // 当前步骤
        public int CurrentStep { get; set; }

        // 是否处于编辑模式
        public bool IsEditMode { get; set; }

        // 创建进度（0-1）
        public float CreationProgress { get; set; }
    }
}
