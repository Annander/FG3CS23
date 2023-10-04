using System;
using UnityEngine;
using Utility.StateMachine;

namespace Controls
{
    public class ActionPlayer : Player
    {
        private enum ActionType
        {
            Default = 0
        }
        
        [SerializeField] private Action[] actions;

        private StackMachine _actionStateMachine;

        private void Start()
        {
            foreach (var action in actions)
            {
                action.Initialize(Root);
            }
            
            _actionStateMachine = new StackMachine();
            _actionStateMachine.PushState(actions[(int)ActionType.Default]);
        }

        private void Update()
        {
            _actionStateMachine.Update();
        }
    }
}