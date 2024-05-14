﻿using System;
using System.Collections.Generic;
using TBydFramework.Runtime.Prefs.TypeEncoder;

namespace TBydFramework.Runtime.Prefs
{
    public class DefaultSerializer : ISerializer
    {
        private readonly object _lock = new object();
        private readonly static ComparerImpl<ITypeEncoder> comparer = new ComparerImpl<ITypeEncoder>();
        private List<ITypeEncoder> encoders = new List<ITypeEncoder>();

        public DefaultSerializer()
        {
            AddTypeEncoder(new PrimitiveTypeEncoder());
            AddTypeEncoder(new VersionTypeEncoder());
            AddTypeEncoder(new EnumTypeEncoder());
            AddTypeEncoder(new JsonTypeEncoder());
        }

        public virtual void AddTypeEncoder(ITypeEncoder encoder)
        {
            lock (_lock)
            {
                if (encoders.Contains(encoder))
                    return;

                encoders.Add(encoder);
                encoders.Sort(comparer);
            }
        }

        public virtual void RemoveTypeEncoder(ITypeEncoder encoder)
        {
            lock (_lock)
            {
                if (!encoders.Contains(encoder))
                    return;

                encoders.Remove(encoder);
            }
        }

        public virtual object Deserialize(string input, Type type)
        {
            lock (_lock)
            {
                for (int i = 0; i < encoders.Count; i++)
                {
                    try
                    {
                        ITypeEncoder encoder = encoders[i];
                        if (!encoder.IsSupport(type))
                            continue;

                        return encoder.Decode(type, input);
                    }
                    catch (Exception) { }
                }

            }
            throw new NotSupportedException(string.Format("This value \"{0}\" cannot be converted to the type \"{1}\"", input, type.Name));
        }

        public virtual string Serialize(object value)
        {
            lock (_lock)
            {
                for (int i = 0; i < encoders.Count; i++)
                {
                    try
                    {
                        ITypeEncoder encoder = encoders[i];
                        if (!encoder.IsSupport(value.GetType()))
                            continue;

                        return encoder.Encode(value);
                    }
                    catch (Exception) { }
                }
            }
            throw new NotSupportedException(string.Format("Unsupported type, this value \"{0}\" cannot be serialized", value));
        }

        class ComparerImpl<T> : IComparer<T> where T : ITypeEncoder
        {
            public int Compare(T x, T y)
            {
                return y.Priority.CompareTo(x.Priority);
            }
        }
    }
}
