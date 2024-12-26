namespace BattleMgr
{
    //进入战斗需要处理的逻辑
    public class FightEnter:FightUnitBase
    {
        public override void Init()
        {
            base.Init();
            //initialize the battle map
            GameManager.MapManager.InitTileMap();
            GameManager.FightManager.InitHeroAndEnemy();
        }
    }
}