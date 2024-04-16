using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Logic.Map;
using UnityEngine;
using Zenject;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Chunk _chunkPrefab;
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
            _chunkPlacer.Init(_player, _chunkPrefab, _gameObjectsPoolContainer);
        }

        private void Update()
        {
            _chunkPlacer.Update();
        }
    }
}