using UnityEngine;
using UnityEngine.UI;
using TByd.Core.Utils.Runtime;

namespace TByd.Core.Utils.Samples
{
    /// <summary>
    /// 展示StringUtils类的使用示例
    /// </summary>
    public class StringUtilsExample : MonoBehaviour
    {
        [SerializeField] private Text _outputText;
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _checkEmptyButton;
        [SerializeField] private Button _generateRandomButton;
        [SerializeField] private Button _toSlugButton;
        [SerializeField] private Button _truncateButton;
        [SerializeField] private Button _splitButton;
        [SerializeField] private Slider _lengthSlider;
        [SerializeField] private Text _sliderValueText;

        private void Start()
        {
            // 初始化UI
            if (_checkEmptyButton != null)
                _checkEmptyButton.onClick.AddListener(CheckIsNullOrWhiteSpace);
            
            if (_generateRandomButton != null)
                _generateRandomButton.onClick.AddListener(GenerateRandom);
            
            if (_toSlugButton != null)
                _toSlugButton.onClick.AddListener(ToSlug);
            
            if (_truncateButton != null)
                _truncateButton.onClick.AddListener(Truncate);
            
            if (_splitButton != null)
                _splitButton.onClick.AddListener(Split);
            
            if (_lengthSlider != null)
            {
                _lengthSlider.onValueChanged.AddListener(UpdateSliderValueText);
                UpdateSliderValueText(_lengthSlider.value);
            }

            // 显示使用说明
            ShowInstructions();
        }

        private void ShowInstructions()
        {
            string instructions = "StringUtils 使用示例:\n\n" +
                                 "1. 输入文本，然后点击按钮测试不同的字符串操作\n" +
                                 "2. 使用滑块调整生成随机字符串的长度或截断长度\n" +
                                 "3. 结果将显示在此文本区域";
            
            SetOutputText(instructions);
        }

        private void CheckIsNullOrWhiteSpace()
        {
            string input = GetInputText();
            bool isEmpty = StringUtils.IsNullOrWhiteSpace(input);
            
            SetOutputText($"IsNullOrWhiteSpace 结果:\n\n" +
                         $"输入: \"{input}\"\n" +
                         $"结果: {(isEmpty ? "为空" : "不为空")}");
        }

        private void GenerateRandom()
        {
            int length = Mathf.RoundToInt(_lengthSlider.value);
            string randomString = StringUtils.GenerateRandom(length);
            string randomStringWithSpecial = StringUtils.GenerateRandom(length, true);
            
            SetOutputText($"GenerateRandom 结果:\n\n" +
                         $"长度: {length}\n" +
                         $"随机字符串: {randomString}\n" +
                         $"带特殊字符的随机字符串: {randomStringWithSpecial}");
        }

        private void ToSlug()
        {
            string input = GetInputText();
            string slug = StringUtils.ToSlug(input);
            
            SetOutputText($"ToSlug 结果:\n\n" +
                         $"输入: \"{input}\"\n" +
                         $"Slug: \"{slug}\"");
        }

        private void Truncate()
        {
            string input = GetInputText();
            int maxLength = Mathf.RoundToInt(_lengthSlider.value);
            string truncated = StringUtils.Truncate(input, maxLength);
            string truncatedCustom = StringUtils.Truncate(input, maxLength, "---");
            
            SetOutputText($"Truncate 结果:\n\n" +
                         $"输入: \"{input}\"\n" +
                         $"最大长度: {maxLength}\n" +
                         $"默认截断: \"{truncated}\"\n" +
                         $"自定义后缀: \"{truncatedCustom}\"");
        }

        private void Split()
        {
            string input = GetInputText();
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Split 结果:\n");
            sb.AppendLine($"输入: \"{input}\"\n");
            sb.AppendLine("分割结果 (使用逗号分隔):");
            
            int index = 0;
            foreach (var part in StringUtils.Split(input, ','))
            {
                sb.AppendLine($"{index++}: \"{part}\"");
            }
            
            SetOutputText(sb.ToString());
        }

        private void UpdateSliderValueText(float value)
        {
            if (_sliderValueText != null)
                _sliderValueText.text = Mathf.RoundToInt(value).ToString();
        }

        private string GetInputText()
        {
            return _inputField != null ? _inputField.text : string.Empty;
        }

        private void SetOutputText(string text)
        {
            if (_outputText != null)
                _outputText.text = text;
        }
    }
} 