using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Repositories;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadProgressState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInteractorContainer _interactorContainer;

        LoadProgressState(GameStateMachine gameStateMachine, IInteractorContainer interactorContainer,
            ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _interactorContainer = interactorContainer;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            // PlayerProgress playerProgress = new PlayerProgress();

            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Blue].ChipsScore[21] = 12;
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Blue].CellsData[20].Score = 65;
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Blue].CellsData[5].CellType = CellType.Protection;
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Blue].CellsData[13].Score = 1;
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Blue].RemainingChips = 0;
            
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Red].CellsData[10].Score = 12;
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Red].ChipsScore[11] = 12;
            // playerProgress.GameFieldRepository.ChipsData[ChipColor.Red].RemainingChips = 0;

            // InteractorsInitializer.Initialize(playerProgress, _interactorContainer);
            
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Authorization.ToString());
        }

        public void Exit()
        {
        }
    }
}