using Systemics.Events;
using Systemics.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private InputContextEvent move;
    [SerializeField] private FloatVariable moveSpeed;

    private InputContextListener _moveListener;
    
    private Animator _animator;
    private Transform _transform;

    private Vector3 _moveVector;

    private static class AnimationHashes
    {
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int Direction = Animator.StringToHash("Direction");
    }

    private void Awake()
    {
        _transform = transform;
        
        _animator = GetComponent<Animator>();
        
        _moveListener = new InputContextListener(move);
        _moveListener.Response += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var moveUpdate = context.ReadValue<Vector2>();
        _moveVector.x = moveUpdate.x;
        _moveVector.y = 0f;
        _moveVector.z = moveUpdate.y;
    }

    private void Update()
    {
        var transformedMoveVector = _transform.TransformDirection(_moveVector);
        var angle = -Vector3.SignedAngle(transformedMoveVector.normalized, _transform.forward, Vector3.up);
        
        var speed = transformedMoveVector.z * moveSpeed.Value;
        
        _animator.SetFloat(AnimationHashes.Speed, speed == 0f ? 0f : Mathf.Max(speed, 1.5f));
        _animator.SetFloat(AnimationHashes.Direction, angle);
    }
}