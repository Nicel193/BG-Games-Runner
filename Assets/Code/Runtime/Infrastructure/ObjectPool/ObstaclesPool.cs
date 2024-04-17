using System.Collections.Generic;
using Code.Runtime.Logic.Map;
using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ObstaclesPool : IObjectPool<Obstacle>
    {
        private const string ObstaclesPoolName = "ObstaclesPool";
        
        private IFactory<Obstacle> _obstaclesFactory;
        
        private readonly int _preloadCount;
        private readonly IGameObjectsPoolContainer _gameObjectsPoolContainer;

        private List<Obstacle> _pool = new List<Obstacle>();
        private Transform _poolContainer;

        public ObstaclesPool(IFactory<Obstacle> obstaclesFactory, int preloadCount, IGameObjectsPoolContainer gameObjectsPoolContainer)
        {
            _preloadCount = preloadCount;
            _obstaclesFactory = obstaclesFactory;
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
            _poolContainer = gameObjectsPoolContainer.CreatePoolContainer(ObstaclesPoolName);
            
            SpawnObjects();
        }
        
        public Obstacle Get()
        {
            Obstacle item = _pool.Count > 0 ? _pool[Random.Range(0, _pool.Count)] : PreloadAction();
 
            GetAction(item);

            return item;
        }

        public void Return(Obstacle item)
        {
            ReturnAction(item);
            ReturnToPoolContainer(item);
            _pool.Add(item);
        }

        private Obstacle PreloadAction()
        {
            Obstacle createdObject = _obstaclesFactory.Create();

            ReturnToPoolContainer(createdObject);

            return createdObject;
        }

        protected void ReturnToPoolContainer(Obstacle createdObject) =>
            _gameObjectsPoolContainer.AddInPoolContainer(createdObject.transform, _poolContainer);

        private void ReturnAction(Obstacle @object)
            => @object.gameObject.SetActive(false);

        private void GetAction(Obstacle @object)
            => @object.gameObject.SetActive(true);

        private void SpawnObjects()
        {
            for (int i = 0; i < _preloadCount; i++)
                Return(PreloadAction());
        }
    }
}