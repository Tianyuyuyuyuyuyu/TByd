using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.Title
{
    public class Example1 : MonoBehaviour
    {
        [Title("Titles and Headers")]
        public string MyTitle = "My Dynamic Title";
        public string MySubtitle = "My Dynamic Subtitle";

        [Title("Static title")]
        public int C;
        public int D;

        [Title("Static title", "Static subtitle")]
        public int E;
        public int F;

        [Title("$MyTitle", "$MySubtitle")]
        public int G;
        public int H;

        [Title("Non bold title", "$MySubtitle", bold: false)]
        public int I;
        public int J;

        [Title("Non bold title", "With no line seperator", horizontalLine: false, bold: false)]
        public int K;
        public int L;

        [Title("$MyTitle", "$MySubtitle", TitleAlignments.Right)]
        public int M;
        public int N;

        [Title("$MyTitle", "$MySubtitle", TitleAlignments.Centered)]
        public int O;
        public int P;

        [Title("$MyTitle", "$MySubtitle", titleAlignment: TitleAlignments.Left)]
        public int Q;
        public int R;
        [Title("$MyTitle", "$MySubtitle", titleAlignment: TitleAlignments.Split)]
        public int S;
        public int T;

        [ShowInInspector]
        [Title("Title on a Property")]
        public int U { get; set; }

        [Title("Title on a Method")]
        [Button]
        public void DoNothing()
        { }

        [Title("@DateTime.Now.ToString(\"dd:MM:yyyy\")", "@DateTime.Now.ToString(\"HH:mm:ss\")")]
        public int Expresion;

        public string Combined { get { return this.MyTitle + " - " + this.MySubtitle; } }
    }
}