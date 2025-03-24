using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace TByd.Tools.VersionControl.Editor
{
    public class Tools
    {
        #region 右键菜单

        [MenuItem("Assets/Git/Commit", false, 0)]
        public static async void Commit()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:commit /path:" + GetSelection() + " /closeonend:0");
        }

        [MenuItem("Assets/Git/Pull", false, 1)]
        public static async void Pull()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:pull /path:" + GetSelection() + " /closeonend:0 /noquestion /branch:master");
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Git/Push", false, 2)]
        public static async void Push()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:push /path:" + GetSelection() + " /closeonend:0 /noquestion /branch:master");
        }

        [MenuItem("Assets/Git/Revert", false, 3)]
        public static async void Revert()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:revert /path:" + GetSelection() + " /closeonend:0");
        }

        [MenuItem("Assets/Git/Log", false, 51)]
        public static async void Log()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:log /path:" + GetSelection() + " /closeonend:0");
        }

        [MenuItem("Assets/Git/Blame", false, 52)]
        public static async void Blame()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:blame /path:" + GetSelection() + " /closeonend:0");
        }

        [MenuItem("Assets/Git/Merge", false, 53)]
        public static async void Merge()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:merge /path:" + GetSelection() + " /closeonend:0");
        }

        #endregion

        #region 工具栏菜单项

        [MenuItem("Git/修复Copy错误 _F1", false, 0)]
        public static void FixCopyError()
        {
            if (EditorUtility.DisplayDialog("修复Copy错误", "防止误触，再次确定。", "确定修复", "点错了"))
            {
                //手动强制Unity重新编译，即可解决问题
                UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
            }
        }

        [MenuItem("Git/Commit _F4", false, 1)]
        public static async void CommitAll()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", $"/command:commit /path:{GetGitPath(true)} /closeonend:0");
        }

        [MenuItem("Git/Pull _F7", false, 2)]
        public static async void PullAll()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", $"/command:pull /path:{GetGitPath(true)} /closeonend:0 /noquestion /branch:master");
            AssetDatabase.Refresh();
        }

        [MenuItem("Git/Push _F9", false, 3)]
        public static async void PushAll()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", $"/command:push /path:{GetGitPath(true)} /closeonend:0 /noquestion /branch:master");
        }

        [MenuItem("Git/Switch _F8", false, 4)]
        public static async void Switch()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", $"/command:switch /path:{GetGitPath(true)} /closeonend:0");
        }

        [MenuItem("Git/Assets Log _F10", false, 5)]
        public static async void AssetsLog()
        {
            await ProcessCommandAsync("TortoiseGitProc.exe", "/command:log /path:" + GetGitPath(false) + " /closeonend:0");
        }

        #endregion

        #region 工具

        static async Task ProcessCommandAsync(string command, string argument)
        {
            await Task.Run((() =>
            {
                var info = new ProcessStartInfo(command)
                {
                    Arguments = argument,
                    CreateNoWindow = true,
                    ErrorDialog = true,
                    UseShellExecute = true
                };

                if (info.UseShellExecute)
                {
                    info.RedirectStandardOutput = false;
                    info.RedirectStandardError = false;
                    info.RedirectStandardInput = false;
                }
                else
                {
                    info.RedirectStandardOutput = true;
                    info.RedirectStandardError = true;
                    info.RedirectStandardInput = true;
                    info.StandardOutputEncoding = System.Text.Encoding.UTF8;
                    info.StandardErrorEncoding = System.Text.Encoding.UTF8;
                }

                var process = Process.Start(info);

                if (!info.UseShellExecute)
                {
                    if (process != null)
                    {
                        UnityEngine.Debug.Log(process.StandardOutput);
                        UnityEngine.Debug.Log(process.StandardError);
                    }
                }

                process?.WaitForExit();
            }));
        }

        /// <summary>
        /// 获取选中路径参数
        /// </summary>
        /// <returns>路径参数</returns>
        public static string GetSelection()
        {
            var path = "Assets";
            var strs = Selection.assetGUIDs;
            if (strs == null) return path;
            path = "\"";
            for (int i = 0; i < strs.Length; i++)
            {
                if (i != 0)
                    path += "*";
                path += AssetDatabase.GUIDToAssetPath(strs[i]);
                if (AssetDatabase.GUIDToAssetPath(strs[i]) != "Assets")
                    path += "*" + AssetDatabase.GUIDToAssetPath(strs[i]) + ".meta";
            }

            path += "\"";

            return path;
        }

        static string GetGitPath(bool includeTrunks)
        {
            var str = Application.dataPath;

            if (!includeTrunks) return str;

            str = str.Replace("/Project/Assets", "");
            
            if (!Directory.Exists(str))
            {
                UnityEngine.Debug.LogError("-------路径不对-------");
            }

            return str;
        }

        #endregion
    }
}