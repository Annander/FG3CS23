using UnityEngine;

public class AimRetention : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            var direction = (target.position - _transform.position).normalized;
            _transform.forward = direction;
        }
        
        Debug.DrawRay(_transform.position, _transform.forward * 100f);
    }
}