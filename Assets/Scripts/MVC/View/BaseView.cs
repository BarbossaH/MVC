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
        
        protected Canvas _canvas;
        protected readonly Dictionary<string, GameObject> m_cache_gameobjects = new Dictionary<string, GameObject>();
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
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
           return _canvas.enabled=true;
        }

        public virtual void InitUI()
        {
            
        }

        public virtual void InitData()
        {
            _isInitialized = true;
        }

        public virtual void Open(params object[] args)
        {
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
            _canvas.enabled = visible;
        }

        public GameObject Find(string res)
        {
            if (m_cache_gameobjects.ContainsKey(res))
            {
                return m_cache_gameobjects[res];
            }
            m_cache_gameobjects.Add(res, transform.Find(res).gameObject);
            
            return m_cache_gameobjects[res];
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