using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    /// <summary>
    /// read the csv file(data separated by comma)
    /// </summary>
    public class ConfigData
    {
        //store data in each csv file to the below dictionary
        private readonly Dictionary<int, Dictionary<string,string>> _lineData = new Dictionary<int, Dictionary<string, string>>();

        public readonly string Filename;

        public ConfigData(string filename)
        {
            Filename = filename;
        }

        public TextAsset LoadFile()
        {
            return Resources.Load<TextAsset>($"Data/{Filename}");
        }

        public void Load(string txt)
        {
            string[] lines = txt.Split('\n'); //换行，把数据切成一行一行的
            string[] tiles = lines[0].Trim().Split(','); //split by comma,tile is the key
            
            //从第三行开始读取数据，下标从2开始（这个要根据配置的去做）
            for (int i = 2; i < lines.Length; i++)
            {
                string[] tmpArray = lines[i].Trim().Split(',');
                Dictionary<string, string> data = new Dictionary<string, string>();
                for (int j = 0; j< tmpArray.Length; j++)
                {
                    //add the data of this line into a dictionary
                    data.Add(tiles[j], tmpArray[j]);
                }
                //add each line data (a dictionary) into the _lineData, and use the id field in the data as the key
                _lineData.Add(int.Parse(data["Id"]),data);
            }
        }

        /// <summary>
        /// 得到对应id的每一行的数据，每个数据都对应的key-value
        /// </summary>
        /// <param name="id">id作为这个数据的索引key</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDataById(int id)
        {
            // if (_lineData.ContainsKey(id))
            // {
            //     return _lineData[id];
            // }
            // return null;
            return _lineData.GetValueOrDefault(id);
        }

        public Dictionary<int, Dictionary<string, string>> GetLines()
        {
            return _lineData;
        }
    }
}