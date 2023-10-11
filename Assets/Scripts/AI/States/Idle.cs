using UnityEngine;
using Utility.StateMachine;

namespace AI.States
{
    public class Idle : AIBaseState
    {
        private readonly Chase _chaseState;
        private readonly Dance _danceState;

        private float _danceCheck;
        
        public Idle(Entity owner, StackMachine stackMachine) : base(owner, stackMachine)
        {
            _chaseState = new Chase(owner, stackMachine);
            _danceState = new Dance(owner, stackMachine);
        }

        public override void OnEnter()
        {
            ResetDanceCheck();
        }

        public override StateReturn OnUpdate()
        {
            _danceCheck -= Time.deltaTime;

            if (_danceCheck <= 0)
            {
                if (Random.value <= Owner.DanceChance)
                {
                    StackMachine.PushState(_danceState);
                }
                
                ResetDanceCheck();
            }
            
            var distanceToPlayer = Vector3.Distance(Owner.Transform.position, PlayerController.Instance.transform.position);

            if (distanceToPlayer < Owner.AggroRadius && distanceToPlayer > Owner.Radius)
            {
                _chaseState.Target = PlayerController.Instance.transform;
                StackMachine.PushState(_chaseState);
            }

            return StateReturn.Running;
        }

        private void ResetDanceCheck()
        {
            _danceCheck = Random.Range(.5f, 3f);
        }
    }
}
