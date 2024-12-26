using Common.Def;
using Common.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 用户控制管理器，键盘操作，鼠标操作
/// </summary>
    public class UserInputManager
    {
        readonly Camera _mainCamera = Camera.main;
        public void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //click UI
                }
                else
                {
                    //check what is clicked
                    Tools.ScreenPointToRay2D(_mainCamera, Input.mousePosition, (col) =>
                    {
                        if (col != null)
                        {
                            //检测到鼠标点击的地方有带有碰撞体的物体
                            GameManager.EventCenter.PostEvent(col.gameObject,CallbackFuncName.OnSelectEvent);
                        }
                        else
                        {
                            //执行未选中
                            GameManager.EventCenter.PostEvent(CallbackFuncName.OnCancelSelectEvent);
                        }
                    });
                }
            }
        }
    }
