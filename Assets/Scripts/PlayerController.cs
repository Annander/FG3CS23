using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), 
    typeof(Rigidbody),
    typeof(Grounder))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float turnSpeed;

    private float _currentMoveSpeed;

    [Header("Looking")] 
    [SerializeField] private Transform headTransform;
    [SerializeField] private float lookSpeed;
    [SerializeField] private Vector2 lookLimits = new Vector2(-90f, 90f);
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    
    private PlayerInput _playerInput;

    private Vector3 _moveVector;
    private float _rotation;
    private float _yLookInput;
    private float _yLook;

    private Quaternion _origin;
    private Transform _transform;

    private Rigidbody _rigidbody;

    private Grounder _grounder;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInput = GetComponent<PlayerInput>();

        _grounder = GetComponent<Grounder>();
        _grounder.OnLanding += CameraShake.Instance.Shake;
       
        _playerInput.actions["Move"].performed += OnMove;
        _playerInput.actions["Move"].started += OnMove;
        _playerInput.actions["Move"].canceled += OnMove;
        
        _playerInput.actions["Look"].performed += OnLook;
        _playerInput.actions["Look"].started += OnLook;
        _playerInput.actions["Look"].canceled += OnLook;
        
        _playerInput.actions["Sprint"].performed += OnSprint;
        _playerInput.actions["Sprint"].started += OnSprint;
        _playerInput.actions["Sprint"].canceled += OnSprint;
        
        _playerInput.actions["Primary"].started += OnPrimary;

        _origin = headTransform.localRotation;

        _currentMoveSpeed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnSprint(InputAction.CallbackContext obj)
    {
        if (obj.phase == InputActionPhase.Started)
        {
            _currentMoveSpeed = runSpeed;
        }

        if (obj.phase == InputActionPhase.Canceled)
        {
            _currentMoveSpeed = walkSpeed;
        }
    }

    private void OnPrimary(InputAction.CallbackContext obj)
    {
        if (_grounder.IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            CameraShake.Instance.Shake(.75f);
            //CameraDisplace.Instance.Displace(.75f, Vector2.down);
        }
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

        // Transform move vector and move player0
        var transformedMoveVector = _transform.TransformDirection(_moveVector.normalized);
        var forwardDot = Vector3.Dot(_transform.forward, transformedMoveVector);
        
        if(forwardDot >= .75f)
            transformedMoveVector *= _currentMoveSpeed;
        else
            transformedMoveVector *= Mathf.Clamp(_currentMoveSpeed, 0, walkSpeed);
        
        _transform.position += transformedMoveVector * deltaTime;
        
        // Rotate player root
        _transform.Rotate(Vector3.up, (_rotation * turnSpeed) * deltaTime);
        
        // Clamp and rotate camera
        _yLook += _yLookInput * lookSpeed;
        _yLook = Mathf.Clamp(_yLook, lookLimits.x, lookLimits.y);
        headTransform.localRotation = Quaternion.AngleAxis(-_yLook, Vector3.right) * _origin;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();
        
        _moveVector.x = inputVector.x;
        _moveVector.y = 0;
        _moveVector.z = inputVector.y;
    }
}