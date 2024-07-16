using System;
using UnityEngine;

namespace Milhouzer.Common.Utility
{
    public class Timer
    {
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public bool IsRunning { get; private set; }
        private Action callback;

        public float Remaining { get; private set; }

        public Timer(float duration, Action callback)
        {
            this.Duration = duration;
            this.callback = callback;
        }

        public void AddDuration(float amount)
        {
            Duration += amount;
        }

        public void Start()
        {
            if (IsRunning)
            {
                Debug.LogWarning("Timer is already running.");
                return;
            }

            Remaining = Duration;
            IsRunning = true;
        }

        public void Update()
        {
            if (!IsRunning)
            {
                return;
            }

            Remaining -= Time.deltaTime;

            if (Remaining <= 0)
            {
                Finish();
            }
        }

        private void Finish()
        {
            IsRunning = false;
            callback?.Invoke();
        }
    }
}