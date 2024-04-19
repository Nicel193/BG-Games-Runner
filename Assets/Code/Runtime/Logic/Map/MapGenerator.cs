using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Map
{
    public class MapGenerator : MonoBehaviour, IMapGenerator
    {
        [SerializeField] private Transform _player;

        private readonly ChunkPlacer _chunkPlacer = new ChunkPlacer();
        private readonly ObstaclePlacer _chunkObstacle = new ObstaclePlacer();

        private IGameObjectsPoolContainer _gameObjectsPoolContainer;
        private MapGenerationConfig _mapGenerationConfig;
        private PlayerConfig _playerConfig;
        private float _startPlayerZPosition;
        private float _distanceToSpawnObstacle;

        [Inject]
        private void Construct(IGameObjectsPoolContainer gameObjectsPoolContainer,
            PlayerConfig playerConfig, MapGenerationConfig mapGenerationConfig)
        {
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
            _mapGenerationConfig = mapGenerationConfig;
            _playerConfig = playerConfig;
        }

        private void Start()
        {
            _chunkObstacle.Init(_mapGenerationConfig, _gameObjectsPoolContainer);
            _chunkPlacer.Init(_player, _mapGenerationConfig, _gameObjectsPoolContainer, _chunkObstacle);

            _startPlayerZPosition = _player.position.z;
            _distanceToSpawnObstacle = _playerConfig.StartMoveSpeed * _mapGenerationConfig.TimeToSpawnObstacles;
        }

        private void Update()
        {
            _chunkPlacer.Update();

            if (_player.position.z - _startPlayerZPosition >= _distanceToSpawnObstacle)
                _chunkObstacle.StartPlaceObstacles();
        }

        public void StartPlaceObstacles()
        {
        }
    }

    // _chunkObstacle.StartPlaceObstacles();
}