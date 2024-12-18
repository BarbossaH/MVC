using Common;
using MVC;

namespace Controller
{
    //处理一些游戏中通用UI的控制器（设置界面，提示界面，开始游戏界面等都在这个控制器注册）
    public sealed class GameUIController:BaseController
    {
        public GameUIController()
        {
            //register view into game ui manager, and then manager can open the view based on the view information
            
            //start game view
            GameManager.ViewManager.RegisterView(ViewType.StartView, new ViewInfo()
            {
                PrefabName = "StartView",
                Controller = this,
                ParentTransform = GameManager.ViewManager.CanvasTransform,
                SortingOrder = 0
            } );
            
            //setting UI
            GameManager.ViewManager.RegisterView(ViewType.SettingsView, new ViewInfo()
            {
                PrefabName = "SetView",
                Controller = this,
                ParentTransform = GameManager.ViewManager.CanvasTransform,
                SortingOrder = 1
            });
            
            GameManager.ViewManager.RegisterView(ViewType.MessageView, new ViewInfo()
            {
                PrefabName = "MessageView",
                Controller = this,
                ParentTransform = GameManager.ViewManager.CanvasTransform,
                SortingOrder = 999
            });
            
            InitModuleEvent();
            InitGlobalEvent();
        }

        //each controller has a manager of events, which is a dictionary. After registering the events, we can call these events based on the key, which is written in the base controller class.
        public override void InitModuleEvent()
        {
            RegisterFunc(Defines.OpenStartView,OpenStartView); //注册打开开始面板
            RegisterFunc(Defines.OpenSettingsView,OpenSettingsView);
            RegisterFunc(Defines.OpenMessageView,OpenMessageView);
        }
        
        //test module register event example
        private void OpenStartView(object[] args)
        {
            GameManager.ViewManager.OpenView(ViewType.StartView,args);
        }

        private void OpenSettingsView(object[] args)
        {
            GameManager.ViewManager.OpenView(ViewType.SettingsView,args);
        }

        private void OpenMessageView(object[] args)
        {
            GameManager.ViewManager.OpenView(ViewType.MessageView,args);
        }
    }
}