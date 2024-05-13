using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class LoadDllNew : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DownLoadAssets(this.StartGame));
    }

    #region download assets

    // key-资源名字  value-二进制数据
    private static Dictionary<string, byte[]> s_assetDatas = new Dictionary<string, byte[]>();

    public static byte[] ReadBytesFromStreamingAssets(string dllName)
    {
        return s_assetDatas[dllName];
    }

    private string GetWebRequestPath(string asset)
    {
        var path = $"{Application.streamingAssetsPath}/{asset}";
        if (!path.Contains("://"))
        {
            path = "file://" + path;
        }
        return path;
    }

    private static List<string> AOTMetaAssemblyFiles { get; } = new List<string>()
    {
        // 三个AOT元数据程序集
        "mscorlib.dll.bytes",
        "System.dll.bytes",
        "System.Core.dll.bytes",
    };

    private IEnumerator DownLoadAssets(Action onDownloadComplete)
    {
        // 把两个列表内容合并
        var assets = new List<string>
        {
            "prefabs",
            "HotUpdate.dll.bytes",
        }.Concat(AOTMetaAssemblyFiles);

        // 遍历 assets 列表
        foreach (var asset in assets)
        {
            // 获取请求地址
            string dllPath = Path.Combine(Application.streamingAssetsPath, asset);
            Debug.Log($"start download asset:{dllPath}");
            // 访问文件并下载
            UnityWebRequest www = UnityWebRequest.Get(dllPath);
            yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
#else
            if (www.isHttpError || www.isNetworkError)
            {
                Debug.Log(www.error);
            }
#endif
            else
            {
                // 下载成功
                // Or retrieve results as binary data
                byte[] assetData = www.downloadHandler.data;
                Debug.Log($"dll:{asset}  size:{assetData.Length}");
                // 将下载下来的数据存储到一个字典中
                s_assetDatas[asset] = assetData;
            }
        }
        // 回调函数
        onDownloadComplete();
    }

    #endregion

    private static Assembly _hotUpdateAss;

    /// <summary>
    /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    /// </summary>
    private static void LoadMetadataForAOTAssemblies()
    {
        //注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
        //热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        foreach (var aotDllName in AOTMetaAssemblyFiles)
        {
            // 拿到下载的二进制数据 并加载出来
            byte[] dllBytes = ReadBytesFromStreamingAssets(aotDllName);
            // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
            Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
        }
    }

    private void StartGame()
    {
        LoadMetadataForAOTAssemblies();
#if !UNITY_EDITOR
        _hotUpdateAss = Assembly.Load(ReadBytesFromStreamingAssets("HotUpdate.dll.bytes"));
#else
        // 从程序域中获取当前程序域，查找程序集
        _hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif
        Type entryType = _hotUpdateAss.GetType("Entry");
        entryType.GetMethod("Start").Invoke(null, null);

        Run_InstantiateComponentByAsset();

        // 开启一个倒计时协程
        StartCoroutine(DelayAndQuit());
    }

    private IEnumerator DelayAndQuit()
    {
#if UNITY_STANDALONE_WIN
        File.WriteAllText(Directory.GetCurrentDirectory() + "/run.log", "ok", System.Text.Encoding.UTF8);
#endif
        for (int i = 10; i >= 1; i--)
        {
            UnityEngine.Debug.Log($"将于{i}s后自动退出");
            yield return new WaitForSeconds(1f);
        }
        Application.Quit();
    }

    private static void Run_InstantiateComponentByAsset()
    {
        // 通过实例化assetbundle中的资源，还原资源上的热更新脚本
        AssetBundle ab = AssetBundle.LoadFromMemory(LoadDllNew.ReadBytesFromStreamingAssets("prefabs"));
        GameObject cube = ab.LoadAsset<GameObject>("Cube");
        GameObject.Instantiate(cube);
    }
}