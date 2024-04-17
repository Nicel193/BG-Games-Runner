using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class RunState : PlayerState
    {
        private PlayerAnimator _playerAnimator;

        public RunState(Rigidbody playerRigidbody, IInputService inputService,
            PlayerStateMachine playerStateMachine, PlayerAnimator playerAnimator) :
            base(playerRigidbody, inputService, playerStateMachine)
        {
            _playerAnimator = playerAnimator;
        }

        public override void Enter()
        {
            base.Enter();
            
            _playerAnimator.Run(true);
        }

        public override void Exit()
        {
            base.Exit();
            
            _playerAnimator.Run(false);
        }
    }
}