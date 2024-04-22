using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Runtime.Services.LogService;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly ILogService _logService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IInteractorContainer _interactorContainer;

        public LoadSceneState(ILogService logService, ISceneLoader sceneLoader, IInteractorContainer interactorContainer)
        {
            _interactorContainer = interactorContainer;
            _logService = logService;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            if(_interactorContainer.TryGet(out UserInteractor userInteractor))
                userInteractor.Clear();

            _sceneLoader.Load(sceneName,
                () => { _logService.Log($"Loaded: {sceneName} (Scene)"); });
        }

        public void Exit()
        {
        }
    }
}