using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 from;
    [SerializeField] private Vector3 to;

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private float duration;

    private Vector3 _oldPosition;
    private Vector3 _velocity;

    private Vector3 _from;
    private Vector3 _to;

    private float _time;

    public Vector3 Velocity => _velocity;

    private void Awake()
    {
        _from = from;
        _to = to;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        var t = _time / duration;
        
        _oldPosition = transform.localPosition;
        
        transform.localPosition = Vector3.Lerp(_from, _to, curve.Evaluate(t));

        _velocity = transform.localPosition - _oldPosition;
        
        Debug.DrawRay(transform.position, _velocity, Color.red);

        if (_time >= duration)
        {
            _time -= duration;
            Flip();
        }
    }

    private void Flip()
    {
        if (_from == from)
        {
            _from = to;
            _to = from;
        }
        else
        {
            _from = from;
            _to = to;
        }
    }

    private void OnDrawGizmosSelected()
    {
        DebugUtility.DrawGizmoCross(transform.TransformPoint(from), Color.green);
        DebugUtility.DrawGizmoCross(transform.TransformPoint(to), Color.red);
    }
}