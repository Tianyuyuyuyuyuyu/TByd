using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HybridCLR;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples
{
    public class LoadDll : MonoBehaviour
    {
        IEnumerator Start()
        {
#if !UNITY_EDITOR
            //要用UnityWebRequest加载
            var filePath = Path.Combine(Application.streamingAssetsPath, "HotProject.dll.bytes");
            Debug.Log("打印路径：" + filePath);
            
            // 使用 UnityWebRequest 来异步加载 StreamingAssets 文件夹中的文件内容
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            
            // 发送请求并等待响应
            yield return www.SendWebRequest();
            
            // 检查是否有错误发生
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load bytes file: " + www.error);
            }
            else
            {
                // 从下载处理器获取加载的字节数组
                byte[] loadedBytes = www.downloadHandler.data;
            
                // 在这里处理加载到的 bytes 数据
                Debug.Log("Loaded bytes content: " + System.Text.Encoding.UTF8.GetString(loadedBytes));
                
                Assembly hotUpdateAss = Assembly.Load(loadedBytes);
                Type type = hotUpdateAss.GetType("HotProject.Hello");
                type.GetMethod("Run").Invoke(null, null);
            }
            
            //用File.ReadAllBytes(path)不行，打包运行会报DirectoryNotFoundException
            // Editor环境下，HotProject.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题。
            // Assembly hotUpdateAss = Assembly.Load(File.ReadAllBytes(filePath));
            // Type type = hotUpdateAss.GetType("HotProject.Hello");
            // type.GetMethod("Run").Invoke(null, null);
            // return null;
#else
            // // Editor下无需加载，直接查找获得HotUpdate程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assembly hotUpdateAss = assemblies.First(a => a.GetName().Name == "HotProject");
            Type type = hotUpdateAss.GetType("HotProject.Hello");
            type.GetMethod("Run").Invoke(null, null);
            return null;
#endif
        }
    }
}
