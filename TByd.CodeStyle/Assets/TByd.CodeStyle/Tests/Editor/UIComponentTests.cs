using NUnit.Framework;
using TByd.CodeStyle.Editor.UI.Components;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// UI组件测试类
    /// </summary>
    public class UIComponentTests
    {
        // 测试用UI组件
        private class TestUIComponent : UIComponent
        {
            public bool DrawContentCalled { get; private set; }

            public TestUIComponent(string _title, string _description = "", bool _isCollapsible = false)
                : base(_title, _description, _isCollapsible)
            {
                DrawContentCalled = false;
            }

            protected override void DrawContent()
            {
                DrawContentCalled = true;
            }

            public void ResetDrawContentCalled()
            {
                DrawContentCalled = false;
            }

            public bool GetIsVisible()
            {
                return m_IsVisible;
            }

            public void SetIsVisible(bool _isVisible)
            {
                m_IsVisible = _isVisible;
            }

            public bool GetIsCollapsed()
            {
                return m_IsCollapsed;
            }

            public void SetIsCollapsed(bool _isCollapsed)
            {
                m_IsCollapsed = _isCollapsed;
            }

            // 添加一个不调用GUI函数的测试方法
            public void TestDraw()
            {
                if (!m_IsVisible)
                    return;

                // 不调用EditorGUILayout.BeginVertical，直接测试逻辑
                if (!m_IsCollapsible || !m_IsCollapsed)
                {
                    DrawContent();
                }
            }
        }

        [Test]
        public void Constructor_SetsProperties()
        {
            // 创建测试组件
            var title = "测试标题";
            var description = "测试描述";
            var isCollapsible = true;

            var component = new TestUIComponent(title, description, isCollapsible);

            // 验证属性
            Assert.AreEqual(title, component.Title);
            Assert.AreEqual(description, component.Description);
            Assert.IsTrue(component.GetIsVisible());
            Assert.IsFalse(component.GetIsCollapsed());
        }

        [Test]
        public void Draw_WhenVisible_CallsDrawContent()
        {
            // 创建测试组件
            var component = new TestUIComponent("测试标题");
            component.SetIsVisible(true);
            component.ResetDrawContentCalled();

            // 使用不调用GUI函数的测试方法
            component.TestDraw();

            // 验证DrawContent被调用
            Assert.IsTrue(component.DrawContentCalled);
        }

        [Test]
        public void Draw_WhenNotVisible_DoesNotCallDrawContent()
        {
            // 创建测试组件
            var component = new TestUIComponent("测试标题");
            component.SetIsVisible(false);
            component.ResetDrawContentCalled();

            // 使用不调用GUI函数的测试方法
            component.TestDraw();

            // 验证DrawContent未被调用
            Assert.IsFalse(component.DrawContentCalled);
        }

        [Test]
        public void Draw_WhenCollapsible_TogglesFoldout()
        {
            // 创建测试组件
            var component = new TestUIComponent("测试标题", "", true);
            component.SetIsVisible(true);
            component.SetIsCollapsed(false);
            component.ResetDrawContentCalled();

            // 模拟点击折叠按钮
            var originalIsCollapsed = component.GetIsCollapsed();
            component.ToggleCollapsed();

            // 验证折叠状态已切换
            Assert.AreNotEqual(originalIsCollapsed, component.GetIsCollapsed());
        }

        [Test]
        public void InfoBox_Constructor_SetsProperties()
        {
            // 创建InfoBox
            var title = "信息标题";
            var message = "信息内容";
            var type = InfoBox.InfoType.Warning;

            var infoBox = new InfoBox(title, message, type);

            // 验证属性
            Assert.AreEqual(title, infoBox.Title);
            Assert.AreEqual(message, infoBox.Message);
            Assert.AreEqual(type, infoBox.Type);
        }
    }
}
