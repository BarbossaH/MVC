using System.Collections.Generic;
using Config;
using UnityEngine;

namespace Model
{
    public class LevelData
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string SceneName;
        public readonly string Des;
        public bool IsFinished;

        public LevelData(Dictionary<string, string> data)
        {
            Id = int.Parse(data["Id"]);
            Name = data["Name"];
            SceneName = data["SceneName"];
            Des = data["Des"];
            IsFinished = false;
        }
    }
    //data of levels
    public class LevelModel:BaseModel
    {
        private ConfigData _levelConfig;
        private readonly Dictionary<int, LevelData> _levels = new();
        public LevelData CurrentLevel;

        public override void Init()
        {
            //because when starting the game, the configuration files have been loaded, so we can get the level data
            _levelConfig = GameManager.ConfigManager.GetConfig("level");
            
            //in the configuration files, one row of data is the whole information of a level
            foreach (var item in _levelConfig.GetLines())
            {
                LevelData data = new LevelData(item.Value);
                _levels.Add(data.Id, data);
            }
        }

        public LevelData GetLevelData(int levelId)
        {
            return _levels[levelId];
        }
    }
}