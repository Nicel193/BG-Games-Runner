using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Map
{
    public class MapGenerator : MonoBehaviour, IMapGenerator
    {
        [SerializeField] private MapGenerationConfig _mapGenerationConfig;
        [SerializeField] private Transform _player;

        private readonly ChunkPlacer _chunkPlacer = new ChunkPlacer();
        private readonly ObstaclePlacer _chunkObstacle = new ObstaclePlacer();

        private IGameObjectsPoolContainer _gameObjectsPoolContainer;

        [Inject]
        private void Construct(IGameObjectsPoolContainer gameObjectsPoolContainer)
        {
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
        }
        
        private void Start()
        {
            _chunkObstacle.Init(_mapGenerationConfig, _gameObjectsPoolContainer);
            _chunkPlacer.Init(_player, _mapGenerationConfig, _gameObjectsPoolContainer, _chunkObstacle);
        }

        private void Update()
        {
            _chunkPlacer.Update();
        }

        public void StartPlaceObstacles() =>
            _chunkObstacle.StartPlaceObstacles();
    }
}