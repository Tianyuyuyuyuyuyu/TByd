using UnityEngine;
using TByd.Core.Utils.Runtime;

namespace TByd.Core.Utils.Samples
{
    /// <summary>
    /// 展示MathUtils类的使用示例
    /// </summary>
    public class MathUtilsExample : MonoBehaviour
    {
        [Header("SmoothDamp示例")]
        [SerializeField] private Transform _targetObject;
        [SerializeField] private float _smoothTime = 0.3f;
        [SerializeField] private float _maxSpeed = 10f;
        
        [Header("Remap示例")]
        [SerializeField] private Transform _remapObject;
        [SerializeField] private float _remapSpeed = 1f;
        [SerializeField] private Vector2 _inputRange = new Vector2(0f, 1f);
        [SerializeField] private Vector2 _outputRange = new Vector2(0.5f, 2f);
        
        [Header("DirectionToRotation示例")]
        [SerializeField] private Transform _rotationObject;
        [SerializeField] private Transform _lookTarget;
        
        [Header("IsPointInPolygon示例")]
        [SerializeField] private Transform _polygonCenter;
        [SerializeField] private float _polygonRadius = 5f;
        [SerializeField] private int _polygonVertices = 5;
        [SerializeField] private Transform _testPoint;
        [SerializeField] private Renderer _testPointRenderer;
        
        private Vector3 _velocity = Vector3.zero;
        private Vector2[] _polygon;
        private float _time;

        private void Start()
        {
            // 初始化多边形
            InitializePolygon();
        }

        private void Update()
        {
            // 更新时间
            _time += Time.deltaTime * _remapSpeed;
            
            // 演示SmoothDamp
            DemoSmoothDamp();
            
            // 演示Remap
            DemoRemap();
            
            // 演示DirectionToRotation
            DemoDirectionToRotation();
            
            // 演示IsPointInPolygon
            DemoIsPointInPolygon();
        }

        private void OnDrawGizmos()
        {
            // 绘制多边形
            if (_polygon != null && _polygon.Length > 2)
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i < _polygon.Length; i++)
                {
                    int nextIndex = (i + 1) % _polygon.Length;
                    Vector3 start = new Vector3(_polygon[i].x, 0, _polygon[i].y);
                    Vector3 end = new Vector3(_polygon[nextIndex].x, 0, _polygon[nextIndex].y);
                    Gizmos.DrawLine(start, end);
                }
            }
        }

        private void DemoSmoothDamp()
        {
            if (_targetObject != null)
            {
                // 使用SmoothDamp平滑移动到目标位置
                transform.position = MathUtils.SmoothDamp(
                    transform.position,
                    _targetObject.position,
                    ref _velocity,
                    _smoothTime,
                    _maxSpeed
                );
            }
        }

        private void DemoRemap()
        {
            if (_remapObject != null)
            {
                // 使用正弦波生成0-1之间的值
                float value = (Mathf.Sin(_time) + 1f) * 0.5f;
                
                // 使用Remap将值映射到输出范围
                float scaleFactor = MathUtils.Remap(
                    value,
                    _inputRange.x,
                    _inputRange.y,
                    _outputRange.x,
                    _outputRange.y
                );
                
                // 应用到缩放
                _remapObject.localScale = Vector3.one * scaleFactor;
            }
        }

        private void DemoDirectionToRotation()
        {
            if (_rotationObject != null && _lookTarget != null)
            {
                // 计算方向向量
                Vector3 direction = _lookTarget.position - _rotationObject.position;
                
                // 使用DirectionToRotation计算旋转
                _rotationObject.rotation = MathUtils.DirectionToRotation(direction);
            }
        }

        private void DemoIsPointInPolygon()
        {
            if (_testPoint != null && _testPointRenderer != null && _polygon != null)
            {
                // 获取测试点的2D位置（忽略Y轴）
                Vector2 point = new Vector2(_testPoint.position.x, _testPoint.position.z);
                
                // 检查点是否在多边形内
                bool isInside = MathUtils.IsPointInPolygon(point, _polygon);
                
                // 根据结果改变测试点的颜色
                _testPointRenderer.material.color = isInside ? Color.green : Color.red;
            }
        }

        private void InitializePolygon()
        {
            if (_polygonCenter != null && _polygonVertices >= 3)
            {
                _polygon = new Vector2[_polygonVertices];
                
                for (int i = 0; i < _polygonVertices; i++)
                {
                    float angle = i * (2f * Mathf.PI / _polygonVertices);
                    float x = _polygonCenter.position.x + Mathf.Cos(angle) * _polygonRadius;
                    float z = _polygonCenter.position.z + Mathf.Sin(angle) * _polygonRadius;
                    _polygon[i] = new Vector2(x, z);
                }
            }
        }
    }
} 