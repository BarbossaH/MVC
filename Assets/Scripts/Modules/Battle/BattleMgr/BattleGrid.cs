using System;
using Common.Def;
using UnityEngine;

namespace BattleMgr
{
    public enum GridType
    {
        Empty,
        Obstacle,
    }
    /// <summary>
    /// 战斗地图单元格子
    /// </summary>
    public class BattleGrid:MonoBehaviour
    {
        public int RowIndex;
        public int ColumnIndex;
        public GridType Type;
        
        private SpriteRenderer _selectedGridSprite; //选中格子的图片
        private SpriteRenderer _gridSprite; //所有格子的图片
        private SpriteRenderer _directionSprite;//移动方向图片
        private void Awake()
        {
            _selectedGridSprite = transform.Find("select").GetComponent<SpriteRenderer>();
            _gridSprite = transform.Find("grid").GetComponent<SpriteRenderer>();
            _directionSprite = transform.Find("dir").GetComponent<SpriteRenderer>();
            
            GameManager.EventCenter.AddEvent(gameObject, CallbackFuncName.OnSelectEvent,OnSelectCallback);
        }

        private void OnDestroy()
        {
            GameManager.EventCenter.RemoveEvent(gameObject, CallbackFuncName.OnSelectEvent,OnSelectCallback);

        }

        public void ShowGrid(Color c)
        {
            _gridSprite.enabled = true;
            _gridSprite.color = c;
        }

        public void HideGrid()
        {
            _gridSprite.enabled = false;
        }
        private void OnSelectCallback(object obj)
        {
            GameManager.EventCenter.PostEvent(CallbackFuncName.OnCancelSelectEvent);
        }

        //this class can use the below method because the prefab has mounted the box collider 
        // private void OnMouseDown()
        // {
        //     _selectedGridSprite.enabled = true;
        // }
        private void OnMouseEnter()
        {
            _selectedGridSprite.enabled = true;
        }

        private void OnMouseExit()
        {
            _selectedGridSprite.enabled = false;
        }
    }
}