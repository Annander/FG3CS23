using UnityEngine;
using Utility.Easing;

public class BounceIn : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    
    [SerializeField] private EasingType easingType;
    [SerializeField] private float duration;

    private float _time;

    private void Start()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        var t = _time / duration;
        t = Mathf.Clamp01(t);

        transform.localScale = Vector3.one * curve.Evaluate(t);
    }
}
