using System;
using UnityEngine;

namespace TBydFramework.Runtime.Prefs.TypeEncoder
{

    public class ColorTypeEncoder : ITypeEncoder
    {
        private int priority = 997;

        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public bool IsSupport(Type type)
        {
            if (type.Equals(typeof(Color)))
                return true;
            return false;
        }

        public object Decode(Type type, string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            Color color;
            if (ColorUtility.TryParseHtmlString(value, out color))
                return color;

            throw new FormatException(string.Format("The '{0}' is illegal Color.", value));
        }

        public string Encode(object value)
        {
            return string.Format("#{0}", ColorUtility.ToHtmlStringRGBA((Color)value));
        }
    }
}
