using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class SlidingState : PlayerState, IUpdatebleState
    {
        private BoxCollider _playerCollider;
        private Vector3 _startColliderSize;
        private float _slidingHeight;
        private float _slideTimer;
        private PlayerAnimator _playerAnimator;

        public SlidingState(Rigidbody playerRigidbody, IInputService inputService,
            PlayerStateMachine playerStateMachine, BoxCollider playerCollider, float slidingHeight, PlayerAnimator playerAnimator) : base(playerRigidbody, inputService, playerStateMachine)
        {
            _playerAnimator = playerAnimator;
            _playerCollider = playerCollider;
            _slidingHeight = slidingHeight;
            _startColliderSize = playerCollider.size;
        }

        public override void Enter()
        {
            _playerCollider.size = new Vector3(_playerCollider.size.x, _slidingHeight, _playerCollider.size.z);
            _playerAnimator.Sliding(true);
        }

        public override void Exit()
        {
            _slideTimer = 0f;
            _playerCollider.size = _startColliderSize;
            _playerAnimator.Sliding(false);
        }

        public void Update()
        {
            _slideTimer += Time.deltaTime;

            if (_slideTimer >= 1f)
                PlayerStateMachine.Enter<RunState>();
        }
    }
}