using System;

namespace CptLost.StateMachine
{
    public class FuncPredicate : IPredicate
    {
        private Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }

        public bool Evaluate()
        {
            return _func.Invoke();
        }
    }

}
