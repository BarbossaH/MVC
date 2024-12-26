using UnityEngine;

namespace Timer
{
    public class GameTimerData
    {
        private float _timer;
        private System.Action _callback;

        public GameTimerData(float timer, System.Action callback)
        {
            _timer = timer;
            _callback = callback;
        }

        public void RestartTimer(float timer, System.Action callback)
        {
            _timer = timer;
            _callback = callback;
        }
        public bool OnUpdate(float deltaTime)
        {
            _timer -= deltaTime;
            // Debug.Log(_timer);
            if (_timer <= 0)
            {
                _callback?.Invoke();
                return true;
            }
            return false;
        }
    }
}