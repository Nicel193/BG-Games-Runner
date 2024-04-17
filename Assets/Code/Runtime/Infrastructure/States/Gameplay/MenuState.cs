using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.UI;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class MenuState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IMenuView _menuView;

        public MenuState(GameplayStateMachine gameplayStateMachine, IMenuView menuView)
        {
            _menuView = menuView;
            _gameplayStateMachine = gameplayStateMachine;
        }
        
        public void Enter()
        {
            _menuView.Enable();
            
            _menuView.OnStartGame += OnStartGame;
        }

        public void Exit()
        {
            _menuView.OnStartGame -= OnStartGame;
            
            _menuView.Disable();
        }

        private void OnStartGame() =>
            _gameplayStateMachine.Enter<GameLoopState>();
    }
}