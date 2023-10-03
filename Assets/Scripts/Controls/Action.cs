using UnityEngine;
using Utility.StateMachine;
using Utility.StateMachine.BaseStates;

namespace Controls
{
    public class Action :  ScriptableState
    {
        protected Transform _transform;

        public Transform Transform => _transform;

        public void Initialize(Transform transform) => _transform = transform;
        
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override StateReturn OnUpdate()
        {
            return base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}