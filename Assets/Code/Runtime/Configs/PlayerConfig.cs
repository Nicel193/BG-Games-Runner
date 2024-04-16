using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Move")]
        public float StartMoveSpeed = 5f;
        public float MoveSpeedScaler = 2f;
        public float SideMoveOffset = 1.7f;
        public float ChangeSideSpeed = 5f;
        
        [Header("Camera")]
        public float SmoothSpeed = 5f;
        public float Height = 10f;
        public float ZOffset = -2.5f;
    }
}