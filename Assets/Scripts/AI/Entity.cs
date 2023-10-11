using AI.States;
using UnityEngine;
using Utility.Easing;
using Utility.StateMachine;

namespace AI
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float radius;
        [SerializeField] private float aggroRadius;
        [SerializeField] private EasingType danceEasing;
        [SerializeField] private float danceChance;

        #region Getters
        public Transform Transform => _transform;
        public float MoveSpeed => moveSpeed;        
        public float Radius => radius;
        public float AggroRadius => aggroRadius;
        public EasingType DanceEasing => danceEasing;
        public float DanceChance => danceChance;
        #endregion
        
        private StackMachine _brain;

        private Idle _idleState;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _brain = new StackMachine();
            
            _idleState = new Idle(this, _brain);
            
            _brain.PushState(_idleState);
        }

        private void Update()
        {
            _brain.Update();
        }

        private void OnDrawGizmosSelected()
        {
            if(Application.isPlaying)
                _brain.OnDrawGizmosSelected(_transform.position);
        }
    }
}