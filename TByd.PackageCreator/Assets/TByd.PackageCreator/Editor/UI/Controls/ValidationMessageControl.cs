using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Controls
{
    /// <summary>
    /// 验证消息控件，用于显示不同级别的验证消息
    /// </summary>
    public static class ValidationMessageControl
    {
        /// <summary>
        /// 绘制单个验证消息
        /// </summary>
        /// <param name="message">验证消息</param>
        public static void Draw(ValidationMessage message)
        {
            if (message == null) return;

            MessageType messageType = GetMessageType(message.Level);
            EditorGUILayout.HelpBox(message.Message, messageType);
        }

        /// <summary>
        /// 绘制验证消息列表
        /// </summary>
        /// <param name="messages">验证消息列表</param>
        /// <param name="title">标题（可选）</param>
        /// <param name="showIfEmpty">列表为空时是否显示标题</param>
        public static void DrawList(List<ValidationMessage> messages, string title = null, bool showIfEmpty = false)
        {
            if (messages == null || (messages.Count == 0 && !showIfEmpty))
                return;

            EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);

            if (!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
                GUILayout.Space(5);
            }

            if (messages.Count == 0)
            {
                EditorGUILayout.LabelField("没有验证消息", EditorStyles.miniLabel);
            }
            else
            {
                foreach (var message in messages)
                {
                    Draw(message);
                }
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制包含筛选功能的验证消息列表
        /// </summary>
        /// <param name="validationResult">验证结果</param>
        /// <param name="title">标题（可选）</param>
        /// <param name="showIfValid">验证通过时是否显示</param>
        public static void DrawValidationResult(ValidationResult validationResult, string title = "验证结果", bool showIfValid = false)
        {
            if (validationResult == null || (validationResult.Messages.Count == 0 && !showIfValid))
                return;

            EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);

            if (!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
                GUILayout.Space(5);
            }

            if (validationResult.IsValid)
            {
                if (showIfValid)
                {
                    EditorGUILayout.HelpBox("验证通过", MessageType.Info);
                }
            }
            else
            {
                // 分类显示错误和警告
                if (validationResult.HasErrors)
                {
                    foreach (var error in validationResult.GetMessages(ValidationMessageLevel.Error))
                    {
                        Draw(error);
                    }
                }

                if (validationResult.HasWarnings)
                {
                    foreach (var warning in validationResult.GetMessages(ValidationMessageLevel.Warning))
                    {
                        Draw(warning);
                    }
                }

                // 显示信息类消息
                foreach (var info in validationResult.GetMessages(ValidationMessageLevel.Info))
                {
                    Draw(info);
                }
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 根据验证消息级别获取对应的Unity消息类型
        /// </summary>
        private static MessageType GetMessageType(ValidationMessageLevel level)
        {
            switch (level)
            {
                case ValidationMessageLevel.Error:
                    return MessageType.Error;
                case ValidationMessageLevel.Warning:
                    return MessageType.Warning;
                case ValidationMessageLevel.Info:
                default:
                    return MessageType.Info;
            }
        }
    }
}
