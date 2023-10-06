using UnityEngine;
using UnityEngine.Events;

public class EventsInUnity : MonoBehaviour
{
    [SerializeField] private UnityEvent UnityEventExample;

    public event MyBeautifulDelegate OnBeautifulDelegate;
    public delegate void MyBeautifulDelegate(Vector3 position);

    private void Awake()
    {
        OnBeautifulDelegate += OnOnBeautifulDelegate;
    }

    private void OnOnBeautifulDelegate(Vector3 position)
    {
        Debug.Log("The delegate event thingy called!");
    }

    private void Start()
    {
        UnityEventExample.Invoke();
        OnBeautifulDelegate?.Invoke(transform.position);
    }

    public void MethodCalledByUnityEvent()
    {
        Debug.Log("Event called!");
    }
}