using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class ObstaclePlacer : IObstaclePlacer
    {
        private Dictionary<Chunk, List<Obstacle>> _obstacles = new Dictionary<Chunk, List<Obstacle>>();
        private MapGenerationConfig _mapGenerationConfig;

        public void Init(MapGenerationConfig mapGenerationConfig,
            IGameObjectsPoolContainer poolContainer)
        {
            _mapGenerationConfig = mapGenerationConfig;
        }

        public void SpawnObstacle(Chunk chunk)
        {
            List<float> obstaclePositions = chunk.obstaclePositions;

            _obstacles.Add(chunk, new List<Obstacle>());

            for (int i = 0; i < obstaclePositions.Count; i++)
            {
                if(Random.Range(0, 11) >= 5) return;
                
                Vector3 obstaclePosition = chunk.GetObstaclePosition(i);
                Obstacle newObstacle = CreateObstacle();
                
                // bool allowedObstacle = (chunk.AllowedObstacles & newObstacle.ObstacleType) != 0;

                newObstacle.transform.position = obstaclePosition;

                _obstacles[chunk].Add(newObstacle);
            }
        }

        public void DestroyObstacle(Chunk chunk)
        {
            if (!_obstacles.TryGetValue(chunk, out List<Obstacle> chunkObstacles)) return;
            
            foreach (Obstacle obstacle in chunkObstacles)
                Object.Destroy(obstacle.gameObject);
                
            chunkObstacles.Clear();
            _obstacles.Remove(chunk);
        }

        private Obstacle CreateObstacle() =>
            Object.Instantiate(
                _mapGenerationConfig.ObstaclesPrefabs[Random.Range(0, _mapGenerationConfig.ObstaclesPrefabs.Length)]);
    }
}