using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Map;
using Code.Runtime.Logic.PlayerSystem.States;
using Code.Runtime.Repositories;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class GameLoopState : IState, IUpdatebleState
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly UserInteractor _userInteractor;
        private readonly IMapGenerator _mapGenerator;

        private float scoreTimer;

        public GameLoopState(PlayerStateMachine playerStateMachine, IInteractorContainer interactorContainer, IMapGenerator mapGenerator)
        {
            _mapGenerator = mapGenerator;
            _playerStateMachine = playerStateMachine;
            _userInteractor = interactorContainer.Get<UserInteractor>();
        }

        public void Enter()
        {
            _playerStateMachine.Enter<StartRunState>();
        }

        public void Update()
        {
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= 1)
            {
                _userInteractor.AddCurrentScore(1);

                scoreTimer = 0;
            }
        }

        public void Exit()
        {
        }
    }
}