using System;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.UI.Utils
{
    /// <summary>
    /// 通知类型
    /// </summary>
    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Success
    }
    
    /// <summary>
    /// 通知系统，用于在编辑器中显示通知和进度
    /// </summary>
    public static class NotificationSystem
    {
        // 通知显示时间（秒）
        private const float c_NotificationDisplayTime = 5f;
        
        // 当前通知
        private static string s_CurrentNotification;
        private static NotificationType s_CurrentNotificationType;
        private static double s_NotificationEndTime;
        
        // 当前进度
        private static string s_ProgressTitle;
        private static string s_ProgressInfo;
        private static float s_Progress;
        private static bool s_IsProgressVisible;
        
        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="_message">通知消息</param>
        /// <param name="_type">通知类型</param>
        public static void ShowNotification(string _message, NotificationType _type = NotificationType.Info)
        {
            s_CurrentNotification = _message;
            s_CurrentNotificationType = _type;
            s_NotificationEndTime = EditorApplication.timeSinceStartup + c_NotificationDisplayTime;
            
            // 确保重绘编辑器窗口
            EditorApplication.update -= UpdateNotification;
            EditorApplication.update += UpdateNotification;
            
            Debug.Log($"[TByd.CodeStyle] {GetNotificationTypePrefix(_type)}{_message}");
        }
        
        /// <summary>
        /// 清除当前通知
        /// </summary>
        public static void ClearNotification()
        {
            s_CurrentNotification = null;
            EditorApplication.update -= UpdateNotification;
        }
        
        /// <summary>
        /// 检查是否有通知
        /// </summary>
        /// <returns>是否有通知</returns>
        public static bool HasNotification()
        {
            return !string.IsNullOrEmpty(s_CurrentNotification) && 
                   EditorApplication.timeSinceStartup <= s_NotificationEndTime;
        }
        
        /// <summary>
        /// 获取当前通知内容
        /// </summary>
        /// <returns>通知内容</returns>
        public static string GetCurrentNotification()
        {
            return s_CurrentNotification;
        }
        
        /// <summary>
        /// 获取当前通知类型
        /// </summary>
        /// <returns>通知类型</returns>
        public static NotificationType GetCurrentNotificationType()
        {
            return s_CurrentNotificationType;
        }
        
        /// <summary>
        /// 显示进度条
        /// </summary>
        /// <param name="_title">进度标题</param>
        /// <param name="_info">进度信息</param>
        /// <param name="_progress">进度值（0-1）</param>
        public static void ShowProgress(string _title, string _info, float _progress)
        {
            s_ProgressTitle = _title;
            s_ProgressInfo = _info;
            s_Progress = Mathf.Clamp01(_progress);
            s_IsProgressVisible = true;
            
            EditorUtility.DisplayProgressBar(s_ProgressTitle, s_ProgressInfo, s_Progress);
        }
        
        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="_info">进度信息</param>
        /// <param name="_progress">进度值（0-1）</param>
        public static void UpdateProgress(string _info, float _progress)
        {
            s_ProgressInfo = _info;
            s_Progress = Mathf.Clamp01(_progress);
            
            if (s_IsProgressVisible)
            {
                EditorUtility.DisplayProgressBar(s_ProgressTitle, s_ProgressInfo, s_Progress);
            }
        }
        
        /// <summary>
        /// 隐藏进度条
        /// </summary>
        public static void HideProgress()
        {
            if (s_IsProgressVisible)
            {
                EditorUtility.ClearProgressBar();
                s_IsProgressVisible = false;
            }
        }
        
        /// <summary>
        /// 清除进度条
        /// </summary>
        public static void ClearProgress()
        {
            HideProgress();
            s_ProgressTitle = null;
            s_ProgressInfo = null;
            s_Progress = 0f;
        }
        
        /// <summary>
        /// 检查是否有进度条
        /// </summary>
        /// <returns>是否有进度条</returns>
        public static bool HasProgress()
        {
            return s_IsProgressVisible;
        }
        
        /// <summary>
        /// 获取进度条标题
        /// </summary>
        /// <returns>进度条标题</returns>
        public static string GetProgressTitle()
        {
            return s_ProgressTitle;
        }
        
        /// <summary>
        /// 获取进度条信息
        /// </summary>
        /// <returns>进度条信息</returns>
        public static string GetProgressInfo()
        {
            return s_ProgressInfo;
        }
        
        /// <summary>
        /// 获取当前进度值
        /// </summary>
        /// <returns>进度值（0-1）</returns>
        public static float GetProgress()
        {
            return s_Progress;
        }
        
        /// <summary>
        /// 绘制通知
        /// </summary>
        public static void DrawNotification()
        {
            if (string.IsNullOrEmpty(s_CurrentNotification) || 
                EditorApplication.timeSinceStartup > s_NotificationEndTime)
                return;
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = GetNotificationColor(s_CurrentNotificationType);
            style.fontStyle = FontStyle.Bold;
            
            EditorGUILayout.LabelField(GetNotificationTypePrefix(s_CurrentNotificationType) + s_CurrentNotification, style);
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制通知
        /// </summary>
        /// <param name="_rect">绘制区域</param>
        /// <returns>是否绘制了通知</returns>
        public static bool DrawNotification(Rect _rect)
        {
            if (string.IsNullOrEmpty(s_CurrentNotification) || 
                EditorApplication.timeSinceStartup > s_NotificationEndTime)
                return false;
            
            GUIStyle style = new GUIStyle(EditorStyles.helpBox);
            style.normal.textColor = GetNotificationColor(s_CurrentNotificationType);
            style.fontStyle = FontStyle.Bold;
            style.padding = new RectOffset(10, 10, 5, 5);
            
            GUI.Box(_rect, GetNotificationTypePrefix(s_CurrentNotificationType) + s_CurrentNotification, style);
            
            return true;
        }
        
        /// <summary>
        /// 更新通知状态
        /// </summary>
        private static void UpdateNotification()
        {
            if (EditorApplication.timeSinceStartup > s_NotificationEndTime)
            {
                s_CurrentNotification = null;
                EditorApplication.update -= UpdateNotification;
                
                // 重绘编辑器窗口
                EditorApplication.RepaintProjectWindow();
            }
        }
        
        /// <summary>
        /// 获取通知类型前缀
        /// </summary>
        /// <param name="_type">通知类型</param>
        /// <returns>前缀字符串</returns>
        private static string GetNotificationTypePrefix(NotificationType _type)
        {
            switch (_type)
            {
                case NotificationType.Info:
                    return "[信息] ";
                case NotificationType.Warning:
                    return "[警告] ";
                case NotificationType.Error:
                    return "[错误] ";
                case NotificationType.Success:
                    return "[成功] ";
                default:
                    return string.Empty;
            }
        }
        
        /// <summary>
        /// 获取通知类型颜色
        /// </summary>
        /// <param name="_type">通知类型</param>
        /// <returns>颜色</returns>
        private static Color GetNotificationColor(NotificationType _type)
        {
            switch (_type)
            {
                case NotificationType.Info:
                    return Color.white;
                case NotificationType.Warning:
                    return Color.yellow;
                case NotificationType.Error:
                    return Color.red;
                case NotificationType.Success:
                    return Color.green;
                default:
                    return Color.white;
            }
        }
    }
} 