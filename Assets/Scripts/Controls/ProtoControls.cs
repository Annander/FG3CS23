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
        
        private PlayerInput _playerInput;
        
        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
            }
            
            BindKey("Move", OnMove);
            BindKey("Look", OnLook);
        }

        private void OnDisable()
        {
            BindKey("Move", OnMove, false);
            BindKey("Look", OnLook, false);   
        }

        private void OnMove(InputAction.CallbackContext obj) => move.Raise(obj);
        private void OnLook(InputAction.CallbackContext obj) => look.Raise(obj);

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
