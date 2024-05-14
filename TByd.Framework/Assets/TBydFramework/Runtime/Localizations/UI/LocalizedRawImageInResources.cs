using System;
using System.Collections;
using TBydFramework.Runtime.Log;
using UnityEngine;
using UnityEngine.UI;

namespace TBydFramework.Runtime.Localizations.UI
{
    [AddComponentMenu("X/Localization/LocalizedRawImageInResources")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RawImage))]
    public class LocalizedRawImageInResources : AbstractLocalized<RawImage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LocalizedRawImageInResources));

        protected override void OnValueChanged(object sender, EventArgs e)
        {
            object v = this.value.Value;
            if (v is Texture2D)
            {
                this.target.texture = (Texture2D)v;
            }
            else if (v is string)
            {
                string path = (string)v;
                this.StartCoroutine(DoLoad(path));
            }
            else if (v != null)
            {
                if (log.IsErrorEnabled)
                    log.ErrorFormat("There is an invalid localization value \"{0}\" on the GameObject named \"{1}\".", v, this.name);
            }

        }

        protected virtual IEnumerator DoLoad(string path)
        {
            var result = Resources.LoadAsync<Texture2D>(path);
            yield return result;
            this.target.texture = (Texture2D)result.asset;
        }
    }
}
