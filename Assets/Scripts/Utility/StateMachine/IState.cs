namespace Utility.StateMachine
{
    public interface IState
    {
        public bool HasEntered { get; }

        public StateReturn OnUpdate();

        public void OnEnter();

        public void OnExit();

        public void OnUndo();

        public void OnDrawGizmos();
    };
}