using System;
using System.Collections.Generic;
using TBydFramework.Runtime.Binding.Reflection;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;
using static TMPro.TMP_InputField;

namespace TBydFramework.TextMeshPro.Runtime.Binding
{
    public class UnityTMPProxyRegister
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            Register<TMP_Text, string>("text", t => t.text, (t, v) => t.text = v);
            Register<TMP_Text, float>("fontSize", t => t.fontSize, (t, v) => t.fontSize = v);
            Register<TMP_Text, FontStyles>("fontStyle", t => t.fontStyle, (t, v) => t.fontStyle = v);
            Register<TMP_Text, Color32>("faceColor", t => t.faceColor, (t, v) => t.faceColor = v);
            Register<TMP_Text, Color32>("outlineColor", t => t.outlineColor, (t, v) => t.outlineColor = v);

            Register<TMP_Dropdown, int>("value", t => t.value, (t, v) => t.value = v);
            Register<TMP_Dropdown, List<OptionData>>("options", t => t.options, (t, v) => t.options = v);
            Register<TMP_Dropdown, DropdownEvent>("onValueChanged", t => t.onValueChanged, null);

            Register<TMP_InputField, string>("text", t => t.text, (t, v) => t.text = v);
            Register<TMP_InputField, bool>("richText", t => t.richText, (t, v) => t.richText = v);
            Register<TMP_InputField, SubmitEvent>("onEndEdit", t => t.onEndEdit, null);
            Register<TMP_InputField, SubmitEvent>("onSubmit", t => t.onSubmit, null);
            Register<TMP_InputField, SelectionEvent>("onSelect", t => t.onSelect, null);
            Register<TMP_InputField, OnChangeEvent>("onValueChanged", t => t.onValueChanged, null);
            Register<TMP_InputField, TextSelectionEvent>("onEndTextSelection", t => t.onEndTextSelection, null);
            Register<TMP_InputField, TextSelectionEvent>("onTextSelection", t => t.onTextSelection, null);
            Register<TMP_InputField, TouchScreenKeyboardEvent>("onTouchScreenKeyboardStatusChanged", t => t.onTouchScreenKeyboardStatusChanged, null);
        }

        static void Register<T, TValue>(string name, Func<T, TValue> getter, Action<T, TValue> setter)
        {
            var propertyInfo = typeof(T).GetProperty(name);
            if (propertyInfo != null)
            {
                ProxyFactory.Default.Register(new ProxyPropertyInfo<T, TValue>(name, getter, setter));
                return;
            }

            var fieldInfo = typeof(T).GetField(name);
            if (fieldInfo != null)
            {
                ProxyFactory.Default.Register(new ProxyFieldInfo<T, TValue>(name, getter, setter));
                return;
            }

            throw new Exception(string.Format("Not found the property or field named '{0}' in {1} type", name, typeof(T).Name));
        }
    }
}