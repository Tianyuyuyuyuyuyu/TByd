using System;

namespace TBydFramework.Log.Runtime.Serialization
{
    [Serializable]
    public class StackFrameItem
    {
        private int lineNumber;
        private string fileName;
        private string className;
        private string fullInfo;
        private string methodName;

        public StackFrameItem(string className, string methodName, string fileName, int lineNumber)
        {
            this.className = className;
            this.methodName = methodName;
            this.fileName = fileName;
            this.lineNumber = lineNumber;

            if (string.IsNullOrEmpty(this.fileName))
                this.fullInfo = this.className + '.' + this.methodName + "()";
            else
                this.fullInfo = this.className + '.' + this.methodName + '(' + this.fileName + ':' + this.lineNumber +
                                ')';
        }

        public string ClassName
        {
            get { return className; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
        }

        public string MethodName
        {
            get { return methodName; }
        }

        public string FullInfo
        {
            get { return fullInfo; }
        }
    }
}