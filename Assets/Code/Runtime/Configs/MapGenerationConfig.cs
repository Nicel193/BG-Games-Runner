using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Configs/MapGenerationConfig")]
    public class MapGenerationConfig : ScriptableObject
    {
        public Chunk[] ChunkPrefabs;
        public float SpawnChunkOffsetX = -10f;
    }
}