using System;
using UnityEngine;
using UnityEngine.UI;

namespace TBydFramework.Runtime.Localizations.UI
{
    [AddComponentMenu("X/Localization/LocalizedText")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]    
    public class LocalizedText : AbstractLocalized<Text>
    {
        protected override void OnValueChanged(object sender, EventArgs e)
        {
            this.target.text = (string)Convert.ChangeType(this.value.Value, typeof(string));
        }
    }
}
