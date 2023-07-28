using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiniteStateMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState">Base state</typeparam>
    public class StateMachine<TState> where TState : IState
    {
        protected TState _currentState;
        
        private Dictionary<Type, TState> _states = new();

        protected void InitializeStates()
        {
            var baseType = typeof(TState);
            var types = baseType.Assembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(baseType));

            foreach (var type in types)
            {
                var state = (TState)Activator.CreateInstance(type);
                ConfigureState(state);
                _states.Add(type, state);
            }
        }

        public void ChangeState<T1State>() where T1State : TState
        {
            var stateType = typeof(T1State);
            if (_states.TryGetValue(stateType, out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }
        
        protected virtual void ConfigureState(TState state) { }
    }
}