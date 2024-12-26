using System.Collections.Generic;
using BattleMgr;
using Common.Def;
using Common.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controller
{
    /// <summary>
    /// 处理拖拽英雄图标的脚本
    /// </summary>
    public class HeroItem:MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
    {
        private Dictionary<string, string> _data;
        private Image _image;
        private void Awake()
        {
            _image = transform.Find("icon").GetComponent<Image>();
        }

        //we must notice that the start method is called at the next frame
        private void Start()
        {
            // Debug.Log("Icon/"+_data["Icon"]);
            // transform.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/"+_data["Icon"]);
            _image.SetIcon(_data["Icon"]);
        }
        
        public void InitHeroData(Dictionary<string, string> data)
        {
            _data = data;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //这个项目中，图标当作一个界面处理，之前我做的项目是当作一个game object处理，当作什么处理取决于需求，如果是拖拽图标，那还是界面，但是如果是种树，那就要显示物体在场景中的样子，那就是不同的处理，但是实际上我觉得还是当作实际场景中的样子比较合理；
            GameManager.ViewManager.OpenView(ViewType.DragHeroView, _data["Icon"]);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameManager.ViewManager.CloseView((int)ViewType.DragHeroView);
            //if this position can be put down a hero, then do it
            Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, (col) =>
            {
                if (col != null)
                {
                    BattleGrid grid = col.GetComponent<BattleGrid>();
                    // Debug.Log(grid);
                    if (grid != null)
                    {
                        // Debug.Log(grid.RowIndex.ToString()+" "+grid.ColumnIndex.ToString());
                        Destroy(gameObject); //销毁当前的icon
                        //创建英雄
                        GameManager.FightManager.CreateHero(grid, _data);
                    }
                }
            });
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}