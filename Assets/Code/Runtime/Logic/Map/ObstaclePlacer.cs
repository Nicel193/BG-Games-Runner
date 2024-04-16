using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class ObstaclePlacer
    {
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private MapGenerationConfig _mapGenerationConfig;
        private Transform _player;

        public void Init(Transform player, MapGenerationConfig mapGenerationConfig,
            IGameObjectsPoolContainer poolContainer)
        {
            _mapGenerationConfig = mapGenerationConfig;
            _player = player;
            // _ObstaclePull = new ComponentPool<Obstacle>(Obstacle, InitialObstacleCount, poolContainer);

            SpawnFirstObstacle();

            for (int i = 0; i < _mapGenerationConfig.InitialObstacleCount; i++) SpawnObstacle();
        }

        public void Update()
        {
            float distance = Vector3.Distance(_obstacles[^1].transform.position, _player.position);

            if (distance <= 10f)
            {
                // DestroyObstacle();
                SpawnObstacle();
            }
        }

        private void SpawnFirstObstacle()
        {
            Obstacle newObstacle = CreateObstacle();
            
            newObstacle.transform.position = new Vector3(0f, 0f, 10f);
            _obstacles.Add(newObstacle);
        }

        private void SpawnObstacle()
        {
            Obstacle newObstacle = CreateObstacle();
            
            Vector3 randomObstaclePosition = new Vector3(0f, 0f, Random.Range(20f, 30f));
            newObstacle.transform.position = _obstacles[^1].transform.position + randomObstaclePosition;

            _obstacles.Add(newObstacle);
        }

        private void DestroyObstacle()
        {
            if (_obstacles.Count > 0)
            {
                // _ObstaclePull.Return(_obstacles[0]);
                _obstacles.RemoveAt(0);
                Object.Destroy(_obstacles[0].gameObject);
            }
        }

        private Obstacle CreateObstacle() =>
            Object.Instantiate(_mapGenerationConfig.ObstaclesPrefabs[Random.Range(0, _mapGenerationConfig.ObstaclesPrefabs.Length)]);
    }
}