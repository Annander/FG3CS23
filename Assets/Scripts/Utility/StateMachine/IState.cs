namespace Utility.StateMachine
{
    public interface IState
    {
        public bool HasEntered { get; }

        public void OnEnter();
        
        public StateReturn OnUpdate();

        public void OnExit();

        public void OnDrawGizmos();
    }
}