using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapGenerationConfig _mapGenerationConfig;
        [SerializeField] private ChunkPlacer _chunkPlacer;
        [SerializeField] private ObstaclePlacer _chunkObstacle = new ObstaclePlacer();
        [SerializeField] private Transform _player;
        
        private IGameObjectsPoolContainer _gameObjectsPoolContainer;

        [Inject]
        public void Construct(IGameObjectsPoolContainer gameObjectsPoolContainer)
        {
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
        }
        
        private void Start()
        {
            _chunkPlacer.Init(_player, _mapGenerationConfig, _gameObjectsPoolContainer);
            _chunkObstacle.Init(_player, _mapGenerationConfig, _gameObjectsPoolContainer);
        }

        private void Update()
        {
            _chunkPlacer.Update();
            _chunkObstacle.Update();
        }
    }
}