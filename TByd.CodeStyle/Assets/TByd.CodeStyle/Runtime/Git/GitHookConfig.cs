using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TByd.CodeStyle.Runtime.Git
{
    /// <summary>
    /// Git钩子配置，用于管理Git钩子的配置
    /// </summary>
    [Serializable]
    public class GitHookConfig
    {
        /// <summary>
        /// 钩子配置项
        /// </summary>
        [Serializable]
        public class HookConfig
        {
            /// <summary>
            /// 钩子类型
            /// </summary>
            [FormerlySerializedAs("m_HookType")] [SerializeField]
            private GitHookType mHookType;

            /// <summary>
            /// 是否启用
            /// </summary>
            [FormerlySerializedAs("m_Enabled")] [SerializeField]
            private bool mEnabled = true;

            /// <summary>
            /// 自定义参数
            /// </summary>
            [SerializeField]
            private Dictionary<string, string> m_Parameters = new Dictionary<string, string>();

            /// <summary>
            /// 构造函数
            /// </summary>
            public HookConfig() { }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="hookType">钩子类型</param>
            /// <param name="enabled">是否启用</param>
            public HookConfig(GitHookType hookType, bool enabled = true)
            {
                mHookType = hookType;
                mEnabled = enabled;
            }

            /// <summary>
            /// 钩子类型
            /// </summary>
            public GitHookType HookType
            {
                get => mHookType;
                set => mHookType = value;
            }

            /// <summary>
            /// 是否启用
            /// </summary>
            public bool Enabled
            {
                get => mEnabled;
                set => mEnabled = value;
            }

            /// <summary>
            /// 自定义参数
            /// </summary>
            public Dictionary<string, string> Parameters
            {
                get => m_Parameters;
                set => m_Parameters = value;
            }

            /// <summary>
            /// 设置参数
            /// </summary>
            /// <param name="key">参数名</param>
            /// <param name="value">参数值</param>
            public void SetParameter(string key, string value)
            {
                if (m_Parameters.ContainsKey(key))
                {
                    m_Parameters[key] = value;
                }
                else
                {
                    m_Parameters.Add(key, value);
                }
            }

            /// <summary>
            /// 获取参数
            /// </summary>
            /// <param name="key">参数名</param>
            /// <param name="defaultValue">默认值</param>
            /// <returns>参数值</returns>
            public string GetParameter(string key, string defaultValue = "")
            {
                return m_Parameters.TryGetValue(key, out var value) ? value : defaultValue;
            }
        }

        /// <summary>
        /// 是否自动安装钩子
        /// </summary>
        [FormerlySerializedAs("m_AutoInstallHooks")] [SerializeField]
        private bool mAutoInstallHooks = true;

        /// <summary>
        /// 是否在编辑器启动时检查钩子状态
        /// </summary>
        [FormerlySerializedAs("m_CheckHooksOnStartup")] [SerializeField]
        private bool mCheckHooksOnStartup = true;

        /// <summary>
        /// 钩子配置列表
        /// </summary>
        [FormerlySerializedAs("m_HookConfigs")] [SerializeField]
        private List<HookConfig> mHookConfigs = new List<HookConfig>();

        /// <summary>
        /// 构造函数，初始化默认钩子配置
        /// </summary>
        public GitHookConfig()
        {
            InitDefaultHookConfigs();
        }

        /// <summary>
        /// 初始化默认钩子配置
        /// </summary>
        private void InitDefaultHookConfigs()
        {
            // 添加默认钩子配置
            mHookConfigs.Add(new HookConfig(GitHookType.k_PreCommit, true));
            mHookConfigs.Add(new HookConfig(GitHookType.k_CommitMsg, true));
            mHookConfigs.Add(new HookConfig(GitHookType.k_PrepareCommitMsg, true));
        }

        /// <summary>
        /// 是否自动安装钩子
        /// </summary>
        public bool AutoInstallHooks
        {
            get => mAutoInstallHooks;
            set => mAutoInstallHooks = value;
        }

        /// <summary>
        /// 是否在编辑器启动时检查钩子状态
        /// </summary>
        public bool CheckHooksOnStartup
        {
            get => mCheckHooksOnStartup;
            set => mCheckHooksOnStartup = value;
        }

        /// <summary>
        /// 是否启用Git钩子，用于测试兼容性
        /// </summary>
        public bool EnableGitHooks
        {
            get => mAutoInstallHooks;
            set => mAutoInstallHooks = value;
        }

        /// <summary>
        /// 钩子配置列表
        /// </summary>
        public List<HookConfig> HookConfigs
        {
            get => mHookConfigs;
            set => mHookConfigs = value;
        }

        /// <summary>
        /// 获取钩子配置
        /// </summary>
        /// <param name="hookType">钩子类型</param>
        /// <returns>钩子配置</returns>
        public HookConfig GetHookConfig(GitHookType hookType)
        {
            return mHookConfigs.Find(config => config.HookType == hookType);
        }

        /// <summary>
        /// 设置钩子启用状态
        /// </summary>
        /// <param name="hookType">钩子类型</param>
        /// <param name="enabled">是否启用</param>
        public void SetHookEnabled(GitHookType hookType, bool enabled)
        {
            var config = GetHookConfig(hookType);
            if (config != null)
            {
                config.Enabled = enabled;
            }
            else
            {
                mHookConfigs.Add(new HookConfig(hookType, enabled));
            }
        }

        /// <summary>
        /// 检查钩子是否启用
        /// </summary>
        /// <param name="hookType">钩子类型</param>
        /// <returns>是否启用</returns>
        public bool IsHookEnabled(GitHookType hookType)
        {
            var config = GetHookConfig(hookType);
            return config != null && config.Enabled;
        }
    }
}
