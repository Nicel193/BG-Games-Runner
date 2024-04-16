using Code.Runtime.Logic.Map;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ChunkPool : ComponentPool<Chunk>
    {
        public ChunkPool(Chunk @object, 
            int preloadCount, 
            IGameObjectsPoolContainer gameObjectsPoolContainer) : base(@object, preloadCount, gameObjectsPoolContainer)
        {
            
        }
    }
}