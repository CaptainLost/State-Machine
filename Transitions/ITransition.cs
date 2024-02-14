namespace CptLost.StateMachine
{
    public interface ITransition
    {
        State TargetState { get; }
        IPredicate Condition { get; }
    }
}
