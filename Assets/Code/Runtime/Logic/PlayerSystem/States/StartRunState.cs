using Code.Runtime.Infrastructure.States;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class StartRunState : IState
    {
        private IPlayerAnimator _playerAnimator;
        private PlayerStateMachine _playerStateMachine;

        public StartRunState(IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _playerAnimator.PlayStartAnimation(() => _playerStateMachine.Enter<RunStateTmp>());
        }

        public void Exit()
        {
        }
    }
}