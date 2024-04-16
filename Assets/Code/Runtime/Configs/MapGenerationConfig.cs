using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Configs/MapGenerationConfig")]
    public class MapGenerationConfig : ScriptableObject
    {
        public Chunk[] ChunkPrefabs;
        public Obstacle[] ObstaclesPrefabs;
        public int InitialChunkCount = 10;
        public int InitialObstacleCount = 5;
        public int SpawnDistance = 40;
    }
}