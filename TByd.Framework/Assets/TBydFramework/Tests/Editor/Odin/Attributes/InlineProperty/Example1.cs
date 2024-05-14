using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.InlineProperty
{
    public class Example1 : MonoBehaviour
    {
        public Vector3 Vector3;

        public Vector3Int MyVector3Int;

        [InlineProperty(LabelWidth = 13)]
        public Vector2Int MyVector2Int;
        public Vector2Int MyVector2Int_NoInline;
        [Serializable]
        [InlineProperty(LabelWidth = 13)]
        public struct Vector3Int
        {
            [HorizontalGroup]
            public int X;

            [HorizontalGroup]
            public int Y;

            [HorizontalGroup]
            public int Z;
        }

        [Serializable]
        public struct Vector2Int
        {
            [HorizontalGroup]
            public int X;

            [HorizontalGroup]
            public int Y;
        }
    }
}