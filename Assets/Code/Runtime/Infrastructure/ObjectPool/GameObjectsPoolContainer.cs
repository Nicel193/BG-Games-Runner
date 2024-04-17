using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GameObjectsPoolContainer : IGameObjectsPoolContainer
    {
        private const string ObjectPoolsName = "ObjectPools";

        private readonly DiContainer _diContainer;
        
        private Transform _objectPools;

        public GameObjectsPoolContainer(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _objectPools = new GameObject(ObjectPoolsName).transform;
        }

        public Transform CreatePoolContainer<T>(T @object) where T : Component
        {
            string poolName = $"{@object.name} ({@object.GetType()})";

            return CreatePoolContainer(poolName);
        }

        public Transform CreatePoolContainer(string poolName)
        {
            Transform poolContainer = new GameObject(poolName).transform;

            poolContainer.SetParent(_objectPools);

            return poolContainer;
        }

        public void AddInPoolContainer<T>(T @object, Transform poolContainer) where T : Component =>
            @object.transform.SetParent(poolContainer);
    }
}