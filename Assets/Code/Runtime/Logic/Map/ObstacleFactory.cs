using Code.Runtime.Configs;
using Code.Runtime.Infrastructure;
using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class ObstacleFactory : IFactory<Obstacle>
    {
        private Obstacle[] _obstaclesPrefabs;
        
        public ObstacleFactory(MapGenerationConfig mapGenerationConfig) =>
            _obstaclesPrefabs = mapGenerationConfig.ObstaclesPrefabs;

        public Obstacle Create() =>
            Object.Instantiate(_obstaclesPrefabs[Random.Range(0, _obstaclesPrefabs.Length)]);
    }
}