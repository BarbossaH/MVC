using System.Collections.Generic;
using Model;
using View;
using UnityEngine;

namespace Controller
{
    public class BaseController
    {
        private readonly Dictionary<string, System.Action<object[]>> _messages = new Dictionary<string, System.Action<object[]>>();
        protected BaseModel _model;
        
        //注册后调用的初始化函数，要所有的控制器初始化后执行
        public virtual void Init(){}
        public virtual void OnLoadView(IBaseView view){}
        
        public virtual void OpenView(IBaseView view){}
        
        public virtual void CloseView(IBaseView view){}

        public void RegisterFunc(string eventName, System.Action<object[]> callback)
        {
            if (!_messages.TryAdd(eventName, callback))
            {
                _messages[eventName] += callback;
            }
            
            // if (!_messages.ContainsKey(eventName))
            // {
            //     _messages.Add(eventName, callback);
            // }
            // else
            // {
            //     _messages[eventName] += callback;
            // }
        }

        public void UnregisterFunc(string eventName)
        {
            if (_messages.ContainsKey(eventName))
            {
                _messages.Remove(eventName);
            }
        }

        public void ApplyFunc(string eventName, params object[] args)
        {
            if (_messages.ContainsKey(eventName))
            {
                _messages[eventName].Invoke(args);
            }
            else
            {
                Debug.Log("Error"+eventName);
            }
        }

        public void ApplyControllerFunc(int controllerId, string eventName, params object[] args)
        {
            //trigger other controller events
            GameManager.ControllerManager.ApplyFunc(controllerId, eventName, args);
        }

        public void ApplyControllerFunc(ControllerType type, string eventName, params object[] args)
        {
            ApplyControllerFunc((int)type, eventName, args);
        }

        public void SetModel(BaseModel model)
        {
            _model = model;
            _model.Controller = this;
        }

        public BaseModel GetModel()
        {
            return _model;
        }

        public T GetModel<T>() where T : BaseModel
        {
            return _model as T;
        }
        public BaseModel GetControllerModel(int controllerId)
        {
            return GameManager.ControllerManager.GetControllerModel(controllerId);
        }
        
        //delete controller
        public virtual void Destroy()
        {
            RemoveModuleEvent();
            RemoveGlobalEvent();
        }
        //initialize this module event
        public virtual void InitModuleEvent(){}
        public virtual void RemoveModuleEvent(){}
        public virtual void InitGlobalEvent(){}
        public virtual void RemoveGlobalEvent(){}
    }
}