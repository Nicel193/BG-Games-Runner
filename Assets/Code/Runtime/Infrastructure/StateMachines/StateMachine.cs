using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Infrastructure.StateMachines
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _activeState;

        protected virtual bool CanRepeatState => true;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state?.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : IExitableState => 
            _states.Add(typeof(TState), state);

        public void UpdateState()
        {
            if (_activeState != null && _activeState is IUpdatebleState updatebleState)
                updatebleState.Update();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            TState state = GetState<TState>();

            if (!CanRepeatState && state == _activeState) return null;
            
            _activeState?.Exit();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}