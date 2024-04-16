using Code.Runtime.Infrastructure.ObjectPool;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameObjectsPoolContainer();
        }

        private void BindGameObjectsPoolContainer()
        {
            Container.BindInterfacesTo<GameObjectsPoolContainer>().AsSingle();
        }
    }
}