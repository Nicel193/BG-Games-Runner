using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class ChunkPlacer
    {
        private const int InitialChunkCount = 3;
        
        [SerializeField] private float _spawnChunkOffsetX = -10f;
        
        private Transform _player;
        private IGameObjectPool<Chunk> _chunkPull;
        private List<Chunk> _chunks = new List<Chunk>();

        public void Init(Transform player, Chunk chunk, IGameObjectsPoolContainer poolContainer)
        {
            _chunkPull = new ComponentPool<Chunk>(chunk, InitialChunkCount, poolContainer);
            _player = player;

            SpawnFirstChunk();
            
            for (int i = 0; i < InitialChunkCount; i++) SpawnChunk();
        }

        public void Update()
        {
            float distance = Vector3.Distance(_chunks[^1].End.position, _player.position);

            Debug.Log(distance);

            if (distance <= 10f)
            {
                DestroyChunk();
                SpawnChunk();
            }
        }

        private void SpawnFirstChunk()
        {
            Chunk newChunk = _chunkPull.Get();
            newChunk.transform.position = new Vector3(_spawnChunkOffsetX, 0, 0);
            _chunks.Add(newChunk);
        }
        
        private void SpawnChunk()
        {
            Chunk newChunk = _chunkPull.Get();
            newChunk.transform.position = _chunks[^1].End.position - newChunk.Begin.localPosition;
        
            _chunks.Add(newChunk);
        }
        
        private void DestroyChunk()
        {
            if (_chunks.Count > 0)
            {
                _chunkPull.Return(_chunks[0]);
                _chunks.RemoveAt(0);
            }
        }
    }
}