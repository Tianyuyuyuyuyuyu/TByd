using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace TByd.Framework.Csv
{
	public class DataRow
	{
		private List<string> _fields;
		private List<string> _values;

		public void _SetFields(List<string> fields) { _fields = fields; }
		public void _SetValues(List<string> values) { _values = values; }
        public bool _GetBool(int index) { return _GetString(index) != "0"; }
        public byte _GetInt8(int index) { return byte.Parse(_GetString(index)); }
        public short _GetInt16(int index) { return short.Parse(_GetString(index)); }
        public Int32 _GetInt32(int index) { return Int32.Parse(_GetString(index)); }
		public Int64 _GetInt64(int index) { return Int64.Parse(_GetString(index)); }
        public float _GetFloat32(int index) { return float.Parse(_GetString(index)); }
	    public Double _GetFloat64(int index) { return Double.Parse(_GetString(index)); }
        public Double _GetDouble(int index) { return Double.Parse(_GetString(index)); }
		public string _GetString(int index) { return _values[index]; }
        public bool _GetBool(string field) { return GetString(field) != "0"; }

        public byte _GetInt8(string field) { return byte.Parse(GetString(field)); }
        public short _GetInt16(string field) { return short.Parse(GetString(field)); }
        public Int32 _GetInt32(string field)	{return Int32.Parse(GetString(field));}
		public Int64 _GetInt64(string field) { return Int64.Parse(GetString(field)); }
        public float _GetFloat32(string field) { return float.Parse(GetString(field)); }
        public Double _GetFloat64(string field) { return Double.Parse(GetString(field)); }

        public Int32[] GetInt32Array(string field)
        {
            string[] ss = GetStringArray(field);
            Int32[] result = new Int32[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                result[i] = Convert.ToInt32(ss[i]);
            }
            return result;
        }
        public Int32[] GetInt32Array(int index, string delims = "|")
        {
            string[] ss = GetStringArray(index, delims);
            Int32[] result = new Int32[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                result[i] = Convert.ToInt32(ss[i]);
            }
            return result;
        }
        public Int64[] GetInt64Array(int index, string delims = "|")
        {
	        string[] ss     = GetStringArray(index, delims);
	        Int64[]  result = new Int64[ss.Length];
	        for (int i = 0; i < ss.Length; i++)
	        {
		        result[i] = Convert.ToInt32(ss[i]);
	        }
	        return result;
        }
        public short[] GetInt16Array(string field)
        {
            string[] ss = GetStringArray(field);
            short[] result = new short[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                result[i] = Convert.ToInt16(ss[i]);
            }
            return result;
        }

        public byte[] GetInt8Array(string field)
        {
            string[] ss = GetStringArray(field);
            byte[] result = new byte[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                result[i] = Convert.ToByte(ss[i]);
            }
            return result;
        }

	    public float[] GetFloat32Array(int index, string delims = "|")
	    {
	        string[] ss = GetStringArray(index, delims);
	        float[] result = new float[ss.Length];
	        for (int i = 0; i < ss.Length; i++)
	        {
	            result[i] = Convert.ToSingle(ss[i]);
	        }
	        return result;
	    }

        public float[] GetFloat32Array(string field)
        {
            string[] ss = GetStringArray(field);
            float[] result = new float[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                result[i] = Convert.ToSingle(ss[i]);
            }
            return result;
        }

        public string GetString(string field)
		{
			int index = _fields.IndexOf(field);
			if (index == -1)
			{
				Debug.Log("can not find filed: " + field);
				return "";
			}

            //替换换行符
            var value = _values[index].Replace(@"\n", "\n");
		    value = value.Replace(@"<br>", "\n");
		    
		    value = value.Replace(@"<rn>", "\r\n");
		    value = value.Replace(@"<rr>", "\r");

            return value;
		}
        public string GetKeyString(string field)
        {
            int index = _fields.IndexOf(field);
            if (index == -1)
            {
                Debug.Log("can not find filed: " + field);
                return "";
            }

            //替换换行符
            var value = _values[index];

            return value;
        }
        public string[] GetStringArray(string field, string delims = "|")
		{
			return GetString(field).Split(new string[]{delims}, StringSplitOptions.RemoveEmptyEntries);
		}
        public string[] GetStringArray(int index, string delims = "|")
        {
            return _GetString(index).Split(new string[] { delims }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
