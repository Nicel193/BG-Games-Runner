using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.Bootstrappers;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Interactors;
using Code.Runtime.Services.AdsService;
using Code.Runtime.Services.AuthService;
using Code.Runtime.Services.DatabaseService;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.LogService;
using Code.Runtime.Services.TimerService;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameBootstrapperFactory();
            
            BindGameStateMachine();
            
            BindCoroutineRunner();
            
            BindStatesFactory();
            
            BindSceneLoader();

            BindLogService();

            BindInteractorContainer();

            BindInputService();

            BindFirebaseService();

            BindFirestoreDatabaseService();

            BindAdsService();

            BindTimerService();
        }

        private void BindTimerService()
        {
            Container.BindInterfacesTo<TimerService>().AsSingle();
        }

        private void BindAdsService()
        {
            Container.BindInterfacesTo<AdMobAdsService>().AsSingle();
        }

        private void BindFirestoreDatabaseService()
        {
            Container.BindInterfacesTo<FirestoreDatabaseService>().AsSingle();
        }

        private void BindFirebaseService()
        {
            Container.BindInterfacesTo<AuthFirebaseService>().AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesTo<MobileInputService>().AsSingle();
        }

        private void BindInteractorContainer()
        {
            Container.BindInterfacesTo<InteractorContainer>().AsSingle();
        }

        private void BindLogService()
        {
            Container.BindInterfacesTo<LogService>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }
        
        private void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }
        
        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();
        }
    
        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }
        
        private void BindGameBootstrapperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapperPath);
        }
    }
}