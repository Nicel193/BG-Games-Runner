using Code.Runtime.Configs;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class JumpState : GroundState
    {
        private float _jumpForce;
        private float _groundHeight;
        
        public JumpState(IReadonlyPlayer player, IInputService inputService, IPlayerAnimator playerAnimator,
            PlayerStateMachine playerStateMachine, PlayerConfig playerConfig) 
            : base(player, inputService, playerAnimator, playerStateMachine)
        {
            _groundHeight = PlayerTransform.position.y;
            _jumpForce = playerConfig.JumpForce;
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerRigidbody.isKinematic = false;
            PlayerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            PlayerAnimator.Jump(true);
        }

        public override void Update()
        {
            base.Update();
            
            if (PlayerTransform.position.y < _groundHeight)
                PlayerStateMachine.Enter<RunStateTmp>();
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerRigidbody.isKinematic = true;
            Vector3 transformPosition = PlayerTransform.position;
            transformPosition.y = _groundHeight;
            PlayerTransform.position = transformPosition;
            PlayerAnimator.Jump(false);
        }
    }
}