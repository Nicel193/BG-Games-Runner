using System;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using UnityEditor;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public abstract class InputState : IState, IDisposable
    {
        protected readonly PlayerStateMachine PlayerStateMachine;
        protected readonly IPlayerAnimator PlayerAnimator;
        protected BoxCollider PlayerBoxCollider => _player.BoxCollider;
        protected Rigidbody PlayerRigidbody => _player.Rigidbody;

        private readonly IInputService _inputService;
        private readonly IReadonlyPlayer _player;

        protected Transform PlayerTransform => PlayerRigidbody.transform;

        public InputState(IReadonlyPlayer player, IInputService inputService, IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine)
        {
            _player = player;
            _inputService = inputService;
            PlayerAnimator = playerAnimator;
            PlayerStateMachine = playerStateMachine;
            
            _inputService.OnJump += OnJump;
            _inputService.OnSliding += OnSliding;
        }

        // public virtual void Enter()
        // {
        //     _inputService.OnJump += OnJump;
        //     _inputService.OnSliding += OnSliding;
        // }
        //
        // public virtual void Exit()
        // {
        //     _inputService.OnJump -= OnJump;
        //     _inputService.OnSliding -= OnSliding;
        // }
        
        public abstract void Enter();
        public abstract void Exit();

        public void Dispose()
        {
            _inputService.OnJump -= OnJump;
            _inputService.OnSliding -= OnSliding;
        }

        private void OnJump() =>
            PlayerStateMachine.Enter<JumpState>();

        private void OnSliding() =>
            PlayerStateMachine.Enter<SlidingState>();
    }
}