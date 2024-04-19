using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem.States;
using Code.Runtime.Services.AdsService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _respawnButton;
        
        private PlayerStateMachine _playerStateMachine;
        private IAdsService _adsService;

        [Inject]
        public void Construct(PlayerStateMachine playerStateMachine, IAdsService adsService)
        {
            _adsService = adsService;
            _playerStateMachine = playerStateMachine;
        }
        
        protected override void Initialize()
        {
            
        }

        protected override void SubscribeUpdates()
        {
            _respawnButton.onClick.AddListener(Respawn);
        }

        protected override void Cleanup()
        {
            _respawnButton.onClick.RemoveListener(Respawn);
        }

        private void Respawn()
        {
            if(!_adsService.IsRewardAdLoaded) return;

            _adsService.ShowRewardedAd(() =>
            {
                _playerStateMachine.Enter<RunStateTmp>();
            });
        }
    }
}