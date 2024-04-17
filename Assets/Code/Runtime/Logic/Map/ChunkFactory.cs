using Code.Runtime.Configs;
using Code.Runtime.Infrastructure;
using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class ChunkFactory : IFactory<Chunk>
    {
        private Chunk[] _chunkPrefabs;

        public ChunkFactory(MapGenerationConfig mapGenerationConfig)
            => _chunkPrefabs = mapGenerationConfig.ChunkPrefabs;

        public Chunk Create() =>
            Object.Instantiate(_chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)]);
    }
}