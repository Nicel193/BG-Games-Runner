using System;
using Code.Runtime.Configs;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerSideMovement
    {
        private readonly IInputService _inputService;
        private readonly Rigidbody _playerRigidbody;
        private readonly float _sideMoveOffset;

        private Transform PlayerTransform => _playerRigidbody.transform;
        private float _playerXPosition;
        private float _changeSideSpeed;

        public PlayerSideMovement(IReadonlyPlayer player, IInputService inputService, PlayerConfig playerConfig)
        {
            _playerRigidbody = player.Rigidbody;
            _inputService = inputService;
            
            _changeSideSpeed = playerConfig.ChangeSideSpeed;
            _sideMoveOffset = playerConfig.SideMoveOffset;
        }

        public void Subscribe()
        {
            _inputService.OnLeftMove += MoveToLeft;
            _inputService.OnRightMove += MoveToRight;
        }

        public void Unsubscribe()
        {
            _inputService.OnLeftMove -= MoveToLeft;
            _inputService.OnRightMove -= MoveToRight;
        }

        public void UpdatePosition()
        {
            PlayerTransform.position =
                Vector3.Lerp(
                    PlayerTransform.position,
                    new Vector3(_playerXPosition, PlayerTransform.position.y, PlayerTransform.position.z), 
                    _changeSideSpeed * Time.deltaTime);
        }

        private void MoveToLeft() =>
            ChangeSide(-_sideMoveOffset);

        private void MoveToRight() =>
            ChangeSide(_sideMoveOffset);

        private void ChangeSide(float offset) =>
            _playerXPosition = Mathf.Clamp(_playerXPosition + offset, -_sideMoveOffset, _sideMoveOffset);
    }
}