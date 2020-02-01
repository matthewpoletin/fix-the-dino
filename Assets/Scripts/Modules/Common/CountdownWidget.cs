using Lifecycle;
using TMPro;
using UnityEngine;

namespace Modules.Common
{
    public class CountdownWidget : BaseView
    {
        [SerializeField] private TextMeshProUGUI _timeLeftText = null;

        private Timer _timer;

        public void SetTimer(Timer timer)
        {
            _timer = timer;

            gameObject.SetActive(true);

            _timer.OnTimerTick += OnTimerTick;
            _timer.OnTimerElapsed += OnTimerElapsed;
        }

        private void OnTimerTick(float secondsLeft)
        {
            _timeLeftText.text = ((int) secondsLeft).ToString();
        }

        private void OnTimerElapsed()
        {
            gameObject.SetActive(false);

            _timer.OnTimerTick -= OnTimerTick;
            _timer.OnTimerElapsed -= OnTimerElapsed;
        }
    }
}