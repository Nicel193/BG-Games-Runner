using Code.Runtime.Infrastructure.StateMachines;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerStateMachine : StateMachine
    {
        protected override bool CanRepeatState => false;
    }
}