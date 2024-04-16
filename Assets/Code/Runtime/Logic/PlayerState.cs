using System;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public abstract class PlayerState : IState, IDisposable
    {
        protected readonly Rigidbody PlayerRigidbody;
        protected readonly PlayerStateMachine PlayerStateMachine;
        private readonly IInputService _inputService;

        protected Transform PlayerTransform => PlayerRigidbody.transform;

        public PlayerState(Rigidbody playerRigidbody, IInputService inputService, PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
            PlayerRigidbody = playerRigidbody;
            _inputService = inputService;

            _inputService.OnJump += OnJump;
            _inputService.OnSliding += OnSliding;
        }

        public void Dispose()
        {
            _inputService.OnJump -= OnJump;
            _inputService.OnSliding -= OnSliding;
        }

        private void OnSliding() =>
            PlayerStateMachine.Enter<SlidingState>();

        private void OnJump() =>
            PlayerStateMachine.Enter<JumpState>();

        public abstract void Exit();
        public abstract void Enter();
    }
}