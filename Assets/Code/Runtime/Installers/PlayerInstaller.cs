using Code.Runtime.Configs;
using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Logic.PlayerSystem.States;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private FollowPlayerCamera followPlayerCamera;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private Player player;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfig).AsSingle();
            
            BindFollowPlayerCamera();

            BindPlayerStateMachine();

            BindPlayerStatesFactory();

            BindPlayerAnimator();
            
            BindPlayer();
        }

        private void BindFollowPlayerCamera()
        {
            Container.BindInterfacesTo<FollowPlayerCamera>().FromInstance(playerAnimator).AsSingle();
        }

        private void BindPlayerStateMachine()
        {
            Container.Bind<PlayerStateMachine>().AsSingle();
        }

        private void BindPlayerStatesFactory()
        {
            Container.Bind<PlayerStatesFactory>().AsSingle();
        }

        private void BindPlayerAnimator()
        {
            Container.BindInterfacesTo<IPlayerAnimator>().FromInstance(playerAnimator).AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesTo<Player>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}