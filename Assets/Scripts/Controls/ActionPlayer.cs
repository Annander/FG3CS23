using UnityEngine;
using Utility.StateMachine;

namespace Controls
{
    public class ActionPlayer : MonoBehaviour
    {
        private enum ActionType
        {
            Default = 0
        }
        
        [SerializeField] private Action[] actions;

        private StackMachine actionStateMachine;

        private void Awake()
        {
            foreach (var action in actions)
            {
                action.Initialize(transform);
            }
            
            actionStateMachine = new StackMachine();
            actionStateMachine.PushState(actions[(int)ActionType.Default]);
        }

        private void Update()
        {
            actionStateMachine.Update();
        }
    }
}