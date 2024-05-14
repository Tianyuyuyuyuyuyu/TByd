using System;
using System.Collections;
using TBydFramework.Runtime.Log;
using UnityEngine;

namespace TBydFramework.Runtime.Localizations.UI
{
    [AddComponentMenu("X/Localization/LocalizedAudioSourceInResources")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class LocalizedAudioSourceInResources : AbstractLocalized<AudioSource>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LocalizedAudioSourceInResources));

        protected override void OnValueChanged(object sender, EventArgs e)
        {
            object v = this.value.Value;
            if (v is AudioClip)
            {
                this.target.clip = (AudioClip)v;
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
            var result = Resources.LoadAsync<AudioClip>(path);
            yield return result;
            this.target.clip = (AudioClip)result.asset;
        }
    }
}
