using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.AuthService;
using Firebase.Firestore;

namespace Code.Runtime.Infrastructure.States.Core
{
   public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAuthService _authService;

        public BootstrapState(ISceneLoader sceneLoader, GameStateMachine gameStateMachine, IAuthService authService)
        {
            _authService = authService;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _authService.Initialize();
            
            _sceneLoader.Load(SceneName.Bootstrap.ToString(), ToLoadProgressState);
        }

        private void ToLoadProgressState() =>
            _gameStateMachine.Enter<AuthState>();

        public void Exit()
        {
        }
    }
}