using System.Collections.Generic;
using UnityEngine;

namespace Timer
{
    public class GameTimer
    {
        private readonly List<GameTimerData> _timers = new List<GameTimerData>();
        // private readonly Queue<GameTimerData> _queue = new Queue<GameTimerData>();
        public int TimerCount => _timers.Count;

        /// <summary>
        /// register timer
        /// </summary>
        /// <param name="timer">this is the lifecycle of this timer</param>
        /// <param name="callback">when lifecycle is over, this function should be called</param>
        public void RegisterTimer(float timer, System.Action callback)
        {
            GameTimerData timerData=new GameTimerData(timer, callback);;
            // if (_queue.Count == 0)
            // {
            //     timerData=new GameTimerData(timer, callback);
            // }
            // else
            // {
            //     timerData = _queue.Dequeue();
            //     timerData.RestartTimer(timer,callback);
            // }
            _timers.Add(timerData);
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < TimerCount; i++)
            {
                if (_timers[i].OnUpdate(deltaTime))
                {
                    // _queue.Enqueue(_timers[i]);
                    _timers.RemoveAt(i);
                }
            }
        }

        public void Break()
        {
            // foreach (var timer in _timers)
            // {
            //     _queue.Enqueue(timer); 
            // }
            _timers.Clear();
        }
        
    }
}