using Systemics.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionLine : MonoBehaviour
{
    [SerializeField] private InputContextEvent move;

    private InputContextListener _moveListener;

    private Vector3 _moveVector;

    private void Awake()
    {
        _moveListener = new InputContextListener(move);
        _moveListener.Response += OnMove;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, _moveVector);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>();

        _moveVector.x = vector.x;
        _moveVector.y = 0f;
        _moveVector.z = vector.y;
    }
}