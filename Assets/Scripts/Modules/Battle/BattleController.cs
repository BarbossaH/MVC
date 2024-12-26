
using BattleMgr;
using Common.Def;

namespace Controller
{
    public class BattleController:BaseController
    {
        public BattleController()
        {
            //register the related UIs
            GameManager.ViewManager.RegisterView(ViewType.BattleView, this,1);
            GameManager.ViewManager.RegisterView(ViewType.BattleSelectHeroView, this, 1);
            GameManager.ViewManager.RegisterView(ViewType.DragHeroView, this, 2, true);
            //register the related events
            InitModuleEvent();
        }

        public sealed override void InitModuleEvent()
        {
            base.InitModuleEvent();
            RegisterFunc(CallbackFuncName.OpenBattleView, OnEnterBattleCallback);
        }

   

        private void OnEnterBattleCallback(object[] obj)
        {       
            GameManager.FightManager.ChangeState(GameState.Enter);
            GameManager.ViewManager.OpenView(ViewType.BattleView);
            GameManager.ViewManager.OpenView(ViewType.BattleSelectHeroView);
        }
    }
}