using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.AuthService;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class AuthState : IState
    {
        private readonly IAuthService _authService;
        private readonly GameStateMachine _gameStateMachine;

        public AuthState(IAuthService authService, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _authService = authService;
        }
        
        public void Enter()
        {
            if (_authService.IsUserAuth)
            {
                _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Gameplay.ToString());
            }
            else
            {
                _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Authorization.ToString());
            }
        }

        public void Exit()
        {
            
        }
    }
}