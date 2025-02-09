using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TByd.Framework.Csv
{
    public interface IConfig
    {
        int cfg_id { get; }
        void FromRow(DataRow row);
    }
    public class GameConfigManager<ConfigBase> where ConfigBase : class, IConfig, new()
    {
        public Dictionary<int, ConfigBase> dic_config { get; protected set; } = new Dictionary<int, ConfigBase>();

        public GameConfigManager()
        {

        }

        public GameConfigManager(String path)
        {
            AddConfigs(path);
        }

        protected void AddConfigs(String path)
        {
            AddConfigs<ConfigBase>(path);
        }

        protected void AddConfigs<Config>(String path) where Config : class, ConfigBase,new()
        {
            var table = ConfigManager.Instance.GetConfigTable(path);

            var count = table.GetRowCount();

            if (count <= 0)
            {
                Debug.LogErrorFormat("GameConfigManager table count <= 0, path: {0}", path);
            }

            for (var i = 0; i < count; i++)
            {
                var row = table.GetDataRow(i);

                if (row == null)
                {
                    continue;
                }
                var cfg = new Config();

                try
                {
                    cfg.FromRow(row);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Table name:{0}, error:{1}", path, e.Message);
                }

                dic_config[cfg.cfg_id] = cfg;

                InitializeConfig(cfg);
            }
        }

        protected virtual void InitializeConfig(ConfigBase cfg)
        {

        }


        public ConfigBase GetConfig(int cfg_id)
        {
            return GetConfig<ConfigBase>(cfg_id);
        }

        public Config GetConfig<Config>(int cfg_id) where Config : class, ConfigBase
        {
            ConfigBase config;

            var success = dic_config.TryGetValue(cfg_id, out config);

            if (success == false)
            {
                return default(Config);
            }
            return config as Config;
        }

        public List<ConfigBase> GetAllConfig()
        {
            if (dic_config == null) return new List<ConfigBase>();
            return dic_config.Values.ToList();
        }
    }

    public interface IGameConfig
    {
        void FromRow(DataRow row);
    }

    public class SingletonGameConfigBase
    {
        protected string _cfg_path;
        protected bool _cfg_loaded = false;
        public SingletonGameConfigBase(string cfg_path)
        {
            _cfg_path = cfg_path;
            Load(false);
        }

        public void Load(bool reload = false)
        {
            if (_cfg_loaded && reload == false) return;
            lock (this)
            {
                if (_cfg_loaded && reload == false) return;
                var table = CSVService.Instance.FetchRows(_cfg_path, null);
                if(table.GetRowCount() > 0)
                {
                    FromRow(table.GetDataRow(0));
                }
                _cfg_loaded = true;
                OnConfigLoaded();
            }
        }

        protected virtual void FromRow(DataRow row)
        {

        }

        protected virtual void OnConfigLoaded()
        {

        }
    }

    public class GameConfigManager<KType, VType> where VType : IGameConfig, new()
    {
        protected string _cfg_path;
        protected Dictionary<KType, VType> _config_map;
        public Dictionary<KType, VType> ConfigMap { get { return _config_map; } }
#if UNITY_EDITOR
        public GameConfigManager(string cfg_path, bool is_editor = false)
        {
            _cfg_path = cfg_path;
            Load(false, is_editor);
        }
#else
        public GameConfigManager(string cfg_path)
        {
            _cfg_path = cfg_path;
            Load();
        }
#endif

        public VType GetConfig(KType key)
        {
            return _config_map.TryGetValue(key, out var config) ? config :  default;
        }

        public void Load(bool reload = false, bool is_editor = false)
        {
            if (_config_map != null && reload == false) return;
            lock (this)
            {
                if (_config_map != null && reload == false) return;
                _config_map = new Dictionary<KType, VType>();
#if UNITY_EDITOR
                DataTable table = CSVService.Instance.FetchRows(_cfg_path, null, is_editor);
#else
                DataTable table = CSVService.getInstance().FetchRows(_cfg_path, null);
#endif
                int rowCount = table.GetRowCount();
                Type key_type = typeof(KType);
                key_type = Nullable.GetUnderlyingType(key_type) ?? key_type;
                for (int i = 0; i < rowCount; ++i)
                {
                    DataRow row = table.GetDataRow(i);
                    VType config = new VType();
                    config.FromRow(row);
                    KType key = (KType)Convert.ChangeType(row._GetString(0), key_type);
                    OnConfigPreLoad(ref key, config);
                    try
                    {
                        _config_map.Add(key, config);
                    }
                    catch
                    {
                        Debug.Log(key);
                        throw;
                    }
                    OnConfigLoaded(key, config);
                }
                OnConfigAllLoaded();
            }
        }

        protected virtual void OnConfigPreLoad(ref KType key, VType config)
        {

        }
        
        protected virtual void OnConfigLoaded(KType key, VType config)
        {

        }

        protected virtual void OnConfigAllLoaded()
        {

        }
    }
}
