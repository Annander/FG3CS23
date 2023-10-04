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
        
        [Header("Events")]
        [SerializeField] private InputContextEvent move;
        [SerializeField] private InputContextEvent look;
        
        [Header("Data")]
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private FloatVariable turnSpeed;
        
        [Header("Acceleration")]
        [SerializeField] private AccelerationType accelerationType;
        [SerializeField] private CurveVariable accelerationCurve;
        [SerializeField] private EasingVariable accelerationEasing;
        [SerializeField] private FloatVariable accelerationTime;
    
        private InputContextListener _moveListener;
        private Vector3 _moveVector;
        private float _accelerationDuration;
        
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
            _accelerationDuration = 0f;
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
                _accelerationDuration = 0f;
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
            // Update X-axis (horizontal) rotation
            var inputVector = a.ReadValue<Vector2>();
            _rotation = inputVector.x;
        }        
    
        private void HandleMovement(float deltaTime)
        {
            // Apply acceleration
            _accelerationDuration += deltaTime;
            _accelerationDuration = Mathf.Clamp(_accelerationDuration, 0, accelerationTime.Value);

            // Calculate frame speed
            var normalizedAcceleration = _accelerationDuration / accelerationTime.Value;
            var frameSpeed = moveSpeed.Value;

            // Apply acceleration
            if (accelerationType == AccelerationType.Curve)
                frameSpeed *= accelerationCurve.Value.Evaluate(normalizedAcceleration);
            else if (accelerationType == AccelerationType.Easing)
                frameSpeed *= Functions.GetEaseValue(accelerationEasing.Value, normalizedAcceleration);

            // Apply vector in world space
            var transformedMoveVector = _transform.TransformDirection(_moveVector.normalized) * frameSpeed;
            _transform.position += transformedMoveVector * deltaTime;
        }
        
        private void HandleRotation(float deltaTime)
        {
            // Rotate player on world space up axis
            _transform.Rotate(Vector3.up, (_rotation * turnSpeed.Value) * deltaTime);
        }
    }
}