using System;
using System.Collections.Generic;
using Common.Def;
using UnityEngine;

namespace Character
{
    public class CharacterBase:MonoBehaviour
    {
        public int Id;
        public Dictionary<string, string> DataFromConfig; //配置表中的数据
        public int step;
        public int attack;
        public int type;
        public int currentHp;
        public int maxHp;
        public int rowIndex;
        public int colIndex;
        public SpriteRenderer body; //via this component, we can set the sprite
        public GameObject stopObj;
        public Animator anim;

        private void Awake()
        {
            Transform bodyTransform = transform.Find("body");
            body = bodyTransform.GetComponent<SpriteRenderer>();
            anim = bodyTransform.GetComponent<Animator>();
            stopObj = transform.Find("stop").gameObject;
        }

        protected virtual void Start()
        {
            AddEvents();
        }

        protected virtual void OnDestroy()
        {
            RemoveEvents();
        }

        protected virtual void AddEvents()
        {
            GameManager.EventCenter.AddEvent(gameObject,CallbackFuncName.OnSelectEvent, OnSelectCallback);
            GameManager.EventCenter.AddEvent(CallbackFuncName.OnCancelSelectEvent, OnCancelSelectCallback);
        }

        protected virtual void RemoveEvents()
        {
            GameManager.EventCenter.RemoveEvent(gameObject, CallbackFuncName.OnSelectEvent,OnSelectCallback);
            GameManager.EventCenter.RemoveEnvent(CallbackFuncName.OnCancelSelectEvent, OnCancelSelectCallback);
        }

        private void OnSelectCallback(object obj)
        {
            GameManager.EventCenter.PostEvent(CallbackFuncName.OnCancelSelectEvent);
            // body.color = Color.red;
            GameManager.MapManager.ShowStepGrid(this, step);
        }
        private void OnCancelSelectCallback(object obj)
        {
            // body.color = Color.white;
            GameManager.MapManager.HideStepGrid(this, step);
        }

    }
}