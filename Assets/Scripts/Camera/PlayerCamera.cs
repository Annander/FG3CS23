using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private FloatVariable lookSpeed;
        
        [SerializeField] private Vector2Variable lookLimits;
        
        [SerializeField] private InputContextEvent look;

        [SerializeField] private Transform target;

        private Transform _noiseTransform;

        private Transform _transform;
        
        private Quaternion _origin;

        private InputContextListener _lookListener;

        private float _yLookInput;
        
        private float _yLook = 0f;

        private void Awake()
        {
            _transform = transform;
            _noiseTransform = _transform.parent;
            _origin = _transform.localRotation;
        }

        private void OnEnable()
        {
            _lookListener ??= new InputContextListener(look);
            _lookListener.Response += OnLook;
        }

        private void OnDisable()
        {
            _lookListener.Response -= OnLook;
        }

        private void Update()
        {
            _yLook += _yLookInput * lookSpeed.Value;
            _yLook = Mathf.Clamp(_yLook, lookLimits.Value.x, lookLimits.Value.y);
            _transform.localRotation = Quaternion.AngleAxis(-_yLook, Vector3.right) * _origin;
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                _transform.forward = (target.position - transform.position).normalized;
            }
        }

        private void OnLook(InputAction.CallbackContext a)
        {
            var lookVector = a.ReadValue<Vector2>();
            _yLookInput = lookVector.y;
        }
    }
}