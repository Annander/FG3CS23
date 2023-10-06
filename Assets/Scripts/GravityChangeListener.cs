using UnityEngine;

public class GravityChangeListener : MonoBehaviour
{
    private void OnEnable()
    {
        GravityController.Instance.OnGravitySwitch += OnGravitySwitch;
    }

    private void OnDisable()
    {
        GravityController.Instance.OnGravitySwitch -= OnGravitySwitch;
    }
    
    private void OnGravitySwitch(Vector3 newGravity)
    {
        // The changed gravity
    }    
}