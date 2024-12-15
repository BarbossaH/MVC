using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    /// <summary>
    /// manage all configuration files
    /// </summary>
    public class ConfigManager
    {
        //the config files that are ready to load
        private readonly Dictionary<string, ConfigData> _loadList = new Dictionary<string, ConfigData>();
        //the files that have been loaded
        private readonly Dictionary<string, ConfigData> _dataList = new Dictionary<string, ConfigData>();
        
        //register the files that needs to be loaded
        public void RegisterConfig(string filename, ConfigData data)
        {
            _loadList[filename] = data;
        }

        public void LoadAllConfigs()
        {
            foreach (var item in _loadList)
            {
                //read the text in the csv file to memory
                TextAsset textAsset = item.Value.LoadFile();
                //deal with the text in memory to the key-value format
                item.Value.Load(textAsset.text);
                _dataList.Add(item.Value.Filename, item.Value);
            }
            _loadList.Clear();
        }

        public ConfigData GetConfig(string filename)
        {
            if (_dataList.ContainsKey(filename))
            {
                return _dataList[filename];
            }
            return null;
        }
    }
}