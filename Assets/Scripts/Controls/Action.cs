using UnityEngine;
using Utility.StateMachine;
using Utility.StateMachine.BaseStates;

namespace Controls
{
    public class Action : ScriptableState
    {
        public void Initialize(Transform transform) => _transform = transform;
        
        protected Transform _transform;

        public Transform Transform => _transform;

        public virtual bool Evaluate() => true;
        
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