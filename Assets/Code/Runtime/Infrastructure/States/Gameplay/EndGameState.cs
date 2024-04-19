using Code.Runtime.Repositories;
using Code.Runtime.Services.AuthService;
using Code.Runtime.Services.DatabaseService;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class EndGameState : IState
    {
        private IDatabaseService _databaseService;
        private IAuthService _authService;

        public EndGameState(IDatabaseService databaseService, IAuthService authService)
        {
            _authService = authService;
            _databaseService = databaseService;
        }
        
        public void Enter()
        {
            _databaseService.SaveUserDataAsync(_authService.UserId);
        }

        public void Exit()
        {
            
        }
    }
}