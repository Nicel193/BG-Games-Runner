using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Core;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Logic.PlayerSystem.States;
using Code.Runtime.Services.AdsService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button respawnButton;
        [SerializeField] private Button restartButton;
        
        private PlayerStateMachine _playerStateMachine;
        private IAdsService _adsService;
        private ISceneLoader _sceneLoader;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(PlayerStateMachine playerStateMachine, IAdsService adsService, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _adsService = adsService;
            _playerStateMachine = playerStateMachine;
        }
        
        protected override void Initialize()
        {
            
        }

        protected override void SubscribeUpdates()
        {
            respawnButton.onClick.AddListener(Respawn);
            restartButton.onClick.AddListener(Restart);
        }

        protected override void Cleanup()
        {
            respawnButton.onClick.RemoveListener(Respawn);
            restartButton.onClick.RemoveListener(Restart);
        }

        private void Respawn()
        {
            if(!_adsService.IsRewardAdLoaded) return;

            _adsService.ShowRewardedAd(() =>
            {
                _playerStateMachine.Enter<RunStateTmp>();
            });
        }

        private void Restart()
        {
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Gameplay.ToString());
        }
    }
}