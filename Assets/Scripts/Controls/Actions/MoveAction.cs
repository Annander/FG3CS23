using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility.StateMachine;

namespace Controls.Actions
{
    [CreateAssetMenu(fileName = "New Move Action", menuName = "Actions/Move")]
    public class MoveAction : Action,
        ISerializationCallbackReceiver
    {
        [Header("Events")]
        [SerializeField] private InputContextEvent move;
        [SerializeField] private InputContextEvent look;
        
        [Header("Data")]
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private FloatVariable turnSpeed;
        [SerializeField] private CurveVariable accelerationCurve;
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
            _hasEntered = false;
            _rotation = 0f;
            _accelerationDuration = 0f;
            _moveVector = Vector3.zero;
        }

        public override void OnEnter()
        {
            _moveListener ??= new InputContextListener(move);
            _moveListener.Response += OnMove;
            
            _lookListener ??= new InputContextListener(look);
            _lookListener.Response += OnLook;

            _hasEntered = true;
        }

        public override StateReturn OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            
            HandleMovement(deltaTime);
            HandleRotation(deltaTime);
        
            return StateReturn.Running;
        }

        public override void OnExit()
        {
            _moveListener.Response -= OnMove;
            _lookListener.Response -= OnLook;
        }
    
        private void OnMove(InputAction.CallbackContext a)
        {
            var phase = a.phase;
            
            if (phase == InputActionPhase.Started)
            {
                _accelerationDuration = 0f;
            }
            else if (phase == InputActionPhase.Canceled)
            {
                
            }
            
            var inputVector = a.ReadValue<Vector2>();
            
            _moveVector.x = inputVector.x;
            _moveVector.y = 0;
            _moveVector.z = inputVector.y;
        }
        
        private void OnLook(InputAction.CallbackContext a)
        {
            var inputVector = a.ReadValue<Vector2>();
            _rotation = inputVector.x;
        }        
    
        private void HandleMovement(float deltaTime)
        {
            _accelerationDuration += deltaTime;
            _accelerationDuration = Mathf.Clamp(_accelerationDuration, 0, accelerationTime.Value);

            var normalizedAcceleration = _accelerationDuration / accelerationTime.Value;
            var frameSpeed = moveSpeed.Value * accelerationCurve.Value.Evaluate(normalizedAcceleration); 
            
            var transformedMoveVector = _transform.TransformDirection(_moveVector.normalized) * frameSpeed;
            _transform.position += transformedMoveVector * deltaTime;
        }
        
        private void HandleRotation(float deltaTime)
        {
            _transform.Rotate(Vector3.up, (_rotation * turnSpeed.Value) * deltaTime);
        }
    }
}