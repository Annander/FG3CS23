using System;
using UnityEngine;
using Utility;

public class GravityController : UnitySingleton<GravityController>
{
    public event SwitchGravity OnGravitySwitch;
    public delegate void SwitchGravity(Vector3 newGravity);

    [SerializeField] private Vector3 currentGravity;
    
    private Vector3 _previousGravity;

    private void Update()
    {
        if (_previousGravity != currentGravity)
        {
            OnGravitySwitch?.Invoke(currentGravity);
            _previousGravity = currentGravity;
        }
    }
}