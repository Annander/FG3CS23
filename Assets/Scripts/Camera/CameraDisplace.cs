using UnityEngine;
using Utility;

public class CameraDisplace : UnitySingleton<CameraDisplace>
{
    private float _duration;
    private float _inTime;
    private float _outTime;
    
    private Vector2 _displacement;
    private Vector3 _force;

    private void Update()
    {
        if (_outTime > 0)
        {
            _outTime -= Time.deltaTime;
            var value = _outTime / _duration;
            _force = Vector2.Lerp(_displacement, Vector2.zero, value);
        }
        else if (_inTime > 0)
        {
            _inTime -= Time.deltaTime;
            var value = _inTime / _duration;
            _force = Vector2.Lerp(Vector2.zero, _displacement, value);
        }

        transform.localPosition = _force;
    }

    public void Displace(float duration, Vector2 displacement)
    {
        _inTime = _outTime = _duration = duration * .5f;
        _displacement = displacement;
    }
}
