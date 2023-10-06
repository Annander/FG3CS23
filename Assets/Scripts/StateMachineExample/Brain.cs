using UnityEngine;
using Utility.StateMachine;

public class Brain : MonoBehaviour
{
    private StackMachine _stackMachine;

    private IState[] _states;

    private void Awake()
    {
        _stackMachine = new StackMachine();
        _states = GetComponents<IState>();
        _stackMachine.PushState(_states[0]);
    }

    private void Update()
    {
        _stackMachine.Update();
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            _stackMachine.OnDrawGizmosSelected(transform.position);
        }
    }
}