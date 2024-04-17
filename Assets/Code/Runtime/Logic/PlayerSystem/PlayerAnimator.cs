using System;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        static int DeadHash = Animator.StringToHash ("Dead");
        static int RunHash = Animator.StringToHash("Run");
        static int MovingHash = Animator.StringToHash("Moving");
        static int JumpingHash = Animator.StringToHash("Jumping");
        static int SlidingHash = Animator.StringToHash("Sliding");
        static int StartHash = Animator.StringToHash("Start");

        private Animator _animator;
        private Action _onStartAnimationPlayed;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }

        public void PlayDeath()
        {
            _animator.SetBool(DeadHash, true);
        }

        public void Jump(bool isPlay)
        {
            _animator.SetBool(JumpingHash, isPlay);
        }

        public void Sliding(bool isPlay)
        {
            _animator.SetBool(SlidingHash, isPlay);
        }

        public void Run(bool isPlay)
        {
            _animator.SetBool(MovingHash, isPlay);
        }

        public void PlayStartAnimation(Action onStartAnimationPlayed)
        {
            _onStartAnimationPlayed = onStartAnimationPlayed;
            
            _animator.Play(StartHash);
        }

        public void OnStartAnimationPlayed() =>
            _onStartAnimationPlayed?.Invoke();

        public void StartRun()
        {
            _animator.Play(RunHash);
        }
    }
}