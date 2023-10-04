using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundingDistance;
    [SerializeField] private Vector2 lookLimits = new Vector2(-90f, 90f);
    
    private PlayerInput _playerInput;

    private Vector3 _moveVector;
    private float _rotation;
    private float _yLookInput;
    private float _yLook;

    private Quaternion _origin;
    private Transform _cameraTransform;
    private Transform _transform;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInput = GetComponent<PlayerInput>();
        
        _playerInput.actions["Move"].performed += OnMove;
        _playerInput.actions["Move"].started += OnMove;
        _playerInput.actions["Move"].canceled += OnMove;
        
        _playerInput.actions["Look"].performed += OnLook;
        _playerInput.actions["Look"].started += OnLook;
        _playerInput.actions["Look"].canceled += OnLook;
        
        _playerInput.actions["Primary"].started += OnPrimary;

        _cameraTransform = GetComponentInChildren<UnityEngine.Camera>().transform;

        _origin = _cameraTransform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPrimary(InputAction.CallbackContext obj)
    {
        if(IsGrounded())
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();
        _rotation = inputVector.x;
        _yLookInput = inputVector.y;
    }

    private void Update()
    {
        // Store deltaTime
        var deltaTime = Time.deltaTime;
        
        // Transform move vector and move player
        var transformedMoveVector = _transform.TransformDirection(_moveVector.normalized) * moveSpeed;
        _transform.position += transformedMoveVector * deltaTime;
        
        // Rotate player root
        _transform.Rotate(Vector3.up, (_rotation * turnSpeed) * deltaTime);
        
        // Clamp and rotate camera
        _yLook += _yLookInput * lookSpeed;
        _yLook = Mathf.Clamp(_yLook, lookLimits.x, lookLimits.y);
        _cameraTransform.localRotation = Quaternion.AngleAxis(-_yLook, Vector3.right) * _origin;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();
        
        _moveVector.x = inputVector.x;
        _moveVector.y = 0;
        _moveVector.z = inputVector.y;
    }

    private bool IsGrounded()
    {
        var position = _transform.position;
        return Physics.Linecast(position + Vector3.up, position + (Vector3.down * groundingDistance));
    }
}