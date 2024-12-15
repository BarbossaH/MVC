using View;
using UnityEngine.UI;

namespace GameUI
{
    public class MessageInfo
    {
        public string MessageText;
        public System.Action OkAction;
        public System.Action CancelAction;
    }
    public class MessageView:BaseView
    {
        MessageInfo messageInfo;
        protected override void OnAwake()
        {
            base.OnAwake();
            Find<Button>("okBtn").onClick.AddListener(OnOkBtnClick);
            Find<Button>("noBtn").onClick.AddListener(OnCancelBtnClick);
        }

        public override void Open(params object[] args)
        {
            messageInfo= args[0] as MessageInfo;
            Find<Text>("content/txt").text = messageInfo?.MessageText;
        }

        private void OnOkBtnClick()
        {
            //why not quit game here directly? because I need to initialize the massage info class.
            //If I want to reuse this info class in other places, I have to use this way, which means I just need to initialize it when I want to use it
            messageInfo.OkAction?.Invoke();
        }

        private void OnCancelBtnClick()
        {
            messageInfo.CancelAction?.Invoke();
            GameManager.ViewManager.CloseView(ViewId);
        }
    }
}