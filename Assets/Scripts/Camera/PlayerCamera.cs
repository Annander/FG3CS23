using System;
using Controls;
using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    [RequireComponent(typeof(Player))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private FloatVariable lookSpeed;
        
        [SerializeField] private Vector2Variable lookLimits;
        
        [SerializeField] private InputContextEvent look;

        [SerializeField] private Transform target;

        private Quaternion _origin;

        private InputContextListener _lookListener;

        private float _yLookInput;
        
        private float _yLook = 0f;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _origin = _player.Camera.localRotation;
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
            _player.Camera.localRotation = Quaternion.AngleAxis(-_yLook, Vector3.right) * _origin;
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                _player.Camera.forward = (target.position - transform.position).normalized;
            }
        }

        private void OnLook(InputAction.CallbackContext a)
        {
            var lookVector = a.ReadValue<Vector2>();
            _yLookInput = lookVector.y;
        }
    }
}