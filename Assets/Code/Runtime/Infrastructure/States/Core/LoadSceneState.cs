using Code.Runtime.Services.LogService;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly ILogService _logService;
        private readonly ISceneLoader _sceneLoader;

        public LoadSceneState(ILogService logService, ISceneLoader sceneLoader)
        {
            _logService = logService;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {

            _sceneLoader.Load(sceneName,
                () => { _logService.Log($"Loaded: {sceneName} (Scene)"); });
        }

        public void Exit()
        {
        }
    }
}