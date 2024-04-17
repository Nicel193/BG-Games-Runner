using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class RunStateTmp : GroundState
    {
        public RunStateTmp(IReadonlyPlayer player, IInputService inputService,
            IPlayerAnimator playerAnimator, PlayerStateMachine playerStateMachine)
            : base(player, inputService, playerAnimator, playerStateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            PlayerAnimator.Run(true);
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerAnimator.Run(false);
        }
    }
}