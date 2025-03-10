using System.Collections.Generic;
using System.Linq;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Controls;
using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Windows
{
    /// <summary>
    /// 包创建器主窗口
    /// </summary>
    public class PackageCreatorWindow : EditorWindow
    {
        #region 常量

        // 窗口尺寸
        private const float MinWindowWidth = 800f;
        private const float MinWindowHeight = 600f;
        private const float PreferredWindowWidth = 900f;
        private const float PreferredWindowHeight = 700f;

        // 菜单路径
        private const string MenuPath = "Tools/TByd/UPM Package Creator";
        private const string WindowTitle = "UPM Package Creator";

        #endregion

        #region 菜单项和初始化

        /// <summary>
        /// 打开窗口的菜单项
        /// </summary>
        [MenuItem(MenuPath, false, 100)]
        public static void ShowWindow()
        {
            // 创建并显示窗口
            var window = GetWindow<PackageCreatorWindow>(false, WindowTitle, true);
            window.minSize = new Vector2(MinWindowWidth, MinWindowHeight);
            window.position = new Rect(
                (Screen.currentResolution.width - PreferredWindowWidth) / 2,
                (Screen.currentResolution.height - PreferredWindowHeight) / 2,
                PreferredWindowWidth,
                PreferredWindowHeight);
        }

        #endregion
    }
}
