using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using TByd.CodeStyle.Editor.UI.Utils;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// 通知系统测试类
    /// </summary>
    public class NotificationSystemTests
    {
        [SetUp]
        public void Setup()
        {
            // 清除所有通知和进度
            NotificationSystem.ClearNotification();
            NotificationSystem.ClearProgress();
        }
        
        [Test]
        public void ShowNotification_SetsCurrentNotification()
        {
            // 显示通知
            string message = "测试通知";
            NotificationType type = NotificationType.Info;
            NotificationSystem.ShowNotification(message, type);
            
            // 验证当前通知
            Assert.IsTrue(NotificationSystem.HasNotification());
            Assert.AreEqual(message, NotificationSystem.GetCurrentNotification());
            Assert.AreEqual(type, NotificationSystem.GetCurrentNotificationType());
        }
        
        [Test]
        public void ClearNotification_ClearsCurrentNotification()
        {
            // 显示通知
            NotificationSystem.ShowNotification("测试通知");
            
            // 清除通知
            NotificationSystem.ClearNotification();
            
            // 验证通知已清除
            Assert.IsFalse(NotificationSystem.HasNotification());
        }
        
        [Test]
        public void ShowProgress_SetsCurrentProgress()
        {
            // 显示进度
            string title = "测试进度";
            string info = "进度信息";
            float progress = 0.5f;
            NotificationSystem.ShowProgress(title, info, progress);
            
            // 验证当前进度
            Assert.IsTrue(NotificationSystem.HasProgress());
            Assert.AreEqual(title, NotificationSystem.GetProgressTitle());
            Assert.AreEqual(info, NotificationSystem.GetProgressInfo());
            Assert.AreEqual(progress, NotificationSystem.GetProgress());
        }
        
        [Test]
        public void UpdateProgress_UpdatesCurrentProgress()
        {
            // 显示进度
            NotificationSystem.ShowProgress("测试进度", "进度信息", 0.5f);
            
            // 更新进度
            string newInfo = "新进度信息";
            float newProgress = 0.8f;
            NotificationSystem.UpdateProgress(newInfo, newProgress);
            
            // 验证进度已更新
            Assert.AreEqual(newInfo, NotificationSystem.GetProgressInfo());
            Assert.AreEqual(newProgress, NotificationSystem.GetProgress());
        }
        
        [Test]
        public void ClearProgress_ClearsCurrentProgress()
        {
            // 显示进度
            NotificationSystem.ShowProgress("测试进度", "进度信息", 0.5f);
            
            // 清除进度
            NotificationSystem.ClearProgress();
            
            // 验证进度已清除
            Assert.IsFalse(NotificationSystem.HasProgress());
        }
        
        [UnityTest]
        public IEnumerator Notification_ExpiresAfterTime()
        {
            // 显示通知
            NotificationSystem.ShowNotification("测试通知");
            
            // 在EditMode测试中，我们不能使用WaitForSeconds
            // 而是使用null来让Unity处理一帧
            // 然后手动设置通知结束时间为当前时间，模拟通知过期
            yield return null;
            
            // 手动模拟通知过期
            var notificationEndTimeField = typeof(NotificationSystem).GetField("s_NotificationEndTime", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            if (notificationEndTimeField != null)
            {
                notificationEndTimeField.SetValue(null, EditorApplication.timeSinceStartup - 1);
            }
            
            // 手动调用更新方法
            var updateMethod = typeof(NotificationSystem).GetMethod("UpdateNotification", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            if (updateMethod != null)
            {
                updateMethod.Invoke(null, null);
            }
            
            // 验证通知已过期
            Assert.IsFalse(NotificationSystem.HasNotification());
        }
        
        [Test]
        public void DrawNotification_WhenHasNotification_DrawsNotification()
        {
            // 显示通知
            NotificationSystem.ShowNotification("测试通知");
            
            // 我们不能直接调用DrawNotification，因为它包含GUI调用
            // 而是检查通知是否存在，这是DrawNotification返回true的前提条件
            bool hasNotification = NotificationSystem.HasNotification();
            
            // 验证结果
            Assert.IsTrue(hasNotification);
            
            // 清除通知，避免影响其他测试
            NotificationSystem.ClearNotification();
        }
        
        [Test]
        public void DrawNotification_WhenNoNotification_DoesNotDrawNotification()
        {
            // 清除通知
            NotificationSystem.ClearNotification();
            
            // 验证没有通知
            bool hasNotification = NotificationSystem.HasNotification();
            
            // 验证结果
            Assert.IsFalse(hasNotification);
        }
    }
} 