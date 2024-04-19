namespace Code.Runtime.Logic.Map
{
    public interface IObstaclePlacer
    {
        void SpawnObstacle(Chunk chunk);
        void DestroyObstacle(Chunk chunk);
        void StartPlaceObstacles();
    }
}