using Common.Def;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Controller
{
    //loading scene controller
    public class LoadingController:BaseController
    {
        private AsyncOperation _asyncOperation;
        public LoadingController() : base()
        {
            GameManager.ViewManager.RegisterView(ViewType.LoadingView, this);
     
            InitModuleEvent();
        }

        public sealed override void InitModuleEvent()
        {
            base.InitModuleEvent();
            RegisterFunc(CallbackFuncName.LoadingScene, LoadSceneCallback);
        }

        private void LoadSceneCallback(System.Object[] args)
        {
            LoadingModel loadingModel = (LoadingModel)args[0];
            SetModel(loadingModel);
            
            //open the loading UI(view)
            GameManager.ViewManager.OpenView(ViewType.LoadingView);
            
            _asyncOperation = SceneManager.LoadSceneAsync(loadingModel.SceneName);
            Debug.Assert(_asyncOperation != null, nameof(_asyncOperation) + " != null");
            _asyncOperation.completed += OnFinishLoadingCallback;
        }
        
        //加载场景后回调
        private void OnFinishLoadingCallback(AsyncOperation operation)
        {
            _asyncOperation.completed -= OnFinishLoadingCallback;
            
      
            GameManager.TimerManager.RegisterTimer(0.25f, delegate()
            {
                GetModel<LoadingModel>().Callback?.Invoke();
                GameManager.ViewManager.CloseView((int) ViewType.LoadingView);
            });
        }
    }
    
    
}