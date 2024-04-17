using Code.Runtime.Configs;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class SlidingState : RunState
    {
        private readonly Vector3 _startColliderSize;

        private float _slidingHeight;
        private float _slideTimer;

        public SlidingState(IReadonlyPlayer player, IInputService inputService,
            IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine, PlayerConfig playerConfig)
            : base(player, inputService, playerAnimator, playerStateMachine)
        {
            _startColliderSize = PlayerBoxCollider.size;
            _slidingHeight = playerConfig.SlidingHeight;
        }


        public override void Enter()
        {
            Vector3 size = PlayerBoxCollider.size;
            PlayerBoxCollider.size = new Vector3(size.x, _slidingHeight, size.z);
            PlayerAnimator.Sliding(true);
        }

        public override void Update()
        {
            base.Update(); 
            
            _slideTimer += Time.deltaTime;

            if (_slideTimer >= 1f)
                PlayerStateMachine.Enter<RunState>();
        }

        public override void Exit()
        {
            _slideTimer = 0f;
            PlayerBoxCollider.size = _startColliderSize;
            PlayerAnimator.Sliding(false);
        }
    }
}