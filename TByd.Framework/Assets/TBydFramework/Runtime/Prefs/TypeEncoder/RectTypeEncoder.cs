using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TBydFramework.Runtime.Prefs.TypeEncoder
{
    public class RectTypeEncoder : ITypeEncoder
    {
        private int priority = 995;

        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public bool IsSupport(Type type)
        {
            if (type.Equals(typeof(Rect)))
                return true;
            return false;
        }

        public object Decode(Type type, string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            try
            {
                var val = Regex.Replace((value).Trim(), @"(^\()|(\)$)", "");
                string[] s = val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length == 4)
                    return new Rect(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
            }
            catch (Exception e)
            {
                throw new FormatException(string.Format("The '{0}' is illegal Rect.", value), e);
            }
            throw new FormatException(string.Format("The '{0}' is illegal Rect.", value));
        }

        public string Encode(object value)
        {
            Rect rect = (Rect)value;
            return string.Format("({0:F2}, {1:F2}, {2:F2}, {3:F2})", rect.x, rect.y, rect.width, rect.height);
        }
    }
}
