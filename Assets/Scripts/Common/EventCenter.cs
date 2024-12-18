using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// event center script
    /// </summary>
    public class EventCenter
    {
        //the dictionary that stores ordinary event 
        private readonly Dictionary<string, System.Action<object>> _events = new Dictionary<string, System.Action<object>>();
        //storing temporary event, and then removing it
        private readonly Dictionary<string, System.Action<object>> _tempEvents = new Dictionary<string, System.Action<object>>();
        //存储特定对象的消息字典
        private readonly Dictionary<System.Object, Dictionary<string, System.Action<object>>> _objectEvents = new Dictionary<System.Object, Dictionary<string, System.Action<object>>>();
        
        public void AddEvent(string eventName, System.Action<object> callback)
        {
            if (!_events.TryAdd(eventName, callback))
            {
                _events[eventName] += callback;
            }
            
            // if (_events.ContainsKey(eventName))
            // {
            //     _events[eventName] += callback;
            // }
            // else
            // {
            //     _events.Add(eventName, callback);
            // }
        }

        public void RemoveEnvent(string eventName, System.Action<object> callback)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName] -= callback;
            }

            if (_events[eventName] == null)
            {
                _events.Remove(eventName);
            }
        }

        //execute event, send events
        public void PostEvent(string eventName, object arg = null)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName].Invoke(arg);
            }
        }

        public void AddEvent(System.Object listener, string eventName, System.Action<object> callback)
        {
            if (_objectEvents.ContainsKey(listener))
            {
                if (_objectEvents[listener].ContainsKey(eventName))
                {
                    _objectEvents[listener][eventName] += callback;
                }
                else
                {
                    _objectEvents[listener].Add(eventName, callback);
                }
            }
            else
            {
                var temp = new Dictionary<string, System.Action<object>> { { eventName, callback } };
                // var temp = new Dictionary<string, System.Action<object>>();
                // temp.Add(eventName, callback);
                _objectEvents.Add(listener, temp);
            }
        }

        public void RemoveEvent(System.Object listenerObj, string eventName, System.Action<object> callback)
        {
            if (_objectEvents.ContainsKey(listenerObj))
            {
                if (_objectEvents[listenerObj].ContainsKey(eventName))
                {
                    _objectEvents[listenerObj][eventName] -= callback;
                    if (_objectEvents[listenerObj][eventName] == null)
                    {
                        _objectEvents[listenerObj].Remove(eventName);
                        if (_objectEvents[listenerObj].Count == 0)
                        {
                            _objectEvents.Remove(listenerObj);
                        }
                    }
                }
            }
        }

        public void RemoveObjAllEvent(System.Object listenerObj)
        {
            if (_objectEvents.ContainsKey(listenerObj))
            {
                // _objectEvents[listenerObj].Clear(); //this just clear the value, but it remains the key
                _objectEvents.Remove(listenerObj);
            }
        }

        public void PostEvent(System.Object listenerObj, string eventName, System.Object arg = null)
        {
            if (_objectEvents.ContainsKey(listenerObj))
            {
                if (_objectEvents[listenerObj].ContainsKey(eventName))
                {
                    _objectEvents[listenerObj][eventName].Invoke(arg);
                }
            }
        }

        public void AddTempEvent(string eventName, System.Action<object> callback)
        {
            if (!_tempEvents.TryAdd(eventName, callback))
            {
                _tempEvents[eventName] += callback;
            }
        }

        public void PostTempEvent(string eventName, System.Object arg=null)
        {
            if (_tempEvents.ContainsKey(eventName))
            {
                _tempEvents[eventName].Invoke(arg);
                // _tempEvents[eventName] = null;
                _tempEvents.Remove(eventName);
            }
        }
    }
    
}