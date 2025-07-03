﻿#if ENABLED_UNITY_URP
using UnityEngine.Rendering;
#endif

namespace TByd.Tools.Extension.Runtime {
    public static class ResourcesUtils {
#if ENABLED_UNITY_URP
        /// <summary>
        /// Load volume profile from given path.
        /// </summary>
        /// <param name="path">Path from where volume profile should be loaded.</param>
        public static void LoadVolumeProfile(this Volume volume, string path) {
            var profile = Resources.Load<VolumeProfile>(path);
            volume.profile = profile;
        }
#endif
    }
}