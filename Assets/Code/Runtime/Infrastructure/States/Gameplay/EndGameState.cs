using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Runtime.Services.AuthService;
using Code.Runtime.Services.DatabaseService;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class EndGameState : IState
    {
        private IDatabaseService _databaseService;
        private IAuthService _authService;
        private UserInteractor _userInteractor;

        public EndGameState(IDatabaseService databaseService, IAuthService authService, IInteractorContainer interactorContainer)
        {
            _authService = authService;
            _databaseService = databaseService;
            _userInteractor = interactorContainer.Get<UserInteractor>();
        }
        
        public void Enter()
        {
            _databaseService.SaveUserDataAsync(_authService.UserId);
            _userInteractor.IncreaseDeathCount();
        }

        public void Exit()
        {
            
        }
    }
}