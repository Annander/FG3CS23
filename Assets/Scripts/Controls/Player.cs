using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private FloatVariable turnSpeed;
        
        [SerializeField] private InputContextEvent move;
        [SerializeField] private InputContextEvent look;

        private InputContextListener _moveListener;
        private InputContextListener _lookListener;

        private Vector3 _moveVector;

        private float _rotation;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _moveListener ??= new InputContextListener(move);
            _moveListener.Response += OnMove;
            
            _lookListener ??= new InputContextListener(look);
            _lookListener.Response += OnLook;
        }
        
        private void OnDisable()
        {
            _moveListener.Response -= OnMove;
            _lookListener.Response -= OnLook;
        }

        private void OnMove(InputAction.CallbackContext a)
        {
            var inputVector = a.ReadValue<Vector2>();
            
            _moveVector.x = inputVector.x;
            _moveVector.y = 0;
            _moveVector.z = inputVector.y;
        }

        private void OnLook(InputAction.CallbackContext a)
        {
            var inputVector = a.ReadValue<Vector2>();
            _rotation = inputVector.x * turnSpeed.Value;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            HandleMovement(deltaTime);
            HandleRotation(deltaTime);
        }

        private void HandleMovement(float deltaTime)
        {
            var transformedMoveVector = transform.TransformDirection(_moveVector.normalized) * moveSpeed.Value;
            _transform.position += transformedMoveVector * deltaTime;
        }
        
        private void HandleRotation(float deltaTime)
        {
            transform.Rotate(Vector3.up, _rotation * deltaTime);
        }
    }
}