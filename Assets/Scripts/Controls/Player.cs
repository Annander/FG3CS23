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
            var worldVector = new Vector3(inputVector.x, 0, inputVector.y);
            var transformedMoveVector = transform.TransformDirection(worldVector.normalized);

            _moveVector = transformedMoveVector * moveSpeed.Value;
        }

        private void OnLook(InputAction.CallbackContext a)
        {
            var inputVector = a.ReadValue<Vector2>();
            var rotation = (inputVector.x * turnSpeed.Value) * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            HandleMovement(deltaTime);
        }

        private void HandleMovement(float deltaTime)
        {
            transform.position += _moveVector * deltaTime;
        }
    }
}