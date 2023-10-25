using UnityEngine;

public class AveragePosition : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var position = transform.position;
        var velocity = _rigidbody.velocity;
        var projectPoint = position + velocity;

        var averagePoint = (position + projectPoint) / 2;
        
        DebugUtility.DrawCross(averagePoint, Color.blue);
        DebugUtility.DrawCross(projectPoint, Color.red);
    }
}