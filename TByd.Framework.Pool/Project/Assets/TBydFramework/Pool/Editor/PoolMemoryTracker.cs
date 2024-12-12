using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TBydFramework.Pool.Editor
{
    public class PoolMemoryTracker
    {
        private static readonly Dictionary<string, PoolMemoryInfo> _poolMemoryInfo = new Dictionary<string, PoolMemoryInfo>();

        public static void TrackObject(string poolName, object obj)
        {
            if (!_poolMemoryInfo.ContainsKey(poolName))
            {
                _poolMemoryInfo[poolName] = new PoolMemoryInfo();
            }

            var info = _poolMemoryInfo[poolName];
            info.ObjectCount++;
            info.EstimatedMemoryUsage += EstimateObjectSize(obj);
        }

        public static void UntrackObject(string poolName, object obj)
        {
            if (_poolMemoryInfo.TryGetValue(poolName, out var info))
            {
                info.ObjectCount--;
                info.EstimatedMemoryUsage -= EstimateObjectSize(obj);
            }
        }

        public static PoolMemoryInfo GetMemoryInfo(string poolName)
        {
            return _poolMemoryInfo.TryGetValue(poolName, out var info) ? info : new PoolMemoryInfo();
        }

        public static void Clear()
        {
            _poolMemoryInfo.Clear();
        }

        private static long EstimateObjectSize(object obj)
        {
            if (obj == null) return 0;

            if (obj is GameObject go)
            {
                return EstimateGameObjectSize(go);
            }

            // 使用Marshal.SizeOf估算基础类型大小
            try
            {
                return Marshal.SizeOf(obj);
            }
            catch
            {
                // 对于不支持的类型，返回一个保守估计
                return 100; // 字节
            }
        }

        private static long EstimateGameObjectSize(GameObject go)
        {
            long size = 0;
            var components = go.GetComponents<Component>();
            
            // 基础GameObject开销
            size += 100; // 保守估计

            foreach (var component in components)
            {
                if (component == null) continue;

                // 组件基础开销
                size += 100;

                // Mesh数据
                if (component is MeshFilter meshFilter && meshFilter.sharedMesh != null)
                {
                    var mesh = meshFilter.sharedMesh;
                    size += EstimateMeshSize(mesh);
                }

                // 材质数据
                if (component is Renderer renderer)
                {
                    foreach (var material in renderer.sharedMaterials)
                    {
                        if (material != null)
                        {
                            size += 1000; // 保守估计每个材质的大小
                        }
                    }
                }
            }

            return size;
        }

        private static long EstimateMeshSize(Mesh mesh)
        {
            long size = 0;
            
            // 顶点数据
            size += mesh.vertexCount * 12; // 每个顶点3个float (12字节)
            
            // UV数据
            size += mesh.vertexCount * 8;  // 每个UV 2个float (8字节)
            
            // 法线数据
            size += mesh.vertexCount * 12; // 每个法线3个float (12字节)
            
            // 三角形索引
            size += mesh.triangles.Length * 4; // 每个索引1个int (4字节)

            return size;
        }
    }

    public class PoolMemoryInfo
    {
        public int ObjectCount { get; set; }
        public long EstimatedMemoryUsage { get; set; } // 字节

        public string FormattedMemoryUsage
        {
            get
            {
                if (EstimatedMemoryUsage < 1024)
                    return $"{EstimatedMemoryUsage}B";
                if (EstimatedMemoryUsage < 1024 * 1024)
                    return $"{EstimatedMemoryUsage / 1024f:F2}KB";
                return $"{EstimatedMemoryUsage / (1024f * 1024f):F2}MB";
            }
        }
    }
} 