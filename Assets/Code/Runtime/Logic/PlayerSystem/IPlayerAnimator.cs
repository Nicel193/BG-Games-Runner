using System;

namespace Code.Runtime.Logic.PlayerSystem
{
    public interface IPlayerAnimator
    {
        void PlayDeath();
        void Jump(bool isPlay);
        void Sliding(bool isPlay);
        void Run(bool isPlay);
        void StartRun();
        void PlayStartAnimation(Action onStartAnimationPlayed);
    }
}