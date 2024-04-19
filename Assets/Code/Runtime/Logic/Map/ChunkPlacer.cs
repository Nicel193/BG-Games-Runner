using System;
using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Code.Runtime.Logic.Map
{
    public class ChunkPlacer
    {
        private const string ChunksPoolName = "ChunksPool";

        private Transform _player;

        private List<Chunk> _chunks = new List<Chunk>();
        private IObstaclePlacer _obstaclePlacer;
        private IObjectPool<Chunk> _chunksPool;
        private int _spawnDistance;
        private int _initialChunkPoolCount;
        private int _initialChunkCount;

        public void Init(Transform player, MapGenerationConfig mapGenerationConfig,
            IGameObjectsPoolContainer poolContainer, IObstaclePlacer obstaclePlacer)
        {
            _obstaclePlacer = obstaclePlacer;
            _player = player;

            _spawnDistance = mapGenerationConfig.SpawnDistance;
            _initialChunkPoolCount = mapGenerationConfig.InitialChunkPoolCount;
            _initialChunkCount = mapGenerationConfig.InitialChunkCount;

            InitializeChunkPool(mapGenerationConfig, poolContainer);
            SpawnInitalChunks();
        }

        private void SpawnInitalChunks()
        {
            SpawnFirstChunk();
            
            for (int i = 0; i < _initialChunkCount; i++)
                SpawnChunk();
        }

        public void Update()
        {
            float distance = Vector3.Distance(_chunks[^1].End.position, _player.position);

            if (distance <= _spawnDistance)
            {
                DestroyChunk();
                SpawnChunk();
            }
        }

        private void SpawnFirstChunk()
        {
            Chunk newChunk = _chunksPool.Get();

            newChunk.transform.position = Vector3.zero;
            _chunks.Add(newChunk);
        }

        private void SpawnChunk()
        {
            Chunk newChunk = _chunksPool.Get();
            newChunk.transform.position = _chunks[^1].End.position - newChunk.Begin.localPosition;

            _chunks.Add(newChunk);
            _obstaclePlacer.SpawnObstacle(newChunk);
        }

        private void DestroyChunk()
        {
            if (_chunks.Count > 0)
            {
                _chunks.RemoveAt(0);
                _chunksPool.Return(_chunks[0]);
                _obstaclePlacer.DestroyObstacle(_chunks[0]);
            }
        }

        private void InitializeChunkPool(MapGenerationConfig mapGenerationConfig,
            IGameObjectsPoolContainer poolContainer)
        {
            ChunkFactory chunkFactory = new ChunkFactory(mapGenerationConfig);

            _chunksPool = new GroupObjectPool<Chunk>(chunkFactory,
                _initialChunkPoolCount, poolContainer, ChunksPoolName);
        }
    }
}