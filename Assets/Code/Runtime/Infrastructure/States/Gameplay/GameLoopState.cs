using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Map;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Logic.PlayerSystem.States;
using Code.Runtime.Repositories;
using Code.Runtime.Services.TimerService;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class GameLoopState : IState, IUpdatebleState
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly UserInteractor _userInteractor;
        private readonly IMapGenerator _mapGenerator;
        private readonly ITimerService _timerService;

        private float _scoreTimer;
        private int _timerId;
        private bool _startScoreCount;

        public GameLoopState(PlayerStateMachine playerStateMachine, IInteractorContainer interactorContainer)
        {
            _playerStateMachine = playerStateMachine;
            _userInteractor = interactorContainer.Get<UserInteractor>();
        }

        public void Enter()
        {
            _playerStateMachine.Enter<StartRunState>();
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
            _timerService.StopTimer(_timerId);
        }
    }
}