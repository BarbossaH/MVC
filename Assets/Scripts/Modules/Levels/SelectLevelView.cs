using Common;
using GameModel;
using UnityEngine.UI;
using View;

namespace GameUI
{
    public class SelectLevelView:BaseView
    {
        protected override void OnStart()
        {
            base.OnStart();
            Find<Button>("close").onClick.AddListener(OnClose);
        }

        private void OnClose()
        {
            //after creating the view, we should register the view id into view manager first
            GameManager.ViewManager.CloseView(ViewId);

            LoadingModel loadingModel = new LoadingModel()
            {
                SceneName = "start",
                Callback = () =>
                {
                    //after loading the game scene, then open start view
                    Controller.ApplyControllerFunc(ControllerType.GameUIController, Defines.OpenStartView);
                }
            };
            Controller.ApplyControllerFunc(ControllerType.LoadingController,Defines.LoadingScene, loadingModel);
        }

        
        public void ShowLevelDescription()
        {
            Find("level").SetActive(true);
        }

        public void HideLevelDescription()
        {
            Find("level").SetActive(false);
        }
    }
}