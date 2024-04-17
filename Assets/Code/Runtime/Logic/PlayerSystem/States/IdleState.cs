using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class IdleState : IState
    {
        private IPlayerAnimator _playerAnimator;

        IdleState(IPlayerAnimator playerAnimator)
        {
            _playerAnimator = playerAnimator;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}