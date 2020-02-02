using System;
using Lifecycle;

namespace Modules.Common
{
    public class Timer : ITick
    {
        private float _timeLeft;

        public event Action<float> OnTimerTick;
        public event Action OnTimerElapsed;

        private bool _elapsed;

        public Timer(float duration)
        {
            _timeLeft = duration;
        }

        public void Tick(float deltaTime)
        {
            if (_elapsed)
            {
                return;
            }

            _timeLeft -= deltaTime;
            OnTimerTick?.Invoke(_timeLeft);

            if (!_elapsed && _timeLeft < 0)
            {
                _elapsed = true;
                OnTimerElapsed?.Invoke();
            }
        }
    }
}