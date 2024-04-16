using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class JumpState : PlayerState
    {
        private float _jumpForce;

        public JumpState(Rigidbody playerRigidbody, IInputService inputService, float jumpForce)
            : base(playerRigidbody, inputService)
        {
            _jumpForce = jumpForce;
        }
        
        public override void Enter()
        {
            PlayerRigidbody.useGravity = true;
            PlayerRigidbody.AddForce(Vector3.up * _jumpForce);
        }

        public override void Exit()
        {
            
        }
    }
}