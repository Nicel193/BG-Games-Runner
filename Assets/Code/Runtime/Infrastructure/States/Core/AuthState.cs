using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.FirebaseService;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class AuthState : IState
    {
        private readonly IAuthFirebaseService _authFirebaseService;
        private readonly GameStateMachine _gameStateMachine;

        public AuthState(IAuthFirebaseService authFirebaseService, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _authFirebaseService = authFirebaseService;
        }
        
        public void Enter()
        {
            if (_authFirebaseService.IsUserAuth)
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