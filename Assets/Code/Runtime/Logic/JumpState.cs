using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class JumpState : PlayerState, IUpdatebleState
    {
        private float _jumpForce;
        private float _groundHeight;

        public JumpState(Rigidbody playerRigidbody, IInputService inputService,
            PlayerStateMachine playerStateMachine, float jumpForce)
            : base(playerRigidbody, inputService, playerStateMachine)
        {
            _jumpForce = jumpForce;
            _groundHeight = PlayerTransform.position.y;
        }

        public override void Enter()
        {
            PlayerRigidbody.isKinematic = false;
            PlayerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        public void Update()
        {
            if (PlayerTransform.position.y < _groundHeight)
                PlayerStateMachine.Enter<RunState>();
        }

        public override void Exit()
        {
            // PlayerRigidbody.velocity = Vector3.zero;
            PlayerRigidbody.isKinematic = true;
            Vector3 transformPosition = PlayerTransform.position;
            transformPosition.y = _groundHeight;
            PlayerTransform.position = transformPosition;
        }
    }
}