using System;
using UnityEngine;

namespace TBydFramework.Runtime.Localizations.UI
{
    [AddComponentMenu("X/Localization/LocalizedTextMesh")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMesh))]
    public class LocalizedTextMesh : AbstractLocalized<TextMesh>
    {
        protected override void OnValueChanged(object sender, EventArgs e)
        {
            this.target.text = (string)Convert.ChangeType(this.value.Value, typeof(string));
        }
    }
}
