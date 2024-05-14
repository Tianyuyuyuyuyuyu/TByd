using UnityEngine;

namespace TBydFramework.Runtime.Localizations.Unity
{
    [CreateAssetMenu(fileName = "New Localization Module", menuName = "X/LocalizationSource", order = 1)]
    public class LocalizationSourceAsset : ScriptableObject
    {
        public MonolingualSource Source = new MonolingualSource();
    }
}