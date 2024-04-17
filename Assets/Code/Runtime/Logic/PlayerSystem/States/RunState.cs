using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class RunState : InputState, IUpdatebleState
    {
        [Inject] private PlayerSideMovement _sideMovement;
        [Inject] private PlayerStraightMovement _straightMovement;

        public RunState(IReadonlyPlayer player, IInputService inputService,
            IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine)
            : base(player, inputService, playerAnimator, playerStateMachine) { }

        public override void Enter()
        {
            PlayerAnimator.Run(true);
        }

        public virtual void Update()
        {
            _sideMovement.UpdatePosition();
            _straightMovement.UpdatePosition();
        }

        public override void Exit()
        {
            PlayerAnimator.Run(false);
        }
    }
}