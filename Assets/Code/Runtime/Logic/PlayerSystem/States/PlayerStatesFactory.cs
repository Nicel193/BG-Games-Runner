using Code.Runtime.Infrastructure;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem.States
{
    public class PlayerStatesFactory : StatesFactory
    {
        public PlayerStatesFactory(IInstantiator instantiator) 
            : base(instantiator)
        {
        }
    }
}