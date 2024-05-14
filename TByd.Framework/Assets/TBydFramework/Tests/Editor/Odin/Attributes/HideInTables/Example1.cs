using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInTables
{
    public class Example1 : MonoBehaviour
    {
        [PropertySpace(0,40)]
        public MyItem Item = new MyItem();

        [TableList]//以表格形式展示List中的成员
        public List<MyItem> TableItemList = new List<MyItem>()
        {
            new(),
            new(),
            new(),
        };

        [Serializable]
        public class MyItem
        {
            public string A;

            public int B;

            [HideInTables]//此字段在表格中不显示
            public int Hidden;
        }
    }
}