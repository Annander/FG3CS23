using UnityEngine;

[RequireComponent(typeof(EventsInUnity))]
public class EventObserver : MonoBehaviour
{
    private void Awake()
    {
        var eventsInUnity = GetComponent<EventsInUnity>();
        eventsInUnity.OnBeautifulDelegate += ObserverOnBeautifulDelegate;
    }

    private void OnDestroy()
    {
        var eventsInUnity = GetComponent<EventsInUnity>();
        eventsInUnity.OnBeautifulDelegate -= ObserverOnBeautifulDelegate;
    }

    private void ObserverOnBeautifulDelegate(Vector3 position)
    {
        Debug.Log("Observer listened to the cool event thing!");
    }
}
