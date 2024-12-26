using Common;
using Common.Def;
using View;
using Model;

namespace Controller
{
    public class LevelController:BaseController
    {
        //register each controller into controller manager
        public LevelController()
        {
            SetModel(new LevelModel());
            
            GameManager.ViewManager.RegisterView(ViewType.SelectLevelView, this);
            
            //Initialize the events that this controller response for
            InitModuleEvent();
            InitGlobalEvent();
        }

        public override void Init()
        {
            base.Init();
            _model.Init();
        }

        public sealed override  void InitModuleEvent()
        {
            RegisterFunc(CallbackFuncName.OpenSelectLevelView, OpenSelectLevelView);
        }

        private void OpenSelectLevelView(System.Object[] args)
        {
            GameManager.ViewManager.OpenView(ViewType.SelectLevelView,args);
        }

        //register the events to event center
        public sealed override void InitGlobalEvent()
        {
            //because this event isn't to open the main view, instead of opening a component, so here uses event to do implement this
            GameManager.EventCenter.AddEvent(CallbackFuncName.ShowLevelDesEvent,ShowLevelDesCallback);
            GameManager.EventCenter.AddEvent(CallbackFuncName.HideLevelDesEvent,HideLevelDesCallback);
        }

        private void ShowLevelDesCallback(System.Object obj)
        {
            // Debug.Log("LevelId:"+ obj.ToString());
            LevelModel lm = GetModel<LevelModel>();
            lm.CurrentLevel = lm.GetLevelData((int)obj);
            // lm.currentLevel = lm.GetLevelData(int.Parse(obj.ToString()));

            GameManager.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDescription();
        }

        private void HideLevelDesCallback(System.Object obj)
        {
            GameManager.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDescription();
        }

        public sealed override void RemoveGlobalEvent()
        {
            base.RemoveGlobalEvent();
            GameManager.EventCenter.RemoveEnvent(CallbackFuncName.ShowLevelDesEvent,ShowLevelDesCallback);
            GameManager.EventCenter.RemoveEnvent(CallbackFuncName.HideLevelDesEvent,HideLevelDesCallback);

        }

     
    }
}