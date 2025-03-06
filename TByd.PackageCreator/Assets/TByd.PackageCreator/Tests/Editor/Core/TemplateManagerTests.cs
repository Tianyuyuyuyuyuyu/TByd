using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Editor.Core.Services;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Core
{
    /// <summary>
    /// 模板管理器测试
    /// </summary>
    public class TemplateManagerTests
    {
        private TemplateManager _manager;
        private TestTemplateProvider _testProvider;

        [SetUp]
        public void Setup()
        {
            _manager = TemplateManager.Instance;
            _testProvider = new TestTemplateProvider();
        }

        [Test]
        public void RegisterProvider_ValidProvider_SuccessfullyRegistered()
        {
            // 注册测试提供者
            _manager.RegisterProvider(_testProvider);

            // 验证提供者已注册
            var providers = _manager.GetRegisteredProviders();
            Assert.IsTrue(providers.Any(p => p.ProviderName == _testProvider.ProviderName));

            // 清理
            _manager.RemoveProvider(_testProvider.ProviderName);
        }

        [Test]
        public void GetAllTemplates_AfterRegisteringProvider_ReturnsTemplates()
        {
            // 注册测试提供者
            _manager.RegisterProvider(_testProvider);

            // 获取所有模板
            var templates = _manager.GetAllTemplates();

            // 验证模板数量
            Assert.IsTrue(templates.Count >= 1);

            // 验证模板内容
            var template = templates.FirstOrDefault(t => t.Id == "test.template");
            Assert.IsNotNull(template);
            Assert.AreEqual("测试模板", template.Name);

            // 清理
            _manager.RemoveProvider(_testProvider.ProviderName);
        }

        [Test]
        public void GetTemplateById_ExistingId_ReturnsTemplate()
        {
            // 注册测试提供者
            _manager.RegisterProvider(_testProvider);

            // 通过ID获取模板
            var template = _manager.GetTemplateById("test.template");

            // 验证模板内容
            Assert.IsNotNull(template);
            Assert.AreEqual("测试模板", template.Name);

            // 清理
            _manager.RemoveProvider(_testProvider.ProviderName);
        }

        [Test]
        public void GetTemplateById_NonExistingId_ReturnsNull()
        {
            // 通过不存在的ID获取模板
            var template = _manager.GetTemplateById("non.existing.template");

            // 验证返回null
            Assert.IsNull(template);
        }

        [Test]
        public void RemoveProvider_ExistingProvider_SuccessfullyRemoved()
        {
            // 注册测试提供者
            _manager.RegisterProvider(_testProvider);

            // 移除提供者
            bool result = _manager.RemoveProvider(_testProvider.ProviderName);

            // 验证移除成功
            Assert.IsTrue(result);

            // 验证提供者已移除
            var providers = _manager.GetRegisteredProviders();
            Assert.IsFalse(providers.Any(p => p.ProviderName == _testProvider.ProviderName));
        }

        [Test]
        public void RemoveProvider_NonExistingProvider_ReturnsFalse()
        {
            // 移除不存在的提供者
            bool result = _manager.RemoveProvider("non.existing.provider");

            // 验证移除失败
            Assert.IsFalse(result);
        }

        [Test]
        public void TemplateChanged_WhenRegisteringProvider_EventFired()
        {
            bool eventFired = false;
            TemplateChangedEventArgs eventArgs = null;

            // 订阅事件
            EventHandler<TemplateChangedEventArgs> handler = (sender, args) =>
            {
                eventFired = true;
                eventArgs = args;
            };

            _manager.TemplateChanged += handler;

            // 注册测试提供者
            _manager.RegisterProvider(_testProvider);

            // 验证事件已触发
            Assert.IsTrue(eventFired);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(TemplateChangeType.Reloaded, eventArgs.ChangeType);

            // 取消订阅
            _manager.TemplateChanged -= handler;

            // 清理
            _manager.RemoveProvider(_testProvider.ProviderName);
        }
    }

    /// <summary>
    /// 用于测试的模板提供者
    /// </summary>
    internal class TestTemplateProvider : ITemplateProvider
    {
        public string ProviderName => "TestTemplateProvider";
        public Version ProviderVersion => new Version(1, 0, 0);

        public IEnumerable<IPackageTemplate> GetTemplates()
        {
            yield return new TestTemplate();
        }
    }

    /// <summary>
    /// 用于测试的模板
    /// </summary>
    internal class TestTemplate : IPackageTemplate
    {
        public string Id => "test.template";
        public string Name => "测试模板";
        public string Description => "用于测试的模板";
        public string Version => "1.0.0";
        public string Author => "TByd Test";
        public Texture2D Icon => null;

        public IReadOnlyList<TemplateDirectory> Directories => new TemplateDirectory[]
        {
            new TemplateDirectory("Test", "测试目录")
        };

        public IReadOnlyList<TemplateFile> Files => new TemplateFile[]
        {
            new TemplateFile("test.txt", "测试内容", "测试文件")
        };

        public IReadOnlyList<TemplateOption> Options => new TemplateOption[]
        {
            new TemplateOption("testOption", "测试选项", "测试选项描述", TemplateOptionType.Boolean, "true")
        };

        public ValidationResult ValidateConfig(PackageConfig config)
        {
            return new ValidationResult();
        }

        public bool Generate(PackageConfig config, string targetPath)
        {
            return true;
        }

        public TemplatePreviewInfo GetPreviewInfo()
        {
            return new TemplatePreviewInfo("测试模板", "用于测试的模板");
        }
    }
}
