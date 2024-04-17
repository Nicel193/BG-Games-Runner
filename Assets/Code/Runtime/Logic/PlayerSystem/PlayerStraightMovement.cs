using Code.Runtime.Configs;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerStraightMovement
    {
        private const float ScaleFactor = 0.0001f;
        
        private readonly Rigidbody _playerRigidbody;
        private readonly float _startMoveSpeed;
        private readonly float _moveSpeedScaler;

        private Transform PlayerTransform => _playerRigidbody.transform;
        private float speedScale;

        public PlayerStraightMovement(IReadonlyPlayer player, PlayerConfig playerConfig)
        {
            _playerRigidbody = player.Rigidbody;

            _startMoveSpeed = playerConfig.StartMoveSpeed;
            _moveSpeedScaler = playerConfig.MoveSpeedScaler;
        }
        
        public void UpdatePosition()
        {
            PlayerTransform.Translate(Vector3.forward * ((_startMoveSpeed + speedScale) * Time.deltaTime));

            speedScale += _moveSpeedScaler * ScaleFactor;
        }
    }
}