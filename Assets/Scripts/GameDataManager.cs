    using System.Collections.Generic;

    public class GameDataManager
    {
        public readonly List<int> HeroIdList = new();

        public int Money;

        public GameDataManager()
        {
            //todo: I used these magic number that caused me to make a mistake, I should use another way to do with this
            HeroIdList.Add(10001);
            HeroIdList.Add(10002);
            HeroIdList.Add(10003);
        }
    }
