using System;
using System.Collections;
using TBydFramework.Runtime.Log;
using UnityEngine;
using UnityEngine.UI;

namespace TBydFramework.Runtime.Localizations.UI
{
    [AddComponentMenu("X/Localization/LocalizedImageInResources")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public class LocalizedImageInResources : AbstractLocalized<Image>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LocalizedImageInResources));

        protected override void OnValueChanged(object sender, EventArgs e)
        {
            object v = this.value.Value;
            if (v is Sprite)
            {
                this.target.sprite = (Sprite)v;
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
            var result = Resources.LoadAsync<Sprite>(path);
            yield return result;
            this.target.sprite = (Sprite)result.asset;
        }
    }
}
