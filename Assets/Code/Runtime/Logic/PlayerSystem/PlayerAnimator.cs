using System;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        static readonly int DeadHash = Animator.StringToHash ("Dead");
        static readonly int MovingHash = Animator.StringToHash("Moving");
        static readonly int JumpingHash = Animator.StringToHash("Jumping");
        static readonly int SlidingHash = Animator.StringToHash("Sliding");
        static readonly int StartHash = Animator.StringToHash("StartRun");

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
            
            _animator.SetTrigger(StartHash);
        }

        public void OnStartAnimationPlayed() =>
            _onStartAnimationPlayed?.Invoke();
    }
}