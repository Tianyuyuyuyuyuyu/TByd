using System;

namespace TByd.CodeStyle.Runtime.Git
{
    /// <summary>
    /// Git钩子类型
    /// </summary>
    public enum GitHookType
    {
        /// <summary>
        /// 提交前钩子，在git commit命令执行前触发
        /// </summary>
        k_PreCommit,

        /// <summary>
        /// 提交消息钩子，在编辑提交消息时触发
        /// </summary>
        k_CommitMsg,

        /// <summary>
        /// 提交后钩子，在提交完成后触发
        /// </summary>
        k_PostCommit,

        /// <summary>
        /// 推送前钩子，在git push命令执行前触发
        /// </summary>
        k_PrePush,

        /// <summary>
        /// 推送后钩子，在推送完成后触发
        /// </summary>
        k_PostPush,

        /// <summary>
        /// 合并前钩子，在合并操作执行前触发
        /// </summary>
        k_PreMerge,

        /// <summary>
        /// 合并后钩子，在合并完成后触发
        /// </summary>
        k_PostMerge,

        /// <summary>
        /// 检出钩子，在git checkout命令执行后触发
        /// </summary>
        k_PostCheckout,

        /// <summary>
        /// 应用补丁前钩子，在git am命令执行前触发
        /// </summary>
        k_ApplyPatchMsg,

        /// <summary>
        /// 预接收钩子，在服务器接收推送前触发
        /// </summary>
        k_PreReceive,

        /// <summary>
        /// 更新钩子，在服务器更新引用前触发
        /// </summary>
        k_Update,

        /// <summary>
        /// 接收后钩子，在服务器接收推送后触发
        /// </summary>
        k_PostReceive,

        /// <summary>
        /// 更新后钩子，在服务器更新引用后触发
        /// </summary>
        k_PostUpdate,

        /// <summary>
        /// 引用更新钩子，在本地引用更新后触发
        /// </summary>
        k_PostRewrite,

        /// <summary>
        /// 准备提交消息钩子，在提交消息编辑器启动前触发
        /// </summary>
        k_PrepareCommitMsg
    }

    /// <summary>
    /// Git钩子类型扩展方法
    /// </summary>
    public static class GitHookTypeExtensions
    {
        /// <summary>
        /// 获取钩子文件名
        /// </summary>
        /// <param name="hookType">钩子类型</param>
        /// <returns>钩子文件名</returns>
        public static string GetFileName(this GitHookType hookType)
        {
            switch (hookType)
            {
                case GitHookType.k_PreCommit:
                    return "pre-commit";
                case GitHookType.k_CommitMsg:
                    return "commit-msg";
                case GitHookType.k_PostCommit:
                    return "post-commit";
                case GitHookType.k_PrePush:
                    return "pre-push";
                case GitHookType.k_PostPush:
                    return "post-push";
                case GitHookType.k_PreMerge:
                    return "pre-merge-commit";
                case GitHookType.k_PostMerge:
                    return "post-merge";
                case GitHookType.k_PostCheckout:
                    return "post-checkout";
                case GitHookType.k_ApplyPatchMsg:
                    return "applypatch-msg";
                case GitHookType.k_PreReceive:
                    return "pre-receive";
                case GitHookType.k_Update:
                    return "update";
                case GitHookType.k_PostReceive:
                    return "post-receive";
                case GitHookType.k_PostUpdate:
                    return "post-update";
                case GitHookType.k_PostRewrite:
                    return "post-rewrite";
                case GitHookType.k_PrepareCommitMsg:
                    return "prepare-commit-msg";
                default:
                    throw new ArgumentOutOfRangeException(nameof(hookType), hookType, "未知的Git钩子类型");
            }
        }

        /// <summary>
        /// 获取钩子描述
        /// </summary>
        /// <param name="hookType">钩子类型</param>
        /// <returns>钩子描述</returns>
        public static string GetDescription(this GitHookType hookType)
        {
            switch (hookType)
            {
                case GitHookType.k_PreCommit:
                    return "提交前钩子，在git commit命令执行前触发";
                case GitHookType.k_CommitMsg:
                    return "提交消息钩子，在编辑提交消息时触发";
                case GitHookType.k_PostCommit:
                    return "提交后钩子，在提交完成后触发";
                case GitHookType.k_PrePush:
                    return "推送前钩子，在git push命令执行前触发";
                case GitHookType.k_PostPush:
                    return "推送后钩子，在推送完成后触发";
                case GitHookType.k_PreMerge:
                    return "合并前钩子，在合并操作执行前触发";
                case GitHookType.k_PostMerge:
                    return "合并后钩子，在合并完成后触发";
                case GitHookType.k_PostCheckout:
                    return "检出钩子，在git checkout命令执行后触发";
                case GitHookType.k_ApplyPatchMsg:
                    return "应用补丁前钩子，在git am命令执行前触发";
                case GitHookType.k_PreReceive:
                    return "预接收钩子，在服务器接收推送前触发";
                case GitHookType.k_Update:
                    return "更新钩子，在服务器更新引用前触发";
                case GitHookType.k_PostReceive:
                    return "接收后钩子，在服务器接收推送后触发";
                case GitHookType.k_PostUpdate:
                    return "更新后钩子，在服务器更新引用后触发";
                case GitHookType.k_PostRewrite:
                    return "引用更新钩子，在本地引用更新后触发";
                case GitHookType.k_PrepareCommitMsg:
                    return "准备提交消息钩子，在提交消息编辑器启动前触发";
                default:
                    return "未知钩子类型";
            }
        }

        /// <summary>
        /// 从文件名获取钩子类型
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>钩子类型</returns>
        public static GitHookType GetHookTypeFromFileName(string fileName)
        {
            switch (fileName)
            {
                case "pre-commit":
                    return GitHookType.k_PreCommit;
                case "commit-msg":
                    return GitHookType.k_CommitMsg;
                case "post-commit":
                    return GitHookType.k_PostCommit;
                case "pre-push":
                    return GitHookType.k_PrePush;
                case "post-push":
                    return GitHookType.k_PostPush;
                case "pre-merge-commit":
                    return GitHookType.k_PreMerge;
                case "post-merge":
                    return GitHookType.k_PostMerge;
                case "post-checkout":
                    return GitHookType.k_PostCheckout;
                case "applypatch-msg":
                    return GitHookType.k_ApplyPatchMsg;
                case "pre-receive":
                    return GitHookType.k_PreReceive;
                case "update":
                    return GitHookType.k_Update;
                case "post-receive":
                    return GitHookType.k_PostReceive;
                case "post-update":
                    return GitHookType.k_PostUpdate;
                case "post-rewrite":
                    return GitHookType.k_PostRewrite;
                case "prepare-commit-msg":
                    return GitHookType.k_PrepareCommitMsg;
                default:
                    throw new ArgumentException($"未知的Git钩子文件名: {fileName}", nameof(fileName));
            }
        }
    }
}
