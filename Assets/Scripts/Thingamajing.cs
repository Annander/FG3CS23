using UnityEngine;

public class Thingamajing : MonoBehaviour
{
    [SerializeField] private float padding = .75f;

    private Collider _collider;

    private UnityEngine.Camera _camera;
    
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
            DebugUtility.DrawCross(transform.position, Color.green);
        }
        else
        {
            DebugUtility.DrawCross(transform.position, Color.red);
        }
    }

    private bool IsOnScreen(Vector3 worldPosition)
    {
        if (_camera == null)
        {
            _camera = UnityEngine.Camera.main;
        }
        else if (!_camera.isActiveAndEnabled)
        {
            _camera = UnityEngine.Camera.main;
        }

        if (_camera != null)
        {
            var viewportPoint = _camera.WorldToViewportPoint(worldPosition);

            var lowerPadding = 1f - padding;

            if (viewportPoint.z > 0f)
            {
                if (viewportPoint.x > lowerPadding &&
                    viewportPoint.x < padding &&
                    viewportPoint.y > lowerPadding &&
                    viewportPoint.y < padding)
                    return true;
            }
        }

        return false;
    }
    
    private bool IsBoundsOnScreen(Bounds bounds) => IsOnScreen(bounds.min) && IsOnScreen(bounds.max);
}
