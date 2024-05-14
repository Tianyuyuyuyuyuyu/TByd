//using TBydFramework.Data.Editors;
//using Newtonsoft.Json;
//using NPOI.SS.UserModel;
//using System.IO;
//using System.Text;
//using UnityEngine;

//namespace TBydFramework.Examples.Data
//{
//    public class CustomExportProcessor : ExportProcessor
//    {
//        protected override bool Filter(FileInfo file, ISheet sheet)
//        {
//            //自定义Sheet表单过滤方法，只导出第一个Sheet
//            var workbook = sheet.Workbook;
//            if (workbook.GetSheetIndex(sheet) != 0)
//                return false;
//            return true;
//        }

//        protected override void DoExportSheet(FileInfo file, ISheet sheet, ISheetReader reader, string outputRoot)
//        {
//            string fullname = this.NameGenerator.Generate(outputRoot, file, sheet, "json");
//            StringBuilder text = Parse(reader);
//            File.WriteAllText(fullname, text.ToString());
//            Debug.LogFormat("File:{0} Sheet:{1} OK……", GetRelativePath(file.FullName), sheet.SheetName);
//        }

//        protected StringBuilder Parse(ISheetReader reader)
//        {
//            StringBuilder buf = new StringBuilder();
//            for (int i = reader.StartLine; i <= reader.TotalCount; i++)
//            {
//                var data = reader.ReadLine(i);
//                if (data == null)
//                    continue;

//                string json = JsonConvert.SerializeObject(data, Formatting.None);
//                buf.AppendLine(json);
//            }
//            return buf;
//        }
//    }
//}
