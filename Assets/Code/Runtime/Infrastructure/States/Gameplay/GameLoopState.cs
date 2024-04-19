using Code.Runtime.Interactors;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Logic.PlayerSystem.States;
using Code.Runtime.Repositories;
using Code.Runtime.UI;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class GameLoopState : IState, IUpdatebleState
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly UserInteractor _userInteractor;
        private readonly IHudView _hudView;

        private float _scoreTimer;
        private bool _isFirstEntry = true;

        public GameLoopState(PlayerStateMachine playerStateMachine, IInteractorContainer interactorContainer, IHudView hudView)
        {
            _hudView = hudView;
            _playerStateMachine = playerStateMachine;
            _userInteractor = interactorContainer.Get<UserInteractor>();
        }

        public void Enter()
        {
            if (_isFirstEntry) _playerStateMachine.Enter<StartRunState>();
            
            _hudView.Enable();

            _isFirstEntry = false;
        }

        public void Update()
        {
            _scoreTimer += Time.deltaTime;

            if (_scoreTimer >= 1)
            {
                _userInteractor.AddCurrentScore(1);

                _scoreTimer = 0;
            }
        }

        public void Exit()
        {
            _hudView.Disable();
        }
    }
}