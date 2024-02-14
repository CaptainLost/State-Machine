using System.Collections.Generic;
using UnityEngine;

namespace CptLost.StateMachine
{
    public class StateMachine
    {
        public State ActiveState { get { return _currentState; } }

        private State _currentState;
        private HashSet<State> _registeredStates = new HashSet<State>();
        private HashSet<ITransition> _anyTransitionHashSet = new HashSet<ITransition>();

        public StateMachine(State initialState)
        {
            RegisterState(initialState);
            ChangeState(initialState);
        }

        public StateMachine(State initialState, params State[] states)
            : this(initialState)
        {
            foreach (State state in states)
            {
                RegisterState(state);
            }
        }

        public State RegisterState(State state)
        {
            _registeredStates.Add(state);

            return state;
        }

        public void ChangeState(State state)
        {
            if (!_registeredStates.Contains(state))
            {
                Debug.Log("State Machine: Failed to set state, used state is not registered");

                return;
            }

            _currentState?.OnExit();
            _currentState = state;
            _currentState?.OnEnter();
        }

        public void AddAnyTransition(State targetState, IPredicate predicate)
        {
            _anyTransitionHashSet.Add(new Transition(targetState, predicate));
        }

        public void OnUpdate()
        {
            TryToChangeState();

            _currentState?.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            _currentState?.OnFixedUpdate();
        }

        private void TryToChangeState()
        {
            ITransition nextTransition = GetNextTransition();

            if (nextTransition != null)
            {
                ChangeState(nextTransition.TargetState);
            }
        }

        private ITransition GetNextTransition()
        {
            foreach (ITransition transitionAny in _anyTransitionHashSet)
            {
                if (transitionAny.Condition.Evaluate())
                {
                    return transitionAny;
                }
            }

            if (_currentState == null)
                return null;

            foreach (ITransition transition in _currentState.TransitionsHashSet)
            {
                if (transition.TargetState == _currentState)
                    continue;

                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            return null;
        }
    }
}