using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class RunState : PlayerState
    {
        public RunState(Rigidbody playerRigidbody, IInputService inputService, PlayerStateMachine playerStateMachine) :
            base(playerRigidbody, inputService, playerStateMachine) { }

        public override void Exit()
        {
        }

        public override void Enter()
        {
        }
    }
}