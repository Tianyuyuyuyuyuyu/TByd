using System;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Log;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace TBydFramework.Runtime.Localizations.UI
{
    [AddComponentMenu("X/Localization/LocalizedDataBinder")]
    [DisallowMultipleComponent]
    [AllowedMembers(typeof(RectTransform), "offsetMax", "offsetMin", "pivot", "sizeDelta", "anchoredPosition", "anchorMax", "anchoredPosition3D", "rect", "anchorMin")]
    [AllowedMembers(typeof(Image), "sprite", "material", "color")]
    [AllowedMembers(typeof(RawImage), "texture", "material", "color")]
    [AllowedMembers(typeof(SpriteRenderer), "sprite", "color", "drawMode")]
    [AllowedMembers(typeof(Text), "text", "font", "fontStyle", "fontSize", "color")]
    [AllowedMembers(typeof(TextMesh), "text", "font", "fontStyle", "fontSize", "color")]
    [AllowedMembers(typeof(AudioSource), "clip")]
    [AllowedMembers(typeof(VideoPlayer), "clip", "url")]
    public class LocalizedDataBinder : MonoBehaviour
    {
        private static readonly ILog log = LogManager.GetLogger("LocalizedComponent");

        [SerializeField]
        protected LocalizedBindingDescriptionSet data = new LocalizedBindingDescriptionSet();

        protected virtual void Start()
        {
            var localization = Localization.Current;
            var bindingSet = this.CreateSimpleBindingSet();
            foreach (var description in data.descriptions)
            {
                string typeName = description.TypeName;
                var target = this.GetComponentByName(typeName);
                if (target == null)
                    throw new MissingComponentException(string.Format("Not found the \"{0}\" component.", typeName));

                string propertyName = description.PropertyName;
                string key = description.Key;
                BindingMode mode = description.Mode;
                if (string.IsNullOrEmpty(key))
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("The key is null or empty.Please check the binding \"{0}\" in the GameObject \"{1}\"", description.ToString(), this.name);

                    continue;
                }

                var value = localization.GetValue(key);
                var builder = bindingSet.Bind(target).For(propertyName).ToValue(value);
                switch (mode)
                {
                    case BindingMode.OneTime:
                        builder.OneTime();
                        break;
                    default:
                        builder.OneWay();
                        break;
                }
            }
            bindingSet.Build();
        }


        protected virtual Component GetComponentByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;

            foreach (AllowedMembersAttribute attribute in this.GetType().GetCustomAttributes(typeof(AllowedMembersAttribute), true))
            {
                Type type = attribute.Type;
                if (!typeName.Equals(type.FullName))
                    continue;

                Component component = this.GetComponent(type);
                if (component != null)
                    return component;
                break;
            }

            return this.GetComponent(typeName);
        }
    }
}
