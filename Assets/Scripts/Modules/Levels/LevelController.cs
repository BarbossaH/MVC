using Common;
using Controller;
using GameUI;
using MVC;

namespace Controller
{
    public class LevelController:BaseController
    {
        //register each controller into controller manager
        public LevelController()
        {
            GameManager.ViewManager.RegisterView(ViewType.SelectLevelView, new ViewInfo()
            {
                PrefabName = "SelectLevelView",
                Controller = this,
                ParentTransform = GameManager.ViewManager.CanvasTransform
            });
            
            //Initialize the events that this controller response for
            InitModuleEvent();
            InitGlobalEvent();
        }

        public sealed override  void InitModuleEvent()
        {
            RegisterFunc(Defines.OpenSelectLevelView, OpenSelectLevelView);
        }

        private void OpenSelectLevelView(System.Object[] args)
        {
            GameManager.ViewManager.OpenView(ViewType.SelectLevelView,args);
        }

        //register the events to event center
        public sealed override void InitGlobalEvent()
        {
            //because this event isn't to open the main view, instead of opening a component, so here uses event to do implement this
            GameManager.EventCenter.AddEvent(Defines.ShowLevelDesEvent,ShowLevelDesCallback);
            GameManager.EventCenter.AddEvent(Defines.HideLevelDesEvent,HideLevelDesCallback);
        }

        private void ShowLevelDesCallback(object obj)
        {
            GameManager.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDescription();
        }

        private void HideLevelDesCallback(object obj)
        {
            GameManager.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDescription();
        }

        public sealed override void RemoveGlobalEvent()
        {
            base.RemoveGlobalEvent();
            GameManager.EventCenter.RemoveEnvent(Defines.ShowLevelDesEvent,ShowLevelDesCallback);
            GameManager.EventCenter.RemoveEnvent(Defines.HideLevelDesEvent,HideLevelDesCallback);

        }

     
    }
}