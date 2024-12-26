using System;
using Common.Def;
using Controller;
using Model;

namespace Common
{
    public class SceneLoader
    {
        private BaseController _controller;

        public void LoadSceneWithCallback(string sceneName, Action callback)
        {
            LoadingModel model = new LoadingModel()
            {
                SceneName = sceneName,
                Callback = callback
            };
            _controller.ApplyControllerFunc(ControllerType.LoadingController, CallbackFuncName.LoadingScene, model);
        }
        /// <summary>
        /// scene switching
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="sceneName"></param>
        /// <param name="callbackControllerType">the controller type that opens the UIs for the next scene</param>
        /// <param name="callbackFuncName"></param>
        public void LoadSceneWithCallback(BaseController controller, string sceneName,
            ControllerType callbackControllerType, string callbackFuncName="")
        {
            _controller = controller;
            LoadSceneWithCallback(sceneName, () =>
            {
                if (callbackFuncName != string.Empty)
                {
                    _controller.ApplyControllerFunc(callbackControllerType, callbackFuncName);
                }
            });
        }
    }
}