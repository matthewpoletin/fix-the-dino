using System;
using Lifecycle;
using UnityEngine;

namespace Modules.Common
{
    public class Timer : ITick
    {
        private readonly float _startTime;
        private readonly float _endTime;

        public event Action<float> OnTimerTick;
        public event Action OnTimerElapsed;

        private bool _elapsed;

        public Timer(float duration)
        {
            _endTime = Time.time + duration;
        }

        public void Tick(float deltaTime)
        {
            if (_elapsed)
            {
                return;
            }

            var timeLeft = _endTime - Time.time;
            OnTimerTick?.Invoke(timeLeft);

            if (!_elapsed && timeLeft < 0)
            {
                _elapsed = true;
                OnTimerElapsed?.Invoke();
            }
        }
    }
}