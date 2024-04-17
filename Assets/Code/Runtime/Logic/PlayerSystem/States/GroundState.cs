using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class GroundState : InputState, IUpdatebleState
    {
        [Inject] private PlayerSideMovement _sideMovement;
        [Inject] private PlayerStraightMovement _straightMovement;

        public GroundState(IReadonlyPlayer player, IInputService inputService,
            IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine)
            : base(player, inputService, playerAnimator, playerStateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            _sideMovement.Subscribe();
        }
        
        public virtual void Update()
        {
            _sideMovement.UpdatePosition();
            _straightMovement.UpdatePosition();
        }

        public override void Exit()
        {
            base.Exit();
            
            _sideMovement.Unsubscribe();
        }
    }
}