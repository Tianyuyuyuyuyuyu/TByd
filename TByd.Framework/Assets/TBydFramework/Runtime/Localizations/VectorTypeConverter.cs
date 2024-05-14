using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TBydFramework.Runtime.Localizations
{
    public class VectorTypeConverter : ITypeConverter
    {
        private static readonly char[] COMMA_SEPARATOR = new char[] { ',' };
        private static readonly string PATTERN = @"(^\()|(\)$)";
        public bool Support(string typeName)
        {
            switch (typeName)
            {
                case "vector2":
                case "vector3":
                case "vector4":
                    return true;
                default:
                    return false;
            }
        }

        public Type GetType(string typeName)
        {
            switch (typeName)
            {
                case "vector2":
                    return typeof(Vector2);
                case "vector3":
                    return typeof(Vector3);
                case "vector4":
                    return typeof(Vector4);
                default:
                    throw new NotSupportedException();
            }
        }

        public object Convert(Type type, object value)
        {
            if (type == null)
                throw new NotSupportedException();

            var val = Regex.Replace(((string)value).Trim(), PATTERN, "");
            if (type.Equals(typeof(Vector2)))
            {
                try
                {
                    string[] s = val.Split(COMMA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);
                    if (s.Length == 2)
                        return new Vector2(float.Parse(s[0]), float.Parse(s[1]));
                }
                catch (Exception e)
                {
                    throw new FormatException(string.Format("The '{0}' is illegal Vector2.", value), e);
                }
                throw new FormatException(string.Format("The '{0}' is illegal Vector2.", value));
            }

            if (type.Equals(typeof(Vector3)))
            {
                try
                {
                    string[] s = val.Split(COMMA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);
                    if (s.Length == 3)
                        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
                }
                catch (Exception e)
                {
                    throw new FormatException(string.Format("The '{0}' is illegal Vector3.", value), e);
                }
                throw new FormatException(string.Format("The '{0}' is illegal Vector3.", value));
            }

            if (type.Equals(typeof(Vector4)))
            {
                try
                {
                    string[] s = val.Split(COMMA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);
                    if (s.Length == 4)
                        return new Vector4(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
                }
                catch (Exception e)
                {
                    throw new FormatException(string.Format("The '{0}' is illegal Vector4.", value), e);
                }
                throw new FormatException(string.Format("The '{0}' is illegal Vector4.", value));
            }

            throw new NotSupportedException();
        }
    }
}
