using TBydFramework.Runtime.Execution;
using UnityEngine;

namespace TBydFramework.XLua.Runtime.LuaLoaders
{
    public class AssetBundleLoader : PathLoaderBase
    {
        private AssetBundle bundle;

        public AssetBundleLoader(AssetBundle bundle, string prefix) : this(bundle, prefix, ".lua.txt")
        {
        }

        public AssetBundleLoader(AssetBundle bundle, string prefix, string suffix) : base(prefix, suffix)
        {
            this.bundle = bundle;
        }

        protected override byte[] Load(ref string path)
        {
            if (this.bundle == null)
                return null;

            TextAsset text = bundle.LoadAsset<TextAsset>(this.GetFullname(path));
            if (text == null)
                return null;

            return text.bytes;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (bundle != null)
                {
                    Executors.RunOnMainThread(() =>
                    {
                        bundle.Unload(false);
                        bundle = null;
                    });
                }
                disposedValue = true;
            }
        }
        #endregion
    }
}