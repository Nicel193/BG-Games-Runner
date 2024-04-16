using Code;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Logic.Map;
using UnityEngine;
using Zenject;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapGenerationConfig _mapGenerationConfig;
        [SerializeField] private ChunkPlacer _chunkPlacer;
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
        }

        private void Update()
        {
            _chunkPlacer.Update();
        }
    }
}