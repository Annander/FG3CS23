using UnityEngine;

public class Thingamajing : MonoBehaviour
{
    [SerializeField] private new UnityEngine.Camera camera;

    [SerializeField] private float padding = .75f;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        var onScreen = _collider != null ? 
            IsBoundsOnScreen(_collider.bounds) : 
            IsOnScreen(transform.position);
        
        if (onScreen)
        {
            Debug.DrawRay(transform.position, Vector3.up * 5f, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.up * 5f, Color.red);
        }
    }

    private bool IsOnScreen(Vector3 worldPosition)
    {
        var viewportPoint = camera.WorldToViewportPoint(worldPosition);

        var lowerPadding = 1f - padding;

        if (viewportPoint.z > 0f)
        {
            if (viewportPoint.x > lowerPadding &&
                viewportPoint.x < padding &&
                viewportPoint.y > lowerPadding &&
                viewportPoint.y < padding)
                return true;
        }
        
        return false;
    }
    
    private bool IsBoundsOnScreen(Bounds bounds) => IsOnScreen(bounds.min) && IsOnScreen(bounds.max);
}
