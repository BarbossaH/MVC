using Common;
using Common.Def;

namespace Controller
{
    /// <summary>
    /// 游戏主控制器（处理游戏开始，保存，退出等操作）
    /// </summary>
    public class GameController:BaseController
    {
        public GameController() : base()
        {
            //目前没有视图
            
            InitModuleEvent();
            InitGlobalEvent();
        }

        public override void Init()
        {
            base.Init();
            //调用GameUIController 触发面板事件
            //流程是通过跨模块的找到对应的controller，而对应的controller执行要操作的界面，因为controller的存在就是为了操作界面的
            //而在游戏启动的时候，所有的controller是准备好了的，而界面的必要的信息也在创建controller对象时也准备好了的，所以要开打界面只需要使用view manager去调用好了，manager会根据该界面是否已经打开过来处理加载和显示的逻辑
            ApplyControllerFunc(ControllerType.GameUIController,CallbackFuncName.OpenStartView);
        }
    }
}