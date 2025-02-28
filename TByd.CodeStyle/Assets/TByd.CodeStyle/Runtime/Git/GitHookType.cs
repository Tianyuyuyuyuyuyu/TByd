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
        PreCommit,
        
        /// <summary>
        /// 提交消息钩子，在编辑提交消息时触发
        /// </summary>
        CommitMsg,
        
        /// <summary>
        /// 提交后钩子，在提交完成后触发
        /// </summary>
        PostCommit,
        
        /// <summary>
        /// 推送前钩子，在git push命令执行前触发
        /// </summary>
        PrePush,
        
        /// <summary>
        /// 推送后钩子，在推送完成后触发
        /// </summary>
        PostPush,
        
        /// <summary>
        /// 合并前钩子，在合并操作执行前触发
        /// </summary>
        PreMerge,
        
        /// <summary>
        /// 合并后钩子，在合并完成后触发
        /// </summary>
        PostMerge,
        
        /// <summary>
        /// 检出钩子，在git checkout命令执行后触发
        /// </summary>
        PostCheckout,
        
        /// <summary>
        /// 应用补丁前钩子，在git am命令执行前触发
        /// </summary>
        ApplyPatchMsg,
        
        /// <summary>
        /// 预接收钩子，在服务器接收推送前触发
        /// </summary>
        PreReceive,
        
        /// <summary>
        /// 更新钩子，在服务器更新引用前触发
        /// </summary>
        Update,
        
        /// <summary>
        /// 接收后钩子，在服务器接收推送后触发
        /// </summary>
        PostReceive,
        
        /// <summary>
        /// 更新后钩子，在服务器更新引用后触发
        /// </summary>
        PostUpdate,
        
        /// <summary>
        /// 引用更新钩子，在本地引用更新后触发
        /// </summary>
        PostRewrite,
        
        /// <summary>
        /// 准备提交消息钩子，在提交消息编辑器启动前触发
        /// </summary>
        PrepareCommitMsg
    }
    
    /// <summary>
    /// Git钩子类型扩展方法
    /// </summary>
    public static class GitHookTypeExtensions
    {
        /// <summary>
        /// 获取钩子文件名
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>钩子文件名</returns>
        public static string GetFileName(this GitHookType _hookType)
        {
            switch (_hookType)
            {
                case GitHookType.PreCommit:
                    return "pre-commit";
                case GitHookType.CommitMsg:
                    return "commit-msg";
                case GitHookType.PostCommit:
                    return "post-commit";
                case GitHookType.PrePush:
                    return "pre-push";
                case GitHookType.PostPush:
                    return "post-push";
                case GitHookType.PreMerge:
                    return "pre-merge-commit";
                case GitHookType.PostMerge:
                    return "post-merge";
                case GitHookType.PostCheckout:
                    return "post-checkout";
                case GitHookType.ApplyPatchMsg:
                    return "applypatch-msg";
                case GitHookType.PreReceive:
                    return "pre-receive";
                case GitHookType.Update:
                    return "update";
                case GitHookType.PostReceive:
                    return "post-receive";
                case GitHookType.PostUpdate:
                    return "post-update";
                case GitHookType.PostRewrite:
                    return "post-rewrite";
                case GitHookType.PrepareCommitMsg:
                    return "prepare-commit-msg";
                default:
                    throw new ArgumentOutOfRangeException(nameof(_hookType), _hookType, "未知的Git钩子类型");
            }
        }
        
        /// <summary>
        /// 获取钩子描述
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>钩子描述</returns>
        public static string GetDescription(this GitHookType _hookType)
        {
            switch (_hookType)
            {
                case GitHookType.PreCommit:
                    return "提交前钩子，在git commit命令执行前触发";
                case GitHookType.CommitMsg:
                    return "提交消息钩子，在编辑提交消息时触发";
                case GitHookType.PostCommit:
                    return "提交后钩子，在提交完成后触发";
                case GitHookType.PrePush:
                    return "推送前钩子，在git push命令执行前触发";
                case GitHookType.PostPush:
                    return "推送后钩子，在推送完成后触发";
                case GitHookType.PreMerge:
                    return "合并前钩子，在合并操作执行前触发";
                case GitHookType.PostMerge:
                    return "合并后钩子，在合并完成后触发";
                case GitHookType.PostCheckout:
                    return "检出钩子，在git checkout命令执行后触发";
                case GitHookType.ApplyPatchMsg:
                    return "应用补丁前钩子，在git am命令执行前触发";
                case GitHookType.PreReceive:
                    return "预接收钩子，在服务器接收推送前触发";
                case GitHookType.Update:
                    return "更新钩子，在服务器更新引用前触发";
                case GitHookType.PostReceive:
                    return "接收后钩子，在服务器接收推送后触发";
                case GitHookType.PostUpdate:
                    return "更新后钩子，在服务器更新引用后触发";
                case GitHookType.PostRewrite:
                    return "引用更新钩子，在本地引用更新后触发";
                case GitHookType.PrepareCommitMsg:
                    return "准备提交消息钩子，在提交消息编辑器启动前触发";
                default:
                    return "未知钩子类型";
            }
        }
        
        /// <summary>
        /// 从文件名获取钩子类型
        /// </summary>
        /// <param name="_fileName">文件名</param>
        /// <returns>钩子类型</returns>
        public static GitHookType GetHookTypeFromFileName(string _fileName)
        {
            switch (_fileName)
            {
                case "pre-commit":
                    return GitHookType.PreCommit;
                case "commit-msg":
                    return GitHookType.CommitMsg;
                case "post-commit":
                    return GitHookType.PostCommit;
                case "pre-push":
                    return GitHookType.PrePush;
                case "post-push":
                    return GitHookType.PostPush;
                case "pre-merge-commit":
                    return GitHookType.PreMerge;
                case "post-merge":
                    return GitHookType.PostMerge;
                case "post-checkout":
                    return GitHookType.PostCheckout;
                case "applypatch-msg":
                    return GitHookType.ApplyPatchMsg;
                case "pre-receive":
                    return GitHookType.PreReceive;
                case "update":
                    return GitHookType.Update;
                case "post-receive":
                    return GitHookType.PostReceive;
                case "post-update":
                    return GitHookType.PostUpdate;
                case "post-rewrite":
                    return GitHookType.PostRewrite;
                case "prepare-commit-msg":
                    return GitHookType.PrepareCommitMsg;
                default:
                    throw new ArgumentException($"未知的Git钩子文件名: {_fileName}", nameof(_fileName));
            }
        }
    }
} 