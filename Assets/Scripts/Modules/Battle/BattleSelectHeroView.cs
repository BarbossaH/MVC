
using System.Collections.Generic;
using Controller;
using Config;
using UnityEngine;

namespace View
{
    public class BattleSelectHeroView:BaseView
    {
        public override void ShowView(params object[] args)
        {
            base.ShowView(args);
            InitView();
        }

        private void InitView()
        {
            GameObject heroIconPrefab = Find("bottom/grid/item");
            Transform iconParent = Find("bottom/grid").transform;
            
            List<int> heroIdList = GameManager.GameDataManager.HeroIdList;
            for (int i = 0; i < heroIdList.Count; i++)
            {
                int heroId = heroIdList[i];
                //get all heroes configuration data which consists of the data in each line 
                ConfigData heroData = GameManager.ConfigManager.GetConfig("player");
                // Debug.Log(heroData);
                //get the hero configuration information by id
                var data = heroData.GetDataById(heroId);
                GameObject heroIcon = GameObject.Instantiate(heroIconPrefab, iconParent);
                heroIcon.SetActive(true);
                HeroItem heroItem = heroIcon.AddComponent<HeroItem>();
                //initialize heroItem with data
                heroItem.InitHeroData(data);
            }


        }
    }
}