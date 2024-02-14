using System;
using System.Collections.Generic;
using UnityEngine;

namespace CptLost.StateMachine
{
    public class State
    {
        public HashSet<ITransition> TransitionsHashSet { get; private set; } = new HashSet<ITransition>();

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }

        public State AddTransition(State targetState, IPredicate condition)
        {
            if (targetState == this)
            {
                Debug.LogError("State Machine: Failed to add state transition, state can't transit to self");

                return this;
            }

            TransitionsHashSet.Add(new Transition(targetState, condition));

            return this;
        }

        public State AddTransition(State targetState, Func<bool> condition)
        {
            AddTransition(targetState, new FuncPredicate(condition));

            return this;
        }
    }
}
