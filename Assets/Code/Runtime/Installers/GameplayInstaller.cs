using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Map;
using Code.Runtime.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private MenuView menuView;
        [SerializeField] private HUDView hudView;
        [SerializeField] private MapGenerator mapGenerator;
        
        public override void InstallBindings()
        {
            BindGameObjectsPoolContainer();

            BindGameplayBootstrapper();

            BindStatesFactory();
            
            BindPlayerStateMachine();

            BindMenuView();

            BindMapGenerator();
        }

        private void BindMapGenerator()
        {
            Container.BindInterfacesTo<MapGenerator>()
                .FromInstance(mapGenerator)
                .AsSingle();
        }

        private void BindMenuView()
        {
            Container.Bind<IMenuView>()
                .FromInstance(menuView)
                .AsSingle();
        }

        private void BindGameObjectsPoolContainer()
        {
            Container.BindInterfacesTo<GameObjectsPoolContainer>().AsSingle();
        }
        
        private void BindGameplayBootstrapper()
        {
            Container.Bind<GameplayStateMachine>().AsSingle();
        }
        
        private void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }
        
        private void BindPlayerStateMachine()
        {
            Container.Bind<PlayerStateMachine>().AsSingle();
        }
    }
}