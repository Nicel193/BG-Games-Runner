using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GroupObjectPool<T> : IObjectPool<T> where T : Component
    {
        private readonly IFactory<T> _objectFactory;
        private readonly IGameObjectsPoolContainer _gameObjectsPoolContainer;
        private readonly int _preloadCount;

        private List<T> _pool = new List<T>();
        private Transform _poolContainer;

        public GroupObjectPool(IFactory<T> objectFactory, int preloadCount, IGameObjectsPoolContainer gameObjectsPoolContainer, string poolName)
        {
            _preloadCount = preloadCount;
            _objectFactory = objectFactory;
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
            _poolContainer = gameObjectsPoolContainer.CreatePoolContainer(poolName);
            
            SpawnObjects();
        }
        
        public T Get()
        {
            T item = _pool.Count > 0 ? _pool[Random.Range(0, _pool.Count)] : PreloadAction();

            if(_pool.Contains(item)) _pool.Remove(item);
 
            GetAction(item);

            return item;
        }

        public void Return(T item)
        {
            ReturnAction(item);
            ReturnToPoolContainer(item);
            _pool.Add(item);
        }

        private T PreloadAction()
        {
            T createdObject = _objectFactory.Create();

            ReturnToPoolContainer(createdObject);

            return createdObject;
        }

        private void ReturnToPoolContainer(T createdObject) =>
            _gameObjectsPoolContainer.AddInPoolContainer(createdObject.transform, _poolContainer);

        private void ReturnAction(T @object)
            => @object.gameObject.SetActive(false);

        private void GetAction(T @object)
            => @object.gameObject.SetActive(true);

        private void SpawnObjects()
        {
            for (int i = 0; i < _preloadCount; i++)
                Return(PreloadAction());
        }
    }
}