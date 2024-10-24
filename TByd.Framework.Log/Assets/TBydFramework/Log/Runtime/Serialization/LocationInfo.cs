using System;
using System.Collections.Generic;

namespace TBydFramework.Log.Runtime.Serialization
{
    [Serializable]
    public class LocationInfo
    {
        private string className;
        private string fileName;
        private int lineNumber;
        private string methodName;
        private string fullInfo;
        private StackFrameItem[] stackFrames;

        public LocationInfo(log4net.Core.LocationInfo locationInfo)
        {
            this.className = locationInfo.ClassName;
            this.fileName = locationInfo.FileName;
            if (!int.TryParse(locationInfo.LineNumber, out this.lineNumber))
                this.lineNumber = 0;
            this.methodName = locationInfo.MethodName;

            if (string.IsNullOrEmpty(this.fileName))
                this.fullInfo = this.className + '.' + this.methodName + "()";
            else
                this.fullInfo = this.className + '.' + this.methodName + '(' + this.fileName + ':' + this.lineNumber +
                                ')';

            var frames = locationInfo.StackFrames;
            if (frames == null)
                return;

            List<StackFrameItem> list = new List<StackFrameItem>();
            foreach (var frame in frames)
            {
                try
                {
                    if (frame == null)
                        continue;

                    var method = frame.Method;
                    string methodName = method.Name;
                    string className = frame.ClassName;
                    StackFrameItem item = new StackFrameItem(className, methodName, frame.FileName,
                        int.Parse(frame.LineNumber));
                    list.Add(item);
                }
                catch (Exception)
                {
                }
            }

            this.stackFrames = list.ToArray();
        }

        public LocationInfo(string className, string methodName, string fileName, int lineNumber)
        {
            this.className = className;
            this.fileName = fileName;
            this.lineNumber = lineNumber;
            this.methodName = methodName;
            this.fullInfo = this.className + '.' + this.methodName + '(' + this.fileName +
                            ':' + this.lineNumber + ')';
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

        public StackFrameItem[] StackFrames
        {
            get { return stackFrames; }
        }
    }
}