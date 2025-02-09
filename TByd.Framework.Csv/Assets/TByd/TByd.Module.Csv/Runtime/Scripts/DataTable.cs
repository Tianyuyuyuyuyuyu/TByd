using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TByd.Framework.Csv
{
	public class DataTable
	{
		private List<string> _fileds = new List<string>();
		private List<DataRow> _data_rows = new List<DataRow>();

        internal void SetFileds(List<string> fields) { _fileds = fields; }

        internal void InsertRow(DataRow data_row)
		{
			data_row._SetFields(_fileds);
			_data_rows.Add(data_row);
		}

        internal DataRow GetDataRow(int index)
		{
			return _data_rows[index];
		}

		internal int GetRowCount()
		{
			return _data_rows.Count;
		}
	}
}
