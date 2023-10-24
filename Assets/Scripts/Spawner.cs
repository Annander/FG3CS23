using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int maxRate = 20;
    [SerializeField] private int minRate = 1;
    
    [SerializeField] private AnimationCurve inputCurve;
    [SerializeField] private float duration;

    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        var t = _time / duration;
        var curveEvaluation = inputCurve.Evaluate(t);

        var currentSpawnRate = (int)Mathf.Lerp(minRate, maxRate, curveEvaluation);
        
        Debug.Log(currentSpawnRate);

        var oldPosition = transform.position;

        transform.position = Vector3.up * curveEvaluation;
        
        Debug.DrawLine(oldPosition, transform.position, Color.red, .25f);
    }
}