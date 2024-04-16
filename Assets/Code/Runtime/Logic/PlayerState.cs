using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public abstract class PlayerState : IState
    {
        protected IInputService InputService;
        protected Rigidbody PlayerRigidbody;
        
        protected Transform PlayerTransform => PlayerRigidbody.transform;

        public PlayerState(Rigidbody playerRigidbody, IInputService inputService)
        {
            PlayerRigidbody = playerRigidbody;
            InputService = inputService;
        }

        public abstract void Exit();
        public abstract void Enter();
    }
}