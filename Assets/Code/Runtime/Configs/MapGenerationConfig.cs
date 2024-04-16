using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Configs/MapGenerationConfig")]
    public class MapGenerationConfig : ScriptableObject
    {
        public Chunk[] ChunkPrefabs;
        public float SpawnChunkOffsetX = -10f;
    }
}