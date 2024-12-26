using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace View
{
    public class BaseView : MonoBehaviour,IBaseView
    {
        private bool _isInitialized;
        public int ViewId { get; set; }
        public BaseController Controller { get; set; }

        protected Canvas CanvasComponent; //this canvas belongs to each main ui view, via controlling the enabled variable to show and hide the view, see the SetVisible() method
        private readonly Dictionary<string, GameObject> _mCacheGameObjects = new Dictionary<string, GameObject>();
        private void Awake()
        {
            CanvasComponent = GetComponent<Canvas>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        public bool IsInitialized()
        {
            return _isInitialized;
        }

        public bool IsShown()
        {
           return CanvasComponent.enabled=true;
        }

        public virtual void InitUI()
        {
            
        }

        public virtual void InitData()
        {
            _isInitialized = true;
        }

        /// <summary>
        /// set the canvas of this view enabled that makes this view visible
        /// </summary>
        /// <param name="args"></param>
        public virtual void ShowView(params object[] args)
        {
            SetVisible(true);
        }

        public virtual void Close(params object[] args)
        {
           SetVisible(false);
        }

        public void DestroyView()
        {
           Controller = null;
           Destroy(gameObject);
        }

        public void ApplyFunc(string eventName, params object[] args)
        {
           Controller.ApplyFunc(eventName, args);
        }

        public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
        {
            Controller.ApplyControllerFunc(controllerKey, eventName, args);
        }

        public void SetVisible(bool visible)
        {
            CanvasComponent.enabled = visible;
        }

        public GameObject Find(string res)
        {
            if (_mCacheGameObjects.ContainsKey(res))
            {
                return _mCacheGameObjects[res];
            }
            _mCacheGameObjects.Add(res, transform.Find(res).gameObject);
            
            return _mCacheGameObjects[res];
        }

        // public T Find<T>(string res) where T : Component
        // {
        //     GameObject obj = Find(res);
        //     return obj.GetComponent<T>();
        // }
        
        public T Find<T>(string res)
        {
            GameObject obj = Find(res);
            return obj.GetComponent<T>();
        }
    }
}