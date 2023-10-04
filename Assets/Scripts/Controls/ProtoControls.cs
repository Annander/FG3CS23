using System;
using Systemics.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    [RequireComponent(typeof(PlayerInput))]
    public class ProtoControls : MonoBehaviour
    {
        [SerializeField] private InputContextEvent move;
        [SerializeField] private InputContextEvent look;
        [SerializeField] private InputContextEvent primary;
        
        private PlayerInput _playerInput;
        
        private void OnEnable()
        {
            _playerInput ??= GetComponent<PlayerInput>();
            
            BindKey("Move", OnMove);
            BindKey("Look", OnLook);
            BindKey("Primary", OnPrimary);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            BindKey("Move", OnMove, false);
            BindKey("Look", OnLook, false);
            BindKey("Primary", OnPrimary, false);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnMove(InputAction.CallbackContext context) => move.Raise(context);
        
        private void OnLook(InputAction.CallbackContext context) => look.Raise(context);

        private void OnPrimary(InputAction.CallbackContext context) => primary.Raise(context);

        private void BindKey(string action, Action<InputAction.CallbackContext> method, bool add = true)
        {
            if (add)
            {
                _playerInput.actions[action].started += method;
                _playerInput.actions[action].performed += method;
                _playerInput.actions[action].canceled += method;
            }
            else
            {
                _playerInput.actions[action].started -= method;
                _playerInput.actions[action].performed -= method;
                _playerInput.actions[action].canceled -= method;
            }
        }
    }
}
