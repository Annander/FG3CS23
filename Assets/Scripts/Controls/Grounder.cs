using UnityEngine;

public class Grounder : MonoBehaviour
{
    [Header("Grounding")]
    [SerializeField] private float groundingDistance;

    public event LandingDelegate OnLanding;
    public delegate void LandingDelegate(float velocity);
    
    private float _velocity;
    private float _oldPosition;

    private bool _wasGrounded;
    
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _velocity = _oldPosition - transform.position.y;
        _oldPosition = _transform.position.y;

        var isGrounded = IsGrounded();

        if (isGrounded && !_wasGrounded)
        {
            OnLanding?.Invoke(_velocity);
            _wasGrounded = true;
        }

        _wasGrounded = isGrounded;
    }

    public bool IsGrounded()
    {
        var position = _transform.position;
        return Physics.Linecast(
            position + Vector3.up, 
            position + (Vector3.down * groundingDistance)
        );
    }
}
