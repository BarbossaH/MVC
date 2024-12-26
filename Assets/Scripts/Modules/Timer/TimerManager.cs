namespace Timer
{
    public class TimerManager
    {
        private readonly GameTimer _timer = new();

        public void RegisterTimer(float timer, System.Action callback)
        {
            _timer.RegisterTimer(timer, callback);
        }

        public void OnUpdate(float dt)
        {
            _timer.OnUpdate(dt);
        }
    }
}