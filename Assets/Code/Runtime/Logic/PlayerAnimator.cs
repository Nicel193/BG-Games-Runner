using UnityEngine;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        static int DeadHash = Animator.StringToHash ("Dead");
        static int RunHash = Animator.StringToHash("Run");
        static int MovingHash = Animator.StringToHash("Moving");
        static int JumpingHash = Animator.StringToHash("Jumping");
        static int JumpingSpeedHash = Animator.StringToHash("JumpSpeed");
        static int SlidingHash = Animator.StringToHash("Sliding");

        private Animator _animator;

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

        public void StartRun()
        {
            _animator.Play(RunHash);
        }
    }
}