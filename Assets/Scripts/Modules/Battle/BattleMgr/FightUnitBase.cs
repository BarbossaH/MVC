namespace BattleMgr
{
    /// <summary>
    /// 战斗单元
    /// </summary>
    public class FightUnitBase
    {
        public virtual void Init(){}

        public virtual bool Update(float dt)
        {
            return false;
        }
    }
}