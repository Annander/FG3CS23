using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoundingBoxVisualizer : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            var bounds = _collider.bounds;
            Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);            
        }
    }
}