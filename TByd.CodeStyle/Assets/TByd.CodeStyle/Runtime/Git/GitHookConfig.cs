using System;
using System.Collections.Generic;
using UnityEngine;

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
            [SerializeField]
            private GitHookType m_HookType;

            /// <summary>
            /// 是否启用
            /// </summary>
            [SerializeField]
            private bool m_Enabled = true;

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
            /// <param name="_hookType">钩子类型</param>
            /// <param name="_enabled">是否启用</param>
            public HookConfig(GitHookType _hookType, bool _enabled = true)
            {
                m_HookType = _hookType;
                m_Enabled = _enabled;
            }

            /// <summary>
            /// 钩子类型
            /// </summary>
            public GitHookType HookType
            {
                get => m_HookType;
                set => m_HookType = value;
            }

            /// <summary>
            /// 是否启用
            /// </summary>
            public bool Enabled
            {
                get => m_Enabled;
                set => m_Enabled = value;
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
            /// <param name="_key">参数名</param>
            /// <param name="_value">参数值</param>
            public void SetParameter(string _key, string _value)
            {
                if (m_Parameters.ContainsKey(_key))
                {
                    m_Parameters[_key] = _value;
                }
                else
                {
                    m_Parameters.Add(_key, _value);
                }
            }

            /// <summary>
            /// 获取参数
            /// </summary>
            /// <param name="_key">参数名</param>
            /// <param name="_defaultValue">默认值</param>
            /// <returns>参数值</returns>
            public string GetParameter(string _key, string _defaultValue = "")
            {
                return m_Parameters.TryGetValue(_key, out var value) ? value : _defaultValue;
            }
        }

        /// <summary>
        /// 是否自动安装钩子
        /// </summary>
        [SerializeField]
        private bool m_AutoInstallHooks = true;

        /// <summary>
        /// 是否在编辑器启动时检查钩子状态
        /// </summary>
        [SerializeField]
        private bool m_CheckHooksOnStartup = true;

        /// <summary>
        /// 钩子配置列表
        /// </summary>
        [SerializeField]
        private List<HookConfig> m_HookConfigs = new List<HookConfig>();

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
            m_HookConfigs.Add(new HookConfig(GitHookType.PreCommit, true));
            m_HookConfigs.Add(new HookConfig(GitHookType.CommitMsg, true));
            m_HookConfigs.Add(new HookConfig(GitHookType.PrepareCommitMsg, true));
        }

        /// <summary>
        /// 是否自动安装钩子
        /// </summary>
        public bool AutoInstallHooks
        {
            get => m_AutoInstallHooks;
            set => m_AutoInstallHooks = value;
        }

        /// <summary>
        /// 是否在编辑器启动时检查钩子状态
        /// </summary>
        public bool CheckHooksOnStartup
        {
            get => m_CheckHooksOnStartup;
            set => m_CheckHooksOnStartup = value;
        }

        /// <summary>
        /// 是否启用Git钩子，用于测试兼容性
        /// </summary>
        public bool EnableGitHooks
        {
            get => m_AutoInstallHooks;
            set => m_AutoInstallHooks = value;
        }

        /// <summary>
        /// 钩子配置列表
        /// </summary>
        public List<HookConfig> HookConfigs
        {
            get => m_HookConfigs;
            set => m_HookConfigs = value;
        }

        /// <summary>
        /// 获取钩子配置
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>钩子配置</returns>
        public HookConfig GetHookConfig(GitHookType _hookType)
        {
            return m_HookConfigs.Find(config => config.HookType == _hookType);
        }

        /// <summary>
        /// 设置钩子启用状态
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <param name="_enabled">是否启用</param>
        public void SetHookEnabled(GitHookType _hookType, bool _enabled)
        {
            var config = GetHookConfig(_hookType);
            if (config != null)
            {
                config.Enabled = _enabled;
            }
            else
            {
                m_HookConfigs.Add(new HookConfig(_hookType, _enabled));
            }
        }

        /// <summary>
        /// 检查钩子是否启用
        /// </summary>
        /// <param name="_hookType">钩子类型</param>
        /// <returns>是否启用</returns>
        public bool IsHookEnabled(GitHookType _hookType)
        {
            var config = GetHookConfig(_hookType);
            return config != null && config.Enabled;
        }
    }
}
