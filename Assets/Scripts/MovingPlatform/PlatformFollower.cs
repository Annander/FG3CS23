using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformFollower : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundingDistance;
    
    private MovingPlatform _currentPlatform;

    private readonly RaycastHit[] _rayHits = new RaycastHit[4];

    private void Update()
    {
        PlatformCheck();
    }

    private void LateUpdate()
    {
        if(_currentPlatform)
            transform.position += _currentPlatform.Velocity;
    }

    private void PlatformCheck()
    {
        var position = transform.position;

        Debug.DrawRay(position + Vector3.up, Vector3.down * groundingDistance);
        
        var hitCount = Physics.RaycastNonAlloc(
            position + Vector3.up, 
            Vector3.down * groundingDistance,
            _rayHits,
            groundingDistance,
            layerMask
            );

        MovingPlatform platformCandidate = null;
        
        for (int i = 0; i < hitCount; i++)
        {
            var movingPlatform = _rayHits[i].collider.transform.root.GetComponentInChildren<MovingPlatform>();
            
            if(movingPlatform)
            {
                platformCandidate = movingPlatform;
            }
        }

        if (platformCandidate)
        {
            _currentPlatform = platformCandidate;
        }
        else
        {
            _currentPlatform = null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_currentPlatform)
        {
            UnityEditor.Handles.Label(transform.position, _currentPlatform.name);
        }
        else
        {
            UnityEditor.Handles.Label(transform.position, "No platform.");
        }
    }
#endif
}
