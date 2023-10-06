using UnityEngine;

public class StateExample : MonoBehaviour
{
    [SerializeField] private States currentState;

    [Header("Bounce")] [SerializeField] private AnimationCurve bounceCurve;

    [SerializeField] private float bounceDuration;

    private float _time;

    [Header("Rotate")] [SerializeField] private float rotationSpeed;
    
    public enum States
    {
        Idle,
        Bounce,
        Rotate,
    }
    
    private void Update()
    {
        if(currentState == States.Bounce)
            Bounce();
        
        if(currentState == States.Rotate)
            Rotate();

        /*
        switch (currentState)
        {
            case States.Bounce:
                Bounce();
                break;
            
            case States.Rotate:
                Rotate();
                break;
            
            case States.Idle: break;
            
            default: break;
        }
        */
    }

    private void Bounce()
    {
        _time += Time.deltaTime;
        
        var position = transform.position;
        var newPosition = new Vector3(position.x, bounceCurve.Evaluate(_time / bounceDuration), position.z);

        transform.position = newPosition;

        if (_time > bounceDuration)
        {
            _time -= bounceDuration;
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}