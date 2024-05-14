using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.GUIColor
{
    public class Example1 : MonoBehaviour
    {
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        public int ColoredInt1;

        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        public int ColoredInt2;

        [ButtonGroup]
        [GUIColor(0, 1, 0)]
        private void Apply()
        {
            Debug.Log("应用");
        }

        [ButtonGroup]
        [GUIColor(1, 0.6f, 0.4f)]
        private void Cancel()
        {
            Debug.Log("取消");
        }

        [GUIColor("GetButtonColor")]
        [Button(ButtonSizes.Gigantic)]
        private static void IAmFabulous()
        {
        }

        private static Color GetButtonColor()
        {
            Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
            return Color.HSVToRGB(Mathf.Cos((float)UnityEditor.EditorApplication.timeSinceStartup + 1f) * 0.225f + 0.325f, 1, 1);
        }

        [Button(ButtonSizes.Large)]
        [GUIColor("CustomColor")]
        private static void Expressive_Two()
        {
        }

# if UNITY_EDITOR
        public Color CustomColor()
        {
            return Color.Lerp(Color.red, Color.green, Mathf.Sin((float)EditorApplication.timeSinceStartup));
        }
# endif
    }
}