using UnityEngine;
using Utility.Easing;
using Utility.StateMachine;

namespace AI.States
{
    public class Dance : AIBaseState
    {
        public Dance(Entity owner, StackMachine stackMachine) : base(owner, stackMachine) {}

        private float _timer;

        private DanceMove _currentDanceMove;

        private sealed class DanceMove
        {
            private readonly Quaternion _origin;
            private readonly Quaternion _target;

            private readonly float _duration;
            private float _time;

            public DanceMove(Quaternion origin, Quaternion target, float duration)
            {
                _origin = origin;
                _target = target;
                _duration = duration;
                _time = 0f;
            }

            public bool Update(Transform transform, float deltaTime, EasingType easingType)
            {
                _time += deltaTime;

                var normalizedTime = _time / _duration;
                
                transform.localRotation = Quaternion.Lerp(_origin, _target, Functions.GetEaseValue(easingType, normalizedTime));

                if (normalizedTime >= 1f)
                    return true;

                return false;
            }
        }

        public override void OnEnter()
        {
            _currentDanceMove = GenerateDanceMove();
        }

        public override StateReturn OnUpdate()
        {
            // Increment timer for current dance move
            var finished = _currentDanceMove.Update(Owner.Transform, Time.deltaTime, Owner.DanceEasing);
            
            if (finished)
            {
                // Check if dancing is over
                if (Random.value <= Owner.DanceChance)
                {
                    _currentDanceMove = GenerateDanceMove();
                }
                else
                {
                    return StateReturn.Completed;
                }
            }
            
            return StateReturn.Running;
        }

        private DanceMove GenerateDanceMove()
        {
            var newDanceMove = new DanceMove(
                Owner.Transform.localRotation,
                Random.rotation,
                    .5f
                );

            return newDanceMove;
        }
    }
}
