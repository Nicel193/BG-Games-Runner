using System;
using System.Collections.Generic;
using Code;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Logic.Map;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Map
{
    [Serializable]
    public class ChunkPlacer
    {
        private Transform _player;

        // private IGameObjectPool<Chunk> _chunkPull;
        private List<Chunk> _chunks = new List<Chunk>();
        private MapGenerationConfig _mapGenerationConfig;

        public void Init(Transform player, MapGenerationConfig mapGenerationConfig,
            IGameObjectsPoolContainer poolContainer)
        {
            _mapGenerationConfig = mapGenerationConfig;
            // _chunkPull = new ComponentPool<Chunk>(chunk, InitialChunkCount, poolContainer);
            _player = player;

            SpawnFirstChunk();

            for (int i = 0; i < _mapGenerationConfig.InitialChunkCount; i++) SpawnChunk();
        }

        public void Update()
        {
            float distance = Vector3.Distance(_chunks[^1].End.position, _player.position);

            if (distance <= _mapGenerationConfig.SpawnDistance)
            {
                DestroyChunk();
                SpawnChunk();
            }
        }

        private void SpawnFirstChunk()
        {
            Chunk newChunk = CreateChunk();
            
            newChunk.transform.position = Vector3.zero;
            _chunks.Add(newChunk);
        }

        private void SpawnChunk()
        {
            Chunk newChunk = CreateChunk();
            newChunk.transform.position = _chunks[^1].End.position - newChunk.Begin.localPosition;

            _chunks.Add(newChunk);
        }

        private void DestroyChunk()
        {
            if (_chunks.Count > 0)
            {
                // _chunkPull.Return(_chunks[0]);
                _chunks.RemoveAt(0);
                Object.Destroy(_chunks[0].gameObject);
            }
        }

        private Chunk CreateChunk() =>
            Object.Instantiate(_mapGenerationConfig.ChunkPrefabs[Random.Range(0, _chunks.Count)]);
    }
}