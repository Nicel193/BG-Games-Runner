using System.Threading.Tasks;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Repositories;
using Code.Runtime.Services.AuthService;
using Code.Runtime.Services.DatabaseService;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInteractorContainer _interactorContainer;
        private readonly IDatabaseService _databaseService;
        private IAuthService _authService;

        LoadProgressState(GameStateMachine gameStateMachine, IInteractorContainer interactorContainer,
            IDatabaseService databaseService, IAuthService authService)
        {
            _authService = authService;
            _databaseService = databaseService;
            _gameStateMachine = gameStateMachine;
            _interactorContainer = interactorContainer;
        }

        public async void Enter()
        {
            PlayerProgress playerProgress = new PlayerProgress();

            await InitializePlayerProgress(playerProgress);

            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Gameplay.ToString());
        }

        public void Exit()
        {
        }

        private async Task InitializePlayerProgress(PlayerProgress playerProgress)
        {
            playerProgress._userRepository =
                await _databaseService.GetUserDataAsync(_authService.UserId, _authService.UserName);
        }
    }
}