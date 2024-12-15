using Controller;

namespace View
{
    public interface IBaseView
    {
        bool IsInitialized();
        bool IsShown();
        void InitUI();
        void InitData();
        void Open(params object[] args);
        void Close(params object[] args);

        void DestroyView();
        
        void ApplyFunc(string eventName, params object[] args); //trigger its own events 
        
        void ApplyControllerFunc(int controllerKey, string eventName, params object[] args); //触发其他控制器模块事件
        
        void SetVisible(bool visible);
        
        int ViewId{set; get;} 
        
        BaseController Controller{set; get;} //面板所属控制器
    }
}