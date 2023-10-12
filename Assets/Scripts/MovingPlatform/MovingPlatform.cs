using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 from;
    [SerializeField] private Vector3 to;

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private float duration;

    private Vector3 _originPosition;
    private Vector3 _oldPosition;
    private Vector3 _velocity;

    private Vector3 _from;
    private Vector3 _to;

    private float _time;

    private Transform _transform;

    public Vector3 Velocity => _velocity;

    private void Awake()
    {
        _transform = transform;
        _originPosition = _transform.position;
        _from = _originPosition + from;
        _to = _originPosition + to;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        var t = _time / duration;
        
        _oldPosition = _transform.localPosition;
        
        var newPosition = Vector3.Lerp(_from, _to, curve.Evaluate(t));

        _transform.localPosition = newPosition;

        _velocity = newPosition - _oldPosition;

        if (_time >= duration)
        {
            _time -= duration;
        }
    }

    private void OnDrawGizmosSelected()
    {
        DebugUtility.DrawGizmoCross(_from, Color.green);
        DebugUtility.DrawGizmoCross(_to, Color.red);
    }
}