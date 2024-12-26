using Common;
using Common.Def;
using Model;
using View;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// start game UI
    /// </summary>
    public class StartView : BaseView
    {
        protected override void OnAwake( )
        {
            base.OnAwake();
            Find<Button>("startBtn").onClick.AddListener(OnStartGameBtnClicked);
            Find<Button>("setBtn").onClick.AddListener(OnSettingsBtnClicked);
            Find<Button>("quitBtn").onClick.AddListener(OnQuitBtnClicked);
        }

        private void OnStartGameBtnClicked()
        {
            //close the start view ui
            GameManager.ViewManager.CloseView(ViewId);
            
            GameManager.SceneLoader.LoadSceneWithCallback(Controller,"map", ControllerType.LevelController, CallbackFuncName.OpenSelectLevelView);
            
        //     LoadingModel loadingModel = new LoadingModel
        //     {
        //         SceneName = "map",
        //         //set the callback, this callback will be called when finishing loading the map scene.
        //         Callback = ()=>
        //         {
        //             //open select level view
        //             Controller.ApplyControllerFunc(ControllerType.LevelController,CallbackFuncName.OpenSelectLevelView);
        //         }
        //     };
        //     Controller.ApplyControllerFunc(ControllerType.LoadingController, CallbackFuncName.LoadingScene, loadingModel);
        }

        private void OnSettingsBtnClicked()
        {
            ApplyFunc(CallbackFuncName.OpenSettingsView);
        }

        private void OnQuitBtnClicked()
        {
            ApplyFunc(CallbackFuncName.OpenMessageView, new MessageInfo()
            {
                MessageText="Are you sure you want to quit?",
                OkAction = QuitGame,
            });
            // Controller.ApplyControllerFunc(ControllerType.GameUIController,CallbackFuncName.OpenMessageView, new MessageInfo()
            // {
            //     MessageText="Are you sure you want to quit?",
            //     OkAction = QuitGame,
            //     // CancelAction = 
            // });
        }

        private void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}