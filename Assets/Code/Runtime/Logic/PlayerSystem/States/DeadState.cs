using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Gameplay;
using Code.Runtime.Services.WindowsService;
using Code.Runtime.UI.Windows;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class DeadState : IState
    {
        private IPlayerAnimator _playerAnimator;
        private IWindowService _windowService;
        private GameplayStateMachine _gameplayStateMachine;

        public DeadState(IPlayerAnimator playerAnimator, IWindowService windowService, GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _windowService = windowService;
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _playerAnimator.PlayDeath();
            _gameplayStateMachine.Enter<EndGameState>();
            _windowService.Open(WindowType.Death);
        }

        public void Exit()
        {
        }
    }
}