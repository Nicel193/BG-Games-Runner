using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem.States;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class GameLoopState : IState
    {
        private PlayerStateMachine _playerStateMachine;

        public GameLoopState(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }
        
        public void Enter()
        {
            _playerStateMachine.Enter<StartRunState>();
        }

        public void Exit()
        {
            
        }
    }
}