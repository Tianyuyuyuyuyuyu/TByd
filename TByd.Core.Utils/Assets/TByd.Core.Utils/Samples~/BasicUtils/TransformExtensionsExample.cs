using UnityEngine;
using UnityEngine.UI;
using TByd.Core.Utils.Runtime;
using System.Collections.Generic;

namespace TByd.Core.Utils.Samples
{
    /// <summary>
    /// 展示TransformExtensions类的使用示例
    /// </summary>
    public class TransformExtensionsExample : MonoBehaviour
    {
        [Header("UI引用")]
        [SerializeField] private Button _resetLocalButton;
        [SerializeField] private Button _createChildButton;
        [SerializeField] private Button _findChildButton;
        [SerializeField] private Button _getAllChildrenButton;
        [SerializeField] private InputField _childNameInput;
        [SerializeField] private Text _resultText;
        
        [Header("示例对象")]
        [SerializeField] private Transform _demoObject;
        
        private void Start()
        {
            // 初始化UI按钮
            InitializeButtons();
        }
        
        private void InitializeButtons()
        {
            if (_resetLocalButton != null)
            {
                _resetLocalButton.onClick.AddListener(OnResetLocalClicked);
            }
            
            if (_createChildButton != null)
            {
                _createChildButton.onClick.AddListener(OnCreateChildClicked);
            }
            
            if (_findChildButton != null)
            {
                _findChildButton.onClick.AddListener(OnFindChildClicked);
            }
            
            if (_getAllChildrenButton != null)
            {
                _getAllChildrenButton.onClick.AddListener(OnGetAllChildrenClicked);
            }
        }
        
        private void OnResetLocalClicked()
        {
            if (_demoObject != null)
            {
                // 随机设置位置、旋转和缩放，以便看到重置效果
                _demoObject.localPosition = Random.insideUnitSphere * 3f;
                _demoObject.localRotation = Random.rotation;
                _demoObject.localScale = Vector3.one * Random.Range(0.5f, 2f);
                
                UpdateResultText($"对象已随机化: 位置={_demoObject.localPosition}, 旋转={_demoObject.localEulerAngles}, 缩放={_demoObject.localScale}");
                
                // 使用ResetLocal扩展方法重置变换
                _demoObject.ResetLocal();
                
                UpdateResultText($"对象已重置: 位置={_demoObject.localPosition}, 旋转={_demoObject.localEulerAngles}, 缩放={_demoObject.localScale}");
            }
        }
        
        private void OnCreateChildClicked()
        {
            if (_demoObject != null && _childNameInput != null)
            {
                string childName = _childNameInput.text;
                if (string.IsNullOrEmpty(childName))
                {
                    childName = "NewChild_" + System.DateTime.Now.Ticks;
                }
                
                // 使用FindOrCreateChild扩展方法创建子对象
                Transform child = _demoObject.FindOrCreateChild(childName);
                
                UpdateResultText($"已创建或找到子对象: {childName}");
                
                // 随机设置子对象位置
                child.localPosition = Random.insideUnitSphere;
            }
        }
        
        private void OnFindChildClicked()
        {
            if (_demoObject != null && _childNameInput != null)
            {
                string childName = _childNameInput.text;
                if (string.IsNullOrEmpty(childName))
                {
                    UpdateResultText("请输入要查找的子对象名称");
                    return;
                }
                
                // 使用FindRecursive扩展方法查找子对象
                Transform child = _demoObject.FindRecursive(childName);
                
                if (child != null)
                {
                    UpdateResultText($"已找到子对象: {childName}, 路径: {GetFullPath(child)}");
                    
                    // 高亮显示找到的对象
                    HighlightObject(child);
                }
                else
                {
                    UpdateResultText($"未找到名为 {childName} 的子对象");
                }
            }
        }
        
        private void OnGetAllChildrenClicked()
        {
            if (_demoObject != null)
            {
                // 使用GetAllChildren扩展方法获取所有子对象
                List<Transform> children = _demoObject.GetAllChildren();
                
                string result = $"找到 {children.Count} 个子对象:\n";
                foreach (Transform child in children)
                {
                    result += $"- {child.name}\n";
                }
                
                UpdateResultText(result);
                
                // 高亮显示所有子对象
                foreach (Transform child in children)
                {
                    HighlightObject(child);
                }
            }
        }
        
        private void UpdateResultText(string text)
        {
            if (_resultText != null)
            {
                _resultText.text = text;
                Debug.Log(text);
            }
        }
        
        private string GetFullPath(Transform transform)
        {
            string path = transform.name;
            Transform parent = transform.parent;
            
            while (parent != null)
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }
            
            return path;
        }
        
        private void HighlightObject(Transform transform)
        {
            // 临时改变对象颜色以高亮显示
            Renderer renderer = transform.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color originalColor = renderer.material.color;
                renderer.material.color = Color.yellow;
                
                // 2秒后恢复原始颜色
                StartCoroutine(ResetColorAfterDelay(renderer, originalColor, 2f));
            }
        }
        
        private System.Collections.IEnumerator ResetColorAfterDelay(Renderer renderer, Color originalColor, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            if (renderer != null)
            {
                renderer.material.color = originalColor;
            }
        }
    }
} 