using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Camera")]
        public float SmoothSpeed = 5f;
        public float Height = 10f;
        public float ZOffset = -2.5f;
    }
}