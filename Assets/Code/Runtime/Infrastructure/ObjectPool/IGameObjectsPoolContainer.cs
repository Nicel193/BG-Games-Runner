using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public interface IGameObjectsPoolContainer
    {
        Transform CreatePoolContainer<T>(T @object) where T : Component;
        Transform CreatePoolContainer(string poolName);
        void AddInPoolContainer<T>(T @object, Transform poolContainer) where T : Component;
    }
}