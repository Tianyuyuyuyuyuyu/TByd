using System;
using System.Collections.Generic;
using UnityEngine;

namespace TByd.Framework.Csv
{
	public class ConfigManager
	{
        public static  ConfigManager Instance =new ConfigManager();

        Dictionary<string, DataTable> _data_table_map = new Dictionary<string, DataTable>();
		Dictionary<string, Dictionary<int, int>> _key_row_maps = new Dictionary<string, Dictionary<int, int>>();

		public void Init(Func<string, string> get_csv_path_func, Func<string, TextAsset> get_csv_asset_func)
		{
			CSVService.Instance.Init(get_csv_path_func, get_csv_asset_func);
		}
		public string GetValue(string config_name, string field, int key)
		{
			DataRow row = GetDataRow(config_name, key);
			return row.GetString(field);
		}

		public DataRow GetDataRow(string config_name, int key)
		{
			DataTable table = GetConfigTable(config_name);
			int row_index = _FindLineByKey(table, config_name, key);
			if (row_index >= 0)
			{
				return table.GetDataRow(row_index);
			}
			return null;
		}

		public DataTable GetConfigTable(string config_name, bool reload = false)
		{
			DataTable table;
			if (_data_table_map.TryGetValue(config_name, out table))
            {
                if (reload)
                {
                    _data_table_map.Remove(config_name);
                }
                else
                {
                    return table;
                }
            }

			table = CSVService.Instance.FetchRows(config_name);
			_data_table_map.Add(config_name, table);

			return table;
		}

		private int _FindLineByKey(DataTable table, string config_name, int key)
		{
			Dictionary<int, int> key_row_map = _GetKeyRowMap(table, config_name);
			int row_index;
			if (key_row_map.TryGetValue(key, out row_index)) return row_index;
			return -1;	
		}

		private Dictionary<int, int> _GetKeyRowMap(DataTable table, string config_name)
		{
			Dictionary<int, int> key_row_map;
			if (_key_row_maps.TryGetValue(config_name, out key_row_map)) return key_row_map;

			key_row_map = new Dictionary<int, int>();
			for (int i = 0; i < table.GetRowCount(); ++i)
			{
				int key = table.GetDataRow(i)._GetInt32(0);
				if (key_row_map.ContainsKey(key) == false)
				{
					key_row_map.Add(key, i);
				}
			}
			_key_row_maps.Add(config_name, key_row_map);
			return key_row_map;
		}
	}
}
