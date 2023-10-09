using System;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

public class CameraShake : UnitySingleton<CameraShake>
{
    private float _duration;
    private float _currentTime;
    
    private Vector2 _shake;

    private void Update()
    {
        if (_currentTime > 0f)
        {
            _shake = Random.insideUnitCircle * RemainingTime;
            _currentTime -= Time.deltaTime;

            transform.localPosition = _shake;
        }
    }

    private float RemainingTime => _currentTime / _duration;

    public void Shake(float duration)
    {
        _currentTime = _duration = duration;
    }
}
