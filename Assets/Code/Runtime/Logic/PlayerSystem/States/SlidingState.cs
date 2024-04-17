using Code.Runtime.Configs;
using Code.Runtime.Services.InputService;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class SlidingState : GroundState
    {
        private readonly Vector3 _startColliderSize;
        private readonly float _slidingHeight;
        private readonly float _slidingTime;
        
        private float _slideTimer;

        public SlidingState(IReadonlyPlayer player, IInputService inputService,
            IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine, PlayerConfig playerConfig)
            : base(player, inputService, playerAnimator, playerStateMachine)
        {
            _startColliderSize = PlayerBoxCollider.size;
            _slidingHeight = playerConfig.SlidingHeight;
            _slidingTime = playerConfig.SlidingTime;
        }


        public override void Enter()
        {
            base.Enter();
            
            Vector3 size = PlayerBoxCollider.size;
            PlayerBoxCollider.size = new Vector3(size.x, _slidingHeight, size.z);
            PlayerAnimator.Sliding(true);
        }

        public override void Update()
        {
            base.Update(); 
            
            _slideTimer += Time.deltaTime;

            if (_slideTimer >= _slidingTime)
                PlayerStateMachine.Enter<RunStateTmp>();
        }

        public override void Exit()
        {
            base.Exit();
            
            _slideTimer = 0f;
            PlayerBoxCollider.size = _startColliderSize;
            PlayerAnimator.Sliding(false);
        }
    }
}