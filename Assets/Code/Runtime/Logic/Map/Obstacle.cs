using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class Obstacle : MonoBehaviour
    {
        [field: SerializeField] public ObstacleType ObstacleType { get; private set; }
        [field: SerializeField] public ObstacleSize  ObstacleSize { get; private set; }
    }
}