using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.WindowsService;
using Code.Runtime.UI.Windows;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class DeadState : IState
    {
        private IPlayerAnimator _playerAnimator;
        private IWindowService _windowService;

        public DeadState(IPlayerAnimator playerAnimator, IWindowService windowService)
        {
            _windowService = windowService;
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _playerAnimator.PlayDeath();
            _windowService.Open(WindowType.Death);
        }

        public void Exit()
        {
        }
    }
}