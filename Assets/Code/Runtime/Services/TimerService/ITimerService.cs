using System;

namespace Code.Runtime.Services.TimerService
{
    public interface ITimerService
    {
        int StartTimer(int seconds, Action onFinish);
        void StopTimer(int timerId);
    }
}