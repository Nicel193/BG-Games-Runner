using System;
using System.Collections;
using System.Collections.Generic;
using Code.Runtime.Infrastructure;
using UnityEngine;

namespace Code.Runtime.Services.TimerService
{
    public class TimerService : ITimerService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private Dictionary<int, Coroutine> _timerCoroutines = new Dictionary<int, Coroutine>();
        private int _nextTimerId = 0;

        public TimerService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public int StartTimer(int seconds, Action onFinish)
        {
            int timerId = NextTimerId();

            Coroutine coroutine = _coroutineRunner.StartCoroutine(Timer(timerId, seconds, onFinish));

            _timerCoroutines.Add(timerId, coroutine);

            return timerId;
        }

        public void StopTimer(int timerId)
        {
            if (_timerCoroutines.TryGetValue(timerId, out Coroutine coroutine))
            {
                _coroutineRunner.StopCoroutine(coroutine);
                _timerCoroutines.Remove(timerId);

                return;
            }

            throw new ArgumentException($"You passed the ID to a non-existent timer: {timerId}");
        }

        private IEnumerator Timer(int timerId, int seconds, Action onFinish)
        {
            yield return new WaitForSeconds(seconds);

            onFinish?.Invoke();
        }

        private int NextTimerId()
        {
            return ++_nextTimerId;
        }
    }
}