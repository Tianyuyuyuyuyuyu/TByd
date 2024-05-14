using System;
using Newtonsoft.Json;
using UnityEngine;

namespace TBydFramework.DataVault.Runtime
{
    public static class XPrefs
    {
        /// <summary>
        /// 保存Int类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="value">Int数据</param>
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        /// <summary>
        /// 保存Float类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="value">Float数据</param>
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        /// <summary>
        /// 保存String类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="value">String数据</param>
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        /// <summary>
        /// 保存Bool类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="value">Bool数据</param>
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 0 : 1);
        }

        /// <summary>
        /// 保存Enum类型数据
        /// </summary>
        /// <typeparam name="T">Enum类型</typeparam>
        /// <param name="key">Key值</param>
        /// <param name="value">Enum数据</param>
        public static void SetEnum<T>(string key, T value) where T : Enum
        {
            PlayerPrefs.SetString(key, value.ToString());
        }

        /// <summary>
        /// 保存Object数据 （Object可序列化）
        /// </summary>
        /// <typeparam name="T">Object类型</typeparam>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void SetObject<T>(string key, T value)
        {
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取Int类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        /// <summary>
        /// 获取Float类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        /// <summary>
        /// 获取String类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        /// <summary>
        /// 获取Bool类型数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 0;
        }

        /// <summary>
        /// 获取Enum类型数据
        /// </summary>
        /// <typeparam name="T">Enum类型</typeparam>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static T GetEnum<T>(string key) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), PlayerPrefs.GetString(key));
        }

        /// <summary>
        /// 获取Object数据
        /// </summary>
        /// <typeparam name="T">Object类型</typeparam>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static T GetObject<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static void Save()
        {
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 删除指定数据
        /// </summary>
        /// <param name="key">Key值</param>
        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        /// 删除全部数据
        /// </summary>
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 是否存在Key值
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>是否存在</returns>
        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}

