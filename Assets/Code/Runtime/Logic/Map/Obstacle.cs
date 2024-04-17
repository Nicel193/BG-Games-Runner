using UnityEngine;

namespace Code.Runtime.Logic.Map
{
    public class Obstacle : MonoBehaviour
    {
        [field: SerializeField] public ObstacleType ObstacleType { get; private set; }
    }
}