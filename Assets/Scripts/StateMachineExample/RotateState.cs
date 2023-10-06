using UnityEngine;
using Utility.StateMachine;
using Utility.StateMachine.BaseStates;

public class RotateState : MonoState
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationLifeTime;

    private float _timer;

    public override void OnEnter()
    {
        Debug.Log("Enter!");
        _timer = 0;
    }

    public override StateReturn OnUpdate()
    {
        _timer += Time.deltaTime;
        
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        if(_timer < rotationLifeTime)
            return StateReturn.Running;

        return StateReturn.Completed;
    }

    public override void OnExit()
    {
        Debug.Log("Exit!");
    }
}