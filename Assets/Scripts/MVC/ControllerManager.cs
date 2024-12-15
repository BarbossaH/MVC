using System.Collections.Generic;
using System.Linq;
using Controller;
using Model;

namespace MVC
{
    /// <summary>
    /// 控制管理器
    /// </summary>
    public class ControllerManager
    {
        private readonly Dictionary<int, BaseController> _modules = new Dictionary<int, BaseController>();

        public void RegisterModule(int controllerKey, BaseController controller)
        {
            _modules.TryAdd(controllerKey, controller);
            // if (_modules.ContainsKey(controllerKey) == false)
            // {
            //     _modules.Add(controllerKey, controller);
            // }
        }

        public void RegisterModule(ControllerType type, BaseController controller)
        {
            RegisterModule((int)type, controller);
        }

        //z执行所有控制器的init函数
        public void InitAllModules()
        {
            foreach (var module in _modules)
            {
                module.Value.Init();
            }
        }

        public void UnregisterModule(int controllerKey)
        {
            if (_modules.ContainsKey(controllerKey))
            {
                _modules.Remove(controllerKey);
            }
        }

        public void Clear()
        {
            _modules.Clear();
        }

        public void ClearAllModules()
        {
            List<int>Keys = _modules.Keys.ToList();

            for (int i = 0; i< Keys.Count; i++)
            {
                _modules[Keys[i]].Destroy();
                _modules.Remove(Keys[i]);
            }
        }

        //跨模板触发消息
        public void ApplyFunc(int controllerKey, string eventName, System.Object[] args)
        {
            // if (_modules.ContainsKey(controllerKey))
            // {
            //     _modules[controllerKey].ApplyFunc(eventName, args);
            // }   
            if (_modules.TryGetValue(controllerKey, out var module))
            {
                module.ApplyFunc(eventName, args);
            }   
        }

        //获取某控制器的model对象
        public BaseModel GetControllerModel(int controllerKey)
        {
            // if (_modules.ContainsKey(controllerKey))
            // {
            //     return _modules[controllerKey].GetModel();
            // }
            if (_modules.TryGetValue(controllerKey, out var module))
            {
                return module.GetModel();
            }
            return null;
        }
    }
}