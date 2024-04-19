using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class ObstaclePlacer : IObstaclePlacer
    {
        private const string ObstaclesPoolName = "ObstaclesPool";

        private Dictionary<Chunk, List<Obstacle>> _obstacles = new Dictionary<Chunk, List<Obstacle>>();
        private IObjectPool<Obstacle> _obstaclesPool;

        private bool _isCanPlaceObstacles;

        public void Init(MapGenerationConfig mapGenerationConfig, IGameObjectsPoolContainer poolContainer)
        {
            ObstacleFactory obstacleFactory = new ObstacleFactory(mapGenerationConfig);

            _obstaclesPool = new GroupObjectPool<Obstacle>(obstacleFactory, mapGenerationConfig.InitialObstacleCount,
                poolContainer, ObstaclesPoolName);
        }

        public void StartPlaceObstacles() =>
            _isCanPlaceObstacles = true;

        public void SpawnObstacle(Chunk chunk)
        {
            if(!_isCanPlaceObstacles) return;

            List<float> obstaclePositions = chunk.obstaclePositions;

            _obstacles.Add(chunk, new List<Obstacle>());

            for (int i = 0; i < obstaclePositions.Count; i++)
            {
                if (Random.Range(0, 11) >= 5) return;

                Vector3 obstaclePosition = chunk.GetObstaclePosition(i);
                Obstacle newObstacle = _obstaclesPool.Get();

                // bool allowedObstacle = (chunk.AllowedObstacles & newObstacle.ObstacleType) != 0;

                obstaclePosition = ObstacleInCells(newObstacle, obstaclePosition);

                newObstacle.transform.position = obstaclePosition;

                _obstacles[chunk].Add(newObstacle);
            }
        }

        private static Vector3 ObstacleInCells(Obstacle obstacle, Vector3 obstaclePosition)
        {
            if (obstacle.ObstacleSize != ObstacleSize.OneCell) return obstaclePosition;

            int cellIndex = Random.Range(0, 3);
            float positionOffset = 0;

            switch (cellIndex)
            {
                case 1:
                    positionOffset = -1.2f;
                    break;
                case 2:
                    positionOffset = 1.2f;
                    break;
            }

            return obstaclePosition + new Vector3(positionOffset, 0f, 0f);
        }

        public void DestroyObstacle(Chunk chunk)
        {
            if (!_obstacles.TryGetValue(chunk, out List<Obstacle> chunkObstacles)) return;

            foreach (Obstacle obstacle in chunkObstacles)
                _obstaclesPool.Return(obstacle);

            chunkObstacles.Clear();
            _obstacles.Remove(chunk);
        }
    }
}