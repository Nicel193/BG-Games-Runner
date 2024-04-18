using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.FirebaseService;

namespace Code.Runtime.Infrastructure.States.Core
{
   public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAuthFirebaseService _authFirebaseService;

        public BootstrapState(ISceneLoader sceneLoader, GameStateMachine gameStateMachine, IAuthFirebaseService authFirebaseService)
        {
            _authFirebaseService = authFirebaseService;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            _authFirebaseService.Initialize();
            
            _sceneLoader.Load(SceneName.Bootstrap.ToString(), ToLoadProgressState);
        }

        private void ToLoadProgressState() =>
            _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}