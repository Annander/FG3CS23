using Utility.StateMachine;
using Utility.StateMachine.BaseStates;

namespace AI
{
    public class AIBaseState : BaseState
    {
        protected Entity Owner;
        protected StackMachine StackMachine;

        protected AIBaseState(Entity owner, StackMachine stackMachine)
        {
            Owner = owner;
            StackMachine = stackMachine;
        }
    }
}