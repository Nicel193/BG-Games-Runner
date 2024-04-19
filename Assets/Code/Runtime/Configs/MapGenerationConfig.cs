using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Configs/MapGenerationConfig")]
    public class MapGenerationConfig : ScriptableObject
    {
        public Chunk[] ChunkPrefabs;
        public Obstacle[] ObstaclesPrefabs;
        public int InitialChunkCount = 6;
        public int InitialChunkPoolCount = 30;
        public int InitialObstacleCount = 5;
        public int SpawnDistance = 50;
        public float TimeToSpawnObstacles = 4f;
    }
}