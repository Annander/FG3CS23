using UnityEngine;
using Utility.StateMachine;

namespace AI.States
{
    public class Chase : AIBaseState
    {
        public Chase(Entity owner, StackMachine stackMachine) : base(owner, stackMachine) {}

        public Transform Target;
        
        public override StateReturn OnUpdate()
        {
            var direction = (Target.position - Owner.Transform.position).normalized;
            var distance = Vector3.Distance(Target.position, Owner.Transform.position);
            var frameSpeed = Mathf.Min(Owner.MoveSpeed, distance, Owner.MoveSpeed);

            Owner.Transform.position += direction * (frameSpeed * Time.deltaTime);

            if (distance > Owner.Radius && distance < Owner.AggroRadius)
                return StateReturn.Running;

            return StateReturn.Completed;
        }
    }
}
