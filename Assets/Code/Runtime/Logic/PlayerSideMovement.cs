using System;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class PlayerSideMovement : IDisposable
    {
        private readonly IInputService InputService;
        private readonly Rigidbody PlayerRigidbody;
        private readonly float _sideMoveOffset;

        private Transform PlayerTransform => PlayerRigidbody.transform;
        private float _playerXPosition;
        private float _changeSideSpeed;

        public PlayerSideMovement(Rigidbody playerRigidbody, IInputService inputService,
            float sideMoveOffset, float changeSideSpeed)
        {
            _changeSideSpeed = changeSideSpeed;
            PlayerRigidbody = playerRigidbody;
            InputService = inputService;
            _sideMoveOffset = sideMoveOffset;

            InputService.OnLeftMove += MoveToLeft;
            InputService.OnRightMove += MoveToRight;
        }

        public void Dispose()
        {
            InputService.OnLeftMove -= MoveToLeft;
            InputService.OnRightMove -= MoveToRight;
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