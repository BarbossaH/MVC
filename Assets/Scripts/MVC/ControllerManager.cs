using System.Collections.Generic;
using System.Linq;
using Common.Def;
using Controller;
using Model;


namespace MVC
{
    /// <summary>
    /// 控制管理器
    /// </summary>
    public class ControllerManager
    {
        private readonly Dictionary<int, BaseController> _controllers = new Dictionary<int, BaseController>();

        public void RegisterModule(int controllerKey, BaseController controller)
        {
            _controllers.TryAdd(controllerKey, controller);
            // if (_controllers.ContainsKey(controllerKey) == false)
            // {
            //     _controllers.Add(controllerKey, controller);
            // }
        }

        public void RegisterModule(ControllerType type, BaseController controller)
        {
            RegisterModule((int)type, controller);
        }

        //z执行所有控制器的init函数
        public void InitAllModules()
        {
            foreach (var controller in _controllers)
            {
                controller.Value.Init();
            }
        }

        public void UnregisterModule(int controllerKey)
        {
            if (_controllers.ContainsKey(controllerKey))
            {
                _controllers.Remove(controllerKey);
            }
        }

        public void Clear()
        {
            _controllers.Clear();
        }

        public void ClearAllModules()
        {
            List<int>Keys = _controllers.Keys.ToList();

            for (int i = 0; i< Keys.Count; i++)
            {
                _controllers[Keys[i]].Destroy();
                _controllers.Remove(Keys[i]);
            }
        }

        //跨模板触发消息
        public void ApplyFunc(int controllerKey, string eventName, System.Object[] args)
        {
            // if (_controllers.ContainsKey(controllerKey))
            // {
            //     _controllers[controllerKey].ApplyFunc(eventName, args);
            // }   
            if (_controllers.TryGetValue(controllerKey, out var controller))
            {
                controller.ApplyFunc(eventName, args);
            }   
        }

        //获取某控制器的model对象
        public BaseModel GetControllerModel(int controllerKey)
        {
            // if (_controllers.ContainsKey(controllerKey))
            // {
            //     return _controllers[controllerKey].GetModel();
            // }
            if (_controllers.TryGetValue(controllerKey, out var module))
            {
                return module.GetModel();
            }
            return null;
        }
    }
}