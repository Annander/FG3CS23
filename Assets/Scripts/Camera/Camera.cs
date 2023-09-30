using System;
using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private FloatVariable lookSpeed;
        
        [SerializeField] private Vector2Variable lookLimits;
        
        [SerializeField] private InputContextEvent look;

        [SerializeField] private Transform target;

        private Transform _noiseTransform;
        
        private Quaternion _origin;

        private InputContextListener _lookListener;
        
        private float _yLook;

        private void Awake()
        {
            _noiseTransform = transform.parent;
            _origin = transform.localRotation;
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
            if (_yLook > lookLimits.Value.x && _yLook < lookLimits.Value.y)
            {
                transform.localRotation = Quaternion.AngleAxis(-_yLook, Vector3.right) * _origin;
            }
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                transform.forward = (target.position - transform.position).normalized;
            }
        }

        private void OnLook(InputAction.CallbackContext a)
        {
            var lookVector = a.ReadValue<Vector2>();
            
            _yLook += (lookVector.y * lookSpeed.Value) * Time.deltaTime;
            _yLook = Mathf.Clamp(_yLook, lookLimits.Value.x, lookLimits.Value.y);
        }
    }
}