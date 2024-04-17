using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class DeadState : IState
    {
        private IPlayerAnimator _playerAnimator;

        public DeadState(IPlayerAnimator playerAnimator)
        {
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _playerAnimator.PlayDeath();
        }

        public void Exit()
        {
        }
    }
}