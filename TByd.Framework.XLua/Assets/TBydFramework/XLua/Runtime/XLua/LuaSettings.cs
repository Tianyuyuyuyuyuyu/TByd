#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.XLua.Runtime.XLua
{
    [Serializable]
    public class LuaSettings : ScriptableObject
    {
        public const string LUA_SETTINGS_PATH = "Assets/XFramework/Editor/AppData/XLua/LuaSettings.asset";

        [SerializeField]
        private List<DefaultAsset> srcRoots = new List<DefaultAsset>();

        public List<string> SrcRoots
        {
            get
            {
                if (this.srcRoots == null)
                    return new List<string>();
                return this.srcRoots.Where(asset => asset != null).Select(asset => AssetDatabase.GetAssetPath(asset)).ToList();
            }
        }

        public static LuaSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<LuaSettings>(LUA_SETTINGS_PATH);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<LuaSettings>();
                FileInfo file = new FileInfo(LUA_SETTINGS_PATH);
                if (!file.Directory.Exists)
                    file.Directory.Create();

                AssetDatabase.CreateAsset(settings, LUA_SETTINGS_PATH);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
}
#endif