using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility.Easing;
using Utility.StateMachine;

namespace Controls.Actions
{
    [CreateAssetMenu(fileName = "New Move Action", menuName = "Actions/Move")]
    public class MoveAction : Action,
        ISerializationCallbackReceiver
    {
        public enum AccelerationType
        {
            Curve,
            Easing,
        }

        [System.Serializable]
        public struct Acceleration
        {
            public AccelerationType type;
            public AnimationCurve curve;
            public EasingType easing;
            public float duration;
            
            private float _duration;

            public void Reset() => _duration = 0f;

            public void Update(float deltaTime)
            {
                _duration += deltaTime;
                _duration = Mathf.Clamp(_duration, 0, duration);
            }

            public float Value
            {
                get
                {
                    var t = _duration / duration;
                    
                    if (type == AccelerationType.Curve)
                        return curve.Evaluate(t);
                    
                    if (type == AccelerationType.Easing)
                        return Functions.GetEaseValue(easing, t);

                    return t;
                }
            }
        }
        
        [Header("Events")]
        [SerializeField] private InputContextEvent move;
        [SerializeField] private InputContextEvent look;
        
        [Header("Data")]
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private FloatVariable turnSpeed;

        [Header("Move Acceleration")]
        [SerializeField] private Acceleration moveAcceleration;
        
        [Header("Rotate Acceleration")]
        [SerializeField] private Acceleration rotationAcceleration;
    
        private InputContextListener _moveListener;
        private Vector3 _moveVector;
        
        private InputContextListener _lookListener;
        private float _rotation;    

        private bool _hasEntered;

        public override bool HasEntered => _hasEntered;
        
        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            // Reset variables, since this is an asset on disk
            _hasEntered = false;
            _rotation = 0f;
            moveAcceleration.Reset();
            rotationAcceleration.Reset();
            _moveVector = Vector3.zero;
        }

        public override void OnEnter()
        {
            // Subscribe to Move and Look events when this state enters
            _moveListener ??= new InputContextListener(move);
            _moveListener.Response += OnMove;
            
            _lookListener ??= new InputContextListener(look);
            _lookListener.Response += OnLook;

            // Mark this state as entered
            _hasEntered = true;
        }

        public override StateReturn OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            
            // Update movement and rotation
            HandleMovement(deltaTime);
            HandleRotation(deltaTime);
            
            // Return current status
            return StateReturn.Running;
        }

        public override void OnExit()
        {
            // Unsubscribe from Move and Look events when this state exits
            _moveListener.Response -= OnMove;
            _lookListener.Response -= OnLook;
        }
    
        private void OnMove(InputAction.CallbackContext a)
        {
            var phase = a.phase;
            
            if (phase == InputActionPhase.Started)
            {
                // Started moving
                moveAcceleration.Reset();
            }
            else if (phase == InputActionPhase.Canceled)
            {
                // Stopped moving
            }
            
            // Update move vector without allocating new memory
            var inputVector = a.ReadValue<Vector2>();
            
            _moveVector.x = inputVector.x;
            _moveVector.y = 0;
            _moveVector.z = inputVector.y;
        }
        
        private void OnLook(InputAction.CallbackContext a)
        {
            var phase = a.phase;

            if (phase == InputActionPhase.Started)
            {
                rotationAcceleration.Reset();
            }
            
            // Update X-axis (horizontal) rotation
            var inputVector = a.ReadValue<Vector2>();
            _rotation = inputVector.x;
        }        
    
        private void HandleMovement(float deltaTime)
        {
            // Apply acceleration
            moveAcceleration.Update(deltaTime);

            // Calculate frame speed
            var frameSpeed = moveSpeed.Value * moveAcceleration.Value;

            // Apply vector in world space
            var transformedMoveVector = _transform.TransformDirection(_moveVector.normalized) * frameSpeed;
            _transform.position += transformedMoveVector * deltaTime;
        }
        
        private void HandleRotation(float deltaTime)
        {
            // Apply acceleration
            //rotationAcceleration.Update(deltaTime);
            
            // Calculate frame rotation
            //var frameRotation = turnSpeed.Value * rotationAcceleration.Value;
            
            // Rotate player on world space up axis
            _transform.Rotate(Vector3.up, (_rotation * turnSpeed.Value) * deltaTime);
        }
    }
}