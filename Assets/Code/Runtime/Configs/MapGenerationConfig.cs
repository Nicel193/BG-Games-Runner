using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Configs/MapGenerationConfig")]
    public class MapGenerationConfig : ScriptableObject
    {
        public Chunk[] ChunkPrefabs;
        public int InitialChunkCount = 10;
        public int SpawnDistance = 40;
    }
}