using Common;
using Common.Def;
using Model;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace View
{
    public class SelectLevelView:BaseView
    {
        protected override void OnStart()
        {
            base.OnStart();
            Find<Button>("close").onClick.AddListener(OnCloseBtnClicked);
            Find<Button>("level/fightBtn").onClick.AddListener(OnFightBtnClicked);
        }

        private void OnFightBtnClicked()
        {
            GameManager.ViewManager.CloseView(ViewId);
            //reset the camera position
            GameManager.CameraManager.ResetPos();
            //loading battle scene. I guess using loading controller to load the scene
            string sceneName = Controller.GetModel<LevelModel>().CurrentLevel.SceneName;
            GameManager.SceneLoader.LoadSceneWithCallback(Controller, sceneName, ControllerType.BattleController,CallbackFuncName.OpenBattleView);
        }

        private void OnCloseBtnClicked()
        {
            //after creating the view, we should register the view id into view manager first
            GameManager.ViewManager.CloseView(ViewId);

            GameManager.SceneLoader.LoadSceneWithCallback(Controller,"start", ControllerType.GameUIController, CallbackFuncName.OpenStartView);
        }

        
        public void ShowLevelDescription()
        {
            Find("level").SetActive(true);
            LevelData currentLevel = Controller.GetModel<LevelModel>().CurrentLevel;
            Find<Text>("level/name/txt").text = currentLevel.Name;
            Find<Text>("level/des/txt").text = currentLevel.Des;
        }

        public void HideLevelDescription()
        {
            Find("level").SetActive(false);
        }
    }
}