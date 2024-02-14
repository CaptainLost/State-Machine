namespace CptLost.StateMachine
{
    public class Transition : ITransition
    {
        public State TargetState { get; }
        public IPredicate Condition { get; }

        public Transition(State targetState, IPredicate condition)
        {
            TargetState = targetState;
            Condition = condition;
        }
    }
}
